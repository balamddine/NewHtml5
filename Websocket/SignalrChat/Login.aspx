<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="css/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="container">

            <h3>Welcome to Signal R chat example</h3>
            <div class="panel panel-default">

                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-offset-4">
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label for="txtUserName">Email * </label>
                                    <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server" placeholder="Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfd" runat="server" Text="!" ControlToValidate="txtUserName" ValidationGroup="groupLogin"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label for="txtUserName">Password * </label>
                                    <asp:TextBox ID="TxtPassword" CssClass="form-control" TextMode="Password" runat="server" placeholder="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="!" ControlToValidate="TxtPassword" ValidationGroup="groupLogin"></asp:RequiredFieldValidator>
                                </div>
                                <div style="float:right">
                                    <asp:Button CssClass="btn btn-primary" Text="LogIn" ID="btnLogin" runat="server" OnClick="btnLogin_Click" ValidationGroup="groupLogin" />
                                </div>
                                <asp:Label ID="lbl_Error" runat="server" ForeColor="Red" > </asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <script src="js/jquery-1.10.1.min.js"></script>
    </form>
</body>
</html>
