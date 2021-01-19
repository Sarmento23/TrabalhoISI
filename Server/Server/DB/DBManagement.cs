using Microsoft.EntityFrameworkCore;
using Server.Objects;


namespace Server.Objects
{

    public class DBManagement : DbContext
    {
        public DBManagement(DbContextOptions<DBManagement> options) : base(options)
        {
        }

        //Tabelas base da dados
        public DbSet<Stadium> stadiums { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Ticket> tickets { get; set; }
        public DbSet<Sector> sectors { get; set; }


        //Relação entre tabelas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>().HasOne(p => p.Event).WithMany(b => b.tickets).HasForeignKey(o => o.EventID).HasPrincipalKey(i => i.ID);
            modelBuilder.Entity<Ticket>().HasOne(p => p.Stadium).WithMany(b => b.tickets).HasForeignKey(o => o.StadiumID).HasPrincipalKey(i => i.ID);
            modelBuilder.Entity<Event>().HasOne(p => p.Stadium).WithMany(b => b.Events).HasForeignKey(o => o.StadiumID).HasPrincipalKey(i => i.ID);
            modelBuilder.Entity<Sector>().HasOne(p => p.Stadium).WithMany(b => b.Sectors).HasForeignKey(o => o.StadiumID).HasPrincipalKey(i => i.ID);
        }

    }
}
