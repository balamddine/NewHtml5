using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DataAccess
/// </summary>
public class DataAccess
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["conn"].ToString();

    #region Users

    public SqlDataReader GetAllUsers()
    {
        string query = "select * from Users";
        SqlConnection con = new SqlConnection(ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand(query);
        SqlDataReader rdr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        return rdr;
    }
    public SqlDataReader GetUserByEmail(string Email)
    {
        string query = "select * from Users where Email=@Email";
        SqlConnection con = new SqlConnection(ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand(query,con);
        cmd.Parameters.Add(new SqlParameter("@Email",Email));
        SqlDataReader rdr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
      
        return rdr;
    }

    public SqlDataReader GetUserByID(long ID)
    {
        string query = "select * from Users where ID=@ID";
        SqlConnection con = new SqlConnection(ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.Add(new SqlParameter("@ID", ID));
        SqlDataReader rdr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

        return rdr;
    }

    public SqlDataReader GetAllFriends(long ID)
    {
        string query = "select U.* from Friends F inner join Users U on F.FriendID = U.ID where F.ID = @ID";
        SqlConnection con = new SqlConnection(ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.Add(new SqlParameter("@ID", ID));
        SqlDataReader rdr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
       
        return rdr;
    }

    public void UpdateUserContext(long ID,string ContextID)
    {
        string query = "update users set ContextID=@ContextID , LastConnected=@LastConnected where ID=@ID";
        SqlConnection con = new SqlConnection(ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.Add(new SqlParameter("@ContextID", ContextID));
        cmd.Parameters.Add(new SqlParameter("@LastConnected", DateTime.Now));
        cmd.Parameters.Add(new SqlParameter("@ID", ID));
        cmd.ExecuteReader();
        con.Close();
    }

    #endregion



}