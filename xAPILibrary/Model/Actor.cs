using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaLearning.xAPI.xAPILibrary.Helper;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    /// <definition>
    /// An identity or persona of an individual or group tracked using Statements as doing an action (Verb) within
    /// an Activity.
    /// </definition>
    public class Actor : StatementTarget, IValidatable
    {      
        
        #region Properties

        /// <summary>
        /// "Agent". This property is optional except when the Agent is used as a Statement's Object.
        /// </summary>
        public override string ObjectType
        {
            get { return "Agent"; }
        }

        /// <summary>
        /// Full name of the Agent.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// The required format is "mailto:email_address".
        /// 
        /// Only email addresses that have only even been and will ever be assigned to this Agent, but no others, should
        /// be used for this property and mbox_sha1sum
        /// </summary>
        public string mbox
        {
            get { return mbox; }
            set
            {
                string mboxPrefix = "mailto:";
                string normalized = value.ToLower();
                if (normalized != null)
                {
                    if (!normalized.StartsWith(mboxPrefix))
                    {
                        throw new ArgumentException(
                            "Mbox value " + normalized + " must begin with mailto: prefix",
                            "value");
                    }
                    if (!ValidationHelper.IsValidEmailAddress(normalized.Substring(mboxPrefix.Length)))
                    {
                        throw new ArgumentException(
                            "Mbox value " + normalized + " is not a valid email address.",
                            "value");
                    }
                }
                mbox = normalized;
            }
        }

        /// <summary>
        /// The SHA1 hash of a mailto URI (i.e. the value of an mbox property). An LRS MAY include Agents with a matching
        /// hash when a request is based on an mbox.
        /// </summary>
        public string mbox_sha1sum
        {
            get { return mbox_sha1sum; }
            set
            {
                mbox_sha1sum = value.ToLower();
            }
        }

        /// <summary>
        /// An openID that uniquely identifies the Agent.
        /// </summary>
        public string openID
        {
            get { return openID; }
            set
            {
                openID = value.ToLower();
            }
        }

        /// <summary>
        /// A user account on an existing system e.g. an LMS or intranet
        /// </summary>
        public AgentAccount account
        {
            get { return account; }
            set
            {
                if (value != null)
                {
                    // TODO - reconsider whether to deep-validate in setters
                    var failures = new List<ValidationFailure>(value.Validate(earlyReturnOnFailure: true));
                    if (failures.Count > 0)
                    {
                        throw new ArgumentException(failures[0].Error);
                    }
                    account = value;
                }
            }
        }

        #endregion                       

        #region Constructor
        public Actor() { }
        public Actor(string name, string email)
        {
            this.name = name;
            this.mbox = email;
        }

        public Actor(string name, AgentAccount account)
        {
            this.name = name;
            this.account = account;
        }

        public Actor(Actor actor)
        {
            this.name = actor.name;
            this.mbox = actor.mbox;
            this.mbox_sha1sum = actor.mbox_sha1sum;
            this.openID = actor.openID;
            this.account = actor.account;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Validates that the object abides by its rules
        /// </summary>
        public virtual IEnumerable<ValidationFailure> Validate(bool earlyReturnOnFailure)
        {
            int properties = 0;
            if (!string.IsNullOrEmpty(mbox))
            {
                properties++;
            }
            if (!string.IsNullOrEmpty(mbox_sha1sum))
            {
                properties++;
            }
            if (!string.IsNullOrEmpty(openID))
            {
                properties++;
            }
            if (account != null)
            {
                properties++;
            }
            if (properties != 1)
            {
                return new List<ValidationFailure>() 
                { 
                    new ValidationFailure("Exactly 1 inverse functional properties must be defined.  However, " + properties + " are defined.") 
                };
            }
            return new List<ValidationFailure>();
        }

        #endregion

    }
}
