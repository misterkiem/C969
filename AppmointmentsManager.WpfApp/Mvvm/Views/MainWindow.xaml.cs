using AppointmentsManager.WpfApp.Mvvm.Vms.WindowVms;

namespace AppointmentsManager.WpfApp.Mvvm.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase
    {
        public MainWindow(MainWindowVm mainWindowVm) : base(mainWindowVm)
        {
            InitializeComponent();
        }
    }
}