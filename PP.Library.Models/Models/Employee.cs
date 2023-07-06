using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PP_Library.Models
{
    public class Employee
    {
        /*
         * An Int property called Id
         * A String property called Name
         * A Decimal property called Rate
         */
        public Employee() 
        { 
            Id = 0;
            Name = string.Empty;
            Rate = 0;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name}. {Rate}.\n";
        }
    }
    /* Allow users to supply CRUD (Create, Read, Update, and Delete) functionality
     * 
     * Create a Singleton file. 
     */
}
