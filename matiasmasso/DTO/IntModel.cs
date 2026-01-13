using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class IntModel:GuidNom, IModel
    {
        public int Value { get; set; }
        public IntModel(int id):base() {
            Value = id;
        }

        public override string ToString() => Value.ToString();
    }
}

