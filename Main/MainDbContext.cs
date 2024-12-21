using Barber_shops.Models;
using Microsoft.EntityFrameworkCore;

namespace Barber_shops.Main
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }


        public DbSet<User> users { get; set; }

        public DbSet<imageupload> imageuploads { get; set; }
    }
}
