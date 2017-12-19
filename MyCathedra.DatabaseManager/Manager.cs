using MyCathedra.DatabaseManager.Model;
using System.Data.Entity;

namespace MyCathedra.DatabaseManager
{
    public class Manager : DbContext
    {
        static Manager()
        {
            Database.SetInitializer(new DbInitializer());
            using (Manager db = new Manager())
                db.Database.Initialize(false);
        }

        public DbSet<RefDocumentCategory> RefDocumentCategories { get; set; }
        public DbSet<RefDocument> RefDocuments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }

    class DbInitializer : DropCreateDatabaseAlways<Manager>
    {
        protected override void Seed(Manager context)
        {
            base.Seed(context);
        }
    }
}
