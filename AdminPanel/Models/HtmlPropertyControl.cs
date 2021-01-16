using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AdminPanel.Models
{
    public class HtmlPropertyControl
    {
        public string bg_color;
        public PropertyInfo propertyInfo { get; set; }
        public DataType? DataType { get; set; }
        public IEnumerable<CustomAttributeData> Attributes { get; set; }
        public string html_input_type { get; set; }
        public string html_control_type { get; set; }
        public object CurrentValue { get; set; }
        public object DisplayName { get; internal set; }
        public object Max { get; set; }
        public object Min { get; set; }
        public string Message { get; internal set; }
        public string Step { get; internal set; }

        public string SelectorName { get; set; }
        public string FriendlyName { get { return ""; } }
        public string OutputScript;

        public Dictionary<Type, string> list;

        public override string ToString()
        {
            return propertyInfo.Name + " - " + (SelectorName ?? "");
        }
    }
}
