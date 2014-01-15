using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    public class AgentAccount : IValidatable
    {
        #region Properties

        public string homePage { get; set; }
        
        public string name { get; set; }

        #endregion

        #region Constructors
        
        public AgentAccount() { }

        public AgentAccount(string homepage, string id)
        {
            homePage = homepage;
            name = id;
        }  

        #endregion

        #region Public Methods
        /// <summary>
        /// Validates the object.
        /// </summary>
        public IEnumerable<ValidationFailure> Validate(bool earlyReturnOnFailure)
        {
            var failures = new List<ValidationFailure>();
            if (string.IsNullOrEmpty(homePage))
            {
                failures.Add(new ValidationFailure("Account service homepage cannot be null"));
                if (earlyReturnOnFailure)
                {
                    return failures;
                }
            }
            if (string.IsNullOrEmpty(name))
            {
                failures.Add(new ValidationFailure("Account name cannot be null"));
                if (earlyReturnOnFailure)
                {
                    return failures;
                }
            }
            return failures;
        }
        #endregion
        
    }
}
