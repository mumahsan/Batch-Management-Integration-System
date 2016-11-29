using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Globalization;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Web.Services;
using System.Threading;
using System.Text.RegularExpressions;



[System.Web.Script.Services.ScriptService]

public partial class NewUser : System.Web.UI.Page
{
    //DbHandler dbh = new DbHandler();
    //Define global Connection String
    string strConnection = ConfigurationManager.ConnectionStrings["BMSConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

        //if (!Session["BranchCode"].ToString().Contains("0914"))
        //{
        //    Response.Redirect("Home.aspx");
        //}


        //if (!Page.IsPostBack)
        //{

        //}

        Lbl_Message.Text = "";

    }

    [WebMethod]
    protected void Btn_Cancel_Click(object sender, EventArgs e)
    {
        resetPage();
        Btn_Save.Text = "Save";
    }

    private void resetPage()
    {
        TxtBox_UserId.Enabled = true;
        TxtBox_UserId.Text = "";
        TxtBox_Password.Attributes.Add("value", "");
        TxtBox_RetypePass.Attributes.Add("value", "");
        TxtBox_EmpName.Text = "";
        Btn_Save.Text = "Save";
        DropDownList_Active.SelectedIndex = 0;
        SqlDataSource_UserDetails.SelectCommand = "SELECT UserInfo.UserId, UserInfo.EmployeeName, DesignationInfo.Designation, UserTypeInfo.UserType, " +
               "UserInfo.Enabled, BranchInfo.BranchName FROM DesignationInfo INNER JOIN UserInfo ON DesignationInfo.DesignationId = " +
               "UserInfo.DesignationId INNER JOIN UserTypeInfo ON UserInfo.UserTypeId = UserTypeInfo.UserTypeId INNER JOIN BranchInfo ON UserInfo.BranchCode = " +
               "BranchInfo.BranchCode WHERE (BranchInfo.BrId = @BrId)";
        //SqlDataSource_UserDetails.SelectCommand = "SELECT UserInfo.UserId, UserInfo.EmployeeName, DesignationInfo.Designation, UserTypeInfo.UserType, UserInfo.Enabled, BranchInfo.BranchName, UserInfo.RoutNo FROM DesignationInfo INNER JOIN UserInfo ON DesignationInfo.DesignationId = UserInfo.DesignationId INNER JOIN UserTypeInfo ON UserInfo.UserTypeId = UserTypeInfo.UserTypeId INNER JOIN BranchInfo ON UserInfo.RoutNo = BranchInfo.RoutingNo AND UserInfo.BranchCode = BranchInfo.BranchCode WHERE (UserInfo.RoutNo = @RoutNo)";
        //SqlDataSource_UserDetails.SelectCommand = "SELECT UserInfo.UserId, UserInfo.EmployeeName, DesignationInfo.Designation, UserTypeInfo.UserType, UserInfo.Enabled, BranchInfo.BranchName, UserInfo.RoutNo FROM DesignationInfo INNER JOIN UserInfo ON DesignationInfo.DesignationId = UserInfo.DesignationId INNER JOIN UserTypeInfo ON UserInfo.UserTypeId = UserTypeInfo.UserTypeId INNER JOIN BranchInfo ON UserInfo.RoutNo = BranchInfo.RoutingNo WHERE (UserInfo.RoutNo = @RoutNo)";
        GridView_NewUser.DataBind();
    }
    
    [WebMethod]
    protected void Btn_Receive_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        SqlConnection MySqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["BMSConnectionString"].ToString());
        SqlCommand command = MySqlConnection.CreateCommand();


        try
        {
            MySqlConnection.Open();

            if (TxtBox_UserId.Enabled)
            {
                command.CommandText = "EXEC dbo.AddNewUser '" + TxtBox_UserId.Text.Trim() + "', '" +
                    TxtBox_Password.Text.Trim() + "', " +
                    DropDownList_UserType.SelectedValue.ToString() + ", '" +
                    TxtBox_EmpName.Text.Trim() + "', " +
                    DropDownList_Designation.SelectedValue.ToString() + ", '" +
                    DropDownList_Active.SelectedValue.ToString() + "', '" +
                    Session["user"].ToString() + "', '" + DateTime.Now + "', '" + NB_BranchCode.Text
                    //DropDownList_Br.SelectedValue.ToString() 
                    + "',"+int.Parse(DropDownList_Br.SelectedValue.ToString())+"";

                command.ExecuteNonQuery();

                Lbl_Message.ForeColor = System.Drawing.Color.Green;
                Lbl_Message.Text = "User Id <b>" + TxtBox_UserId.Text.Trim() + "</b> created successfully.";
            }
            else
            {
                command.CommandText = "EXEC dbo.UpdateUser '" + TxtBox_UserId.Text.Trim() + "', '" + TxtBox_Password.Text.Trim() + "', " +
                DropDownList_UserType.SelectedValue.ToString() + ", '" + TxtBox_EmpName.Text.Trim() + "', " +
                DropDownList_Designation.SelectedValue.ToString() + ", '" + DropDownList_Br.SelectedValue.ToString() + "', '" +
                DropDownList_Active.SelectedValue.ToString() + "', '" +
                Session["user"].ToString() + "', '" + DateTime.Now + "'";
                //+ "','"+
                //DropDownList_Br.SelectedValue.ToString()+"'";

                command.ExecuteNonQuery();


                Lbl_Message.ForeColor = System.Drawing.Color.Green;
                Lbl_Message.Text = "User Id <b>" + TxtBox_UserId.Text.Trim() + "</b> updated successfully.";
            }
            resetPage();
        }
        catch (Exception Ex)
        {
            if (Ex.Message.Contains("duplicate"))
            {
                command.CommandText = "EXEC getUserBranchName '" + TxtBox_UserId.Text + "'";
                Lbl_Message.ForeColor = System.Drawing.Color.Red;
                Lbl_Message.Text = "User exists in " + (String)command.ExecuteScalar();
            }
            else
            {
                Lbl_Message.ForeColor = System.Drawing.Color.Red;
                Lbl_Message.Text = "Error : " + Ex.Message;
            }
        }
        finally
        {
            MySqlConnection.Close();
        }
    }
    
    [WebMethod]
    protected void GridView_NewUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("ins_"))
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow gvRow = GridView_NewUser.Rows[index];
            DataTable dTable = new DataTable();
            //dbh.GetDataTable("Exec UserInfo4Edit '"+gvRow.Cells[1].Text+"'");
            using (SqlConnection conn = new SqlConnection(strConnection))
            using (SqlCommand cmd = new SqlCommand("UserInfo4Edit", conn))
            {
                try
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = gvRow.Cells[1].Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dTable);
                    //int noOfRowsAffected = cmd.ExecuteNonQuery();
                    //if (noOfRowsAffected > 0)
                    //    Lbl_Message.Text = "Executed Sucessfully";
                }
                catch (Exception eX) { Lbl_Message.Text = "Error on: " + eX.Message; }
                finally { if (conn.State != ConnectionState.Closed) { conn.Close(); } }
            }

            TxtBox_UserId.Text = gvRow.Cells[1].Text;
            TxtBox_Password.Attributes.Add("value", dTable.Rows[0]["Password"].ToString());
            TxtBox_RetypePass.Attributes.Add("value", dTable.Rows[0]["Password"].ToString());
            TxtBox_EmpName.Text = dTable.Rows[0]["EmployeeName"].ToString();
            DropDownList_Br.SelectedIndex = DropDownList_Br.Items.IndexOf(DropDownList_Br.Items.FindByText(dTable.Rows[0]["BranchName"].ToString()));
            DropDownList_Designation.SelectedIndex = DropDownList_Designation.Items.IndexOf(DropDownList_Designation.Items.FindByText(dTable.Rows[0]["Designation"].ToString()));
            DropDownList_UserType.SelectedIndex = DropDownList_UserType.Items.IndexOf(DropDownList_UserType.Items.FindByText(dTable.Rows[0]["UserType"].ToString()));
            DropDownList_Active.SelectedIndex = DropDownList_Active.Items.IndexOf(DropDownList_Active.Items.FindByText(dTable.Rows[0]["Enabled"].ToString()));

            TxtBox_UserId.Enabled = false;
            Btn_Save.Text = "Update";
        }
    }
    
    [WebMethod]
    protected void DropDownList_Br_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        using(SqlConnection conn=new SqlConnection(strConnection))
        using (SqlCommand cmd = new SqlCommand("SELECT RoutingNo FROM BranchInfo WHERE (BrId = " + DropDownList_Br.SelectedValue.ToString() + ")", conn))
        {
            try
            {
                conn.Close();
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if(dt.Rows.Count>0)
                    NB_BranchCode.Text = dt.Rows[0][0].ToString();
            }
            catch(Exception eX)
            {
                Lbl_Message.Text = "Error on : "+eX.Message;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
        if (!TxtBox_UserId.Enabled)
        {
            resetPage();
        }
        else
        {
            SqlDataSource_UserDetails.SelectCommand = "SELECT UserInfo.UserId, UserInfo.EmployeeName, DesignationInfo.Designation, UserTypeInfo.UserType, " +
               "UserInfo.Enabled, BranchInfo.BranchName FROM DesignationInfo INNER JOIN UserInfo ON DesignationInfo.DesignationId = " +
               "UserInfo.DesignationId INNER JOIN UserTypeInfo ON UserInfo.UserTypeId = UserTypeInfo.UserTypeId INNER JOIN BranchInfo ON UserInfo.BranchCode = " +
               "BranchInfo.BranchCode WHERE (BranchInfo.BrId = @BrId)";
            //SqlDataSource_UserDetails.SelectCommand = "SELECT UserInfo.UserId, UserInfo.EmployeeName, DesignationInfo.Designation, UserTypeInfo.UserType, UserInfo.Enabled, BranchInfo.BranchName FROM DesignationInfo INNER JOIN UserInfo ON DesignationInfo.DesignationId = UserInfo.DesignationId INNER JOIN UserTypeInfo ON UserInfo.UserTypeId = UserTypeInfo.UserTypeId INNER JOIN BranchInfo ON UserInfo.BranchCode = BranchInfo.BranchCode WHERE (UserInfo.RoutNo = @RoutNo)";
            //SqlDataSource_UserDetails.SelectCommand = "SELECT UserInfo.UserId, UserInfo.EmployeeName, DesignationInfo.Designation, UserTypeInfo.UserType, UserInfo.Enabled, BranchInfo.BranchName FROM DesignationInfo INNER JOIN UserInfo ON DesignationInfo.DesignationId = UserInfo.DesignationId INNER JOIN UserTypeInfo ON UserInfo.UserTypeId = UserTypeInfo.UserTypeId INNER JOIN BranchInfo ON UserInfo.BranchCode = BranchInfo.BranchCode WHERE (BranchInfo.BranchCode = @BranchCode)";
            GridView_NewUser.DataBind();
        }
    }
    protected void DropDownList_Br_DataBound(object sender, EventArgs e)
    {
        NB_BranchCode.Text = DropDownList_Br.SelectedValue.ToString();
        SqlDataSource_UserDetails.SelectCommand = "SELECT UserInfo.UserId, UserInfo.EmployeeName, DesignationInfo.Designation, UserTypeInfo.UserType, UserInfo.Enabled, BranchInfo.BranchName, UserInfo.RoutNo FROM DesignationInfo INNER JOIN UserInfo ON DesignationInfo.DesignationId = UserInfo.DesignationId INNER JOIN UserTypeInfo ON UserInfo.UserTypeId = UserTypeInfo.UserTypeId INNER JOIN BranchInfo ON UserInfo.RoutNo = BranchInfo.RoutingNo WHERE (BranchInfo.BrId = @BrId)";
        GridView_NewUser.DataBind();
    }
    protected void Btn_Serach_Click(object sender, EventArgs e)
    {
        DropDownList_Br.SelectedValue = NB_BranchCode.Text.PadLeft(4, '0');
    }
}
