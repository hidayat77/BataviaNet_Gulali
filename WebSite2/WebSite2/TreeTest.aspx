
<%@ Page Title="Tree Test" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="TreeTest.aspx.cs" Inherits="TreeTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    <br />
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    <asp:Literal ID="LitTest" runat="server"></asp:Literal>

    <asp:TreeView ID="TreeView1" runat="server">
    </asp:TreeView>

</asp:Content>
