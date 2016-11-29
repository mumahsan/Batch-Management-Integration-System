using System;
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

public partial class _Default : System.Web.UI.Page
{
    //Define global Connection String
    string strConnection = ConfigurationManager.ConnectionStrings["BMSConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        txtUserName.Focus();
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        #region login
        if (!Page.IsValid)
        {
            return;
        }

        //Create sql Connection and Sql Command
        SqlConnection conn = new SqlConnection(strConnection);
        conn.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "getLogInInfo";
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.SelectCommand.Parameters.Add("@userid", SqlDbType.VarChar).Value = txtUserName.Text;


        DataTable dt = new DataTable();
        da.Fill(dt);
        if (conn.State != ConnectionState.Closed) { conn.Close(); }
        //DbHandler dbh = new DbHandler();

        //DataTable dt = dbh.GetDataTable(str);

        if (dt.Rows.Count < 1)
        {
            //errorDisplay.Value = "Invalid User Id & Password.";
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Invalid UserName or Password');", true);
        }
        else
        {
            if (dt.Rows[0]["Password"].ToString().Trim() != txtPassword.Text.Trim())
            {
                //errorDisplay.Value = "Invalid User Id & Password.";
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Invalid UserName or Password');", true);
            }
            else
            {
                using (SqlCommand cmd1 = new SqlCommand("LogInUser", conn))
                {
                    try
                    {
                        conn.Open();
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add("@userid", SqlDbType.VarChar).Value = txtUserName.Text.Trim();
                        //int noOfRowsAffected = cmd1.ExecuteNonQuery();
                        int noOfRowsAffected = (int)cmd1.ExecuteScalar();
                        if (noOfRowsAffected > 0)
                        {
                            Session.Add("user", txtUserName.Text.Trim());
                            Session.Add("EmployeeName", dt.Rows[0]["EmployeeName"].ToString());
                            Session.Add("BranchName", dt.Rows[0]["BranchName"].ToString());
                            Session.Add("BrCode", dt.Rows[0]["BranchCode"].ToString());
                            Session.Add("UserTypeId",dt.Rows[0]["UserTypeId"].ToString());
                            Response.Redirect(@"~\Forms\Home.aspx");
                        }
                    }
                    catch (SqlException eX)
                    {
                        Label1.ForeColor = System.Drawing.Color.Red;
                        Label1.Text = eX.Message;
                    }
                    finally { if (conn.State != ConnectionState.Closed) { conn.Close(); } }
                }
            }
        }
        #endregion
    }
}
