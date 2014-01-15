using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    /// <definition>
    /// Something with which an Actor interacted. It can be a unit of instruction, experience, or performance that
    /// is to be tracked in a meaningful combination with a Verb. Interpretation of Activity is broad, meaning that
    /// Activities can even be tangible objects.
    /// </definition>
    public class Activity : StatementTarget, IValidatable
    {
        #region Properties

        /// <summary>
        /// MUST be "Activity" when present. Optional in all cases. 
        /// </summary>
        public override string ObjectType
        {
            get { return "Activity"; }
        }

        /// <summary>
        /// An identifier for a single unique Activity. Required.  
        /// </summary>
        public Uri id { get; set; }

        /// <summary>
        /// Optional Metadata. See ActivityDefinition.cs  
        /// </summary>
        public ActivityDefinition definition { get; set; }

        #endregion

        #region Constructor
        public Activity() { }

        public Activity(Uri id)
        {
            this.id = id;
        }

        public Activity(Uri id, ActivityDefinition definition)
        {
            this.id = id;
            this.definition = definition;
        }
        #endregion

        #region Public Methods
        public IEnumerable<ValidationFailure> Validate(bool earlyReturnOnFailure)
        {
            var failures = new List<ValidationFailure>();
            if (id == null)
            {
                failures.Add(new ValidationFailure("Activity does not have an identifier"));
                if (earlyReturnOnFailure)
                {
                    return failures;
                }
            }
            if (definition != null && definition is IValidatable)
            {
                failures.AddRange(((IValidatable)definition).Validate(earlyReturnOnFailure));
                if (earlyReturnOnFailure && failures.Count > 0)
                {
                    return failures;
                }
            }
            return failures;
        }
        #endregion
    }
}
