﻿using stepic.Models;
using stepic.Services;

namespace stepic;

public class UsersProcessing()
{
    public User PerformRegistration()
    {
        var userName = "";
        while (string.IsNullOrEmpty(userName))
        {
            Console.WriteLine("Введите имя и фамилию через пробел и нажмите Enter:");
            userName = Console.ReadLine();
        }

        var newUser = new User
        {
            FullName = userName,
            JoinDate = DateTime.Now,
            IsActive = true
        };

        bool isAdditionSuccessful = ServiceProvider.usersService.Add(newUser);

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
            return new User();
        }
    }

    public User PerformLogin()
    {
        var userName = "";
        while (string.IsNullOrEmpty(userName))
        {
            Console.WriteLine("Введите имя и фамилию через пробел и нажмите Enter:");
            userName = Console.ReadLine();
        }

        User? user = ServiceProvider.usersService.Get(userName);

        if (user != null)
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
            return new User();
        }
    }
}