using AppointmentsManager.WpfApp.Mvvm.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentsManager.WpfApp.Core;
public class WindowManager : IWindowManager
{
    private IFactory<MainWindow> _mainWindowFactory;

    public WindowManager(IFactory<MainWindow> mainWindowFactory)
    {
        _mainWindowFactory = mainWindowFactory;
    }

    public void OpenMainWindow() => _mainWindowFactory.Create().Show();
}
