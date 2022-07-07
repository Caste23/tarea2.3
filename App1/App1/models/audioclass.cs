using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace App1.models
{
    public class audioclass
    {
        [AutoIncrement]
        public int id { get; set; }
        public string url { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha { get; set; }
    }
}
