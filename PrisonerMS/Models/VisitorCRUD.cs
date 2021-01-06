using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PrisonerMS.Models
{
    public class VisitorCRUD
    {
        static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PMS"].ConnectionString;
        //Others

        public static Visitor GetVisitor(int id)
        {
            using (SqlConnection Server = new SqlConnection(ConnectionString))
            {
                Server.Open();
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "GetVisitor";
                    cmd.Connection = Server;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //input para
                    cmd.Parameters.Add(new SqlParameter("@vid", id));

                    //output parameters
                    cmd.Parameters.Add(new SqlParameter("@FName", SqlDbType.VarChar, 30));
                    cmd.Parameters["@FName"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@LName", SqlDbType.VarChar, 30));
                    cmd.Parameters["@LName"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@gender", SqlDbType.VarChar, 1));
                    cmd.Parameters["@gender"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@cnic", SqlDbType.Char, 11));
                    cmd.Parameters["@cnic"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@vaddress", SqlDbType.VarChar, 100));
                    cmd.Parameters["@vaddress"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@relation", SqlDbType.VarChar, 20));
                    cmd.Parameters["@relation"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@dateV", SqlDbType.DateTime));
                    cmd.Parameters["@dateV"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@pid", SqlDbType.Int));
                    cmd.Parameters["@pid"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@pno", SqlDbType.VarChar, 30));
                    cmd.Parameters["@pno"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@pfname", SqlDbType.VarChar, 30));
                    cmd.Parameters["@pfname"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@plname", SqlDbType.VarChar, 30));
                    cmd.Parameters["@plname"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@prid", SqlDbType.Int));
                    cmd.Parameters["@prid"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@prnum", SqlDbType.VarChar, 30));
                    cmd.Parameters["@prnum"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@prname", SqlDbType.VarChar, 100));
                    cmd.Parameters["@prname"].Direction = ParameterDirection.Output;
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
                    Visitor v1 = new Visitor();
                    v1.VisitorRecordID = id;
                    v1.FirstName = (string)cmd.Parameters["@FName"].Value;
                    v1.LastName = (string)cmd.Parameters["@LName"].Value;
                    v1.Gender = (string)cmd.Parameters["@gender"].Value;
                    v1.CNIC = (string)cmd.Parameters["@cnic"].Value;
                    v1.Address = (string)cmd.Parameters["@vaddress"].Value;
                    v1.Relation = (string)cmd.Parameters["@relation"].Value;
                    v1.VisitDate = ((DateTime)cmd.Parameters["@dateV"].Value).ToString("dd/MM/yyyy");
                    v1.VisitTime = ((DateTime)cmd.Parameters["@dateV"].Value).ToString("HH:mm:ss");
                    v1.Prisoner = new Prisoner();
                    v1.Prisoner.PrisonerID = Convert.ToInt32(cmd.Parameters["@pid"].Value);
                    v1.Prisoner.PrisonerNo = (string)cmd.Parameters["@pno"].Value;
                    v1.Prisoner.FirstName = (string)cmd.Parameters["@pfname"].Value;
                    v1.Prisoner.LastName = (string)cmd.Parameters["@plname"].Value;
                    v1.Prison = new Prison();
                    v1.Prison.PrisonID = (int)cmd.Parameters["@prid"].Value;
                    v1.Prison.PNumber = (string)cmd.Parameters["@prnum"].Value; ;
                    v1.Prison.Name = (string)cmd.Parameters["@prname"].Value; ;
                    return v1;
                }
                else
                    return null;
            }
        }


        public static List<Visitor> GetAllPrisonVisitors(int id)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();
                List<Visitor> VisitorsList = new List<Visitor>();

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    DataTable sqlPrisoners = new DataTable();
                    SqlDataAdapter Data = new SqlDataAdapter("Select VID From [Visitor] where PrisonID = " + id + " Order by Date_Visit desc", ServerConnection);
                    Data.Fill(sqlPrisoners);

                    foreach (DataRow row in sqlPrisoners.Rows)
                    {
                        Visitor tvisitor = GetVisitor((int)row["VID"]);
                        if (tvisitor != null)
                            VisitorsList.Add(tvisitor);
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

                return VisitorsList;
            }
        }


        public static bool InsertVisitor(Visitor visitor)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();

                //calling procedure from db
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "InsertVisitor";
                    cmd.Connection = ServerConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //passing parameters to procedure
                    cmd.Parameters.Add(new SqlParameter("@FName", visitor.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LName", visitor.LastName));
                    cmd.Parameters.Add(new SqlParameter("@gender", visitor.Gender));
                    cmd.Parameters.Add(new SqlParameter("@cnic", visitor.CNIC));
                    cmd.Parameters.Add(new SqlParameter("@vaddress", visitor.Address));
                    cmd.Parameters.Add(new SqlParameter("@relation", visitor.Relation));
                    cmd.Parameters.Add(new SqlParameter("@pid", visitor.Prisoner.PrisonerID));
                    cmd.Parameters.Add(new SqlParameter("@prid", visitor.Prison.PrisonID));
                    
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


        public static bool UpdateVisitor(Visitor visitor)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();

                //calling procedure from db
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "UpdateVisitor";
                    cmd.Connection = ServerConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //passing parameters to procedure
                    cmd.Parameters.Add(new SqlParameter("@vid", visitor.VisitorRecordID));
                    cmd.Parameters.Add(new SqlParameter("@FName", visitor.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LName", visitor.LastName));
                    cmd.Parameters.Add(new SqlParameter("@gender", visitor.Gender));
                    cmd.Parameters.Add(new SqlParameter("@cnic", visitor.CNIC));
                    cmd.Parameters.Add(new SqlParameter("@vaddress", visitor.Address));
                    cmd.Parameters.Add(new SqlParameter("@relation", visitor.Relation));

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


        public static bool DeleteVisitor(int id)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();

                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "DeleteVisitor";
                    cmd.Connection = ServerConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //passing parameters to procedure
                    cmd.Parameters.Add(new SqlParameter("@vid", id));

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