using System;
using System.Globalization;
using System.Management.Automation.Host;
using System.Reflection;
using System.Threading;

namespace Sheldon.Scripting.PowerShell
{
    public class PowerShellHost : PSHost
    {
        private readonly PowerShellUserInterfaceAdapter _uiAdapter;
        private readonly Guid _instanceID = Guid.NewGuid();

        public PowerShellHost(ScriptOutputDispatcher dispatcher)
        {
            _uiAdapter = new PowerShellUserInterfaceAdapter(dispatcher);
        }

        public override CultureInfo CurrentCulture
        {
            get { return Thread.CurrentThread.CurrentCulture; }
        }

        public override CultureInfo CurrentUICulture
        {
            get { return Thread.CurrentThread.CurrentUICulture; }
        }

        public override void EnterNestedPrompt()
        {
        }

        public override void ExitNestedPrompt()
        {
        }

        public override Guid InstanceId { get { return _instanceID; } }

        public override string Name
        {
            get { return "PowerShellHost"; }
        }

        public override void NotifyBeginApplication()
        {
        }

        public override void NotifyEndApplication()
        {
        }

        public override void SetShouldExit(int exitCode)
        {
        }

        public override PSHostUserInterface UI
        {
            get { return _uiAdapter; }
        }

        public override Version Version
        {
            get
            {
                var executing = Assembly.GetExecutingAssembly();
                var name = executing.GetName();
                return name.Version;
            }
        }
    }
}
