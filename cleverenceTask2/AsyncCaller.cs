using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace cleverenceTask2
{
    public class AsyncCaller
    {
        public AsyncCaller(EventHandler<CustomEventArgs> eventHandler)
        {
            _myEventHandler = eventHandler;
        }
        private EventHandler<CustomEventArgs> _myEventHandler;
        
        public bool Invoke(int timeout, object? sender, CustomEventArgs e)
        {
            var source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            var stateInvoke = false;

            Task taskInvoke = new Task(() =>
            {
                e.token = token;
                _myEventHandler.Invoke(sender, e);
                stateInvoke = true;
            }, token);
           
            taskInvoke.Start();
            taskInvoke.Wait(timeout);
            source.Cancel();

            return stateInvoke;
        }
    } 
}
