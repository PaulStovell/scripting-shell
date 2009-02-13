using System;

namespace Sheldon.Scripting.PowerShell
{
    /// <summary>
    /// A scripting context for Windows PowerShell.
    /// </summary>
    public interface IPowerShellScriptingContext : IScriptingContext
    {
        /// <summary>
        /// Loads a PowerShell cmdlet into the application.
        /// </summary>
        void RegisterCmdlet(Type type, string name, params string[] aliases);

        /// <summary>
        /// Injects a variable into the session store for the current PowerShell runspace. The variable can be retrieved from your Cmdlets by 
        /// calling 
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        void AddContextVariable(string name, object value);
    }
}