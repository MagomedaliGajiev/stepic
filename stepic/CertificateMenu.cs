﻿using Org.BouncyCastle.Security;
using stepic.ADO.NET;
using stepic.Models;
using System.Data;

namespace stepic;

public record class CertificateMenu(User _user, WrongChoice _wrongChoice)
{
    private readonly CertificatesService _certificatesService = new CertificatesService();
    public void Display()
    {
        var certificates = _certificatesService.Get(_user.full_name);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n* Сертификаты пользователя " + _user.full_name + " *\n\n" +
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

    public void HandleUserChoice()
    {
        while (true)
        {
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var userMenu = new UserMenu(_user, _wrongChoice);
                    userMenu.Display();
                    userMenu.HandleUserChoice();
                    return;
                default:
                    _wrongChoice.PrintWrongChoiceMessage();
                    break;
            }
        }
    }
}
