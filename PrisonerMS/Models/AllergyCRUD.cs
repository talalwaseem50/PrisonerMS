using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PrisonerMS.Models
{
    public class AllergyCRUD
    {
        static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PMS"].ConnectionString;
        //Others

        public static Allergy GetAllergy(int id)
        {
            using (SqlConnection Server = new SqlConnection(ConnectionString))
            {
                Server.Open();
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "GetPrisonerAllergy";
                    cmd.Connection = Server;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //input para
                    cmd.Parameters.Add(new SqlParameter("@paid", id));

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
                    Allergy a = new Allergy();
                    a.AllergyID = id;
                    a.Desc = (string)cmd.Parameters["@des"].Value;
                    a.EntryDate = ((DateTime)cmd.Parameters["@datee"].Value).ToString("dd/MM/yyyy");
                    a.EntryTime = ((DateTime)cmd.Parameters["@datee"].Value).ToString("HH:mm:ss");
                    a.Medical = new Medical();
                    a.Medical.MedicalID = Convert.ToInt32(cmd.Parameters["@HR"].Value);
                    return a;
                }
                else
                    return null;
            }
        }


        public static List<Allergy> GetAllMedicalAllergys(int id)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();
                List<Allergy> AllergysList = new List<Allergy>();

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    DataTable sqlPrisoners = new DataTable();
                    SqlDataAdapter Data = new SqlDataAdapter("Select PAID from [PrisonerAllergy] where HRID = " + id, ServerConnection);
                    Data.Fill(sqlPrisoners);

                    foreach (DataRow row in sqlPrisoners.Rows)
                    {
                        Allergy tallergy = GetAllergy((int)row["PAID"]);
                        if (tallergy != null)
                            AllergysList.Add(tallergy);
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

                return AllergysList;
            }
        }


        public static bool InsertAllergy(Allergy allergy)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();

                //calling procedure from db
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "InsertPrisonerAllergy";
                    cmd.Connection = ServerConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //passing parameters to procedure
                    cmd.Parameters.Add(new SqlParameter("@des",allergy.Desc));
                    cmd.Parameters.Add(new SqlParameter("@HR", allergy.Medical.MedicalID));

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


        public static bool UpdateAllergy(Allergy allergy)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();

                //calling procedure from db
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "UpdatePrisonerAllergy";
                    cmd.Connection = ServerConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //passing parameters to procedure
                    cmd.Parameters.Add(new SqlParameter("@paid", allergy.AllergyID));
                    cmd.Parameters.Add(new SqlParameter("@des", allergy.Desc));

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


        public static bool DeleteAllergy(int id)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();

                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "DeletePrisonerAllergy";
                    cmd.Connection = ServerConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //passing parameters to procedure
                    cmd.Parameters.Add(new SqlParameter("@paid", id));

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