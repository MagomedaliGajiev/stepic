using MySql.Data.MySqlClient;
using stepic.Models;
using System.Data;

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
                    INSERT INTO stepik.users (full_name, details, join_date, avatar, is_active)
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
    public static User? Get(string fullName)
    {
        using var connection = new MySqlConnection(Constant.ConnectionString);
        connection.Open();
        var query = @"SELECT * FROM stepik.users
                   WHERE full_name = @FullName AND is_active = 1;";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@FullName", fullName);
        using var reader = command.ExecuteReader();
        return reader.Read()
            ? new User
            {
                FullName = reader.GetString("full_name"),
                Details = reader.IsDBNull("details") ? null : reader.GetString("details"),
                JoinDate = reader.GetDateTime("join_date"),
                Avatar = reader.IsDBNull("avatar") ? null : reader.GetString("avatar"),
                IsActive = reader.GetBoolean("is_active"),
                Knowledge = reader.GetInt32("knowledge"),
                Reputation = reader.GetInt32("reputation"),
                FollowersCount = reader.GetInt32("followers_count")
            }
            : null;
    }

    /// <summary>
    /// Получение общего количества пользователей
    /// </summary>
    public static int GetTotalCount()
    {
        using var connection = new MySqlConnection(Constant.ConnectionString);

        connection.Open();

        var query = @"
                     SELECT COUNT(id) FROM stepik.users;";

        using var command = new MySqlCommand(query, connection);

        var result = command.ExecuteScalar();

        return result != null ? Convert.ToInt32(result) : 0;
    }

    /// <summary>
    /// Форматирование показателей пользователя
    /// </summary>
    /// <param name="number">Число для форматирования</param>
    /// <returns>Отформатированное число</returns>
    public static string FormatUserMetrics(int number)
    {
        using var connection = new MySqlConnection(Constant.ConnectionString);
        connection.Open();

        var functionName = "format_number";

        using var command = new MySqlCommand(functionName, connection);

        command.CommandType = CommandType.StoredProcedure;

        var numberParam = new MySqlParameter("number", number)
        {
            Direction = ParameterDirection.Input
        };

        var resultParam = new MySqlParameter()
        {
            Direction = ParameterDirection.ReturnValue
        };

        command.Parameters.Add(numberParam);
        command.Parameters.Add(resultParam);

        command.ExecuteNonQuery();

        var result = resultParam.Value.ToString();

        return result == null ? string.Empty : result;
    }

    /// <summary>
    /// Рейтинг пользователей
    /// </summary>
    /// <returns>DataSet</returns>
    public static DataSet GetUserRating()
    {
        using var connection = new MySqlConnection(Constant.ConnectionString);
        connection.Open();

        var query = @"
        SELECT full_name, knowledge, reputation 
        FROM stepik.users 
        WHERE is_active = 1 
        ORDER BY knowledge DESC 
        LIMIT 10;
    ";

        using var command = new MySqlCommand(query, connection);
        using var adapter = new MySqlDataAdapter(command);

        var dataSet = new DataSet();
        adapter.Fill(dataSet); // Убрали второй параметр

        return dataSet;
    }

    /// <summary>
    /// Получение социальной информации пользователя
    /// </summary>
    /// <param name="userName">Имя пользователя</param>
    /// <returns>DataSet</returns>
    public DataSet GetUserSocialInfo(string userName)
    {
        using var connection = new MySqlConnection(Constant.ConnectionString);
        connection.Open();

        var query = @"CALL get_user_social_info(@user_name);";

        using var command = new MySqlCommand(query, connection);
        var userNameParam = new MySqlParameter("@username", userName);
        command.Parameters.Add(userNameParam);
        using var adapter = new MySqlDataAdapter(command);

        var dataSet = new DataSet();
        adapter.Fill(dataSet);

        return dataSet;
    }
}

