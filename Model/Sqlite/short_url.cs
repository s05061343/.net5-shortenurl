using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Sqlite
{
    public class short_url
    {
        [Key]
        public string id { get; set; }
        public string url { get; set; }
        public DateTime? create_date { get; set; } = DateTime.Now;
    }
}
