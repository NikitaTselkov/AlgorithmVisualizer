using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class VisibilityAttribute : Attribute
    {
        private Visibility _visibility;
        public Visibility Visibility
        {
            get { return _visibility; }
            set { _visibility = value; }
        }

        public VisibilityAttribute(Visibility visibility)
        {
            Visibility = visibility;
        }
    }
}
