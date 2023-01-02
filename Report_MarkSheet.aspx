<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true"
    CodeFile="Report_MarkSheet.aspx.cs" Inherits="Report_MarkSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        }

        function openModalStudSelection() {
            $('#DivStudSelection').modal({
                backdrop: 'static'
            })

            $('#DivStudSelection').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>Report<span class="divider"> <i class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Report Card – Detailed<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
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
                                                                <asp:Label runat="server" CssClass="red" ID="Label115">Division</asp:Label>
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
                                                                <asp:Label runat="server" CssClass="red" ID="Label116">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcadYear" Width="215px" AutoPostBack="True"
                                                                    data-placeholder="Select Acad Year" CssClass="chzn-select" OnSelectedIndexChanged="ddlAcadYear_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" CssClass="red" ID="Label18">Centre</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCentre" Width="215px" data-placeholder="Select Centre"
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
                                                                <asp:Label runat="server" CssClass="red" ID="Label117">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlStandard" Width="215px" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label28">Batch</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlBatch" Width="215px" data-placeholder="Select Batch"
                                                                    OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" CssClass="chzn-select"
                                                                    SelectionMode="Multiple" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" CssClass="red" ID="Label19">Test Category</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlTestCategory" Width="215px" data-placeholder="Select Test Category"
                                                                    OnSelectedIndexChanged="ddlTestCategory_SelectedIndexChanged" CssClass="chzn-select"
                                                                    AutoPostBack="True" />
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
                                                                <asp:Label runat="server"  ID="Label29">  Test Period</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span8" name="date-range-picker"
                                                                    style="width: 208px" id="id_date_range_picker_1" placeholder="Date Search" data-placement="bottom"
                                                                    data-original-title="Date Range" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" CssClass="red" ID="Label20">Test Type</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlTestType" Width="215px" data-placeholder="Select Test Type"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="True" OnSelectedIndexChanged="ddlTestType_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" CssClass="red" ID="Label30">Test Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlTestName" Width="215px" data-placeholder="Select Test Name"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"
                                                                    onselectedindexchanged="ddlTestName_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="span8" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 19.25%;">
                                                                <asp:Label runat="server" CssClass="red" ID="Label211">Student Name</asp:Label>
                                                                <asp:LinkButton ID="btnStudentName" ToolTip="Get Student Names" class="btn-small btn-primary icon-refresh" 
                                                                    OnClick="btnStudentName_Click" runat="server"  Visible="false"/>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 80.75%;">
                                                                <asp:DropDownList runat="server" ID="ddlStudentName" Width="215px" data-placeholder="Select Student"
                                                                    OnSelectedIndexChanged="ddlStudentName_SelectedIndexChanged" CssClass="chzn-select"
                                                                    AutoPostBack="True" />
                                                                <asp:DropDownList runat="server" ID="ddlStudentRollNo" Width="215px" Visible="false" />
                                                                <asp:DropDownList runat="server" ID="ddlStudentEMailId" Width="215px" Visible="false" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label11">Roll No</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtRollNo" Width="200px" ReadOnly="true" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                    <asp:PostBackTrigger ControlID="btnStudentName" />
                                    </Triggers>
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
            <div id="DivResultPanel" runat="server" class="dataTables_wrapper">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="btnPrint" ToolTip="Print" class="btn-small btn-warning icon-2x icon-print"
                                        OnClick="btnPrint_Click" Height="25px" />
                                    <asp:LinkButton runat="server" ID="btnEmail" ToolTip="Email" class="btn-small btn-danger icon-2x icon-envelope-alt"
                                        Height="25px" OnClick="btnEmail_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
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
                                        <asp:Label runat="server" ID="Label217">Test Category</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblTestCategory_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label15">Test Type(s)</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblTestType_Result" class="blue"></asp:Label>
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
                                        <asp:Label runat="server" ID="Label17">Test Name(s)</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblTestName_Result" class="blue"></asp:Label>
                                        <asp:Label runat="server" ID="lblTestID_Result" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label35">Student Name</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblStudentName_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label37">Roll No</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblRollNo_Result" class="blue"></asp:Label>
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
                                        <asp:Label runat="server" ID="Label4">  Test Period</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblTestPeriod" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div id="DivResult_MCQ" runat="server">
                    <div class="tabbable">
                        <ul class="nav nav-tabs" id="Ul1">
                            <li class="active"><a data-toggle="tab" href="#AnswerKey">Test Summary </a></li>
                            <li><a data-toggle="tab" href="#StudentResult">Details of Answering </a></li>
                            <li><a data-toggle="tab" href="#ErrorRecords">Overall Toppers </a></li>
                        </ul>
                        <div class="tab-content" style="border: 1px solid #DDDDDD; overflow: hidden">
                            <div id="AnswerKey" class="tab-pane in active">
                                <asp:DataList ID="dlGridSummaryReport" CssClass="table table-striped table-bordered table-hover"
                                    runat="server" Width="100%">
                                    <HeaderTemplate>
                                        <b>Test Date</b> </th>
                                        <th style="width: 28%; text-align: left">
                                            Test Name
                                        </th>
                                        <th style="width: 28%; text-align: left">
                                            Subject
                                        </th>
                                        <th style="width: 5%; text-align: left">
                                            Attendance
                                        </th>
                                        <th style="width: 5%; text-align: center">
                                            Score
                                        </th>
                                        <th style="width: 5%; text-align: center">
                                            Out of
                                        </th>
                                        <th style="width: 5%; text-align: center">
                                            Percent
                                        </th>
                                        <th style="width: 5%; text-align: center">
                                            Centre Rank
                                        </th>
                                        <th style="width: 7%; text-align: center">
                                        Overall Rank
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDLTestDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Date")%>' />
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblDLTestName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />                                           
                                            <%--<asp:Label ID="lblDLTestConduct" runat="server" Text='<%# (System.Convert.ToInt32(Eval("Conduct_No")) == 1 ? " " : "*" ) %>' />--%>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblDLSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblDlAttendStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AttendStatus")%>' />
                                        </td>

                                        <td style="text-align: center">
                                            <asp:Label ID="lblDLMarksObtd" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Obtd_Marks")%>' />
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblDLMarksOutOf" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OutOf_Marks")%>' />
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblDLPercent" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Percent_Marks")%>' />
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblDLCentreRank" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CentreRank")%>' />
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblDLOvarllRank" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OverallRank")%>' />
                                            <asp:Label ID="lblDlCenter_Highest_Mark" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center_Highest_Mark")%>' />
                                            
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                            <div id="StudentResult" class="tab-pane">
                                <asp:DataList ID="dlGridDetailsofAnswering" CssClass="table table-striped table-bordered table-hover"
                                    runat="server" Width="100%">
                                    <HeaderTemplate>
                                        <left><b>Test Name</b></left>
                                        </th>
                                        <th style="width: 20%; text-align: left;">
                                            Subject
                                        </th>
                                        <th style="width: 10%; text-align: center;">
                                            Status
                                        </th>
                                        <th style="width: 6%; text-align: center;">
                                            Count
                                        </th>
                                        <th style="width: 18%; text-align: left;">
                                            Que No - Easy
                                        </th>
                                        <th style="width: 18%; text-align: left;">
                                            Que No - Moderate
                                        </th>
                                        <th style="width: 18%; text-align: left;">
                                        Que No - Difficult
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDLTestName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblDLSubjectName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblDLResultStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ResultStatus")%>' />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblDLResultCount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ResultCount")%>' />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblDLEasy" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Easy")%>' />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblDLModerate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Moderate")%>' />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblDLDifficult" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Difficult")%>' />
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                            <div id="ErrorRecords" class="tab-pane">
                                <asp:DataList ID="dlGridOverallToppers" CssClass="table table-striped table-bordered table-hover"
                                    runat="server" Width="100%">
                                    <HeaderTemplate>
                                        <left><b>Test Name</b></left>
                                        </th>
                                        <th style="width: 20%; text-align: left;">
                                            Subject
                                        </th>
                                        <th style="width: 35%; text-align: left;">
                                            Name of Student
                                        </th>
                                        <th style="width: 25%; text-align: left;">
                                            Centre
                                        </th>
                                        <th style="width: 10%; text-align: center;">
                                        Score
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDLTestName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblDLSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblDLStudentName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblDLCentre" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Centre_Name")%>' />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblDLScore" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Obtd_Marks")%>' />
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="DivPrint" runat="server" visible="true" style="height: 0px; overflow: hidden;">
                    <table style="width: 410mm" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 9pt">
                                <table style="width: 100%" border="1" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="text-align: left;" colspan="1" height="10mm">
                                            <%--<img id ="imgPrint_Logo" runat ="server" src="../images/LEPL-LOGO.jpg" alt ="LEPL" />--%>
                                            <asp:Label ID="Label2" runat="server" Text="LEPL" Font-Size="14pt" Font-Bold="True" />
                                        </td>
                                        <td style="text-align: right; vertical-align: bottom; background-color: #00FFFF;"
                                            colspan="3">
                                            <asp:Label ID="Label1" runat="server" Text="STATEMENT OF MARKS" Font-Size="14pt"
                                                Font-Bold="True" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-width: 1px; font-family: Arial, Helvetica, sans-serif; font-size: 9pt;
                                border-top-style: solid;">
                                <table style="width: 210mm" border="1" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="10%" style="text-align: left; font-size: 9pt; font-family: Arial, Helvetica, sans-serif;">
                                            Student Name :
                                        </td>
                                        <td width="50%" style="text-align: left; font-size: 9pt; font-family: Arial, Helvetica, sans-serif;">
                                            <asp:Label ID="lblPrint_StudentName" runat="server" Text="" Font-Bold="True" />
                                        </td>
                                        <td width="10%" style="text-align: left; font-size: 9pt; font-family: Arial, Helvetica, sans-serif;">
                                            Roll No :
                                        </td>
                                        <td width="10%" style="text-align: left; font-size: 9pt; font-family: Arial, Helvetica, sans-serif;">
                                            <asp:Label ID="lblPrint_RollNo" runat="server" Text="" Font-Bold="True" />
                                        </td>
                                        <td width="10%" style="text-align: left; font-size: 9pt; font-family: Arial, Helvetica, sans-serif;">
                                            Center :
                                        </td>
                                        <td width="10%" style="text-align: left; font-size: 9pt; font-family: Arial, Helvetica, sans-serif;">
                                            <asp:Label ID="lblPrint_Center" runat="server" Text="" Font-Bold="True" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 9pt">
                                <asp:DataList ID="dlPrint_Summary" runat="server" Width="100%" Font-Size="9pt">
                                    <HeaderTemplate>
                                        Test Date </th>
                                        <th style="width: 30%; text-align: left">
                                            Test Name
                                        </th>
                                        <th style="width: 30%; text-align: left">
                                            Subject
                                        </th>
                                        <th style="width: 5%; text-align: left">
                                            Attendance
                                        </th>
                                        <th style="width: 5%; text-align: center">
                                            Score
                                        </th>
                                        <th style="width: 5%; text-align: center">
                                            Out of
                                        </th>
                                        <th style="width: 5%; text-align: center">
                                            Percent
                                        </th>
                                        <th style="width: 5%; text-align: center">
                                            Centre Rank
                                        </th>
                                        <th style="width: 5%; text-align: center">
                                        Overall Rank
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDLTestDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Date")%>' />
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblDLTestName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblDLSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                                        </td>

                                        <td style="text-align: center">
                                            <asp:Label ID="lblDLAttendStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AttendStatus")%>' />
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblDLMarksObtd" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Obtd_Marks")%>' />
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblDLMarksOutOf" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OutOf_Marks")%>' />
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblDLPercent" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Percent_Marks")%>' />
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblDLCentreRank" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CentreRank")%>' />
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblDLOvarllRank" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OverallRank")%>' />
                                            <asp:Label ID="lblDlCenter_Highest_Mark" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center_Highest_Mark")%>' />
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 9pt">
                                <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="border-style: solid; border-width: 1px; text-align: left; font-size: 11pt;
                                            font-weight: bold; font-family: arial, Helvetica, sans-serif; width: 100mm;">
                                            Details of Answering
                                        </td>
                                        <td style="text-align: left; font-size: 11pt; font-weight: bold; font-family: arial, Helvetica, sans-serif;
                                            width: 110mm;">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 9pt">
                                <asp:DataList ID="dlPrint_Answering" runat="server" Width="100%" Font-Size="9pt">
                                    <HeaderTemplate>
                                        <left>Test Name</left>
                                        </th>
                                        <th style="width: 20%; text-align: left;">
                                            Subject
                                        </th>
                                        <th style="width: 10%; text-align: center;">
                                            Status
                                        </th>
                                        <th style="width: 6%; text-align: center;">
                                            Count
                                        </th>
                                        <th style="width: 18%; text-align: left;">
                                            Que No - Easy
                                        </th>
                                        <th style="width: 18%; text-align: left;">
                                            Que No - Moderate
                                        </th>
                                        <th style="width: 18%; text-align: left;">
                                        Que No - Difficult
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDLTestName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblDLSubjectName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblDLResultStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ResultStatus")%>' />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblDLResultCount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ResultCount")%>' />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblDLEasy" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Easy")%>' />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblDLModerate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Moderate")%>' />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblDLDifficult" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Difficult")%>' />
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 9pt">
                                <table style="width: 100%" border="1" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="100%" valign="middle" align="left">
                                            Overall Topper
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 9pt">
                                <asp:DataList ID="dlPrint_Topper" runat="server" Width="100%" Font-Size="9pt">
                                    <HeaderTemplate>
                                        <left>Test Name</left>
                                        </th>
                                        <th style="width: 20%; text-align: left;">
                                            Subject
                                        </th>
                                        <th style="width: 35%; text-align: left;">
                                            Name of Student
                                        </th>
                                        <th style="width: 25%; text-align: left;">
                                            Centre
                                        </th>
                                        <th style="width: 10%; text-align: center;">
                                        Score
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDLTestName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblDLSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblDLStudentName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblDLCentre" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Centre_Name")%>' />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblDLScore" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Obtd_Marks")%>' />
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <!--/row-->
    <div class="modal fade" id="DivStudSelection" style="left: 50% !important; top: 10% !important;
        display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel_StudSelect" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title">
                                <asp:Label runat="server" ID="lblStudSelect_Header">Select Students</asp:Label>
                            </h4>
                            <asp:Label runat="server" Visible="false" ID="lblStudAttend_Action"></asp:Label>
                            <asp:CheckBox ID="chkStudentAllHidden" runat="server" Visible="False" />
                        </div>
                        <div class="modal-body" style="height: 250px">
                            <asp:DataList ID="dlGridStudSelect" CssClass="table table-striped table-bordered table-hover"
                                runat="server" Width="100%">
                                <HeaderTemplate>
                                    <b>
                                        <asp:CheckBox ID="chkStudentAll" runat="server" AutoPostBack="True" OnCheckedChanged="All_Student_ChkBox_Selected" />
                                        <span class="lbl"></span></b></th>
                                    <th style="width: 20%; text-align: center">
                                        Roll Number
                                    </th>
                                    <th style="width: 70%; text-align: left">
                                    Student Name
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <%-- <asp:CheckBox ID="chkStudent" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem,"StudentSelFlag")%>' />--%>

                                   <asp:CheckBox ID="chkStudent" runat="server" Checked='<%#(System.Convert.ToInt32(Eval("StudentSelFlag")) == 1 ? true : false )%>' />
                                    <%--<asp:Label ID="lblDLTestConduct" runat="server" Text='<%# (System.Convert.ToInt32(Eval("Conduct_No")) == 1 ? " " : "*" ) %>' />--%>
                                    <span class="lbl"></span></td>
                                    <td style="width: 20%; text-align: center">
                                        <asp:Label ID="lblStudentRollNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                                    </td>
                                    <td style="width: 70%; text-align: left">
                                        <asp:Label ID="lblStudentName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                                        <asp:Label ID="lblSBEntryCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>'
                                            Visible="False" />
                                        <asp:Label ID="lblStudentEmail" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ParentEMailId")%>'
                                            Visible="False" />
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-footer">
                    <table cellpadding="0" style="border-style: none;" width="100%">
                        <tr>
                            <td style="border-style: none; text-align: left; width: 30%;">
                                <asp:Label runat="server" ID="Label3">Print Overall Rank</asp:Label>
                            </td>
                            <td style="border-style: none; text-align: left; width: 30%;">
                                <label>
                                    <input runat="server" id="chkOverallRankFlag" name="switch-field-1" type="checkbox"
                                        class="ace-switch ace-switch-2" />
                                    <span class="lbl"></span>
                                </label>
                            </td>
                            <td style="border-style: none; width: 40%;">
                                <!--Button Area -->
                                <asp:Label runat="server" ID="Label13" Text="" Visible="false" />
                                <asp:Button class="btn btn-app  btn-warning btn-mini radius-4" ID="btnStudSelect_Print"
                                    OnClick="btnStudSelect_Print_Click" ToolTip="Print" runat="server" Text="Print" />
                                <asp:Button class="btn btn-app  btn-danger btn-mini radius-4" ID="btnStudSelect_Mail"
                                    ToolTip="Mail" runat="server" Text="Mail" Visible="false" OnClick="btnStudSelect_Mail_Click" />
                                <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                                    ID="btnStudSelect_Close" ToolTip="Cancel" runat="server" Text="Cancel" OnClick="btnStudSelect_Close_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!--/#page-content-->
</asp:Content>
