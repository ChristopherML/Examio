using Microsoft.EntityFrameworkCore;
using Examio.Models;
using System.Security.Cryptography.X509Certificates;

namespace Examio.Data
{
    public class ExamioContext : DbContext
    {
        public ExamioContext(DbContextOptions<ExamioContext> options)
            : base(options)
        {
        }

        public DbSet<ExamSession> ExamSession { get; set; }
        public DbSet<ExamSite> ExamSites { get; set; }
    }
}
