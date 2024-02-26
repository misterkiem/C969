using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.WindowVms;
public abstract class WindowVmBase
{
    public Action? CloseAction { get; set; }
}
