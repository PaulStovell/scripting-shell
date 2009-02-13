using System;
using System.Management.Automation.Host;

namespace Sheldon.Scripting.PowerShell
{
    public class PowerShellRawUserInterfaceAdapter : PSHostRawUserInterface
    {
        private readonly ScriptOutputDispatcher _outputDispatcher;

        public PowerShellRawUserInterfaceAdapter(ScriptOutputDispatcher outputDispatcher)
        {
            _outputDispatcher = outputDispatcher;
        }

        public override ConsoleColor ForegroundColor { get; set; }
        public override ConsoleColor BackgroundColor { get; set; }
        public override Size BufferSize { get; set; }
        public override Coordinates CursorPosition { get; set; }
        public override int CursorSize { get; set; }
        public override Coordinates WindowPosition { get; set; }
        public override Size WindowSize { get; set; }
        public override string WindowTitle { get; set; }

        public override void FlushInputBuffer()
        {
        }

        public override BufferCell[,] GetBufferContents(Rectangle rectangle)
        {
            return null;
        }

        public override bool KeyAvailable
        {
            get { return false; }
        }

        public override Size MaxPhysicalWindowSize
        {
            get { throw new NotImplementedException(); }
        }

        public override Size MaxWindowSize
        {
            get { throw new NotImplementedException(); }
        }

        public override KeyInfo ReadKey(ReadKeyOptions options)
        {
            throw new NotImplementedException();
        }

        public override void ScrollBufferContents(Rectangle source, Coordinates destination, Rectangle clip, BufferCell fill)
        {
        }

        public override void SetBufferContents(Rectangle rectangle, BufferCell fill)
        {
            _outputDispatcher.Clear();
        }

        public override void SetBufferContents(Coordinates origin, BufferCell[,] contents)
        {
            _outputDispatcher.Clear();
        }
    }
}