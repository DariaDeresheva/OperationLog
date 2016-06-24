﻿using System.Collections.Generic;

namespace OpertaionLog.Database.Objects.Entities
{
    public class Program
    {
        public string ProgramId { get; set; }
        public string ProgramName { get; set; }
        public virtual List<Operation> Operations { get; set; }
    }
}
