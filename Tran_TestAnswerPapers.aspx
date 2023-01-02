<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true"
    CodeFile="Tran_TestAnswerPapers.aspx.cs" Inherits="Tran_TestAnswerPapers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        }


        function PrintDiv() {
        var contents = document.getElementById("divPrintSlip").innerHTML;
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
            setTimeout(function () 
            {
                window.frames["frame1"].focus();
                window.frames["frame1"].print();
                document.body.removeChild(frame1);
            }, 500);
            return false;
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




            
            function txtIssueDate_onclick() {

            }

    </script>
    
    <style type="text/css">
        .style1
        {
            width: 600px;
            height: 28px;
        }
        .style2
        {
            width: 104px;
            height: 28px;
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
                    Manage Answer Sheets<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="BtnAdd"
                Text="Add" OnClick="BtnAdd_Click" />
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
                                                        <asp:Label runat="server" ID="Label15" class="red">Division</asp:Label>
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
                                                        <asp:Label runat="server" ID="Label16" class="red">Academic Year</asp:Label>
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
                                                        <asp:Label runat="server" ID="Label18" class="red">Center</asp:Label>
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
                                                        <asp:Label runat="server" ID="Label17" class="red">Course</asp:Label>
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
                                                        <asp:Label runat="server" ID="Label29">Issue Period</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <input readonly="readonly" runat="server" class="id_date_range_picker_1 span8" name="date-range-picker"
                                                            id="id_date_range_picker_1" placeholder="Date Search" data-placement="bottom"
                                                            data-original-title="Date Range"  style="width:200px"/>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;display:none" class="table-hover" width="100%" >
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label19">Issued From</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:DropDownList runat="server" ID="ddlIssuedFrom_Search" Width="142px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;display:none" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label20">Issued To</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:DropDownList runat="server" ID="ddlIssuedTo_Search" Width="142px" />
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
            <div id="DivResultPanel" runat="server" class="dataTables_wrapper">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="style1">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="style2">
                                    <asp:LinkButton ID="HLExport" Font-Underline="true" Height="25px" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt" runat="server"
                                         onclick="HLExport_Click" />

                                    &nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                    <HeaderTemplate>
                        <b>Test Name</b> </th>
                        <th  style="width: 20%">
                            Batch
                        </th>
                        <th  style="width: 5%;text-align: center">
                            Conduct No
                        </th>
                        <th  style="width: 7%;text-align: center">
                            Test Date
                        </th>
                        <th  style="width: 20%">
                            Issued To
                        </th>
                        <th align="left" style="width: 7%;text-align: center">
                            Issue Date
                        </th>
                        
                        <th  style="width: 5%; text-align: center">
                           Issue Quantity
                        </th>
                        <th  style="width: 7%; text-align: center">
                          Is Paper Return?
                        </th>
                        
                        <th  style="width: 7%;text-align: center">
                            Return Date
                        </th>
                        
                        <th  style="width: 5%; text-align: center">
                           Returned Quantity
                        </th>

                        <th style="width: 7%; text-align: center">
                        Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label33" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Conduct_No")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestDate")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label27" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Partner_Name")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Issue_Date")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Issue_Quantity")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label75" runat="server" Text='<%# Convert.ToInt32( DataBinder.Eval(Container.DataItem,"Return_Flag")) == 1 ? "Yes" : "No" %>' />
                        </td>
                        
                       <td style="text-align: center">
                            <asp:Label ID="Label69" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Return_Date")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label74" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Returned_Quantity")%>' />
                        </td>
                        
                        <td style="text-align: center">
                            <asp:LinkButton ID="lnkEditInfo" ToolTip="Edit" CommandName="Manage" class="btn-small btn-primary icon-info-sign"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"TestBagPKey")%>' runat="server" />

                                <asp:LinkButton ID="lnkReruntInfo" ToolTip="Return Sheet Details" CommandName="Return" class="btn-small btn-primary icon-book"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"TestBagPKey")%>' runat="server" />
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dlGridExport" Visible="false" runat="server" 
                    HorizontalAlign="Left"  CssClass="table table-striped table-bordered table-hover"
                     Width="100%">                    
                       <HeaderTemplate>
                        <b>Test Name</b> </th>
                        <th  style="width: 20%">
                            Batch
                        </th>
                        <th  style="width: 5%;text-align: center">
                            Conduct No
                        </th>
                        <th  style="width: 7%;text-align: center">
                            Test Date
                        </th>
                        <th  style="width: 20%">
                            Issued To
                        </th>
                        <th align="left" style="width: 7%;text-align: center">
                            Issue Date
                        </th>
                        
                        <th  style="width: 5%; text-align: center">
                           Issue Quantity
                        </th>
                        <th  style="width: 7%; text-align: center">
                          Is Paper Return?
                        </th>
                        
                        <th  style="width: 7%;text-align: center">
                            Return Date
                        </th>
                        
                        <th  style="width: 5%; text-align: center">
                           Returned Quantity
                        
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label33" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Conduct_No")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestDate")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label27" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Partner_Name")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Issue_Date")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Issue_Quantity")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label75" runat="server" Text='<%# Convert.ToInt32( DataBinder.Eval(Container.DataItem,"Return_Flag")) == 1 ? "Yes" : "No" %>' />
                        </td>
                        
                       <td style="text-align: center">
                            <asp:Label ID="Label69" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Return_Date")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label74" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Returned_Quantity")%>' />
                            </ItemTemplate>
                            </asp:DataList>
                        
            </div>
        </div>
        <div id="DivAddPanel" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        Answer Sheets Issue Details
                    </h5>
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanelEntry" runat="server">
                                <ContentTemplate>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label213" CssClass="red">Division</asp:Label>
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
                                                            <asp:Label runat="server" ID="Label214" CssClass="red">Academic Year</asp:Label>
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
                                                            <asp:Label runat="server" ID="Label215" CssClass="red">Center</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlCentre_Add" Width="215px" data-placeholder="Select Center"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCentre_Add_SelectedIndexChanged" />
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
                                                            <asp:Label runat="server" ID="Label23" CssClass="red">Course</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlStandard_Add" Width="215px" data-placeholder="Select Course"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlStandard_Add_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label216" CssClass="red">Batch</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlBatch_Add" Width="215px" data-placeholder="Select Batch"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_Add_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label3" CssClass="red">Test Category</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlTestCategory_Add" Width="215px" data-placeholder="Select Test Category"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlTestCategory_Add_SelectedIndexChanged" />
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
                                                            <asp:Label runat="server" ID="Label4" CssClass="red">Test Type</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:ListBox runat="server" ID="ddlTestType_Add" Width="215px" data-placeholder="Select Test Type"
                                                                CssClass="chzn-select" SelectionMode="Multiple" OnSelectedIndexChanged="ddlTestType_Add_SelectedIndexChanged"
                                                                AutoPostBack="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label12" CssClass="red">Test Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlTestName_Add" Width="215px" data-placeholder="Select Test Name"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlTestName_Add_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label10" CssClass="red">Conduct No.</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlConductNo_Add" Width="215px" data-placeholder="Select Conduct No."
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlConductNo_Add_SelectedIndexChanged" />
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
                                                            <asp:Label runat="server" ID="Label32" CssClass="red">Subject(s)</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtSubject_Add" ToolTip="Subject(s)" type="text"
                                                                Width="215px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td colspan="2" class="span8" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 19.25%;">
                                                            <asp:Label runat="server" ID="Label113">Remarks</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 80.75%;">
                                                            <asp:TextBox runat="server" ID="txtRemarks_Add" ToolTip="Remarks" type="text" Width="350px"
                                                                MaxLength="200" ReadOnly="True" />
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
                                                            <asp:Label runat="server" ID="Label114" CssClass="red">Maximum Marks</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtMaxMarks_Add" ToolTip="Maximum Marks" type="text"
                                                                Width="205px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <i class="icon-calendar"></i>
                                                            <asp:Label runat="server" ID="Label115" CssClass="red">  Test Date</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <div class="row-fluid input-append date">
                                                                <span>
                                                                    <asp:TextBox Width="205px" runat="server" ReadOnly="true" ID="txtTestDate_Add" type="text" />
                                                                </span>
                                                            </div> 
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;display:none" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <i class="icon-time"></i>
                                                            <asp:Label runat="server" ID="Label116">Test Time</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <span>
                                                                <asp:TextBox ID="txtfromtime" Width="55px" runat="server" ReadOnly="True" />
                                                                <asp:TextBox ID="txttotime" Width="55px" runat="server" ReadOnly="True" />
                                                            </span>
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
                                                            <asp:Label runat="server" ID="Label50" class="red">Issuer Type</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlIssuerType" Width="215px" data-placeholder="Select Type"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlIssuerType_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="lblIssuerType" class="red"></asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" class="blue" ID="lblIssuedToCentreName" Visible="False">Mulund</asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlIssuedToPaperChecker" CssClass="chzn-select"
                                                                Width="215px" Visible="False" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%" runat="server" id="tblIssueDate">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <i class="icon-calendar"></i>
                                                            <asp:Label runat="server" ID="Label7" class="red">Issue Date</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <div class="row-fluid input-append date">
                                                                <span>
                                                                    <input readonly="readonly" class="span10 date-picker" id="txtIssueDate" runat="server"
                                                                      style="width:205px"  type="text" data-date-format="dd M yyyy"  onclick="return txtIssueDate_onclick()" />
                                                                </span>
                                                            </div>
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
                                                            <asp:Label runat="server" ID="Label9" class="red">Receiver Type</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlReceiverType" Width="215px" CssClass="chzn-select"
                                                                Enabled="false" OnSelectedIndexChanged="ddlReceiverType_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="lblReceiverType"></asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" class="blue" ID="lblRcvdToCentreName" Visible="False"></asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlRcvdFromPaperChecker" CssClass="chzn-select"
                                                                Width="215px" Visible="False" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                 <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label78" CssClass="red">Paper Checker Slab</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                           <asp:DropDownList runat="server" ID="ddlPCSlab" Width="210px" ToolTip="Paper Checker Slab"
                                                                data-placeholder="Select Paper Checker Slab " CssClass="chzn-select" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                        <tr runat="server" id="trExpectedReturnDate">
                                            <td class="span4" style="text-align: left">
                                                
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                 <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label80" class="red">Expected Return Date</asp:Label>
                                                        </td>
                                                       <td style="border-style: none; text-align: left; width: 60%;">
                                                            <div class="row-fluid input-append date">
                                                                <span>
                                                                    <input readonly="readonly" class="span10 date-picker" id="txtexpectdreturndate_Add" runat="server"
                                                                      style="width:205px"  type="text" data-date-format="dd M yyyy" />
                                                                </span>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trStudentList">
                                            <td colspan="2" class="span8" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 19.75%;">
                                                            <asp:Label runat="server" ID="Label11">Select Students </asp:Label>
                                                            <asp:LinkButton ID="lnkRefreshStudentList" ToolTip="Refresh" class="btn-small btn-primary icon-refresh"
                                                                runat="server" OnClick="lnkRefreshStudentList_Click" />
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 80.25%; height: 300px;">
                                                            <asp:DataList ID="dlStudentList" CssClass="table table-striped table-bordered table-hover"
                                                                runat="server" Width="90%">
                                                                <HeaderTemplate>
                                                                    
                                                                     <asp:CheckBox ID="chkStudentAllAdd" runat="server" AutoPostBack="true" 
                                                                        oncheckedchanged="chkStudentAllAdd_CheckedChanged" /><span class="lbl"></span>
                                                                    <b>Select</b>
                                                                     </td>
                                                                    <td>
                                                                        Roll No
                                                                    </td>
                                                                    <td>
                                                                        Student Name
                                                                    </td>
                                                                    <td>
                                                                        Marks
                                                                    </td>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkStudent" runat="server" 
                                                                    visible ='<%#(string)DataBinder.Eval(Container.DataItem,"Marks")=="A"||(string)DataBinder.Eval(Container.DataItem,"Marks")=="E"||(string)DataBinder.Eval(Container.DataItem,"Marks")== "S" ? false : true%>'
                                                                     /><span class="lbl"></span> </td>
                                                                    <td>
                                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                                                                        <asp:Label ID="lblSBEntryCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>'
                                                                            Visible="false" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txtDLMarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Marks")%>'
                                                                            Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="lblerrorDivision" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveAdd" runat="server"
                            Text="Save" OnClick="BtnSaveAdd_Click" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                            runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div id="DivEditPanel" visible="false" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        Modify Answer Sheets Issue Details
                    </h5>
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label2" CssClass="red">Division</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtEdit_Division" ToolTip="Division" type="text"
                                                                Width="205px" ReadOnly="True"  />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label5" CssClass="red">Academic Year</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtEdit_Year" ToolTip="Academic Year" type="text"
                                                                Width="205px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label6" CssClass="red">Center</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtEditCentre" ToolTip="Center" type="text" Width="205px"
                                                                ReadOnly="True" />
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
                                                            <asp:Label runat="server" ID="Label8" CssClass="red">Course</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtEditStandard" ToolTip="Course" type="text" Width="205px"
                                                                ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label13" CssClass="red">Batch</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtEditBatch" ToolTip="Batch" type="text" Width="205px"
                                                                ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label25" CssClass="red">Test Category</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtEdit_Category" ToolTip="Test Category" type="text"
                                                                Width="205px" ReadOnly="True" />
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
                                                            <asp:Label runat="server" ID="Label26" CssClass="red">Test Type</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtEdit_TestType" ToolTip="Test Type" type="text"
                                                                Width="205px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label31" CssClass="red">Test Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtEdit_TestName" ToolTip="Test Name" type="text"
                                                                Width="205px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label34" CssClass="red">Conduct No.</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtEdit_ConductNo" ToolTip="Conduct No." type="text"
                                                                Width="205px" ReadOnly="True" />
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
                                                            <asp:Label runat="server" ID="Label37" CssClass="red">Maximum Marks</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtEdit_MaxMarks" ToolTip="Maximum Marks" type="text"
                                                                Width="205px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label38" CssClass="red">Test Date</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtEdit_TestDate" ToolTip="Test Date" type="text"
                                                                Width="205px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <i class="icon-calendar"></i>
                                                            <asp:Label runat="server" ID="Label43" class="red">Issue Date</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <%--<div class="row-fluid input-append date">
                                                                <span>--%>
                                                                    <input readonly="readonly" class="span10 date-picker" id="txtEdit_IssueDate" runat="server"
                                                                      style="width:205px" type="text" data-date-format="dd M yyyy" />
                                                             <%--  </span>
                                                            </div>--%>
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
                                                            <asp:Label runat="server" ID="Label44" class="red">Receiver Type</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtEdit_ReceiverType" ToolTip="Receiver Type" type="text"
                                                                Width="205px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="lblEdit_ReceiverType" class="red"></asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" class="blue" ID="Label46" Visible="False"></asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlEdit_IssuedTo" CssClass="chzn-select" Width="215px"
                                                                Visible="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">                                                
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label79" CssClass="red">Paper Checker Slab</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                           <asp:DropDownList runat="server" ID="ddlEditPCSlab" Width="210px" ToolTip="Paper Checker Slab"
                                                                data-placeholder="Select Paper Checker Slab " CssClass="chzn-select" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td class="span4" style="text-align: left">
                                                
                                            </td>
                                            <td class="span4" style="text-align: left">
                                            </td>
                                            <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" 
                                                    width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label82" class="red">Expected Return Date</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%; ">
                                                            <div class="row-fluid input-append date">
                                                                <span>
                                                                <input readonly="readonly" class="span9 date-picker" id="txtexpectedreturndate_edit" runat="server"
                                                                      style="width:205px"  type="text" data-date-format="dd M yyyy" />
                                                                </span>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td colspan="2" class="span8" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 19.75%;">
                                                            <asp:Label runat="server" ID="Label47">Select Students </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 80.25%; height: 300px;">
                                                            <asp:DataList ID="DLEdit_StudentList" CssClass="table table-striped table-bordered table-hover"
                                                                runat="server" Width="90%">
                                                                <HeaderTemplate>
                                                                     
                                                                    <asp:CheckBox ID="chkStudentAllEdit" runat="server" AutoPostBack="true" 
                                                                        oncheckedchanged="chkStudentAllEdit_CheckedChanged" /><span class="lbl"></span>
                                                                    <b>Select </b>
                                                                    
                                                                    </th>
                                                                    <th align="left" style="width: 80px">
                                                                        Roll No
                                                                    </th>
                                                                    <th align="left">
                                                                        Student Name
                                                                    </th>
                                                                    <th align="left" style="width: 80px">
                                                                    Marks
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkStudent" runat="server"
                                                                    Visible ='<%#(string)DataBinder.Eval(Container.DataItem,"Marks")== "A"||(string)DataBinder.Eval(Container.DataItem,"Marks")== "E" ||(string)DataBinder.Eval(Container.DataItem,"Marks")== "S" ? false : true%>'
                                                                     Checked='<%# (int)DataBinder.Eval(Container.DataItem,"AnswerSheetIssueStatus") == 1 ? true : false%>'  /><span
                                                                        class="lbl"></span> </td>
                                                                    <td>
                                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                                                                        <asp:Label ID="lblSBEntryCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>'
                                                                            Visible="false" />
                                                                        <asp:Label ID="lblBagDispatch_Id" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BagDispatch_Id")%>'
                                                                            Visible="false" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txtDLMarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Marks")%>'
                                                                            Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="lblerrorEdit" Text="" ForeColor="Red" />
                        <asp:Label runat="server" ID="lblPKey_Edit" Text="" Visible="false" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveEdit" runat="server"
                            OnClick="BtnSaveEdit_Click" Text="Save" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseEdit" Visible="true"
                            OnClick="BtnCloseEdit_Click" runat="server" Text="Close" />
                                                    
                             <a href="#"  onclick="javascript:PrintDiv();"  runat="server" id="btnPrint" title="Print Paper Corrector Slip" class="btn btn-app btn-warning btn-mini radius-4 icon-print" style="height:25px" ></a>
                    </div>
                </div>
            </div>
        </div>

        <div id="DivReturn" visible="false" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        Return Answer Sheets Details
                    </h5>
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label55" CssClass="red">Division</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtReturnDivision" ToolTip="Division" type="text"
                                                                Width="205px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label57" CssClass="red">Academic Year</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtReturnAcademic" ToolTip="Academic Year" type="text"
                                                                Width="205px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label58" CssClass="red">Center</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtReturnCenter" ToolTip="Center" type="text" Width="130px"
                                                                ReadOnly="True" />
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
                                                            <asp:Label runat="server" ID="Label59" CssClass="red">Course</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtReturnCourse" ToolTip="Course" type="text" Width="205px"
                                                                ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label60" CssClass="red">Batch</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtReturnBatch" ToolTip="Batch" type="text" Width="205px"
                                                                ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label61" CssClass="red">Test Category</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtReturnTestCategory" ToolTip="Test Category" type="text"
                                                               Width="205px" ReadOnly="True" />
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
                                                            <asp:Label runat="server" ID="Label62" CssClass="red">Test Type</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtReturnTestType" ToolTip="Test Type" type="text"
                                                               Width="205px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label63" CssClass="red">Test Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtReturnTestName" ToolTip="Test Name" type="text"
                                                                Width="205px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label64" CssClass="red">Conduct No.</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtReturnConductNo" ToolTip="Conduct No." type="text"
                                                                Width="205px" ReadOnly="True" />
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
                                                            <asp:Label runat="server" ID="Label65" CssClass="red">Maximum Marks</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtReturnMaximumMarks" ToolTip="Maximum Marks" type="text"
                                                                Width="205px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label66" CssClass="red">Test Date</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtreturnTestDate" ToolTip="Test Date" type="text"
                                                                Width="205px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label76" CssClass="red">Issue Quantity</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtreturnIssueQuantity" ToolTip="Test Date" type="text"
                                                               Width="205px" ReadOnly="True" />
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
                                                            <asp:Label runat="server" ID="Label67" CssClass="red" >Receiver Type</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtreturnReceiverType" ToolTip="Receiver Type" type="text"
                                                                Width="205px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label68" CssClass="red"> Paper Checker</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            
                                                            <asp:TextBox runat="server" ID="txtReturnPartner" ToolTip="" type="text"
                                                                Width="205px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <i class="icon-calendar"></i>
                                                            <asp:Label runat="server" ID="Label70" class="red">Paper Return Date</asp:Label>
                                                        </td>
                                                      <td style="border-style: none; text-align: left; width: 60%;">
                                                            <div class="row-fluid input-append date">
                                                                <span>
                                                                    <input readonly="readonly" class="span10 date-picker" id="txtReturnDate" runat="server"
                                                                      style="width:205px"  type="text" data-date-format="dd M yyyy" />
                                                                </span>
                                                            </div>
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
                                                            <asp:Label runat="server" ID="Label77" CssClass="red">Returned Quantity</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtreturnQuantity" ToolTip="Test Date" type="text"
                                                                Width="205px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">


                                            </td>
                                            <td class="span4" style="text-align: left">


                                            </td>
                                        
                                        
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="span8" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 19.75%;">
                                                            <asp:Label runat="server" ID="Label71">Enter Student Marks </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 80.25%; height: 300px;">
                                                            <asp:DataList ID="ddlReturn" CssClass="table table-striped table-bordered table-hover"
                                                                runat="server" Width="90%">
                                                                <HeaderTemplate>
                                                                    
                                                                      <b>  Roll No</b>
                                                                    </th>
                                                                    <th align="left">
                                                                        Student Name
                                                                    </th>
                                                                    <th align="left" style="width: 80px">
                                                                    Marks
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                   
                                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                                                                        <asp:Label ID="lblSBEntryCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>'
                                                                            Visible="false" />
                                                                        <asp:Label ID="lblBagDispatch_Id" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BagDispatch_Id")%>'
                                                                            Visible="false" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                                                                    </td>
                                                                    <td>
                                                                     <asp:TextBox ID="txtDLMarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Marks")%>'
                                                                            Width="70px" Enabled='<%#(string)DataBinder.Eval(Container.DataItem,"Marks")== "A" ? false : true%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="Label72" Text="" ForeColor="Red" />
                        <asp:Label runat="server" ID="Label73" Text="" Visible="false" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" 
                            ID="btnReturnSave" runat="server"
                            Text="Save" onclick="btnReturnSave_Click" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" 
                            ID="btnReturnClose" Visible="true"
                             runat="server" Text="Close" onclick="btnReturnClose_Click" />
                               
                                                    
                             <a href="#"  onclick="javascript:PrintDiv1();"  runat="server" id="A1" title="Print Paper Corrector Slip" class="btn btn-app btn-warning btn-mini radius-4 icon-print" style="height:25px" ></a>
                                     

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
    <div id="divPrintSlip" style="margin: 0px auto 0px auto; width: 800px; border: 1px solid Gray; display:none;visibility:hidden;
        font-family: Arial">
        <table width="100%" border="3" cellpadding="0" cellspacing="0" style="border-color: Black;
            font-family: Arial;">
            <tr>
                <td colspan="3" style="border-color: Black; padding: 20px 20px 20px 20px; font-family: Arial;">
                    <table cellpadding="3" width="100%">
                        <tr>
                            <td class="span4" style="text-align: left">
                            </td>
                            <td class="span4" style="text-align: center">
                                <p style="font-size: 20px;">MT EDUCARE LTD.</p>                                
                                <p style="font-size: 14px;">TEST PAPER ASSIGNMENT SLIP</p>

                            </td>
                            <td class="span4" style="text-align: left" >
                                <img src="images/logo.jpg" alt="" style="width: 146px; height: 70px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="span12" colspan="3">
                            <b> <span style="font-size: 12px;"> Paper Corrector :</span> <asp:Label runat="server" ID="lblPaperCoamrrectorName" Style="font-size: 12px;"></asp:Label></b>
                                                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" ID="Label35" Style="font-size: 12px;">Division</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblPrintDivision" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" ID="Label36" Style="font-size: 12px;">Academic Year</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblPrintAcademic" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label Style="font-size: 12px;" runat="server" ID="Label39">Center</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblPrintCenter" ToolTip="Center" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label Style="font-size: 12px;" runat="server" ID="Label40">Course</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblPrintCourse" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label41">Batch</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblPrintBatch" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" ID="Label42" Style="font-size: 12px;">Category</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblPrintTestCategory" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label45">Test Type</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblTestType" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" ID="Label48" Style="font-size: 12px;">Test Name</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblPrintTestName" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server"  Style="font-size: 12px;" ID="Label52">Test Date</asp:Label>
                                            </b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblTestDate" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 45%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label51">Out of Marks</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 55%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblMaximumMarks" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label Style="font-size: 12px;" runat="server" ID="Label56">Issue Date</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblPrintIssueDate" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 45%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label53"> Exp Sub Date</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 55%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblreturndate" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 55%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label83">Total Students  </asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 45%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblprinttotalstudents" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label Style="font-size: 12px;" runat="server" ID="Label85">Absent</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblprintabsent" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 45%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label87">Present</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 55%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblPaperCount" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>   <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 45%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label54">Subject</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 55%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblPrintSubjects" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <%--<tr>
                        <td  class="span4">
                        <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 12%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label54">Subject</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 88%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblPrintSubjects" />
                                        </td>
                                    </tr>
                                </table></td>
                                <td class="span4">
                        <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 12%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label80">Absent</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 88%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="Label81" />
                                        </td>
                                    </tr>
                                </table></td>
                                <td  class="span4">
                        <table  style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 12%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label82">Exp Sub Date</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 88%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblreturndate" />
                                        </td>
                                    </tr>
                                </table></td>
                        
                        </tr>--%>
                        
                        
                        <tr>
                            <td colspan="3" class="span12" style="text-align: center">
                                <asp:Repeater ID="dsPrint" runat="server">
                                    <HeaderTemplate>
                                        <table width="100%"  cellpadding="0" cellspacing="0" style="border-color: Black ;border: 1px; border-style: solid;">
                                            <th style="text-align: left; border-color: Black; border: 1px; border-style: solid;   padding-left: 5px">
                                             <span style="font-size: 12px;">   Roll No</span>
                                            </th>
                                            <th style="text-align: left; border-color: Black; border: 1px; border-style: solid; padding-left: 5px">
                                               <span style="font-size: 12px;">   Student Name</span>
                                            </th>
                                            <th style="text-align: center; border-color: Black; border: 1px; border-style: solid">
                                              <span style="font-size: 12px;">   Attendance</span>
                                            </th>
                                            <th style="text-align: center; border-color: Black; border: 1px; border-style: solid">
                                             <span style="font-size: 12px;">    Marks Obtained </span>
                                            </th>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: left; border-color:Gray; border: 1px; border-style: solid;   padding-left: 5px"">
                                                <asp:Label   ID="lblRollNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                                            </td>
                                            <td style="text-align: left; border-color: Gray; border: 1px; border-style: solid;   padding-left: 5px"">
                                                <asp:Label Style="font-size: 12px;" ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                                            </td>
                                            <td style="text-align: center;  border-color: Gray; border: 1px; border-style: solid;   padding-left: 5px"">
                                                <asp:Label Style="font-size: 12px;" ID="Label49" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AttendStatusStr")%>' />
                                            </td>
                                            <td style="text-align: center; border-color: Gray; border: 1px; border-style: solid;   padding-left: 5px"">
                                                <asp:Label Style="font-size: 12px;" ID="lblMarks" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="span12" colspan="3" style="height:30px">
                            </td>
                        </tr>
                        <%--<tr>
                            <td class="span4" style="text-align: left">
                                <span style="font-size: 12px;">Office Staff(Sign)</span>
                            </td>
                            <td class="span4" style="text-align: center">
                                <span style="font-size: 12px;">Office On(Stamp)</span>
                            </td>
                            <td class="span4" style="text-align: right">
                                <span style="font-size: 12px;">Paper Corrector(Sign)</span>
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="span12" colspan="3">
                                <span style="font-size: 12px;">Note: <br />
                                    1.Please Verify the Total number of papers given to you in the Paper Bundle and the count given in the details above.<br>
                                    2.Ensure that you submit the papers on or before the Expected submission Date.<br>
                                    3.Collect your paper correction submission slip when you submit your papers.<br>
                                    <asp:Label ID="lblFooterDate" runat="server" Visible="false"></asp:Label>
                                    
                                    
                                    
                                    </span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="PrintSlip" style="margin: 0px auto 0px auto; width: 800px; border: 1px solid Gray; display:none;visibility:hidden;
        font-family: Arial">
        <table width="100%" border="3" cellpadding="0" cellspacing="0" style="border-color: Black;
            font-family: Arial;">
            <tr>
                <td colspan="3" style="border-color: Black; padding: 20px 20px 20px 20px; font-family: Arial;">
                    <table cellpadding="3" width="100%">
                        <tr>
                            <td class="span4" style="text-align: left">
                            </td>
                            <td class="span4" style="text-align: center">
                                <p style="font-size: 12px;">CORRECTORS COPY</p>  
                                <p style="font-size: 20px;">MT EDUCARE LTD.</p>                                
                                <p style="font-size: 14px;">TEST CORRECTION SUBMISSION SLIP</p>

                            </td>
                            <td class="span4" style="text-align: left" >
                                <img src="images/logo.jpg" alt="" style="width: 146px; height: 70px" />
                            </td>
                        
                        </tr>
                        <tr>
                            <td class="span12" colspan="3">
                            <b> <span style="font-size: 12px;"> Paper Corrector :</span> <asp:Label runat="server" ID="lblpaper_corrector" Style="font-size: 12px;"></asp:Label></b>
                                                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" ID="Label81" Style="font-size: 12px;">Division</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lbldivisionprint" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" ID="Label84" Style="font-size: 12px;">Academic Year</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblacadyearprint" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label Style="font-size: 12px;" runat="server" ID="Label88">Center</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblCenterPrint" ToolTip="Center" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label Style="font-size: 12px;" runat="server" ID="Label90">Course</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblCoursePrint" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label92">Batch</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblBatchPrint" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" ID="Label94" Style="font-size: 12px;">Category</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblTestCategoryPrint" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label96">Test Type</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lbltesttyp_Print" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" ID="Label98" Style="font-size: 12px;">Test Name</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lbltestNM_Print" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server"  Style="font-size: 12px;" ID="Label100">Test Date</asp:Label>
                                            </b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblDate" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                           <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 55%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label102">Out of Marks</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 45%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblmarksPrint" />
                                        </td>
                                    </tr>
                                </table>
                            </td> 
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label Style="font-size: 12px;" runat="server" ID="Label104">Issue Date</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblissueDatePrint" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 45%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label106"> Exp Sub Date</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 55%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="submissionDateprint" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 55%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label108">Total Students  </asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 45%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="TotalstdntPrint" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label Style="font-size: 12px;" runat="server" ID="Label110">Absent</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblabsentPrint" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 45%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label112">Present</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 55%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblpresentprint" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr> 
                          
                            
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 45%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label118">Subject</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 55%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblsubjecrprint" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                             <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 45%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label121">Papers Corrected</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 55%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblpaperscorrectedPrint" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td class="span12" colspan="3" style="height:30px">
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <span style="font-size: 12px;">Office Staff(Sign)</span>
                            </td>
                            <td class="span4" style="text-align: center">
                                <span style="font-size: 12px;">Office (Stamp)</span>
                            </td>
                            <td class="span4" style="text-align: right">
                                <span style="font-size: 12px;">Paper Corrector(Sign)</span>
                            </td>
                        </tr>
            
                    </table>
                </td>
            </tr>
        </table>
    </br>
        <table width="100%" border="3" cellpadding="0" cellspacing="0" style="border-color: Black;
            font-family: Arial;">
            <tr>
                <td colspan="3" style="border-color: Black; padding: 20px 20px 20px 20px; font-family: Arial;">
                    <table cellpadding="3" width="100%">
                        <tr>
                            <td class="span4" style="text-align: left">
                            </td>
                            <td class="span4" style="text-align: center">
                                <p style="font-size: 12px;">OFFICE COPY</p>  
                                <p style="font-size: 20px;">MT EDUCARE LTD.</p>                                
                                <p style="font-size: 14px;">TEST CORRECTION SUBMISSION SLIP</p>

                            </td>
                            <td class="span4" style="text-align: left" >
                                <img src="images/logo.jpg" alt="" style="width: 146px; height: 70px" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="span12" colspan="3">
                            <b> <span style="font-size: 12px;"> Paper Corrector :</span> <asp:Label runat="server" ID="lblCorrectorPrint" Style="font-size: 12px;"></asp:Label></b>
                                                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" ID="Label123" Style="font-size: 12px;">Division</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblDivisionP" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" ID="Label125" Style="font-size: 12px;">Academic Year</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblyearP" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label Style="font-size: 12px;" runat="server" ID="Label127">Center</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblcenterp" ToolTip="Center" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label Style="font-size: 12px;" runat="server" ID="Label129">Course</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblcoursep" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label131">Batch</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblbatchp" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" ID="Label133" Style="font-size: 12px;">Category</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblcategoryP" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label135">Test Type</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            
                                              <asp:Label runat="server" ID="testP" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" ID="Label137" Style="font-size: 12px;">Test Name</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="TestNameP" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server"  Style="font-size: 12px;" ID="Label139">Test Date</asp:Label>
                                            </b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="DateP" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 55%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label141">Out of Marks</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 4                5%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="MaxMarkP" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label Style="font-size: 12px;" runat="server" ID="Label143">Issue Date</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="DateIssueP" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 45%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label145"> Exp Sub Date</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 55%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblreturndateP" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 55%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label147">Total Students  </asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 45%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="total_studentP" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label Style="font-size: 12px;" runat="server" ID="Label149">Absent</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblprintabsentP" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 45%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label151">Present</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 55%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblprintpresentP" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr> 
                          <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 45%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label153">Subject</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 55%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblsubjectP" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                             <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 45%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label155">Papers Corrected</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 55%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblpapercorrectedP" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                     
                       
                        <tr>
                            <td class="span12" colspan="3" style="height:30px">
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <span style="font-size: 12px;">Office Staff(Sign)</span>
                            </td>
                            <td class="span4" style="text-align: center">
                                <span style="font-size: 12px;">Office (Stamp)</span>
                            </td>
                            <td class="span4" style="text-align: right">
                                <span style="font-size: 12px;">Paper Corrector(Sign)</span>
                            </td>
                        </tr>
                        <%--<tr>
                            <td class="span12" colspan="3">
                                <span style="font-size: 12px;">Note: <br />
                                    1.Please Verify the Total number of papers given to you in the Paper Bundle and the count given in the details above.<br>
                                    2.Ensure that you submit the papers on or before the Expected submission Date.<br>
                                    3.Collect your paper correction submission slip when you submit your papers.<br>
                                    <asp:Label ID="Label120" runat="server" Visible="false"></asp:Label>
                                    
                                    
                                    
                                    </span>
                            </td>
                        </tr>--%>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
    <!--/#page-content-->
</asp:Content>
