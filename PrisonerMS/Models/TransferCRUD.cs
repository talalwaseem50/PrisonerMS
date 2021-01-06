using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PrisonerMS.Models
{
    public class TransferCRUD
    {
        static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PMS"].ConnectionString;
        //Others

        public static Transfer GetTransfer(int id)
        {
            using (SqlConnection Server = new SqlConnection(ConnectionString))
            {
                Server.Open();
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "GetTransfer";
                    cmd.Connection = Server;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //input para
                    cmd.Parameters.Add(new SqlParameter("@tid", id));

                    //output parameters
                    cmd.Parameters.Add(new SqlParameter("@transferT", SqlDbType.VarChar, 20));
                    cmd.Parameters["@transferT"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@typeN", SqlDbType.VarChar, 10));
                    cmd.Parameters["@typeN"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@transferS", SqlDbType.VarChar, 20));
                    cmd.Parameters["@transferS"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@des", SqlDbType.VarChar, 100));
                    cmd.Parameters["@des"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@datee", SqlDbType.DateTime));
                    cmd.Parameters["@datee"].Direction = ParameterDirection.Output;
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
                    Transfer t = new Transfer();
                    t.TransferID = id;
                    t.Type = (string)cmd.Parameters["@transferT"].Value;
                    t.TypeNumber = (string)cmd.Parameters["@typeN"].Value;
                    t.Status = (string)cmd.Parameters["@transferS"].Value;
                    t.Description = (string)cmd.Parameters["@des"].Value;
                    t.EntryDate = ((DateTime)cmd.Parameters["@datee"].Value).ToString("dd/MM/yyyy");
                    t.EntryTime = ((DateTime)cmd.Parameters["@datee"].Value).ToString("HH:mm:ss");
                    t.Prisoner = new Prisoner();
                    t.Prisoner.PrisonerID = Convert.ToInt32(cmd.Parameters["@pid"].Value);
                    t.Prisoner.PrisonerNo = (string)cmd.Parameters["@pno"].Value;
                    t.Prisoner.FirstName = (string)cmd.Parameters["@pfname"].Value;
                    t.Prisoner.LastName = (string)cmd.Parameters["@plname"].Value;
                    t.Prison = new Prison();
                    t.Prison.PrisonID = (int)cmd.Parameters["@prid"].Value;
                    t.Prison.PNumber = (string)cmd.Parameters["@prnum"].Value; ;
                    t.Prison.Name = (string)cmd.Parameters["@prname"].Value; ;
                    return t;
                }
                else
                    return null;
            }
        }


        public static List<Transfer> GetAllPrisonTransfers(int id)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();
                List<Transfer> TransfersList = new List<Transfer>();

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    DataTable sqlPrisoners = new DataTable();
                    SqlDataAdapter Data = new SqlDataAdapter("Select TID From [Transfer] where PrisonID = " + id + " OR (TypeNumber = " + GetPrisonNumber(id) + " AND TransferStatus = 'Complete') Order by Date_Entry desc", ServerConnection);
                    Data.Fill(sqlPrisoners);

                    foreach (DataRow row in sqlPrisoners.Rows)
                    {
                        Transfer transfer = GetTransfer((int)row["TID"]);
                        if (transfer != null)
                            TransfersList.Add(transfer);
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

                return TransfersList;
            }
        }


        public static String GetPrisonNumber(int id)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();
                String num = null;

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    DataTable sqlPrisoners = new DataTable();
                    SqlDataAdapter Data = new SqlDataAdapter("Select PNumber From [Prison] where PrisonID = " + id, ServerConnection);
                    Data.Fill(sqlPrisoners);

                    foreach (DataRow row in sqlPrisoners.Rows)
                    {
                        num = (string)row["PNumber"];
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

                return num;
            }
        }


        public static List<Transfer> GetDashBoardTransfers(int id)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();
                List<Transfer> TransfersList = new List<Transfer>();

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    DataTable sqlPrisoners = new DataTable();
                    SqlDataAdapter Data = new SqlDataAdapter("Select TID From [Transfer] where (PrisonID = " + id + " And TransferStatus = 'Approved') OR (TransferStatus = 'CheckOut' AND TypeNumber = " + GetPrisonNumber(id) + ") OR (TransferStatus = 'CheckOut' AND TypeNumber = '-' AND PrisonID = " + id + ") OR (PrisonID = " + id + " And TransferStatus = 'Approved Jailer') Order by Date_Entry desc", ServerConnection);

                    Data.Fill(sqlPrisoners);

                    foreach (DataRow row in sqlPrisoners.Rows)
                    {
                        Transfer transfer = GetTransfer((int)row["TID"]);
                        if (transfer != null)
                            TransfersList.Add(transfer);
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

                return TransfersList;
            }
        }


        public static List<Transfer> GetIncomingTransfers(int id)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();
                List<Transfer> TransfersList = new List<Transfer>();

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    DataTable sqlPrisoners = new DataTable();
                    SqlDataAdapter Data = new SqlDataAdapter("Select TID From [Transfer] where TransferStatus = 'Approved Jailer' AND TypeNumber = " + GetPrisonNumber(id) + " Order by Date_Entry desc", ServerConnection);

                    Data.Fill(sqlPrisoners);

                    foreach (DataRow row in sqlPrisoners.Rows)
                    {
                        Transfer transfer = GetTransfer((int)row["TID"]);
                        if (transfer != null)
                            TransfersList.Add(transfer);
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

                return TransfersList;
            }
        }


        public static List<Transfer> GetAdminTransfers(int id)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();
                List<Transfer> TransfersList = new List<Transfer>();

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    DataTable sqlPrisoners = new DataTable();
                    SqlDataAdapter Data = new SqlDataAdapter("Select TID From [Transfer] where PrisonID = " + id + " And TransferStatus = 'InProgress' Order by Date_Entry desc", ServerConnection);

                    Data.Fill(sqlPrisoners);

                    foreach (DataRow row in sqlPrisoners.Rows)
                    {
                        Transfer transfer = GetTransfer((int)row["TID"]);
                        if (transfer != null)
                            TransfersList.Add(transfer);
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

                return TransfersList;
            }
        }


        public static bool InsertTransfer(Transfer transfer)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();

                //calling procedure from db
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "InsertTransfer";
                    cmd.Connection = ServerConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //passing parameters to procedure
                    cmd.Parameters.Add(new SqlParameter("@transferT", transfer.Type));
                    cmd.Parameters.Add(new SqlParameter("@typeN", transfer.TypeNumber));
                    cmd.Parameters.Add(new SqlParameter("@transferS", transfer.Status));
                    cmd.Parameters.Add(new SqlParameter("@des", transfer.Description));
                    cmd.Parameters.Add(new SqlParameter("@pid", transfer.Prisoner.PrisonerID));
                    cmd.Parameters.Add(new SqlParameter("@prid", transfer.Prison.PrisonID));

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


        public static bool UpdateTransfer(Transfer transfer)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();

                //calling procedure from db
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "UpdateTransfer";
                    cmd.Connection = ServerConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //passing parameters to procedure
                    cmd.Parameters.Add(new SqlParameter("@tid", transfer.TransferID));
                    cmd.Parameters.Add(new SqlParameter("@des", transfer.Description));

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


        public static bool UpdateStatusTransfer(Transfer transfer)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();

                //calling procedure from db
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "UpdateStatusTransfer";
                    cmd.Connection = ServerConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //passing parameters to procedure
                    cmd.Parameters.Add(new SqlParameter("@tid", transfer.TransferID));
                    cmd.Parameters.Add(new SqlParameter("@transferS", transfer.Status));

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

        public static bool DeleteTransfer(int id)
        {
            using (SqlConnection ServerConnection = new SqlConnection(ConnectionString))
            {
                ServerConnection.Open();

                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "DeleteTransfer";
                    cmd.Connection = ServerConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //passing parameters to procedure
                    cmd.Parameters.Add(new SqlParameter("@tid", id));

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