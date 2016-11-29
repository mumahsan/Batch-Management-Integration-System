<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataDisplay.aspx.cs" Inherits="_DataDisplay" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Untitled Page</title>
    <link href="App_Themes/ThemeLogin/Control.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/ThemeLogin/Style.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/ThemeLogin/ddsmoothmenu.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/MyStyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/ThemePBMINC/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=ResolveUrl("~/js/jquery-1.3.2.min.js")%>"></script>

    <link href="sdmenu/sdmenu.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=ResolveUrl("~/sdmenu/sdmenu.js")%>"></script>

    <script type="text/javascript">
        // <![CDATA[
        //        var myMenu;
        //        window.onload = function() {
        //            myMenu = new SDMenu("my_menu");
        //            myMenu.init();
        //        };
        function openNewWindow(text, text1) {
            if (confirm("Do you want to continue to: " + text1) == true) {
                window.open(text, 'name', 'width=auto,height=auto,status=yes,toolbar=yes,menubar=yes,location=yes,scrollbars=yes,resizable=yes,titlebar=yes');
                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <%--<asp:ContentPlaceHolder ID="head" runat="server">
    </asp:ContentPlaceHolder>--%>
</head>
<body>
    <form id="form1" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div id="div_main">
        <div id="wrapper">
            <div style="font-size: small;">
                <%--<div style="float: left; padding-right: 2%;">
                    Welcome To The Page
                </div>--%>
                <div style="float: left;">
                </div>
                <%--<div style="float: right;">
                <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="~/images/imagebot.jpg"
                    Text="Exit">HyperLink</asp:HyperLink>
            </div>--%>
            </div>
            <div id="header">
                <h1>
                    NATIONAL BANK LIMITED</h1>
                <h2>
                    BACH Management System</h2>
            </div>
            <!-- end #header -->
        </div>
        <!-- end #wrapper -->
        <div style="padding-left: 200px">
        </div>
        <!-- end #my_menu -->
        <div>
            <div id="content">
                <div>
                    <asp:Label ID="Label1" runat="server" Text="Status Date: "></asp:Label>
                    <ww:jQueryDatePicker runat="server" ID="statusDate" Width='80px' DisplayMode="ImageButton"
                        DateFormat="dd/MM/yyyy" AutoPostBack="true" OnTextChanged="Page_Load"></ww:jQueryDatePicker>
                </div>
                <div style="width: 49%; float: left;">
                    <div style="text-align: center;">
                        <asp:Label ID="Label5" runat="server" Text="PBM Info" Font-Size="10"></asp:Label>
                    </div>
                    <%--<div>
                    <div id="header_PBM">::.Inward</div>
                    </div>--%>
                    <div>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Worksource" HeaderText="Worksource" SortExpression="Worksource" />
                                <asp:BoundField DataField="Description" HeaderText="WorksourceDesc" SortExpression="WorksourceDesc" />
                                <asp:BoundField DataField="Item" HeaderText="Items" SortExpression="Items" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" DataFormatString="{0:########,###.00}" ItemStyle-HorizontalAlign="Right" /><%--DataFormatString="{0:c}"/>--%>
                            </Columns>
                        </asp:GridView>
                        <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:AP20120527ConnectionString %>" 
                            SelectCommand="SELECT Worksource, (CASE WHEN Worksource = '110' THEN 'Outward Regular' WHEN Worksource = '115' THEN 'Outward High Value' WHEN Worksource = '120' THEN 'Outward Returns Regular' WHEN Worksource = '125' THEN 'Outward Returns High Value' WHEN Worksource = '130' THEN 'Inward Regular' WHEN Worksource = '135' THEN 'Inward High Value' WHEN Worksource = '140' THEN 'Inward Returns Regular' WHEN Worksource = '145' THEN 'Inward Returns High Value' WHEN Worksource = '160' THEN 'EFT Outward' WHEN Worksource = '170' THEN 'EFT Outward Returns' WHEN Worksource = '180' THEN 'EFT Inward' WHEN Worksource = '190' THEN 'EFT Inward Returns' ELSE CAST(Worksource AS VARCHAR) END) AS WorksourceDesc, (CASE WHEN COUNT(Amount) IS NULL THEN 0 ELSE COUNT(Amount) END) AS Items, (CASE WHEN (SUM(Amount) * .01) IS NULL THEN 0 ELSE (SUM(Amount) * .01) END) AS Amount FROM ItemLatest GROUP BY Worksource HAVING (Worksource &lt; 160) ORDER BY Worksource">
                        </asp:SqlDataSource>--%>
                    </div>
                    <%--<asp:Label ID="Label4" runat="server" Text="Inward High Value on PBM: "></asp:Label>
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>--%>
                </div>
                <div style="float: right; width: 49%;">
                    <div style="text-align: center;">
                        <asp:Label ID="Label3" runat="server" Text="Incheqs Info" Font-Size="20"></asp:Label>
                    </div>
                </div>
                   <div>
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false">
                        <Columns>
                                <asp:BoundField DataField="Description" HeaderText="WorksourceDesc" SortExpression="WorksourceDesc" />
                                <asp:BoundField DataField="Item" HeaderText="Items" SortExpression="Items" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" DataFormatString="{0:########,###.00}" ItemStyle-HorizontalAlign="Right" />
                        </Columns>
                        </asp:GridView>
                    </div>
            </div>
            <!-- end #content -->
        </div>
    </div>
    </form>
</body>
</html>
