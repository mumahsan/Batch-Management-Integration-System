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
using System.Data.SqlClient;

public partial class WebLOgIn : System.Web.UI.Page
{
    //DbHandler db ;
    //Define global Connection String
    string strConnection = ConfigurationManager.ConnectionStrings["BMSConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((String)Session["user"] == null)
        {
            Response.AddHeader("Cache-control", "no-store, must-revalidate, private,no-cache");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            Response.Redirect("LogIn.aspx");
        }
        else
        {
            lblEmpName.Text = (String)Session["EmployeeName"].ToString();
            lblUserName.Text = (String)Session["user"].ToString();
        }

        if (!Page.IsPostBack)
        {
            Panel_First.Visible = true;
            Panel_Success.Visible = false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string oldpass, newpass, repass, query;
        if (!Page.IsValid)
        {
            return;
        }
        DataTable dt = new DataTable();
        //db = new DbHandler();
        string user_ = (String)Session["user"].ToString();
        query = "select Password from UserInfo where UserId='" + user_ + "'";
        using (SqlConnection conn = new SqlConnection(strConnection))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            try
            {
                conn.Open(); cmd.CommandType = CommandType.Text; SqlDataAdapter da = new SqlDataAdapter(cmd); da.Fill(dt);
            }
            catch (Exception eX)
            {
                Lbl_Message.Text = "Error on:" + eX.Message;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }


        if (string.IsNullOrEmpty(txtOldPass.Text))
            Lbl_Message.Text = "Fill the current password field";
        else
        {
            if (dt.Rows.Count > 0)
            {
                oldpass = dt.Rows[0].ItemArray[0].ToString();
                if (oldpass == txtOldPass.Text)
                {

                    if (!(string.IsNullOrEmpty(txtNewPass.Text)) & !(string.IsNullOrEmpty(txtRePass.Text)))
                    {
                        newpass = txtNewPass.Text;
                        repass = txtRePass.Text;
                        if (newpass == repass)
                        {
                            query = "update UserInfo set Password='" + newpass + "' where UserId='" + user_ + "'";
                            using (SqlConnection conn = new SqlConnection(strConnection))
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                try
                                {
                                    conn.Open();
                                    cmd.CommandType = CommandType.Text;
                                    int noOfRowsAffected = cmd.ExecuteNonQuery();
                                    if (noOfRowsAffected > 0)
                                    {
                                        Panel_First.Visible = false;
                                        Panel_Success.Visible = true;
                                    }
                                }
                                catch (Exception eX) { Lbl_Message.Text = "Error on : "+eX.Message; }
                                finally { if (conn.State != ConnectionState.Closed) { conn.Close(); } }
                            }
                            //Lbl_Message.Text = "Cannot change the password!! internal error.";
                            //if (db.ExecuteQuery(query))
                            //{
                            //    Panel_First.Visible = false;
                            //    Panel_Success.Visible = true;
                            //}
                            //else


                        }
                        else
                            Lbl_Message.Text = "Your new password does not match with re-password.";
                    }
                    else
                        Lbl_Message.Text = "Cannot change the password!! internal error.";
                }
                else
                    Lbl_Message.Text = "Current Password not match";
            }
        }
    }
    protected void Btn_Cancel_Click(object sender, EventArgs e)
    {
        txtNewPass.Attributes.Add("value", "");
        txtOldPass.Attributes.Add("value", "");
        txtRePass.Attributes.Add("value", "");
    }
    protected void Btn_home_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}
