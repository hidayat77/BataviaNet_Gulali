<%@ Page Language="C#" AutoEventWireup="true" CodeFile="list_employee.aspx.cs" Inherits="list" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding-top:5px; padding-bottom:10px;">
        <asp:TextBox placeholder="Search Department, Division, Title, Employee" ID="text_search" runat="server" AutoPostBack="True" Width="45%" OnTextChanged="btn_search_Click"/>
    </div>
    <div>       
        <table cellspacing="0" cellpadding="3" border="1" id="tb2" style="border-collapse: collapse;">
            <tr>
                <td scope="col" style="width: 250px; background-color: #B4B8B8;" align="center">
                    Department    
                </td>
                <td scope="col" style="width: 250px; background-color: #B4B8B8;" align="center">
                    Division    
                </td>
                <td scope="col" style="width: 250px; background-color: #B4B8B8;" align="center">
                    Title
                </td>
                <td scope="col" style="width: 250px; background-color: #B4B8B8;" align="center">
                    Name
                </td>
                <td scope="col" style="width: 40px; background-color: #B4B8B8;" align="center">
                    Action
                </td>
            </tr>
            <asp:Label ID="list2" runat="server" />
        </table>
    </div>
    </form>
    <script type="text/javascript">
        function confirmAdd(x) {
		
            
            var answer = confirm('Contact Added!');
            
            if (parent.opener.document.getElementById("txt_multiple_employee").value.length > 0)
                parent.opener.document.getElementById("txt_multiple_employee").value += ',';

            if (x.length > 0)
                parent.opener.document.getElementById("txt_multiple_employee").value += x;
		}
    </script>
</body>
</html>
