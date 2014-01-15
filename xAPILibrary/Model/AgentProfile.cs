using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaLearning.xAPI.xAPILibrary.Helper;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    public class AgentProfile
    {
        #region Properties

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

        public Actor actor { get; set; }

        public string updated { get; set; }

        public string contents { get; set; }

        public string etag { get; set; }

        public string contentType { get; set; }

        #endregion

         #region Constructors

        public AgentProfile() { }

        public AgentProfile(Actor actor, string id, string contents, string etag, string contentType)
        {
            this.actor = actor;
            this.id = id;
            this.contents = contents;
            this.etag = etag;
            this.contentType = contentType;
        }

        #endregion

    }
}
