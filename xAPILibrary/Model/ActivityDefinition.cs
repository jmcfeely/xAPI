using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaLearning.xAPI.xAPILibrary.Helper;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    public class ActivityDefinition : Extensible
    {        
        #region Properties

        /// <summary>
        /// The human readable/visual name of the Activity
        /// </summary>
        public LanguageMap name { get; set; }

        /// <summary>
        /// A description of the Activity
        /// </summary>
        public LanguageMap description { get; set; }

        /// <summary>
        /// The type of Activity.
        /// </summary>
        public Uri type { get; set; }

        /// <summary>
        /// SHOULD resolve to a document human-readable information about the Activity, which MAY include a way to
        /// "launch" the Activity
        /// </summary>
        public string moreInfo { get; set; }
        //public string moreinfo { get; set; }  ?? What is the "IL" datatype mentioned in the spec for this property ??

        /// <summary>
        /// The human readable/visual name of the Activity
        /// </summary>
        public Interaction interaction { get; set; }

        /// <summary>
        /// A map of other properties as needed
        /// </summary>
        public Extensible extensions { get; set; }

        #endregion

        #region Constructors
        public ActivityDefinition()
        {
        }

        /// <summary>
        /// Simplified constructor to create an activity with a
        /// single language code, name, and description.
        /// </summary>
        public ActivityDefinition(string name,string description,string languageCode,Uri type,Interaction interaction) : this()
        {
            this.name.Add(languageCode, name);
            this.description.Add(languageCode, description);
            this.type = type;
            this.interaction = interaction;
        }

        public ActivityDefinition(LanguageMap name,LanguageMap description, Uri type, string interactionType, string moreInfo, Extensible extensions)
        {
            this.name = name;
            this.description = description;
            this.type = type;
            this.interaction = interaction;
            this.moreInfo = moreInfo;
            this.extensions = extensions;
        }

        public ActivityDefinition(ActivityDefinition activityDefinition)
        {
            this.extensions = activityDefinition.extensions;
            this.name = activityDefinition.name;
            this.description = activityDefinition.description;
            this.type = activityDefinition.type;
            this.interaction = activityDefinition.interaction;
            this.moreInfo = activityDefinition.moreInfo;
        }

        #endregion
        
        #region Public Methods
        public virtual bool Update(ActivityDefinition def)
        {
            bool updated = false;
            if (def == null)
            {
                return false;
            }

            if (!def.type.Equals(this.type))
            {
                this.type = def.type;
                updated = true;
            }
            if (def.name != null && def.name.Count > 0 && !CommonFunctions.AreDictionariesEqual(this.name, def.name))
            {
                this.name = def.name;
                updated = true;
            }
            if (def.description != null && def.description.Count > 0 && !CommonFunctions.AreDictionariesEqual(this.description, def.description))
            {
                this.description = def.description;
                updated = true;
            }
            if (def.interaction != null && !def.interaction.Equals(this.interaction))
            {
                this.interaction = def.interaction;
                updated = true;
            }
            if (def.moreInfo != null && !def.moreInfo.Equals(this.moreInfo))
            {
                this.moreInfo = def.moreInfo;
                updated = true;
            }
            if (def.extensions != null && !def.extensions.Equals(this.extensions))
            {
                this.extensions = def.extensions;
                updated = true;
            }

            return updated;
        }
        #endregion
    }
}
