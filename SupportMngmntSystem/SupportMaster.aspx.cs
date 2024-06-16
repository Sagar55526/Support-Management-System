using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SupportMngmntSystem
{
    public partial class SupportMaster : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("userLogin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    BindUserName();
                    BindSchoolName();
                    BindQueryGrid();
                }
            }

        }

        protected void QuerySubmitBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                InsertSupportQuery();
            }
            else
            {
                Response.Redirect("userLogin.aspx");
            }
        }


        protected void btnNewQuery_Click(object sender, EventArgs e)
        {
            txtQuery.Text = "";
            txtReply.Text = "";
            txtStatus.Text = "";
            txtReply.Visible = false;
            txtQuery.ReadOnly = false;
            QuerySubmitBtn.Visible = true;
        }


        void InsertSupportQuery()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string selectQuery = @"SELECT uc.UserID, udm.SchoolID
                                   FROM UserCredential uc
                                   INNER JOIN UserDetailsMaster udm ON uc.UserID = udm.UserID
                                   WHERE uc.UserID = @UserID";

                    SqlCommand selectCmd = new SqlCommand(selectQuery, con);
                    selectCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

                    SqlDataAdapter da = new SqlDataAdapter(selectCmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        int userID = Convert.ToInt32(dt.Rows[0]["UserID"]);
                        int schoolID = Convert.ToInt32(dt.Rows[0]["SchoolID"]);

                        // Get current date without time
                        DateTime currentDate = DateTime.Now.Date;

                        string insertQuery = @"INSERT INTO SupportMaster (SchoolID, UserID, Query, time, Status)
                   VALUES (@SchoolID, @UserID, @Query, @time, @Status);
                   SELECT SCOPE_IDENTITY();"; // Get the inserted QueryID

                        SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                        insertCmd.Parameters.AddWithValue("@SchoolID", schoolID);
                        insertCmd.Parameters.AddWithValue("@UserID", userID);
                        insertCmd.Parameters.AddWithValue("@Query", txtQuery.Text.Trim());
                        insertCmd.Parameters.AddWithValue("@time", currentDate); // Use currentDate here
                        insertCmd.Parameters.AddWithValue("@Status", "Pending");

                        // Execute the insert command and get the inserted QueryID
                        int queryID = Convert.ToInt32(insertCmd.ExecuteScalar());

                        string currentTime = currentDate.ToString("dd/MM/yyyy"); // Format date only
                        Response.Write("<script>alert('Query Submitted Successfully at " + currentTime + ".');</script>");

                        // Bind the GridView again to reflect the new data
                        BindQueryGrid();
                    }
                    else
                    {
                        Response.Write("<script>alert('User details not found.');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('An error occurred: " + ex.Message + "');</script>");
            }
        }


        void BindUserName()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT UserName FROM UserDetailsMaster WHERE UserID = @UserID", con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            txtUserName.Text = dt.Rows[0]["UserName"].ToString();
                        }
                        else
                        {
                            Response.Write("<script>alert('User Name not found.');</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void BindSchoolName()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    using (SqlCommand sqlCmd = new SqlCommand("SELECT SchoolID FROM UserDetailsMaster WHERE UserID = @UserID", con))
                    {
                        sqlCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

                        object stdID = sqlCmd.ExecuteScalar();

                        if (stdID != null)
                        {
                            using (SqlCommand cmd = new SqlCommand("SELECT SchoolName FROM SchoolDetailsMaster WHERE SchoolID = @SchoolID", con))
                            {
                                cmd.Parameters.AddWithValue("@SchoolID", stdID.ToString());

                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                DataTable dt = new DataTable();
                                da.Fill(dt);

                                if (dt.Rows.Count > 0)
                                {
                                    txtSchoolName.Text = dt.Rows[0]["SchoolName"].ToString();
                                }
                                else
                                {
                                    Response.Write("<script>alert('School Name not found.');</script>");
                                }
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('School ID not found.');</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void BindQueryGrid()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT QueryID, Query, time, Status, QueryReply, ReplyDate FROM SupportMaster WHERE UserID = @UserID", con);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
                Button btnView = (Button)e.Row.FindControl("btnview");

                if (status == "Solved")
                {
                    btnView.Visible = true;
                }
                else
                {
                    btnView.Visible = false;
                }
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                int queryID = Convert.ToInt32(e.CommandArgument);

                try
                {
                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlCommand cmd = new SqlCommand("SELECT Query, QueryReply, Status FROM SupportMaster WHERE QueryID = @QueryID", con);
                        cmd.Parameters.AddWithValue("@QueryID", queryID);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            txtQuery.Text = reader["Query"].ToString();
                            txtReply.Text = reader["QueryReply"].ToString();
                            txtStatus.Text = reader["Status"].ToString();
                        }

                        txtReply.Visible = true;
                        txtReply.ReadOnly = true;
                        txtQuery.ReadOnly = true;
                        QuerySubmitBtn.Visible = false;

                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

       
    }
}
