using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Linq;

namespace SignalRChat
{
    public class ChatHub : Hub
    {
        private static List<Userrs> _UsersL = new List<Userrs>();
        public static List<Userrs> UsersL { get { return _UsersL; } set { _UsersL = value; } }
        public UserControl UsrCtrl = new UserControl();
        public MessagesControl msgctrl = new MessagesControl();

        public void addUser(long UserId)
        {
            Userrs Usr = GetUserInList(UserId);
            if (Usr == null)
            {
                Usr = UsrCtrl.GetUserByID(UserId);
                Usr.ContextID = Context.ConnectionId;
                UsersL.Add(Usr);
            }
            else // remove and add as new 
            {
                UsersL.Remove(Usr);
                Usr = UsrCtrl.GetUserByID(UserId);
                UsersL.Add(Usr);
            }
        }

        public Userrs GetUserInList(long UserId)
        {
            List<Userrs> UsrL = (from U in UsersL
                         where U.ID == UserId
                         select U).ToList<Userrs>();
            return UsrL.Count > 0 ? UsrL[0] : null;
        }
        public void sendMessage(long FromID, long ToID, string message)
        {

            
            Messages msg = new Messages();
            msg.DateSend = DateTime.Now;
            msg.FromID = FromID;
            msg.ToID = ToID;
            msg.Message = message;
            msg.Status = "A";
            msgctrl.AddMessage(msg);
            List<string> ConnectionIDsL = new List<string>();
            Userrs ToUser = UsrCtrl.GetUserByID(ToID);
            Userrs FUsr = GetUserInList(FromID);
            Userrs TUsr = GetUserInList(ToID);
            if (FUsr != null)
                ConnectionIDsL.Add(FUsr.ContextID);
            if(TUsr!=null)
                ConnectionIDsL.Add(TUsr.ContextID);

            Clients.Clients(ConnectionIDsL).ClientMessageReceived(message,FUsr.ID,FUsr.Name,ToUser.ID, ToUser.Name);
        }
        public void logout(long UserID) {
            Userrs Usr = UsrCtrl.GetUserByID(UserID);
            UsersL.Remove(Usr);
        }
    }
}