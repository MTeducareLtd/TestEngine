<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true"
    CodeFile="Report_Questionwise_Analysis.aspx.cs" Inherits="Report_Questionwise_Analysis" %>

<%@ Register TagPrefix="SearchPanel" TagName="SearchPanel" Src="~/Report_UC_SearchPanel.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                <h5 class="smaller">Questionwise Analysis<span class="divider"></span></h5>
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
                    <asp:Panel class="alert alert-block alert-success" ID="Msg_Success" Visible="false"
                        runat="server">
                        <button type="button" class="close" data-dismiss="alert">
                            <i class="icon-remove"></i>
                        </button>
                        <p>
                            <strong><i class="icon-ok"></i></strong>
                            <asp:Label ID="lblSuccess" runat="server" Text="Label"></asp:Label>
                        </p>
                    </asp:Panel>
                    <asp:Panel class="alert alert-error" ID="Msg_Error" Visible="false" runat="server">
                        <button type="button" class="close" data-dismiss="alert">
                            <i class="icon-remove"></i>
                        </button>
                        <p>
                            <strong><i class="icon-remove"></i>Error!</strong>
                            <asp:Label ID="lblerror" runat="server" Text="Label"></asp:Label>
                        </p>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="DivSearchPanel" runat="server">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5>Search Options
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <%--<asp:UpdatePanel ID="UpdatePanelSearch" runat="server">--%>
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
                                                        <asp:Label runat="server" ID="Label18">Center(s)</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:ListBox runat="server" ID="ddlCentre" Width="215px" data-placeholder="Select Center(s)"
                                                            CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlCentre_SelectedIndexChanged" />
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
                                                            CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label28">Batch(s)</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:ListBox runat="server" ID="ddlBatch" Width="215px" data-placeholder="Select Batch"
                                                            CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" />
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
                                                            CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlTestCategory_SelectedIndexChanged"
                                                            Enabled="false" />
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
                                                        <asp:Label runat="server" ID="Label1">Test Period</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <input readonly="readonly" runat="server" class="id_date_range_picker_1 span8" name="date-range-picker"
                                                            id="id_date_range_picker_1" style="width: 215px" placeholder="Date Search" data-placement="bottom"
                                                            data-original-title="Date Range" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label30" CssClass="red">Test Name</asp:Label>
                                                        <asp:LinkButton ID="btnTestName" ToolTip="Get Test Names" class="btn-small btn-primary icon-refresh"
                                                            OnClick="btnTestName_Click" runat="server" />
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:DropDownList runat="server" ID="ddlTest" Width="215px" data-placeholder="Select Test"
                                                            CssClass="chzn-select" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left"></td>
                                    </tr>
                                </table>
                                <%--</asp:UpdatePanel>--%>
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
                                <td class="span10">Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="btnExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" OnClick="btnExport_Click" />
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
                                        <asp:Label runat="server" ID="Label2">Center(s)</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblCenter_Result" class="blue"></asp:Label>
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
                                        <asp:Label runat="server" ID="Label4">Batch(es)</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblBatch_Result" class="blue"></asp:Label>
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

                    </tr>
                    <tr>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label15">Test Name</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblTestName_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="2" style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left;">
                                        <i class="icon-bell-alt blue"></i><small>
                                            <asp:Label runat="server" ID="Label3">This report is generated for only those tests whose Marks Authorisation is done</asp:Label>
                                        </small>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div id="DivResult_MCQ" runat="server">
                    <%--style="overflow-x: scroll; overflow-y: scroll; width: 100%; height: 350px"--%>
                    <%--<asp:GridView ID="dlGridReport1" runat="server" Width="100%">
                        <AlternatingRowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                       <RowStyle HorizontalAlign="Left"></RowStyle>
                    </asp:GridView>--%>
                    <asp:DataList ID="dlGridReport1" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" OnItemDataBound="dlGridReport1_ItemDataBound">
                        <HeaderTemplate>
                            <div style="text-align: center;">
                                <b>QP Set</b>
                            </div>
                            </th>
                            <th style="width: 5%; text-align: center;">Q.No.
                            </th>
                            <th style="width: 15%; text-align: center;">Chapter
                            </th>
                            <th style="width: 10%; text-align: center;">Subject
                            </th>
                            <th style="width: 10%; text-align: center;">Difficulty Level
                            </th>

                            <th style="width: 5%; text-align: center;">Total Students/ Total Absent Student
                            </th>
                            <th style="width: 5%; text-align: center;">Total Present Students
                            </th>
                            <th style="width: 5%; text-align: center;">Absent %
                            </th>
                            <th style="width: 10%; text-align: center;">Total Attempt
                            </th>
                            <th style="width: 10%; text-align: center;">Correct
                            </th>
                            <th style="width: 10%; text-align: center;">Attempt Rate
                            </th>
                            <th style="width: 10%; text-align: center;">Accuracy Rate
                            </th>
                            <th style="width: 10%; text-align: center;">
                            Avg %age
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div style="text-align: center;">
                                <asp:Label ID="lblDLQPSet" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"QPSet")%>' />
                            </div>
                            </td>
                             <td style="text-align: left;">
                                 <asp:Label ID="lblDLQuestion_No" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Question_No")%>' />
                             </td>
                            <td style="text-align: left;">
                                <asp:Label ID="lblDLChapter_Name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                            </td>
                            <td style="text-align: left;">
                                <asp:Label ID="lblDlSubject_Name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                            </td>
                            <td style="text-align: left;">
                                <asp:Label ID="lblDLDifficultyLevel" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DifficultyLevel")%>' />
                            </td>

                            <td style="text-align: center;">
                                <asp:Label ID="lbltotalstud_absent" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Totalstudent_absentstudent")%>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblDLTotalStudent" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TotalStudent")%>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblabsent_percent" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Absent_Percent")%>' />
                            </td>

                            <td style="text-align: center;">
                                <asp:Label ID="lblDLTotalAttempt" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TotalAttempt")%>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblDLCorrect" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Correct")%>' />
                            </td>
                            <td style="text-align: center;" id="tdAttemptRate" runat="server">
                                <asp:Label ID="lblDLAttemptRate" runat="server" Font-Bold="true" Text='<%#DataBinder.Eval(Container.DataItem,"AttemptRate")%>' />
                            </td>
                            <td style="text-align: center; background-color: Green;" id="tdAccuracyRate" runat="server">
                                <asp:Label ID="lblDLAccuracyRate" runat="server" Font-Bold="true" Text='<%#DataBinder.Eval(Container.DataItem,"AccuracyRate")%>' />
                            </td>
                            <td style="text-align: center; background-color: Red;" id="tdAvg_Perc" runat="server">
                                <asp:Label ID="lblAvg_Perc" runat="server" Font-Bold="true" Text='<%#DataBinder.Eval(Container.DataItem,"Avg_Perc")%>' />
                            </td>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
    </div>
    <!--/row-->
    <!--/#page-content-->
</asp:Content>
