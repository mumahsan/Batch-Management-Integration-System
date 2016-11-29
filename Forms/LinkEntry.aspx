<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="LinkEntry.aspx.cs" Inherits="Forms_LinkEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function DeleteConfirmation() {
            var result = confirm('Are you sure you want to delete selected File(s)?');
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }
        function Check_Click(objRef) {
            //Get the Row based on checkbox  
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //If checked change color to Aqua  
                row.style.backgroundColor = "aqua";
            }
            else {
                //If not checked change back to original color  
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color  
                    row.style.backgroundColor = "#C2D69B";
                }
                else {
                    row.style.backgroundColor = "white";
                }
            }
            //Get the reference of GridView  
            var GridView = row.parentNode;
            //Get all input elements in Gridview  
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox  
                var headerCheckBox = inputList[0];
                //Based on all or none checkboxes  
                //are checked check/uncheck Header Checkbox  
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        break;
                    }
                }
            }
            //headerCheckBox.checked = checked;  
        }
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex  
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked  
                        //check all checkboxes  
                        //and highlight all rows  
                        row.style.backgroundColor = "aqua";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked  
                        //uncheck all checkboxes  
                        //and change rowcolor back to original   
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color  
                            //row.style.backgroundColor = "#C2D69B";  
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }  
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        Enter Your Link Here</h3>
    <table>
        <tr>
            <td>
                Link Address:
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" class="textBoxExtraLarge"></asp:TextBox>
                <%--<input id="Text1" class="textBoxExtraLarge" runat="server" type="text" />--%>
            </td>
        </tr>
        <tr>
            <td>
                Display Name:
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" class="textBoxExtraLarge"></asp:TextBox>
                <%--<input id="Text2" class="textBoxExtraLarge" runat="server" type="text" />--%>
            </td>
        </tr>
        <tr>
            <td>
                Link Status:
            </td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Selected="True" Text="Live" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Save" OnClick="Button1_Click" />
                <asp:Button ID="Button2" runat="server" Text="Cancel" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="LinkID"
            DataSourceID="SqlDataSource1" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:BoundField DataField="LinkID" HeaderText="LinkID" ReadOnly="True" SortExpression="LinkID" />
                <asp:BoundField DataField="URLAddress" HeaderText="URLAddress" SortExpression="URLAddress" />
                <asp:BoundField DataField="DisplayName" HeaderText="DisplayName" SortExpression="DisplayName" />
                <asp:BoundField DataField="URLStatus" HeaderText="URLStatus" SortExpression="URLStatus" />
                <asp:BoundField DataField="EntryBy" HeaderText="EntryBy" SortExpression="EntryBy" />
                <asp:BoundField DataField="EntryDt" HeaderText="EntryDt" DataFormatString="{0:d}"
                    SortExpression="EntryDt" />
                <asp:ButtonField CommandName="Edit_" Text="Edit" HeaderText="Edit">
                    <%--<ItemStyle BorderColor="Black" />
                    <HeaderStyle BorderColor="Black" BorderWidth="1px" />--%>
                </asp:ButtonField>
                <asp:TemplateField HeaderText="Select">
                    <HeaderTemplate>
                        <asp:CheckBox ID="checkAll" runat="server" Text="Select All" onclick="checkAll(this);" />
                        <%--<asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged"/>--%>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" Text="Select" onclick="Check_Click(this);" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:BMSConnectionString %>" 
            SelectCommand="SELECT * FROM [tblLink] WHERE ([URLStatus] = @URLStatus) ORDER BY [LinkID]">
            <SelectParameters>
                <asp:Parameter DefaultValue="Live" Name="URLStatus" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    <div>
        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClientClick="return DeleteConfirmation();"
            OnClick="btnDelete_Click" /></div>
</asp:Content>
