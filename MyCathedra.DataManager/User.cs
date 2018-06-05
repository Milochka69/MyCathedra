using System;
using SQLite;

namespace MyCathedra.DataManager
{
    public class User
    {
        [PrimaryKey] public Guid Id { get; set; }
        [NotNull] [Unique] public string Login { get; set; }
        [NotNull] public string Password { get; set; }
    }
}