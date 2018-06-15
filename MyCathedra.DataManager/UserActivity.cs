using System;

namespace MyCathedra.DataManager
{
    public class UserActivity
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Path { get; set; }
        public DateTime Data { get; set; }

    }
}