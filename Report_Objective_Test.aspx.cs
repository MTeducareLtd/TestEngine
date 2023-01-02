using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web;
using System.Linq;
using System.Globalization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Net.Mail;
using System.Net;





public partial class Report_Objective_Test : System.Web.UI.Page
{
    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {

        try
        {

        
        
        
        //Validate if all information is entered correctly
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivision.Focus();
            return;
        }

        if (ddlAcadYear.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0002");
            ddlAcadYear.Focus();
            return;
        }

        if (ddlCentre.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0006");
            ddlCentre.Focus();
            return;
        }

        if (ddlStandard.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0003");
            ddlStandard.Focus();
            return;
        }
        if (ddlBatch.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Batch");
            ddlBatch.Focus();
            return;
        }
        if (ddlConductNo.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Conduct No");
            ddlConductNo.Focus();
            return;
        }



        if (ddlStudentName.SelectedIndex == 0 | ddlStudentName.Items.Count == 0)
        {
            Show_Error_Success_Box("E", "0030");
            ddlStudentName.Focus();
            return;
        }

        if (ddlTestName.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Test Name");
            ddlTestName.Focus();
            return;
        }
        string BatchCode = "";
        int BatchCnt = 0;
        int BatchSelCnt = 0;
     
        

        for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
        {
            if (ddlBatch.Items[BatchCnt].Selected == true)
            {
                BatchSelCnt = BatchSelCnt + 1;
            }
        }

        if (BatchSelCnt == 0)
        {
            //When all is selected
            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
            }
            
            BatchCode = Common.RemoveComma(BatchCode);
        }
        else
        {
            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                if (ddlBatch.Items[BatchCnt].Selected == true)
                {
                    BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
                }
            }

            BatchCode = Common.RemoveComma(BatchCode);
        }


        string TestType_ID = "";
        string TestType_Name = "";
        int TypeCnt = 0;
        int TypeSelCnt = 0;
        for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
        {
            if (ddlTestType.Items[TypeCnt].Selected == true)
            {
                TypeSelCnt = TypeSelCnt + 1;
            }
        }

        if (TypeSelCnt == 0)
        {
            //When all is selected
            for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
            {
                TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
                TestType_Name = TestType_Name + ddlTestType.Items[TypeCnt].ToString() + ",";
            }
            TestType_ID = Common.RemoveComma(TestType_ID);
            TestType_Name = Common.RemoveComma(TestType_Name);
           
        }
        else
        {
            for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
            {
                if (ddlTestType.Items[TypeCnt].Selected == true)
                {
                    TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
                    TestType_Name = TestType_Name + ddlTestType.Items[TypeCnt].ToString() + ",";
                }
            }
            TestType_ID = Common.RemoveComma(TestType_ID);
            TestType_Name = Common.RemoveComma(TestType_Name);
            
        }


        
        ControlVisibility("Result");

        string SBEntryCode = null;
        SBEntryCode = ddlStudentName.SelectedValue.ToString();

       // lblTestID_Result.Text = Test_ID;


        string TestKey = "";
        string OtherKey = "";

        TestKey = ddlTestName.SelectedValue;
        OtherKey = ddlCentre.SelectedValue + "%" + ddlBatch.SelectedValue + "%" + ddlConductNo.SelectedValue;

        //For MCQ Type test

        DataSet dsGrid = new DataSet();

        dsGrid = Bind_Data();

       // DataSet dsGrid = ProductController.Report_Object_Test(TestKey, SBEntryCode, OtherKey);
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {
                if (dsGrid.Tables[0].Rows.Count != 0)
                {
                    dlGridDetailsofAnswering.DataSource = dsGrid.Tables[0];
                    dlGridDetailsofAnswering.DataBind();

                    lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
                    btnPrint.Visible = true;
                    btnEmail.Visible = true;
                }
                else
                {
                    dlGridDetailsofAnswering.DataSource = null;
                    dlGridDetailsofAnswering.DataBind();

                    lbltotalcount.Text = "0";
                    btnPrint.Visible = false;
                    btnEmail.Visible = false;
                }
            }
            else
            {
                dlGridDetailsofAnswering.DataSource = null;
                dlGridDetailsofAnswering.DataBind();
                lbltotalcount.Text = "0";
                btnPrint.Visible = false;
                btnEmail.Visible = false;
            }
        }
        else
        {
            dlGridDetailsofAnswering.DataSource = null;
            dlGridDetailsofAnswering.DataBind();
            lbltotalcount.Text = "0";
            btnPrint.Visible = false;
            btnEmail.Visible = false;
        }      
               


        lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
        lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
        lblStandard_Result.Text = ddlStandard.SelectedItem.ToString();
        lblCentre_Result.Text = ddlCentre.SelectedItem.ToString();
        lblStudentName_Result.Text = ddlStudentName.SelectedItem.ToString();
        lblRollNo_Result.Text = txtRollNo.Text;
        lblTestType_Result.Text = TestType_Name;
        lblTestName_Result.Text = ddlTestName.SelectedItem.Text;
        lblTestPeriod.Text = id_date_range_picker_1.Value.ToString();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }

    }



    private DataSet  Bind_Data()
    {

        DataSet dsGrid = new DataSet();

        try
        {

            //Validate if all information is entered correctly
            
            string BatchCode = "";
            int BatchCnt = 0;
            int BatchSelCnt = 0;




            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                if (ddlBatch.Items[BatchCnt].Selected == true)
                {
                    BatchSelCnt = BatchSelCnt + 1;
                }
            }

            if (BatchSelCnt == 0)
            {
                //When all is selected
                for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
                {
                    BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
                }

                BatchCode = Common.RemoveComma(BatchCode);
            }
            else
            {
                for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
                {
                    if (ddlBatch.Items[BatchCnt].Selected == true)
                    {
                        BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
                    }
                }

                BatchCode = Common.RemoveComma(BatchCode);
            }


            string TestType_ID = "";
            string TestType_Name = "";
            int TypeCnt = 0;
            int TypeSelCnt = 0;
            for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
            {
                if (ddlTestType.Items[TypeCnt].Selected == true)
                {
                    TypeSelCnt = TypeSelCnt + 1;
                }
            }

            if (TypeSelCnt == 0)
            {
                //When all is selected
                for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
                {
                    TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
                    TestType_Name = TestType_Name + ddlTestType.Items[TypeCnt].ToString() + ",";
                }
                TestType_ID = Common.RemoveComma(TestType_ID);
                TestType_Name = Common.RemoveComma(TestType_Name);

            }
            else
            {
                for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
                {
                    if (ddlTestType.Items[TypeCnt].Selected == true)
                    {
                        TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
                        TestType_Name = TestType_Name + ddlTestType.Items[TypeCnt].ToString() + ",";
                    }
                }
                TestType_ID = Common.RemoveComma(TestType_ID);
                TestType_Name = Common.RemoveComma(TestType_Name);

            }

                       

            string SBEntryCode = null;
            SBEntryCode = ddlStudentName.SelectedValue.ToString();
           

            string TestKey = "";
            string OtherKey = "";

            TestKey = ddlTestName.SelectedValue;
            OtherKey = ddlCentre.SelectedValue + "%" + ddlBatch.SelectedValue + "%" + ddlConductNo.SelectedValue;

            //For MCQ Type test

            dsGrid = ProductController.Report_Object_Test(TestKey, SBEntryCode, OtherKey);           

            
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            dsGrid = null;
            
        }
        return dsGrid;

    }

    private DataSet PrintBind_Data()
    {

        DataSet dsGrid = new DataSet();

        try
        {

            //Validate if all information is entered correctly

            string BatchCode = "";
            int BatchCnt = 0;
            int BatchSelCnt = 0;




            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                if (ddlBatch.Items[BatchCnt].Selected == true)
                {
                    BatchSelCnt = BatchSelCnt + 1;
                }
            }

            if (BatchSelCnt == 0)
            {
                //When all is selected
                for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
                {
                    BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
                }

                BatchCode = Common.RemoveComma(BatchCode);
            }
            else
            {
                for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
                {
                    if (ddlBatch.Items[BatchCnt].Selected == true)
                    {
                        BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
                    }
                }

                BatchCode = Common.RemoveComma(BatchCode);
            }


            string TestType_ID = "";
            string TestType_Name = "";
            int TypeCnt = 0;
            int TypeSelCnt = 0;
            for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
            {
                if (ddlTestType.Items[TypeCnt].Selected == true)
                {
                    TypeSelCnt = TypeSelCnt + 1;
                }
            }

            if (TypeSelCnt == 0)
            {
                //When all is selected
                for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
                {
                    TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
                    TestType_Name = TestType_Name + ddlTestType.Items[TypeCnt].ToString() + ",";
                }
                TestType_ID = Common.RemoveComma(TestType_ID);
                TestType_Name = Common.RemoveComma(TestType_Name);

            }
            else
            {
                for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
                {
                    if (ddlTestType.Items[TypeCnt].Selected == true)
                    {
                        TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
                        TestType_Name = TestType_Name + ddlTestType.Items[TypeCnt].ToString() + ",";
                    }
                }
                TestType_ID = Common.RemoveComma(TestType_ID);
                TestType_Name = Common.RemoveComma(TestType_Name);

            }



            string SBEntryCode = null;
            SBEntryCode = ddlStudentName.SelectedValue.ToString();


            string TestKey = "";
            string OtherKey = "";

            TestKey = ddlTestName.SelectedValue;
            OtherKey = ddlCentre.SelectedValue + "%" + ddlBatch.SelectedValue + "%" + ddlConductNo.SelectedValue;

            //For MCQ Type test

            dsGrid = ProductController.Report_Object_Test(TestKey, SBEntryCode, OtherKey);


        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            dsGrid = null;

        }
        return dsGrid;

    }

    private DataSet Bind_Email_Data(string SBEntryCode1)
    {

        DataSet dsGrid = new DataSet();

        try
        {

            //Validate if all information is entered correctly

            string BatchCode = "";
            int BatchCnt = 0;
            int BatchSelCnt = 0;




            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                if (ddlBatch.Items[BatchCnt].Selected == true)
                {
                    BatchSelCnt = BatchSelCnt + 1;
                }
            }

            if (BatchSelCnt == 0)
            {
                //When all is selected
                for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
                {
                    BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
                }

                BatchCode = Common.RemoveComma(BatchCode);
            }
            else
            {
                for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
                {
                    if (ddlBatch.Items[BatchCnt].Selected == true)
                    {
                        BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
                    }
                }

                BatchCode = Common.RemoveComma(BatchCode);
            }


            string TestType_ID = "";
            string TestType_Name = "";
            int TypeCnt = 0;
            int TypeSelCnt = 0;
            for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
            {
                if (ddlTestType.Items[TypeCnt].Selected == true)
                {
                    TypeSelCnt = TypeSelCnt + 1;
                }
            }

            if (TypeSelCnt == 0)
            {
                //When all is selected
                for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
                {
                    TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
                    TestType_Name = TestType_Name + ddlTestType.Items[TypeCnt].ToString() + ",";
                }
                TestType_ID = Common.RemoveComma(TestType_ID);
                TestType_Name = Common.RemoveComma(TestType_Name);

            }
            else
            {
                for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
                {
                    if (ddlTestType.Items[TypeCnt].Selected == true)
                    {
                        TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
                        TestType_Name = TestType_Name + ddlTestType.Items[TypeCnt].ToString() + ",";
                    }
                }
                TestType_ID = Common.RemoveComma(TestType_ID);
                TestType_Name = Common.RemoveComma(TestType_Name);

            }



            string SBEntryCode = null;
            SBEntryCode = SBEntryCode1;

            string TestKey = "";
            string OtherKey = "";

            TestKey = ddlTestName.SelectedValue;
            OtherKey = ddlCentre.SelectedValue + "%" + ddlBatch.SelectedValue + "%" + ddlConductNo.SelectedValue;

            //For MCQ Type test

            dsGrid = ProductController.Report_Object_Test(TestKey, SBEntryCode, OtherKey);


        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            dsGrid = null;

        }
        return dsGrid;

    }


    private void Print_Data()
    {
        try
        {

            DataSet dsPrintData = new DataSet();

            string BatchCode = "";
            int BatchCnt = 0;
            int BatchSelCnt = 0;

            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                if (ddlBatch.Items[BatchCnt].Selected == true)
                {
                    BatchSelCnt = BatchSelCnt + 1;
                }
            }

            if (BatchSelCnt == 0)
            {
                //When all is selected
                for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
                {
                    BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
                }

                BatchCode = Common.RemoveComma(BatchCode);
            }
            else
            {
                for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
                {
                    if (ddlBatch.Items[BatchCnt].Selected == true)
                    {
                        BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
                    }
                }

                BatchCode = Common.RemoveComma(BatchCode);
            }


            string TestType_ID = "";
            string TestType_Name = "";
            int TypeCnt = 0;
            int TypeSelCnt = 0;
            for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
            {
                if (ddlTestType.Items[TypeCnt].Selected == true)
                {
                    TypeSelCnt = TypeSelCnt + 1;
                }
            }

            if (TypeSelCnt == 0)
            {
                //When all is selected
                for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
                {
                    TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
                    TestType_Name = TestType_Name + ddlTestType.Items[TypeCnt].ToString() + ",";
                }
                TestType_ID = Common.RemoveComma(TestType_ID);
                TestType_Name = Common.RemoveComma(TestType_Name);

            }
            else
            {
                for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
                {
                    if (ddlTestType.Items[TypeCnt].Selected == true)
                    {
                        TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
                        TestType_Name = TestType_Name + ddlTestType.Items[TypeCnt].ToString() + ",";
                    }
                }
                TestType_ID = Common.RemoveComma(TestType_ID);
                TestType_Name = Common.RemoveComma(TestType_Name);

            }


            string SBEntryCode = "";

            foreach (DataListItem dtlItem in dlPrintStudent.Items)
            {
                CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
                if (chkStudent.Checked == true)
                {
                    Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
                    SBEntryCode = SBEntryCode + lblSBEntryCode.Text + ",";
                }
            }

            if (SBEntryCode == "")
            {
                return;
            }
           // SBEntryCode = ddlStudentName.SelectedValue.ToString();


            string TestKey = "";
            string OtherKey = "";

            TestKey = ddlTestName.SelectedValue;
            OtherKey = ddlCentre.SelectedValue + "%" + ddlBatch.SelectedValue + "%" + ddlConductNo.SelectedValue;

            //For MCQ Type test

            

                        dynamic document = new Document(PageSize.A4, 50, 50, 25, 25);

                        // Create a new PdfWriter object, specifying the output stream
                        dynamic output = new MemoryStream();
                        dynamic writer = PdfWriter.GetInstance(document, output);



                        dynamic boldTableFont = FontFactory.GetFont("Arial", 10, Font.BOLD);
                        dynamic endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
                        dynamic bodyFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);

                        // Open the Document for writing
                        document.Open();

                        foreach (DataListItem dtlItem in dlPrintStudent.Items)
                        {
                            CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
                            if (chkStudent.Checked == true)
                            {
                                Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
                                SBEntryCode = lblSBEntryCode.Text;
                            
                        dsPrintData = ProductController.Report_Object_Test(TestKey, SBEntryCode, OtherKey);


                        float YPos = 0;
                        YPos = 800;

                        BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        Color Cl = Color.CYAN;
                        



                        PdfContentByte cb = writer.DirectContent;

                        cb.BeginText();
                        cb.SetTextMatrix(220, 820);
                        cb.SetFontAndSize(bf, 14);


                        cb.ShowText("MT Educare Ltd.");
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);


                        cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                        cb.SetLineWidth(0.5f);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                        cb.EndText();


                        cb.MoveTo(20, YPos + 15);
                        cb.LineTo(580, YPos + 15);
                        cb.Stroke();

                        cb.BeginText();

                        cb.SetTextMatrix(20, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Roll No:");


                        cb.SetTextMatrix(80, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                        cb.ShowText(lblRollNo_Result.Text);




                        cb.SetTextMatrix(130, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Student Name :");


                        cb.SetTextMatrix(200, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                        cb.ShowText(lblStudentName_Result.Text);


                        cb.SetTextMatrix(450, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Center :");


                        cb.SetTextMatrix(500, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                        cb.ShowText(lblCentre_Result.Text);


                        cb.SetTextMatrix(20, YPos - 15);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Test Name:");


                        cb.SetTextMatrix(80, YPos - 15);
                        cb.SetFontAndSize(bf, 10);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                        cb.ShowText(lblTestName_Result.Text);


                        cb.EndText();

                        DataView dvfilter = new DataView(dsPrintData.Tables[0]);
                        DataTable dtfilter = new DataTable();
                        float Yaxis = YPos;

                        if (dsPrintData.Tables[1].Rows.Count != 0)
                        {
                            foreach (DataRow drSubject in dsPrintData.Tables[1].Rows)
                            {


                                dtfilter = new DataTable();
                                dvfilter.RowFilter = string.Empty;
                                string subject_Code = "";
                                string subject_Name = "";
                                subject_Code = drSubject["Subject_Code"].ToString();
                                subject_Name = drSubject["Subject_Name"].ToString();

                                dvfilter.RowFilter = "Subject_Code = '" + subject_Code + "'";
                                dtfilter = dvfilter.ToTable();


                                int datarowcount = dtfilter.Rows.Count;
                                float finalyaxis = 0;
                                finalyaxis = (Yaxis - 30 - (datarowcount / 4));

                                if (finalyaxis < 20)
                                {
                                    document.NewPage();
                                    Yaxis = 800;
                                    bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                                    cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                    cb.SetLineWidth(0.5f);
                                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                                }



                                Yaxis = Yaxis - 5;

                                cb.BeginText();
                                cb.SetTextMatrix(20, Yaxis - 40);
                                cb.SetFontAndSize(bf, 10);
                                cb.ShowText(subject_Name.ToUpper());

                                cb.EndText();


                                //Start Header

                                cb.SetLineWidth(1f);
                                cb.MoveTo(20, Yaxis - 50);
                                cb.LineTo(580, Yaxis - 50);
                                cb.Stroke();


                                cb.MoveTo(20, Yaxis - 70);
                                cb.LineTo(580, Yaxis - 70);
                                cb.Stroke();


                                cb.MoveTo(20, Yaxis - 50);
                                cb.LineTo(20, Yaxis - 70);
                                cb.Stroke();


                                cb.SetLineWidth(0.5f);

                                string col1 = "NO.";
                                string col2 = "KEY";
                                string col3 = "ANS";
                                string col4 = "DL";
                                string col5 = "Score";
                                List<string> objCollist = new List<string>();

                                for (int i = 0; i < 4; i++)
                                {
                                    objCollist.Add(col1);
                                    objCollist.Add(col2);
                                    objCollist.Add(col3);
                                    objCollist.Add(col4);
                                    objCollist.Add(col5);
                                }



                                int x = 0;
                                for (int i = 0; i < 20; i++)
                                {

                                    if (i == 0)
                                    {

                                        cb.BeginText();
                                        cb.SetTextMatrix((x + (((x + 70) - x) / 2) - (cb.GetEffectiveStringWidth(objCollist[i].ToString(), false) / 2)), Yaxis - 62);
                                        cb.SetFontAndSize(bf, 8);
                                        cb.SetColorFill(Color.RED);
                                        cb.ShowText(objCollist[i].ToString());
                                       
                                        cb.EndText();
                                        

                                        x = 49;

                                        cb.MoveTo(x, Yaxis - 50);
                                        cb.LineTo(x, Yaxis - 70);
                                        cb.Stroke();
                                    }
                                    else if (i == 19)
                                    {
                                        cb.BeginText();
                                        cb.SetTextMatrix((x + (((x + 28) - x) / 2) - (cb.GetEffectiveStringWidth(objCollist[i].ToString(), false) / 2)), Yaxis - 62);
                                        cb.SetFontAndSize(bf, 8);
                                        cb.ShowText(objCollist[i].ToString());
                                        cb.EndText();


                                        cb.SetLineWidth(1f);
                                        cb.MoveTo(580, Yaxis - 50);
                                        cb.LineTo(580, Yaxis - 70);
                                        cb.Stroke();
                                        cb.SetLineWidth(0.5f);
                                    }
                                    else
                                    {

                                        cb.BeginText();
                                        cb.SetTextMatrix((x + (((x + 28) - x) / 2) - (cb.GetEffectiveStringWidth(objCollist[i].ToString(), false) / 2)), Yaxis - 62);
                                        cb.SetFontAndSize(bf, 8);
                                        cb.ShowText(objCollist[i].ToString());
                                        cb.EndText();

                                        x = x + 28;

                                        if (i == 4 || i == 9 || i == 14)
                                        {
                                            cb.SetLineWidth(1f);
                                        }


                                        cb.MoveTo(x, Yaxis - 50);
                                        cb.LineTo(x, Yaxis - 70);
                                        cb.Stroke();
                                        cb.SetLineWidth(0.5f);
                                    }

                                }

                                //End Header


                                int dataCount = 1;

                                Yaxis = Yaxis - 70;
                                x = 20;

                                int totalcount = 0;

                                foreach (DataRow drf in dtfilter.Rows)
                                {


                                    totalcount++;



                                    cb.BeginText();
                                    
                                    cb.SetTextMatrix((x + (((x + 29) - x) / 2) - (cb.GetEffectiveStringWidth(drf["No"].ToString(), false) / 2)), Yaxis - 10);
                                    cb.SetFontAndSize(bf, 8);
                                    cb.SetColorFill(Color.RED);
                                    cb.ShowText(drf["No"].ToString());
                                    cb.EndText();
                                    

                                    cb.SetLineWidth(1f);

                                    cb.MoveTo(x, Yaxis);
                                    cb.LineTo(x, Yaxis - 15);
                                    cb.Stroke();

                                    cb.SetLineWidth(0.5f);
                                    if (dataCount == 1)
                                    {
                                        x = x + 29;
                                    }
                                    else
                                    {
                                        x = x + 28;

                                    }


                                    cb.BeginText();
                                    cb.SetTextMatrix((x + (((x + 29) - x) / 2) - (cb.GetEffectiveStringWidth(drf["KEY"].ToString(), false) / 2)), Yaxis - 10);
                                    cb.SetFontAndSize(bf, 8);
                                    cb.ShowText(drf["KEY"].ToString());
                                    cb.EndText();



                                    cb.MoveTo(x, Yaxis);
                                    cb.LineTo(x, Yaxis - 15);
                                    cb.Stroke();


                                    x = x + 28;
                                    cb.BeginText();
                                    cb.SetTextMatrix((x + (((x + 29) - x) / 2) - (cb.GetEffectiveStringWidth(drf["ANS"].ToString(), false) / 2)), Yaxis - 10);
                                    cb.SetFontAndSize(bf, 8);
                                    cb.ShowText(drf["ANS"].ToString());
                                    cb.EndText();



                                    cb.MoveTo(x, Yaxis);
                                    cb.LineTo(x, Yaxis - 15);
                                    cb.Stroke();

                                    x = x + 28;

                                    cb.BeginText();
                                    cb.SetTextMatrix((x + (((x + 29) - x) / 2) - (cb.GetEffectiveStringWidth(drf["DL"].ToString(), false) / 2)), Yaxis - 10);
                                    cb.SetFontAndSize(bf, 8);
                                    cb.ShowText(drf["DL"].ToString());
                                    cb.EndText();


                                    cb.MoveTo(x, Yaxis);
                                    cb.LineTo(x, Yaxis - 15);
                                    cb.Stroke();


                                    x = x + 28;
                                    cb.BeginText();
                                    cb.SetTextMatrix((x + (((x + 29) - x) / 2) - (cb.GetEffectiveStringWidth(drf["Score"].ToString(), false) / 2)), Yaxis - 10);
                                    cb.SetFontAndSize(bf, 8);
                                    cb.ShowText(drf["Score"].ToString());
                                    cb.EndText();


                                    cb.MoveTo(x, Yaxis);
                                    cb.LineTo(x, Yaxis - 15);
                                    cb.Stroke();


                                    x = x + 28;
                                    if (dataCount == 4)
                                    {

                                        dataCount = 0;
                                        x = 20;
                                        Yaxis = Yaxis - 15;
                                        cb.MoveTo(x, Yaxis - 15);
                                        cb.LineTo(580, Yaxis - 15);
                                        cb.Stroke();

                                        cb.SetLineWidth(1f);
                                        cb.MoveTo(580, Yaxis);
                                        cb.LineTo(580, Yaxis - 15);
                                        cb.Stroke();
                                        cb.SetLineWidth(0.5f);

                                    }
                                    dataCount++;

                                    if (totalcount == 4)
                                    {

                                        cb.MoveTo(20, Yaxis);
                                        cb.LineTo(580, Yaxis);
                                        cb.Stroke();

                                        cb.SetLineWidth(1f);
                                        cb.MoveTo(580, Yaxis);
                                        cb.LineTo(580, Yaxis - 15);
                                        cb.Stroke();


                                        cb.MoveTo(580, Yaxis);
                                        cb.LineTo(580, Yaxis + 15);
                                        cb.Stroke();
                                        cb.SetLineWidth(0.5f);
                                    }

                                    if (totalcount == dtfilter.Rows.Count)
                                    {
                                        cb.SetLineWidth(1f);

                                        cb.MoveTo(x, Yaxis);
                                        cb.LineTo(x, Yaxis - 15);
                                        cb.Stroke();

                                        cb.MoveTo(20, Yaxis - 15);
                                        cb.LineTo(580, Yaxis - 15);
                                        cb.Stroke();

                                        cb.SetLineWidth(0.5f);



                                    }

                                }

                            }

                            //
                        }

                        Yaxis = Yaxis - 30;
                        float subjectaxis = 0;
                        subjectaxis = Yaxis;


                        if (subjectaxis < 20)
                        {
                            document.NewPage();
                            Yaxis = 800;
                            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                            cb.SetLineWidth(0.5f);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                        }

                        float xsubj = 20;
                        float ysubj = 20;
                        Yaxis = Yaxis - 15;
                        int datac = 0;

                        foreach (DataRow drSubject in dsPrintData.Tables[1].Rows)
                        {
                            if (datac == 4)
                            {
                                datac = 0;
                                Yaxis = Yaxis - 70;
                                xsubj = 20;
                            }

                            ysubj = Yaxis;

                            if (Yaxis < 20)
                            {
                                document.NewPage();
                                Yaxis = 800;
                                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                                cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                cb.SetLineWidth(0.5f);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                                
                            }
                            //Top
                            cb.MoveTo(xsubj, ysubj + 15);
                            cb.LineTo(xsubj + 120, ysubj + 15);
                            cb.Stroke();

                            cb.BeginText();
                            //cb.SetTextMatrix((xsubj + (((xsubj + 120) - xsubj) / 2) - (cb.GetEffectiveStringWidth(drSubject["Subject_Name"].ToString(), false) / 2)), ysubj + 5);
                           // cb.SetTextMatrix((xsubj + (((xsubj + 120) - xsubj) / 2) - (cb.GetEffectiveStringWidth(drSubject["Subject_Name"].ToString(), false) / 2)), ysubj + 5);


                            if (xsubj == 20)
                            {
                                cb.SetTextMatrix(xsubj + 50, ysubj + 5);
                            }
                            else
                            {
                                cb.SetTextMatrix((xsubj + (((xsubj + 120) - xsubj) / 2) - (cb.GetEffectiveStringWidth(drSubject["Subject_Name"].ToString(), false) / 2)), ysubj + 5);
                            }
                            

                            cb.SetFontAndSize(bf, 8);
                            cb.ShowText(drSubject["Subject_Name"].ToString());
                            
                            cb.EndText();

                            //right                        
                            cb.MoveTo(xsubj + 120, ysubj + 15);
                            cb.LineTo(xsubj + 120, ysubj);
                            cb.Stroke();

                            //left
                            cb.MoveTo(xsubj, ysubj + 15);
                            cb.LineTo(xsubj, ysubj);
                            cb.Stroke();


                            //Bottom
                            cb.MoveTo(xsubj, ysubj);
                            cb.LineTo(xsubj + 120, ysubj);
                            cb.Stroke();
                                                      

                            ysubj = ysubj - 10;


                            cb.BeginText();                            
                            cb.SetTextMatrix(xsubj + 3, ysubj + 3);
                            cb.SetFontAndSize(bf, 8);
                            cb.ShowText("Correct");
                            cb.EndText();



                            cb.BeginText();
                            cb.SetTextMatrix((xsubj + (((xsubj + 200) - xsubj) / 2) - (cb.GetEffectiveStringWidth(drSubject["Correct"].ToString(), false) / 2)), ysubj + 3);
                            cb.SetFontAndSize(bf, 8);
                            cb.ShowText(drSubject["Correct"].ToString());
                            cb.EndText();

                            //right                        
                            cb.MoveTo(xsubj + 70, ysubj + 10);
                            cb.LineTo(xsubj + 70, ysubj);
                            cb.Stroke();


                            //right                        
                            cb.MoveTo(xsubj + 120, ysubj + 15);
                            cb.LineTo(xsubj + 120, ysubj);
                            cb.Stroke();

                            //left
                            cb.MoveTo(xsubj, ysubj + 15);
                            cb.LineTo(xsubj, ysubj);
                            cb.Stroke();


                            //Bottom
                            cb.MoveTo(xsubj, ysubj);
                            cb.LineTo(xsubj + 120, ysubj);
                            cb.Stroke();



                            ysubj = ysubj - 10;


                            cb.BeginText();
                            //cb.SetTextMatrix((xsubj + (((xsubj + 70) - xsubj) / 2) - (cb.GetEffectiveStringWidth("Wrong", false) / 2)), ysubj);
                            cb.SetTextMatrix(xsubj + 3, ysubj + 3);
                            cb.SetFontAndSize(bf, 8);
                            cb.ShowText("Wrong");
                            cb.EndText();



                            cb.BeginText();
                            cb.SetTextMatrix((xsubj + (((xsubj + 200) - xsubj) / 2) - (cb.GetEffectiveStringWidth(drSubject["Wrong"].ToString(), false) / 2)), ysubj + 3);
                            cb.SetFontAndSize(bf, 8);
                            cb.ShowText(drSubject["Wrong"].ToString());
                            cb.EndText();

                            //right                        
                            cb.MoveTo(xsubj + 70, ysubj + 10);
                            cb.LineTo(xsubj + 70, ysubj);
                            cb.Stroke();


                            //right                        
                            cb.MoveTo(xsubj + 120, ysubj + 15);
                            cb.LineTo(xsubj + 120, ysubj);
                            cb.Stroke();

                            //left
                            cb.MoveTo(xsubj, ysubj + 15);
                            cb.LineTo(xsubj, ysubj);
                            cb.Stroke();


                            //Bottom
                            cb.MoveTo(xsubj, ysubj);
                            cb.LineTo(xsubj + 120, ysubj);
                            cb.Stroke();


                            ysubj = ysubj - 10;



                            cb.BeginText();
                            //cb.SetTextMatrix((xsubj + (((xsubj + 70) - xsubj) / 2) - (cb.GetEffectiveStringWidth("UnAttempted", false) / 2)), ysubj);

                            cb.SetTextMatrix(xsubj + 3, ysubj + 3);
                            cb.SetFontAndSize(bf, 8);
                            cb.ShowText("Unattempted");
                            cb.EndText();


                            cb.BeginText();
                            cb.SetTextMatrix((xsubj + (((xsubj + 200) - xsubj) / 2) - (cb.GetEffectiveStringWidth(drSubject["UnAttempted"].ToString(), false) / 2)), ysubj + 3);
                            cb.SetFontAndSize(bf, 8);
                            cb.ShowText(drSubject["UnAttempted"].ToString());
                            cb.EndText();


                            //right                        
                            cb.MoveTo(xsubj + 70, ysubj + 10);
                            cb.LineTo(xsubj + 70, ysubj);
                            cb.Stroke();


                            //right                        
                            cb.MoveTo(xsubj + 120, ysubj + 15);
                            cb.LineTo(xsubj + 120, ysubj);
                            cb.Stroke();

                            //left
                            cb.MoveTo(xsubj, ysubj + 15);
                            cb.LineTo(xsubj, ysubj);
                            cb.Stroke();


                            //Bottom
                            cb.MoveTo(xsubj, ysubj);
                            cb.LineTo(xsubj + 120, ysubj);
                            cb.Stroke();


                            ysubj = ysubj - 10;



                            cb.BeginText();
                            cb.SetTextMatrix(xsubj + 3, ysubj + 3);
                            cb.SetFontAndSize(bf, 8);
                            cb.ShowText("Score");
                            cb.EndText();

                            cb.BeginText();
                            cb.SetTextMatrix((xsubj + (((xsubj + 200) - xsubj) / 2) - (cb.GetEffectiveStringWidth(drSubject["Score"].ToString(), false) / 2)), ysubj + 3);
                            cb.SetFontAndSize(bf, 8);
                            cb.ShowText(drSubject["Score"].ToString());
                            cb.EndText();


                            //right                        
                            cb.MoveTo(xsubj + 70, ysubj + 10);
                            cb.LineTo(xsubj + 70, ysubj);
                            cb.Stroke();

                            //right                        
                            cb.MoveTo(xsubj + 120, ysubj + 15);
                            cb.LineTo(xsubj + 120, ysubj);
                            cb.Stroke();

                            //left
                            cb.MoveTo(xsubj, ysubj + 15);
                            cb.LineTo(xsubj, ysubj);
                            cb.Stroke();


                            //Bottom
                            cb.MoveTo(xsubj, ysubj);
                            cb.LineTo(xsubj + 120, ysubj);
                            cb.Stroke();



                            xsubj = xsubj + 140;

                            datac++;
                        }

                        document.NewPage();
                    }
                }






                        document.Close();
                        string CurTimeFrame = null;
                        CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Test_Answer_Sheet{0}.pdf", CurTimeFrame));
                        Response.BinaryWrite(output.ToArray());

                        Show_Error_Success_Box("S", "PDF File generated successfully.");
                    
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }

    }


    private void Email_PrintAttach()
    {
        try
        {
            //// Create a Document object
            //dynamic document = new Document(PageSize.A4, 50, 50, 25, 25);

            //// Create a new PdfWriter object, specifying the output stream
            //dynamic output = new MemoryStream();
            //dynamic writer = PdfWriter.GetInstance(document, output);


            //// Open the Document for writing
            //document.Open();



            dynamic TitleFont = FontFactory.GetFont("Arial", 16, Font.BOLD);
            dynamic subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
            dynamic boldTableFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
            dynamic endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
            dynamic bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);


            // Open the Document for writing

            
            //For each item selected in Grid run the following things
            foreach (DataListItem dtlItem in dlGridStudSelect.Items)
            {
                // Create a Document object
                dynamic document = new Document(PageSize.A4, 50, 50, 25, 25);

                // Create a new PdfWriter object, specifying the output stream
                dynamic output = new MemoryStream();
                dynamic writer = PdfWriter.GetInstance(document, output);


                // Open the Document for writing
                document.Open();


                CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
                Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
                Label lblStudentRollNo = (Label)dtlItem.FindControl("lblStudentRollNo");
                Label lblStudentName = (Label)dtlItem.FindControl("lblStudentName");
                Label lblStudentEmail = (Label)dtlItem.FindControl("lblStudentEmail");

                if (chkStudent.Checked == true & !string.IsNullOrEmpty(lblStudentEmail.Text.Trim()))
                {
                    DataSet dsPrintData = new DataSet();
                    dsPrintData = Bind_Email_Data(lblSBEntryCode.Text);
                    if (dsPrintData != null)
                    {
                        if (dsPrintData.Tables.Count != 0)
                        {
                            if (dsPrintData.Tables[0].Rows.Count != 0)
                            {
                                float YPos = 0;
                                YPos = 800;

                                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                                PdfContentByte cb = writer.DirectContent;

                                cb.BeginText();
                                cb.SetTextMatrix(220, 820);
                                cb.SetFontAndSize(bf, 14);

                                cb.ShowText("MT Educare Ltd.");
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);


                                cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                cb.SetLineWidth(0.5f);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                                cb.EndText();


                                cb.MoveTo(20, YPos + 15);
                                cb.LineTo(580, YPos + 15);
                                cb.Stroke();

                                cb.BeginText();

                                cb.SetTextMatrix(20, YPos);
                                cb.SetFontAndSize(bf, 10);
                                cb.ShowText("Roll No:");


                                cb.SetTextMatrix(80, YPos);
                                cb.SetFontAndSize(bf, 10);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                                cb.ShowText(lblStudentRollNo.Text);

                                cb.SetTextMatrix(130, YPos);
                                cb.SetFontAndSize(bf, 10);
                                cb.ShowText("Student Name :");


                                cb.SetTextMatrix(200, YPos);
                                cb.SetFontAndSize(bf, 10);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                                cb.ShowText(lblStudentName.Text);


                                cb.SetTextMatrix(450, YPos);
                                cb.SetFontAndSize(bf, 10);
                                cb.ShowText("Center :");


                                cb.SetTextMatrix(500, YPos);
                                cb.SetFontAndSize(bf, 10);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                                cb.ShowText(ddlCentre.SelectedItem.ToString());

                                cb.SetTextMatrix(20, YPos - 15);
                                cb.SetFontAndSize(bf, 10);
                                cb.ShowText("Test Name:");


                                cb.SetTextMatrix(80, YPos - 15);
                                cb.SetFontAndSize(bf, 10);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                                cb.ShowText(ddlTestName.SelectedItem.Text);

                                cb.EndText();

                                DataView dvfilter = new DataView(dsPrintData.Tables[0]);
                                DataTable dtfilter = new DataTable();
                                float Yaxis = YPos;

                                if (dsPrintData.Tables[1].Rows.Count != 0)
                                {
                                    foreach (DataRow drSubject in dsPrintData.Tables[1].Rows)
                                    {


                                        dtfilter = new DataTable();
                                        dvfilter.RowFilter = string.Empty;
                                        string subject_Code = "";
                                        string subject_Name = "";
                                        subject_Code = drSubject["Subject_Code"].ToString();
                                        subject_Name = drSubject["Subject_Name"].ToString();

                                        dvfilter.RowFilter = "Subject_Code = '" + subject_Code + "'";
                                        dtfilter = dvfilter.ToTable();


                                        int datarowcount = dtfilter.Rows.Count;
                                        float finalyaxis = 0;
                                        finalyaxis = (Yaxis - 30 - (datarowcount / 4));

                                        if (finalyaxis < 20)
                                        {
                                            document.NewPage();
                                            Yaxis = 800;
                                            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                                            cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                            cb.SetLineWidth(0.5f);
                                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                                        }



                                        Yaxis = Yaxis - 5;

                                        cb.BeginText();
                                        cb.SetTextMatrix(20, Yaxis - 40);
                                        cb.SetFontAndSize(bf, 10);
                                        cb.ShowText(subject_Name.ToUpper());

                                        cb.EndText();


                                        //Start Header

                                        cb.SetLineWidth(1f);
                                        cb.MoveTo(20, Yaxis - 50);
                                        cb.LineTo(580, Yaxis - 50);
                                        cb.Stroke();


                                        cb.MoveTo(20, Yaxis - 70);
                                        cb.LineTo(580, Yaxis - 70);
                                        cb.Stroke();


                                        cb.MoveTo(20, Yaxis - 50);
                                        cb.LineTo(20, Yaxis - 70);
                                        cb.Stroke();


                                        cb.SetLineWidth(0.5f);

                                        string col1 = "NO";
                                        string col2 = "KEY";
                                        string col3 = "ANS";
                                        string col4 = "DL";
                                        string col5 = "Score";
                                        List<string> objCollist = new List<string>();

                                        for (int i = 0; i < 4; i++)
                                        {
                                            objCollist.Add(col1);
                                            objCollist.Add(col2);
                                            objCollist.Add(col3);
                                            objCollist.Add(col4);
                                            objCollist.Add(col5);
                                        }



                                        int x = 0;
                                        for (int i = 0; i < 20; i++)
                                        {

                                            if (i == 0)
                                            {

                                                cb.BeginText();
                                                cb.SetTextMatrix((x + (((x + 70) - x) / 2) - (cb.GetEffectiveStringWidth(objCollist[i].ToString(), false) / 2)), Yaxis - 62);
                                                cb.SetFontAndSize(bf, 8);
                                                cb.ShowText(objCollist[i].ToString());
                                                cb.EndText();

                                                x = 49;

                                                cb.MoveTo(x, Yaxis - 50);
                                                cb.LineTo(x, Yaxis - 70);
                                                cb.Stroke();
                                            }
                                            else if (i == 19)
                                            {
                                                cb.BeginText();
                                                cb.SetTextMatrix((x + (((x + 28) - x) / 2) - (cb.GetEffectiveStringWidth(objCollist[i].ToString(), false) / 2)), Yaxis - 62);
                                                cb.SetFontAndSize(bf, 8);
                                                cb.ShowText(objCollist[i].ToString());
                                                cb.EndText();


                                                cb.SetLineWidth(1f);
                                                cb.MoveTo(580, Yaxis - 50);
                                                cb.LineTo(580, Yaxis - 70);
                                                cb.Stroke();
                                                cb.SetLineWidth(0.5f);
                                            }
                                            else
                                            {

                                                cb.BeginText();
                                                cb.SetTextMatrix((x + (((x + 28) - x) / 2) - (cb.GetEffectiveStringWidth(objCollist[i].ToString(), false) / 2)), Yaxis - 62);
                                                cb.SetFontAndSize(bf, 8);
                                                cb.ShowText(objCollist[i].ToString());
                                                cb.EndText();

                                                x = x + 28;

                                                if (i == 4 || i == 9 || i == 14)
                                                {
                                                    cb.SetLineWidth(1f);
                                                }


                                                cb.MoveTo(x, Yaxis - 50);
                                                cb.LineTo(x, Yaxis - 70);
                                                cb.Stroke();
                                                cb.SetLineWidth(0.5f);
                                            }

                                        }

                                        //End Header


                                        int dataCount = 1;

                                        Yaxis = Yaxis - 70;
                                        x = 20;

                                        int totalcount = 0;

                                        foreach (DataRow drf in dtfilter.Rows)
                                        {


                                            totalcount++;



                                            cb.BeginText();
                                            cb.SetTextMatrix((x + (((x + 29) - x) / 2) - (cb.GetEffectiveStringWidth(drf["No"].ToString(), false) / 2)), Yaxis - 10);
                                            cb.SetFontAndSize(bf, 8);
                                            cb.ShowText(drf["No"].ToString());
                                            cb.EndText();

                                            cb.SetLineWidth(1f);

                                            cb.MoveTo(x, Yaxis);
                                            cb.LineTo(x, Yaxis - 15);
                                            cb.Stroke();

                                            cb.SetLineWidth(0.5f);
                                            if (dataCount == 1)
                                            {
                                                x = x + 29;
                                            }
                                            else
                                            {
                                                x = x + 28;

                                            }


                                            cb.BeginText();
                                            cb.SetTextMatrix((x + (((x + 29) - x) / 2) - (cb.GetEffectiveStringWidth(drf["KEY"].ToString(), false) / 2)), Yaxis - 10);
                                            cb.SetFontAndSize(bf, 8);
                                            cb.ShowText(drf["KEY"].ToString());
                                            cb.EndText();



                                            cb.MoveTo(x, Yaxis);
                                            cb.LineTo(x, Yaxis - 15);
                                            cb.Stroke();


                                            x = x + 28;
                                            cb.BeginText();
                                            cb.SetTextMatrix((x + (((x + 29) - x) / 2) - (cb.GetEffectiveStringWidth(drf["ANS"].ToString(), false) / 2)), Yaxis - 10);
                                            cb.SetFontAndSize(bf, 8);
                                            cb.ShowText(drf["ANS"].ToString());
                                            cb.EndText();



                                            cb.MoveTo(x, Yaxis);
                                            cb.LineTo(x, Yaxis - 15);
                                            cb.Stroke();

                                            x = x + 28;

                                            cb.BeginText();
                                            cb.SetTextMatrix((x + (((x + 29) - x) / 2) - (cb.GetEffectiveStringWidth(drf["DL"].ToString(), false) / 2)), Yaxis - 10);
                                            cb.SetFontAndSize(bf, 8);
                                            cb.ShowText(drf["DL"].ToString());
                                            cb.EndText();


                                            cb.MoveTo(x, Yaxis);
                                            cb.LineTo(x, Yaxis - 15);
                                            cb.Stroke();


                                            x = x + 28;
                                            cb.BeginText();
                                            cb.SetTextMatrix((x + (((x + 29) - x) / 2) - (cb.GetEffectiveStringWidth(drf["Score"].ToString(), false) / 2)), Yaxis - 10);
                                            cb.SetFontAndSize(bf, 8);
                                            cb.ShowText(drf["Score"].ToString());
                                            cb.EndText();


                                            cb.MoveTo(x, Yaxis);
                                            cb.LineTo(x, Yaxis - 15);
                                            cb.Stroke();


                                            x = x + 28;
                                            if (dataCount == 4)
                                            {

                                                dataCount = 0;
                                                x = 20;
                                                Yaxis = Yaxis - 15;
                                                cb.MoveTo(x, Yaxis - 15);
                                                cb.LineTo(580, Yaxis - 15);
                                                cb.Stroke();

                                                cb.SetLineWidth(1f);
                                                cb.MoveTo(580, Yaxis);
                                                cb.LineTo(580, Yaxis - 15);
                                                cb.Stroke();
                                                cb.SetLineWidth(0.5f);

                                            }
                                            dataCount++;

                                            if (totalcount == 4)
                                            {

                                                cb.MoveTo(20, Yaxis);
                                                cb.LineTo(580, Yaxis);
                                                cb.Stroke();

                                                cb.SetLineWidth(1f);
                                                cb.MoveTo(580, Yaxis);
                                                cb.LineTo(580, Yaxis - 15);
                                                cb.Stroke();


                                                cb.MoveTo(580, Yaxis);
                                                cb.LineTo(580, Yaxis + 15);
                                                cb.Stroke();
                                                cb.SetLineWidth(0.5f);
                                            }

                                            if (totalcount == dtfilter.Rows.Count)
                                            {
                                                cb.SetLineWidth(1f);

                                                cb.MoveTo(x, Yaxis);
                                                cb.LineTo(x, Yaxis - 15);
                                                cb.Stroke();

                                                cb.MoveTo(20, Yaxis - 15);
                                                cb.LineTo(580, Yaxis - 15);
                                                cb.Stroke();

                                                cb.SetLineWidth(0.5f);



                                            }

                                        }

                                    }

                                    //
                                }

                                Yaxis = Yaxis - 30;
                                float subjectaxis = 0;
                                subjectaxis = Yaxis;

                                if (subjectaxis < 20)
                                {
                                    document.NewPage();
                                    Yaxis = 800;
                                    bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                                    cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                    cb.SetLineWidth(0.5f);
                                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                                }

                                float xsubj = 20;
                                float ysubj = 20;
                                Yaxis = Yaxis - 15;
                                int datac = 0;

                                foreach (DataRow drSubject in dsPrintData.Tables[1].Rows)
                                {
                                    if (datac == 4)
                                    {
                                        datac = 0;
                                        Yaxis = Yaxis - 70;
                                        xsubj = 20;
                                    }

                                    ysubj = Yaxis;

                                    if (Yaxis < 20)
                                    {
                                        document.NewPage();
                                        Yaxis = 800;
                                        bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                                        cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                        cb.SetLineWidth(0.5f);
                                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                                    }
                                    //Top
                                    cb.MoveTo(xsubj, ysubj + 15);
                                    cb.LineTo(xsubj + 120, ysubj + 15);
                                    cb.Stroke();

                                    cb.BeginText();
                                    //cb.SetTextMatrix((xsubj + (((xsubj + 120) - xsubj) / 2) - (cb.GetEffectiveStringWidth(drSubject["Subject_Name"].ToString(), false) / 2)), ysubj + 5);
                                    // cb.SetTextMatrix((xsubj + (((xsubj + 120) - xsubj) / 2) - (cb.GetEffectiveStringWidth(drSubject["Subject_Name"].ToString(), false) / 2)), ysubj + 5);


                                    if (xsubj == 20)
                                    {
                                        cb.SetTextMatrix(xsubj + 50, ysubj + 5);
                                    }
                                    else
                                    {
                                        cb.SetTextMatrix((xsubj + (((xsubj + 120) - xsubj) / 2) - (cb.GetEffectiveStringWidth(drSubject["Subject_Name"].ToString(), false) / 2)), ysubj + 5);
                                    }


                                    cb.SetFontAndSize(bf, 8);
                                    cb.ShowText(drSubject["Subject_Name"].ToString());

                                    cb.EndText();

                                    //right                        
                                    cb.MoveTo(xsubj + 120, ysubj + 15);
                                    cb.LineTo(xsubj + 120, ysubj);
                                    cb.Stroke();

                                    //left
                                    cb.MoveTo(xsubj, ysubj + 15);
                                    cb.LineTo(xsubj, ysubj);
                                    cb.Stroke();


                                    //Bottom
                                    cb.MoveTo(xsubj, ysubj);
                                    cb.LineTo(xsubj + 120, ysubj);
                                    cb.Stroke();


                                    ysubj = ysubj - 10;


                                    cb.BeginText();
                                    cb.SetTextMatrix(xsubj + 3, ysubj + 3);
                                    cb.SetFontAndSize(bf, 8);
                                    cb.ShowText("Correct");
                                    cb.EndText();



                                    cb.BeginText();
                                    cb.SetTextMatrix((xsubj + (((xsubj + 200) - xsubj) / 2) - (cb.GetEffectiveStringWidth(drSubject["Correct"].ToString(), false) / 2)), ysubj + 3);
                                    cb.SetFontAndSize(bf, 8);
                                    cb.ShowText(drSubject["Correct"].ToString());
                                    cb.EndText();

                                    //right                        
                                    cb.MoveTo(xsubj + 70, ysubj + 10);
                                    cb.LineTo(xsubj + 70, ysubj);
                                    cb.Stroke();


                                    //right                        
                                    cb.MoveTo(xsubj + 120, ysubj + 15);
                                    cb.LineTo(xsubj + 120, ysubj);
                                    cb.Stroke();

                                    //left
                                    cb.MoveTo(xsubj, ysubj + 15);
                                    cb.LineTo(xsubj, ysubj);
                                    cb.Stroke();


                                    //Bottom
                                    cb.MoveTo(xsubj, ysubj);
                                    cb.LineTo(xsubj + 120, ysubj);
                                    cb.Stroke();



                                    ysubj = ysubj - 10;


                                    cb.BeginText();
                                    //cb.SetTextMatrix((xsubj + (((xsubj + 70) - xsubj) / 2) - (cb.GetEffectiveStringWidth("Wrong", false) / 2)), ysubj);
                                    cb.SetTextMatrix(xsubj + 3, ysubj + 3);
                                    cb.SetFontAndSize(bf, 8);
                                    cb.ShowText("Wrong");
                                    cb.EndText();



                                    cb.BeginText();
                                    cb.SetTextMatrix((xsubj + (((xsubj + 200) - xsubj) / 2) - (cb.GetEffectiveStringWidth(drSubject["Wrong"].ToString(), false) / 2)), ysubj + 3);
                                    cb.SetFontAndSize(bf, 8);
                                    cb.ShowText(drSubject["Wrong"].ToString());
                                    cb.EndText();

                                    //right                        
                                    cb.MoveTo(xsubj + 70, ysubj + 10);
                                    cb.LineTo(xsubj + 70, ysubj);
                                    cb.Stroke();


                                    //right                        
                                    cb.MoveTo(xsubj + 120, ysubj + 15);
                                    cb.LineTo(xsubj + 120, ysubj);
                                    cb.Stroke();

                                    //left
                                    cb.MoveTo(xsubj, ysubj + 15);
                                    cb.LineTo(xsubj, ysubj);
                                    cb.Stroke();


                                    //Bottom
                                    cb.MoveTo(xsubj, ysubj);
                                    cb.LineTo(xsubj + 120, ysubj);
                                    cb.Stroke();


                                    ysubj = ysubj - 10;



                                    cb.BeginText();
                                    //cb.SetTextMatrix((xsubj + (((xsubj + 70) - xsubj) / 2) - (cb.GetEffectiveStringWidth("UnAttempted", false) / 2)), ysubj);

                                    cb.SetTextMatrix(xsubj + 3, ysubj + 3);
                                    cb.SetFontAndSize(bf, 8);
                                    cb.ShowText("Unattempted");
                                    cb.EndText();


                                    cb.BeginText();
                                    cb.SetTextMatrix((xsubj + (((xsubj + 200) - xsubj) / 2) - (cb.GetEffectiveStringWidth(drSubject["UnAttempted"].ToString(), false) / 2)), ysubj + 3);
                                    cb.SetFontAndSize(bf, 8);
                                    cb.ShowText(drSubject["UnAttempted"].ToString());
                                    cb.EndText();


                                    //right                        
                                    cb.MoveTo(xsubj + 70, ysubj + 10);
                                    cb.LineTo(xsubj + 70, ysubj);
                                    cb.Stroke();


                                    //right                        
                                    cb.MoveTo(xsubj + 120, ysubj + 15);
                                    cb.LineTo(xsubj + 120, ysubj);
                                    cb.Stroke();

                                    //left
                                    cb.MoveTo(xsubj, ysubj + 15);
                                    cb.LineTo(xsubj, ysubj);
                                    cb.Stroke();


                                    //Bottom
                                    cb.MoveTo(xsubj, ysubj);
                                    cb.LineTo(xsubj + 120, ysubj);
                                    cb.Stroke();


                                    ysubj = ysubj - 10;



                                    cb.BeginText();
                                    cb.SetTextMatrix(xsubj + 3, ysubj + 3);
                                    cb.SetFontAndSize(bf, 8);
                                    cb.ShowText("Score");
                                    cb.EndText();

                                    cb.BeginText();
                                    cb.SetTextMatrix((xsubj + (((xsubj + 200) - xsubj) / 2) - (cb.GetEffectiveStringWidth(drSubject["Score"].ToString(), false) / 2)), ysubj + 3);
                                    cb.SetFontAndSize(bf, 8);
                                    cb.ShowText(drSubject["Score"].ToString());
                                    cb.EndText();


                                    //right                        
                                    cb.MoveTo(xsubj + 70, ysubj + 10);
                                    cb.LineTo(xsubj + 70, ysubj);
                                    cb.Stroke();

                                    //right                        
                                    cb.MoveTo(xsubj + 120, ysubj + 15);
                                    cb.LineTo(xsubj + 120, ysubj);
                                    cb.Stroke();

                                    //left
                                    cb.MoveTo(xsubj, ysubj + 15);
                                    cb.LineTo(xsubj, ysubj);
                                    cb.Stroke();


                                    //Bottom
                                    cb.MoveTo(xsubj, ysubj);
                                    cb.LineTo(xsubj + 120, ysubj);
                                    cb.Stroke();



                                    xsubj = xsubj + 140;

                                    datac++;
                                }
                                cb.EndText();
                            }   
                        }                   

                    }

                    //document.NewPage(); 
                    writer.CloseStream = false;
                    document.Close();
                    output.Position = 0;


                    //EMail code should come over her

                    string userid = "", Password = "", Host = "", SSL = "", MailType = "";
                    int Port = 0;
                    DataSet dsCRoom = ProductController.GetMailDetails_ByCenter(ddlCentre.SelectedValue.ToString().Trim(), "Transactional");


                    if (dsCRoom.Tables[0].Rows.Count > 0)
                    {

                        userid = Convert.ToString(dsCRoom.Tables[0].Rows[0]["UserId"]);
                        Password = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Password"]);
                        Host = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Host"]);
                        Port = Convert.ToInt32(Convert.ToString(dsCRoom.Tables[0].Rows[0]["Port"]));
                        SSL = Convert.ToString(dsCRoom.Tables[0].Rows[0]["EnableSSl"]);
                        MailType = Convert.ToString(dsCRoom.Tables[0].Rows[0]["MailType"]);

                        //////

                        MailMessage Msg = new MailMessage(userid, lblStudentEmail.Text.Trim());
                        //MailMessage Msg = new MailMessage(userid, "digambar1212@gmail.com");

                        string CurTimeFrame = null;
                        CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

                        // Subject of e-mail
                        Msg.Subject = "Answer Sheet for " + lblStudentName.Text;
                        Msg.Body += "Dear Parent <br/><br/>Please find enclosed a PDF file containing Statement of Marks for your ward " + lblStudentName.Text + " for " + lblStandard_Result.Text + " standard at MT Educare. <br/><br/> Warm Regards <br/><br/> MT Educare Ltd. <br/><br/>This is an automated system generated Mail. Please do not reply.<br>";
                        string Att_Name = "AnswerSheet.pdf";
                        Msg.Attachments.Add(new Attachment(output, Att_Name));

                        Msg.IsBodyHtml = true;

                        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                        string Userid = cookie.Values["UserID"];

                        bool value = System.Convert.ToBoolean(SSL);
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = Host;
                        smtp.EnableSsl = value;
                        NetworkCredential NetworkCred = new NetworkCredential(userid, Password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = Port;

                        int resultid = 0;
                        try
                        {
                            smtp.Timeout = 20000;
                            smtp.Send(Msg);

                            resultid = ProductController.Insert_Mailog(lblStudentEmail.Text.Trim(), Msg.Subject.ToString().Trim(), Msg.Body.ToString().Trim(), 1, Att_Name, "1", Userid, 1, ddlCentre.SelectedValue.ToString().Trim(), MailType);

                        }
                        catch (Exception ex)
                        {
                            resultid = ProductController.Insert_Mailog(lblStudentEmail.Text.Trim(), Msg.Subject.ToString().Trim(), Msg.Body.ToString().Trim(), 1, Att_Name, "2", Userid, 1, ddlCentre.SelectedValue.ToString().Trim(), MailType);
                        }


                        //
                    }
                    else
                    {

                    }

                }                
            }

            //document.Close();
            //string CurTimeFrame = null;
            //CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

            //Response.ContentType = "application/pdf";
            //Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Test_Answer_Sheet{0}.pdf", CurTimeFrame));
            //Response.BinaryWrite(output.ToArray());
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();            
            FillDDL_TestTypes();
            Fill_ConductNo();
            
        }
    }
      

    private void FillDDL_TestTypes()
    {
        DataSet dsTestType = ProductController.GetAllActiveTestType();
        BindListBox(ddlTestType, dsTestType, "TestType_Name", "TestType_Id");

    }

    
    private void FillDDL_TestName()
    {
        ddlTestName.Items.Clear();      
        

        string BatchCode = "";
        int BatchCnt = 0;
        int BatchSelCnt = 0;
        for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
        {
            if (ddlBatch.Items[BatchCnt].Selected == true)
            {
                BatchSelCnt = BatchSelCnt + 1;
            }
        }

        if (BatchSelCnt == 0)
        {
            //When all is selected
            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
            }

            BatchCode = Common.RemoveComma(BatchCode);
        }
        else
        {
            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                if (ddlBatch.Items[BatchCnt].Selected == true)
                {
                    BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
                }
            }
            BatchCode = Common.RemoveComma(BatchCode);

        }

        string TestType_ID = "";
        int TypeCnt = 0;
        int TypeSelCnt = 0;
        for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
        {
            if (ddlTestType.Items[TypeCnt].Selected == true)
            {
                TypeSelCnt = TypeSelCnt + 1;
            }
        }

        if (TypeSelCnt == 0)
        {
            //When all is selected
            for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
            {
                TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
            }
            //if (Strings.Right(TestType_ID, 1) == ",")
            //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);
            TestType_ID = Common.RemoveComma(TestType_ID);
        }
        else
        {
            for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
            {
                if (ddlTestType.Items[TypeCnt].Selected == true)
                {
                    TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
                }
            }
            TestType_ID = Common.RemoveComma(TestType_ID);

        }

        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        string TestName = null;
        TestName = "%";

        
        string FromDate = null;
        string ToDate = null;


        string DateRange = id_date_range_picker_1.Value.ToString();
        if (DateRange != "")
        {
            FromDate = DateRange.Substring(0, 10);//Strings.Left(Report_Period, 10);            
            DateTime result = DateTime.ParseExact(FromDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            FromDate = result.ToString("yyyy-MM-dd");
        }
        if (string.IsNullOrEmpty(FromDate))
        {

            FromDate = "2010-01-01";
        }
        if (DateRange != "")
        {
            ToDate = DateRange.Substring(DateRange.Length - 10);//Strings.Right(Report_Period, 10);            
            DateTime result = DateTime.ParseExact(ToDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            ToDate = result.ToString("yyyy-MM-dd");
        }
        if (string.IsNullOrEmpty(ToDate))
        {
            ToDate = System.DateTime.Now.ToString("yyyy-MM-dd");
        }


        //if (DateRange != "")
        //{
        //    FromDate = DateRange.Substring(0, DateRange.Length);
        //}
        
        //if (string.IsNullOrEmpty(FromDate))
        //    FromDate = System.DateTime.Now.ToString("dd MMM yyyy");
        //if (DateRange != "")
        //{
        //    ToDate = DateRange.Substring(DateRange.Length - 10);
        //}
        
        //if (string.IsNullOrEmpty(ToDate))
        //    ToDate = System.DateTime.Now.ToString("dd MMM yyyy");


        string Centre_Code = null;
        Centre_Code = ddlCentre.SelectedItem.Value;
        string Conduct_No = "1";
        Conduct_No = ddlConductNo.SelectedItem.Value;

        if (ddlConductNo.SelectedItem.Value == "Select")
        {
            Conduct_No = "1";
        }
        else
        {
           Conduct_No =  ddlConductNo.SelectedItem.Value;
        }

        DataSet dsTestName = ProductController.Get_Test_Name(DivisionCode, YearName, StandardCode, BatchCode, "002", TestType_ID, FromDate, ToDate, Centre_Code, Conduct_No);
        BindDDL(ddlTestName, dsTestName, "Test_Name", "PKey");
        ddlTestName.Items.Insert(0, "Select");
        ddlTestName.SelectedIndex = 0;

    }

    private void FillDDL_Division()
    {
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        if (string.IsNullOrEmpty(lblHeader_User_Code.Text))
            Response.Redirect("Default.aspx");

        DataSet dsDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, "", "", "2", lblHeader_DBName.Text);
        BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
        ddlDivision.Items.Insert(0, "Select");
        ddlDivision.SelectedIndex = 0;


    }

    private void FillDDL_AcadYear()
    {
        DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcadYear, dsAcadYear, "Description", "Id");
        ddlAcadYear.Items.Insert(0, "Select");
        ddlAcadYear.SelectedIndex = 0;


    }


    private void Fill_ConductNo()
    {
        ddlConductNo.Items.Insert(0, "Select");
        ddlConductNo.Items.Insert(1, "1");
        ddlConductNo.Items.Insert(2, "2");
        ddlConductNo.Items.Insert(3, "3");
        ddlConductNo.Items.Insert(4, "4");
        ddlConductNo.SelectedIndex = 0;
        

    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
         if (ds != null)
        {
            if (ds.Tables.Count != 0)
            {
                if (ds.Tables[0].Rows.Count != 0)
                {
                    ddl.DataSource = ds;
                    ddl.DataTextField = txtField;
                    ddl.DataValueField = valField;
                    ddl.DataBind();
                }
            }
        }
    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
           DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;


        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;


        }
        Clear_Error_Success_Box();
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
        FillDDL_Search_Centre();
        Clear_Error_Success_Box();
    }

    private void FillDDL_Search_Centre()
    {
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);

        BindDDL(ddlCentre, dsCentre, "Center_Name", "Center_Code");
        ddlCentre.Items.Insert(0, "Select");
        ddlCentre.SelectedIndex = 0;
    }

    private void FillDDL_Standard()
    {
        ddlStudentName.Items.Clear();
        ddlStandard.Items.Clear();
        ddlBatch.Items.Clear();
        txtRollNo.Text = "";

        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        DataSet dsStandard = ProductController.GetAllActive_Standard_ForYear(Div_Code, YearName);
        BindDDL(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");
        ddlStandard.Items.Insert(0, "Select");
        ddlStandard.SelectedIndex = 0;
    }

    protected void ddlAcadYear_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
        Clear_Error_Success_Box();
    }

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        if (ds != null)
        {
            if (ds.Tables.Count != 0)
            {
                if (ds.Tables[0].Rows.Count != 0)
                {
                    ddl.DataSource = ds;
                    ddl.DataTextField = txtField;
                    ddl.DataValueField = valField;
                    ddl.DataBind();
                }
            }            
        }
        
    }

    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }

    private void Show_Error_Success_Box(string BoxType, string Error_Code)
    {
        if (BoxType == "E")
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox.Update();
        }
        else
        {
            Msg_Success.Visible = true;
            Msg_Error.Visible = false;
            lblSuccess.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox.Update();
        }
    }
    
    protected void ddlStandard_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Batch();
        FillDDL_TestName();
        Clear_Error_Success_Box();
    }

    private void FillDDL_Batch()
    {
        ddlStudentName.Items.Clear();
        ddlBatch.Items.Clear();
        txtRollNo.Text = "";

        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        string CentreCode = null;
        CentreCode = ddlCentre.SelectedValue;

        DataSet dsBatch = ProductController.GetAllActive_Batch_ForStandard(Div_Code, YearName, StandardCode, CentreCode);
        BindDDL(ddlBatch, dsBatch, "Batch_Name", "Batch_Code");
        ddlBatch.Items.Insert(0, "Select");
        ddlBatch.SelectedIndex = 0;

    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");        
    }

    protected void btnTestName_Click(object sender, System.EventArgs e)
    {
        FillDDL_TestName();
    }


    protected void ddlTestType_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_TestName();
        Clear_Error_Success_Box();
    }

    protected void ddlTestCategory_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_TestName();
        Clear_Error_Success_Box();
    }

    protected void ddlBatch_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_TestName();
        ddlStudentName.Items.Clear();
        txtRollNo.Text = "";
        Clear_Error_Success_Box();
      //  btnStudentName_Click(sender, e);
    }

    protected void ddlCentre_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Batch();
        FillDDL_TestName();
        Clear_Error_Success_Box();
    }

    protected void btnStudentName_Click(object sender, System.EventArgs e)
    {
        ddlStudentName.Items.Clear();
        ddlStudentRollNo.Items.Clear();
        txtRollNo.Text = "";

        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string Centre_Code = null;
        Centre_Code = ddlCentre.SelectedValue;

        string BatchCode = "";
        int BatchCnt = 0;
        int BatchSelCnt = 0;
        for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
        {
            if (ddlBatch.Items[BatchCnt].Selected == true)
            {
                BatchSelCnt = BatchSelCnt + 1;
            }
        }

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        if (BatchSelCnt == 0)
        {
            //When all is selected
            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
            }
            BatchCode = Common.RemoveComma(BatchCode);
            //if (Strings.Right(BatchCode, 1) == ",")
            //    BatchCode = Strings.Left(BatchCode, Strings.Len(BatchCode) - 1);
        }
        else
        {
            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                if (ddlBatch.Items[BatchCnt].Selected == true)
                {
                    BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
                }
            }

            BatchCode = Common.RemoveComma(BatchCode);
        }

        DataSet dsStudent = ProductController.GetStudent_ForTest_ByDivision_Centre_Standard(Div_Code, YearName, Centre_Code, StandardCode, BatchCode, "CDB", 1);
        BindDDL(ddlStudentName, dsStudent, "StudentName", "SBEntryCode");
        ddlStudentName.Items.Insert(0, "Select");
        ddlStudentName.SelectedIndex = 0;
        dlPrintStudent.DataSource = dsStudent.Tables[0];
        dlPrintStudent.DataBind();
        //--ddlStudentName
        BindDDL(ddlStudentRollNo, dsStudent, "SBEntryCode", "RollNo");
        ddlStudentRollNo.Items.Insert(0, "Select");
        ddlStudentRollNo.SelectedIndex = 0;


        BindDDL(ddlStudentEMailId, dsStudent, "SBEntryCode", "ParentsEMailId");
        ddlStudentEMailId.Items.Insert(0, "Select");
        ddlStudentEMailId.SelectedIndex = 0;


        Clear_Error_Success_Box();
    }

    protected void ddlStudentName_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        txtRollNo.Text = ddlStudentRollNo.Items[ddlStudentName.SelectedIndex].Value;
        Clear_Error_Success_Box();
    }


   
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlTestName.Items.Clear();
        ddlBatch.Items.Clear();
        ddlCentre.Items.Clear();
        ddlAcadYear.SelectedIndex = 0;
        ddlConductNo.SelectedIndex = 0;        
        ddlDivision.SelectedIndex = 0;        
        id_date_range_picker_1.Value = "";
        ddlStandard.Items.Clear();
        
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //Print_Data();        
        foreach (DataListItem dtlItem in dlPrintStudent.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkStudent");
            chkitemck.Checked = false;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalPrintStudSelection();", true);
    }
    protected void ddlConductNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_TestName();
        Clear_Error_Success_Box();

    }

    protected void btnEmail_Click(object sender, System.EventArgs e)
    {
        FillGridStudent();
        btnStudSelect_Mail.Visible = true;
        btnStudSelect_Print.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalStudSelection();", true);
    }

    private void FillGridStudent()
    {
        dlGridStudSelect.DataSource = null;
        dlGridStudSelect.DataBind();

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] {
			new DataColumn("RollNo", typeof(string)),
			new DataColumn("StudentName", typeof(string)),
			new DataColumn("StudentSelFlag", typeof(int)),
			new DataColumn("SBEntryCode", typeof(string)),
			new DataColumn("ParentEMailId", typeof(string))
		});

        int Cnt = 0;
        int StudentSelFlag = 0;
        for (Cnt = 1; Cnt <= ddlStudentName.Items.Count - 1; Cnt++)
        {
            if (ddlStudentName.Items[Cnt].Value == ddlStudentName.SelectedValue.ToString())
            {
                StudentSelFlag = 1;
            }
            else
            {
                StudentSelFlag = 0;
            }
            dt.Rows.Add(ddlStudentRollNo.Items[Cnt].Value, ddlStudentName.Items[Cnt].Text, 0, ddlStudentName.Items[Cnt].Value, ddlStudentEMailId.Items[Cnt].Value);
        }

        dlGridStudSelect.DataSource = dt;
        dlGridStudSelect.DataBind();

    }


    public void All_Student_ChkBox_Selected(object sender, System.EventArgs e)
    {
        //Change checked status of a hidden check box
        chkStudentAllHidden.Checked = !(chkStudentAllHidden.Checked);

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlGridStudSelect.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkStudent");

            chkitemck.Checked = chkStudentAllHidden.Checked;
        }

    }

    protected void btnStudSelect_Mail_Click(object sender, System.EventArgs e)
    {
        Email_PrintAttach();
    }

    public void All_StudentPrint_ChkBox_Selected(object sender, System.EventArgs e)
    {
        //Change checked status of a hidden check box
        chkStudentAllHidden.Checked = !(chkStudentAllHidden.Checked);

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlPrintStudent.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkStudent");

            chkitemck.Checked = chkStudentAllHidden.Checked;
        }

    }
    protected void btnPrintSelectedStud_Click(object sender, EventArgs e)
    {
        Print_Data();
    }
}