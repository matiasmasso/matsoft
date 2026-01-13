using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Helpers
{
    public class MatEventArgs<V> : EventArgs
    {
        public V? Value { get; set; }

        public MatEventArgs(V? value)
        {
            this.Value = value;
        }
    }
    public class MatEventArgs<V,T> : EventArgs
    {
        public T? Tag { get; set; }
        public V? Value { get; set; }

        public MatEventArgs(V? value, T? tag)
        {
            this.Value = value;
            this.Tag = tag;
        }
    }
}
