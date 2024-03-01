using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;
using System.ComponentModel;
using System.Windows.Data;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;

public abstract partial class DtoVmBase : ObservableObject
{
    public int Id { get; set; }

    [ObservableProperty]
    private ICollectionView? viewSource;

    protected void SetView(IEnumerable objects)
       => ViewSource = CollectionViewSource.GetDefaultView(objects);
}