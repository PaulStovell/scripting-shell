using System.Windows;
using Sheldon.Demo.Automation;
using Sheldon.Scripting.IronPython;
using System.Reflection;

namespace Sheldon.Demo
{
    public partial class ApplicationDefinition    
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // In reality, I expect applications to setup these objects and put them in an ambient ServiceLocator/Inversion of Control container.
            var context = new IronPythonScriptingContext();
            context.InjectGlobalVariable("automation_context", new AutomationContext { Application = this, ScriptingContext = context });
            context.LoadEmbeddedScript(Assembly.GetExecutingAssembly(), "Automation/Scripts/(Initialize).py");
            context.LoadEmbeddedScript(Assembly.GetExecutingAssembly(), "Automation/Scripts/Globals.py");
            context.LoadEmbeddedScript(Assembly.GetExecutingAssembly(), "Automation/Scripts/UIAutomation.py");

            // Launch the application
            MainWindow = new ConsoleWindow(context);
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
