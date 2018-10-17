using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Recipe_App.Views
{
    class ExpandableEditor : Editor
    {
        public ExpandableEditor()
        {
             TextChanged += OnTextChanged;
        }

       
    

    ~ExpandableEditor()
        {
             TextChanged -= OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            InvalidateMeasure();

        }
    }
}



