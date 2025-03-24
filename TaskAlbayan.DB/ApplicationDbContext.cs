
using Microsoft.EntityFrameworkCore;
using TaskAlbayan.DB.Models;


namespace TaskAlbayan.DB
{

    public class ApplicationDbContext :DbContext
    {
    public  ApplicationDbContext (DbContextOptions <ApplicationDbContext>options) : base(options){}
  
        public DbSet<User> Users { get; set; }
        public DbSet<Client> clients {get;set;}
        public DbSet<Task>tasks {get;set;}
        public DbSet<Visit> visits {get;set;}
        public DbSet<VisitTask>visitTasks {get;set;}


    }
    
}