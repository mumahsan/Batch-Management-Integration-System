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
            statusDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        recordsInPBM(statusDate.Text);
    }

    private void recordsInPBM(string p)
    {
        DateTime dbNameDate = DateTime.ParseExact(p, "dd/MM/yyyy", null);
        string dbName = "AP" + dbNameDate.Year + dbNameDate.Month.ToString().PadLeft(2, '0') + dbNameDate.Day.ToString().PadLeft(2, '0');
        SqlConnection ApConn = new SqlConnection();
        //string constr = "Server=" + servername + ";database=" + dbname + ";integrated security=sspi";
        ApConn.ConnectionString = "Data Source=192.168.0.226;Initial Catalog=" + dbName + ";User ID=nblit;Password=nbl123;";
        ApConn.Open();
        string strquery = "select count(*)  from dbo.ItemLatest where Worksource=135";
        SqlCommand cmd = new SqlCommand(strquery, ApConn);
        using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
        {
            if (dr.HasRows)
            {
                dr.Read();
                Label2.Text = Convert.ToString(dr[0]);
            }   
        }
    }
}
