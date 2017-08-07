using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;

namespace UIAutomationDetector
{
    public static class AutomationExtensions
    {
        public static AutomationElement FindDescendentByClassPath(this AutomationElement element, IEnumerable<string> classNames)
        {
            var conditionPath = CreateClassNameConditionPath(classNames);

            return element.FindDescendentByConditionPath(conditionPath);
        }

        public static AutomationElement FindDescendentByConditionPath(this AutomationElement element, IEnumerable<Condition> conditionPath)
        {
            if (!conditionPath.Any())
            {
                return element;
            }

            var result = conditionPath.Aggregate(
                element,
                (parentElement, nextCondition) => parentElement == null
                                                      ? null
                                                      : parentElement.FindChildByCondition(nextCondition));

            return result;
        }

        public static AutomationElement FindChildByCondition(this AutomationElement element, Condition condition)
        {
            var result = element.FindFirst(
                TreeScope.Children,
                condition);

            return result;
        }

        public static IEnumerable<Condition> CreateClassNameConditionPath(IEnumerable<string> classNames)
        {
            return classNames.Select(name => new PropertyCondition(AutomationElement.ClassNameProperty, name, PropertyConditionFlags.IgnoreCase)).ToArray();
        }
    }
}