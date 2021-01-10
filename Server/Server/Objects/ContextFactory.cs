using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;


namespace Server.Objects
{
    class ContextFactory : IDesignTimeDbContextFactory<DBManagement>
    {
        public DBManagement CreateDbContext(string[] args)
        {
            string connectionString = @"Server=.\SQLEXPRESS;Database=ISITable;Trusted_Connection=True;";
            var builder = new DbContextOptionsBuilder<DBManagement>();

            builder.UseSqlServer(connectionString);
            return new DBManagement(builder.Options);
        }
    }
}
