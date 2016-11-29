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
using Nbl;

public partial class _DataDisplay : System.Web.UI.Page
{
    //Define global Connection String
    string strConnection = ConfigurationManager.ConnectionStrings["BMSConnectionString"].ConnectionString;
    string INCHEQSstrConnection = ConfigurationManager.ConnectionStrings["INCHEQSConnectionString"].ConnectionString;
    string INCHEQS_ICS_BANGLAstrConnection = ConfigurationManager.ConnectionStrings["INCHEQS_ICS_BANGLAConnectionString"].ConnectionString;
    string INCHEQS_ACstrConnection = ConfigurationManager.ConnectionStrings["INCHEQS_ACConnectionString"].ConnectionString;
    string Incheqs_EFTConnectionString = ConfigurationManager.ConnectionStrings["Incheqs_EFTConnectionString"].ConnectionString;

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

        //string strQueryOutward = "SELECT (case when tblItemInitial.fldCapturingType ='01' then 'Outward Regular' when  tblItemInitial.fldCapturingType ='77' then 'Outward High Value' end) as Description,COUNT(tblItemInfoTrans.fldAmount) AS Items, SUM(tblItemInfoTrans.fldAmount) AS Amount FROM  tblItemInfoTrans INNER JOIN tblItemInitial ON tblItemInfoTrans.fldItemID = tblItemInitial.fldItemInitialID WHERE (tblItemInitial.fldCreateTimeStamp BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETime,'" + dtTo + "',102)) GROUP BY tblItemInitial.fldCapturingType";
        string strQueryOutward = "SELECT (CASE WHEN Initial.fldCapturingType = '01' THEN 'Outward Regular' WHEN Initial.fldCapturingType = '77' THEN 'Outward High Value' END) AS Description, COUNT(tblItemInfoTrans.fldAmount) AS Item, SUM(tblItemInfoTrans.fldAmount)*0.01 AS Amount FROM tblItemInitial AS Initial INNER JOIN tblItemInfoTrans ON Initial.fldItemInitialID = tblItemInfoTrans.fldItemID INNER JOIN tblItemInfo ON tblItemInfoTrans.fldTransNo = tblItemInfo.fldItemInitialID INNER JOIN tblAIFDetail ON tblItemInfoTrans.fldPostAccNo = tblAIFDetail.fldPVAccount WHERE (tblItemInfo.fldIssuerAccNo IS NOT NULL) AND (Initial.fldCapturingDate BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) GROUP BY Initial.fldCapturingType ORDER BY Initial.fldCapturingType";

        using (SqlConnection conn1 = new SqlConnection(INCHEQSstrConnection))
        using (SqlCommand cmd1 = new SqlCommand(strQueryOutward, conn1))
        {
            conn1.Open();
            cmd1.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(strQueryOutward, conn1);
            da.Fill(dt);
        }
        #region returnOutward
        DataTable dt3 = new DataTable();
        string strOutwardReturn = "SELECT (CASE WHEN fldChequeType = '11' THEN 'Outward Returns Regular' WHEN fldChequeType = '19' THEN 'Outward Returns High Value' ELSE fldChequeType END) AS Type, COUNT(tblInwardItemInfo.fldAccountNumber) AS Expr1, SUM (tblInwardItemInfo.fldAmount)*0.01 AS Expr2 FROM         tblInwardItemInfo INNER JOIN tblInwardItemHistory ON tblInwardItemInfo.fldInwardItemId = tblInwardItemHistory.fldInwardItemId WHERE     (tblInwardItemInfo.fldCreateTimeStamp BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETime, '" + dtTo + "', 102)) AND (tblInwardItemHistory.fldActionStatus = 'R') GROUP BY tblInwardItemInfo.fldChequeType, tblInwardItemHistory.fldActionStatus ORDER BY tblInwardItemInfo.fldChequeType";
        using (SqlConnection conn3 = new SqlConnection(INCHEQS_ICS_BANGLAstrConnection))
        using (SqlCommand cmd3 = new SqlCommand(strOutwardReturn, conn3))
        {
            conn3.Open();
            cmd3.CommandType = CommandType.Text;
            SqlDataAdapter da3 = new SqlDataAdapter(strOutwardReturn, conn3);
            da3.Fill(dt3);

            for (int count = 0; count < dt3.Rows.Count; count++)
            {
                DataRow row = dt.NewRow();
                row[0] = dt3.Rows[count][0].ToString();
                row[1] = Int32.Parse(dt3.Rows[count][1].ToString());
                row[2] = Decimal.Parse(dt3.Rows[count][2].ToString());
                dt.Rows.Add(row);
            }
        }
        #endregion
        DataTable dt1 = new DataTable();
        //string strQuery = "SELECT 'INWARD' as Decription, COUNT(fldAmount) AS Items, SUM(fldAmount) AMOUNT FROM tblInwardItemInfo WHERE (fldClearDate BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102))";
        string strQuery = "SELECT (case  WHEN fldChequeType = '11' THEN 'Inward Regular' WHEN fldChequeType = '19' THEN 'Inward High Value' else fldChequeType end) as Type ,COUNT(fldAccountNumber) AS Expr1, SUM(fldAmount) AS Expr2 FROM  tblInwardItemInfo WHERE (fldCreateTimeStamp BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETime, '" + dtTo + "', 102)) GROUP BY fldChequeType order by fldChequeType";
        using (SqlConnection conn = new SqlConnection(INCHEQS_ICS_BANGLAstrConnection))
        using (SqlCommand cmd = new SqlCommand(strQuery, conn))
        {
            conn.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da1 = new SqlDataAdapter(strQuery, conn);
            da1.Fill(dt1);

            for (int count = 0; count < dt1.Rows.Count; count++)
            {
                DataRow row = dt.NewRow();
                row[0] = dt1.Rows[count][0].ToString();
                row[1] = Int32.Parse(dt1.Rows[count][1].ToString());
                row[2] = Decimal.Parse(dt1.Rows[count][2].ToString());
                dt.Rows.Add(row);
            }
        }

        string strQueryINwardReturn = "SELECT (case when tblItemInitial.fldCapturingType ='01' then 'Inward Returns Regular' when  tblItemInitial.fldCapturingType ='77' then 'Inward Returns High Value' end) Description,COUNT(InRet.fldAmount) AS Items,SUM(InRet.fldAmount)*0.01 AS Amount FROM tblInwardReturnItem AS InRet INNER JOIN tblItemInitial ON InRet.fldUIC = tblItemInitial.fldUIC WHERE (InRet.fldClearingDate BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETime, '" + dtTo + "', 102)) GROUP BY tblItemInitial.fldCapturingType";
        DataTable dt2 = new DataTable();
        using (SqlConnection conn2 = new SqlConnection(INCHEQSstrConnection))
        using (SqlCommand cmd2 = new SqlCommand(strQueryINwardReturn, conn2))
        {
            conn2.Open();
            cmd2.CommandType = CommandType.Text;
            SqlDataAdapter da2 = new SqlDataAdapter(strQueryINwardReturn, conn2);
            da2.Fill(dt2);

            for (int count = 0; count < dt2.Rows.Count; count++)
            {
                DataRow row = dt.NewRow();
                row[0] = dt2.Rows[count][0].ToString();
                row[1] = Int32.Parse(dt2.Rows[count][1].ToString());
                row[2] = dt2.Rows[count][2].ToString();
                dt.Rows.Add(row);
            }
        }

        dt1.Clear();
        //string strEFT = "SELECT 'EFT Outward', COUNT(*) AS Item, SUM(fldEDRAmount) * .01 AS Amount FROM tblEFTTransCompanyBatchItem WHERE (fldCreateTimeStamp BETWEEN CONVERT(DATETIME, '"+dtFrom+"', 102) AND CONVERT(DATETIME, '"+dtTo+"', 102))";
        //using(SqlConnection connEFT=new SqlConnection(Incheqs_EFTConnectionString))
        //using (SqlCommand cmdEFT = new SqlCommand(strEFT, connEFT))
        //{
        //    connEFT.Open();
        //    cmdEFT.CommandType = CommandType.Text;
        //    SqlDataAdapter daEFT = new SqlDataAdapter(strEFT, connEFT);
        //    daEFT.Fill(dt1);
        //    for (int count = 0; count < dt1.Rows.Count; count++)
        //    {
        //        DataRow row = dt.NewRow();
        //        row[0] = dt1.Rows[count][0].ToString();
        //        row[1] = Int32.Parse(dt1.Rows[count][1].ToString());
        //        row[2] = Decimal.Parse(dt1.Rows[count][2].ToString());
        //        dt.Rows.Add(row);
        //    }
        //}

        GridView2.DataSource = dt;
        GridView2.DataBind();

    }

    private void InwardHighValueInPBM(string p)
    {
        DataTable dt = new DataTable();
        DateTime dbNameDate = DateTime.ParseExact(p, "dd/MM/yyyy", null);
        string dbName = "AP" + dbNameDate.Year + dbNameDate.Month.ToString().PadLeft(2, '0') + dbNameDate.Day.ToString().PadLeft(2, '0');
        SqlConnection ApConn = new SqlConnection();
        //string constr = "Server=" + servername + ";database=" + dbname + ";integrated security=sspi";
        ApConn.ConnectionString = "Data Source=192.168.0.224;Initial Catalog=" + dbName + ";User ID=bms;Password=bms123;";
        ApConn.Open();
        //string strquery = "SELECT     Worksource, (CASE WHEN Worksource = '110' THEN 'Outward Regular' WHEN Worksource = '115' THEN 'Outward High Value' WHEN Worksource = '120' THEN 'Outward Returns Regular' WHEN Worksource = '125' THEN 'Outward Returns High Value' WHEN Worksource = '130' THEN 'Inward Regular' WHEN Worksource = '135' THEN 'Inward High Value' WHEN Worksource = '140' THEN 'Inward Returns Regular' WHEN Worksource = '145' THEN 'Inward Returns High Value' WHEN Worksource = '160' THEN 'EFT Outward' WHEN Worksource = '170' THEN 'EFT Outward Returns' WHEN Worksource = '180' THEN 'EFT Inward' WHEN Worksource = '190' THEN 'EFT Inward Returns' ELSE CAST(Worksource AS VARCHAR) END) AS WorksourceDesc, (CASE WHEN COUNT(Amount) IS NULL THEN 0 ELSE COUNT(Amount) END) AS Items, (CASE WHEN (SUM(Amount) * .01) IS NULL THEN 0 ELSE (SUM(Amount) * .01) END) AS Amount FROM ItemLatest GROUP BY Worksource ORDER BY Worksource"; //ORDER BY Worksource DESC";
        //string strquery = "SELECT Worksource, (CASE WHEN Worksource = '110' THEN 'Outward Regular' WHEN Worksource = '115' THEN 'Outward High Value' WHEN Worksource = '120' THEN 'Outward Returns Regular' WHEN Worksource = '125' THEN 'Outward Returns High Value' WHEN Worksource = '130' THEN 'Inward Regular' WHEN Worksource = '135' THEN 'Inward High Value' WHEN Worksource = '140' THEN 'Inward Returns Regular' WHEN Worksource = '145' THEN 'Inward Returns High Value' WHEN Worksource = '160' THEN 'EFT Outward' WHEN Worksource = '170' THEN 'EFT Outward Returns' WHEN Worksource = '180' THEN 'EFT Inward' WHEN Worksource = '190' THEN 'EFT Inward Returns' ELSE CAST(Worksource AS VARCHAR) END) AS WorksourceDesc, (CASE WHEN COUNT(Amount) IS NULL THEN 0 ELSE COUNT(Amount) END) AS Items, (CASE WHEN (SUM(Amount) * .01) IS NULL THEN 0 ELSE (SUM(Amount) * .01) END) AS Amount FROM ItemLatest GROUP BY Worksource HAVING (Worksource < 160) ORDER BY Worksource";
        string strquery = "select '110' as Worksource,'Outward Regular' as Description ,isnull(COUNT(*),0) as Item,isnull((SUM(Amount) * .01),0) AS Amount FROM ItemLatest " +
            "where (Worksource ='110') union " +
            "select '115' as Worksource,'Outward High Value' as Description ,isnull(COUNT(*),0) as Item,isnull((SUM(Amount) * .01),0) AS Amount FROM ItemLatest " +
            "where (Worksource ='115') " +
            "union " +
            "select '120' as Worksource,'Outward Returns Regular' as Description,isnull(COUNT(*),0) as Item,isnull((SUM(Amount) * .01),0) AS Amount FROM ItemLatest " +
            "where (Worksource ='120') " +
            "union " +
            "select '125' as Worksource,'Outward Returns High Value' as Description,isnull(COUNT(*),0) as Item,isnull((SUM(Amount) * .01),0) AS Amount FROM ItemLatest " +
            "where (Worksource ='125') " +
            "union " +
            "select '130' as Worksource,'Inward Regular' as Description,isnull(COUNT(*),0) as Item,isnull((SUM(Amount) * .01),0) AS Amount FROM ItemLatest " +
            "where (Worksource ='130') " +
            "union " +
            "select '135' as Worksource,'Inward High Value' as Description,isnull(COUNT(*),0) as Item,isnull((SUM(Amount) * .01),0) AS Amount FROM ItemLatest " +
            "where (Worksource ='135') " +
            "union " +
            "select '140' as Worksource,'Inward Returns Regular' as Description,isnull(COUNT(*),0) as Item,isnull((SUM(Amount) * .01),0) AS Amount FROM ItemLatest " +
            "where (Worksource ='140') " +
            "union " +
            "select '145' as Worksource,'Inward Returns High Value' as Description,isnull(COUNT(*),0) as Item,isnull((SUM(Amount) * .01),0) AS Amount FROM ItemLatest " +
            "where (Worksource ='145') " +
            "union " +
            "select '160' as Worksource,'EFT Outward' as Description,isnull(COUNT(*),0) as Item,isnull((SUM(Amount) * .01),0) AS Amount FROM ItemLatest " +
            "where (Worksource ='160') and Field7Data = 'EDR' " +
            "union " +
            "select '170' as Worksource,'EFT Outward Returns' as Description,isnull(COUNT(*),0) as Item,isnull((SUM(Amount) * .01),0) AS Amount FROM ItemLatest " +
            "where (Worksource ='170') and Field7Data = 'EDR' " +
            "union " +
            "select '180' as Worksource,'EFT Inward' as Description,isnull(COUNT(*),0) as Item,isnull((SUM(Amount) * .01),0) AS Amount FROM ItemLatest " +
            "where (Worksource ='180') and Field7Data = 'EDR' " +
            "union " +
            "select '190' as Worksource,'EFT Inward Returns' as Description,isnull(COUNT(*),0) as Item,isnull((SUM(Amount) * .01),0) AS Amount FROM ItemLatest " +
            "where (Worksource ='190') and Field7Data = 'EDR'";

        //SqlCommand cmd = new SqlCommand("sp_pbm", ApConn);
        //cmd.CommandType = CommandType.StoredProcedure;
        SqlCommand cmd = new SqlCommand(strquery, ApConn);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);


        /*DataTable dt1 = new DataTable();
        strquery = "SELECT Worksource,(case WHEN Worksource = '160' THEN 'EFT Outward' WHEN Worksource = '170' THEN 'EFT Outward Returns' WHEN Worksource = '180' THEN 'EFT Inward' WHEN Worksource = '190' THEN 'EFT Inward Returns' else 'Not Match' end) AS WorksourceDesc, (CASE WHEN COUNT(Amount) IS NULL THEN 0 ELSE COUNT(Amount) END) AS Items, (CASE WHEN (SUM(Amount) * .01) IS NULL THEN 0 ELSE (SUM(Amount) * .01) END) AS Amount FROM ItemLatest WHERE     (Field7Data = 'EDR') and (Worksource > 145) GROUP BY Worksource ORDER BY Worksource";
        da = new SqlDataAdapter(strquery, ApConn);
        da.Fill(dt1);

        for (int count = 0; count < dt1.Rows.Count; count++)
        {
            DataRow row = dt.NewRow();
            row[0] = dt1.Rows[count][0].ToString();
            row[1] = dt1.Rows[count][1].ToString();
            row[2] = dt1.Rows[count][2].ToString();
            row[3] = dt1.Rows[count][3].ToString();
            dt.Rows.Add(row);
        }*/

        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
}
