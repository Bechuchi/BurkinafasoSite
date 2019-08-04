using System;
using System.Collections.Generic;

namespace BurkinafasoSite.Models
{
    public partial class GeneralDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string Extn { get; set; }
        public string Language { get; set; }
    }
}
