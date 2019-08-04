using System;
using System.Collections.Generic;

namespace BurkinafasoSite.Models
{
    public partial class Facts
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public byte[] Content { get; set; }
        public string Extn { get; set; }
        public string Language { get; set; }
        public int? ModuleId { get; set; }

        public Module Module { get; set; }
    }
}
