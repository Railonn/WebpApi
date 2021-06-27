using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data 
{
    public class ApiDbContext : DbContext 
    {
        public virtual DbSet<UserData> Users {get;set;}
        public virtual DbSet<UserService> UsersServices {get;set;}
        public virtual DbSet<UserCategory> UsersCategories {get;set;}
        public virtual DbSet<UserMessage> UsersMessages {get;set;}

        public ApiDbContext(DbContextOptions<ApiDbContext> options) 
            :base(options)
        {
            
        }
    }
}