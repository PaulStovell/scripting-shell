using System;
using System.IO;
using System.Linq;
using System.Reflection;
using IronPython.Hosting;

namespace Sheldon.Scripting.IronPython
{
    /// <summary>
    /// Represents a Scripting Context for IronPython.
    /// </summary>
    public class IronPythonScriptingContext : AbstractScriptingContext, IIronPythonScriptingContext
    {
        private readonly PythonEngine _pythonEngine;

        /// <summary>
        /// Initializes a new instance of the <see cref="IronPythonScriptingContext"/> class.
        /// </summary>
        public IronPythonScriptingContext()
            : base(">> ")
        {
            _pythonEngine = new PythonEngine();
            _pythonEngine.SetStandardOutput(new ScriptOutputWriterStream(Output));
        }

        /// <summary>
        /// Pushes a variable into the current scripting context as a global variable.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="variable">The variable.</param>
        public void InjectGlobalVariable(string name, object variable)
        {
            _pythonEngine.Globals[name] = variable;
        }

        /// <summary>
        /// Extracts a script file from an embedded resource in an assembly and loads it into the current scripting context.
        /// </summary>
        /// <param name="containingAssembly">The assembly containing the script file.</param>
        /// <param name="scriptFile">The script file, specified as a file name, for example: <c>Scripts/Foo.py</c>.</param>
        public void LoadEmbeddedScript(Assembly containingAssembly, string scriptFile)
        {
            var resourceNames = containingAssembly.GetManifestResourceNames();
            var bestMatch = resourceNames.Where(name => name.EndsWith(scriptFile.Replace('\\', '.').Replace('/', '.'), StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (bestMatch == null)
            {
                throw new Exception(string.Format("Script file ending with '{0}' could not be found in assembly '{1}'. Ensure that the file is marked as an Embedded Resource.", scriptFile, containingAssembly.FullName));
            }

            var resourceStream = containingAssembly.GetManifestResourceStream(bestMatch);
            if (resourceStream == null)
            {
                throw new Exception(string.Format("Script file '{0}' was found in assembly '{1}', but the resource stream was null.", bestMatch, containingAssembly.FullName));
            }

            using (var reader = new StreamReader(resourceStream))
            {
                _pythonEngine.Execute(reader.ReadToEnd());
            }
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        public override void ExecuteCommand(string commandLine)
        {
            Output.WriteLine(Prompt + commandLine);
            try
            {
                _pythonEngine.Execute(commandLine);
            }
            catch (Exception ex)
            {
                using (Output.ChangeForegroundColor(ConsoleColor.Red))
                {
                    Output.EnsureNewLine();
                    Output.WriteLine(ex.ToString());
                }
            }
        }
    }
}