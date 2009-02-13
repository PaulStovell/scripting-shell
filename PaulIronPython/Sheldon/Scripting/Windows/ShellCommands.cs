using System.Windows.Input;

namespace Sheldon.Scripting.Windows
{
    public static class ShellCommands
    {
        public static RoutedUICommand Clear = new RoutedUICommand("Clear", "Clear", typeof(ShellCommands));
        public static RoutedUICommand Execute = new RoutedUICommand("Execute", "Execute", typeof(ShellCommands));
        public static RoutedUICommand CommandHistoryUp = new RoutedUICommand("CommandHistoryDown", "History Down", typeof(ShellCommands));
        public static RoutedUICommand CommandHistoryDown = new RoutedUICommand("CommandHistoryDown", "History Down", typeof(ShellCommands));
    }
}
