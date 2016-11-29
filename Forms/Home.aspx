<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="Forms_Home" %>

<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:Label ID="Label3" runat="server" Text="Status Date: "></asp:Label>
        <ww:jQueryDatePicker runat="server" ID="statusDate" Width='80px' DisplayMode="ImageButton"
            DateFormat="dd/MM/yyyy" AutoPostBack="true" OnTextChanged="Page_Load"></ww:jQueryDatePicker>
    </div>
    <asp:Label ID="Label1" runat="server" Text="Number of Inward Items on PBM: "></asp:Label>
    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
    <div>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1">
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:AP20120527ConnectionString2 %>" 
            SelectCommand="SELECT [Type], [Group], [Value] FROM [AbstractIdGeneratorTable]">
        </asp:SqlDataSource>
    </div>
    <%--<div align="center">
        <table width="100%">
            <tr align="center">
                <td valign="middle">
                    <asp:Panel BackColor="BlueViolet" Height="200px" runat="server" ID="MainPanel" Width="100%"
                        HorizontalAlign="Center">
                        <table align="center" style="height: 100px;">
                            <tr style="border-bottom: solid 1px black;">
                                <td colspan="3" align="center" valign="top" style="font-size: x-large; color: #668014;">
                                    <img src="../images/24cheque.jpg" style="height: 30px; vertical-align: top;" />INCHEQUES
                                    Links
                                </td>
                            </tr>
                            <caption>
                                <br />
                                <tr>
                                    <td>
                                        <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="~/images/outward cheque.jpg"
                                            Height="10px" Width="10px">INCHEQS-OCS</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/images/cheque inward.jpg"
                                            Height="10px" Width="10px">INCHEQS-ICS</asp:HyperLink>
                                    </td>
                                </tr>
                            </caption>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <cc1:RoundedCornersExtender Corners="All" Radius="20" TargetControlID="MainPanel"
            ID="RoundedCornersExtender1" runat="server">
        </cc1:RoundedCornersExtender>
    </div>--%>
</asp:Content>
