using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace Sheldon.Scripting.PowerShell
{
    /// <summary>
    /// A scripting context for Windows PowerShell.
    /// </summary>
    public class PowerShellScriptingContext : AbstractScriptingContext, IPowerShellScriptingContext
    {
        private readonly PowerShellHost _host;
        private readonly Runspace _runspace;

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerShellScriptingContext"/> class.
        /// </summary>
        public PowerShellScriptingContext() : base("PS> ")
        {
            _host = new PowerShellHost(Output);
            _runspace = RunspaceFactory.CreateRunspace(_host);
            Runspace.DefaultRunspace = _runspace;
            _runspace.Open();
            UpdatePrompt();
        }

        /// <summary>
        /// Loads a PowerShell cmdlet into the application.
        /// </summary>
        public void RegisterCmdlet(Type type, string name, params string[] aliases)
        {
            _runspace.RunspaceConfiguration.Cmdlets.Append(new CmdletConfigurationEntry(name, type, null));
            foreach (var alias in aliases)
            {
                _runspace.CreatePipeline(string.Format("new-alias {0} {1}", alias, name)).Invoke();
            }
            _runspace.RunspaceConfiguration.Cmdlets.Update();
        }

        /// <summary>
        /// Injects a variable into the session store for the current PowerShell runspace. The variable can be retrieved from your Cmdlets by 
        /// calling 
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void AddContextVariable(string name, object value)
        {
            _runspace.SessionStateProxy.SetVariable(name, value);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="command">The command.</param>
        public override void ExecuteCommand(string command)
        {
            Output.WriteLine(Prompt + command);
            try
            {
                var engine = GetExecutionEngine();
                if (engine == null) throw new InvalidOperationException("No ExecutionContext was available in the current runspace.");

                var results = engine.InvokeCommand.InvokeScript(command, false, PipelineResultTypes.Output, null);
                foreach (var psObject in results)
                {
                    Output.WriteLine(psObject.ToString());
                }
            }
            catch (Exception ex)
            {
                using (Output.ChangeForegroundColor(ConsoleColor.Red))
                {
                    Output.EnsureNewLine();
                    Output.WriteLine(ex.ToString());
                }
            }
            finally
            {
                UpdatePrompt();   
            }
        }

        public override string CompleteCommand(string command)
        {
            var engine = GetExecutionEngine();
            if (engine != null)
            {
                command = engine.InvokeCommand.ExpandString(command);
            }
            return command;
        }

        private void UpdatePrompt()
        {
            var engine = GetExecutionEngine();
            if (engine == null) return;

            var path = engine.SessionState.Path.CurrentLocation.Path;
            if (path.Length > 20)
            {
                path = path.Substring(0, 5) + "..." + path.Substring(path.Length - 15, 15);
            }

            Prompt = string.Format("PS {0}> ", path);
        }

        private EngineIntrinsics GetExecutionEngine()
        {
            return _runspace.SessionStateProxy.GetVariable("ExecutionContext") as EngineIntrinsics;
        }
    }
}
