using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    public class SubStatement : Statement
    {
        #region Properties

        /// <summary>
        /// In this case, MUST be "SubStatement".
        /// </summary>
        public override string ObjectType
        {
            get { return "SubStatement"; }
        }

        public Actor actor { get; set; }

        public StatementVerb verb { get; set; }

        public StatementTarget target { get; set; }

        public Result result { get; set; }

        public Context context { get; set; }

        public NullableDateTime timestamp { get; set; }
        
        #endregion

        #region Constructor
        public SubStatement() : base() { }
        #endregion

    }
}
