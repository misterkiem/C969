using AppointmentsManager.DataAccess.Models;
using AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;

namespace AppointmentsManager.WpfApp.Core;
public interface ICustomerCardVmFactory
{
    CustomerCardVm CreateEmpty();
    CustomerCardVm CreateFromExisting(Customer customer);
}