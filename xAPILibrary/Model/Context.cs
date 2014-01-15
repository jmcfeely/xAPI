using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaLearning.xAPI.xAPILibrary.Helper;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    /// <summary>
    /// Description: An optional field that provides a place to add contextual information to a Statement. All properties 
    ///              are optional. 
    ///
    /// Rationale:   The "context" field provides a place to add some contextual information to a Statement. It can store 
    ///              information such as the instructor for an experience, if this experience happened as part of a team 
    ///              Activity, or how an experience fits into some broader activity. 
    /// 
    /// Requirement: The revision property MUST NOT be used if the Statement's Object is an Agent or Group. 
    /// Requirement: The platform property MUST NOT be used if the Statement's Object is an Agent or Group. 
    /// Requirement: The language property MUST NOT be used if not applicable or unknown. 
    /// Requirement: The revision property SHOULD be used to track fixes of minor issues (like a spelling error). 
    /// Requirement: The revision property SHOULD NOT be used if there is a major change in learning objectives, 
    ///              pedagogy, or assets of an Activity. (Use a new Activity id instead). 
    /// </summary>
    public class Context : Extensible, IValidatable
    {
        #region Properties

        /// <summary>
        /// The registration that the Statement is associated with. 
        /// </summary>
        public string registration
        {
            get { return registration; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    registration = null;
                }
                else
                {
                    string normalized = value.ToLower();
                    if (!ValidationHelper.IsValidUUID(normalized))
                    {
                        throw new ArgumentException("Registration must be UUID", "value");
                    }
                    registration = normalized;
                }
            }
        }

        /// <summary>
        /// Instructor that the Statement relates to, if not included as the Actor of the statement. 
        /// </summary>
        public Actor instructor { get; set; }

        /// <summary>
        /// Team that this Statement relates to, if not included as the Actor of the statement.
        /// </summary>
        public Group team { get; set; }

        /// <summary>
        /// A map of the types of learning activity context that this Statement is related to. Valid context 
        /// types are: "parent", "grouping", "category" and "other". 
        /// </summary>
        public ContextActivities contextActivities { get; set; }

        /// <summary>
        /// Revision of the learning activity associated with this Statement. Format is free. 
        /// </summary>
        public string revision { get; set; }

        /// <summary>
        /// Platform used in the experience of this learning activity. 
        /// </summary>
        public string platform { get; set; }

        /// <summary>
        /// Code representing the language in which the experience being recorded in this Statement (mainly) 
        /// occurred in, if applicable and known.
        /// </summary>
        public string language { get; set; } //RFC 5646 compliance validation needs to be added

        /// <summary>
        /// Another Statement which should be considered as context for this Statement. 
        /// </summary>
        public StatementRef statement { get; set; }

        /// <summary>
        /// A map of any other domain-specific context relevant to this Statement. For example, in a flight 
        /// simulator altitude, airspeed, wind, attitude, GPS coordinates might all be relevant 
        /// </summary>
        public Extensible extensions { get; set; }

        #endregion

        #region Constructor
        public Context() { }
        #endregion

        #region Public Methods
        /// <summary>
        /// Validates the context
        /// </summary>
        public IEnumerable<ValidationFailure> Validate(bool earlyReturnOnFailure)
        {
            object[] children = new object[] 
            { 
                registration, instructor, team, 
                contextActivities, revision, platform, statement 
            };
            var failures = new List<ValidationFailure>();
            foreach (object o in children)
            {
                if (o != null && o is IValidatable)
                {
                    failures.AddRange(((IValidatable)o).Validate(earlyReturnOnFailure));
                    if (failures.Count > 0)
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
