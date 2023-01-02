using System;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Globalization;
using LMSIntegration;
using System.Text.RegularExpressions;
using System.ComponentModel;

public partial class Get_Test_Details_LMS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //testdetails td = new testdetails { FromDate = "2016-10-01", ToDate = "2016-10-15" ,SPICODE="SPI20161000045248" };
        //client.BaseAddress = new Uri(DBConnection.connStringLMS);
        //var response = client.PutAsJsonAsync("UserTest/UserTestDetails", td).Result;
        //if (response.IsSuccessStatusCode)
        //{
        //    Console.Write("Success");
        //}
        //else
        //    Console.Write("Error");

        try
        {
            using (var client = new HttpClient())
            {
                string FromDate = "2016-06-01";
                string ToDate = "2016-10-15";
                string SPICode = "SPI20161000045248";
                string data = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(DBConnection.connStringLMS + "UserTest/UserTestDetails?StartDate=" + FromDate + "&EndDate=" + ToDate + "&SPICode=" + SPICode);
                request.Method = "GET";
                request.ContentType = "application/json; charset=utf-8";
                request.ContentLength = data.Length;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();


                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                //var strResponse = JsonConvert.SerializeObject(reader.ReadToEnd());
                // var a= JsonConvert.DeserializeObject(strResponse);

                //var v = "[{\"TestAssignStartDate\":\"2016-10-14T05:00:00\",\"TestAssignEndDate\":\"2016-10-21T23:30:00\",\"TestName\":\"Scalars_And_Vectors-EOC-Objective-Set-I\",\"TestId\":60820,\"TotalQuestion\":50,\"Score\":10.000,\"Outof\":50.000,\"CenterCode\":\"E026\",\"BatchCode\":\"B10001\",\"ProductCode\":\"LMSPM0000102\",\"SkipQuestionCount\":31,\"RightAnswerCount\":10,\"IncorrectAnswerCount\":9,\"TestCompletedDate\":\"2016-10-14T07:38:43\",\"CenterName\":\"VASAI-WEST\",\"BatchName\":\"VSPC1\",\"ProductName\":\"CR-XI-MH-BOARD-ENTRANCE-16-17\",\"ExamMode\":\"Online\",\"ExamType\":\"Objective\",\"Attendance\":\"Present\",\"Subjects\":\" Physics\",\"Syllabus\":\" Scalars And Vectors\",\"Correct\":\"11,13,15,25,26,27,30,5,6,9\",\"InCorrect\":\"12,14,17,19,28,29,3,4,7\",\"UnAnswered\":\"1,10,16,18,2,20,21,22,23,24,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,8\",\"OverAllRank\":16,\"CenterRank\":1},{\"TestAssignStartDate\":\"2016-10-14T05:00:00\",\"TestAssignEndDate\":\"2016-10-21T23:30:00\",\"TestName\":\"Logarithm-EOC-Objective-Set-I\",\"TestId\":60903,\"TotalQuestion\":25,\"Score\":null,\"Outof\":50.000,\"CenterCode\":\"E026\",\"BatchCode\":\"B10001\",\"ProductCode\":\"LMSPM0000102\",\"SkipQuestionCount\":null,\"RightAnswerCount\":null,\"IncorrectAnswerCount\":null,\"TestCompletedDate\":null,\"CenterName\":\"VASAI-WEST\",\"BatchName\":\"VSPC1\",\"ProductName\":\"CR-XI-MH-BOARD-ENTRANCE-16-17\",\"ExamMode\":\"Online\",\"ExamType\":\"Objective\",\"Attendance\":\"Absent\",\"Subjects\":\" Maths\",\"Syllabus\":\" Logarithm\",\"Correct\":\"\",\"InCorrect\":\"\",\"UnAnswered\":\"\",\"OverAllRank\":null,\"CenterRank\":null},{\"TestAssignStartDate\":\"2016-10-14T05:00:00\",\"TestAssignEndDate\":\"2016-10-21T23:30:00\",\"TestName\":\"Basic_Prin_and_Tech_in_Organic_Chemi-Part - II-EOC-Objective-Set-I\",\"TestId\":62063,\"TotalQuestion\":50,\"Score\":29.000,\"Outof\":50.000,\"CenterCode\":\"E026\",\"BatchCode\":\"B10001\",\"ProductCode\":\"LMSPM0000102\",\"SkipQuestionCount\":0,\"RightAnswerCount\":29,\"IncorrectAnswerCount\":21,\"TestCompletedDate\":\"2016-10-14T08:07:54\",\"CenterName\":\"VASAI-WEST\",\"BatchName\":\"VSPC1\",\"ProductName\":\"CR-XI-MH-BOARD-ENTRANCE-16-17\",\"ExamMode\":\"Online\",\"ExamType\":\"Objective\",\"Attendance\":\"Present\",\"Subjects\":\" Chemistry\",\"Syllabus\":\" Basic Principle and Techniques in Organic Chemistry - Part - II\",\"Correct\":\"10,11,14,15,17,18,19,21,22,24,26,27,28,29,3,30,32,34,35,37,4,40,41,42,45,46,49,5,50\",\"InCorrect\":\"1,12,13,16,2,20,23,25,31,33,36,38,39,43,44,47,48,6,7,8,9\",\"UnAnswered\":\"\",\"OverAllRank\":3,\"CenterRank\":1},{\"TestAssignStartDate\":\"2016-10-14T05:00:00\",\"TestAssignEndDate\":\"2016-10-21T23:30:00\",\"TestName\":\"Diversity_in_Organisms-EOC-objective-set-I\",\"TestId\":62165,\"TotalQuestion\":45,\"Score\":null,\"Outof\":45.000,\"CenterCode\":\"E026\",\"BatchCode\":\"B10001\",\"ProductCode\":\"LMSPM0000102\",\"SkipQuestionCount\":null,\"RightAnswerCount\":null,\"IncorrectAnswerCount\":null,\"TestCompletedDate\":null,\"CenterName\":\"VASAI-WEST\",\"BatchName\":\"VSPC1\",\"ProductName\":\"CR-XI-MH-BOARD-ENTRANCE-16-17\",\"ExamMode\":\"Online\",\"ExamType\":\"Objective\",\"Attendance\":\"Absent\",\"Subjects\":\" Biology\",\"Syllabus\":\" Diversity in Organisms\",\"Correct\":\"\",\"InCorrect\":\"\",\"UnAnswered\":\"\",\"OverAllRank\":null,\"CenterRank\":null},{\"TestAssignStartDate\":\"2016-10-12T05:00:00\",\"TestAssignEndDate\":\"2016-10-19T23:30:00\",\"TestName\":\"Units_And_Dimensions-EOC-Objective-Set-I\",\"TestId\":60783,\"TotalQuestion\":50,\"Score\":3.000,\"Outof\":50.000,\"CenterCode\":\"E026\",\"BatchCode\":\"B10001\",\"ProductCode\":\"LMSPM0000102\",\"SkipQuestionCount\":38,\"RightAnswerCount\":3,\"IncorrectAnswerCount\":9,\"TestCompletedDate\":\"2016-10-14T08:35:11\",\"CenterName\":\"VASAI-WEST\",\"BatchName\":\"VSPC1\",\"ProductName\":\"CR-XI-MH-BOARD-ENTRANCE-16-17\",\"ExamMode\":\"Online\",\"ExamType\":\"Objective\",\"Attendance\":\"Present\",\"Subjects\":\" Physics\",\"Syllabus\":\" Units And Dimensions\",\"Correct\":\"12,5,9\",\"InCorrect\":\"1,10,11,2,3,4,6,7,8\",\"UnAnswered\":\"13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50\",\"OverAllRank\":35,\"CenterRank\":2},{\"TestAssignStartDate\":\"2016-10-12T05:00:00\",\"TestAssignEndDate\":\"2016-10-19T23:30:00\",\"TestName\":\"Angle_and_its_Measurements-EOC-Objective-Set-I\",\"TestId\":60818,\"TotalQuestion\":25,\"Score\":null,\"Outof\":25.000,\"CenterCode\":\"E026\",\"BatchCode\":\"B10001\",\"ProductCode\":\"LMSPM0000102\",\"SkipQuestionCount\":null,\"RightAnswerCount\":null,\"IncorrectAnswerCount\":null,\"TestCompletedDate\":null,\"CenterName\":\"VASAI-WEST\",\"BatchName\":\"VSPC1\",\"ProductName\":\"CR-XI-MH-BOARD-ENTRANCE-16-17\",\"ExamMode\":\"Online\",\"ExamType\":\"Objective\",\"Attendance\":\"Absent\",\"Subjects\":\" Maths\",\"Syllabus\":\" Angle and its Measurements\",\"Correct\":\"\",\"InCorrect\":\"\",\"UnAnswered\":\"\",\"OverAllRank\":null,\"CenterRank\":null},{\"TestAssignStartDate\":\"2016-10-12T05:00:00\",\"TestAssignEndDate\":\"2016-10-19T23:30:00\",\"TestName\":\"States_of_Matter-EOC-objective-set-I\",\"TestId\":61989,\"TotalQuestion\":50,\"Score\":null,\"Outof\":50.000,\"CenterCode\":\"E026\",\"BatchCode\":\"B10001\",\"ProductCode\":\"LMSPM0000102\",\"SkipQuestionCount\":null,\"RightAnswerCount\":null,\"IncorrectAnswerCount\":null,\"TestCompletedDate\":null,\"CenterName\":\"VASAI-WEST\",\"BatchName\":\"VSPC1\",\"ProductName\":\"CR-XI-MH-BOARD-ENTRANCE-16-17\",\"ExamMode\":\"Online\",\"ExamType\":\"Objective\",\"Attendance\":\"Absent\",\"Subjects\":\" Chemistry\",\"Syllabus\":\" States of Matter\",\"Correct\":\"\",\"InCorrect\":\"\",\"UnAnswered\":\"\",\"OverAllRank\":31,\"CenterRank\":2},{\"TestAssignStartDate\":\"2016-10-12T05:00:00\",\"TestAssignEndDate\":\"2016-10-19T23:30:00\",\"TestName\":\"States_of_Matter-EOC-objective-set-I\",\"TestId\":61989,\"TotalQuestion\":50,\"Score\":null,\"Outof\":50.000,\"CenterCode\":\"E026\",\"BatchCode\":\"B10006\",\"ProductCode\":\"LMSPM0000102\",\"SkipQuestionCount\":null,\"RightAnswerCount\":null,\"IncorrectAnswerCount\":null,\"TestCompletedDate\":null,\"CenterName\":\"VASAI-WEST\",\"BatchName\":\"VSM4\",\"ProductName\":\"CR-XI-MH-BOARD-ENTRANCE-16-17\",\"ExamMode\":\"Online\",\"ExamType\":\"Objective\",\"Attendance\":\"Absent\",\"Subjects\":\" Chemistry\",\"Syllabus\":\" States of Matter\",\"Correct\":\"\",\"InCorrect\":\"\",\"UnAnswered\":\"\",\"OverAllRank\":31,\"CenterRank\":2},{\"TestAssignStartDate\":\"2016-10-12T05:00:00\",\"TestAssignEndDate\":\"2016-10-19T23:30:00\",\"TestName\":\"States_of_Matter-EOC-objective-set-I\",\"TestId\":61989,\"TotalQuestion\":50,\"Score\":2.000,\"Outof\":50.000,\"CenterCode\":\"E026\",\"BatchCode\":\"B10010\",\"ProductCode\":\"LMSPM0000102\",\"SkipQuestionCount\":48,\"RightAnswerCount\":2,\"IncorrectAnswerCount\":0,\"TestCompletedDate\":\"2016-10-13T09:00:51\",\"CenterName\":\"VASAI-WEST\",\"BatchName\":\"XI-MH-BOARD-1617-VAS-010\",\"ProductName\":\"CR-XI-MH-BOARD-ENTRANCE-16-17\",\"ExamMode\":\"Online\",\"ExamType\":\"Objective\",\"Attendance\":\"Present\",\"Subjects\":\" Chemistry\",\"Syllabus\":\" States of Matter\",\"Correct\":\"2,3\",\"InCorrect\":\"\",\"UnAnswered\":\"1,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,4,40,41,42,43,44,45,46,47,48,49,5,50,6,7,8,9\",\"OverAllRank\":31,\"CenterRank\":2},{\"TestAssignStartDate\":\"2016-10-12T05:00:00\",\"TestAssignEndDate\":\"2016-10-19T23:30:00\",\"TestName\":\"Cell_Division-EOC-objective-set-I\",\"TestId\":62212,\"TotalQuestion\":45,\"Score\":null,\"Outof\":45.000,\"CenterCode\":\"E026\",\"BatchCode\":\"B10001\",\"ProductCode\":\"LMSPM0000102\",\"SkipQuestionCount\":null,\"RightAnswerCount\":null,\"IncorrectAnswerCount\":null,\"TestCompletedDate\":null,\"CenterName\":\"VASAI-WEST\",\"BatchName\":\"VSPC1\",\"ProductName\":\"CR-XI-MH-BOARD-ENTRANCE-16-17\",\"ExamMode\":\"Online\",\"ExamType\":\"Objective\",\"Attendance\":\"Absent\",\"Subjects\":\" Biology\",\"Syllabus\":\" Cell Division\",\"Correct\":\"\",\"InCorrect\":\"\",\"UnAnswered\":\"\",\"OverAllRank\":null,\"CenterRank\":null}]";

                DataTable dt = (DataTable)JsonConvert.DeserializeObject(reader.ReadToEnd(), (typeof(DataTable)));

                //dlGridDisplay.DataSource = dt;
                //dlGridDisplay.DataBind();
                //string  XMLData = "<StudentTestDetails>";
                //foreach (DataListItem dtlItem in dlGridDisplay.Items)
                //{
                //    Label TestAssignStartDate = (Label)dtlItem.FindControl("TestAssignStartDate");
                //    Label TestAssignEndDate = (Label)dtlItem.FindControl("TestAssignEndDate");
                //    Label TestName = (Label)dtlItem.FindControl("TestName");
                //    Label TestId = (Label)dtlItem.FindControl("TestId");
                //    Label TotalQuestion = (Label)dtlItem.FindControl("TotalQuestion");
                //    Label Score = (Label)dtlItem.FindControl("Score");
                //    Label OutOf = (Label)dtlItem.FindControl("OutOf");
                //    Label CenterCode = (Label)dtlItem.FindControl("CenterCode");
                //    Label BatchCode = (Label)dtlItem.FindControl("BatchCode");
                //    Label ProductCode = (Label)dtlItem.FindControl("ProductCode");

                //    Label SkipQuestionCount = (Label)dtlItem.FindControl("SkipQuestionCount");
                //    Label RightAnswerCount = (Label)dtlItem.FindControl("RightAnswerCount");
                //    Label InCorrectAnswerCount = (Label)dtlItem.FindControl("InCorrectAnswerCount");
                //    Label TestCompletedDate = (Label)dtlItem.FindControl("TestCompletedDate");
                //    Label CenterName = (Label)dtlItem.FindControl("CenterName");
                //    Label BatchName = (Label)dtlItem.FindControl("BatchName");
                //    Label ProductName = (Label)dtlItem.FindControl("ProductName");
                //    Label ExamMode = (Label)dtlItem.FindControl("ExamMode");
                //    Label ExamType = (Label)dtlItem.FindControl("ExamType");
                //    Label Attendance = (Label)dtlItem.FindControl("Attendance");

                //    Label Subjects = (Label)dtlItem.FindControl("Subjects");
                //    Label Syllabus = (Label)dtlItem.FindControl("Syllabus");
                //    Label Correct = (Label)dtlItem.FindControl("Correct");
                //    Label InCorrect = (Label)dtlItem.FindControl("InCorrect");
                //    Label UnAnswered = (Label)dtlItem.FindControl("UnAnswered");
                //    Label OverAllRank = (Label)dtlItem.FindControl("OverAllRank");
                //    Label CenterRank = (Label)dtlItem.FindControl("CenterRank");

                //    XMLData = XMLData + "<TestDetails><SPID>" + SPICode + "</SPID><TestAssignStartDate>" + TestAssignStartDate.Text.Trim() + "</TestAssignStartDate><TestAssignEndDate>" + TestAssignEndDate.Text.Trim() +
                //        "</TestAssignEndDate><TestName>" + TestName.Text.Trim() + "</TestName><TestId>" + TestId.Text.Trim() +
                //        "</TestId><TotalQuestion>" + TotalQuestion.Text.Trim() + "</TotalQuestion><Score>" + Score.Text.Trim() + "</Score><OutOf>" + OutOf.Text.Trim() +
                //        "</OutOf><CenterCode>" + CenterCode.Text.Trim() + "</CenterCode><BatchCode>" + BatchCode.Text.Trim() + "</BatchCode><ProductCode>" + ProductCode.Text.Trim() +
                //        "</ProductCode><SkipQuestionCount>" + SkipQuestionCount.Text.Trim() + "</SkipQuestionCount><RightAnswerCount>" + RightAnswerCount.Text.Trim() +
                //        "</RightAnswerCount><InCorrectAnswerCount>" + InCorrectAnswerCount.Text.Trim() + "</InCorrectAnswerCount><TestCompletedDate>" + TestCompletedDate.Text.Trim() +
                //        "</TestCompletedDate><CenterName>" + CenterName.Text.Trim() + "</CenterName><BatchName>" + BatchName.Text.Trim() + "</BatchName><ProductName>"+
                //        ProductName.Text.Trim()+"</ProductName><ExamMode>"+ExamMode.Text.Trim()+"</ExamMode><ExamType>"+ExamType.Text.Trim()+"</ExamType><Attendance>"+
                //        Attendance.Text.Trim()+"</Attendance><Subjects>"+Subjects.Text.Trim()+"</Subjects><Syllabus>"+Syllabus.Text.Trim()+"</Syllabus><Correct>"+
                //        Correct.Text.Trim()+"</Correct><InCorrect>"+InCorrect.Text.Trim()+"</InCorrect><UnAnswered>"+UnAnswered.Text.Trim()+"</UnAnswered><OverAllRank>"+
                //        OverAllRank.Text.Trim() + "</OverAllRank><CenterRank>" + CenterRank.Text.Trim() + "</CenterRank></TestDetails>";  





                //}



                if (dt.Rows.Count > 0)
                {
                    string XMLData = "<StudentTestDetails>";
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        //DateTime TestAssignStartDate = Convert.ToDateTime(null);
                        //DateTime TestAssignEndDate = Convert.ToDateTime(null);
                        //string TestName=null;
                        //string TestId = null;
                        //int TotalQuestion =Convert.ToInt32(null);
                        //int Score = Convert.ToInt32(null);
                        //int OutOf = Convert.ToInt32(null);
                        //string CenterCode = null;
                        //string BatchCode = null;
                        //string ProductCode = null;
                        //int SkipQuestionCount = Convert.ToInt32(null);
                        //int RightAnswerCount = Convert.ToInt32(null);
                        //int InCorrectAnswerCount = Convert.ToInt32(null);
                        //DateTime TestCompletedDate = Convert.ToDateTime(null);
                        //string CenterName = null;
                        //string BatchName = null;
                        //string ProductName = null;
                        //string ExamMode = null;
                        //string ExamType = null;
                        //string Attendance = null;
                        //string Subjects = null;
                        //string Syllabus = null;
                        //string Correct = null;
                        //string InCorrect = null;
                        //string UnAnswered = null;
                        //int OverAllRank = Convert.ToInt32(null);
                        //int CenterRank = Convert.ToInt32(null);


                        string TestAssignStartDate = dt.Rows[i]["TestAssignStartDate"].ToString();
                        string TestAssignEndDate = dt.Rows[i]["TestAssignEndDate"].ToString();
                        string TestName = dt.Rows[i]["TestName"].ToString();
                        string TestId = dt.Rows[i]["TestId"].ToString();
                        string TotalQuestion = dt.Rows[i]["TotalQuestion"].ToString();
                        string Score = dt.Rows[i]["Score"].ToString();
                        string OutOf = dt.Rows[i]["OutOf"].ToString();
                        string CenterCode = dt.Rows[i]["CenterCode"].ToString();
                        string BatchCode = dt.Rows[i]["BatchCode"].ToString();
                        string ProductCode = dt.Rows[i]["ProductCode"].ToString();
                        string SkipQuestionCount = dt.Rows[i]["SkipQuestionCount"].ToString();
                        string RightAnswerCount = dt.Rows[i]["RightAnswerCount"].ToString();
                        string InCorrectAnswerCount = dt.Rows[i]["InCorrectAnswerCount"].ToString();
                        string TestCompletedDate = dt.Rows[i]["TestCompletedDate"].ToString();
                        string CenterName = dt.Rows[i]["CenterName"].ToString();
                        string BatchName = dt.Rows[i]["BatchName"].ToString();
                        string ProductName = dt.Rows[i]["ProductName"].ToString();
                        string ExamMode = dt.Rows[i]["ExamMode"].ToString();
                        string ExamType = dt.Rows[i]["ExamType"].ToString();
                        string Attendance = dt.Rows[i]["Attendance"].ToString();
                        string Subjects = dt.Rows[i]["Subjects"].ToString();
                        string Syllabus = dt.Rows[i]["Syllabus"].ToString();
                        string Correct = dt.Rows[i]["Correct"].ToString();
                        string InCorrect = dt.Rows[i]["InCorrect"].ToString();
                        string UnAnswered = dt.Rows[i]["UnAnswered"].ToString();
                        string OverAllRank = dt.Rows[i]["OverAllRank"].ToString();
                        string CenterRank = dt.Rows[i]["CenterRank"].ToString();

                        XMLData = XMLData + "<TestDetails><SPID>" + SPICode + "</SPID><TestAssignStartDate>" + TestAssignStartDate + "</TestAssignStartDate><TestAssignEndDate>" + TestAssignEndDate +
                            "</TestAssignEndDate><TestName>" + TestName + "</TestName><TestId>" + TestId +
                            "</TestId><TotalQuestion>" + TotalQuestion + "</TotalQuestion><Score>" + Score + "</Score><OutOf>" + OutOf +
                            "</OutOf><CenterCode>" + CenterCode + "</CenterCode><BatchCode>" + BatchCode + "</BatchCode><ProductCode>" + ProductCode +
                            "</ProductCode><SkipQuestionCount>" + SkipQuestionCount + "</SkipQuestionCount><RightAnswerCount>" + RightAnswerCount +
                            "</RightAnswerCount><InCorrectAnswerCount>" + InCorrectAnswerCount + "</InCorrectAnswerCount><TestCompletedDate>" + TestCompletedDate +
                            "</TestCompletedDate><CenterName>" + CenterName + "</CenterName><BatchName>" + BatchName + "</BatchName><ProductName>" +
                            ProductName + "</ProductName><ExamMode>" + ExamMode + "</ExamMode><ExamType>" + ExamType + "</ExamType><Attendance>" +
                            Attendance + "</Attendance><Subjects>" + Subjects + "</Subjects><Syllabus>" + Syllabus + "</Syllabus><Correct>" +
                            Correct + "</Correct><InCorrect>" + InCorrect + "</InCorrect><UnAnswered>" + UnAnswered + "</UnAnswered><OverAllRank>" +
                            OverAllRank + "</OverAllRank><CenterRank>" + CenterRank + "</CenterRank></TestDetails>";



                        //DataSet ds = ProductController.Insert_Student_Test_Details_LMS(SPICode, TestAssignStartDate, TestAssignEndDate, TestName, TestId, TotalQuestion, Score, OutOf,
                        //    CenterCode, BatchCode, ProductCode, SkipQuestionCount, RightAnswerCount, InCorrectAnswerCount, TestCompletedDate, CenterName, BatchName, ProductName, ExamMode,
                        //    ExamType, Attendance, Subjects, Syllabus, Correct, InCorrect, UnAnswered, OverAllRank, CenterRank, Flag
                        //    );
                    }
                    DataSet ds = new DataSet();
                    XMLData = XMLData + "</StudentTestDetails>";
                    ds = ProductController.Insert_Student_Test_Details_LMS_NEW(XMLData, 1);


                }
            }
        }
        catch (Exception ex)
        {
        }
    }




}