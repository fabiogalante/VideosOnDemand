using AprendaDotNet.VideoOnDemand.DtoModels;
using System.Collections.Generic;

namespace AprendaDotNet.VideoOnDemand.MembershipViewModels
{
    public class CourseViewModel
    {
        public CourseDto Course { get; set; }
        public InstructorDto Instructor { get; set; }
        public IEnumerable<ModuleDto> Modules { get; set; }
    }
}
