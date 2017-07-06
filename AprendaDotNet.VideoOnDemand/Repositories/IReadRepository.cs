using System.Collections.Generic;
using AprendaDotNet.VideoOnDemand.Entities;

namespace AprendaDotNet.VideoOnDemand.Repositories
{
    public interface IReadRepository
    {
        IEnumerable<Course> GetCourses(string userId);

        Course GetCourse(string userId, int courseId);

        Video GetVideo(string userId, int videoId);

        IEnumerable<Video> GetVideos(string userId, int moduleId = default(int));
    }
}
