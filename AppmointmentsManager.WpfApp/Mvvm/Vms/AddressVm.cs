
using AppointmentsManager.DataAccess.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AppointmentsManager.WpfApp.Mvvm.Vms;

[ObservableObject]
public partial class AddressVm : DbVm
{

    [ObservableProperty]
    private int addressId;

    [ObservableProperty]
    private string address;

    [ObservableProperty]
    private string address2;

    [ObservableProperty]
    private int cityId;

    [ObservableProperty]
    private string postalCode;

    [ObservableProperty]
    private string phone;

    [ObservableProperty]
    private DateTime createDate;

    [ObservableProperty]
    private string createdBy;

    [ObservableProperty]
    private DateTime lastUpdate;

    [ObservableProperty]
    private string lastUpdateBy;

    [ObservableProperty]
    private CityVm city;

    [ObservableProperty]
    private ICollection<Customer> customers;

    public AddressVm(Address addressModel) : base(addressModel)
    {
        addressId = addressModel.addressId;
        address = addressModel.address;
        address2 = addressModel.address2;
        cityId = addressModel.cityId;
        postalCode = addressModel.postalCode;
        phone = addressModel.phone;
        createDate = addressModel.createDate;
        createdBy = addressModel.createdBy;
        lastUpdate = addressModel.lastUpdate;
        lastUpdateBy = addressModel.lastUpdateBy;
        city = addressModel.City;
    }

}