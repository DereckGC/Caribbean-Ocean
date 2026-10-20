using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Domain.Entities
{
    public class MultimediaContent
    {
        public int id { get; set; }
        public string url { get; set; } = string.Empty;
        public string alt { get; set; } = string.Empty;
        public string publicID { get; set; } = string.Empty;
    }
}
