using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimpleDataBinding
{
    public partial class DataBinding : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                BindDataToGridView();
        }

        protected void gvColor_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ltError.Text = string.Empty;
            GridViewRow gvRow = (GridViewRow)gvColor.Rows[e.RowIndex];
            HiddenField hdnColorId = (HiddenField)gvRow.FindControl("hdnColorId");
            var connectionFormConfiguration = WebConfigurationManager.ConnectionStrings["DbConnection"];
            using (SqlConnection dbConncetion = new SqlConnection(connectionFormConfiguration.ConnectionString))
            {
                try
                {
                    dbConncetion.Open();
                    string sql = string.Format("DELETE FROM Colors WHERE ColorID={0}", hdnColorId.Value);
                    SqlCommand cmd = new SqlCommand(sql, dbConncetion);
                    cmd.ExecuteNonQuery();
                    gvColor.EditIndex = -1;
                    BindDataToGridView();
                }
                catch (Exception ex)
                {
                    ltError.Text = ex.Message;
                }
                finally
                {
                    dbConncetion.Close();
                    dbConncetion.Dispose();
                }
            }
        }

        protected void gvColor_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ltError.Text = string.Empty;
            gvColor.EditIndex = e.NewEditIndex;
            BindDataToGridView();
        }

        protected void gvColor_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ltError.Text = string.Empty;
            GridViewRow gvRow = gvColor.Rows[e.RowIndex];
            HiddenField hdnColorId = (HiddenField)gvRow.FindControl("hdnColorId");
            TextBox txtName = (TextBox)gvRow.Cells[1].Controls[0];
            TextBox txtHex = (TextBox)gvRow.Cells[2].Controls[0];

            var connectionFromConfiguration = WebConfigurationManager.ConnectionStrings["DbConnection"];
            using (SqlConnection dbConnection = new SqlConnection(connectionFromConfiguration.ConnectionString))
            {
                try
                {
                    dbConnection.Open();
                    string sql = string.Format("UPDATE Colors SET Name='{0}', HexCode='{1}' WHERE ColorID={2}", txtName.Text, txtHex.Text, hdnColorId.Value);
                    SqlCommand cmd = new SqlCommand(sql, dbConnection);
                    cmd.ExecuteNonQuery();
                    gvColor.EditIndex = -1;
                    BindDataToGridView();
                }
                catch (SqlException ex)
                {
                    ltError.Text = "Error:" + ex.Message;
                }
                finally
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }

        protected void gvColor_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvColor.EditIndex = -1;
            BindDataToGridView();
        }

        public void BindDataToGridView()
        {
            var connectionFromConfiguration = WebConfigurationManager.ConnectionStrings["DbConnection"];
            using (SqlConnection dbConnection = new SqlConnection(connectionFromConfiguration.ConnectionString))
            {
                try
                {
                    dbConnection.Open();
                    SqlCommand command = new SqlCommand("SELECT ColorID, Name, HexCode FROM Colors ORDER BY ColorID", dbConnection);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        gvColor.DataSource = dataSet;
                        gvColor.DataBind();
                    }
                }
                catch (SqlException ex)
                {
                    ltError.Text = "Error:" + ex.Message;
                }
                finally
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }

        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            var connectionFromConfiguration = WebConfigurationManager.ConnectionStrings["DbConnection"];
            using (SqlConnection dbConnection = new SqlConnection(connectionFromConfiguration.ConnectionString))
            {
                try
                {
                    dbConnection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Colors (Name, HexCode) VALUES ('','')", dbConnection);
                    command.ExecuteNonQuery();
                    BindDataToGridView();

                }
                catch (Exception ex)
                {
                    ltError.Text = "Error:" + ex.Message;
                }
                finally
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }
    }
}