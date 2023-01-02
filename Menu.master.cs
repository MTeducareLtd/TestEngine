using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Microsoft.VisualBasic;
using ShoppingCart.BL;
using System.Web;

partial class Menu : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            FindUserCompany();

        }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Request.Cookies["MyCookiesLoginInfo"] != null)
            {
                if (!IsPostBack)
                {
                    Generate_Menu();
                    FindUserNotification();
                    FindUserMessages();
                }
            }
            else
            {
                Response.Redirect("Default.aspx", false);
            }
        }
        catch (Exception)
        {
            
            throw;
        }
        

        
    }

    protected void btnShortCut_TestSchedule_ServerClick(object sender, System.EventArgs e)
    {
        Response.Redirect("Tran_TestSchedule.aspx");
    }

    protected void btnShortCut_TestAttendance_ServerClick(object sender, System.EventArgs e)
    {
        Response.Redirect("Tran_Testattendance.aspx");
    }

    protected void btnShortCut_TestAnswerPaper_ServerClick(object sender, System.EventArgs e)
    {
        Response.Redirect("Tran_Testanswerpapers.aspx");
    }

    protected void btnShortCut_TestMarks_ServerClick(object sender, System.EventArgs e)
    {
        Response.Redirect("Tran_Testmarks.aspx");
    }

    private void FindUserCompany()
    {
        try
        {


            // HttpCookie cookie = Request.Cookies.Get("UserInfo");
            string UserId = null;
            string UserName = null;
            string DBName = null;
            string UserTypeCode = null;

            if (Request.Cookies["MyCookiesLoginInfo"] != null)
            {
                UserId = Request.Cookies["MyCookiesLoginInfo"]["UserID"];
                UserName = Request.Cookies["MyCookiesLoginInfo"]["UserName"];
                DBName = Request.Cookies["MyCookiesLoginInfo"]["DBName"];
                UserTypeCode = Request.Cookies["MyCookiesLoginInfo"]["UserTypeCode"];
                string role = Request.Cookies["MyCookiesLoginInfo"]["Role"];
                lblHeader_User_Name.Text = UserName;
                lblHeader_User_Code.Text = UserId;
                lblHeader_DBName.Text = DBName;

                DataSet dsUserCompany = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserId, "", "", "", "1", DBName);
                lblHeader_Company_Count.Text = Convert.ToString(dsUserCompany.Tables[0].Rows.Count);


                if (dsUserCompany.Tables[0].Rows.Count == 1)
                {
                    lblHeader_Company_Count_String.Text = "1 Company is assigned to you";
                }
                else
                {
                    lblHeader_Company_Count_String.Text = dsUserCompany.Tables[0].Rows.Count + " Companies are assigned to you";
                }

                if (dsUserCompany.Tables[0].Rows.Count > 0)
                {
                    lblHeader_Company_Name.Text = Convert.ToString(dsUserCompany.Tables[0].Rows[0]["Company_Name"]);
                    lblHeader_Company_Code.Text = Convert.ToString(dsUserCompany.Tables[0].Rows[0]["Company_code"]);
                }
                else
                {
                    lblHeader_Company_Name.Text = "Not assigned";
                    lblHeader_Company_Code.Text = "";
                }

                //Admin module


                //if (role == "R037" || role == "R026" || role == "R014")
                //{
                //     if (role == "R032") //j@yesh

                ////if (role == "R026")
                ////{
                //    Menu_H_Config.Visible = false;
                //    //True (Configuration menu is also not required for admin
                //    Menu_H_Master.Visible = true;
                //    Menu_I_ManageBatch.Visible = false;
                //    Menu_I_ManageTestSchedule.Visible = false;
                //    Menu_I_ManageTestAttendance.Visible = false;
                //    Menu_I_ManageAnswerSheets.Visible = false;
                //    Menu_I_ManageTestMarks.Visible = false;
                //    Menu_I_ProcessAnswers.Visible = true;
                //    Menu_I_ManageFinalExamMarks.Visible = false;
                //    Menu_I_AproveTestRemove.Visible = true;
                //    Menu_H_Reports.Visible = true;

                //    btnShortCut_TestSchedule.Disabled = true;
                //    btnShortCut_TestAttendance.Disabled = true;
                //    btnShortCut_TestAnswerPaper.Disabled = true;
                //    btnShortCut_TestMarks.Disabled = true;

                //    //User module
                //}
                //else 
                //{
                //    Menu_H_Config.Visible = false;
                //    Menu_H_Master.Visible = false;
                //    Menu_I_ManageBatch.Visible = false;
                //    Menu_I_ManageTestSchedule.Visible = true;
                //    Menu_I_ManageTestAttendance.Visible = true;
                //    Menu_I_ManageAnswerSheets.Visible = true;
                //    Menu_I_ManageTestMarks.Visible = true;
                //    Menu_I_ProcessAnswers.Visible = false;
                //    Menu_I_ManageFinalExamMarks.Visible = false;
                //    Menu_I_AproveTestRemove.Visible = false;
                //    Menu_H_Reports.Visible = true;
                //}
                ////else
                ////{
                ////    //All menus should be activated through User Roles currently hide all
                ////    Menu_H_Config.Visible = false;
                ////    Menu_H_Master.Visible = false;
                ////    Menu_I_ManageBatch.Visible = false;
                ////    Menu_I_ManageTestSchedule.Visible = false;
                ////    Menu_I_ManageTestAttendance.Visible = false;
                ////    Menu_I_ManageAnswerSheets.Visible = false;
                ////    Menu_I_ManageTestMarks.Visible = false;
                ////    Menu_I_ProcessAnswers.Visible = false;
                ////    Menu_I_ManageFinalExamMarks.Visible = false;
                ////    Menu_I_AproveTestRemove.Visible = false;
                ////    Menu_H_Reports.Visible = false;

                ////}

            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        catch (Exception)
        {

            throw;
        }

    }

    private void FindUserNotification()
    {
        int TotalNoteCount = 0;

        string FromDate = null;
        string ToDate = null;
        FromDate = System.DateTime.Now.ToString("dd MMM yyyy");
        ToDate = System.DateTime.Now.ToString("dd MMM yyyy");

        DataSet dsUserNote = ProductController.GetTest_Notification(lblHeader_Company_Code.Text, lblHeader_User_Code.Text, FromDate, ToDate, lblHeader_DBName.Text, "1");
        if (dsUserNote.Tables.Count > 0)
        {
            lblHeader_Notification_TodaysTest.Text = Convert.ToString(dsUserNote.Tables[0].Rows[0]["TestCount"]);

            if (Convert.ToInt32(lblHeader_Notification_TodaysTest.Text) > 0)
            {
                lnk_Notification_TodaysTest.Visible = true;
                TotalNoteCount = TotalNoteCount + 1;
            }
            else
            {
                lnk_Notification_TodaysTest.Visible = false;
            }

            lblHeader_Notification_Attendance.Text = Convert.ToString(dsUserNote.Tables[0].Rows[0]["TestAttendAuthorisationCount"]);
            if (Convert.ToInt32(lblHeader_Notification_Attendance.Text) > 0)
            {
                lnk_Notification_Attendance.Visible = true;
                TotalNoteCount = TotalNoteCount + 1;
            }
            else
            {
                lnk_Notification_Attendance.Visible = false;
            }

            lblHeader_Notification_Answersheet.Text = Convert.ToString(dsUserNote.Tables[0].Rows[0]["TestAttendAnswersheetCount"]);
            if (Convert.ToInt32(lblHeader_Notification_Answersheet.Text) > 0)
            {
                lnk_Notification_Answersheet.Visible = true;
                TotalNoteCount = TotalNoteCount + 1;
            }
            else
            {
                lnk_Notification_Answersheet.Visible = false;
            }

            lblHeader_Notification_Marks.Text = Convert.ToString(dsUserNote.Tables[0].Rows[0]["TestMarksAuthorisationCount"]);
            if (Convert.ToInt32(lblHeader_Notification_Marks.Text) > 0)
            {
                lnk_Notification_Marks.Visible = true;
                TotalNoteCount = TotalNoteCount + 1;
            }
            else
            {
                lnk_Notification_Marks.Visible = false;
            }

            lblHeader_Notification_Test_Cancellation_Authorisation.Text = Convert.ToString(dsUserNote.Tables[0].Rows[0]["TestCancelApprovalCount"]);
            if (Convert.ToInt32(lblHeader_Notification_Test_Cancellation_Authorisation.Text) > 0)
            {
                lnk_Notification_Cancel_Test.Visible = true;
                TotalNoteCount = TotalNoteCount + 1;
            }
            else
            {
                lnk_Notification_Cancel_Test.Visible = false;
            }


            lblHeader_Notification_ReTest.Text = Convert.ToString(dsUserNote.Tables[0].Rows[0]["ReTest_NotificationCount"]);
            if (Convert.ToInt32(lblHeader_Notification_ReTest.Text) > 0)
            {
                lblHeader_Notification_ReTest.Visible = true;
                TotalNoteCount = TotalNoteCount + 1;
            }
            else
            {
                lblHeader_Notification_ReTest.Visible = false;
            }

            lblHeader_Notification_Count.Text = Convert.ToString(TotalNoteCount);
            lblHeader_Notification_Count2.Text = Convert.ToString(TotalNoteCount);
            if (TotalNoteCount > 0)
            {
                lnk_Notification.Visible = true;
            }
            else
            {
                lnk_Notification.Visible = false;
            }
        }

    }

    private void FindUserMessages()
    {
        lnk_Message.Visible = false;
    }

   

    protected void BtnLogOut_Click(object sender, System.EventArgs e)
    {
        Response.Cookies["MyCookiesLoginInfo"].Expires.TimeOfDay.ToString();       
        Session.RemoveAll();
        Response.Redirect("Default.aspx", false);
    }
    public Menu()
    {
        Load += Page_Load;
        Init += Page_Init;
    }



    private void Generate_Menu()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        if (Request.Cookies["MyCookiesLoginInfo"] != null)
        {
            string defaultpage = cookie.Values["Default_page"];      
            
            string Userid = cookie.Values["UserID"];
            string lstr = "";
            lstr = Convert.ToString(("<ul class='nav nav-list'>"));
            //DataTable dt = client.GetMenuList("1", Userid, "");
            DataSet ds = ProductController.GetMenuList("1", Userid, "");
            //lstr += Convert.ToString(("<li> <a href=' " + defaultpage + "'><i class='icon-home'></i><span>Dashboard</span></a></li>"));
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                string Application_no = Convert.ToString(ds.Tables[0].Rows[i]["Application_No"]);
                if (Application_no == "DB03")
                {
                    lstr += Convert.ToString(("<li> <a href=' " + ds.Tables[0].Rows[i]["Menu_link"] + "' class='" + ds.Tables[0].Rows[i]["Toggle"] + "'><i class='" + ds.Tables[0].Rows[i]["Menu_CSS"] + "'></i><span>"));

                    //lstr += Convert.ToString(("<li> <a href=' " + ds.Tables[0].Rows[i]["Menu_link"] + "'><i class='" + ds.Tables[0].Rows[i]["Menu_CSS"] + "'></i><span>"));
                    //lstr += Convert.ToString(("<li class=''> <a href='#' class='dropdown-toggle'><i class='" + dt.Rows[i]["Menu_CSS"] + "'></i><span>"));
                    lstr += (Convert.ToString(ds.Tables[0].Rows[i]["Menu_Name"]));
                    //DataTable dt1 = client.GetMenuList("2", Userid, ds.Tables[0].Rows.[i]["Menu_Code"].ToString());
                    DataSet ds1 = ProductController.GetMenuList("2", Userid, ds.Tables[0].Rows[i]["Menu_Code"].ToString());
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        lstr += Convert.ToString(("</span><b class='arrow icon-angle-down'></b>"));
                        lstr += Convert.ToString(("</a><ul class='submenu'>"));
                        for (int j = 0; j <= ds1.Tables[0].Rows.Count - 1; j++)
                        {
                            lstr += Convert.ToString((((" <li><a href='") + ds1.Tables[0].Rows[j]["Menu_Link"] + "'><i></i>") + ds1.Tables[0].Rows[j]["Menu_Name"] + "</a>"));
                        }
                        lstr += Convert.ToString(("</ul></li>"));
                    }
                    lstr += Convert.ToString(("</span></a></li>"));
                    lblHeaderMenu.Text = lstr;
                }
            }
            lstr += Convert.ToString(("</ul>"));

        }

    }



    //private void Generate_Menu()
    //{
    //    HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
    //    Login_Service.LoginServiceSoapClient client = new Login_Service.LoginServiceSoapClient();
    //    if (Request.Cookies["MyCookiesLoginInfo"] != null)
    //    {
    //        //DataTable dtApplicatUrl = new DataTable();
    //        //dtApplicatUrl = client.GetApplication_Url();

    //        //lblPath1.Text = dtApplicatUrl.Rows[0]["Homepage_Path"].ToString();
    //        //lblPath2.Text = dtApplicatUrl.Rows[1]["Homepage_Path"].ToString();
    //        //lblPath3.Text = dtApplicatUrl.Rows[2]["Homepage_Path"].ToString();
    //        //lblPath4.Text = dtApplicatUrl.Rows[3]["Homepage_Path"].ToString();
    //        //lblPath5.Text = dtApplicatUrl.Rows[4]["Homepage_Path"].ToString();
    //        //lblPath6.Text = dtApplicatUrl.Rows[5]["Homepage_Path"].ToString();

    //        string Userid = cookie.Values["UserID"];
    //        string lstr = "";
    //        lstr = Convert.ToString(("<ul class='nav nav-list'>"));
    //        DataTable dt = client.GetMenuList("1", Userid, "");


    //        for (int i = 0; i <= dt.Rows.Count - 1; i++)
    //        {
    //            string Application_no = Convert.ToString(dt.Rows[i]["Application_No"]);
    //            if (Application_no == "DB03")
    //            {
    //                lstr += Convert.ToString(("<li> <a href=' " + dt.Rows[i]["Menu_link"] + "' class='dropdown-toggle'><i class='" + dt.Rows[i]["Menu_CSS"] + "'></i><span>"));
    //                //lstr += Convert.ToString(("<li class=''> <a href='#' class='dropdown-toggle'><i class='" + dt.Rows[i]["Menu_CSS"] + "'></i><span>"));
    //                lstr += (Convert.ToString(dt.Rows[i]["Menu_Name"]));
    //                DataTable dt1 = client.GetMenuList("2", Userid, dt.Rows[i]["Menu_Code"].ToString());
    //                if (dt1.Rows.Count > 0)
    //                {
    //                    lstr += Convert.ToString(("</span><b class='arrow icon-angle-down'></b>"));
    //                    lstr += Convert.ToString(("</a><ul class='submenu'>"));
    //                    for (int j = 0; j <= dt1.Rows.Count - 1; j++)
                        
    //                    {
    //                        lstr += Convert.ToString((((" <li><a href='") + dt1.Rows[j]["Menu_Link"] + "'><i></i>") + dt1.Rows[j]["Menu_Name"] + "</a>"));
    //                    }
    //                    lstr += Convert.ToString(("</ul></li>"));
    //                }
    //                lstr += Convert.ToString(("</span></a></li>"));
    //                lblHeaderMenu.Text = lstr;
    //            }
    //        }
    //        lstr += Convert.ToString(("</ul>"));

    //    }

    //}


    protected void btnShortCut_CMDM_Engine_ServerClick(object sender, System.EventArgs e)
    {
        string Path = lblPath1.Text.Trim();
        int lenPath = Path.Length;

        if (lenPath == 0)
        {
        }
        else
        {
            Response.Redirect(Path, false);
        }


    }

    protected void btnShortCut_Order_Engine_ServerClick(object sender, System.EventArgs e)
    {
        string Path = lblPath2.Text.Trim();
        int lenPath = Path.Length;

        if (lenPath == 0)
        {
        }
        else
        {
            Response.Redirect(Path, false);
        }
        //Response.Redirect(lblPath2.Text.Trim(), false);
    }

    protected void btnShortCut_Scheduling_Engine_ServerClick(object sender, System.EventArgs e)
    {
        string Path = lblPath3.Text.Trim();
        int lenPath = Path.Length;

        if (lenPath == 0)
        {
        }
        else
        {
            Response.Redirect(Path, false);
        }
        //Response.Redirect(lblPath3.Text.Trim(), false);
    }

    protected void btnShortCut_Test_Engine_ServerClick(object sender, System.EventArgs e)
    {
        string Path = lblPath4.Text.Trim();
        int lenPath = Path.Length;

        if (lenPath == 0)
        {
        }
        else
        {
            Response.Redirect(Path, false);
        }
        // Response.Redirect(lblPath4.Text.Trim(), false);
    }

    protected void btnShortCut_Messaging_Engine_ServerClick(object sender, System.EventArgs e)
    {
        string Path = lblPath5.Text.Trim();
        int lenPath = Path.Length;

        if (lenPath == 0)
        {
        }
        else
        {
            Response.Redirect(Path, false);
        }
        //Response.Redirect(lblPath5.Text.Trim(), false);
    }

    protected void btnShortCut_Engine_ServerClick(object sender, System.EventArgs e)
    {
        //Response.Redirect(lblPath5.Text.Trim(), false);
    }

}
