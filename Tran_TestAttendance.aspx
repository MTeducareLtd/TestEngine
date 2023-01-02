<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true"
    CodeFile="Tran_TestAttendance.aspx.cs" Inherits="Tran_TestAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        }

        function openModalStudAttend() {
            $('#DivStudAttendance').modal({
                backdrop: 'static'
            })

            $('#DivStudAttendance').modal('show');
        }

        function openModalTestSMS() {
            $('#Test_SMS').modal({
                backdrop: 'static'
            })

            $('#Test_SMS').modal('show');
        }; 

    </script>
    <style type="text/css">
        .style1
        {
            width: 231px;
        }
        .style2
        {
            width: 152px;
        }
        .style3
        {
            width: 40%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Manage Test Attendance<span class="divider"></span></h5>
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
                                                                <asp:Label runat="server" ID="Label115" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" />
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
                                                                <asp:Label runat="server" ID="Label117" CssClass="red">Course</asp:Label>
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
                                                                    CssClass="chzn-select" SelectionMode="Multiple" />
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
                                                                <asp:TextBox runat="server" ID="txtTestName" ToolTip="Test Name" type="text" Width="205px" />
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
                                                                <asp:Label runat="server" ID="Label29">  Test Period</asp:Label>
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
                                                                <asp:Label runat="server" ID="Label19" CssClass="red">Test Category</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlTestCategory" Width="215px" data-placeholder="Select Test Category"
                                                                    CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label20">Test Type</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlTestType" Width="215px" data-placeholder="Select Test Type"
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
                                    runat="server" Text="Clear" OnClick="BtnClearSearch_Click" />
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
                                    <asp:LinkButton ID="HLExport" Font-Underline="true" Height="25px" ToolTip="Export"
                                        class="btn-small btn-danger icon-2x icon-download-alt" runat="server" OnClick="HLExport_Click" />
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
                                        <asp:Label runat="server" ID="Label217">Test Category</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblTestCategory_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                        </td>
                    </tr>
                </table>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                    <HeaderTemplate>
                        <b>Test Name</b> </th>
                        <th style="width: 10%; text-align: center">
                            Conduct No
                        </th>
                        <th align="left" style="width: 10%">
                            Batch
                        </th>
                        <th align="left" style="width: 10%">
                            Test Type
                        </th>
                        <th align="left" style="width: 15%">
                            Subjects
                        </th>
                        <th style="width: 10%; text-align: center">
                            Max Marks
                        </th>
                        <th style="width: 10%; text-align: center">
                            Test Date
                        </th>
                        <th style="width: 10%; text-align: center">
                            Test Time
                        </th>
                        <th style="width: 70px; text-align: center">
                        Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                        <span id="iconDL_Authorise" runat="server" visible='<%#(int)DataBinder.Eval(Container.DataItem,"AttendClosureStatus_Flag") == 1 ? true : false%>'
                            class="btn btn-danger btn-mini tooltip-error" data-rel="tooltip" data-placement="right"
                            title="Attendance Authorised"><i class="icon-lock"></i></span></td>
                        <td style="width: 10%; text-align: center">
                            <asp:Label ID="lblDLConductNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Conduct_No")%>' />
                            <asp:Label ID="lblDLAuthoriseFlag" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AttendClosureStatus_Flag")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblDLBatchName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblDLTestType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestType_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblDLSubjects" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subjects")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblDLMaxMarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MaxMarks")%>' />
                            <asp:Label ID="lblDLRemarks" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Remarks")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblDLTestDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Date")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblDLTestTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestTimeStr")%>' />
                            <asp:Label ID="lblDLFromTime" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"FromTimeStr")%>' />
                            <asp:Label ID="lblDLToTime" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"ToTimeStr")%>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:LinkButton ID="lnkEditInfo" ToolTip="Manage" class="btn-small btn-primary icon-info-sign"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server"
                                CommandName="Manage" />
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dlGridExport" Visible="false" runat="server" HorizontalAlign="Left"
                    CssClass="table table-striped table-bordered table-hover" Width="100%">
                    <HeaderTemplate>
                        <b>Test Name</b> </th>
                        <th align="left" style="width: 9%">
                            Conduct No
                        </th>
                        <th align="left" style="width: 8%">
                            Batch
                        </th>
                        <th align="left" style="width: 8%">
                            Test Type
                        </th>
                        <th align="left" style="width: 11%">
                            Subjects
                        </th>
                        <th style="width: 20%; text-align: center">
                            Test Date
                        </th>
                        <th style="width: 13%; text-align: center">
                        Test Time
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Conduct_No")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label33" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestType_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label26" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subjects")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Date")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestTimeStr")%>' />
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <div id="DivAddPanel" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        Test Attendance Details
                    </h5>
                    <asp:Label runat="server" ID="lblTestPKey_Edit" Visible="False"></asp:Label>
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                <tr>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label3">Division</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblDivision_Edit"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label4">Academic Year</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblAcadYear_Edit"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label5">Centre</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblCentre_Edit"></asp:Label>
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
                                                    <asp:Label runat="server" ID="Label23">Course</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblStandard_Edit"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label6">Batch</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblBatch_Edit"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <i class="icon-calendar"></i>
                                                    <asp:Label runat="server" ID="Label1">  Test Date</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblTestDate_Edit"></asp:Label>
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
                                                    <asp:Label runat="server" ID="Label10">Conduct No</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblConductNo_Edit"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label12">Test Name</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblTestName_Edit"></asp:Label>&nbsp;&nbsp;
                                                    <span id="Flag_Authorise" runat="server" visible="false" class="label label-important arrowed">
                                                        Authorised</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <i class="icon-time"></i>
                                                    <asp:Label runat="server" ID="Label2">Test Time</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblTestTime_Edit"></asp:Label>
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
                                                    <asp:Label runat="server" ID="Label9">Maximum Marks</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblMaxMarks_Edit"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label7">Test Category</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblTestCategory_Edit"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label8">Test Type</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblTestType_Edit"></asp:Label>
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
                                                    <asp:Label runat="server" ID="Label32">Subject(s)</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblSubject_Edit"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span8" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none; width: 47%;" class="table-hover">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 19.75%;">
                                                    <asp:Label runat="server" ID="Label31">Remarks</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 80.25%;">
                                                    <asp:Label runat="server" class="blue" ID="lblRemarks_Edit"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span8" style="text-align: left">
                                        <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label ID="Label110" runat="server" Visible="False">Message Template</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label ID="lblMessage_Template_SMS" runat="server" CssClass="blue" Visible="false"></asp:Label>
                                                    <br />
                                                    <button id="Btn_TestMessage" runat="server" class="btn btn-small btn-success radius-4"
                                                        data-rel="tooltip" data-placement="left" title="SMS Preview" visible="false"
                                                        onserverclick="Btn_TestMessage_Click">
                                                        <i class="icon-envelope-alt"></i>
                                                    </button>
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
                                                    <asp:Label runat="server" ID="Label51">For Entity Type</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:DropDownList runat="server" ID="ddlEntityType" Width="142px" data-placeholder="Select Entity Type"
                                                        CssClass="chzn-select" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <asp:Button class="btn  btn-app btn-primary btn-mini radius-4" runat="server" ID="btnSearchAttendance"
                                            Text="Mark Attendance" OnClick="btnSearchAttendance_Click" Width="150px" />
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" class="table-hover">
                                            <tr>
                                                <td class="style2">
                                                    <asp:Label ID="Label63" runat="server">Message Status</asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td class="style1">
                                                    <span id="Flag_Authorise0" runat="server" class="label label-important arrowed" visible="false">
                                                        SMS Sent</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <div id="DivResultPanelLevel2" runat="server" class="dataTables_wrapper" visible="False">
                                <asp:UpdatePanel ID="UpdatePanelMsgBox2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DataList ID="dlGridDisplay_StudAttendance" CssClass="table table-striped table-bordered table-hover"
                                            runat="server" Width="100%" OnItemCommand="dlGridDisplay_StudAttendance_ItemCommand">
                                            <HeaderTemplate>
                                                <b>Roll No</b> </th>
                                                <th align="left" style="width: 30%">
                                                    Student Name
                                                </th>
                                                <th style="width: 10%; text-align: center;">
                                                    <asp:CheckBox ID="chkAttendanceAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAttendanceAll_CheckedChanged" />
                                                    <span class="lbl"></span>Attendance<br />
                                                </th>
                                                <!--<th style="width: 10%; text-align: center;">
                                                    Answer Sheet Status
                                                </th>-->
                                                <th style="width: 20%; text-align: center;">
                                                    Reason
                                                </th>
                                                <th style="width: 10%; text-align: center;">
                                                    Is Re-Test
                                                </th>
                                                <th style="width: 15%; text-align: center;">
                                                    Re-Test Date
                                                </th>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                                                </td>
                                                <td style="text-align: center;">
                                                    <%--<span class='label label-<%#DataBinder.Eval(Container.DataItem,"AttendStatusColor")%>' >
                                                        <asp:Label ID="Label33" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AttendStatusStr")%>' />
                                                    </span>--%>
                                                    <asp:CheckBox ID="chkStudent" runat="server"
                                                    Visible='<%#(int)DataBinder.Eval(Container.DataItem,"StudentSelFlag") == 2 ? false :true %>' 
                                                     Checked='<%#(int)DataBinder.Eval(Container.DataItem,"StudentSelFlag") == 1 ? true : false %>'   AutoPostBack="True" OnCheckedChanged="chkAttendance_CheckedChanged"/>
                                                    <span class="lbl"></span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="Label52" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AnswerSheetIssueStatus")%>'
                                                        Visible="false" />
                                                    <%--    <asp:TextBox ID="lblDLAbsentReason" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AbsentReason_Name")%>'></asp:TextBox>--%>
                                                    <asp:Label ID="lblDLAbsentReasonID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AbsentReason_ID")%>'
                                                        Visible="false" />
                                                    <asp:Label ID="lblDLAbsentReason" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AbsentReason_Name")%>'
                                                        Visible="false" />


                                                    <asp:DropDownList ID="ddlabsentreason"  runat="server"  Width="215px" data-placeholder="Select Reason"
                                                        CssClass="chzn-select" AutoPostBack="True" />
                                                    <asp:Label ID="lblSBEntryCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>'
                                                        Visible="false" />
                                                    <a id="lbl_DLError" runat="server" title="Error" data-rel="tooltip" href="#"></a>
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:CheckBox ID="chkReset" runat="server"
                                                    Visible='<%#(int)DataBinder.Eval(Container.DataItem,"StudentSelFlag") == 2 ? false :true %>'
                                                     Checked='<%#(int)DataBinder.Eval(Container.DataItem,"IsReTest") == 1 ? true : false %>' />
                                                    <span class="lbl"></span>
                                                </td>
                                                <td>
                                                    <input type="text" id="ReTestDate" class="span8 date-picker" style="width: 120px"
                                                        runat="server" readonly="readonly" 
                                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"StudentSelFlag") == 2 ? false :true %>'
                                                        value='<%# DataBinder.Eval(Container.DataItem,"ReTestDate","{0:MM/dd/yyyy}")%>' />
                                                    <span class="add-on" id="spanFromTime" runat="server"><i class="icon-calendar"></i>
                                                    </span>
                                            </ItemTemplate>
                                        </asp:DataList>
                                        <div class="alert alert-block alert-success" id="Msg_Success2" visible="false" runat="server">
                                            <button type="button" class="close" data-dismiss="alert">
                                                <i class="icon-remove"></i>
                                            </button>
                                            <p>
                                                <strong><i class="icon-ok"></i></strong>
                                                <asp:Label ID="lblSuccess2" runat="server" Text="Label"></asp:Label>
                                            </p>
                                        </div>
                                        <div class="alert alert-error" id="Msg_Error2" visible="false" runat="server">
                                            <button type="button" class="close" data-dismiss="alert">
                                                <i class="icon-remove"></i>
                                            </button>
                                            <p>
                                                <strong><i class="icon-remove"></i>Error!</strong>
                                                <asp:Label ID="lblerror2" runat="server" Text="Label"></asp:Label>
                                            </p>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                    <tr>
                                        <td class="span2" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label54">Batch Strength</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: center; width: 30%;">
                                                        <asp:Label runat="server" class="blue" ID="lblSummary_BatchStrength" Font-Bold="True"></asp:Label>
                                                        <asp:Label runat="server" class="blue" ID="Label11" Font-Bold="True" Visible="false"></asp:Label>
                                                        <asp:Label runat="server" class="blue" ID="lblSummary_ExemptCount" Font-Bold="True"
                                                            Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--<td class="span2" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label runat="server" ID="Label60">Exempted Count</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: center; width: 40%;">
                                                                <asp:Label runat="server" class="blue" ID="lblSummary_ExemptCount" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>--%>
                                        <td class="span2" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 35%;">
                                                        <asp:Label runat="server" ID="Label55">Present Count</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: center; width: 20%;">
                                                        <asp:Label runat="server" class="blue" ID="lblSummary_PresentCount" Font-Bold="True"></asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: center; width: 20%;">
                                                        <asp:Label runat="server" class="blue" ID="lblSummary_PresentPercent"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span2" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 35%;">
                                                        <asp:Label runat="server" ID="Label58">Absent Count</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: center; width: 20%;">
                                                        <asp:Label runat="server" class="blue" ID="lblSummary_AbsentCount" Font-Bold="True"></asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: center; width: 20%;">
                                                        <asp:Label runat="server" class="blue" ID="lblSummary_AbsentPercent"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span2" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 35%;">
                                                        <asp:Label runat="server" ID="Label62">Not Marked</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 20%;">
                                                        <asp:Label runat="server" class="blue" ID="lblSummary_NMCount" Font-Bold="True"></asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 50%;">
                                                        <button id="btnLock_Authorise" runat="server" class="btn btn-small btn-success radius-4"
                                                            data-rel="tooltip" data-placement="left" title="Authorise attendance for the test"
                                                            onserverclick="btnLock_Authorise_ServerClick">
                                                            <i class="icon-lock"></i>
                                                        </button>
                                                        <button id="btnLock_UnAuthorise" runat="server" class="btn btn-small btn-danger radius-4"
                                                            data-rel="tooltip" data-placement="left" title="Open test attendance for editing"
                                                            visible="false" onserverclick="btnLock_UnAuthorise_ServerClick">
                                                            <i class="icon-unlock"></i>
                                                        </button>
                                                        <button id="BtnAttendanceMessage" runat="server" class="btn btn-small btn-success radius-4"
                                                            data-rel="tooltip" data-placement="left" title="SMS" onserverclick="btnMesage_ManualSending_ServerClick"
                                                            visible="false">
                                                            <i class="icon-envelope-alt"></i>
                                                        </button>
                                                        <span id="FlagAutoSMS" class="help-button ace-popover" runat="server" visible="false"
                                                            data-trigger="hover" data-placement="left" data-content="SMS will be send automatically once Attendance Authorise is Done."
                                                            title="Auto SMS"><i class="icon-envelope-alt"></i></span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnAllStudAttend_Save"
                                    ToolTip="Save" runat="server" Text="Save" Visible="false" OnClick="btnAllStudAttend_Save_Click" />
                                <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                                    runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--/row-->
    <div class="modal fade" id="DivDelete" style="left: 50% !important; top: 30% !important;
        display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        Delete Test
                    </h4>
                </div>
                <div class="modal-body">
                    <!--Controls Area -->
                    You are about to delete Test
                    <asp:Label runat="server" Font-Bold="false" ForeColor="Red" ID="txtDeleteItemName"
                        Text="" />. Do you want to Continue ?
                    <center />
                </div>
                <div class="modal-footer">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="lbldelCode" Text="" Visible="false" />
                    <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnDelete" ToolTip="Yes"
                        runat="server" Text="Yes" />
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                        ID="btnCancel" ToolTip="No" runat="server" Text="No" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="modal fade" id="DivStudAttendance" style="left: 50% !important; top: 10% !important;
        display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel_StudAttendance" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title">
                                <asp:Label runat="server" ID="lblStudAttend_Header"></asp:Label>
                            </h4>
                            <asp:Label runat="server" Visible="false" ID="lblStudAttend_Action"></asp:Label>
                        </div>
                        <div class="modal-body" style="height: 250px">
                            <asp:DataList ID="dlGridStudent" CssClass="table table-striped table-bordered table-hover"
                                runat="server" Width="100%">
                                <HeaderTemplate>
                                    <b>Select</b> </th>
                                    <th style="width: 20%; text-align: center">
                                        Roll Number
                                    </th>
                                    <th style="width: 70%; text-align: left">
                                    Student Name
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkStudent" runat="server" Checked='<%#(int)DataBinder.Eval(Container.DataItem,"StudentSelFlag") == 1 ? true : false %>' />
                                    <span class="lbl"></span></td>
                                    <td style="width: 20%; text-align: center">
                                        <asp:Label ID="lblStudentRollNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                                    </td>
                                    <td style="width: 70%; text-align: left">
                                        <asp:Label ID="lblStudentName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                                        <asp:Label ID="lblSBEntryCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>'
                                            Visible="False" />
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-footer">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="Label13" Text="" Visible="false" />
                    <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnStudAttend_Save"
                        ToolTip="Save" runat="server" Text="Save" OnClick="btnStudAttend_Save_Click" />
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                        ID="btnStudAttend_Close" ToolTip="Cancel" runat="server" Text="Cancel" OnClick="btnStudAttend_Close_Click" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal fade" id="Test_SMS" style="left: 50% !important; top: 20% !important;
                display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title">
                                SMS Preview
                            </h4>
                        </div>
                        <div class="modal-body">
                            <!--Controls Area -->
                            <table cellpadding="0" style="border-style: none;" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 100%;">
                                        <asp:Label runat="server" Font-Bold="false" ID="Label46" Text="" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <!--Button Area -->
                            <asp:Label runat="server" ID="Label52" Text="" Visible="false" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                                ID="Button1" ToolTip="No" runat="server" Text="Cancel" />
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!--/#page-content-->
</asp:Content>
