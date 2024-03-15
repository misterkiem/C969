using AppointmentsManager.DataAccess.Models;
using AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;

namespace AppointmentsManager.WpfApp.Core;
public interface IDtoVmFactory<T> where T : DtoVmBase
{
    T CreateEmpty();
    T CreateFromExisting(DbModel model);
}