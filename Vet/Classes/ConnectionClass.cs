using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.DBModel;

namespace Vet.Classes
{
    public class ConnectionClass
    {
        public static DBEntities VetClinicContext = new DBEntities();

        public static Users CurrentUser;
    }
}
