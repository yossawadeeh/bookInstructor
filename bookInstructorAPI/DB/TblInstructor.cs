using System;
using System.Collections.Generic;

#nullable disable

namespace bookInstructorAPI
{
    public partial class TblInstructor
    {
        public int Id { get; set; }
        public string InstructorCode { get; set; }
        public string FullName { get; set; }
        public string ActiveDay { get; set; }
        public int? Period { get; set; }
        public TimeSpan? TimeStart { get; set; }
        public TimeSpan? TimeEnd { get; set; }
    }
}
