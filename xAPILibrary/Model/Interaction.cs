using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    public class Interaction : Activity
    {
        /// <summary>
        /// Activity Definition
        /// </summary>
        public InteractionDefinition definition { get; set; }
    }
}
