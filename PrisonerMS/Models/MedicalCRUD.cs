using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PrisonerMS.Models
{
    public class MedicalCRUD
    {
        static string ConnectionString = "data source=DESKTOP-QGDLCC0; database=PMS; integrated security = SSPI;";
        //Others

        public static Medical GetMedical(int id)
        {
            using (SqlConnection Server = new SqlConnection(ConnectionString))
            {
                Server.Open();
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "GetHealthRecord";
                    cmd.Connection = Server;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //input para
                    cmd.Parameters.Add(new SqlParameter("@hrid", id));

                    //output parameters
                    cmd.Parameters.Add(new SqlParameter("@sym", SqlDbType.VarChar, 1000));
                    cmd.Parameters["@sym"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@dig", SqlDbType.VarChar, 1000));
                    cmd.Parameters["@dig"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@datee", SqlDbType.DateTime));
                    cmd.Parameters["@datee"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@pd", SqlDbType.Int));
                    cmd.Parameters["@pd"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.Int));
                    cmd.Parameters["@flag"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.Write("SQL Server Error" + ex);
                }

                int Flag = (int)cmd.Parameters["@flag"].Value;

                if (Flag == 1)
                {
                    Medical m = new Medical();
                    m.MedicalID = id;
                    m.Symptoms = (string)cmd.Parameters["@sym"].Value;
                    m.Diagnosis = (string)cmd.Parameters["@dig"].Value;
                    m.EntryDate = ((DateTime)cmd.Parameters["@datee"].Value).ToString("dd/MM/yyyy");
                    m.EntryTime = ((DateTime)cmd.Parameters["@datee"].Value).ToString("HH:mm:ss");
                    m.Prisoner = new Prisoner();
                    m.Prisoner.PrisonerID = Convert.ToInt32(cmd.Parameters["@pd"].Value);
                    return m;
                }
                else
                    return null;
            }
        }


        public static List<Medical> GetAllPrisonerMedicals(int id)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();
                List<Medical> MedicalsList = new List<Medical>();

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    DataTable sqlPrisoners = new DataTable();
                    SqlDataAdapter Data = new SqlDataAdapter("Select HRID from [HealthRecord] where PID = " + id, ServerConnection);
                    Data.Fill(sqlPrisoners);

                    foreach (DataRow row in sqlPrisoners.Rows)
                    {
                        Medical tmedical = GetMedical((int)row["HRID"]);
                        if (tmedical != null)
                            MedicalsList.Add(tmedical);
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

                return MedicalsList;
            }
        }


        public static bool InsertMedical(Medical medical)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();

                //calling procedure from db
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "InsertHealthRecord";
                    cmd.Connection = ServerConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //passing parameters to procedure
                    cmd.Parameters.Add(new SqlParameter("@sym", medical.Symptoms));
                    cmd.Parameters.Add(new SqlParameter("@dig", medical.Diagnosis));
                    cmd.Parameters.Add(new SqlParameter("@pd", medical.Prisoner.PrisonerID));

                    //passing output para
                    cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.Int));
                    cmd.Parameters["@flag"].Direction = ParameterDirection.Output;

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

                int Flag = (int)cmd.Parameters["@flag"].Value;

                return Flag == 1;
            }
        }


        public static bool UpdateMedical(Medical medical)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();

                //calling procedure from db
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "UpdateHealthRecord";
                    cmd.Connection = ServerConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //passing parameters to procedure
                    cmd.Parameters.Add(new SqlParameter("@hrid", medical.MedicalID));
                    cmd.Parameters.Add(new SqlParameter("@sym", medical.Symptoms));
                    cmd.Parameters.Add(new SqlParameter("@dig", medical.Diagnosis));

                    //passing output para
                    cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.Int));
                    cmd.Parameters["@flag"].Direction = ParameterDirection.Output;

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

                int Flag = (int)cmd.Parameters["@flag"].Value;

                return Flag == 1;
            }
        }


        public static bool DeleteMedical(int id)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();

                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "DeleteHealthRecord";
                    cmd.Connection = ServerConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //passing parameters to procedure
                    cmd.Parameters.Add(new SqlParameter("@hrid", id));

                    //passing output para
                    cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.Int));
                    cmd.Parameters["@flag"].Direction = ParameterDirection.Output;

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

                int Flag = (int)cmd.Parameters["@flag"].Value;

                return Flag == 1;
            }
        }


    }
}