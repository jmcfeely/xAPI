using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    public class State
    {
        #region Properties

        public string id { get; set; }

        public Boolean updated { get; set; }

        public string contents { get; set; }

        public string etag { get; set; }

        public string contentType { get; set; }

        #endregion

        #region Constructors

        public State() { }

        public State(string id, string contents, string etag, string contentType)
        {
            this.id = id;
            this.contents = contents;
            this.etag = etag;
            this.contentType = contentType;
        }

        #endregion

    }
}
