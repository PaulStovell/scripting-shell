using Sheldon.Scripting;

namespace Sheldon.Demo
{
    public partial class ConsoleWindow
    {
        public ConsoleWindow(IScriptingContext scriptingContext)
        {
            InitializeComponent();
            
            Shell1.ScriptingContext = Shell2.ScriptingContext = Shell3.ScriptingContext = scriptingContext;
        }
    }
}
