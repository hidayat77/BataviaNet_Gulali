<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPassword.aspx.cs" Inherits="_rpassword"
    ValidateRequest="false" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmenu" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Gulali HRIS - Reset Password</title>
    <ucmenu:meta ID="meta" runat="server" />
</head>
<body class="loginpage">
    <form id="login" runat="server" onsubmit="return LoginFormValidate()">
        <div class="account-pages"></div>
        <div class="clearfix"></div>
        <div class="wrapper-page">
            <div class="text-center">
                <a href="index.html" class="logo"><span>Admin<span>to</span></span></a>
                <h5 class="text-muted m-t-0 font-600">Responsive Admin Dashboard</h5>
            </div>
            <div class="m-t-40 card-box">
                <div class="text-center">
                    <h4 class="text-uppercase font-bold m-b-0">Reset Password</h4>
                </div>
                <div class="panel-body">
                    <div class="form-group ">
                        <div class="col-xs-12">
                            <asp:TextBox ID="username" runat="server" CssClass="form-control" placeholder="Username" Enabled="false" />
                        </div>
                    </div>
                    <br>
                    <div class="form-group" style="padding-top: 15px;">
                        <div class="col-xs-12">
                            <asp:TextBox ID="old_password" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password" />
                            <%--<input class="form-control" type="password" required="" placeholder="Password">--%>
                        </div>
                    </div>
                    <br>
                    <div class="form-group" style="padding-top: 15px;">
                        <div class="col-xs-12">
                            <asp:TextBox ID="new_password" runat="server" TextMode="Password" CssClass="form-control" placeholder="New Password" />
                            <%--<input class="form-control" type="password" required="" placeholder="Password">--%>
                        </div>
                    </div>
                    <br>
                    <div class="form-group" style="padding-top: 15px;">
                        <div class="col-xs-12">
                            <asp:TextBox ID="confirm_new_password" runat="server" TextMode="Password" CssClass="form-control" placeholder="Confirm New Password" />
                            <%--<input class="form-control" type="password" required="" placeholder="Password">--%>
                        </div>
                        <div class="col-xs-12" style="padding-top: 10px; text-align: center;">
                            <asp:Literal ID="note" runat="server" />
                        </div>
                    </div>

                    <div class="form-group text-center m-t-40">
                        <div class="col-xs-6" style="padding-top: 10px;">
                            <asp:Button ID="btnlogout" runat="server" Style="background-color: #ff2828; border-color: #ff2828; color: white; padding: 6px 12px; text-align: center; border-radius: 4px; width: 100%" Text="Log Out" OnClick="logout_Click" AccessKey="s" />
                        </div>
                        <div class="col-xs-6" style="padding-top: 10px;">
                            <asp:Button ID="btnreset" runat="server" Style="background-color: #71b6f9; border-color: #71b6f9; color: white; padding: 6px 12px; text-align: center; border-radius: 4px; width: 100%" Text="Reset Password" OnClick="BtnReset" AccessKey="s" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- end card-box -->
            <script>
                var resizefunc = [];
            </script>
    </form>
</body>
</html>
