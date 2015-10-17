using System.Text.RegularExpressions;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Kulman.WPA81
{
    public sealed partial class HighlightTextBox : UserControl
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(HighlightTextBox), new PropertyMetadata(null, PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var c = d as HighlightTextBox;
            c.Update();
        }

        public string HighlightedText
        {
            get { return (string)GetValue(HighlightedTextProperty); }
            set { SetValue(HighlightedTextProperty, value); }
        }

        public static readonly DependencyProperty HighlightedTextProperty = DependencyProperty.Register("HighlightedText", typeof(string), typeof(HighlightTextBox), new PropertyMetadata(null, PropertyChangedCallback));

        public Brush HighlightBrush
        {
            get { return (Brush)GetValue(HighlightBrushProperty); }
            set { SetValue(HighlightBrushProperty, value); }
        }

        public static readonly DependencyProperty HighlightBrushProperty = DependencyProperty.Register("HighlightBrush", typeof(Brush), typeof(HighlightTextBox), new PropertyMetadata(new SolidColorBrush(Colors.Red)));


        public HighlightTextBox()
        {
            this.InitializeComponent();
        }

        private void Update()
        {
            if (string.IsNullOrEmpty(Text) || string.IsNullOrEmpty(HighlightedText))
            {
                TB.Inlines.Clear();
                return;
            }

            TB.Inlines.Clear();
            var parts = Regex.Split(Text, HighlightedText, RegexOptions.IgnoreCase);
            var len = 0;
            foreach (var part in parts)
            {
                len = len + part.Length + 1;

                TB.Inlines.Add(new Run
                {
                    Text = part
                });

                if (Text.Length >= len)
                {
                    var highlight = Text.Substring(len - 1, HighlightedText.Length); //to match the case

                    TB.Inlines.Add(new Run
                    {
                        Text = highlight,
                        Foreground = HighlightBrush
                    });
                }
            }
        }
    }
}
