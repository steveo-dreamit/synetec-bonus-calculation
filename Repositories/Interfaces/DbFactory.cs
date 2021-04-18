using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    class DbFactory : IDbFactory
    {
        DBCustomerEntities dbContext;

        public DBCustomerEntities Init()
        {
            return dbContext ?? (dbContext = new DBCustomerEntities());
        }
    }
}
