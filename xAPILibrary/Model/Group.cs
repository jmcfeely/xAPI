using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    /// <definition>
    /// A group represents a collection of Agents and can be used in most of the same situations as an Agent can be used.
    /// There are two types of Groups, annonymous and identified
    /// 
    /// An ANNONYMOUS GROUP is used to describe a cluster of people where there is no ready identifier for this cluster,
    /// e.g. an ad hoc team
    /// Requirements: 1. An anonymous group MUST include a 'member' property listing constituent agents
    ///               2. An anonymous group MUST NOT contain Group Objects in the 'member' property
    ///               3. An anonymous group MUST NOT include any Inverse Functional Identifiers
    /// 
    /// An IDENTIFIED GROUP is used to uniquely identify a cluster of agents
    /// Requirements: 1. An identified group MUST include exactly one Inverse Functional Identifier
    ///               2. An identified group MUST NOT contain Group Objects in the 'member' property
    ///               3. An identified group SHOULD NOT use Inverse Functional Identifiers that are also used as Agent identifiers
    ///               4. An identified group MAY include a 'member' property listing constituent Agents
    /// </definition>
    public class Group : Actor
    {
        #region Properties

        /// <summary>
        /// "Group".
        /// </summary>
        public override string ObjectType
        {
            get { return "Group"; }
        }

        /// <summary>
        /// The members of this group.
        /// </summary>
        public List<Actor> member { get; set; }        

        #endregion
                
        #region Constructor
        public Group() : base() { }
        #endregion

        #region Public Methods
        public override IEnumerable<ValidationFailure> Validate(bool earlyReturnOnFailure)
        {
            var failures = new List<ValidationFailure>();
            if (member == null || member.Count == 0)
            {
                failures.Add(new ValidationFailure("Group must be populated"));
                if (earlyReturnOnFailure)
                {
                    return failures;
                }
            }
            else
            {
                foreach (Actor a in member)
                {
                    failures.AddRange(a.Validate(earlyReturnOnFailure));
                    if (earlyReturnOnFailure && failures.Count != 0)
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
