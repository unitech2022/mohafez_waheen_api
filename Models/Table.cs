using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mohafezApi.Models
{
    public class Table
    {
        public int Id { get; set; }

        public string? UserId { get; set; }
        public int TeacherId { get; set; }

        public int Status { get; set; }

        public string? Hours { get; set; }

        public string? Link { get; set; }

        public string? Note { get; set; }

        //  public DateTime Expired { get; set; }
        public string? Today { get; set; }
        public DateTime DateToday { get; set; }
        public DateTime? CreatedAt { get; set; }

        public Table()
        {
            CreatedAt = DateTime.UtcNow.AddHours(3);

          
            Status = 0; // 0 ====> active ; 1 ======> unActive





        }
    }
}
