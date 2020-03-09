using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EkspertBookerMobileApp.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CVImageButtonAlt : ContentView 
    {

        public static BindableProperty ButtonTextProperty =
            BindableProperty.Create("ButtonText", typeof(string), typeof(CVImageButtonAlt), default(string));

        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        public event EventHandler Clicked;

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(CVImageButtonAlt), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(CVImageButtonAlt), null);

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create("Source", typeof(ImageSource), typeof(CVImageButtonAlt), default(ImageSource));

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static BindableProperty BgColorProperty =
                  BindableProperty.Create("BgColor", typeof(Color), typeof(CVImageButtonAlt), default(Color));

        public Color BgColor
        {
            get { return (Color)GetValue(BgColorProperty); }
            set { SetValue(BgColorProperty, value); }
        }

        public static BindableProperty PaddingProperty =
            BindableProperty.Create("ButtonPadding", typeof(Thickness), typeof(CVImageButtonAlt), default(Thickness));

        public Thickness ButtonPadding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }
        

        public CVImageButtonAlt()
        {
            InitializeComponent();

            innerLabel.SetBinding(Label.TextProperty, new Binding("ButtonText", source: this));
            innerImage.SetBinding(Image.SourceProperty, new Binding("Source", source: this));
            CVImageButtonAltFrame.SetBinding(Frame.BackgroundColorProperty, new Binding("BgColor", source:this));
            CVImageButtonAltFrame.SetBinding(Frame.PaddingProperty, new Binding("ButtonPadding", source: this));
            this.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    Clicked?.Invoke(this, EventArgs.Empty);

                    if((Command != null))
                    {
                        if (Command.CanExecute(CommandParameter))
                            Command.Execute(CommandParameter);
                    }
                })
            });
        }
    }
}