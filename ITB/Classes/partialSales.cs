using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITB.DB
{
    partial class Sale
    {
        public string Client_Name
        {
            get 
            {
                return AppData.DB.Clients.Where(c => c.ID == this.Client_ID).FirstOrDefault().Name;
            }
        }
    }
}
