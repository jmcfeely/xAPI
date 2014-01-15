using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    public class Attachment
    {
        /// <summary>
        /// Identifies the usage of this attachment. For example: 
        /// one expected use case for attachments is to include a 
        /// "completion certificate". A type IRI corresponding to 
        /// this usage should be coined, and used with completion 
        /// certificate attachments. 
        /// </summary>
        public string usageType { get; set; }

        /// <summary>
        /// Display name (title) of this attachment.
        /// </summary>
        public LanguageMap display { get; set; }

        /// <summary>
        /// A description of the attachment.
        /// </summary>
        public LanguageMap description { get; set; }

        /// <summary>
        /// The content type of the attachment
        /// </summary>
        public string contentType { get; set; }

        /// <summary>
        /// The length of the attachment data in octets.
        /// </summary>
        public int length { get; set; }

        /// <summary>
        /// The SHA-2 hash of the attachment data. A minimum 
        /// key size of 256 bits is recommended. 
        /// </summary>
        public string sha2 { get; set; }

        /// <summary>
        /// An IRL at which the attachment data may be retrieved, 
        /// or from which it used to be retrievable.
        /// </summary>
        public Uri fileUrl { get; set; }
    }
}
