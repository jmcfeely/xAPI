using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaLearning.xAPI.xAPILibrary.Helper;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    public class StatementRef : StatementTarget
    {
        /*Description: A Statement Reference is a pointer to another pre-existing Statement. 
        
        //Requirements: A Statement Reference MUST specify an "objectType" property with the value "StatementRef".
        //              A Statement Reference MUST set the "id" property to the UUID of a Statement.
        */

        #region Properties

        /// <summary>
        /// In this case, MUST be "StatementRef".
        /// </summary>
        public override string ObjectType
        {
            get { return "StatementRef"; }
        }

        /// <summary>
        /// The UUID of a Statement.
        /// </summary>
        public string id
        {
            get { return id; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    id = null;
                }
                else
                {
                    string normalized = value.ToLower();
                    if (!ValidationHelper.IsValidUUID(normalized))
                    {
                        throw new ArgumentException("Statement ID must be UUID", "value");
                    }
                    id = normalized;
                }
            }
        }
               
        #endregion

        #region Constructor
        
        public StatementRef(string id)
        {
            this.id = id;
        }
        
        #endregion
    }
}
