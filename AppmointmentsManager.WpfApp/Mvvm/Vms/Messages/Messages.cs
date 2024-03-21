using AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.Messages;

public record class DeletedCustomerCardMessage(CustomerDtoVm Sender);

public record class AppointmentDateChangedMessage(DateOnly oldValue, DateOnly newValue);