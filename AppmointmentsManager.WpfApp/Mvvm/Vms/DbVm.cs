using AppointmentsManager.DataAccess.Models;

namespace AppointmentsManager.WpfApp.Mvvm.Vms;

public abstract class DbVm
{
    protected DbModel _model;

    public DbVm(DbModel model) { _model = model; }
}
