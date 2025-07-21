using stepic.Models;
using stepic.Services;

namespace stepic;

public class Program
{
    public static void Main()
    {
        var exitRequested = false;
        while (!exitRequested)
        {
            Console.WriteLine(@"
************************************************
* Добро пожаловать на онлайн платформу Stepik! *
************************************************

Выберите действие (введите число и нажмите Enter):

1. Войти
2. Зарегистрироваться
3. Закрыть приложение

************************************************
");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    LoginUser();
                    break;
                case "2":
                    RegisterUser();
                    break;
                case "3":
                    Console.WriteLine("До свидания!");
                    exitRequested = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }

        }
    }

    public static void RegisterUser()
    {
        Console.WriteLine("Введите имя и фамилию через пробел и нажмите Enter:");

        var fullName = Console.ReadLine().Trim();

        var user = new User
        {
            FullName = fullName
        };

        var isAdditionSuccessful = UsersService.Add(user);

        if (isAdditionSuccessful)
        {
            Console.WriteLine($"Пользователь {fullName} успешно добавлен.\n");
        }
        else
        {
            Console.WriteLine("Произошла ошибка, произведен выход на главную страницу\n");
        }
    }

    public static void LoginUser()
    {
        Console.WriteLine("Введите имя и фамилию через пробел и нажмите Enter:");
        var fullName = Console.ReadLine().Trim();

        var user = UsersService.Get(fullName);

        if (user != null)
        {
            Console.WriteLine($"Пользователь '{user.FullName}' успешно вошел\n");
        }
        else
        {
            Console.WriteLine("Пользователь не найден, произведен выход на главную страницу\n");
        }
    }
}

