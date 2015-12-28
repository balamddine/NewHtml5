using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Users
/// </summary>
public class Messages
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
    private string vMessage;
    public string Message
    {
        get
        {
            return vMessage;
        }
        set
        {
            vMessage = value;
        }
    }
    private long vFromID;
    public long FromID
    {
        get
        {
            return vFromID;
        }
        set
        {
            vFromID = value;
        }
    }
    private long vToID;
    public long ToID
    {
        get
        {
            return vToID;
        }
        set
        {
            vToID = value;
        }
    }
    private DateTime vDateSend;
    public DateTime DateSend
    {
        get
        {
            return vDateSend;
        }
        set
        {
            vDateSend = value;
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