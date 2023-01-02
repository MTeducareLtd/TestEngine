<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="Tran_Test_Schedule_Upload.aspx.cs" Inherits="Tran_Test_Schedule_Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">Test Schedule Upload<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  "
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="Btndownloadtemplate"
                Text="Download Template" Width="150px" OnClick="Btndownloadtemplate_Click" />
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


                    <div id="DivNew_Upload_1" visible="true" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="DivNew_Upload" visible="false" runat="server">
                                    <div class="widget-box">
                                        <div class="widget-header widget-header-small header-color-dark">
                                            <h5>New Upload
                            <%-- <asp:Label ID="lblPkey" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblUserType" runat="server" Text="" Visible="false"></asp:Label>--%>
                                            </h5>
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
                                                                            <asp:Label runat="server" ID="Label5" CssClass="red">Division</asp:Label>
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
                                                                            <asp:Label runat="server" ID="Label6" CssClass="red">Academic Year</asp:Label>
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
                                                                            <asp:Label runat="server" ID="Label23" CssClass="red">Course</asp:Label>
                                                                        </td>
                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:DropDownList runat="server" ID="ddlStandard_Add" Width="215px" data-placeholder="Select Course"
                                                                                CssClass="chzn-select" AutoPostBack="True" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="span4" style="text-align: left">
                                                                <table cellpadding="0" style="border-style: none; width: 100%;" class="table-hover"
                                                                    runat="server" id="Table4">
                                                                    <tr>
                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="lblselectfile" runat="server" CssClass="red">Select File</asp:Label>
                                                                        </td>
                                                                        <td style="border-style: none; text-align: left; width: 100%;">
                                                                            <asp:FileUpload ID="uploadfile" runat="server" size="22" Width="220" />
                                                                            <br />
                                                                            <asp:Label ID="lblfilepath" runat="server" Visible="False"></asp:Label>
                                                                            <asp:Label ID="lblfilename" runat="server" Visible="False"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="span4" style="text-align: left"></td>
                                                            <td class="span4" style="text-align: left"></td>
                                                        </tr>

                                                    </table>
                                                    <div runat="server" class="widget-main alert-block alert-info" id="Divuploadbtn"
                                                        style="text-align: center;">
                                                        <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="btnUpload"
                                                            Text="Upload" ToolTip="Upload" ValidationGroup="UcValidateSearch" OnClick="btnUpload_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="New_UploadGrid" runat="server" visible="false">

                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label2">Division</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lbldivisionresult" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label3">Acad Year</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblacadyearresult" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label1">Course</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblcourseresult" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                        
                                    </table>



                                    <asp:DataList ID="datalist_NewUploads1" CssClass="table table-striped table-bordered table-hover"
                                        runat="server" Width="100%" Visible="True">
                                        <HeaderTemplate>
                                            <b>Test Name</b>
                                            </th>
                                              

                                            <th runat="server" visible="true" style="width: 14%; text-align: center;">Center Short Name
                                            </th>

                                            <th runat="server" visible="true" style="width: 14%; text-align: center;">Batch Short Name
                                            </th>


                                            <th runat="server" visible="true" style="width: 14%; text-align: center;">Max Marks
                                            </th>


                                            <th runat="server" visible="true" style="width: 14%; text-align: center;">Test Date
                                            </th>


                                            <th runat="server" visible="true" style="width: 14%; text-align: center;">From Time
                                            </th>


                                            <th runat="server" visible="true" style="width: 16%; text-align: center;">To Time
                                            </th>

                                            <th style="width: 14%; text-align: center;">Status
                                            </th>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbltestname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestName")%>' />

                                            </td>
                                                

                                            <td style="text-align: center;">
                                                <asp:Label ID="lblcentername" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center")%>' />
                                            </td>


                                            <td style="text-align: Center;">
                                                <asp:Label ID="lblbatchname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch")%>' />
                                            </td>


                                            <td style="text-align: Center;">
                                                <asp:Label ID="lblmaxmarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MaxMarks")%>' />
                                            </td>


                                            <td style="text-align: center;">
                                                <asp:Label ID="lbltestdate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestDate")%>' />
                                            </td>


                                            <td style="text-align: Center;">
                                                <asp:Label ID="lblfromtime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FromTime")%>' />
                                            </td>


                                            <td style="text-align: Center;">
                                                <asp:Label ID="lbltotime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ToTime")%>' />
                                            </td>

                                            <td style="text-align: center;">
                                                <asp:Label ID="labelSTATUS" runat="server" Text=""></asp:Label>
                                            </td>
                                        </ItemTemplate>
                                    </asp:DataList>

                                    <div runat="server" class="widget-main alert-block alert-info" id="Divbtnimport"
                                        style="text-align: center;">
                                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnsaveexcel" runat="server" visibe="false"
                                            Text="Save" ValidationGroup="UcValidate" OnClick="btnsaveexcel_Click" />
                                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="Btnimport" runat="server"
                                            Text="Import" ValidationGroup="UcValidate" OnClick="Btnimport_Click" />
                                        <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="btnClose"
                                            Text="Close" ToolTip="Close" ValidationGroup="UcValidateSearch" OnClick="btnClose_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                                            ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
                                    </div>
                                </div>


                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnUpload" />
                                <asp:PostBackTrigger ControlID="btnClose" />
                                <asp:PostBackTrigger ControlID="Btnimport" />
                                <asp:PostBackTrigger ControlID="btnsaveexcel" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
</asp:Content>

