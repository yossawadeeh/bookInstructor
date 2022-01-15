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

        public async Task<TblTransactionTimeSlot> Booking(string instrutorCode, DateTime startDate, DateTime endDate)
        {
            DateTime createDate = DateTime.Now;
            int bookStartH = startDate.Hour;
            int bookEndH = endDate.Hour;

            TimeSpan bookStart = new TimeSpan(bookStartH, 0, 0);
            TimeSpan bookEnd = new TimeSpan(bookEndH, 0, 0);

            var book = new TblTransactionTimeSlot()
            {
                InstrutorCode = instrutorCode,
                CreateDate = createDate,
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

        public async Task<List<AvailableInstructorModel>> AvailableInstructor(string instrutorCode, DateTime date)
        {
            var avaList = new List<AvailableInstructorModel>();
            var slot = new List<TimeSlotModel>();

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

            //string[] words = instructorsSlot.ActiveDay.Split(',').Select(Int32.Parse).ToArray();
            string[] words = instructorsSlot.ActiveDay.Split(',');
            int today = (int)date.DayOfWeek;

            int startTimeFix = (int)instructorsSlot.TimeStart.Value.Hours;
            int endTimeFix = (int)instructorsSlot.TimeEnd.Value.Hours;
            int startTime = (int)instructorsSlot.TimeStart.Value.Hours;

            for (var i= startTimeFix; i<= endTimeFix-1; i++)
            {
                    slot.Add(new TimeSlotModel()
                {
                    Start = startTime,
                    End = startTime+1,
                });
                startTime += 1;
            }

            //if (words.Contains(today))
            //{
            //    avaList.Add(new AvailableInstructorModel()
            //    {
            //        InstrutorCode = instrutorCode,
            //        Date = date,
            //        TimeSlot = slot
            //    });
            //}
            //else
            //{
            //    avaList.Add(new AvailableInstructorModel()
            //    {
            //        InstrutorCode = instrutorCode,
            //        Date = date,
            //        //TimeSlot = { }
            //    });
            //}

            avaList.Add(new AvailableInstructorModel()
            {
                InstrutorCode = instrutorCode,
                Date = date,
                TimeSlot = slot
            });

            return avaList;
        }
    }
}
