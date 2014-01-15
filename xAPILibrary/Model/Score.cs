using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    /// <summary>
    /// Description: An optional numeric field that represents the outcome of a graded Activity achieved by an Agent. 
    ///
    /// Requirement: The Score Object SHOULD include 'scaled' if a logical percent based score is known. 
    /// Requirement: The Score Object SHOULD NOT be used for scores relating to progress or completion. Consider 
    ///              using an extension from an extension profile instead.
    /// </summary>
    public class Score : IValidatable
    {
        #region Properties

        /// <summary>
        /// Type: Decimal number between -1 and 1, inclusive 
        /// Description: Cf. 'cmi.score.scaled' in SCORM 2004 4th Edition
        /// </summary>
        public NullableDouble scaled { get; set; }

        /// <summary>
        /// Type: Decimal number between min and max (if present, otherwise unrestricted), inclusive 
        /// Description: Cf. 'cmi.score.raw'
        /// </summary>
        public NullableDouble raw { get; set; }

        /// <summary>
        /// Type: Decimal number less than max (if present)  
        /// Description: Cf. 'cmi.score.min'
        /// </summary>
        public NullableDouble min { get; set; }

        /// <summary>
        /// Type: Decimal number greater than min (if present) 
        /// Description: Cf. 'cmi.score.max'
        /// </summary>
        public NullableDouble max { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public Score() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="scaled">The value for scaled, the recommended property</param>
        public Score(double scaled)
        {
            this.scaled = scaled;
        }

        /// <summary>
        /// Constructor for raw score and the score range
        /// </summary>
        /// <param name="raw">The score</param>
        /// <param name="min">The lowest permissible value</param>
        /// <param name="max">The greatest permissible value</param>
        public Score(double raw, double min, double max)
        {
            this.raw = raw;
            this.min = min;
            this.max = max;
        }
        #endregion

        #region Public Methods
        public IEnumerable<ValidationFailure> Validate(bool earlyReturnOnFailure)
        {
            var failures = new List<ValidationFailure>();
            if (scaled != null && (scaled.Value < 0.0 || scaled.Value > 1.0))
            {
                failures.Add(new ValidationFailure("Scaled score must be between 0.0 and 1.0"));
                if (earlyReturnOnFailure)
                {
                    return failures;
                }
            }
            if ((min != null && max != null) && (max.Value < min.Value))
            {
                failures.Add(new ValidationFailure("Max score cannot be lower than min score"));
                if (earlyReturnOnFailure)
                {
                    return failures;
                }
            }
            if (raw != null)
            {
                if (max != null && raw.Value > max.Value)
                {
                    failures.Add(new ValidationFailure("Raw score cannot be greater than max score"));
                    if (earlyReturnOnFailure)
                    {
                        return failures;
                    }
                }
                if (min != null && raw.Value < min.Value)
                {
                    failures.Add(new ValidationFailure("Raw score cannot be less than min score"));
                    if (earlyReturnOnFailure)
                    {
                        return failures;
                    }
                }
            }
            return failures;
        }
        #endregion
    }
}
