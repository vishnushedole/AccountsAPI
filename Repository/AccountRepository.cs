using AccountsWebAPI.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Xml;

namespace AccountsWebAPI.Repository
{
    public class AccountRepository:BaseDataAccess
    {
        public Account GetAccountById(int id)
        {
            var sql = "sp_showDetails";
            Account account = null;
            try
            {
                var reader = ExecuteSql(
                    sqltext: sql,
                    commandType: CommandType.StoredProcedure,
                    parameters: new SqlParameter("@accid", id)
                    );
                  while(reader.Read())
                {
                    account = new Account(
                        reader.GetInt32(0),
                          reader.GetInt32(1),
                            reader.GetInt32(2),
                            reader.GetDecimal(3),
                              reader.GetInt32(4),
                                reader.GetInt32(5),
                                  reader.GetInt32(6),
                                  reader.GetInt32(7),
                                
                                  reader.GetInt32(8)
                        );
                }
                if (reader.IsClosed) reader.Close();
            }
            catch(SqlException sqle)
            {
                throw;
            }
            catch(Exception e)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return account;
        }

        public void UpdatePassbook(int id)
        {
            var sql = "sp_ApplyForCheckBook";
            OpenConnection();
            try
            {
                ExecuteNonQuery(
                    sqltext: sql,
                    commandType: CommandType.StoredProcedure,
                    parameters: new SqlParameter("@accid", id));
            }
            catch (SqlException sqle)
            {
               
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void DeleteAccount(int id)
        {
            var sql = "sp_CloseAccount";
            OpenConnection();
            try
            {
                ExecuteNonQuery(
                    sqltext: sql,
                    commandType: CommandType.StoredProcedure,
                    parameters: new SqlParameter("@accid", id));
            }
            catch (SqlException sqle)
            {

                throw;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        public List<Account> ListAll(int id)
        {
            List<Account> list=new List<Account>();
            var sql = "sp_ListAccount";
            try
            {
                var reader = ExecuteSql(
                    sqltext: sql,
                    commandType: CommandType.StoredProcedure,
                    parameters: new SqlParameter("@custId", id)
                    );
                while (reader.Read())
                {
                   Account account = new Account(
                        reader.GetInt32(0),
                          reader.GetInt32(1),
                            reader.GetInt32(2),
                             reader.GetDecimal(3),
                              reader.GetInt32(4),
                                reader.GetInt32(5),
                                  reader.GetInt32(6),
                                  reader.GetInt32(7),
                                 
                                  reader.GetInt32(8)
                        );
                    list.Add(account);
                }
                if (reader.IsClosed) reader.Close();
            }
            catch (SqlException sqle)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return list;
        }
        public List<Transactions> ListAllTransaction(int accid)
        {
            List<Transactions> list = new List<Transactions>();
           
            var sql = "sp_TransactionList";
            try
            {
                var reader = ExecuteSql(
                    sqltext: sql,
                    commandType: CommandType.StoredProcedure,
                    parameters: new SqlParameter("@accId", accid)
                    );
                while (reader.Read())
                {
                    Transactions transaction = new Transactions(
                      reader.GetInt32(0),
                           reader.GetInt32(1),
                             reader.GetInt32(2),
                             reader.GetDecimal(3),
                             reader.GetDateTime(4)
                        ); 
                    
                    list.Add(transaction);
                }
                if (reader.IsClosed) reader.Close();
            }
            catch (SqlException sqle)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return list;
        }
        public List<int> ListBenifitiary(int accid)
        {
            List<int> list=new List<int>();
            var sql = "sp_beneficiaryList";
            try
            {
                var reader = ExecuteSql(
                    sqltext: sql,
                    commandType: CommandType.StoredProcedure,
                    parameters: new SqlParameter("@accId", accid)
                    );
                while (reader.Read())
                {
                   
                    list.Add(reader.GetInt32(0));
                }
                if (reader.IsClosed) reader.Close();
            }
            catch (SqlException sqle)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return list;
        }
        public bool CheckBenificiary(int accId, int BaccId)
        {
            string sql = "sp_CheckBenifitiaryId";
            bool flag = false;
            try
            {
                OpenConnection();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sql;

                command.Parameters.Add(new SqlParameter("@accId", accId));
                command.Parameters.Add(new SqlParameter("@BaccId", BaccId));

                SqlParameter outputIdParam = new SqlParameter("@return", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputIdParam);

                command.ExecuteNonQuery();
                connection.Close();

                int check = Convert.ToInt32(outputIdParam.Value);
                if (check == 1) flag = true;
            }
            catch (SqlException sqle)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return flag;
        }
        public bool CheckTransactionAllowOrNot(int id,decimal amount)
        {
            var sql = "sp_checkTransAllowOrNot";
            bool flag = false;
            try
            {
              
                OpenConnection();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sql;

                command.Parameters.Add(new SqlParameter("@accid", id));
                command.Parameters.Add(new SqlParameter("@amount", amount));

                SqlParameter outputIdParam = new SqlParameter("@result", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputIdParam);

                command.ExecuteNonQuery();
                connection.Close();

                int check = Convert.ToInt32(outputIdParam.Value);
                 if (check == 1) flag = true;

            }
            catch (SqlException sqle)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return flag;

        }
        public bool BankTransfer(int accId,int benificiaryId,decimal amount)
        {
            var sql = "sp_BankTransfer";
            bool flag = false;
            try
            {
                OpenConnection();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sql;
              
                command.Parameters.Add(new SqlParameter("@accid", accId));
                command.Parameters.Add(new SqlParameter("@beneficiaryID", benificiaryId));
                command.Parameters.Add(new SqlParameter("@amount", amount));
                
                SqlParameter outputIdParam = new SqlParameter("@result", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputIdParam);

                command.ExecuteNonQuery();
                connection.Close();

                int check = Convert.ToInt32(outputIdParam.Value);
                if (check == 1) flag = true;

            }
            catch (SqlException sqle)
            {

                throw;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return flag;
        }

    }
   
}
