﻿using stepic.Models;
using stepic.Services;
using System.Data;

namespace stepic;

public class Program
{
    private readonly User _user; 
    /// <summary>
    /// Обработка начального меню
    /// </summary>
    public static void Main()
    {
        DisplayMainMenu();

        while (true)
        {
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    User user = PerformLogin();
                    if (!string.IsNullOrEmpty(user?.FullName))
                    {
                        HandleUserMenu(user);
                    }
                    break;
                case "2":
                    User newUser = PerformRegistration();
                    if (!string.IsNullOrEmpty(newUser?.FullName))
                    {
                        HandleUserMenu(newUser);
                    }
                    break;
                case "3":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("До свидания!\n");
                    Console.ResetColor();
                    return;
                default:
                    PrintWrongChoiceMessage();
                    break;
            }
        }
    }

    /// <summary>
    /// Отображение главного меню приложения.
    /// </summary>
    public static void DisplayMainMenu()
    {
        var totalCoursesCount = CoursesService.GetTotalCount();
        var totalUsersCount = UsersService.GetTotalCount();
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine(@$"
************************************************
* Добро пожаловать на онлайн платформу Stepik! *
************************************************
Количество курсов на платформе: {totalCoursesCount}
Количество пользователей на платформе: {totalUsersCount}

Выберите действие (введите число и нажмите Enter):

1. Войти
2. Зарегистрироваться
3. Закрыть приложение

************************************************

");
        Console.ResetColor();
    }

    /// <summary>
    /// Вывод сообщения об ошибке при неверном выборе.
    /// </summary>
    public static void PrintWrongChoiceMessage()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Неверный выбор. Попробуйте снова.");
        Console.ResetColor();
    }

    /// <summary>
    /// Регистрация нового пользователя.
    /// </summary>
    /// <returns>Возвращает объект пользователя, если регистрация успешна, иначе пустой объект.</returns>
    public static User PerformRegistration()
    {
        var userName = "";
        while (string.IsNullOrEmpty(userName))
        {
            Console.WriteLine("Введите имя и фамилию через пробел и нажмите Enter:");
            userName = Console.ReadLine();
        }

        var newUser = new User
        {
            FullName = userName
        };

        bool isAdditionSuccessful = UsersService.Add(newUser);

        if (isAdditionSuccessful)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Пользователь '{newUser.FullName}' успешно добавлен.\n");
            Console.ResetColor();
            return newUser;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Произошла ошибка, произведен выход на главную страницу.\n");
            Console.ResetColor();
            DisplayMainMenu();
            return new User();
        }
    }

    /// <summary>
    /// Вход пользователя в систему.
    /// </summary>
    /// <returns>Возвращает объект пользователя, если вход успешен, иначе пустой объект.</returns>
    public static User PerformLogin()
    {
        var userName = "";
        while (string.IsNullOrEmpty(userName))
        {
            Console.WriteLine("Введите имя и фамилию через пробел и нажмите Enter:");
            userName = Console.ReadLine();
        }

        User user = UsersService.Get(userName);

        if (!string.IsNullOrEmpty(user?.FullName))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Пользователь '{user.FullName}' успешно вошел.\n");
            Console.ResetColor();
            return user;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Пользователь не найден, произведен выход на главную страницу.\n");
            Console.ResetColor();
            DisplayMainMenu();
            return new User();
        }
    }

    /// <summary>
    /// Обработка меню пользователя после успешного входа.
    /// </summary>
    public static void HandleUserMenu(User user)
    {
        while (true)
        {
            DisplayUserMenu(user);
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    HandleProfileMenu(user);
                    break;
                case "2":
                    HandleUserCoursesMenu(user);
                    break;
                case "3":
                    DisplayMainMenu();
                    return;
                default:
                    PrintWrongChoiceMessage();
                    break;
            }
        }
    }

    /// <summary>
    /// Отображение меню пользователя.
    /// </summary>
    public static void DisplayUserMenu(User user)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@$"
* {user.FullName} *

Выберите действие (введите число и нажмите Enter):

1. Посмотреть профиль
2. Посмотреть курсы
3. Выйти
");
        Console.ResetColor();
    }

    /// <summary>
    /// Обработка меню профиля.
    /// </summary>
    public static void HandleProfileMenu(User user)
    {
        while (true)
        {
            DisplayProfileDetails(user);
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    return;
                default:
                    PrintWrongChoiceMessage();
                    break;
            }
        }
    }

    /// <summary>
    /// Отображение деталей профиля.
    /// </summary>
    public static void DisplayProfileDetails(User user)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(@$"
* {user.FullName} *

Выберите действие (введите число и нажмите Enter):

1. Назад

Профиль пользователя: {user.FullName}
Дата регистрации: {user.JoinDate}
Описание профиля: {user.Details ?? "Не заполнено"}
Фото профиля: {user.Avatar ?? "Не заполнено"}
{UsersService.FormatUserMetrics(user.FollowersCount)} подписчиков
{UsersService.FormatUserMetrics(user.Reputation)} репутация
{UsersService.FormatUserMetrics(user.Knowledge)} знания
");
        Console.ResetColor();
    }

    /// <summary>
    /// Обработка меню курсов пользователя.
    /// </summary>
    public static void HandleUserCoursesMenu(User user)
    {
        while (true)
        {
            DisplayUserCourses(user.FullName);
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    return;
                default:
                    PrintWrongChoiceMessage();
                    break;
            }
        }
    }

    /// <summary>
    /// Отображение списка курсов пользователя.
    /// </summary>
    private static void DisplayUserCourses(string fullName)
    {
        List<Course> courses = CoursesService.Get(fullName);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(@$"* Список курсов {fullName} *

Выберите действие (введите число и нажмите Enter):

1. Назад
");
        var count = 1;

        if (courses.Count == 0)
        {
            Console.WriteLine("У пользователя еще нет курсов.");
        }
        else
        {
            foreach (var course in courses)
            {
                Console.WriteLine(@$"
______________________________________________
{count}.
Название: {course.Title}
Описание: {course.Summary ?? "Отсутствует"}
Фото: {course.Photo ?? "Отсутствует"}
______________________________________________");
                count++;
            }
        }
        Console.ResetColor();
    }

    /// <summary>
    /// Отображение рейтинга пользователей.
    /// </summary>
    public static void DisplayUserRating()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n* Рейтинг пользователей *\n\n" +
                          "Выберите действие (введите число и нажмите Enter):\n" +
                          "1. Назад\n");

        var dataSet = UsersService.GetUserRating();

        if (dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
        {
            Console.WriteLine("На платформе еще нет пользователей");
            return;
        }

        var indent = 22;
        var separatorCount = 56;

        Console.WriteLine(new string('-', separatorCount));
        Console.WriteLine($"{"Пользователь".PadRight(indent)} " +
                          $"{"Знания".PadRight(indent)} " +
                          $"{"Репутация".PadRight(indent)}");
        Console.WriteLine(new string('-', separatorCount));

        foreach (DataRow row in dataSet.Tables[0].Rows)
        {
            Console.WriteLine($"{row["full_name"]?.ToString()?.PadRight(indent)} " +
                              $"{row["knowledge"]?.ToString()?.PadRight(indent)} " +
                              $"{row["reputation"]?.ToString()?.PadRight(indent)}");
        }

        Console.WriteLine(new string('-', separatorCount));
        Console.ResetColor();
    }

    public void Display()
    {
        var certificates = CertificatesService.Get(_user.FullName);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n* Сертификаты пользователя " + _user.FullName + " *\n\n" +
                          "Выберите действие (введите число и нажмите Enter):\n" +
                          "1. Назад\n");

        if (certificates.Tables.Count == 0 || certificates.Tables[0].Rows.Count == 0)
        {
            Console.WriteLine("У пользователя еще нет сертификатов");
            return;
        }

        var indent = 25;
        var separatorCount = 60;

        Console.WriteLine(new string('-', separatorCount));
        Console.WriteLine($"{"Курс".PadRight(indent)} " +
                          $"{"Дата выдачи".PadRight(indent)} " +
                          $"{"Оценка".PadRight(indent)}");
        Console.WriteLine(new string('-', separatorCount));

        foreach (DataRow row in certificates.Tables[0].Rows)
        {
            Console.WriteLine($"{row["title"]?.ToString()?.PadRight(indent)} " +
                              $"{row["issue_date"]?.ToString()?.PadRight(indent)} " +
                              $"{row["grade"]?.ToString()?.PadRight(indent)}");
        }

        Console.WriteLine(new string('-', separatorCount));
        Console.ResetColor();
    }
}

