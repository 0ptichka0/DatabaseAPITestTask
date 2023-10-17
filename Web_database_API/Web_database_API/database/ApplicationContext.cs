using Microsoft.EntityFrameworkCore;
using Web_database_API.models;

namespace Web_database_API.database
{
    public class ContactsAPIDbContext : DbContext
    {
        
        public ContactsAPIDbContext(DbContextOptions options) : base(options) 
        {
        
        }

        public DbSet<DatabaseInfo> Contacts { get; set; }


        //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Databases;Username=postgres;Password=1234");
        
    }
}
