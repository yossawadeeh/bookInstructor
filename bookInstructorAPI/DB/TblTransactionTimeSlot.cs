using System;
using System.Collections.Generic;

#nullable disable

namespace bookInstructorAPI
{
    public partial class TblTransactionTimeSlot
    {
        public int Id { get; set; }
        public string InstrutorCode { get; set; }
        public DateTime? CreateDate { get; set; }
        public TimeSpan? TimeStart { get; set; }
        public TimeSpan? TimeEnd { get; set; }
    }
}
