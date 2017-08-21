<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default_Login"
    ValidateRequest="false" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmenu" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Gulali HRIS - Forgot Password</title>
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
            <%--Div Reset password--%>
            <div class="m-t-40 card-box" id="div_reset" runat="server" visible="true">
                <div class="text-center">
                    <h4 class="text-uppercase font-bold m-b-0">Forgot Password</h4>

                    <p class="text-muted m-b-0 font-13 m-t-20">Enter your Username and we'll send you an email with instructions to change your password.  </p>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="col-xs-12">
                            <asp:TextBox ID="username" runat="server" CssClass="form-control" placeholder="Enter Username" autocomplete="off" />
                        </div>
                    </div>

                    <div class="form-group text-center m-t-20 m-b-0">
                        <div class="col-xs-6" style="padding-top: 15px;">
                            <asp:Button ID="Backlogin" runat="server" Style="background-color: #ff2828; border-color: #ff2828; color: white; padding: 6px 12px; text-align: center; border-radius: 4px; width: 100%" Text="Batal" name="submit" OnClick="Back_Click" AccessKey="s" />
                        </div>
                        <div class="col-xs-6" style="padding-top: 15px;">
                            <asp:Button ID="SignIn" runat="server" Style="background-color: #71b6f9; border-color: #71b6f9; color: white; padding: 6px 12px; text-align: center; border-radius: 4px; width: 100%" Text="Send Email" name="submit" OnClick="BtnSendEmail" AccessKey="s" />
                        </div>
                    </div>
                </div>
            </div>

            <%--Div Notiv Email Send--%>
            <div class="m-t-40 card-box" id="div_notif_email" runat="server" visible="false">
                <div class="text-center">
                    <h4 class="text-uppercase font-bold m-b-0">Confirm Email</h4>
                </div>
                <div class="panel-body text-center">
                    <img src="/assets/images/mail_confirm.png" alt="img" class="thumb-lg m-t-20 center-block" />
                    <p class="text-muted font-13 m-t-20">
                        A email has been send to <b>
                            <asp:Literal ID="label_email" runat="server" /></b>. Please check for an email from company and click on the included link to change your password.
                    </p>
                </div>
            </div>

            <%--Div Forgot Password Link --%>
            <div class="m-t-40 card-box" id="div_forget_link" runat="server" visible="false">
                <div class="text-center">
                    <h4 class="text-uppercase font-bold m-b-0">Forgot Password</h4>
                </div>
                <div class="panel-body">
                    <form class="form-horizontal m-t-20" action="index.html">
                        <div class="form-group ">
                            <div class="col-xs-12">
                                <asp:TextBox ID="username_link" runat="server" CssClass="form-control" placeholder="Username" Enabled="false" />
                            </div>
                        </div>
                        <br>
                        <div class="form-group" style="padding-top: 15px;">
                            <div class="col-xs-12">
                                <asp:TextBox ID="new_password" runat="server" TextMode="Password" CssClass="form-control" placeholder="New Password" />
                            </div>
                        </div>
                        <br>
                        <div class="form-group" style="padding-top: 15px;">
                            <div class="col-xs-12">
                                <asp:TextBox ID="confirm_new_password" runat="server" TextMode="Password" CssClass="form-control" placeholder="Confirm New Password" />
                            </div>
                            <div class="col-xs-12" style="padding-top: 10px; text-align: center;">
                                <asp:Literal ID="note" runat="server" />
                            </div>
                        </div>

                        <div class="form-group text-center m-t-40">
                            <div class="col-xs-12" style="padding-top: 10px;">
                                <asp:Button ID="btnreset" runat="server" CssClass="btn btn-custom btn-bordred btn-block waves-effect waves-light" Text="Reset Password" OnClick="BtnReset" AccessKey="s" />
                            </div>
                        </div>
                    </form>
                </div>
            </div>
            <asp:Literal ID="alert" runat="server" />

            <div class="row">
                <div class="col-sm-12 text-center">
                    <p class="text-muted">Already have account?<a href="/gulali/sign-up/" class="text-primary m-l-5"><b>Sign In</b></a></p>
                </div>
            </div>

        </div>
        <!-- end wrapper page -->
        <script>
            var resizefunc = [];
        </script>
    </form>
</body>
</html>
