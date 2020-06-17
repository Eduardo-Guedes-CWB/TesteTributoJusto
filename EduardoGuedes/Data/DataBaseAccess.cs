using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EduardoGuedes.Data
{
    public class DataBaseAccess
    {
        public static SqlConnection ConexaoDB() 
        {
            SqlConnection conexao = new SqlConnection(@"data source= ESG-INSPIRON001; Integrated Security= SSPI; Initial Catalog= eduardo_guedes;MultipleActiveResultSets=true;");
            return conexao;
        }
    }
}