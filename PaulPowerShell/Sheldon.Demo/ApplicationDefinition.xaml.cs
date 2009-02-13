using System.Windows;
using Sheldon.Demo.Automation.Cmdlets;
using Sheldon.Scripting.PowerShell;

namespace Sheldon.Demo
{
    public partial class ApplicationDefinition : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var context = new PowerShellScriptingContext();
            context.RegisterCmdlet(typeof(GetWindowCmdlet), "Get-Window", "gw", "gwin");
            
            MainWindow = new ConsoleWindow(context);
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
