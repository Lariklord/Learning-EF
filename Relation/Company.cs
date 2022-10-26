using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relation
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual List<Worker> Workers { get; set; } = new();
    }
}
