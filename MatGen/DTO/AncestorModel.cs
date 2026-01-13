using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AncestorModel
    {
        public PersonModel Person { get; set; }
        public int Level { get; set; }
        public int Id { get; set; }
        public PersonModel? Pare { get; set; }
        public PersonModel? Mare { get; set; }

        public AncestorModel(PersonModel person, int level, int id )
        {
            Id = id;
            Person = person;
            Level = level;
        }

        public int Gen()=> (int)Math.Log(Id, 2);

        public override string ToString()
        {
            return $"Level {Level}: {Person.ToString()}";
        }
    }
}
