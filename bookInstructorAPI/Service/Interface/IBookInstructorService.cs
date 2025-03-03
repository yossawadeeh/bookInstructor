﻿using bookInstructorAPI.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookInstructorAPI.Service.Interface
{
    public interface IBookInstructorService
    {
        public Task<List<TblInstructor>> SelectInstructor();
        public Task<TblTransactionTimeSlot> Booking(AddInstructorModel addInstructor);
        public Task<AvailableInstructorModel> AvailableInstructor(string instrutorCode, DateTime date);
    }
}
