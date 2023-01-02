<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true"
    CodeFile="Attendance_ReminderLetter_Print.aspx.cs" Inherits="Attendance_ReminderLetter_Print" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @media print
        {
            h1
            {
                page-break-before: always;
            }
        }
    </style>
    <style type="text/css">
        .ajax__calendar_container
        {
            position: absolute;
            z-index: 400000 !important; /*background-color: #DEF1F4;
        border:solid 0px #77D5F7;*/
            border: 0px solid #646464;
            background-color: White;
            color: red;
        }
    </style>
    <style type="text/css">
        .rightAlign
        {
            text-align: right;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function Selectall_Deselectall(CheckBoxControl) {
            if (CheckBoxControl.checked == true) {

                var i;
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    if ((document.forms[0].elements[i].type == 'checkbox') && (document.forms[0].elements[i].name.indexOf('ckhselect1') > -1)) {
                        document.forms[0].elements[i].checked = true;
                    }
                }
            }
            else {
                var i;

                for (i = 0; i < document.forms[0].elements.length; i++) {
                    if ((document.forms[0].elements[i].type == 'checkbox') && (document.forms[0].elements[i].name.indexOf('ckhselect1') > -1)) {
                        document.forms[0].elements[i].checked = false;
                    }
                }
            }
        }
        function PrintDiv1() {
            var contents = document.getElementById("PrintSlip").innerHTML;
            var frame1 = document.createElement('iframe');
            frame1.name = "frame1";
            frame1.style.position = "absolute";
            frame1.style.top = "-1000000px";
            document.body.appendChild(frame1);
            var frameDoc = (frame1.contentWindow) ? frame1.contentWindow : (frame1.contentDocument.document) ? frame1.contentDocument.document : frame1.contentDocument;
            frameDoc.document.open();
            frameDoc.document.write('<html><head><title></title>');
            frameDoc.document.write('</head><body>');
            frameDoc.document.write(contents);
            frameDoc.document.write('</body></html>');
            frameDoc.document.close();
            setTimeout(function () {
                window.frames["frame1"].focus();
                window.frames["frame1"].print();
                document.body.removeChild(frame1);
            }, 500);
            return false;
        }

        function CertificateSelection1() {
            $('#CertificateSelection').modal({
                backdrop: 'static'
            })

            $('#CertificateSelection').modal('show');

        };

        function CertificateSelection_Hide() {
            $('#CertificateSelection').modal({
                backdrop: 'static'
            })

            $('#CertificateSelection').modal('hide');

        };


        function CertificateSelection2() {
            $('#cerifi_Management').modal({
                backdrop: 'static'
            })

            $('#cerifi_Management').modal('show');

        };

        function CertificateSelection2_Hide() {
            $('#cerifi_Management').modal({
                backdrop: 'static'
            })

            $('#cerifi_Management').modal('hide');

        };








    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">
                    Attendance Reminder Letter<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" 
                onclick="BtnShowSearchPanel_Click" />
        </div>
        <!--#nav-search-->
    </div>
    <div id="page-content" class="clearfix">
        <!--/page-header-->
        <div class="row-fluid">
            <!-- -->
            <!-- PAGE CONTENT BEGINS HERE -->
            <asp:UpdatePanel ID="UpdatePanelMsgBox" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="alert alert-block alert-success" id="Msg_Success" visible="false" runat="server">
                        <button type="button" class="close" data-dismiss="alert">
                            <i class="icon-remove"></i>
                        </button>
                        <p>
                            <strong><i class="icon-ok"></i></strong>
                            <asp:Label ID="lblSuccess" runat="server" Text="Label"></asp:Label>
                        </p>
                    </div>
                    <div class="alert alert-error" id="Msg_Error" visible="false" runat="server">
                        <button type="button" class="close" data-dismiss="alert">
                            <i class="icon-remove"></i>
                        </button>
                        <p>
                            <strong><i class="icon-remove"></i>Error!</strong>
                            <asp:Label ID="lblerror" runat="server" Text="Label"></asp:Label>
                        </p>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="DivSearchPanel" runat="server">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5>
                            Search Options
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label115" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" AutoPostBack="True"
                                                                    data-placeholder="Select Division" CssClass="chzn-select" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label116" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcadYear" Width="215px" AutoPostBack="True"
                                                                    data-placeholder="Select Acad Year" CssClass="chzn-select" OnSelectedIndexChanged="ddlAcadYear_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label117" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlStandard" Width="215px" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label18" CssClass="red">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCentre" Width="215px" data-placeholder="Select Center"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCentre_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label29" CssClass="red">  Test Period</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span8" name="date-range-picker"
                                                                    style="width: 205px" id="id_date_range_picker_1" placeholder="Date Search" data-placement="bottom"
                                                                    data-original-title="Date Range" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label28" CssClass="red">Batch</asp:Label>
                                                            </td>
                                                            <%--    <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlBatch" Width="215px" data-placeholder="Select Batch"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" />
                                                            </td>--%>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlBatch" Width="215px" data-placeholder="Select Batch"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="well" style="text-align: center; background-color: #F0F0F0">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch"
                                    Text="Search" ToolTip="Search" OnClick="BtnSearch_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" onclick="BtnClearSearch_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--<div id="DivResultPanel" runat="server" class="dataTables_wrapper">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton ID="HLExport" Font-Underline="true" Height="25px" ToolTip="Export"
                                        class="btn-small btn-danger icon-2x icon-download-alt" runat="server" />
                                    &nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                    <tr>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label21">Division</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblDivision_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label27">Academic Year</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblAcadYear_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label215">Centre</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblCentre_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label216">Course</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblStandard_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label217">Batch</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblBatch" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label1">Period</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblPeriod" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:DataList ID="dlGridDisplay" runat="server" HorizontalAlign="Left" CssClass="table table-striped table-bordered table-hover"
                    Width="100%">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkStudentAllAdd" runat="server" AutoPostBack="true" OnCheckedChanged="chkStudentAllAdd_CheckedChanged" /><span
                            class="lbl"></span> </th><th>
                            <b>Test Date</b> </th>
                        <th align="left" style="width: 9%">
                            Test Code
                        </th>
                        <th align="left" style="width: 8%">
                            Student Name
                        </th>
                        <th align="left" style="width: 8%">
                            Roll No
                        </th>
                        <th align="left" style="width: 11%">
                            Subject
                        </th>
                        <th style="width: 20%; text-align: center">
                            Center
                        </th>
                        <th style="width: 13%; text-align: center">
                            Course
                        </th>
                        <th style="width: 13%; text-align: center">
                        Batch Name
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkStudent" runat="server" /><span class="lbl"></span> </td>
                        <td>
                            <asp:Label ID="Label33" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Date")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_ID")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label26" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Source_Center_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                    </ItemTemplate>
                </asp:DataList>
                
                       <div class="widget-main alert-block alert-success  alert- " style="text-align: center;" visible="false">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="lblerrorDivision" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnPrintAdd" runat="server"
                            Text="Print"  />
                       
                    </div>
            </div>--%>
            <div id="DivResultPanel" runat="server" class="dataTables_wrapper" visible="true">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="HLExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" Visible="true" onclick="HLExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <asp:Label runat="server" ID="Label21">Division</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblDivision_Result" class="blue"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <asp:Label runat="server" ID="Label27">Academic Year</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblAcadYear_Result" class="blue"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <asp:Label runat="server" ID="Label215">Centre</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblCentre_Result" class="blue"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <asp:Label runat="server" ID="Label216">Course</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblStandard_Result" class="blue"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <asp:Label runat="server" ID="Label217">Batch</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblBatch" class="blue"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <asp:Label runat="server" ID="Label1">Period</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblPeriod" class="blue"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:DataList ID="dlGridDisplay" runat="server" Width="100%">
                    <HeaderTemplate>
                        <table class="table table-striped table-bordered table-hover ">
                            <thead>
                                <tr>
                                    <td>
                                        <b>
                                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="AllChk_Selected" />
                                            <span class="lbl"></span></b></th>
                                        <th style="width: 25%; text-align: center;">
                                            <b>Student Name</b>
                                        </th>
                                       
                                        <th align="left" style="width: 15%">
                                            Roll No
                                        </th>
<%--                                        <th align="left" style="width: 20%">
                                            Subject
                                        </th>--%>
                                        <th style="width: 20%; text-align: center">
                                            Center
                                        </th>
                                        <th style="width: 20%; text-align: center">
                                            Course
                                        </th>
                                        <th style="width: 20%; text-align: center">
                                    Batch Name
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkStudent" runat="server" /><span class="lbl"></span> </td>
                      <%--  <td>
                            <asp:Label ID="lblDLTestDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Date")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblDLTestName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_ID")%>' />
                             
                        </td>--%>
                        <td>
                            <asp:Label ID="lblPrint_StudentName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                            <asp:Label ID="lblStudentName" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblStudentRollNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                             <asp:Label ID="lblSBEntryCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>' />

                        </td>
                  
                        <td>
                            <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Source_Center_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody> </table>
                    </FooterTemplate>
                </asp:DataList>
                <asp:DataList ID="dlGridExport" runat="server"  Visible="false" Width="100%">
                    <HeaderTemplate>
                        <table class="table table-striped table-bordered table-hover ">
                            <thead>
                                <tr>
                                    <td>
                                    
                                            <b>Student Name</b>
                                        </th>
                                       
                                        <th align="left" style="width: 15%">
                                            Roll No
                                        </th>
<%--                                        <th align="left" style="width: 20%">
                                            Subject
                                        </th>--%>
                                        <th style="width: 20%; text-align: center">
                                            Center
                                        </th>
                                        <th style="width: 20%; text-align: center">
                                            Course
                                        </th>
                                        <th style="width: 20%; text-align: center">
                                    Batch Name
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                       
                            <asp:Label ID="lblPrint_StudentName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                            <asp:Label ID="lblStudentName" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblStudentRollNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                             <asp:Label ID="lblSBEntryCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>' />

                        </td>
                  
                        <td>
                            <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Source_Center_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody> </table>
                    </FooterTemplate>
                </asp:DataList>
                <asp:DataList ID="dlGrid" runat="server" Width="100%" Visible="false">
                    <HeaderTemplate>
                        <table class="table table-striped table-bordered table-hover ">
                            <thead>
                                <tr>
                                    <td>

                                            <b>Test Date</b>
                                        </th>
                                       
                                        <th align="left" style="width: 30%">
                                            Subject
                                        </th>
                                        <th align="left" style="width: 30%">
                                            Exam Type
                                        </th>
                                        <th align="left" style="width: 20%">
                                            Chapter
                                       
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                 
                            <asp:Label ID="lblDLTestDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Date")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblDLSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
 
                        </td>
                        <td>
                            <asp:Label ID="lblexamType" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem,"[Exam Type]")%>' />
                             <asp:Label ID="lblSBEntryCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>' />
                        </td>
                        <td>
                           <asp:Label ID="lblchapter" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem,"Chapters")%>' />
                           <asp:Label ID="lblPartner" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Parent_Name")%>' />
                      
                       
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody> </table>
                    </FooterTemplate>
                </asp:DataList>
                <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                    <!--Button Area -->
                   <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="Btprint" runat="server"
                        Text="Print" 
                        onclick="Btprint_Click"  />
                    <%--<a href="#" onclick="javascript:PrintDiv1();" runat="server" id="A1" title="Print Paper Corrector Slip"
                        class="btn btn-app btn-warning btn-mini radius-4 icon-print" style="height: 25px">--%>
                    </a>
                    <%-- <asp:LinkButton runat="server" ID="btnPrint" ToolTip="Print" Height="25px" CommandName="print" 
                        OnClick="Btprint_Click" />--%>
                   <%-- <asp:Button ID="BtnCloseAdd" Visible="true" runat="server" class="btn btn-app btn-primary btn-mini radius-4"
                        Text="Close" />--%>
                </div>
            </div>
        </div>
    </div>
    <div id="PrintSlip" style="margin: 0px auto 0px auto; width: 800px; border: 1px solid Gray;
        display: none; visibility: hidden; font-family: Arial">
        <table width="100%" border="3" cellpadding="0" cellspacing="0" style="border-color: Black;
            font-family: Arial;">
            <tr>
                <td colspan="3" style="border-color: Black; padding: 20px 20px 20px 20px; font-family: Arial;">
                    <table cellpadding="3" width="100%">
                        <tr>
                            <td class="span4" style="text-align: left">
                            </td>
                            <td class="span4" style="text-align: center">
                                <p style="font-size: 20px;">
                                    MT EDUCARE LTD - Science
                                </p>
                            </td>
                            <td class="span4" style="text-align: left">
                                <img src="images/logo.jpg" alt="" style="width: 146px; height: 70px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="span12" colspan="3">
                                <b><span style="font-size: 12px;">Stream :</span>
                                    <asp:Label runat="server" ID="lblCourse" Style="font-size: 12px;"></asp:Label></b>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" colspan="2">
                                <b><span style="font-size: 12px;">Batch :</span>
                                    <asp:Label runat="server" ID="lblBatchPrint1" Style="font-size: 12px;"></asp:Label></b>
                            </td>
                            <td class="span6" colspan="3">
                                <b><span style="font-size: 12px;">Date :</span>
                                    <asp:Label runat="server" ID="lbldate" Style="font-size: 12px;"></asp:Label></b>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                            </td>
                            <td class="span4" style="text-align: center">
                                <p style="font-size: 20px;">
                                    Attendance Reminder</p>
                            </td>
                            <td class="span4" style="text-align: left">
                            </td>
                        </tr>
                          <tr>
                            <td class="span4" style="text-align: left">
                                <asp:Label runat="server" ID="lblParent" Style="font-size: 12px;"></asp:Label></b>
                            </td>
                        </tr>
                        </table>
                        <table>
                  <%--      <tr>
                            <td class="span4" style="text-align: left">
                            <p style="font-size: 20px;">
                                Dear Parents</p>
                                </td>
                        </tr>--%>
                       <%-- <tr>
                        <td class="span4" style="text-align: left">
                            <p style="font-size: 12px;">
                                This is to bring to your notice that your Word  <asp:Label runat="server" ID="lblstudent" Style="font-size: 12px;"></asp:Label>
                                has not appeared in this following tests held from   <asp:Label runat="server" ID="lbldateptint1" Style="font-size: 12px;"> </asp:Label></p>
                                </td>
                        </tr>--%>
                     
                        <tr>
                            <td class="span12" colspan="3" style="height: 30px">
                            </td>
                        </tr>
                        <%--<tr>
                            <td class="span4" style="text-align: left">
                                <span style="font-size: 12px;">Office Staff(Sign)</span>
                            </td>
                            <td class="span4" style="text-align: center">
                                <span style="font-size: 12px;">Office (Stamp)</span>
                            </td>
                            <td class="span4" style="text-align: right">
                                <span style="font-size: 12px;">Paper Corrector(Sign)</span>
                            </td>
                        </tr>--%>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
