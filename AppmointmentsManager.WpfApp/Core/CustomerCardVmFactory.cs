using AppointmentsManager.DataAccess.Models;
using AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;

namespace AppointmentsManager.WpfApp.Core
{
    public class CustomerCardVmFactory : ICustomerCardVmFactory
    {
        private Func<CustomerCardVm> _factory;

        public CustomerCardVmFactory(Func<CustomerCardVm> factory)
        {
            _factory = factory;
        }

        public CustomerCardVm CreateEmpty()
        {
            var customer = new Customer { Address = new() };
            var vm = _factory();
            vm.LoadEntity(customer);
            return vm;
        }

        public CustomerCardVm CreateFromExisting(Customer customer)
        {
            var vm = _factory();
            vm.LoadEntity(customer);
            return vm;
        }
    }
}
