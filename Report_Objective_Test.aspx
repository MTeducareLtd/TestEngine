<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true"
    CodeFile="Report_Objective_Test.aspx.cs" Inherits="Report_Objective_Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function openModalStudSelection() {
            $('#DivStudSelection').modal({
                backdrop: 'static'
            })

            $('#DivStudSelection').modal('show');
        }

        function openModalPrintStudSelection() {
            $('#DivPrintStudSelection').modal({
                backdrop: 'static'
            })

            $('#DivPrintStudSelection').modal('show');
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
                    Details of Objective Test<span class="divider"></span></h5>
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
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label1" CssClass="red">    Center </asp:Label>
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
                                                                <asp:Label runat="server" ID="Label28" CssClass="red">Batch</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlBatch" Width="215px" data-placeholder="Select Batch"
                                                                    OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" CssClass="chzn-select"
                                                                    AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label13" CssClass="red">Conduct No</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlConductNo" Width="215px" data-placeholder="Select Conduct No"
                                                                    CssClass="chzn-select" 
                                                                    onselectedindexchanged="ddlConductNo_SelectedIndexChanged" AutoPostBack="true"/>
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
                                                                <asp:Label runat="server" ID="Label30" CssClass="red">Test Name</asp:Label>
                                                                <asp:LinkButton ID="btnTestName" ToolTip="Get Test Names" class="btn-small btn-primary icon-refresh" 
                                                                    OnClick="btnTestName_Click" runat="server" />
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlTestName" Width="215px" data-placeholder="Select Test Name"
                                                                    CssClass="chzn-select" />
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
                                                                <asp:Label runat="server" ID="Label211" CssClass="red">Student Name</asp:Label>
                                                                <asp:LinkButton ID="btnStudentName" ToolTip="Get Student Names" class="btn-small btn-primary icon-refresh"
                                                                    OnClick="btnStudentName_Click" runat="server" />
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 80.75%;">
                                                                <asp:DropDownList runat="server" ID="ddlStudentName" Width="350px" data-placeholder="Select Student"
                                                                    OnSelectedIndexChanged="ddlStudentName_SelectedIndexChanged" CssClass="chzn-select"
                                                                    AutoPostBack="True" />
                                                                <asp:DropDownList runat="server" ID="ddlStudentEMailId" Width="50px" Visible="false" />
                                                                <asp:DropDownList runat="server" ID="ddlStudentRollNo" Width="50px" Visible="false" />
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
                                                                <asp:TextBox runat="server" ID="txtRollNo" Width="205px" ReadOnly="true" />
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
                                    <asp:LinkButton runat="server" ID="btnPrint" ToolTip="Print" class="btn-small btn-warning icon-2x icon-print"
                                        Height="25px" onclick="btnPrint_Click" />
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
                                        Center
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
                                        <asp:Label runat="server" ID="Label17">Test Name</asp:Label>
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
                </table>
                
                    <div id="StudentResult" class="tab-pane">
                        <asp:DataList ID="dlGridDetailsofAnswering" CssClass="table table-striped table-bordered table-hover"
                            runat="server" Width="100%">
                            <HeaderTemplate>
                                <left><b>No.</b></left>
                                </th>
                                <th style="width: 25%; text-align: left;">
                                    Subject
                                </th>
                                <th style="width: 15%; text-align: center;">
                                    KEY
                                </th>
                                <th style="width: 15%; text-align: center;">
                                    ANS
                                </th>
                                <th style="width: 15%; text-align: center;">
                                    DL
                                </th>
                                <th style="width: 20%; text-align: center;">
                                Score
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblQNo" runat="server" Text='<%# DataBinder.GetPropertyValue(Container.DataItem, "No") %>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblDLSubject_Name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                                </td>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblKEY" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"KEY")%>' />
                                </td>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblANS" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ANS")%>' />
                                </td>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblDL" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DL")%>' />
                                </td>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblScore" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Score")%>' />
                                </td>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            
        </div>
    </div>


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
                                            Visible="true" />
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
                                <asp:Label runat="server" ID="Label2" Text="" Visible="false" />
                                <asp:Button class="btn btn-app  btn-warning btn-mini radius-4" ID="btnStudSelect_Print"
                                    ToolTip="Print" runat="server" Text="Print" />
                                <asp:Button class="btn btn-app  btn-danger btn-mini radius-4" ID="btnStudSelect_Mail"
                                    ToolTip="Mail" runat="server" Text="Mail" Visible="false" OnClick="btnStudSelect_Mail_Click" />
                                <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                                    ID="btnStudSelect_Close" ToolTip="Cancel" runat="server" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="modal fade" id="DivPrintStudSelection" style="left: 50% !important; top: 10% !important;
        display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title">
                                <asp:Label runat="server" ID="Label5">Select Students</asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body" style="height: 250px">
                            <asp:DataList ID="dlPrintStudent" CssClass="table table-striped table-bordered table-hover"
                                runat="server" Width="100%">
                                <HeaderTemplate>
                                    <b>
                                        <asp:CheckBox ID="chkPrintStudentAll" runat="server" AutoPostBack="True" OnCheckedChanged="All_StudentPrint_ChkBox_Selected" />
                                        <span class="lbl"></span></b>
                                    </th>                                    
                                    <th style="width: 70%; text-align: left">
                                        Student Name
                                </HeaderTemplate>
                                <ItemTemplate>
                                       <%-- <asp:CheckBox ID="chkStudent" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem,"StudentSelFlag")%>' />--%>

                                       <asp:CheckBox ID="chkStudent" runat="server"/>
                                        <%--<asp:Label ID="lblDLTestConduct" runat="server" Text='<%# (System.Convert.ToInt32(Eval("Conduct_No")) == 1 ? " " : "*" ) %>' />--%>
                                        <span class="lbl"></span>
                                    </td>                                    
                                    <td style="width: 70%; text-align: left">
                                        <asp:Label ID="lblStudentName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                                        <asp:Label ID="lblSBEntryCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>' Visible="False" />
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-footer">
                    <table cellpadding="0" style="border-style: none;" width="100%">
                        <tr>                           
                            <td style="border-style: none; width: 100%;">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app  btn-warning btn-mini radius-4" ID="btnPrintSelectedStud"
                                    ToolTip="Print" runat="server" Text="Print" 
                                    onclick="btnPrintSelectedStud_Click" />
                                <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                                    ID="btnPrintSelectedStud_Cancel" ToolTip="Cancel" runat="server" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>
