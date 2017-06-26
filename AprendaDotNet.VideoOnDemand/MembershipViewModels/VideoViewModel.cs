using AprendaDotNet.VideoOnDemand.DtoModels;

namespace AprendaDotNet.VideoOnDemand.MembershipViewModels
{
    public class VideoViewModel
    {
        public VideoDto Video { get; set; }
        public InstructorDto Instructor { get; set; }
        public CourseDto Course { get; set; }
        public LessonInfoDto LessonInfo { get; set; }
    }
}
