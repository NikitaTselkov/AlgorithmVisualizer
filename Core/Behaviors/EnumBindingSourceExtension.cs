using Core.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;

namespace Core.Behaviors
{
    public class EnumBindingSourceExtension : MarkupExtension
    {
        private Type _enumType;
        public Type EnumType
        {
            get { return _enumType; }
            set
            {
                if (value != _enumType)
                {
                    if (null != value)
                    {
                        Type enumType = Nullable.GetUnderlyingType(value) ?? value;
                        if (!enumType.IsEnum)
                            throw new ArgumentException("Type must be for an Enum.");
                    }

                    _enumType = value;
                }
            }
        }

        public EnumBindingSourceExtension() { }

        public EnumBindingSourceExtension(Type enumType)
        {
            EnumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (null == _enumType)
                throw new InvalidOperationException("The EnumType must be specified.");

            Type actualEnumType = Nullable.GetUnderlyingType(this._enumType) ?? this._enumType;
            Array enumValues = Enum.GetValues(actualEnumType);

            if (actualEnumType == _enumType)
                return ProcessAttributes(enumValues);

            Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
            enumValues.CopyTo(tempArray, 1);
            return tempArray;
        }

        private static Array ProcessAttributes(Array array)
        {
            var values = new List<object>();

            foreach (var item in array)
            {
                FieldInfo fi = item.GetType().GetField(item.ToString());
                if (fi != null)
                {
                    var attributes = (VisibilityAttribute[])fi.GetCustomAttributes(typeof(VisibilityAttribute), false);

                    if (attributes.Length > 0)
                    {
                        if (attributes[0].Visibility == Visibility.Visible)
                            values.Add(item);
                    }
                    else values.Add(item);
                }
            }

            return values.ToArray();
        }
    }
}
