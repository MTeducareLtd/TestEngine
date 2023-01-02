<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="Rpt_Facultywise_TestPerformance_Summary.aspx.cs" Inherits="Rpt_Facultywise_TestPerformance_Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
  <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>Report<span class="divider"> <i class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                   Facultywise Test Performance Summary<span class="divider"></span></h5>
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
                                                                <asp:Label runat="server" ID="Label115">Division</asp:Label>
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
                                                                <asp:Label runat="server" ID="Label116">Academic Year</asp:Label>
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
                                                                <asp:Label runat="server" ID="Label117">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlStandard" Width="215px" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged" />
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
                                                                <asp:Label runat="server" ID="Label18">Centre</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                               
                                                                    <asp:ListBox runat="server" ID="ddlCentre" Width="215px" data-placeholder="Select Center"
                                                                   CssClass="chzn-select"
                                                                    SelectionMode="Multiple" AutoPostBack="True" OnSelectedIndexChanged="ddlCentre_SelectedIndexChanged" />
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
                                                                <asp:Label runat="server" ID="Label19">Test Category</asp:Label>
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
                                                                <asp:Label runat="server" ID="Label20">Test Type</asp:Label>
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
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label29">  Test Period</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span8" name="date-range-picker"
                                                                    id="id_date_range_picker_1" placeholder="Date Search" data-placement="bottom"
                                                                    data-original-title="Date Range" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label30">Test Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlTestName" Width="215px" data-placeholder="Select Test Name"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" />
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
            <div id="DivResultPanel" runat="server" class="dataTables_wrapper" visible="false">
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
                           
                        </td>
                    </tr>
                    <tr>
                        <td class="span4" style="text-align: left">
                            
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
                   
                        <div class="tab-content" style="border: 1px solid #DDDDDD; overflow: hidden">
                            <div id="AnswerKey" class="tab-pane in active">
                                <asp:DataList ID="dlGridSummaryReport" CssClass="table table-striped table-bordered table-hover"
                                    runat="server" Width="100%">
                                    <HeaderTemplate>
                                        <b>Sr.No</b> </th>
                                        <th style="width: 28%; text-align: left">
                                            Faculty Name
                                        </th>
                                        <th style="width: 28%; text-align: left">
                                            Faculty Short Name
                                        </th>
                                        <th style="width: 5%; text-align: left">
                                            Above 90%
                                        </th>
                                        <th style="width: 5%; text-align: center">
                                            Above 80-90%
                                        </th>
                                        <th style="width: 5%; text-align: center">
                                          Above 70-80%
                                        </th>
                                        <th style="width: 5%; text-align: center">
                                           Above 60-70%
                                        </th>
                                        <th style="width: 5%; text-align: center">
                                         Below 60%
                                        </th>
                                        <th style="width: 7%; text-align: center">
                                         Present %
                                         </th>
                                         <th>
                                          Absent %
                                         
                                           
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
                                            </td>
                                            <td>
                                                 <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CentreRank")%>' />
                                             
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
              
            </div>
        </div>
    </div>
    <!--/row-->
    


</asp:Content>

