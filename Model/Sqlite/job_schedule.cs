using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Sqlite
{
    public class job_schedule
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string cron_expression { get; set; }
    }
}
