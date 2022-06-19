
using System.Data;
using System.Data.SqlClient;

namespace FullStack
{
    public  class DbFactory
    {
        private readonly string conStr;
        public  DbFactory()
        {
            conStr = @"server=DESKTOP-D81NGBO;database=mytestdb;trusted_connection=true; Integrated Security=true";
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(conStr);
            }

        }



    }
}
