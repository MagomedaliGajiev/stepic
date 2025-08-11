using Microsoft.EntityFrameworkCore;
using stepic.Models;

namespace stepic.Services.EF;

public class CoursesService : ICoursesService
{
    public List<Course> Get(string fullName)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Получение общего количества курсов
    /// </summary>
    public int GetTotalCount()
    {
        using ApplicationDbContext dbContext = new();
        return dbContext
            .Courses
            .AsNoTracking()
            .ToList()
            .Count;
    }
}
