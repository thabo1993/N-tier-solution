using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace N_tier_solution.Models
{

    public class Records
    {
        [Key]
        public int ID { get; set; }
        public int FormulaID { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public double Results { get; set; }

        public virtual Formulae Formular { get; set; }

        
    }
}