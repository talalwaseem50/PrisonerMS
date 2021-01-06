using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PrisonerMS.Models
{
    public class MedicationCRUD
    {
        //static string ConnectionString = "data source=DESKTOP-QGDLCC0; database=PMS; integrated security = SSPI;";
        static string ConnectionString = "Server=tcp:seprojectpmsserver.database.windows.net,1433;Initial Catalog=PMSDB;Persist Security Info=False;User ID=rootpms;Password=rootROOT1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        //Others

        public static Medication GetMedication(int id)
        {
            using (SqlConnection Server = new SqlConnection(ConnectionString))
            {
                Server.Open();
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "GetPrisonerMedication";
                    cmd.Connection = Server;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //input para
                    cmd.Parameters.Add(new SqlParameter("@pmid", id));

                    //output parameters
                    cmd.Parameters.Add(new SqlParameter("@des", SqlDbType.VarChar, 500));
                    cmd.Parameters["@des"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@datee", SqlDbType.DateTime));
                    cmd.Parameters["@datee"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@HR", SqlDbType.Int));
                    cmd.Parameters["@HR"].Direction = ParameterDirection.Output;
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
                    Medication m = new Medication();
                    m.MedicationID = id;
                    m.Desc = (string)cmd.Parameters["@des"].Value;
                    m.EntryDate = ((DateTime)cmd.Parameters["@datee"].Value).ToString("dd/MM/yyyy");
                    m.EntryTime = ((DateTime)cmd.Parameters["@datee"].Value).ToString("HH:mm:ss");
                    m.Medical = new Medical();
                    m.Medical.MedicalID = Convert.ToInt32(cmd.Parameters["@HR"].Value);
                    return m;
                }
                else
                    return null;
            }
        }


        public static List<Medication> GetAllMedicalMedications(int id)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();
                List<Medication> MedicationsList = new List<Medication>();

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    DataTable sqlPrisoners = new DataTable();
                    SqlDataAdapter Data = new SqlDataAdapter("Select PMID from [PrisonerMedication] where HRID = " + id, ServerConnection);
                    Data.Fill(sqlPrisoners);

                    foreach (DataRow row in sqlPrisoners.Rows)
                    {
                        Medication tmedication = GetMedication((int)row["PMID"]);
                        if (tmedication != null)
                            MedicationsList.Add(tmedication);
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

                return MedicationsList;
            }
        }


        public static bool InsertMedication(Medication medication)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();

                //calling procedure from db
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "InsertPrisonerMedication";
                    cmd.Connection = ServerConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //passing parameters to procedure
                    cmd.Parameters.Add(new SqlParameter("@des", medication.Desc));
                    cmd.Parameters.Add(new SqlParameter("@HR", medication.Medical.MedicalID));

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


        public static bool UpdateMedication(Medication medication)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();

                //calling procedure from db
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "UpdatePrisonerMedication";
                    cmd.Connection = ServerConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //passing parameters to procedure
                    cmd.Parameters.Add(new SqlParameter("@pmid", medication.MedicationID));
                    cmd.Parameters.Add(new SqlParameter("@des", medication.Desc));

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


        public static bool DeleteMedication(int id)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();

                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "DeletePrisonerMedication";
                    cmd.Connection = ServerConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //passing parameters to procedure
                    cmd.Parameters.Add(new SqlParameter("@pmid", id));

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