
<%@ Page Title="Excel Test" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="ExcelTest.aspx.cs" Inherits="ExcelTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    <br />
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
</asp:Content>
