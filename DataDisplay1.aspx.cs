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

public partial class _DataDisplay : System.Web.UI.Page
{
    //Define global Connection String
    string strConnection = ConfigurationManager.ConnectionStrings["BMSConnectionString"].ConnectionString;
    string INCHEQSstrConnection = ConfigurationManager.ConnectionStrings["INCHEQSConnectionString"].ConnectionString;
    string INCHEQS_ICS_BANGLAstrConnection = ConfigurationManager.ConnectionStrings["INCHEQS_ICS_BANGLAConnectionString"].ConnectionString;
    string INCHEQS_ACstrConnection = ConfigurationManager.ConnectionStrings["INCHEQS_ACConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            statusDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        InwardHighValueInPBM(statusDate.Text);
        InwardHighValueInIncheques(statusDate.Text);
    }

    private void InwardHighValueInIncheques(string p)
    {
        DataTable dt = new DataTable();
        DateTime dtFrom = DateTime.ParseExact(p, "dd/MM/yyyy", null);
        //DateTime dtToTemp = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
        DateTime dtTo = new DateTime(dtFrom.Year, dtFrom.Month, dtFrom.Day, 23, 59, 59);
        string strQuery = "SELECT    'INWARD', COUNT(fldAmount) AS ITEM_NUMBER, SUM(fldAmount) AMOUNT FROM tblInwardItemInfo WHERE (fldClearDate BETWEEN CONVERT(DATETIME, '"+dtFrom+"', 102) AND CONVERT(DATETIME, '"+dtTo+"', 102))";
        using(SqlConnection conn=new SqlConnection(INCHEQS_ICS_BANGLAstrConnection))
        using (SqlCommand cmd = new SqlCommand(strQuery, conn))
        {
            conn.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(strQuery, conn);
            da.Fill(dt);
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
    }

    private void InwardHighValueInPBM(string p)
    {
        DataTable dt=new DataTable();
        DateTime dbNameDate = DateTime.ParseExact(p, "dd/MM/yyyy", null);
        string dbName = "AP" + dbNameDate.Year + dbNameDate.Month.ToString().PadLeft(2, '0') + dbNameDate.Day.ToString().PadLeft(2, '0');
        SqlConnection ApConn = new SqlConnection();
        //string constr = "Server=" + servername + ";database=" + dbname + ";integrated security=sspi";
        ApConn.ConnectionString = "Data Source=192.168.0.224;Initial Catalog=" + dbName + ";User ID=bms;Password=bms123;";
        ApConn.Open();
        string strquery = "SELECT  (CASE WHEN Worksource='110' THEN 'OUTWARD REGULAR' WHEN Worksource='115' THEN 'OUTWARD HIGH VALUE' WHEN Worksource='120' THEN 'OUTWARD RETURNS REGULAR' WHEN Worksource='125' THEN 'OUTWARD RETURNS HIGH VALUE' WHEN Worksource='130' THEN 'INWARD REGULAR' WHEN Worksource='135' THEN 'INWARD HIGH VALUE' WHEN Worksource='140' THEN 'INWARD RETURNS REGULAR' WHEN Worksource='145' THEN 'INTWARD RETURNS HIGH VALUE' WHEN Worksource='160' THEN 'EFT OUTWARD' WHEN Worksource='170' THEN 'EFT OUTWARD RETURNS' WHEN Worksource='180' THEN 'EFT INWARD' WHEN Worksource='190' THEN 'EFT INWARD RETURNS' ELSE CAST(Worksource AS VARCHAR) END) Worksource,count(Amount) NumberOfItem, sum(Amount)*.01 Amount FROM ItemLatest GROUP BY Worksource ORDER BY Worksource";
        SqlCommand cmd = new SqlCommand(strquery, ApConn);
        SqlDataAdapter da = new SqlDataAdapter(strquery, ApConn);
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        //using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
        //{
        //    if (dr.HasRows)
        //    {
        //        dr.Read();
        //        Label2.Text = Convert.ToString(dr[0]);
        //    }
        //}
    }
    
}
