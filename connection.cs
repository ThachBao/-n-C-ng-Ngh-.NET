using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HMresourcemanagementsystem
{
    internal class connection
    {
        SqlCommand cmd;
        public SqlConnection con = new SqlConnection("Data Source=LAPTOP-K0ODBNC5\\SQLSEVER2022;Initial Catalog=QL_NHANSU;Integrated Security=True;Encrypt=False");

        public bool checkkey(string s)
        {
            cmd = new SqlCommand(s, con);
            int count = (int)cmd.ExecuteScalar();
            if(count > 0)
                return false;
            return true;
        }
    }
}
