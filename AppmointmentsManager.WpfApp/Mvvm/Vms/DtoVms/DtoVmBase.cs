using AppointmentsManager.DataAccess.Models;
using AppointmentsManager.WpfApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;

public abstract partial class DtoVmBase : ObservableValidator
{
    protected readonly IDataService _data;

    protected readonly IDialogService _dialog;

    protected string TypeName { get; set; } = null!;

    public DtoVmBase(IDataService service, IDialogService dialog)
    {
        _data = service;
        _dialog = dialog;
    }

    public abstract DbModel DbModel { get; }

    public virtual IEnumerable<DbModel> DependentEntities { get; } = Enumerable.Empty<DbModel>();

    public abstract void InitEmpty();

    public virtual void LoadEntity(DbModel entity) => TypeName = entity.GetType().Name;

    public bool SaveToDb()
    {
        SaveEntity();
        try
        {
            _data.SaveModel(DbModel, DependentEntities);
            _dialog.ShowMessage($"Successfully saved {TypeName} to database.", "Save Successful");
            return true;
        }
        catch (Exception ex)
        {
            _dialog.ShowExceptionMessage($"Unable to save {TypeName} to database.", "Database Error", ex);
        }
        return false;
    }

    public bool DeleteFromDb()
    {
        var result = _dialog.ShowConfirmMessage(
            $"Are you sure you want to delete this {TypeName}?", $"Delete {TypeName}?");
        if (!result) return false;
        try
        {
            _data.DeleteModel(DbModel);
            _dialog.ShowMessage($"Successfully deleted {TypeName} from db.", "Delete Successful");
            return true;
        }
        catch (Exception ex)
        {
            _dialog.ShowExceptionMessage(
                $"Unable to delete {TypeName} from database.", "Database Exception", ex);
        }
        return false;
    }

    protected abstract void SaveEntity();
}