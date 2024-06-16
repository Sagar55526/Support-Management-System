using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace SupportMngmntSystem
{
    public partial class QueryReply : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["QueryID"] != null)
                {
                    string QueryID = Request.QueryString["QueryID"];
                    getMemberDetailsById(QueryID);
                }
                else
                {
                    Response.Redirect("userLogin.aspx");
                    Response.Write("<script>alert('QueryID is missing');</script>");
                }
            }
        }

        protected void QuerySubmitBtn_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["QueryID"] != null)
            {
                string QueryID = Request.QueryString["QueryID"];
                insertReplyToQuery(QueryID);
            }
        }

        void insertReplyToQuery(string QueryID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string updateQuery = @"
                        UPDATE SupportMaster 
                        SET QueryReply = @QueryReply, ReplyDate = @ReplyDate, Status = @Status
                        WHERE QueryID = @QueryID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@QueryID", QueryID);
                        cmd.Parameters.AddWithValue("@QueryReply", txtReply.Text.Trim());
                        cmd.Parameters.AddWithValue("@ReplyDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Status", "Solved");

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('Reply submitted successfully');</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed to submit reply');</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void getMemberDetailsById(string QueryID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string selectQuery = @"
                        SELECT 
                            u.UserName,
                            s.SchoolName,
                            sm.Query,
                            sm.Status
                        FROM 
                            SupportMaster sm
                        JOIN 
                            UserDetailsMaster u ON sm.UserID = u.UserID
                        JOIN 
                            SchoolDetailsMaster s ON sm.SchoolID = s.SchoolID
                        WHERE 
                            sm.QueryID = @QueryID;";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@QueryID", QueryID);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            txtUserName.Text = dt.Rows[0]["UserName"].ToString();
                            txtSchoolName.Text = dt.Rows[0]["SchoolName"].ToString();
                            txtQuery.Text = dt.Rows[0]["Query"].ToString();
                            txtStatus.Text = dt.Rows[0]["Status"].ToString();
                        }
                        else
                        {
                            Response.Write("<script>alert('No data found for the given QueryID');</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}
