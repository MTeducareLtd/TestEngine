<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="Master_QPSet_Upload.aspx.cs" Inherits="Master_QPSet_Upload" %>

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
                <h5 class="smaller">QP Set Upload<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  "
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click1" />
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





                                    <asp:DataList ID="datalist_NewUploads1" CssClass="table table-striped table-bordered table-hover"
                                        runat="server" Width="100%" Visible="True">
                                        <HeaderTemplate>
                                            <b>Assesment Test Code</b>
                                            </th>
                                              

                                            <th runat="server" visible="true" style="width: 5%; text-align: center;">Que No
                                            </th>

                                            <th runat="server" visible="true" style="width: 10%; text-align: center;">Que Type
                                            </th>


                                            <th runat="server" visible="true" style="width: 10%; text-align: center;">Answer Key
                                            </th>


                                            <th runat="server" visible="true" style="width: 10%; text-align: center;">Difficulty Level
                                            </th>


                                            <th runat="server" visible="true" style="width: 5%; text-align: center;">Corrrect Marks
                                            </th>


                                            <th runat="server" visible="true" style="width: 5%; text-align: center;">Worng Marks(-ve)
                                            </th>


                                            <th style="width: 15%; text-align: center;">Subject
                                            </th>

                                            <th style="width: 15%; text-align: center;">Ref Course
                                            </th>

                                            <th style="width: 15%; text-align: center;">Ref Subject
                                            </th>

                                            <th style="width: 15%; text-align: center;">Chapter
                                            </th>


                                            <th style="width: 15%; text-align: center;">Topic
                                            </th>

                                            <th style="width: 15%; text-align: center;">Sub Topic
                                            </th>


                                            <th style="width: 15%; text-align: center;">Module
                                            </th>

                                            <th style="width: 15%; text-align: center;">Question Rule
                                            </th>

                                            <th style="width: 14%; text-align: center;">Status
                                            </th>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblassesmentcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Assesment_TestCode")%>' />

                                            </td>
                                                

                                            <td style="text-align: center;">
                                                <asp:Label ID="lblqueno" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Que_No")%>' />
                                            </td>


                                            <td style="text-align: Center;">
                                                <asp:Label ID="lblquetype" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Que_Type")%>' />
                                            </td>


                                            <td style="text-align: Center;">
                                                <asp:Label ID="lblanswerkey" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Answer_Key")%>' />
                                            </td>


                                            <td style="text-align: center;">
                                                <asp:Label ID="lbldifficultylevel" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Difficulti_Level")%>' />
                                            </td>


                                            <td style="text-align: Center;">
                                                <asp:Label ID="lblcorrectmarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Correct_Marks")%>' />
                                            </td>


                                            <td style="text-align: Center;">
                                                <asp:Label ID="lblwrongmarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Wrong_Marks")%>' />
                                            </td>

                                            <td style="text-align: Center;">
                                                <asp:Label ID="lblsubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject")%>' />
                                            </td>


                                            <td style="text-align: Center;">
                                                <asp:Label ID="lblrefcourse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Ref_Course")%>' />
                                            </td>


                                            <td style="text-align: Center;">
                                                <asp:Label ID="lblrefsubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Ref_Subject")%>' />
                                            </td>


                                            <td style="text-align: Center;">
                                                <asp:Label ID="lblchapter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter")%>' />
                                            </td>


                                            <td style="text-align: Center;">
                                                <asp:Label ID="lbltopic" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Topic")%>' />
                                            </td>


                                            <td style="text-align: Center;">
                                                <asp:Label ID="lblsubtopic" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SubTopic")%>' />
                                            </td>


                                            <td style="text-align: Center;">
                                                <asp:Label ID="lblmodule" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Module")%>' />
                                            </td>


                                            <td style="text-align: Center;">
                                                <asp:Label ID="lblquestionrule" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Question_Rule")%>' />
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
                                            Text="Close" ToolTip="Close" ValidationGroup="UcValidateSearch" OnClick="btnClose_Click"  />
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

