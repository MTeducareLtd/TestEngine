<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="Manage_OnlineTestCode.aspx.cs" Inherits="Manage_OnlineTestCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>Master<span class="divider"> <i class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">Online Test Code<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
               <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="btnuploadviexcel"
                Width="150px" Text="Upload QP Set Excel" OnClick="btnuploadviexcel_Click" />
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
                        <h5>Search Options
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
                                                                <asp:Label runat="server" ID="Label15" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label16" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcadyear" Width="215px" data-placeholder="Select Acad Year"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlAcadYear_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label17" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlStandard" Width="215px" ToolTip="Course" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" />
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
                                                                <asp:Label runat="server" ID="Label18" CssClass="red">Test Category</asp:Label>
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
                                                                <asp:Label runat="server" ID="Label19">Test Type</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlTestType" Width="215px" ToolTip="Test Type" data-placeholder="Select Test Type"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label20">Test Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtTestName" ToolTip="Test Name" type="text" Width="205px" />
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
                                <td class="span10">Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton ID="HLExport" Font-Underline="true" Height="25px" ToolTip="Export"
                                        class="btn-small btn-danger icon-2x icon-download-alt" runat="server" OnClick="HLExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:UpdatePanel ID="updatepanneladd" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DataList ID="dlgridonlinetestcode" CssClass="table table-striped table-bordered table-hover"
                            runat="server" Width="100%">
                            <HeaderTemplate>

                                <asp:CheckBox ID="chkAttendanceAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAttendanceAll_CheckedChanged" />
                                <span class="lbl"></span></th>
                                        <th><b>Division</b> </th>
                                <th align="left" style="width: 5%">Acad Year
                                </th>
                                <th style="width: 10%; text-align: center;">Course
                                </th>
                                <th style="width: 10%; text-align: left;">Test Category
                                </th>
                                <th style="width: 10%; text-align: left;">Test Type
                                </th>
                                <th style="width: 10%; text-align: left;">Test Name
                                </th>

                                <th style="width: 15%; text-align: left;">Subjects
                                </th>
                                <th style="width: 5%; text-align: left;">QP Set Number
                                </th>
                                <th style="width: 15%; text-align: left;">Assesment Test Code
                                </th>
                                <th style="width: 15%; text-align: center;">
                                Result
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%--<asp:CheckBox ID="chkCenter" runat="server" AutoPostBack="true" OnCheckedChanged="chkCenter_CheckedChanged" />--%>
                                <asp:CheckBox ID="chkCenter" runat="server" OnCheckedChanged="chksingle" AutoPostBack="true" Visible='<%#(string)DataBinder.Eval(Container.DataItem,"AssesmentTestCode") != "" ? false : true%>' />
                                <span class="lbl"></span>
                                </td>

                                        <td>
                                            <asp:Label ID="lbldivname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DivName")%>' />
                                            <asp:Label ID="lbldivcode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"DivCode")%>' />
                                        </td>
                                <td>
                                    <asp:Label ID="lblacadyear" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AcadYear")%>' />

                                </td>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblcoursename" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CourseName")%>' />
                                    <asp:Label ID="lblcoursecode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"StandardCode")%>' />
                                </td>

                                <td style="text-align: center;">
                                    <asp:Label ID="lbltestcategory" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestCategory")%>' />

                                </td>

                                <td style="text-align: center;">
                                    <asp:Label ID="lbltesttype" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestType")%>' />

                                </td>
                                <td style="text-align: center;">
                                    <asp:Label ID="lbltestname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                                    <asp:Label ID="lbltestid" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"TestId")%>' />
                                </td>

                                <td style="text-align: center;">
                                    <asp:Label ID="lblsubjetcts" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subjects")%>' />

                                </td>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblqpsetno" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SetNumber")%>' />

                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtassesmentcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AssesmentTestCode")%>'
                                        Width="85%" Visible="false" />
                                    <asp:Label ID="lblassesmenttestcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AssesmentTestCode")%>' />


                                </td>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblResult" runat="server" Text="" />
                            </ItemTemplate>
                        </asp:DataList>

                        <asp:DataList ID="dlexport" CssClass="table table-striped table-bordered table-hover"
                            runat="server" Width="100%" Visible="false">
                            <HeaderTemplate>


                                <th><b>Division</b> </th>
                                <th align="left" style="width: 5%">Acad Year
                                </th>
                                <th style="width: 10%; text-align: center;">Course
                                </th>
                                <th style="width: 10%; text-align: left;">Test Category
                                </th>
                                <th style="width: 10%; text-align: left;">Test Type
                                </th>
                                <th style="width: 10%; text-align: left;">Test Name
                                </th>

                                <th style="width: 15%; text-align: left;">Subjects
                                </th>
                                <th style="width: 5%; text-align: left;">Qp Set Number
                                </th>
                                <th style="width: 15%; text-align: left;">Assesment Test Code
                                </th>

                            </HeaderTemplate>
                            <ItemTemplate>


                                <td>
                                    <asp:Label ID="lbldivname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DivName")%>' />

                                </td>
                                <td>
                                    <asp:Label ID="lblacadyear" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AcadYear")%>' />

                                </td>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblcoursename" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CourseName")%>' />

                                </td>

                                <td style="text-align: center;">
                                    <asp:Label ID="lbltestcategory" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestCategory")%>' />

                                </td>

                                <td style="text-align: center;">
                                    <asp:Label ID="lbltesttype" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestType")%>' />

                                </td>
                                <td style="text-align: center;">
                                    <asp:Label ID="lbltestname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />

                                </td>

                                <td style="text-align: center;">
                                    <asp:Label ID="lblsubjetcts" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subjects")%>' />

                                </td>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblqpsetno" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SetNumber")%>' />

                                </td>
                                <td style="text-align: left;">

                                    <asp:Label ID="lblassesmenttestcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AssesmentTestCode")%>' />
                            </ItemTemplate>
                        </asp:DataList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                    <!--Button Area -->
                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnAuthorization"
                        runat="server" Text="Save" OnClick="BtnAuthorization_Click" />
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnClose" Visible="true"
                        runat="server" Text="Close" OnClick="BtnClose_Click" />
                </div>
            </div>
        </div>

    </div>

</asp:Content>

