using bookInstructorAPI.DB;
using bookInstructorAPI.Service.Interface;
using Microsoft.EntityFrameworkCore;
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

        public async Task<TblTransactionTimeSlot> Booking(AddInstructorModel addInstructor)
        {
            //int bookStartH = startDate.Hour;
            //int bookEndH = endDate.Hour;

            TimeSpan bookStart = new TimeSpan(addInstructor.TimeStart, 0, 0);
            TimeSpan bookEnd = new TimeSpan(addInstructor.TimeEnd, 0, 0);

            var book = new TblTransactionTimeSlot()
            {
                InstrutorCode = addInstructor.InstrutorCode,
                CreateDate = addInstructor.CreateDate,
                TimeStart = bookStart,
                TimeEnd = bookEnd
            };

            try
            {
                _context.Add(book);
                _context.SaveChanges();
            }catch(DbUpdateConcurrencyException e)
            {
                throw e;
            }

            return book;
        }

        public async Task<AvailableInstructorModel> AvailableInstructor(string instrutorCode, DateTime date)
        {
            var instructorsSlot = _context.TblInstructors
                .Where(e => e.InstructorCode == instrutorCode)
                .Select(e => new TblInstructor() { 
                    InstructorCode = e.InstructorCode,
                    FullName = e.FullName,
                    ActiveDay = e.ActiveDay,
                    Period = e.Period,
                    TimeStart = e.TimeStart,
                    TimeEnd = e.TimeEnd
                }).FirstOrDefault();

            string[] activeday = instructorsSlot.ActiveDay.Split('|');
            int[] convertedActiveday = Array.ConvertAll<string, int>(activeday, int.Parse);
            int today = (int)date.DayOfWeek;

            int startTimeFix = (int)instructorsSlot.TimeStart.Value.Hours;
            int endTimeFix = (int)instructorsSlot.TimeEnd.Value.Hours;
            int startTime = (int)instructorsSlot.TimeStart.Value.Hours;

            // loop for add timeslot by 1 hr period
            var slot = new List<TimeSlotModel>(); // int
            for (var i= startTimeFix; i<= endTimeFix-1; i++)
            {
                    slot
                    .Add(new TimeSlotModel()
                {
                    Start = startTime,
                    End = startTime+1,
                });
                startTime += 1;
            }

            // filter booked
            var booked = _context.TblTransactionTimeSlots
                .Where(e => e.InstrutorCode == instrutorCode && date == e.CreateDate)
                .Select(e => new
                {
                    Start = (int)e.TimeStart.Value.Hours,
                    End = (int)e.TimeEnd.Value.Hours,
                })
                .ToList();

            List<int> bookedStart = new List<int>();
            foreach (var b in booked)
            {
                bookedStart.Add(b.Start);
            }

            slot.RemoveAll(r => bookedStart.Any(a => a == r.Start));

            // check active day in today of instructor
            var avaList = new AvailableInstructorModel();
            if (convertedActiveday.Contains(today))
            {
                avaList = new AvailableInstructorModel()
                {
                    InstrutorCode = instrutorCode,
                    Date = date,
                    TimeSlot = slot
                };

            }
            else
            {
                avaList = new AvailableInstructorModel()
                {
                    InstrutorCode = instrutorCode,
                    Date = date,
                    TimeSlot = null
                };
            }

            return avaList;
        }
    }
}
