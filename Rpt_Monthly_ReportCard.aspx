<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="Rpt_Monthly_ReportCard.aspx.cs" Inherits="Rpt_Studentwise_Absentisum_Detailed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
        setTimeout(function () {
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

        
            
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                  Report Card - Summary<span class="divider"></span></h5>
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
                                                                <asp:Label runat="server" ID="Label117" CssClass="red">Course</asp:Label>
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
                                                                <asp:Label runat="server" ID="Label18" CssClass="red">Center</asp:Label>
                                                            </td>
                                                            <%--<td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCentre" Width="215px" data-placeholder="Select Center"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCentre_SelectedIndexChanged" />
                                                            </td>--%>
                                                             <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlCentre" Width="215px" data-placeholder="Select Center"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="True"  OnSelectedIndexChanged="ddlCentre_SelectedIndexChanged"  />
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
                                                           <%-- <td style="border-style: none; text-align: left; width: 60%;">
                                                                 <asp:DropDownList runat="server" ID="ddlBatch" Width="215px" data-placeholder="Select Batch"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" />
                                                            </td>--%>
                                                              <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlBatch" Width="215px" data-placeholder="Select Batch"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="True"  OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label30" CssClass="red">Roll No.</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                 <asp:DropDownList runat="server" ID="ddlRollNo" Width="215px" data-placeholder="Select Roll No"
                                                                    CssClass="chzn-select" AutoPostBack="True"  />
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
                                                                <asp:Label runat="server" ID="Label29" >  Period</asp:Label>
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
                                                    
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    
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
                                    <asp:LinkButton ID="HLExport" Font-Underline="true" Height="30px" ToolTip="Export" 
                                        class="btn-small btn-danger icon-2x icon-download-alt" runat="server" OnClick="HLExport_Click" />
                                   
                                     
                             <a href="#"  onclick="javascript:PrintDiv();"  runat="server" id="btnPrint" title="Print" class="btn btn-app btn-warning btn-mini radius-1 icon-print" style="height:15px ;width:32px" ></a>
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
                                        <asp:Label runat="server" ID="Label21">Student Name</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblStudent_name" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label27">Roll No</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblRoll_No" class="blue"></asp:Label>
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
                                        <asp:Label runat="server" ID="Label215">Period</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                          
                                            <asp:Label runat="server" ID="lblperiod" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label216">Center</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lblCentre_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <%--<td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label217">Batch</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblBatch_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>--%>
                        <%--<td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label3">Roll No</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblRollNo_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>--%>
                    </tr>
                    <%--<tr>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label4">Period</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lblPeriod_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                
                    </tr>--%>
                </table>
         <asp:DataList ID="dlGridDisplay"  runat="server" HorizontalAlign="Left"
                    CssClass="table table-striped table-bordered table-hover" Width="100%">
                    <HeaderTemplate>
                        <b>Date</b> </th>
                        <th align="left" style="width: 12%">
                           Exam Type
                        </th>
                        <th align="left" style="width: 12%">
                            
                            Subject
                        </th>
                        <th align="left" style="width: 12%">
                            Test Name
                        </th>
                        <th align="center" style="width: 11%">
                            Syllabus
                        </th>
                        <th style="width: 14%; text-align: center">
                            Attendance
                        </th>
                        <th style="width: 13%; text-align: center">
                        Score
                        </th>
                        <th style="width: 13%; text-align: center">
                        Out Of Marks
                        
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Date")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Exam Type]")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                        </td>
                           <td>
                            <asp:Label ID="Label26" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_ID")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label33" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapters")%>' />
                        </td>
                     
                        <td>
                            <asp:Label ID="Label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AttendStatus")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ObtdMarks")%>' />
                            </td>
                            <td>
                              <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MaxMarks")%>' />
                    </ItemTemplate>

                </asp:DataList>
                
            </div>
      
        </div>
        <div id="DivResult" runat="server" class="dataTables_wrapper"  visible="false">
        <table cellpadding="3"  class="table table-striped table-bordered table-condensed" >
                    <tr>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label2">Total Applicable Test</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lbltotalapplicable_Test" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label4">Total present Test</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lbltotal_presentTest" class="blue"></asp:Label>
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
                                        <asp:Label runat="server" ID="Label6">Absentiseem Percantage</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                          
                                            <asp:Label runat="server" ID="lblabsentpersent" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <%--<td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label8">Center</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="Label9" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>--%>
                
                    </tr>
             
                </table>
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
                        <td class="span4" style="text-align: left" >
                                <img src="images/logo.jpg" alt="" style="width: 146px; height: 70px" />
                            </td>
                           
                            <td class="span4" style="text-align: left">
                                <p style="font-size: 25px;">MT EDUCARE LTD.</p> 
                                                            
                           

                            </td>
                            </tr>

                               <tr>
                        <td class="span4" >
                                
                            </td>
                           
                            <td class="span4" style="text-align: left">
                                
                                 <p style="font-size: 22px;"> Monthly Report Card</p>                              
                                &nbsp;&nbsp;&nbsp;&nbsp;

                            </td>
                            </tr>
                     
                        <tr>

                            <td class="span6" style="text-align: left">
                                <table width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" ID="Label35" >Student Name:</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblPrintstudent" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span8" style="text-align: left">
                                 &nbsp;&nbsp; 
                            </td>
                            <td class="span8" style="text-align: left">
                                <table width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" ID="Label36" >Roll No</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblPrintrollNo"/>
                                        </td>
                                    </tr>
                                </table>
                            </td> 
                            
                        </tr>
                        <tr>
                            <td class="span8" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label  runat="server" ID="Label40">Period</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblPrintperiod" Style="font-size: 14px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                               <td class="span8" style="text-align: left">
                                 &nbsp;&nbsp; 
                            </td>
                            <td class="span8" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server"  ID="Label41">Center</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblPrint_Center" Style="font-size: 14px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            
                        </tr>
              
                        <tr>
                            <td colspan="3" class="span12" style="text-align: center">
                                <asp:Repeater ID="dsPrint" runat="server">
                                    <HeaderTemplate>
                                        <table width="100%"  cellpadding="0" cellspacing="0" style="border-color: Black ;border: 1px; border-style: solid;">
                                            <th style="text-align: left; border-color: Black; border: 1px; border-style: solid">
                                             <span style="font-size: 10px;"> Test Date</span>
                                            </th>
                                            <th style="text-align: left; border-color: Black; border: 1px; border-style: solid; padding-left: 5px">
                                               <span style="font-size: 12px;"> Exam Type</span>
                                            </th>
                                            <th style="text-align: center; border-color: Black; border: 1px; border-style: solid">
                                              <span style="font-size: 12px;"> Subjects</span>
                                            </th>
                                            <th style="text-align: center; border-color: Black; border: 1px; border-style: solid">
                                             <span style="font-size: 12px;"> Test Name  </span>
                                            </th>
                                            <th style="text-align: center; border-color: Black; border: 1px; border-style: solid">
                                             <span style="font-size: 12px;"> Chapter  </span>
                                            </th>
                                            <th style="text-align: center; border-color: Black; border: 1px; border-style: solid">
                                             <span style="font-size: 12;"> Attend</span>
                                            </th>
                                            <th style="text-align: center; border-color: Black; border: 1px; border-style: solid">
                                             <span style="font-size: 12px;"> Score  </span>
                                            </th>
                                            <th style="text-align: center; border-color: Black; border: 1px; border-style: solid">
                                             <span style="font-size: 12px;"> Out of Marks  </span>
                                            </th>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: center; border-color:Gray; border: 1px; border-style: solid;   padding-left: 5px"">
                                                <asp:Label   ID="lbltestdate"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Date")%>' />
                                            </td>
                                            <td style="text-align: left; border-color: Gray; border: 1px; border-style: solid;   padding-left: 5px"">
                                                <asp:Label  ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Exam Type")%>' />
                                            </td>
                                            <td style="text-align: center;  border-color: Gray; border: 1px; border-style: solid;   padding-left: 5px"">
                                                <asp:Label  ID="Label49" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                                            </td>
                                             <td style="text-align: center;  border-color: Gray; border: 1px; border-style: solid;   padding-left: 5px"">
                                                <asp:Label  ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_ID")%>' />
                                            </td>
                                             <td style="text-align: center;  border-color: Gray; border: 1px; border-style: solid;   padding-left: 5px"">
                                                <asp:Label  ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapters")%>' />
                                            </td>
                                             <td style="text-align: center;  border-color: Gray; border: 1px; border-style: solid;   padding-left: 5px"">
                                                <asp:Label  ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AttendStatus")%>' />
                                            </td>
                                             <td style="text-align: center;  border-color: Gray; border: 1px; border-style: solid;   padding-left: 5px"">
                                                <asp:Label  ID="Label7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ObtdMarks")%>' />
                                            </td>
                                             <td style="text-align: center;  border-color: Gray; border: 1px; border-style: solid;   padding-left: 5px"">
                                                <asp:Label  ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MaxMarks")%>' />
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
                       <tr>
                            <td class="span4" style="text-align: left">
                                <span >Total Applicable Test :</span>

                            <asp:Label runat="server" ID="lbltotaltest"  /></td>
                            <td class="span4" style="text-align: center">
                                <span >Total present Test :</span>
                                <asp:Label runat="server" ID="lbltotalpresent_test"  />
                            </td>
                            <td class="span4" style="text-align: right">
                                <span >Absentiseem Percantage :</span>
                                <asp:Label runat="server" ID="lblabsent_percentage"  />
                            </td>
                        </tr>
                    
                    </table>
                </td>
            </tr>
        </table>
    </div>
   

</asp:Content>

