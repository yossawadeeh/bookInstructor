using bookInstructorAPI.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookInstructorAPI.Controllers
{
    [Route("api/BookInstructor")]
    [ApiController]
    public class BookInstructorController : ControllerBase
    {
        private readonly IBookInstructorService _bookInstructorService;
        private static IWebHostEnvironment _hostEnvironment;

        public BookInstructorController(IBookInstructorService bookInstructorService)
        {
            _bookInstructorService = bookInstructorService;
        }

        [HttpGet]
        public async Task<IActionResult> SelectInstructor()
        {
            try
            {
                var getInstructor = await _bookInstructorService.SelectInstructor();

                if (getInstructor == null)
                {
                    return NoContent();
                }

                return Ok(getInstructor);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
    }


}
