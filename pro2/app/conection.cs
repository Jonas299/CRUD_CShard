using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pro2.app
{
    internal class conection
    {

        protected string getConection() 
        {
            var conString = ConfigurationManager.ConnectionStrings["dbEmployees"].ConnectionString;
            return conString;
        }
    }
}
