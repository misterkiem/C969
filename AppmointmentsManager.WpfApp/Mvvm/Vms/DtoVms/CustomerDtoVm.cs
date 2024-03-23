using AppointmentsManager.DataAccess.Models;
using AppointmentsManager.WpfApp.Mvvm.Vms.Messages;
using AppointmentsManager.WpfApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;

[ObservableRecipient]
public partial class CustomerDtoVm : DtoVmBase
{
    private static string _invalidPhoneMessage =
        @"Invalid phone format. Valid phone formats:
xxx-xxx-xxxx
xxx-xxxx
xxxxxxxxxx
xxxxxxx";

    private static Regex[] _phoneFormats = new Regex[]
    {
        //dashes and dashes 10 number
        new (@"^[0-9]{3}-[0-9]{3}-[0-9]{4}$"),
        //dashes and dashes 7 number
        new (@"^[0-9]{3}-[0-9]{4}$"),
        //numbers only 10 number
        new (@"^[0-9]{10}$"),
        //numbers only 7 number
        new (@"^[0-9]{7}$")
    };

    private bool _initialized = false;

    private Customer _customer = null!;

    public override DbModel DbModel => _customer;

    public override IEnumerable<DbModel> DependentEntities =>
        new DbModel[] { _customer.Address, _customer.Address.City, _customer.Address.City.Country };

    public CustomerDtoVm(IDataService data,
        IDialogService dialogService,
        IMessenger messenger) :
        base(data, dialogService)
        => Messenger = messenger;

    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private bool _isModified = false;

    [ObservableProperty]
    [Required]
    [NotifyDataErrorInfo]
    [NotifyCanExecuteChangedFor(nameof(SaveCustomerCommand))]
    private string? _name;

    [ObservableProperty]
    [CustomValidation(typeof(CustomerDtoVm), nameof(ValidatePhone))]
    [NotifyDataErrorInfo]
    [NotifyCanExecuteChangedFor(nameof(SaveCustomerCommand))]
    private string? _phone;

    [ObservableProperty]
    [Required]
    [NotifyDataErrorInfo]
    [NotifyCanExecuteChangedFor(nameof(SaveCustomerCommand))]
    private string? _address;

    [ObservableProperty]
    private string? _address2 = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyCanExecuteChangedFor(nameof(SaveCustomerCommand))]
    [Required]
    private City? _city = null!;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyCanExecuteChangedFor(nameof(SaveCustomerCommand))]
    [Required]
    public Country? _country = null!;

    [ObservableProperty]
    private ICollectionView? _cities;

    [ObservableProperty]
    private ICollectionView? _countries;

    public bool CanSave => !HasErrors;

    [RelayCommand(CanExecute = nameof(CanSave))]
    void SaveCustomer()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            _dialog.ShowMessage("This Customer has errors. Please check inputs and correct.", "Input Errors For This Customer");
            return;
        }
        if (!SaveToDb()) return;
        Id = _customer.Id;
        IsModified = false;
    }

    [RelayCommand]
    void DeleteCustomer()
    {
        if (DeleteFromDb()) Messenger.Send(new DeletedCustomerCardMessage(this));
    }

    public static ValidationResult? ValidatePhone(string name, ValidationContext context)
    {
        var instance = (CustomerDtoVm)context.ObjectInstance;
        var phone = instance.Phone;
        if (phone is null) return new("The Phone field is required.");
        var validFormat = _phoneFormats.Any(x => x.IsMatch(phone));
        if (!validFormat) return new(_invalidPhoneMessage);
        return ValidationResult.Success;
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (!_initialized) return;
        if (e.PropertyName == nameof(IsModified)) return;
        if (e.PropertyName == nameof(HasErrors)) return;
        if (e.PropertyName == nameof(Cities)) return;
        if (e.PropertyName == nameof(Countries)) return;
        IsModified = true;

    }

    partial void OnCityChanged(City? value) => Country = value?.Country;

    partial void OnCountryChanged(Country? value) { if (City is not null) City.Country = value; }

    public override void InitEmpty()
    {
        _customer = new() { Address = new() };
        _initialized = true;
    }

    public override void LoadEntity(DbModel entity)
    {
        if (entity is not Customer customer) return;
        base.LoadEntity(customer);
        TypeName = nameof(Customer);
        _customer = customer;
        Id = customer.customerId;
        Name = customer.customerName;
        Address = customer.Address.address;
        Address2 = customer.Address.address2;
        Phone = customer.Address.phone;
        City = customer.Address.City;
        Country = customer.Address.City?.Country;
        ValidateAllProperties();
        _initialized = true;
    }


    protected override void SaveEntity()
    {
        if (_customer is null) return;
        _customer.customerName = Name;
        _customer.Address.address = Address;
        _customer.Address.address2 = Address2 ?? string.Empty;
        _customer.Address.phone = Phone;
        _customer.Address.City = City;
        _customer.Address.City!.Country = Country;
    }
}
