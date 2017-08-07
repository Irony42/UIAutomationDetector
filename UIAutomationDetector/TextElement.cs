using System;
using System.Windows;

namespace UIAutomationDetector
{
    internal class TextElement
    {
        private readonly string _controlType;
        private readonly Rect _elementBounds;
        private readonly string _text;
        private readonly bool? _visible;

        public TextElement(string controlType, Rect elementBounds, string text, bool? visible)
        {
            _controlType = controlType;
            _elementBounds = elementBounds;
            _text = text;
            _visible = visible;
        }

        public override string ToString()
        {
            return String.Format(
                "Automation Element : ControlType = {0}, Text = {1}, ElementBounds = {2}, Visible = {3}\n",
                _controlType,
                _text,
                _elementBounds,
                _visible
                );
        }
    }
}