using MySql.Data.MySqlClient;
using stepic.Models;

namespace stepic.Services
{
    public class CoursesService
    {
        /// <summary>
        /// Получение общего количества курсов
        /// </summary>
        public static int GetTotalCount()
        {
            using var connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();

            var query = "SELECT COUNT(id) FROM stepik.courses;";

            using var command = new MySqlCommand(query, connection);

            var result = command.ExecuteScalar();

            return result != null ? Convert.ToInt32(result) : 0;
        }

        /// <summary>
        /// Получение списка курсов пользователя
        /// </summary>
        /// <param name="fullName">Полное имя пользователя</param>
        /// <returns>Список курсов</returns>
        public static List<Course> Get(string fullName)
        {
            var courses = new List<Course>();

            using var connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();

            var query = @"
                        SELECT c.title, c.summary, c.photo 
                        FROM stepik.courses c
                        JOIN stepik.user_courses uc ON c.id = uc.course_id
                        JOIN stepik.users u ON uc.user_id = u.id
                        WHERE u.full_name = @fullName AND u.is_active = true
                        ORDER BY uc.last_viewed DESC";

            using var command = new MySqlCommand(query, connection);

            var fullNameParam = new MySqlParameter(@fullName, fullName);
            command.Parameters.Add(fullNameParam);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                courses.Add(new Course()
                {
                    Title = reader.GetString("title"),
                    Summary = reader.IsDBNull(reader.GetOrdinal("summary")) ? null : reader.GetString("suumary"),
                    Photo = reader.IsDBNull(reader.GetOrdinal("photo")) ? null : reader.GetString("photo"),
                });
            }

            return courses;
        }
    }
}
