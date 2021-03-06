﻿using System.Collections.Generic;
using System.Linq;
using AprendaDotNet.VideoOnDemand.Data;
using AprendaDotNet.VideoOnDemand.Entities;

namespace AprendaDotNet.VideoOnDemand.Repositories
{
    public class SqlReadRepository : IReadRepository
    {
        private readonly ApplicationDbContext _db;
        public SqlReadRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Course> GetCourses(string userId)
        {

            //Lambda Expression
            var courses = _db.UserCourses.Where(uc => uc.UserId.Equals(userId))
                .Join(_db.Courses, uc => uc.CourseId, c => c.Id, (uc, c) => new { Course = c })
                .Select(s => s.Course).ToList();

            foreach (var course in courses)
            {
                course.Instructor = _db.Instructors.SingleOrDefault(s => s.Id.Equals(course.InstructorId));
                course.Modules = _db.Modules.Where(m => m.CourseId.Equals(course.Id)).ToList();
            }

            return courses;
        }
        public Course GetCourse(string userId, int courseId)
        {
            var course = _db.UserCourses.Where(uc => uc.UserId.Equals(userId))
                .Join(_db.Courses, uc => uc.CourseId, c => c.Id, (uc, c) => new { Course = c })
                .SingleOrDefault(s => s.Course.Id.Equals(courseId)).Course;

            course.Instructor = _db.Instructors.SingleOrDefault(s => s.Id.Equals(course.InstructorId));
            course.Modules = _db.Modules.Where(m => m.CourseId.Equals(course.Id)).ToList();

            foreach (var module in course.Modules)
            {
                module.Downloads = _db.Downloads.Where(d => d.ModuleId.Equals(module.Id)).ToList();
                module.Videos = _db.Videos.Where(v => v.ModuleId.Equals(module.Id)).ToList();
            }

            return course;
        }
        public Video GetVideo(string userId, int videoId)
        {
            var video = _db.Videos
                .Where(v => v.Id.Equals(videoId))
                .Join(_db.UserCourses, v => v.CourseId, uc => uc.CourseId, (v, uc) => new { Video = v, UserCourse = uc })
                .Where(vuc => vuc.UserCourse.UserId.Equals(userId))
                .FirstOrDefault().Video;

            return video;
        }
        public IEnumerable<Video> GetVideos(string userId, int moduleId = default(int))
        {
            var videos = _db.Videos
                .Join(_db.UserCourses, v => v.CourseId, uc => uc.CourseId, (v, uc) => new { Video = v, UserCourse = uc })
                .Where(vuc => vuc.UserCourse.UserId.Equals(userId));

            return moduleId.Equals(0) ?
                videos.Select(s => s.Video) :
                videos.Where(v => v.Video.ModuleId.Equals(moduleId)).Select(s => s.Video);
        }
    }
}
