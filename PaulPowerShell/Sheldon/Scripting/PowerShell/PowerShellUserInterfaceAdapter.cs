using System;
using System.Collections.Generic;
using System.Management.Automation.Host;
using System.Management.Automation;
using System.Collections.ObjectModel;

namespace Sheldon.Scripting.PowerShell
{
    public class PowerShellUserInterfaceAdapter : PSHostUserInterface
    {
        private readonly PowerShellRawUserInterfaceAdapter _rawUI;
        private readonly ScriptOutputDispatcher _outputDispatcher;
        
        public PowerShellUserInterfaceAdapter(ScriptOutputDispatcher outputDispatcher)
        {
            _rawUI = new PowerShellRawUserInterfaceAdapter(outputDispatcher);
            _outputDispatcher = outputDispatcher;
        }

        public override Dictionary<string, PSObject> Prompt(string caption, string message, Collection<FieldDescription> descriptions)
        {
            throw new NotImplementedException();
        }

        public override int PromptForChoice(string caption, string message, Collection<ChoiceDescription> choices, int defaultChoice)
        {
            throw new NotImplementedException();
        }

        public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName, System.Management.Automation.PSCredentialTypes allowedCredentialTypes, System.Management.Automation.PSCredentialUIOptions options)
        {
            throw new NotImplementedException();
        }

        public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName)
        {
            throw new NotImplementedException();
        }

        public override PSHostRawUserInterface RawUI
        {
            get { return _rawUI; }
        }

        public override string ReadLine()
        {
            throw new NotImplementedException();
        }

        public override System.Security.SecureString ReadLineAsSecureString()
        {
            throw new NotImplementedException();
        }

        public override void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string value)
        {
            using (_outputDispatcher.ChangeForegroundColor(foregroundColor))
            {
                _outputDispatcher.Write(value);
            }
        }

        public override void Write(string value)
        {
            _outputDispatcher.Write(value);
        }

        public override void WriteDebugLine(string message)
        {
            using (_outputDispatcher.ChangeForegroundColor(ConsoleColor.White))
            {
                _outputDispatcher.WriteLine(message);
            }
        }

        public override void WriteErrorLine(string value)
        {
            using (_outputDispatcher.ChangeForegroundColor(ConsoleColor.Red))
            {
                _outputDispatcher.WriteLine(value);
            }
        }

        public override void WriteLine(string value)
        {
            _outputDispatcher.WriteLine(value);
        }

        public override void WriteProgress(long sourceId, ProgressRecord record)
        {
            throw new NotImplementedException();
        }

        public override void WriteVerboseLine(string message)
        {
            using (_outputDispatcher.ChangeForegroundColor(ConsoleColor.DarkGray))
            {
                _outputDispatcher.WriteLine(message);
            }
        }

        public override void WriteWarningLine(string message)
        {
            using (_outputDispatcher.ChangeForegroundColor(ConsoleColor.Yellow))
            {
                _outputDispatcher.WriteLine(message);
            }
        }
    }
}
