using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Web;

/// <summary>
/// Summary description for UserControl
/// </summary>
public class UserControl
{
   public List<Userrs> GetAllUsers()
   {
        List<Userrs> L = new List<Userrs>();
        Userrs User = null;
        SqlDataReader rdr = Provider.DataAccessConcrete().GetAllUsers();
        while (rdr.Read()) {
            User = new Userrs();
            User.ID = long.Parse(rdr["ID"].ToString());
            User.Name = rdr["Name"].ToString();
            User.Status = rdr[ "Status"].ToString();
            User.Email = rdr["Email"].ToString();
            User.ContextID = rdr["ContextID"].ToString();
            User.LastConnected = DateTime.Parse(rdr["LastConnected"].ToString());
            L.Add(User);
        }
        return L;
   }

    public Userrs GetUserByID(long ID)
    {
        Userrs User = new Userrs();
        SqlDataReader rdr = Provider.DataAccessConcrete().GetUserByID(ID);
        if (rdr.Read())
        {
            User = new Userrs();
            User.ID = long.Parse(rdr["ID"].ToString());
            User.Name = rdr["Name"].ToString();
            User.Status = rdr["Status"].ToString();
            User.Email = rdr["Email"].ToString();
            User.ContextID = rdr["ContextID"].ToString();
            User.LastConnected = DateTime.Parse(rdr["LastConnected"].ToString());
        }

        return User;
    }

    public List<Userrs> GetAllFriends(long ID)
    {
        List<Userrs> L = new List<Userrs>();
        Userrs User = null;
        SqlDataReader rdr = Provider.DataAccessConcrete().GetAllFriends(ID);
        while (rdr.Read())
        {
            User = new Userrs();
            User.ID = long.Parse(rdr["ID"].ToString());
            User.Name = rdr["Name"].ToString();
            User.Status = rdr["Status"].ToString();
            User.Email = rdr["Email"].ToString();
            User.ContextID = rdr["ContextID"].ToString();
            User.LastConnected = DateTime.Parse(rdr["LastConnected"].ToString());
            L.Add(User);
        }
        return L;
    }

    public void UpdateUserContext(Userrs user)
    {
         Provider.DataAccessConcrete().UpdateUserContext(user.ID,user.ContextID);
    }

    public Userrs GetUserByEmail(string Email)
    {
        Userrs User = new Userrs();
        SqlDataReader rdr = Provider.DataAccessConcrete().GetUserByEmail(Email);
        if(rdr.Read())
        {
            User = new Userrs();
            User.ID = long.Parse(rdr["ID"].ToString());
            User.Name = rdr["Name"].ToString();
            User.Status = rdr["Status"].ToString();
            User.Email = rdr["Email"].ToString();
            User.ContextID = rdr["ContextID"].ToString();
            User.LastConnected = DateTime.Parse(rdr["LastConnected"].ToString());
        }
        
        return User;
    }

    public void UpdateUser(Userrs user)
    {
        //throw new NotImplementedException();
    }
}