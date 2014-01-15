using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    /// <summary>
    /// Requirement: Within an array of all interaction components, all values MUST be distinct. 
    /// Requirement: An interaction component‟s id value SHOULD not have whitespace. 
    /// </summary>
    public class InteractionComponent
    {
        /// <summary>
        /// A value such a used in practice for "cmi.interactions.n.id" as defined in the SCORM 2004 4th Edition RTE.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// A description of the interaction component (for example, the text for a given multiple-choice interaction). 
        /// </summary>
        public LanguageMap description { get; set; }        
    }
}
