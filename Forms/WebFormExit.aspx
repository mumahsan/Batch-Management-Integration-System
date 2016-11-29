<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormExit.aspx.cs" Inherits="WebFormExit" Title="Logout Page" %>--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebFormExit.aspx.cs" Inherits="WebFormExit" Title="LogOut Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NBL EDW System</title>
    <link href="../App_Themes/ThemeLogin/Control.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/ThemeLogin/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table id="login" width="250" border="0" cellpadding="1" cellspacing="1" bgcolor="#348017">
        <tr>
            <td>
                <h4>
                    NBL EDW System</h4>
            </td>
        </tr>
        <tr>
            <td bgcolor="#FFFFFF">
                Thank you for using the system
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="White" NavigateUrl="~/Default.aspx"><h4>Login Agian</h4></asp:HyperLink>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
