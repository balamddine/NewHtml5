<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chat.aspx.cs" Inherits="Chat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chat Page</title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/signalrchat.css" rel="stylesheet" />
    <link href="js/Fancybox/jquery.fancybox.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hfUserId" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hf_contextID" runat="server" ClientIDMode="Static" />
        <nav class="navbar navbar-default">
            <div class="container-fluid">
                <div class="navbar-header">
                    <a class="navbar-brand" href="javascript:void(0);">Signal-R</a><br />
                </div>
                <div>
                    <ul class="nav navbar-nav">
                        <%--<li class="active"><a href="#">Home</a></li>--%>
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li><a href="#dv_info" id="ainfo"><span class="glyphicon glyphicon-user"></span>&nbsp;&nbsp;<asp:Label ID="ltWelcome" runat="server"></asp:Label></a></li>
                        <li><a href="#"><span class="glyphicon glyphicon-log-in"></span>&nbsp;&nbsp;Sign out</a></li>
                    </ul>
                </div>
            </div>
        </nav>

        <div class="Info" id="dv_info" style="display: none">
            <ul>
                <li>Name : 
                    <asp:Literal ID="ltName" runat="server"></asp:Literal>
                </li>
                <li>Email : 
                    <asp:Literal ID="ltEmail" runat="server"></asp:Literal>
                </li>
                <li>Status : 
                    <div id="dvStatus" class="inline"></div>
                </li>
                <li>Context ID : 
                     <div id="dvContextId" class="inline"></div>
                </li>
                <li>Last Connected : 
                    <div id="dvlastConnected" class="inline"></div>
                </li>

            </ul>
        </div>
      
                <div class="col-lg-10 dvchatWrapper" >
                    <div class="row fullHeight" >
                        <div class="col-lg-12 fullHeight" id="dvChatContainer">
                           
                           
                        </div>

                    </div>

                </div>
                <div class="dvfriends col-lg-2">
                    <ul>
                        <asp:Repeater ID="FrdRpt" runat="server" OnItemDataBound="FrdRpt_ItemDataBound">
                            <ItemTemplate>
                                <li runat="server" id="lifriend"><a href="javascript:void(0)">
                                    <asp:Literal ID="litName" runat="server"></asp:Literal></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
          
    </form>
    <script src="js/jquery-1.10.1.min.js"></script>
    <script src="js/Fancybox/jquery.fancybox.js"></script>
    <script src="js/jquery.signalR-2.2.0.min.js"></script>
    <script src="signalr/hubs" type="text/javascript"></script>
    <script src="js/Chat.js"></script>

</body>
</html>
