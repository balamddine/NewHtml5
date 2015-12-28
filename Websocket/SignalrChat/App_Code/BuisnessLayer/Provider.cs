using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Provider
/// </summary>
public class Provider
{
    public static DataAccess DataAccessConcrete()
    {
       return new DataAccess(ConfigurationManager.ConnectionStrings["conn"].ToString());
    }
}