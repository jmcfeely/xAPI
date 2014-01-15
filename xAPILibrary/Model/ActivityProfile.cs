using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    public class ActivityProfile : State
    {
        public string activityId { get; set; }

        public string profileId { get; set; }

        public NullableDateTime timestamp { get; set; }
    }
}
