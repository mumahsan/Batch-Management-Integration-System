using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Forms_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //statusDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        //recordsInPBM(statusDate.Text);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView drv1 = (DataRowView)e.Row.DataItem;
        //string fullURL = "window.open('" + drv1[1].ToString() + "', '_blank', 'height=600,width=800,status=yes,toolbar=yes,menubar=yes,location=yes,scrollbars=yes,resizable=yes,titlebar=yes' );";
        //    //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        if (drv1 != null)
            e.Row.Attributes.Add("onclick", "openNewWindow('" + drv1[1].ToString() + "','" + drv1[0].ToString() + "')");
    }
}
