using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Linq;

namespace SignalRChat
{
    public class ChatHub : Hub
    {
        public static List<Userrs> UsersL = new List<Userrs>();
        public UserControl UsrCtrl = new UserControl();
        public void sendMessage(long ToID,long FromID,string message)
        {
           
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(message);
        }
        public void addUser(long UserId)
        {
            Userrs Usr = GetUserInList(UserId);
            if (Usr == null) {
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
    }
}