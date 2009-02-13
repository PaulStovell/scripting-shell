using System;
using System.Management.Automation;
using System.Windows;

namespace Sheldon.Demo.Automation.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "Window")]
    public class GetWindowCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true), Alias("Caption")]
        public string Title { get; set; }

        protected override void BeginProcessing()
        {
            var found = false;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Title.IndexOf(Title, StringComparison.CurrentCultureIgnoreCase) < 0) continue;
                WriteObject(window);
                found = true;
            }

            if (!found)
            {
                WriteWarning(string.Format("Sorry, no windows containing '{0}' could be found.", Title));
            }
        }
    }
}