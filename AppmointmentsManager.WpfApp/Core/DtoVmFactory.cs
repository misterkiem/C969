using AppointmentsManager.DataAccess.Models;
using AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;

namespace AppointmentsManager.WpfApp.Core
{ 
    public class DtoVmFactory<T> : IDtoVmFactory<T> where T : DtoVmBase
    {
        private Func<T> _factory;

        public DtoVmFactory(Func<T> factory)
        {
            _factory = factory;
        }

        public virtual T CreateEmpty()
        {
            var vm = _factory();
            vm.InitEmpty();
            return vm;
        }

        public virtual T CreateFromExisting(DbModel model)
        {
            var vm = _factory();
            vm.LoadEntity(model);
            return vm;
        }
    }
}
