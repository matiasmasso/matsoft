using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatHelperCFwk
{
    public delegate void ProgressBarHandler(long min, long max, long value, string label, ref bool CancelRequest);

    public delegate void ToggleProgressBarHandler(bool visible);

}
