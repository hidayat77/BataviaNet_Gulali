<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default_Login" ValidateRequest="false" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmenu" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Gulali HRIS - Login Page</title>
    <ucmenu:meta ID="meta" runat="server" />
</head>
<body class="loginpage">
    <form id="login" runat="server" onsubmit="return LoginFormValidate()">
        <div class="account-pages"></div>
        <div class="clearfix"></div>
        <div class="wrapper-page">
            <div class="text-center">
                <a href="#" class="logo">
                    <asp:Literal ID="logo" runat="server"></asp:Literal>
                </a>
            </div>
            <div class="m-t-40 card-box">
                <div class="text-center">
                    <h4 class="text-uppercase font-bold m-b-0">Sign In</h4>
                </div>
                <div class="panel-body">
                    <form class="form-horizontal m-t-20">
                        <div class="form-group ">
                            <div class="col-xs-12">
                                <asp:TextBox ID="username" runat="server" CssClass="form-control" placeholder="Username" autocomplete="off" />
                            </div>
                        </div>
                        <br>
                        <div class="form-group" style="padding-top: 15px;">
                            <div class="col-xs-12">
                                <asp:TextBox ID="password" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password" />
                            </div>
                        </div>

                        <div class="form-group text-center m-t-30">
                            <div class="col-xs-12" style="padding-top: 15px;">
                                <asp:Button ID="SignIn" runat="server" Style="background-color: #71b6f9; border-color: #71b6f9; color: white; padding: 6px 12px; text-align: center; border-radius: 4px; width: 100%"
                                    Text="Sign In" name="submit" OnClick="BtnSignIn" AccessKey="s" />
                            </div>
                        </div>
                        <div class="form-group m-t-30 m-b-0">
                            <div class="col-sm-12" style="padding-top: 15px;">
                                <a href="/gulali/forgot-password/" class="text-muted"><i class="fa fa-lock m-r-5"></i>Forgot your password?</a>
                            </div>
                        </div>
                    </form>

                </div>
            </div>
            <!-- end card-box-->
        </div>
        <!-- end wrapper page -->
        <script>
            var resizefunc = [];
        </script>
    </form>
</body>
</html>
