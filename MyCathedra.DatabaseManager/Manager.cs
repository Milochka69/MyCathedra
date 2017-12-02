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
    }

    class DbInitializer : DropCreateDatabaseAlways<Manager>
    {
        protected override void Seed(Manager context)
        {
            base.Seed(context);
        }
    }
}
