using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace EkspertBookerMobileApp.Helper
{
    public class NumericValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {

            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                string newText = args.NewTextValue;
                bool isValid = newText.ToCharArray().All(x => char.IsDigit(x)); //Make sure all characters are numbers
                if (!isValid) newText = newText.Remove(newText.Length - 1);
                //var text = isValid ? args.NewTextValue : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                if (!string.IsNullOrEmpty(newText))
                {
                    if (int.Parse(newText) > 1000) newText = "1000";
                    if (int.Parse(newText) < 1) newText = "1";

                    ((Entry)sender).Text = newText;
                } else
                {
                    ((Entry)sender).Text = null;
                }

            }
        }

    }
}
