<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="LinkPage.aspx.cs" Inherits="Forms_Home" %>

<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <div id="grDiv" runat="server" style="height: 130px; width: 200px; background-color:#7E946C;">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                OnRowDataBound="GridView1_RowDataBound" GridLines="None" RowStyle-VerticalAlign="NotSet">
                <Columns>
                <asp:ImageField DataImageUrlField="imageAddress"></asp:ImageField>
                    <asp:ButtonField CommandName="web_link" ButtonType="Link" HeaderText="LINKS of BACH"
                        DataTextField="DisplayName" HeaderStyle-ForeColor="Black" ItemStyle-ForeColor="#800000" HeaderStyle-Font-Size="15" ItemStyle-Font-Size="12">
                        <HeaderStyle ForeColor="Black" Font-Bold="true"></HeaderStyle>
                        <ItemStyle ForeColor="Maroon" Font-Bold="true" HorizontalAlign="Left"></ItemStyle>
                    </asp:ButtonField>
                    <asp:BoundField DataField="URLAddress" Visible="false" />
                </Columns>
            </asp:GridView>
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BMSConnectionString %>"
            
            SelectCommand="SELECT DisplayName, URLAddress, imageAddress FROM tblLink WHERE (URLStatus = @URLStatus) ORDER BY LinkID">
            <SelectParameters>
                <asp:Parameter DefaultValue="Live" Name="URLStatus" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <cc1:RoundedCornersExtender Corners="All" Radius="35" TargetControlID="grDiv"
            ID="RoundedCornersExtender1" runat="server">
        </cc1:RoundedCornersExtender>
    </center>
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
