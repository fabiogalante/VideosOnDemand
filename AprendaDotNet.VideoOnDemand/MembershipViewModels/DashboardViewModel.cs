using AprendaDotNet.VideoOnDemand.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprendaDotNet.VideoOnDemand.MembershipViewModels
{
    public class DashboardViewModel
    {
        public List<List<CourseDto>> Courses { get; set; }
    }
}
