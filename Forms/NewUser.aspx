<%@ page language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" CodeFile="NewUser.aspx.cs" Inherits="NewUser" title="New User" %>

<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <script type="text/javascript" >
    function Confirm4Save()
    {
        if(Page_ClientValidate())
         {
         return confirm('Do you want to continue?');
         }
        else
         {
         return false;
         }
     }     
</script>
  <div>           
    <div class="Div_Header">
          User Info
    </div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>  
          <table class="Table_">       
            <tr>
                <td class="Table_TD_Data_Left">
                  Branch/Div Name : 
                </td>     
                <td class="Table_TD_Data_Right">
                    <asp:DropDownList ID="DropDownList_Br" runat="server" Width="250px" DataSourceID="SqlDataSource_Br" DataTextField="BranchName" DataValueField="BrId" AutoPostBack="True" onselectedindexchanged="DropDownList_Br_SelectedIndexChanged" ondatabound="DropDownList_Br_DataBound" >
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource_Br" runat="server" ConnectionString="<%$ ConnectionStrings:BMSConnectionString %>" 
                        
                        
                        SelectCommand="SELECT BranchName, BranchCode, RoutingNo, BrId FROM BranchInfo">
                    </asp:SqlDataSource>
                </td> 
            </tr>
            <tr>
                <td class="Table_TD_Data_Left">
                    Routing No :
                </td>
                <td class="Table_TD_Data_Right">
                    <ew:NumericBox ID="NB_BranchCode" runat="server" MaxLength="3" CssClass="Numeric_Box_" ValidationGroup="brCode" PlacesBeforeDecimal="3" PositiveNumber="True" TruncateLeadingZeros="True"></ew:NumericBox>
                    <asp:RequiredFieldValidator ID="RFV_BranchCode" runat="server" ErrorMessage="Required !" ValidationGroup="brCode" ControlToValidate="NB_BranchCode" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:LinkButton ID="Btn_Serach" runat="server" Text="Search"  CssClass="LinkBtn" onclick="Btn_Serach_Click" />    
                </td>
            </tr>   
              <tr>
                <td class="Table_TD_Data_Left">
                  Employee Name : 
                </td>     
                <td class="Table_TD_Data_Right">
                    <asp:TextBox ID="TxtBox_EmpName" runat="server" Class="TextBoxLagre_" MaxLength="200" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="Required !" ValidationGroup="NewCircular" 
                        ControlToValidate="TxtBox_EmpName" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </td> 
            </tr> 
            <tr>
                <td class="Table_TD_Data_Left">
                  Designation : 
                </td>     
                <td class="Table_TD_Data_Right">
                    <asp:DropDownList ID="DropDownList_Designation" runat="server" Width="200px" 
                        DataSourceID="SqlDataSource_Designation" DataTextField="Designation" 
                        DataValueField="DesignationId" >
                    </asp:DropDownList>
                   
                    <asp:SqlDataSource ID="SqlDataSource_Designation" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:BMSConnectionString %>" 
                        SelectCommand="SELECT [DesignationId], [Designation] FROM [DesignationInfo] WHERE [DesignationId] &gt;2ORDER BY [DesignationId]">
                    </asp:SqlDataSource>
                   
                </td> 
            </tr>                  
            <tr>
                <td class="Table_TD_Data_Left">
                    User Id :
                </td>
                <td class="Table_TD_Data_Right">
                    <asp:TextBox ID="TxtBox_UserId" runat="server" Class="TextBoxMedium_" 
                     onkeyup="this.value=this.value.toLowerCase();"   MaxLength="20" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_UserId" runat="server" 
                        ErrorMessage="Required !" ValidationGroup="NewUser" 
                        ControlToValidate="TxtBox_UserId" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr> 
            <tr>
                <td class="Table_TD_Data_Left">
                   Password :
                </td>
                <td class="Table_TD_Data_Right">
                    <asp:TextBox ID="TxtBox_Password" runat="server" Class="TextBoxMedium_"
                        TextMode="Password" MaxLength="20" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Pass" runat="server" 
                        ErrorMessage="Required !" ValidationGroup="NewUser" 
                        ControlToValidate="TxtBox_Password" Display="Dynamic"></asp:RequiredFieldValidator>
                       <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                   ErrorMessage="min 6 alphanumerics" ControlToValidate="TxtBox_Password" 
                                   Display="Dynamic" 
                                   ValidationExpression="^.*(?=.{6,})((?=.*[a-z])(?=.*[0-9])|(?=.*[A-Z])(?=.*[0-9])|(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])).*$">
                                   </asp:RegularExpressionValidator>       
                </td>
            </tr> 
            <tr>
                <td class="Table_TD_Data_Left">
                 Retype Password :
                </td>
                <td class="Table_TD_Data_Right">
                    <asp:TextBox ID="TxtBox_RetypePass" runat="server" Class="TextBoxMedium_"
                        TextMode="Password" MaxLength="20" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ErrorMessage="Required !" ValidationGroup="NewUser" 
                        ControlToValidate="TxtBox_RetypePass" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ErrorMessage="Password mismatch" ControlToCompare="TxtBox_Password" 
                        ControlToValidate="TxtBox_RetypePass" Display="Dynamic" SetFocusOnError="True" 
                        ValidationGroup="NewUser"></asp:CompareValidator>
                    
                </td>
            </tr> 
            <tr>
                <td class="Table_TD_Data_Left">
                   User Type :
                </td>
                <td class="Table_TD_Data_Right">
                    <asp:DropDownList ID="DropDownList_UserType" runat="server" Width="200px" DataSourceID="SqlDataSource_UserType" DataTextField="UserType" DataValueField="UserTypeId">
                    </asp:DropDownList> 
                    <asp:SqlDataSource ID="SqlDataSource_UserType" runat="server" ConnectionString="<%$ ConnectionStrings:BMSConnectionString %>" 
                        SelectCommand="SELECT [UserTypeId], [UserType] FROM [UserTypeInfo] WHERE ([UserTypeId] &gt;= @UserTypeId) ORDER BY [UserType] DESC">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserTypeId" SessionField="UserTypeId" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="Table_TD_Data_Left">
                  User Status : 
                </td>     
                <td class="Table_TD_Data_Right">
                    <asp:DropDownList ID="DropDownList_Active" runat="server" Width="200px">
                        <asp:ListItem Value="True">Enable</asp:ListItem>
                        <asp:ListItem Value="False">Disable</asp:ListItem>
                    </asp:DropDownList>
                </td> 
            </tr>     
            <tr>
                <td class="Table_TD_Data_Left">
                   &nbsp;
                </td>
                <td class="Table_TD_Data_Right">
                    <asp:Label ID="Lbl_Message" runat="server" ></asp:Label>
                </td>
            </tr>                                     
            <tr>
                <td class="Table_TD_Data_Left">
                   &nbsp;
                </td>
                <td class="Table_TD_Data_Right">
                     <asp:Button ID="Btn_Save" runat="server" class="Button_" OnClientClick="javascript:return Confirm4Save()" Text="Save" onclick="Btn_Receive_Click" ValidationGroup="NewCircular" Width="100px"/>
                     <asp:Button ID="Btn_Cancel" runat="server" Text="Cancel" CausesValidation="false" class="Button_" onclick="Btn_Cancel_Click" />  
                </td>
            </tr>
            <tr>
                <td class="Table_TD_Data_Left">
                   &nbsp;
                </td>
                <td class="Table_TD_Data_Right">
                     &nbsp;
                </td>
            </tr>
            <tr>
                <td class="Table_TD_Data_Left">
                     &nbsp;
                </td>
                <td class="Table_TD_Data_Right">
                     &nbsp;
                </td>
            </tr>
                    
            </table> 
           <center>
             <asp:GridView ID="GridView_NewUser" runat="server" AutoGenerateColumns="False" BorderStyle="None" CellPadding="4" CellSpacing="2" 
                     ForeColor="Black" AllowSorting="True" Width="700px" onrowcommand="GridView_NewUser_RowCommand" EmptyDataText="No Data" 
                     DataSourceID="SqlDataSource_UserDetails" >
                     <FooterStyle BackColor="#CCCCCC" />
                     <RowStyle BackColor="White" />
                     <EmptyDataRowStyle BackColor="#669999" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Top" />
                     <Columns>
                         <asp:TemplateField>
                            <HeaderStyle Font-Names="Arial" Font-Size="11px" Font-Bold="True" BackColor="Gray" ForeColor="White" HorizontalAlign="Center" 
                                 VerticalAlign="Middle"  BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                             <ItemStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="11px" ForeColor="Black" HorizontalAlign="Left" 
                                 VerticalAlign="Top" BorderColor="Black" />
                            
                            <HeaderTemplate>
                            SL
                            </HeaderTemplate>
                            <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                         </asp:TemplateField>
                         <asp:BoundField DataField="UserId" HeaderText="UserId" 
                             SortExpression="UserId">
                              <HeaderStyle Font-Names="Arial" Font-Size="11px" Font-Bold="True" 
                                 BackColor="Gray" ForeColor="White" HorizontalAlign="Center" 
                                 VerticalAlign="Middle"  BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                             <ItemStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" 
                                 Font-Names="Arial" Font-Size="11px" ForeColor="Black" HorizontalAlign="Left" 
                                 VerticalAlign="Top" BorderColor="Black" />
                         </asp:BoundField>
                         <asp:BoundField DataField="EmployeeName" HeaderText="Name" 
                             SortExpression="EmployeeName">
                            <HeaderStyle Font-Names="Arial" Font-Size="11px" Font-Bold="True" 
                                 BackColor="Gray" BorderColor="Black" BorderStyle="Solid" 
                                 BorderWidth="1px" ForeColor="White" HorizontalAlign="Center" 
                                 VerticalAlign="Middle"  />
                             <ItemStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" 
                                 Font-Names="Arial" Font-Size="11px" ForeColor="Black" HorizontalAlign="Left" 
                                 VerticalAlign="Top" BorderColor="Black"   />
                         </asp:BoundField>
                         <asp:BoundField DataField="Designation" HeaderText="Designation" 
                             SortExpression="Designation">
                            <HeaderStyle Font-Names="Arial" Font-Size="11px" Font-Bold="True" 
                                 BackColor="Gray" BorderColor="Black" BorderStyle="Solid" 
                                 BorderWidth="1px" ForeColor="White" HorizontalAlign="Center" 
                                 VerticalAlign="Middle"  />
                             <ItemStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" 
                                 Font-Names="Arial" Font-Size="11px" ForeColor="Black" HorizontalAlign="Left" 
                                 VerticalAlign="Top" BorderColor="Black"   />
                         </asp:BoundField>
                         <asp:BoundField DataField="UserType" HeaderText="UserType" 
                             SortExpression="UserType">
                            <HeaderStyle Font-Names="Arial" Font-Size="11px" Font-Bold="True" 
                                 BackColor="Gray" BorderColor="Black" BorderStyle="Solid" 
                                 BorderWidth="1px" ForeColor="White" HorizontalAlign="Center" 
                                 VerticalAlign="Middle"  />
                             <ItemStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" 
                                 Font-Names="Arial" Font-Size="11px" ForeColor="Black" HorizontalAlign="Left" 
                                 VerticalAlign="Top" BorderColor="Black"   />
                         </asp:BoundField>
                        <asp:BoundField DataField="BranchName" HeaderText="BranchName" 
                             SortExpression="BranchName">
                              <HeaderStyle Font-Names="Arial" Font-Size="11px" Font-Bold="True" 
                                 BackColor="Gray" BorderColor="Black" BorderStyle="Solid" 
                                 BorderWidth="1px" ForeColor="White" HorizontalAlign="Center" 
                                 VerticalAlign="Middle"  />
                             <ItemStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" 
                                 Font-Names="Arial" Font-Size="11px" ForeColor="Black" HorizontalAlign="Left" 
                                 VerticalAlign="Top" BorderColor="Black"  />
                         </asp:BoundField>
                         <asp:BoundField DataField="Enabled" HeaderText="Enabled" 
                             SortExpression="Enabled">
                              <HeaderStyle Font-Names="Arial" Font-Size="11px" Font-Bold="True" 
                                 BackColor="Gray" BorderColor="Black" BorderStyle="Solid" 
                                 BorderWidth="1px" ForeColor="White" HorizontalAlign="Center" 
                                 VerticalAlign="Middle"  />
                             <ItemStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" 
                                 Font-Names="Arial" Font-Size="11px" ForeColor="Black" HorizontalAlign="Left" 
                                 VerticalAlign="Top" BorderColor="Black"  />
                         </asp:BoundField>
                         <asp:ButtonField CommandName="ins_" HeaderText="Edit" Text="Edit" >
                            <HeaderStyle Font-Names="Arial" Font-Size="11px" Font-Bold="True" BackColor="Gray" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle"  />
                            <ItemStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="11px" ForeColor="Black" HorizontalAlign="Left" VerticalAlign="Top" BorderColor="Black" />
                        </asp:ButtonField>
                     </Columns>
                     <PagerStyle ForeColor="Black" HorizontalAlign="Left" BackColor="#CCCCCC" />
                     <SelectedRowStyle BackColor="#996633" Font-Bold="false" ForeColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="3px"/>
                 </asp:GridView>
             <asp:SqlDataSource ID="SqlDataSource_UserDetails" runat="server" 
                     ConnectionString="<%$ ConnectionStrings:BMSConnectionString %>" 
                   SelectCommand="SELECT UserInfo.UserId, UserInfo.EmployeeName, DesignationInfo.Designation, UserTypeInfo.UserType, UserInfo.Enabled, BranchInfo.BranchName, UserInfo.RoutNo FROM DesignationInfo INNER JOIN UserInfo ON DesignationInfo.DesignationId = UserInfo.DesignationId INNER JOIN UserTypeInfo ON UserInfo.UserTypeId = UserTypeInfo.UserTypeId INNER JOIN BranchInfo ON UserInfo.RoutNo = BranchInfo.RoutingNo AND UserInfo.BranchCode = BranchInfo.BranchCode WHERE (BranchInfo.BrId = @BrId)">
                     <SelectParameters>
                         <asp:ControlParameter ControlID="DropDownList_Br" Name="BrId" 
                             PropertyName="SelectedValue" />
                     </SelectParameters>
                 </asp:SqlDataSource>
     
     
     </center>       
        
     <br /><br />                 
        </ContentTemplate>
    </asp:UpdatePanel>
  </div>   
     
     
</asp:Content>

