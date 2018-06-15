using System;

namespace MyCathedra.DataManager
{
    public class LogInfo
    {
        public Guid Id { get; set; }
        public DateTime DataUtc { get; set; }
        public string File { get; set; }
        public ActivityType Type { get; set; }
        public string UserName { get; set; }
}
}