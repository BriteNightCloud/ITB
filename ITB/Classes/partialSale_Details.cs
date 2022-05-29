using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITB.DB
{
    partial class Sale_Details
    {
        public decimal Cost
        {
            get
            {
                return Price * Count;
            }
        }

        public string Nomenclature_Name
        {
            get
            {
                return AppData.DB.Database.SqlQuery<Nomenclature>("SELECT * FROM Nomenclature WHERE ID = " + Nomenclature_ID).ToList().First().Name;
            }
        }
    }
}
