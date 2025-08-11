using Microsoft.EntityFrameworkCore;
using System.Data;

namespace stepic.Services.EF;

public class CertificatesService : ICertificatesService
{
    /// <summary>
    /// Получение сертификатов пользователя
    /// </summary>
    /// <param name="fullName">Полное имя пользователя</param>
    /// <returns>DataSet</returns>
    public DataSet Get(string fullName)
    {
        using ApplicationDbContext dbContext = new();

        var certificates = dbContext.Certificates
            .AsNoTracking()
            .Include(c => c.Course)
            .Where(c => c.User.FullName == fullName)
            .OrderByDescending(c => c.IssueDate)
            .Select(c => new
            {
                c.Course.Title,
                c.IssueDate,
                c.Grade
            })
            .ToList();

        var dataTable = new DataTable("Certificates");
        dataTable.Columns.Add("title", typeof(string));
        dataTable.Columns.Add("issue_date", typeof(DateTime));
        dataTable.Columns.Add("grade", typeof (int));

        foreach (var certificate in certificates)
        {
            dataTable.Rows.Add(certificate.Title, certificate.IssueDate, certificate.Grade);
        }

        var dataSet = new DataSet();
        dataSet.Tables.Add(dataTable);
        return dataSet;
    }
}