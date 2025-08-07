using Microsoft.AspNetCore.Mvc;
using stepic.Services.ADO.NET;

namespace stepic_webApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly CoursesService _coursesService;

        public CoursesController()
        {
            _coursesService = new CoursesService();
        }

        [HttpGet("GetUserCourses")]
        public IActionResult GetUsersCourses(string fullName)
        {
            var courses = _coursesService.Get(fullName);
            return (courses != null && courses.Any()) ? Ok(courses) 
                : NotFound("У пользователя не найдено курсов");
        }

        [HttpGet("GetTotalCoursesCount")]
        public IActionResult GetTotalCoursesCount()
        {
            var totalCount = _coursesService.GetTotalCount();
            return Ok(totalCount);
        }
    }
}
