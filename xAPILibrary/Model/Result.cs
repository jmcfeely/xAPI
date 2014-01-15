using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    /// <summary>
    /// Description: An optional field that represents a measured outcome related to the Statement in which it is included.
    /// </summary>
    public class Result : Extensible, IValidatable
    {
        #region Properties

        /// <summary>
        /// The score of the agent in relation to the success or quality of the experience.
        /// </summary>
        public Score score { get; set; }

        /// <summary>
        /// Indicates whether or not the attempt on the Activity was successful.
        /// </summary>
        public NullableBoolean success { get; set; }

        /// <summary>
        /// Indicates whether or not the Activity was completed. 
        /// </summary>
        public NullableBoolean completion { get; set; }

        /// <summary>
        /// A response appropriately formatted for the given Activity.
        /// </summary>
        public string response { get; set; }

        /// <summary>
        /// Period of time over which the Statement occurred. 
        /// </summary>
        public NullableDateTime duration { get; set; }

        /// <summary>
        /// A map of other properties as needed.
        /// </summary>
        public Extensible extensions { get; set; }

        #endregion
                
        #region Constructor
        public Result() { }
        #endregion

        #region Public Methods
        public IEnumerable<ValidationFailure> Validate(bool earlyReturnOnFailure)
        {
            object[] children = new object[] { score };
            var failures = new List<ValidationFailure>();
            foreach (object o in children)
            {
                if (o != null && o is IValidatable)
                {
                    failures.AddRange(((IValidatable)o).Validate(earlyReturnOnFailure));
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
