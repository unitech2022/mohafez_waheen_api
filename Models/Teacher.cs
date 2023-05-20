using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mohafezApi.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        public string? UseId { get; set; }
        public string? Name { get; set; }

        public string? Desc { get; set; }

             public string? Gender { get; set; }

        public string? Image { get; set; }

        public string? BannerImage { get; set; }
        public string? Specialty { get; set; } //  تخصص

        public double Rate { get; set; }
        public int Status { get; set; }
        public int CountStudent { get; set; }
        public string? Country { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Teacher()
        {
            CreatedAt = DateTime.UtcNow.AddHours(3);
            Status = 0; // 0 ====> active ; 1 ======> unActive
            Rate=0.0;
            CountStudent=0;



        }
    }
}