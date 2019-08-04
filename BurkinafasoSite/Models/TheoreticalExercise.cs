﻿using System;
using System.Collections.Generic;

namespace BurkinafasoSite.Models
{
    public partial class TheoreticalExercise
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public byte[] Content { get; set; }
        public string Extn { get; set; }
        public string Language { get; set; }
        public int? FkModuleId { get; set; }

        public Module FkModule { get; set; }
    }
}
