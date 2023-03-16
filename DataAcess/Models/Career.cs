using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Models
{
    public class Career
    {
        public Career()
        {
            CareerItems = new List<CareerItem>();
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public IList<CareerItem> CareerItems { get; set; }
    }
}
