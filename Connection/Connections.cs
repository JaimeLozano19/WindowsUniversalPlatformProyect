using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public class Connections : DataConnection
    {
        //       public Connections() : base(new SqlServerDataProvider("",
        //           SqlServerVersion.v2012),
        //           "Data Source=LUISMIGUEL;Database=sales_system;Integrated Security=False;TRUSTED_CONNECTION = False;")
        //      {
        //      }

          public Connections() : base(new LinqToDB.DataProvider.MySql.MySqlDataProvider(),
                                 "Server=localhost;Port=3306;Database=sales_system;Uid=root;Pwd='123456789'")
            { }

        public ITable<TUsers> TUsers => GetTable<TUsers>();
    }
}
