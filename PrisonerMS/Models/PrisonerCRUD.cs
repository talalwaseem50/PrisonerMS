using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PrisonerMS.Models
{
    public class PrisonerCRUD
    {
        static string ConnectionString = "data source=DESKTOP-QGDLCC0; database=PMS; integrated security = SSPI;";
        //Others

        public static Prisoner GetPrisoner(int id)
        {
            using (SqlConnection Server = new SqlConnection(ConnectionString))
            {
                Server.Open();
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "GetPrisoner";
                    cmd.Connection = Server;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //input para
                    cmd.Parameters.Add(new SqlParameter("@pid", id));

                    //output parameters
                    cmd.Parameters.Add(new SqlParameter("@pnum", SqlDbType.VarChar, 30));
                    cmd.Parameters["@pnum"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@fname", SqlDbType.VarChar, 30));
                    cmd.Parameters["@fname"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@gen", SqlDbType.Char, 1));
                    cmd.Parameters["@gen"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@faname", SqlDbType.VarChar, 30));
                    cmd.Parameters["@faname"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@cnic", SqlDbType.Char, 11));
                    cmd.Parameters["@cnic"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@pAdd", SqlDbType.VarChar, 100));
                    cmd.Parameters["@pAdd"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@dateb", SqlDbType.VarChar, 10));
                    cmd.Parameters["@dateb"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@vDay", SqlDbType.VarChar, 10));
                    cmd.Parameters["@vDay"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@vTime", SqlDbType.VarChar, 5));
                    cmd.Parameters["@vTime"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@datee", SqlDbType.Date));
                    cmd.Parameters["@datee"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.Int));
                    cmd.Parameters["@flag"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                }
                catch (SqlException ex)
                {
                    Console.Write("SQL Server Error" + ex);
                }

                int Flag = (int)cmd.Parameters["@flag"].Value;  //check if required prisoner id retrieved 

                if (Flag == 1)
                {
                    Prisoner tPrisoner = new Prisoner();
                    tPrisoner.PrisonerID = id;
                    tPrisoner.PrisonerNo = (string)cmd.Parameters["@pnum"].Value;
                    tPrisoner.FullName = (string)cmd.Parameters["@fname"].Value;
                    tPrisoner.Gender = Convert.ToChar(cmd.Parameters["@gen"].Value);
                    tPrisoner.FatherName = (string)cmd.Parameters["@faName"].Value;
                    tPrisoner.CNIC = (string)cmd.Parameters["@cnic"].Value;
                    tPrisoner.Address = (string)cmd.Parameters["@pAdd"].Value;
                    tPrisoner.DateBirth = (string)cmd.Parameters["@dateb"].Value;
                    tPrisoner.VisitingDay = (string)cmd.Parameters["@vDay"].Value;
                    tPrisoner.VisitingTime = (string)cmd.Parameters["@vTime"].Value;

                    //DateTime dt = (DateTime)cmd.Parameters["@datee"].Value;
                    //string s = dt.ToString("dd/MM/yyyy");
                    //String t = dt.ToString("HH:mm");

                    tPrisoner.DateEntry = ((DateTime)cmd.Parameters["@datee"].Value).ToString("dd/MM/yyyy");
                    

                    return tPrisoner;
                }
                else
                    return null;
            }
        }

        public static List<Prisoner> GetAllPrisoners()
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();
                List<Prisoner> PrisonersList = new List<Prisoner>();

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    DataTable sqlPrisoners = new DataTable();
                    SqlDataAdapter Data = new SqlDataAdapter("Select PID From [Prisoner]", ServerConnection);
                    Data.Fill(sqlPrisoners);

                    foreach (DataRow row in sqlPrisoners.Rows)
                    {
                        Prisoner tprisoner = GetPrisoner((int)row["PID"]);
                        if (tprisoner != null)
                            PrisonersList.Add(tprisoner);
                    }
                }
                catch (SqlException ex)
                {
                    Console.Write("SQL Server Error" + ex);
                }
                finally
                {
                    ServerConnection.Close();
                }

                return PrisonersList;
            }
        }






    }
}