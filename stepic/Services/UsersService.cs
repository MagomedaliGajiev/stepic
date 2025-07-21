using MySql.Data.MySqlClient;
using stepic.Models;

namespace stepic.Services;

public class UsersService
{

    /// <summary>
    /// Добавление нового пользователя в таблицу users
    /// </summary>
    /// <param name="user">Новый пользователь</param>
    /// <returns>Удалось ли добавить пользователя</returns>
    public static bool Add(User user)
    {
        try
        {
            using (var connection = new MySqlConnection(Constant.ConnectionString))
            {
                connection.Open();

                var sqlQuery = @"
                    INSERT INTO users (full_name, details, join_date, avatar, is_active)
                    VALUES (@FullName, @Details, @JoinDate, @Avatar, @IsActive);";

                using (var command = new MySqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@FullName", user.FullName);
                    command.Parameters.AddWithValue("@Details", user.Details);
                    command.Parameters.AddWithValue("@JoinDate", user.JoinDate);
                    command.Parameters.AddWithValue("@Avatar", user.Avatar);
                    command.Parameters.AddWithValue("@IsActive", user.IsActive);

                    var execute = command.ExecuteNonQuery();
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Получение пользователя из таблицы users
    /// </summary>
    /// <param name="fullName">Полное имя пользователя</param>
    /// <returns>User</returns>
    public static User Get(string fullName)
    {
        using var connection = new MySqlConnection(Constant.ConnectionString);

        connection.Open();

        var sqlQuery = @"
                SELECT * 
                FROM users 
                WHERE full_name = @FullName AND is_active = true;";

        using var command = new MySqlCommand(sqlQuery, connection);

        command.Parameters.AddWithValue("@FullName", fullName);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new User
            {
                FullName = reader.GetString(0),
                Details = reader.IsDBNull(1) ? null : reader.GetString(1),
                JoinDate = reader.GetDateTime(2),
                Avatar = reader.IsDBNull(3) ? null : reader.GetString(3),
                IsActive = reader.GetBoolean(4)
            };
        }
        return null;
    }
}
