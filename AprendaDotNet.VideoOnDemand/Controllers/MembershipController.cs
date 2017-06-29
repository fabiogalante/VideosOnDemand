using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprendaDotNet.VideoOnDemand.DtoModels;
using AprendaDotNet.VideoOnDemand.MembershipViewModels;
using AprendaDotNet.VideoOnDemand.Models;
using AprendaDotNet.VideoOnDemand.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AprendaDotNet.VideoOnDemand.Controllers
{
    public class MembershipController : Controller
    {
        private readonly string _userId;
        private readonly IReadRepository _db;
        private readonly IMapper _mapper;
        public MembershipController(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IMapper mapper, IReadRepository db)
        {
            // Get Logged in user's UserId
            var user = httpContextAccessor.HttpContext.User;
            _userId = userManager.GetUserId(user);
            _db = db;
            _mapper = mapper;
        }

        public IActionResult Dashboard()
        {
            var courseDtoObjects = _mapper.Map<List<CourseDto>>(_db.GetCourses(_userId));

            var dashboardModel = new DashboardViewModel
            {
                Courses = new List<List<CourseDto>>()
            };

            var noOfRows = courseDtoObjects.Count <= 3 ? 1 : courseDtoObjects.Count / 3;

            for (var i = 0; i < noOfRows; i++)
            {
                dashboardModel.Courses.Add(courseDtoObjects.Take(3).ToList());
            }



            return View(dashboardModel);
        }

        public IActionResult Course(int id)
        {
            var course = _db.GetCourse(_userId, id);

            var mappedCourseDtos = _mapper.Map<CourseDto>(course);

            var mappedInstructorDto = _mapper.Map<InstructorDto>(course.Instructor);


            var mappedModuleDtos = _mapper.Map<List<ModuleDto>>(course.Modules);

            for (var i = 0; i < mappedModuleDtos.Count; i++)
            {
                mappedModuleDtos[i].Downloads = course.Modules[i].Downloads.Count.Equals(0) ? null : _mapper.Map<List<DownloadDto>>(course.Modules[i].Downloads);
                mappedModuleDtos[i].Videos = course.Modules[i].Videos.Count.Equals(0) ? null :_mapper.Map<List<VideoDto>>(course.Modules[i].Videos);
            }
            var courseModel = new CourseViewModel
            {
                Course = mappedCourseDtos,
                Instructor = mappedInstructorDto,
                Modules = mappedModuleDtos
            };
            return View(courseModel);
        }

        [HttpGet]
        public IActionResult Video(int id)
        {
            var video = _db.GetVideo(_userId, id);
            var course = _db.GetCourse(_userId, video.CourseId);
            var mappedVideoDto = _mapper.Map<VideoDto>(video);
            var mappedCourseDto = _mapper.Map<CourseDto>(course);
            var mappedInstructorDto = _mapper.Map<InstructorDto>(course.Instructor);


            // Create a LessonInfoDto object
            var videos = _db.GetVideos(_userId, video.ModuleId).ToList();
            var count = videos.Count();
            var index = videos.IndexOf(video);
            var previous = videos.ElementAtOrDefault(index - 1);
            var previousId = previous == null ? 0 : previous.Id;
            var next = videos.ElementAtOrDefault(index + 1);
            var nextId = next == null ? 0 : next.Id;
            var nextTitle = next == null ? string.Empty : next.Title;
            var nextThumb = next == null ? string.Empty : next.Thumbnail;
            var videoModel = new VideoViewModel
            {
                Video = mappedVideoDto,
                Instructor = mappedInstructorDto,
                Course = mappedCourseDto,
                LessonInfo = new LessonInfoDto
                {
                    LessonNumber = index + 1,
                    NumberOfLessons = count,
                    NextVideoId = nextId,
                    PreviousVideoId = previousId,
                    NextVideoTitle = nextTitle,
                    NextVideoThumbnail = nextThumb
                }
            };
            return View(videoModel);
        }
    }
}
