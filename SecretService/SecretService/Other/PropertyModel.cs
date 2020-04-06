using System;

namespace SGet
{
    public class PropertyModel
    {
        private string _name = String.Empty;
        private string _value = String.Empty;

        public PropertyModel(string value)
        {
            _value = value;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Value
        {
            get { return _value; }
        }
    }
}
