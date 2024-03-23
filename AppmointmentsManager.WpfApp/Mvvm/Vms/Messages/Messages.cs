using AppointmentsManager.DataAccess.Models;
using AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.Messages;

public record class DeletedCustomerCardMessage(CustomerDtoVm Sender);

public record class AppointmentDateChangedMessage(DateOnly OldValue, DateOnly NewValue);

public record class AppointmentErrorsChangedMessage(AppointmentDtoVm Appointment);

public record class AppointmentsDeletedMessage(IEnumerable<Appointment> Appointments);