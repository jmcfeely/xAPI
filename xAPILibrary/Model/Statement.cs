using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaLearning.xAPI.xAPILibrary.Helper;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    /// <definition>
    /// A simple construct consisting of [Actor (learner)] [verb] [object], with [result] in [context] to track
    /// an aspect of a learning experience. A set of several statements may be used to track complete details
    /// about a learning experience.
    /// </definition>
    public class Statement : IValidatable
    {
        /// <properties>
        /// Actor, Verb, and Object are required, all other properties are optional. Properties can occur in any order,
        /// but are limited to one use each.
        /// </properties>
        #region Properties

        /// <summary>
        /// UUID assigned by LRS if not set by the Activity Provider 
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
                
        /// <summary>
        /// Who the statement is about, as an AgentAccount or Group object. Represents the "I" in "I did this".
        /// </summary>
        public Actor actor { get; set; }

        /// <summary>
        /// Action of the Learner or Team Object. Represents the "Did" in "I Did This". 
        /// </summary>
        public StatementVerb verb { get; set; }

        /// <summary>
        /// Activity, Agent, or another statement that is the Object of the Statement. Represents the "This" in "I Did This". 
        /// Note that Objects which are provided as a value for this field should include an "objectType" field. If not 
        /// specified, the Object is assumed to be an Activity.  
        /// </summary>
        public StatementTarget _object { get; set; }

        /// <summary>
        /// Result Object, further details representing a measured outcome relevant to the specified Verb. 
        /// </summary>
        public Result result { get; set; }

        /// <summary>
        /// Context that gives the Statement more meaning. Examples: a team the Actor is working with, altitude at which 
        /// a scenario was attempted in a flight simulator
        /// </summary>
        public Context context { get; set; }

        /// <summary>
        /// Timestamp (Formatted according to ISO 8601) of when the events described within this Statement occurred. If not 
        /// provided, LRS should set this to the value of "stored" time.  
        /// </summary>
        public NullableDateTime timestamp { get; set; }

        /// <summary>
        /// /Time Timestamp (Formatted according to ISO 8601) of when this Statement was recorded. Set by LRS. 
        /// </summary>
        public NullableDateTime stored { get; set; }

        /// <summary>
        /// Agent who is asserting this Statement is true. Verified by the LRS based on authentication, and set by LRS 
        /// if left blank.  
        /// </summary>
        public Actor authority { get; set; }

        /// <summary>
        /// The Statement‟s associated xAPI version, formatted according to Semantic Versioning 1.0.0  
        /// </summary>
        public xAPIVersion version { get; set; }

        /// <summary>
        /// Headers for attachments to the Statement
        /// </summary>
        public List<Attachment> attachments { get; set; }

        /// <summary>
        /// Returns the statement verb in its internal enum format
        /// </summary>
        /// <returns>Statement verb as an enum</returns>
        public StatementVerb GetVerbAsEnum()
        {
            return verb;
        }

        #endregion
        
        #region Constructors
        
        public Statement() { }

        public Statement(Actor actor, StatementVerb verb, StatementTarget statementTarget)
        {
            this.actor = actor;
            this.verb = verb;
            this._object = statementTarget;
        }

        public Statement(Actor actor, PredefinedVerbs verb, StatementTarget statementTarget)
            : this(actor, new StatementVerb(verb), statementTarget)
        {
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Validates the statement, ensuring required fields are used
        /// and any necessary information (such as a result for specific verbs)
        /// is provided and valid.
        /// </summary>
        public virtual IEnumerable<ValidationFailure> Validate(bool earlyReturnOnFailure)
        {
            var failures = new List<ValidationFailure>();
            if (actor == null && verb != null && !verb.IsVoided())
            {
                failures.Add(new ValidationFailure("Statement " + id + " does not have an actor"));
                if (earlyReturnOnFailure)
                {
                    return failures;
                }
            }
            if (verb == null)
            {
                failures.Add(new ValidationFailure("Statement " + id + " does not have a verb"));
                if (earlyReturnOnFailure)
                {
                    return failures;
                }
            }
            else if (verb.IsVoided())
            {
                // This will test for StatementRef OR TargetedStatement because any statement that is being validated has already been promoted.
                bool objectStatementIdentified = ((_object is StatementRef) && !string.IsNullOrEmpty(((StatementRef)_object).Id) ||
                    (_object is Model.x090.TargetedStatement) && !string.IsNullOrEmpty(((Model.x090.TargetedStatement)_object).Id));
                if (!objectStatementIdentified)
                {
                    failures.Add(new ValidationFailure("Statement " + id + " has verb 'voided' but does not properly identify a statement as its object"));
                    if (earlyReturnOnFailure)
                    {
                        return failures;
                    }
                }
            }
            if (_object == null)
            {
                failures.Add(new ValidationFailure("Statement " + id + " does not have an object"));
                if (earlyReturnOnFailure)
                {
                    return failures;
                }
            }


            object[] children = new object[] { actor, verb, _object, result, context, timestamp, authority };
            foreach (object o in children)
            {
                if (o != null && o is IValidatable)
                {
                    failures.AddRange(((IValidatable)o).Validate(earlyReturnOnFailure));
                    if (earlyReturnOnFailure && failures.Count > 0)
                    {
                        return failures;
                    }
                }
            }
            return failures;
        }
        #endregion

        #region Verb Handling
        /// <summary>
        /// Handles verbs with special requirements
        /// </summary>
        public void HandleSpecialVerbs()
        {
            if (this.verb.Equals("passed"))
            {
                result = (result == null) ? new Result() : result;
                VerifySuccessAndCompletionValues(result, "passed", true, true);
                result.success = true;
                result.completion = true;
            }
            else if (this.verb.Equals("failed"))
            {
                result = (result == null) ? new Result() : result;
                VerifySuccessAndCompletionValues(result, "failed", false, true);
                result.success = false;
                result.completion = true;
            }
            else if (this.verb.Equals("completed"))
            {
                result = (result == null) ? new Result() : result;
                VerifyCompletionValue(result, "completed", true);
                result.completion = true;
            }
        }

        /// <summary>
        /// Validates both success and completion
        /// </summary>
        /// <param name="result">The result object</param>
        /// <param name="verb">The statement verb</param>
        /// <param name="expectedSuccess">The expected success value</param>
        /// <param name="expectedCompletion">The expected completion value</param>
        protected void VerifySuccessAndCompletionValues(Result result, string verb, bool expectedSuccess, bool expectedCompletion)
        {
            VerifySuccessValue(result, verb, expectedSuccess);
            VerifyCompletionValue(result, verb, expectedCompletion);
        }
        /// <summary>
        /// Validates expect success values
        /// </summary>
        /// <param name="result">The result object</param>
        /// <param name="verb">The verb for the statement</param>
        /// <param name="expectedSuccess">What value the success should be</param>
        protected void VerifySuccessValue(Result result, string verb, bool expectedSuccess)
        {
            if (result.success != null && result.success.Value != expectedSuccess)
            {
                throw new ArgumentException("Specified verb \"" + verb + "\" but with a result success value of " + result.success.Value, "verb");
            }
        }

        /// <summary>
        /// Validates expected completion values
        /// </summary>
        /// <param name="result">The result object</param>
        /// <param name="verb">The statement verb</param>
        /// <param name="expectedCompletion">What value the completion should be</param>
        protected void VerifyCompletionValue(Result result, string verb, bool expectedCompletion)
        {
            if (result.completion != null && result.completion.Value != expectedCompletion)
            {
                throw new ArgumentException("Specified verb \"" + verb + "\" but with a result completion value of " + result.completion.Value, "verb");
            }
        }
        #endregion

    }
}
