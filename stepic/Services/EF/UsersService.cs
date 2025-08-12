using Microsoft.EntityFrameworkCore;
using stepic.Models;
using System.Data;

namespace stepic.Services.EF;

public class UsersService : IUsersService
{
    public bool Add(User user)
    {
        throw new NotImplementedException();
    }

    public string? FormatUserMetrics(int number)
    {
        throw new NotImplementedException();
    }

    public User? Get(string fullName)
    {
        throw new NotImplementedException();
    }

    public int GetTotalCount()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Рейтинг пользователей (топ-10 по знаниям)
    /// </summary>
    /// <returns>DataSet</returns>
    public DataSet GetUserRating()
    {
        using ApplicationDbContext dbContext = new ();
        var topUsers = dbContext.Users
            .Where(u => u.IsActive)
            .AsNoTracking()
            .OrderByDescending(u => u.Knowledge)
            .Take(10)
            .Select(u => new
            {
                u.FullName,
                u.Knowledge,
                u.Reputation
            })
            .ToList();

        var dataTable = new DataTable("UserRaiting");
        dataTable.Columns.Add("full_name", typeof(string));
        dataTable.Columns.Add("knowledge", typeof(int));
        dataTable.Columns.Add("reputation", typeof(int));

        foreach (var user in topUsers)
        {
            dataTable.Rows.Add(user.FullName, user.Knowledge, user.Reputation);
        }

        var dataSet = new DataSet();
        dataSet.Tables.Add(dataTable);
        return dataSet;
    }

    public DataSet GetUserSocialInfo(string userName)
    {
        throw new NotImplementedException();
    }
}
