using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace SupportMngmntSystem
{
    public partial class userLogin : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UserLoginEvent();
        }

        void UserLoginEvent()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM UserCredential WHERE UserName=@UserName AND Password=@Password", con);
                    cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

                    cmd.CommandTimeout = 10;

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {

                            Session["UserName"] = dr["UserName"].ToString();
                            Session["UserID"] = dr["UserID"].ToString();

                            //if (dr["UserID"].ToString() == "9890264656")
                            //{
                            //    Session["role"] = "admin";
                            //    string alertScript = "alert('Admin Login Successfully.');";
                            //    ClientScript.RegisterStartupScript(this.GetType(), "LoginSuccess", alertScript, true);
                            //    Response.Redirect("adminSupportDash.aspx", false);
                            //    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                            //    return; 
                            //}

                            if (dr["UserName"].ToString() != "9890264656")
                            {
                                Session["role"] = "user";
                                Response.Redirect("SupportMaster.aspx");
                            }
                            else
                            {
                                Session["role"] = "admin";
                                Response.Redirect("AdminSupportDash.aspx");
                            }

                        }

                        // Redirect regular users
                        string userAlertScript = "alert('Login Successfully.');";
                        ClientScript.RegisterStartupScript(this.GetType(), "LoginSuccess", userAlertScript, true);
                        Response.Redirect("SupportMaster.aspx", false);
                        System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        string alertScript = "alert('Invalid credentials');";
                        ClientScript.RegisterStartupScript(this.GetType(), "LoginFailure", alertScript, true);
                    }
                }
            }
            catch (Exception ex)
            {
                string errorScript = $"alert('An error occurred: {ex.Message}');";
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorScript", errorScript, true);
            }
        }
    }
}
