using MySql.Data.MySqlClient;
using stepic.Models;

namespace stepic.Services
{
    public class CommentsService
    {
        /// <summary>
        /// Получение всех комментариев к курсу
        /// </summary>
        /// <param name="id">id курса</param>
        /// <returns>Список комментариев</returns>
        public List<Comment> Get(int id)
        {
            var comments = new List<Comment>();

            using var connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();

            var query = @"
                          SELECT c.id, c.text, c.time
                          FROM stepik.comments AS c
                          JOIN stepik.steps AS s ON c.step_id = s.id
                          JOIN stepik.unit_lessons AS ul ON s.lesson_id = ul.lesson_id
                          JOIN stepik.units AS u ON ul.unit_id = u.id
                          JOIN stepik.courses AS cr ON u.course_id = cr.id
                          WHERE reply_comment_id IS NULL AND cr.id = @id
                          ORDER BY c.time DESC;";

            using var command = new MySqlCommand(query, connection);
            var idParam = new MySqlParameter("@id", id);
            command.Parameters.Add(idParam);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                comments.Add(new Comment()
                {
                    Id = reader.GetInt32("id"),
                    Text = reader.GetString("text"),
                    Time = reader.GetDateTime("time"),
                });
            }

            return comments;
        }

        /// <summary>
        /// Удаление комментария пользователя
        /// </summary>
        /// <param name="id">id комментария</param>
        /// <returns>Удалось ли удалить комментарий</returns>
        public bool Delete(int id)
        {
            using var connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();
            var transaction = connection.BeginTransaction();

            try
            {
                // Удаляем связанные записи в course_reviews
                using var command1 = new MySqlCommand(
                    "DELETE FROM course_reviews WHERE comment_id = @id;",
                    connection,
                    transaction);
                command1.Parameters.AddWithValue("@id", id);
                command1.ExecuteNonQuery();

                // Обнуляем ссылки в дочерних комментариях
                using var command2 = new MySqlCommand(
                    "UPDATE comments SET reply_comment_id = NULL WHERE reply_comment_id = @id;",
                    connection,
                    transaction);
                command2.Parameters.AddWithValue("@id", id);
                command2.ExecuteNonQuery();

                // Удаляем основной комментарий
                using var command3 = new MySqlCommand(
                    "DELETE FROM comments WHERE id = @id;",
                    connection,
                    transaction);
                command3.Parameters.AddWithValue("@id", id);
                int affectedRows = command3.ExecuteNonQuery();

                transaction.Commit();
                return affectedRows > 0;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
