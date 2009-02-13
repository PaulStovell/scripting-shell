using System;
using Sheldon.Scripting;

namespace Sheldon.Demo.Automation
{
    /// <summary>
    /// Represents a common set of objects shared between the IronPython runtime and this 
    /// application. 
    /// </summary>
    public class AutomationContext
    {
        /// <summary>
        /// Gets or sets the current application.
        /// </summary>
        /// <value>The application.</value>
        public ApplicationDefinition Application { get; set; }

        /// <summary>
        /// Gets or sets the current scripting context.
        /// </summary>
        /// <value>The scripting context.</value>
        public IScriptingContext ScriptingContext { get; set; }

        /// <summary>
        /// Exits the application.
        /// </summary>
        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}