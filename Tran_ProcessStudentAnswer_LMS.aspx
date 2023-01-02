<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true"
    CodeFile="Tran_ProcessStudentAnswer_LMS.aspx.cs" Inherits="Tran_Tran_ProcessStudentAnswer_LMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Process Student Answers LMS<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" />
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
        </div>
        <div id="DivSearchPanel" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        Post Student Test Details LMS
                    </h5>
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanelAdd" runat="server">
                                <ContentTemplate>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label3" CssClass="red">Division</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlDivision_Add" Width="215px" data-placeholder="Select Division"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_Add_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label4" CssClass="red">Academic Year</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlAcadYear_Add" Width="215px" data-placeholder="Select Acad Year"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlAcadYear_Add_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label5" CssClass="red">Course</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlStandard_Add" Width="215px" data-placeholder="Select Course"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlStandard_Add_SelectedIndexChanged" />
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
                                                            <asp:Label runat="server" ID="Label23" CssClass="red">Test Type</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlTestType_Add" Width="215px" data-placeholder="Select Test Type"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlTestType_Add_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label6" CssClass="red">Test Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlTestName_Add" Width="215px" data-placeholder="Select Test Name"
                                                                CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlTestName_Add_SelectedIndexChanged" />
                                                            <i class="icon-bell-alt red icon-animated-bell icon-only" visible="false" runat="server"
                                                                id="icon_NegativeMarking_Add"></i>
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
                                                            <asp:DropDownList runat="server" ID="ddlConductNo_Add" Width="215px" data-placeholder="Select Conduct No"
                                                                CssClass="chzn-select" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
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
        <div id="DivResultPanel" runat="server" class="dataTables_wrapper">
            <div class="widget-box">
                <div class="table-header">
                    <table width="100%">
                        <tr>
                            <td class="span10">
                                Total No of Records:
                                <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                runat="server" Width="100%">
                <HeaderTemplate>
                    <b>Student Code</b> </th>
                    <th align="left" style="width: 15%">
                        Roll Number
                    </th>
                    <th align="left" style="width: 15%">
                        Test Code
                    </th>
                    <th align="left" style="width: 15%">
                        Assesment Test Code
                    </th>
                    <th style="width: 15%; text-align: center">
                        Center Code
                    </th>
                    <th style="width: 8%; text-align: center">
                        Batch Code
                    </th>
                    <th style="width: 8%; text-align: center">
                        Product Code
                    </th>
                    <th style="width: 80px; text-align: center">
                    Course Code
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblstudentcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Student_Code")%>' />
                    </td>
                    <td>
                        <asp:Label ID="lblrollno" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                    </td>
                    <td>
                        <asp:Label ID="lbltestcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_ID")%>' />
                    </td>
                    <td>
                        <asp:Label ID="lblassesmentcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Assesment_Engine_Test_Code")%>' />
                    </td>
                    <td style="text-align: center;">
                        <asp:Label ID="lblcentercode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Centre_Code")%>' />
                    </td>
                    <td style="text-align: center;">
                        <asp:Label ID="lblbatchcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch_Code")%>' />
                    </td>
                    <td style="text-align: center;">
                        <asp:Label ID="lblproductcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ProductCode")%>' />
                    </td>
                    <td style="text-align: center;">
                        <asp:Label ID="lblcoursecode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Standard_Code")%>' />
                        <asp:Label ID="lblteststardatetime" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"TestStartDatetime")%>' />
                        <asp:Label ID="lblanswerkey" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Answer_Key")%>' />
                        <asp:Label ID="lblsbentrycode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>' />
                </ItemTemplate>
            </asp:DataList>
            <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                <!--Button Area -->
                <asp:Label runat="server" ID="lblerrorDivision" Text="" ForeColor="Red" />
                <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSave" runat="server"
                    Text="Post LMS" OnClick="BtnSave_Click" />
                <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                    runat="server" Text="Close" onclick="BtnCloseAdd_Click" />
            </div>
        </div>
    </div>
</asp:Content>
