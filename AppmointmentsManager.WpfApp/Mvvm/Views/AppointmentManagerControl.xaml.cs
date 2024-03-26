using AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;
using AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace AppointmentsManager.WpfApp.Mvvm.Views;

public partial class AppointmentManagerControl : ControlBase
{
    private bool _gridReset = false;

    public AppointmentManagerControl()
    {
        InitializeComponent();
    } 

    private void AppointmentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var cast = (DataGrid)sender;
        if (!cast.CurrentCell.IsValid) return;
        cast.CurrentCell = new DataGridCellInfo(cast.SelectedItem, cast.CurrentCell.Column); 
    }
}