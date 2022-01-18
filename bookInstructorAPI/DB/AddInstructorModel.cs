using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookInstructorAPI.DB
{
    public class AddInstructorModel
    {
        public int Id { get; set; }
        public string InstrutorCode { get; set; }
        public DateTime? CreateDate { get; set; }
        public int TimeStart { get; set; }
        public int TimeEnd { get; set; }
    }
}
