﻿using System;

namespace Sheldon.Helpers
{
    /// <summary>
    /// An IDisposable that invokes a delegate when Dispose is called. 
    /// </summary>
    internal class CallbackDisposable : IDisposable
    {
        private readonly Action _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="CallbackDisposable"/> class.
        /// </summary>
        /// <param name="disposeAction">The dispose action.</param>
        public CallbackDisposable(Action disposeAction)
        {
            _action = disposeAction;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _action();
        }
    }
}
