using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete.Exceptions.Newsletter
{
    public  class SubscribersAreNullException:Exception
    {

        public SubscribersAreNullException():base("No subscriber was found.")
        {
            
        }
    }
}
