using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winforms
{

    public class AppState
    {
        public AppState() { 
        }
        public event EventHandler<EventArgs>? GotoForm;
        public int CurrentCount { get; set; } = 3;
        public void NavigateNext()
        {
            GotoForm?.Invoke(this, new EventArgs());
        }
    }
}

