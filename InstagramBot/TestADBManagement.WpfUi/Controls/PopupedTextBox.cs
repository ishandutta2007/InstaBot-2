using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TestADBManagement.WpfUi.Controls
{
    public sealed class PopupedTextBox : TextBox
    {
        #region PopupContent dp
        public object PopupContent
        {
            get { return GetValue(PopupContentProperty); }
            set { SetValue(PopupContentProperty, value); }
        }

        public static readonly DependencyProperty PopupContentProperty =
            DependencyProperty.Register(nameof(PopupContent), typeof(object), typeof(PopupedTextBox), new PropertyMetadata(null));
        #endregion
    }
}
