using MedianProgram.Models;
using Microsoft.EntityFrameworkCore;

namespace MedianProgram.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<MedianModel> MedianModels { get; set; }
    }
}
