using EkspertBookerMobileApp.ContentViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EkspertBookerMobileApp.Helper
{
    public static class AnimationExtensions
    {
        public static Task<bool> ChangeBackgroundColorTo(this CVImageButton self, Color newColor, uint length = 250, Easing easing = null)
        {
            Task<bool> ret = new Task<bool>(() => false);

            if (!self.AnimationIsRunning(nameof(ChangeBackgroundColorTo)))
            {
                Color fromColor = self.BgColor;

                try
                {
                    Func<double, Color> transform = (t) =>
                      Color.FromRgba(fromColor.R + t * (newColor.R - fromColor.R),
                                     fromColor.G + t * (newColor.G - fromColor.G),
                                     fromColor.B + t * (newColor.B - fromColor.B),
                                     fromColor.A + t * (newColor.A - fromColor.A));

                    ret = TransmuteColorAnimation(self, nameof(ChangeBackgroundColorTo), transform, length, easing);
                }
                catch (Exception ex)
                {
                    // to supress animation overlapping errors 
                    self.BackgroundColor = fromColor;
                }
            }

            return ret;
        }

        private static Task<bool> TransmuteColorAnimation(CVImageButton button, string name, Func<double, Color> transform, uint length, Easing easing)
        {
            easing = easing ?? Easing.Linear;
            var taskCompletionSource = new TaskCompletionSource<bool>();

            button.Animate(name, transform, (color) => { button.BgColor = color; }, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));
            return taskCompletionSource.Task;
        }

        public static Task<bool> ChangeBackgroundColorTo(this CVImageButtonAlt self, Color newColor, uint length = 250, Easing easing = null)
        {
            Task<bool> ret = new Task<bool>(() => false);

            if (!self.AnimationIsRunning(nameof(ChangeBackgroundColorTo)))
            {
                Color fromColor = self.BgColor;

                try
                {
                    Func<double, Color> transform = (t) =>
                      Color.FromRgba(fromColor.R + t * (newColor.R - fromColor.R),
                                     fromColor.G + t * (newColor.G - fromColor.G),
                                     fromColor.B + t * (newColor.B - fromColor.B),
                                     fromColor.A + t * (newColor.A - fromColor.A));

                    ret = TransmuteColorAnimation(self, nameof(ChangeBackgroundColorTo), transform, length, easing);
                }
                catch (Exception ex)
                {
                    // to supress animation overlapping errors 
                    self.BackgroundColor = fromColor;
                }
            }

            return ret;
        }

        private static Task<bool> TransmuteColorAnimation(CVImageButtonAlt button, string name, Func<double, Color> transform, uint length, Easing easing)
        {
            easing = easing ?? Easing.Linear;
            var taskCompletionSource = new TaskCompletionSource<bool>();

            button.Animate(name, transform, (color) => { button.BgColor = color; }, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));
            return taskCompletionSource.Task;
        }
    }

}
