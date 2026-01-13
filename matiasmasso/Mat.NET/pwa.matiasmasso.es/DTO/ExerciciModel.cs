using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ExerciciModel
    {
        public EmpModel.EmpIds Emp {  get; set; }
        public int Year { get; set; }

        public ExerciciModel() { }
        public ExerciciModel(EmpModel.EmpIds emp, int year) {
        Emp = emp; Year = year;
        }
    }
}
