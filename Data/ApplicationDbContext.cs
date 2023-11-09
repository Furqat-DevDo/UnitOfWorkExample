using Microsoft.EntityFrameworkCore;
using UnitOfWorkExample.Entities;

namespace UnitOfWorkExample.Data;

public class ApplicationDbContext : DbContext
{
   public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}
        
   public virtual DbSet<User> Users { get; set; }
}
