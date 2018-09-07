using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using N_tier_solution.Models;

namespace N_tier_solution.DAL
{
    public class SolutionInitializer: DropCreateDatabaseIfModelChanges<SolutionContext>//DropCreateDatabaseIfModelChanges<SolutionContext>
    {
        protected override void Seed (SolutionContext context)
        {
            var formule = new List<Formulae>
            {
                new Formulae{ ID =1,Formula_Desc="AxB/C"},
                new Formulae{ ID =2,Formula_Desc="A mod BxC"},
                new Formulae{ ID =3,Formula_Desc="A^C-√BxC"},
            };
            formule.ForEach(f=> context.Formulae.Add(f));
            context.SaveChanges();
            var records = new List<Records>
            {
                new Records{ FormulaID =2,A=3,B=1,C=120 },
                new Records{ FormulaID =1,A=6,B=8,C=1 },
                new Records{ FormulaID =3,A=7,B=4,C=2 },
                new Records{ FormulaID =1,A=9,B=9,C=17 },
                new Records{ FormulaID =2,A=4,B=17,C=3 },
                new Records{ FormulaID =3,A=6,B=9,C=17 },
                new Records{ FormulaID =2,A=1,B=17,C=75 },
                new Records{ FormulaID =1,A=3,B=9,C=7 },
                new Records{ FormulaID =3,A=13,B=17,C=11 },
            };
            records.ForEach(x => x.Results = Compute(x.FormulaID, x.A, x.B, x.C));
            records.ForEach(r => context.Records.Add(r));
            context.SaveChanges();
        }
        /// <summary>
        /// This function perform computation based on the specified formularID
        /// </summary>
        /// <param name="formulaID"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private double Compute(int formulaID, int a, int b, int c)
        {
            double results = 0;

            switch (formulaID)
            {
                case 1:
                    results = a * b / c;
                    break;
                case 2:
                    results = a % b * c;
                    break;
                case 3:
                    results = Math.Pow(a, c) - Math.Sqrt(b) * c;
                break;

            }
            return results;
        }
    }
    
}