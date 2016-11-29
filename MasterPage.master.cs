using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class MasterPage : System.Web.UI.MasterPage
{
    //Define global Connection String
    string strConnection = ConfigurationManager.ConnectionStrings["BMSConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        //side_menu();
        //if (!Page.IsPostBack)
        //{
        //    if (Session.Count > 0)
        //        Label1.Text = Session["EmployeeName"].ToString() + ", " + Session["BranchName"].ToString();
        //}
        //if ((String)Session["user"] == null)
        //{
        //    Response.AddHeader("Cache-control", "no-store, must-revalidate, private,no-cache");
        //    Response.AddHeader("Pragma", "no-cache");
        //    Response.AddHeader("Expires", "0");
        //    Response.Redirect("/BMS/Forms/Home.aspx");
        //}
        //else
        //{
        //    if (int.Parse(Session["UserTypeId"].ToString()) < 5)
        //    {
        //        id_Settings.Visible = true;
        //    }
        //}
    }

    //public void side_menu()
    //{
    //    GridView1.DataBind();
    //}
    //public void cllapsible_menu()
    //{
    //    //id_Settings.Visible = true;
    //}
    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    DataRowView drv1 = (DataRowView)e.Row.DataItem;
    //    //string fullURL = "window.open('" + drv1[1].ToString() + "', '_blank', 'height=600,width=800,status=yes,toolbar=yes,menubar=yes,location=yes,scrollbars=yes,resizable=yes,titlebar=yes' );";
    //    //    //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    //    if (drv1 != null)
    //        e.Row.Attributes.Add("onclick", "openNewWindow('" + drv1[1].ToString() + "','" + drv1[0].ToString() + "')");
    //}
    //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    string url = string.Empty;
    //    //string fullURL = string.Empty;
    //    DataTable dt = new DataTable();
    //    int rowNum = int.Parse(e.CommandArgument.ToString());
    //    Control ctrl = GridView1.Rows[rowNum].Cells[0].Controls[0];
    //    string text = string.Empty;
    //    if (((ButtonField)GridView1.Columns[0]).ButtonType == ButtonType.Link)
    //    {
    //        LinkButton btn = ctrl as LinkButton;
    //        text = btn.Text;
    //    }
    //    else
    //    {
    //        Button btn = ctrl as Button;
    //        text = btn.Text;
    //    }
    //    //GridViewRow selectedRow = GridView1.Rows[rowNum];
    //    //TableCell displayNameCell = selectedRow.Cells[0];
    //    //string display_name = displayNameCell.Text;
    //    //GridView1.Rows[rowNum].Cells[0].Text;
    //    string sql_string = "SELECT URLAddress FROM tblLink WHERE(DisplayName = @DisplayName) AND (URLStatus = 'Live')";
    //    //sql_stae = sql_stae.Replace("@P1", "'" + SqlStatement.DateAsSQLFormat(txt_time.Text) + "'");
    //    sql_string = sql_string.Replace("@DisplayName", "'" + text + "'");

    //    using (SqlConnection conn = new SqlConnection(strConnection))
    //    using (SqlCommand cmd = new SqlCommand(sql_string, conn))
    //    {
    //        try
    //        {
    //            conn.Open();
    //            cmd.CommandType = CommandType.Text;
    //            //cmd.ExecuteNonQuery();
    //            SqlDataAdapter da = new SqlDataAdapter(cmd);
    //            da.Fill(dt);
    //            if (dt.Rows.Count > 0)
    //            {
    //                url = dt.Rows[0]["URLAddress"].ToString();
    //            }
    //        }
    //        //catch (Exception strEx)
    //        //{
    //        //    Label1.ForeColor = System.Drawing.Color.Red;
    //        //    Label1.Text = "Error on:" + strEx.Message;
    //        //}
    //        finally
    //        {
    //            if (conn.State != ConnectionState.Closed)
    //            {
    //                conn.Close();
    //            }
    //        }
    //    }//using sql cmd
    //    //openNewWindow(url);
    //    //HyperLink1.Attributes.Add("onClick", "openNewWindow('" + url + "')");
    //    //string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=800,status=yes,toolbar=yes,menubar=yes,location=yes,scrollbars=yes,resizable=yes,titlebar=yes' );";
    //    //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    //    //ScriptManager.RegisterStartupScript(this, typeof(Page),"UniqueID", "alert('This pops up')", true);

    //}
}
