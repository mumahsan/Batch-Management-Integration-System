<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="WebLOgIn" Title="Change Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="Div_Main">
        <asp:UpdatePanel ID="Chng_Pass" runat="server">
            <ContentTemplate>
                <div id="FirstDiv" class="Div_First">
                    <asp:Panel ID="Panel_First" runat="server" class="Panel_first">
                        <div class="Div_Header">
                            Change Your Password
                        </div>
                        <table class="Table_">
                            <tr>
                                <td class="Table_TD_Data_Left">
                                    &nbsp;
                                </td>
                                <td class="Table_TD_Data_Right">
                                    <asp:Label ID="Lbl_Message" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Table_TD_Data_Left">
                                    User Name :
                                </td>
                                <td class="Table_TD_Data_Right">
                                    <asp:Label ID="lblEmpName" runat="server" Width="350px" ForeColor="#004000"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Table_TD_Data_Left">
                                    User Id :
                                </td>
                                <td class="Table_TD_Data_Right">
                                    <asp:Label ID="lblUserName" runat="server" Width="350px" ForeColor="#004000"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Table_TD_Data_Left">
                                    Enter Old Password :
                                </td>
                                <td class="Table_TD_Data_Right">
                                    <asp:TextBox ID="txtOldPass" runat="server" TextMode="Password" MaxLength="20" Style="width: 160px;
                                        height: 15px;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter old password"
                                        ControlToValidate="txtOldPass"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="Table_TD_Data_Left">
                                    Enter New Password :
                                </td>
                                <td class="Table_TD_Data_Right">
                                    <asp:TextBox ID="txtNewPass" runat="server" TextMode="Password" MaxLength="20" Style="width: 160px;
                                        height: 15px;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter new password"
                                        ControlToValidate="txtNewPass"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="Table_TD_Data_Left">
                                    Retype New Password :
                                </td>
                                <td class="Table_TD_Data_Right">
                                    <asp:TextBox ID="txtRePass" runat="server" TextMode="Password" MaxLength="20" Style="width: 160px;
                                        height: 15px;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please retype new password"
                                        ControlToValidate="txtRePass"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="Table_TD_Data_Left">
                                    &nbsp;
                                </td>
                                <td class="Table_TD_Data_Right">
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Your new password entries did not match"
                                        ControlToCompare="txtNewPass" ControlToValidate="txtRePass"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="Table_TD_Data_Left">
                                    &nbsp;
                                </td>
                                <td class="Table_TD_Data_Right">
                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CausesValidation="true"
                                        class="Button_" />
                                    <asp:Button ID="Btn_Cancel" runat="server" Text="Cancel" CausesValidation="false"
                                        class="Button_" OnClick="Btn_Cancel_Click" />
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
                    </asp:Panel>
                </div>
                <div id="SecondDiv" class="Div_Second">
                    <asp:Panel ID="Panel_Success" runat="server" class="Panel_Success_">
                        <div class="Div_Success_Message">
                            Your Password changed successfully.
                            <br />
                            <br />
                            <asp:Button ID="Btn_home" runat="server" Text="Home" CausesValidation="false" OnClick="Btn_home_Click"
                                class="Button_" />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
