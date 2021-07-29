using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestDev.Data.Entities
{
    public class Module
    {
        public int Id { get; set; }
        public int SeriesId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
    }
}
