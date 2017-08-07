using System;
using System.Windows;
using System.Windows.Automation;

namespace UIAutomationDetector
{
    internal static class UiAutomationHelper
    {
        public static TextElement GetElement(this AutomationElement ae)
        {
            try
            {
                // Text label
                // http://stackoverflow.com/questions/23850176/c-sharp-system-windows-automation-get-element-text
                // -----------------------------------------------------------
                string text = null;
                object pattern;
                if (ae.TryGetCurrentPattern(ValuePattern.Pattern, out pattern))
                {
                    var valuePattern = pattern as ValuePattern;
                    if (valuePattern != null)
                    {
                        text = valuePattern.Current.Value;
                    }
                }
                else if (ae.TryGetCurrentPattern(TextPattern.Pattern, out pattern))
                {
                    var textPattern = pattern as TextPattern;
                    if (textPattern != null)
                    {
                        text = textPattern.DocumentRange.GetText(-1).TrimEnd('\r');
                    }
                }
                else
                {
                    text = ae.Current.Name;
                }

                // Text position
                // http://msdn.microsoft.com/en-us/library/system.windows.automation.automationelement.boundingrectangleproperty(v=vs.110).aspx
                // -----------------------------------------------------------
                var elementBounds = new Rect();
                var boundingRectangleObject = ae.GetCurrentPropertyValue(AutomationElement.BoundingRectangleProperty, true);
                if (boundingRectangleObject != AutomationElement.NotSupported)
                {
                    elementBounds = (Rect)boundingRectangleObject;
                }

                // Control Type
                // -----------------------------------------------------------
                var controlType = ae.GetCurrentPropertyValue(AutomationElement.ControlTypeProperty, true) as ControlType;
                var controleTypeName = controlType != null ? controlType.ProgrammaticName : String.Empty;


                // Visibility
                // -----------------------------------------------------------
                bool? visible;
                var isOffScreenElement = ae.GetCurrentPropertyValue(AutomationElement.IsOffscreenProperty, true);
                if (isOffScreenElement == AutomationElement.NotSupported)
                    visible = null;
                else
                    visible = !(bool)isOffScreenElement;
               
                
                return new TextElement(controleTypeName, elementBounds, text, visible);
            }
            catch (ElementNotAvailableException)
            {
                return null;
            }
        }
    }
}