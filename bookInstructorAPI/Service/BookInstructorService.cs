using bookInstructorAPI.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookInstructorAPI.Service
{
    public class BookInstructorService : IBookInstructorService
    {
        private readonly BookInstructorsContext _context;

        public BookInstructorService(BookInstructorsContext context)
        {
            _context = context;
        }

        public async Task<List<TblInstructor>> SelectInstructor()
        {
            var res = _context.TblInstructors.Select(e => new TblInstructor()
            {
                Id = e.Id,
                InstructorCode = e.InstructorCode,
                FullName = e.FullName,
                ActiveDay = e.ActiveDay,
                Period = e.Period,
                TimeStart = e.TimeStart,
                TimeEnd = e.TimeEnd
            }).ToList();

            return res;
        }

        public async Task<TblTransactionTimeSlot> Booking(string InstrutorCode, DateTime startDate, DateTime endDate)
        {

        }
    }
}
