using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PrisonerMS.Models
{
    public class AccountCRUD
    {
        static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PMS"].ConnectionString;
        //Others

        public static Account UserLogin(string uname, string password)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();
                SqlCommand cmd = new SqlCommand();
                try
                {
                    //calling login procedure from db
                    cmd.CommandText = "LoginValidate";
                    cmd.Connection = ServerConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //passing parameters to procedure
                    cmd.Parameters.Add(new SqlParameter("@un", uname));
                    cmd.Parameters.Add(new SqlParameter("@pa", password));

                    //passing output variables to procedure
                    cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.Int));
                    cmd.Parameters["@flag"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.Int));
                    cmd.Parameters["@uid"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@acc_type", SqlDbType.VarChar, 2));
                    cmd.Parameters["@acc_type"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@fname", SqlDbType.VarChar, 30));
                    cmd.Parameters["@fname"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@prid", SqlDbType.Int));
                    cmd.Parameters["@prid"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();  //run procedure
                }
                catch (SqlException ex)
                {
                    Console.Write("SQL Server Error" + ex);
                }
                finally
                {
                    ServerConnection.Close();
                }
                //get output values from procedure
                int Flag = (int)cmd.Parameters["@flag"].Value;

                if (Flag == 1)
                {
                    Account retAcc = new Account();
                    retAcc.UserID = (int)cmd.Parameters["@uid"].Value;
                    retAcc.AccType = (string)cmd.Parameters["@acc_type"].Value;
                    retAcc.FullName = (string)cmd.Parameters["@fname"].Value;
                    retAcc.Prison = new Prison();
                    retAcc.Prison.PrisonID = (int)cmd.Parameters["@prid"].Value;
                    return retAcc;
                }
                else
                    return null;
            }
        }
    }
}