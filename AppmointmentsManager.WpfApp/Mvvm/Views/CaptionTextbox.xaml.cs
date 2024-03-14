using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppointmentsManager.WpfApp.Mvvm.Views
{
    /// <summary>
    /// Interaction logic for CaptionTextbox.xaml
    /// </summary>
    public partial class CaptionTextbox : UserControl
    {
        public Dock CaptionDock
        {
            get { return (Dock)GetValue(CaptionDockProperty); }
            set { SetValue(CaptionDockProperty, value); }
        }

        public static readonly DependencyProperty CaptionDockProperty =
            DependencyProperty.Register("CaptionDock",
                typeof(Dock),
                typeof(CaptionTextbox),
                new PropertyMetadata(Dock.Left));

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption",
                typeof(string),
                typeof(CaptionTextbox),
                new PropertyMetadata(null));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
           DependencyProperty.Register("Text",
               typeof(string),
               typeof(CaptionTextbox),
               new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Brush TextboxBackground
        {
            get { return (Brush)GetValue(TextboxBackgroundProperty); }
            set { SetValue(TextboxBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextboxBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextboxBackgroundProperty =
            DependencyProperty.Register("TextboxBackground",
                typeof(Brush),
                typeof(CaptionTextbox),
                new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public CaptionTextbox()
        {
            InitializeComponent();
        }
    }
}