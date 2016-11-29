using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Specialized;

public partial class Forms_LinkEntry : System.Web.UI.Page
{
    //Define global Connection String
    string strConnection = ConfigurationManager.ConnectionStrings["BMSConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((String)Session["user"] == null)
            {
                Response.AddHeader("Cache-control", "no-store, must-revalidate, private,no-cache");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");
                Response.Redirect("/BMS/Default.aspx");
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text == string.Empty || TextBox2.Text == string.Empty)
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Please Enter values for URL Address and Display Name.";
        }
        else
        {
            if (Button1.Text.Contains("Save"))
            {
                #region save

                using (SqlConnection conn = new SqlConnection(strConnection))
                using (SqlCommand cmd = new SqlCommand("spLinkIns", conn))
                {
                    try
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@URLAddress", SqlDbType.VarChar).Value = TextBox1.Text.ToString();
                        cmd.Parameters.Add("@DisplayName", SqlDbType.VarChar).Value = TextBox2.Text.ToString();
                        cmd.Parameters.Add("@URLStatus", SqlDbType.VarChar).Value = DropDownList1.SelectedItem.Text;
                        cmd.Parameters.Add("@EntryBy", SqlDbType.VarChar).Value = Session["user"].ToString();
                        cmd.Parameters.Add("@EntryDt", SqlDbType.DateTime).Value = DateTime.Now;
                        int noOfRowsAffected = cmd.ExecuteNonQuery();
                        if (noOfRowsAffected > 0)
                        {
                            Label1.ForeColor = System.Drawing.Color.Green;
                            Label1.Text = "Saved Successfully.";
                            resetControls();
                        }
                    }
                    catch (Exception errEx)
                    {
                        Label1.ForeColor = System.Drawing.Color.Red;
                        Label1.Text = "Error: " + errEx.Message;
                    }
                    finally
                    {
                        if (conn.State != ConnectionState.Closed)
                        {
                            conn.Close();
                        }
                    }
                }
                #endregion
            }
            else if (Button1.Text.Contains("Update"))
            {
                #region Update

                using (SqlConnection conn = new SqlConnection(strConnection))
                using (SqlCommand cmd = new SqlCommand("spLinkUpd", conn))
                {
                    try
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        string linkID = HiddenField1.Value.ToString();
                        //@LinkID int,@URLAddress VARCHAR(150),@DisplayName VARCHAR(150),@URLStatus VARCHAR(5),@EntryBy VARCHAR(50),@EntryDt DATETIME
                        cmd.Parameters.Add("@LinkID", SqlDbType.Int).Value = Int32.Parse(linkID);
                        cmd.Parameters.Add("@URLAddress", SqlDbType.VarChar).Value = TextBox1.Text.ToString();
                        cmd.Parameters.Add("@DisplayName", SqlDbType.VarChar).Value = TextBox2.Text.ToString();
                        cmd.Parameters.Add("@URLStatus", SqlDbType.VarChar).Value = DropDownList1.SelectedItem.Text;
                        cmd.Parameters.Add("@EntryBy", SqlDbType.VarChar).Value = Session["user"].ToString();
                        cmd.Parameters.Add("@EntryDt", SqlDbType.DateTime).Value = DateTime.Now;
                        int noOfRowsAffected = cmd.ExecuteNonQuery();
                        if (noOfRowsAffected > 0)
                        {
                            Label1.ForeColor = System.Drawing.Color.Green;
                            Label1.Text = "Updated Successfully.";
                            Button1.Text = "Save";
                            resetControls();
                            
                            //this.Page_Load(null,EventArgs.Empty);
                            //base.OnLoad(e);
                            
                        }
                    }
                    catch (Exception errEx)
                    {
                        Label1.ForeColor = System.Drawing.Color.Red;
                        Label1.Text = "Error: " + errEx.Message;
                    }
                    finally
                    {
                        if (conn.State != ConnectionState.Closed)
                        {
                            conn.Close();
                        }
                    }
                }
                #endregion
            }
        }
    }

    private void resetControls()
    {
        MasterPage master = (MasterPage)this.Master;
        //master.side_menu();
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            btnDelete.Visible = true;
        }
        else if (GridView1.Rows.Count < 1)
        {
            btnDelete.Visible = false;
        }

        TextBox1.Text = string.Empty;
        TextBox2.Text = string.Empty;
        DropDownList1.ClearSelection();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //Create String Collection to store 
        //IDs of records to be deleted 
        StringCollection idCollection = new StringCollection();
        string strID = string.Empty;

        //Loop through GridView rows to find checked rows 
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chkDelete = (CheckBox)
            GridView1.Rows[i].Cells[0].FindControl("chkSelect");
            if (chkDelete != null)
            {
                if (chkDelete.Checked)
                {
                    strID = GridView1.Rows[i].Cells[0].Text;
                    idCollection.Add(strID);
                }
            }
        }

        //Call the method to Delete records 
        DeleteMultipleRecords(idCollection);

        // rebind the GridView
        GridView1.DataBind();

    }
    /// <summary>
    /// This is to Delete multiple records in gridview
    /// </summary>
    /// <param name="idCollection">stringCollection</param>
    private void DeleteMultipleRecords(StringCollection idCollection)
    {
        //Create sql Connection and Sql Command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        string IDs = "";

        foreach (string id in idCollection)
        {
            IDs += "'" + id.ToString() + "'" + ",";
        }
        try
        {
            string strIDs = IDs.Substring(0, IDs.LastIndexOf(","));
            string strSql = "Delete from tblLink WHERE LinkID in (" + strIDs + ")";
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSql;
            cmd.Connection = con;
            con.Open();
            //cmd.ExecuteNonQuery();
            int noOfRowsAffected = cmd.ExecuteNonQuery();

            if (noOfRowsAffected > 0)
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Successfully Deleted.";
                resetControls();
            }
        }
        catch (SqlException ex)
        {
            string errorMsg = "Error in Deletion";
            errorMsg += ex.Message;
            throw new Exception(errorMsg);
        }
        finally
        {
            con.Close();
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        if (commandName == "Edit_")
        {
            StringCollection idCollection = new StringCollection();
            string strID = string.Empty;
            int rowNum = int.Parse(e.CommandArgument.ToString());
            string reqId = GridView1.Rows[rowNum].Cells[0].Text;
            //Loop through GridView rows to find same data rows 
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (GridView1.Rows[i].Cells[1].Text == GridView1.Rows[rowNum].Cells[1].Text)
                {
                    strID = GridView1.Rows[i].Cells[0].Text;
                    idCollection.Add(strID);
                }
                //TextBox txtAccNo = (TextBox)GridView1.Rows[i].Cells[0].FindControl("TxtAccNo");
                //if (txtAccNo != null)
                //{   
                //}
            }
            
            Session.Add("IDs", idCollection);
            HiddenField1.Value = strID;
            TextBox1.Text = GridView1.Rows[rowNum].Cells[1].Text;
            //TxtAccNo.Text = GridView1.Rows[rowNum].Cells[1].Text;
            TextBox2.Text = GridView1.Rows[rowNum].Cells[2].Text;
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(GridView1.Rows[rowNum].Cells[3].Text));
            Button1.Text = "Update";
        }//elseif
    }
}
