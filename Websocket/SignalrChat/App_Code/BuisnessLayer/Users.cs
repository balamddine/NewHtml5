using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Users
/// </summary>
public class Userrs
{
    private long vID;
    public long ID
    {
        get
        {
            return vID;
        }
        set
        {
            vID = value;
        }
    }
    private string vName;
    public string Name
    {
        get
        {
            return vName;
        }
        set
        {
            vName = value;
        }
    }
    private string vEmail;
    public string Email
    {
        get
        {
            return vEmail;
        }
        set
        {
            vEmail = value;
        }
    }
    private string vContextID;
    public string ContextID
    {
        get
        {
            return vContextID;
        }
        set
        {
            vContextID = value;
        }
    }
    private DateTime vLastConnected;
    public DateTime LastConnected
    {
        get
        {
            return vLastConnected;
        }
        set
        {
            vLastConnected = value;
        }
    }
    private string vStatus;
    public string Status
    {
        get
        {
            return vStatus;
        }
        set
        {
            vStatus = value;
        }
    }
}