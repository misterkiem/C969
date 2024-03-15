using AppointmentsManager.DataAccess.Models;
using AppointmentsManager.WpfApp.Core;
using AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;
using AppointmentsManager.WpfApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;

[ObservableRecipient]
public partial class CustomerManagerControlVm : ControlVmBase, IRecipient<DeletedCustomerCardMessage>
{
    private IDataService _data;

    private readonly IDialogService _dialogService;

    private readonly IDtoVmFactory<CustomerDtoVm> _customerVmFac;

    public CustomerManagerControlVm(IDataService dataService,
        IDialogService dialogService,
        IDtoVmFactory<CustomerDtoVm> cardFac,
        IMessenger messenger)
    {
        _data = dataService;
        _dialogService = dialogService;
        _customerVmFac = cardFac;
        Messenger = messenger;
        Messenger.RegisterAll(this);
        Countries = CollectionViewSource.GetDefaultView(_data.Countries);
        Cities = CollectionViewSource.GetDefaultView(_data.Cities);
        CustomerCards = new(_data.Customers.Select(x =>
        {
            var cardVm = _customerVmFac.CreateFromExisting(x);
            cardVm.Cities = Cities;
            cardVm.Countries = Countries;
            return cardVm;
        }));
        SelectedCustomerCard = CustomerCards.FirstOrDefault();
    }

    [ObservableProperty]
    private ObservableCollection<CustomerDtoVm> customerCards;

    [ObservableProperty]
    private CustomerDtoVm? _selectedCustomerCard;

    [ObservableProperty]
    private ICollectionView? _countries;

    [ObservableProperty]
    private ICollectionView? _cities;

    [ObservableProperty]
    [CustomValidation(typeof(CustomerManagerControlVm), nameof(ValidateCityName))]
    private string? _newCityName;

    [ObservableProperty]
    [Required(ErrorMessage = "Country for new city is required.")]
    private Country? _newCityCountry;

    [ObservableProperty]
    [CustomValidation(typeof(CustomerManagerControlVm), nameof(ValidateCountryName))]
    private string? _newCountryName;

    [RelayCommand]
    void AddCountry()
    {
        ValidateProperty(NewCountryName, nameof(NewCountryName));
        if (GetErrors(nameof(NewCountryName)).Any())
        {
            _dialogService.ShowMessage("Invalid input for new country.", "Invalid New Country Input");
            return;
        }


        var newCountry = new Country() { country = NewCountryName };
        try
        {
            _data.SaveModel(newCountry);
            _dialogService.ShowMessage($"Successfully added new country {newCountry.country}.", "Added New Country");
        }
        catch (Exception ex)
        {
            _dialogService.ShowExceptionMessage(
                $"Unable to add new country to database", "Database Exception.", ex);
        }
    }

    [RelayCommand]
    void AddCity()
    {
        ValidateProperty(NewCityName, nameof(NewCityName));
        ValidateProperty(NewCityCountry, nameof(NewCityCountry));
        var cityErrors = GetErrors(nameof(NewCityName)).Concat(GetErrors(nameof(NewCityCountry)));
        if (cityErrors.Any())
        {
            _dialogService.ShowMessage("Invalid inputs for new city.", "Invalid New City Inputs");
            return;
        } 
        var newCity = new City()
        {
            city = NewCityName,
            Country = NewCityCountry
        };
        try
        {
            _data.SaveModel(newCity);
            _dialogService.ShowMessage($"Successfully added new city {newCity.city}.", "Added New City");
        }
        catch (Exception ex)
        {
            _dialogService.ShowExceptionMessage(
                $"Unable to add new city to database", "Database Exception", ex);
        }
    }

    [RelayCommand]
    void NewCustomer()
    {
        var card = _customerVmFac.CreateEmpty();
        card.Cities = Cities;
        card.Countries = Countries;
        CustomerCards.Add(card);
        SelectedCustomerCard = card;
    }

    public static ValidationResult ValidateCityName(string name, ValidationContext context)
    {
        var instance = (CustomerManagerControlVm)context.ObjectInstance;
        var cityName = instance.NewCityName;
        if (cityName is null) return new("Name for new city is required.");
        if (instance._data.Cities.Any(c => c.city.ToLower() == cityName.ToLower()))
            return new($"City with name {instance.NewCityName} is already in database.");
        return ValidationResult.Success!;
    }

    public static ValidationResult ValidateCountryName(string name, ValidationContext context)
    {
        var instance = (CustomerManagerControlVm)context.ObjectInstance;
        var countryName = instance.NewCountryName;
        if (countryName is null) return new("Name for new country is required.");
        if (instance._data.Countries.Any(c => c.country.ToLower() == countryName.ToLower()))
            return new($"Country with name {instance.NewCountryName} is already in database.");
        return ValidationResult.Success!;
    }

    public void Receive(DeletedCustomerCardMessage message)
    {
        CustomerCards.Remove(message.Sender);
    }
}