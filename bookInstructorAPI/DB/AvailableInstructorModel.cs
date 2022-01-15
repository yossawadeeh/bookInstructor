using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookInstructorAPI.DB
{
    public class AvailableInstructorModel
    {
        public string InstrutorCode { get; set; }
        public DateTime? Date { get; set; }
        public List<TimeSlotModel> TimeSlot { get; set; }
    }
}
