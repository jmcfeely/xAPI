using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    public class StatementVerb
    {
        #region Properties

        public string id { get; set; }

        public LanguageMap display { get; set; }

        #endregion

        #region Constructors
        
        public StatementVerb()
        {
        }

        public StatementVerb(Uri id, LanguageMap display)
        {
            this.id = id.ToString();
            this.display = display;
        }

        public StatementVerb(Uri id, string locale, string name)
        {
            this.id = id.ToString();
            display = new LanguageMap();
            display[locale] = name;
        }

        public StatementVerb(string id, string locale, string name)
        {
            if (IsUri(id))
            {
                this.id = id;
            }
            else
            {
                throw new ArgumentException("The URI " + id + " is malformed.", "id");
            }
            display = new LanguageMap();
            display[locale] = name;
        }

        public StatementVerb(PredefinedVerbs verb)
        {
            this.id = "http://adlnet.gov/expapi/verbs/" + verb.ToString().ToLower();
            this.display = new LanguageMap();
            this.display["en-US"] = verb.ToString().ToLower();
        }
                
        public StatementVerb(Model.x090.StatementVerb verb)
            : this((PredefinedVerbs)Enum.Parse(typeof(PredefinedVerbs), verb.ToString(), true))
        {
        }

        #endregion

        #region Public Methods

        private bool IsUri(string source)
        {
            if (!string.IsNullOrEmpty(source) && Uri.IsWellFormedUriString(source, UriKind.RelativeOrAbsolute))
            {
                Uri tempValue;
                return Uri.TryCreate(source, UriKind.RelativeOrAbsolute, out tempValue);
            }
            return false;
        }

        public bool IsVoided()
        {
            foreach (string s in display.Values)
            {
                if (s.ToLower().Equals("voided"))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Demotes a 0.95 verb to a 0.90 verb.
        /// </summary>
        /// <param name="verb">A 0.95 verb.  It MUST have an en-US entry in the display field.</param>
        /// <returns></returns>
        /// <remarks>If no en-US entry is in the display map, this method will always fail and throw an exception.
        /// The core verbs from 0.90 are adl provided verbs in 0.95 to maintain some form of verb mapping.</remarks>
        public static explicit operator Model.x090.StatementVerb(StatementVerb verb)
        {
            try
            {
                return (Model.x090.StatementVerb)Enum.Parse(typeof(Model.x090.StatementVerb), verb.display["en-US"], true);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("The verb " + verb.display["en-US"] + " has no 0.90 verb representation.", "verb");
            }
        }
        #endregion
    }

    public enum PredefinedVerbs
    {        
        Answered,
        Asked,
        Attempted,
        Attended,
        Commented,
        Completed,
        Exited,
        Experienced,
        Failed,
        Imported,
        Initialized,
        Interacted,
        Launched,
        Mastered,
        Passed,
        Preferred,
        Progressed,
        Registered,
        Responded,
        Resumed,
        Scored,
        Shared,
        Suspended,
        Terminated,
        Voided,
    }
}
