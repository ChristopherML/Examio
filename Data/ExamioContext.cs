using Microsoft.EntityFrameworkCore;
using Examio.Models;

namespace Examio.Data
{
    public class ExamioContext : DbContext
    {
        public ExamioContext(DbContextOptions<ExamioContext> options)
            : base(options)
        {
        }

        public DbSet<ExamSession> ExamSessions { get; set; }
        public DbSet<ExamSite> ExamSites { get; set; }
    }
}