using AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppointmentsManager.WpfApp.Mvvm.Views;
/// <summary>
/// Interaction logic for AppointmentsControl.xaml
/// </summary>
public partial class AppointmentsControl : ControlBase
{
    public AppointmentsControl(AppointmentsControlVm vm) : base(vm)
    {
        InitializeComponent();
    }
}
