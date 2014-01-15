using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaLearning.xAPI.xAPILibrary.Helper;

namespace MetaLearning.xAPI.xAPILibrary.Model
{
    public class InteractionDefinition : ActivityDefinition
    {
        #region Properties
        
        public InteractionType interactionType = new InteractionType(InteractionTypeValue.Undefined);

        public List<string> correctResponsesPattern { get; set; }

        //Each of these is optional, depending on the type of interaction
        public List<InteractionComponent> choices { get; set; }

        public List<InteractionComponent> scale { get; set; }

        public List<InteractionComponent> source { get; set; }

        public List<InteractionComponent> target { get; set; }

        public List<InteractionComponent> steps { get; set; }

        #endregion

        #region Constructors

        public InteractionDefinition() { }

        public InteractionDefinition(ActivityDefinition def)
        {
            this.Update(def);
        }

        #endregion

        #region Public Methods

        public override bool Update(ActivityDefinition activityDef)
        {
            bool updated = base.Update(activityDef);
            if (!(activityDef is InteractionDefinition))
            {
                return updated;
            }

            InteractionDefinition def = (InteractionDefinition)activityDef;

            if (NotNullAndNotEqual(def.correctResponsesPattern, this.correctResponsesPattern))
            {
                this.correctResponsesPattern = def.correctResponsesPattern;
                updated = true;
            }

            if (NotNullAndNotEqual(def.choices, this.choices))
            {
                this.choices = def.choices;
                updated = true;
            }

            if (NotNullAndNotEqual(def.scale, this.scale))
            {
                this.scale = def.scale;
                updated = true;
            }

            if (NotNullAndNotEqual(def.source, this.source))
            {
                this.source = def.source;
                updated = true;
            }

            if (NotNullAndNotEqual(def.target, this.target))
            {
                this.target = def.target;
                updated = true;
            }

            if (NotNullAndNotEqual(def.steps, this.steps))
            {
                this.steps = def.steps;
                updated = true;
            }

            return updated;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        protected bool NotNullAndNotEqual<T>(List<T> list1, List<T> list2)
        {
            return list1 != null && list1.Count > 0 && !CommonFunctions.AreListsEqual(list1, list2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is InteractionDefinition))
            {
                return false;
            }
            InteractionDefinition otherDef = (InteractionDefinition)obj;
            return base.Equals(obj)
                        && CommonFunctions.AreListsEqual(correctResponsesPattern, otherDef.correctResponsesPattern)
                        && CommonFunctions.AreListsEqual(this.choices, otherDef.choices)
                        && CommonFunctions.AreListsEqual(this.scale, otherDef.scale)
                        && CommonFunctions.AreListsEqual(this.source, otherDef.source)
                        && CommonFunctions.AreListsEqual(this.target, otherDef.target)
                        && CommonFunctions.AreListsEqual(this.steps, otherDef.steps);
        }

        public static bool IsValidComponent(InteractionTypeValue interactionType, InteractionComponentName componentName)
        {
            if (interactionType == InteractionTypeValue.Choice || interactionType == InteractionTypeValue.Sequencing)
            {
                return componentName == InteractionComponentName.Choices;
            }
            else if (interactionType == InteractionTypeValue.Likert)
            {
                return componentName == InteractionComponentName.Scale;
            }
            else if (interactionType == InteractionTypeValue.Matching)
            {
                return componentName == InteractionComponentName.Source || componentName == InteractionComponentName.Target;
            }
            else if (interactionType == InteractionTypeValue.Performance)
            {
                return componentName == InteractionComponentName.Steps;
            }
            return false;
        }

        protected List<InteractionComponent> ProtectedGet(InteractionComponentName componentName, List<InteractionComponent> componentList)
        {
            if (!IsValidComponent(this.interactionType.Value, componentName))
            {
                return null;
            }
            return componentList;
        }

        protected void CheckComponentSet(InteractionComponentName componentName, List<InteractionComponent> componentList)
        {
            if (componentList == null)
            {
                return;
            }
            if (!IsValidComponent(this.interactionType.Value, componentName))
            {
                throw new ArgumentException(componentName.ToString().ToLower() + " is not a valid interaction component for the given interactionType", "componentName");
            }
        }

        #endregion
    }
}
