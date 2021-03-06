﻿using System;
using SQLite;

namespace MyCathedra.DataManager
{
    public class Activity
    {
        [PrimaryKey] public Guid Id { get; set; }
        [Indexed] public Guid UserId { get; set; }
        public DateTime DataUtc { get; set; }
        [NotNull] public string File { get; set; }   //место и файл с которым работаю
        public ActivityType Type { get; set; }
        public string NewName { get; set; }   // новое имя при переименовании
    }
}