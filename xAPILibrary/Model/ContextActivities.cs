using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    public class ContextActivities : IValidatable
    {
        #region Properties

        public List<Activity> parent { get; set; }

        public List<Activity> grouping { get; set; }

        public List<Activity> other { get; set; }

        #endregion
                
        #region Constructor

        public ContextActivities() { }

        public ContextActivities(List<Activity> parent, List<Activity> grouping, List<Activity> other) 
        {
            this.parent = parent;
            this.grouping = grouping;
            this.other = other;
        }

        #endregion

        #region Public Methods

        public IEnumerable<ValidationFailure> Validate(bool earlyReturnOnFailure)
        {
            //Validate children
            object[] children = new object[] { parent, grouping, other };
            var failures = new List<ValidationFailure>();
            foreach (object child in children)
            {
                if (child != null && child is IValidatable)
                {
                    failures.AddRange(((IValidatable)child).Validate(earlyReturnOnFailure));
                    if (earlyReturnOnFailure && failures.Count > 0)
                    {
                        return failures;
                    }
                }
            }
            return failures;
        }

        #endregion
    }
}
