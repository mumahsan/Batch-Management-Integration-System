<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <link href="App_Themes/ThemeLogin/Style.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/ThemeLogin/Control.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table id="login" width="250" border="0" cellpadding="1" cellspacing="1" bgcolor="#C5E7F2">
        <tr>
            <td align="center">
                <b><font size="4" color="#E79256">Login Required</font></b>
            </td>
        </tr>
        <tr>
            <td bgcolor="#FFFFFF">
                <table border="0" cellspacing="10" cellpadding="0" width="100%">
                    <tr>
                        <td colspan="2" id="LL">
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Username
                        </td>
                        <td align="left" style="width: 50%;">
                            <asp:TextBox ID="txtUserName" runat="server" Style="border: 1px solid #348017; width: 70%;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Password
                        </td>
                        <td align="left" style="width: 90%;">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Style="border: 1px solid #348017;
                                width: 70%;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" Style="border: 1px solid #348017;
                                color: #348017;" OnClick="btnLogin_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Style="border: 1px solid #348017;
                                color: #348017;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr align="center">
            <td>
                <b>NBL BACH Management System</b>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
