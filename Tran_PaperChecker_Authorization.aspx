<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true"
    CodeFile="Tran_PaperChecker_Authorization.aspx.cs" Inherits="Tran_PaperChecker_Authorization" %>

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
                <h5 class="smaller">
                    Paper Checker Authorization<span class="divider"></span></h5>
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
                                                                <asp:Label runat="server" ID="Label18" CssClass="red">Center(s)</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlCenter" Width="215px" data-placeholder="Select Center" AutoPostBack="true"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" onselectedindexchanged="ddlCenter_SelectedIndexChanged" 
                                                                     />
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
                                                                <asp:DropDownList runat="server" ID="ddlStandard" Width="210px" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label29" CssClass="red"> Test Date</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span8" name="date-range-picker"
                                                                    id="id_date_range_picker_1" placeholder="Date Search" data-placement="bottom"
                                                                    data-original-title="Date Range" style="width: 250px" />
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
                                    <asp:LinkButton runat="server" ID="HLExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" OnClick="HLExport_Click" />
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
                                        <asp:Label runat="server" ID="Label215">Center(s)</asp:Label>
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
                                        <asp:Label runat="server" ID="Label5">Test Period</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblPeriod" Text="" CssClass="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                        </td>
                    </tr>
                </table>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkTestAll" runat="server" 
                            oncheckedchanged="chkTestAll_CheckedChanged" AutoPostBack="true" />
                        <span class="lbl"></span>
                        </th>
                        <th style="width: 12%; text-align: center">
                            Test Name
                        </th>
                        <th style="width: 11%; text-align: center">
                            Batch
                        </th>
                        <th style="width: 14%; text-align: center">
                            Subjects
                        </th>
                        <th style="width: 15%; text-align: center">
                            Paper Corrector
                        </th>                        
                        <th style="width: 8%; text-align: center">
                            Out of Marks
                        </th>
                        <th style="width: 8%; text-align: center">
                            Paper Count
                        </th>
                        <th style="width: 8%; text-align: center">
                            Slab Name
                        </th>
                        <th style="width: 8%; text-align: center">
                            Rate
                        </th>
                        <th style="width: 10%; text-align: center">
                            Amount
                        </th>
                        <th style="width: 4%; text-align: center">
                        </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkTest" runat="server" Visible='<%#(int)DataBinder.Eval(Container.DataItem,"PCIsAuthorised") == 1 ? false : true%>'/>
                        <span class="lbl"></span></td>
                        <td style="width: 15%; text-align: left">
                            <asp:Label ID="lblTest_Name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                            <asp:Label ID="lblConductNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Conduct_No")%>'
                                Visible="false" />
                            <asp:Label ID="lblTest_ID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_ID")%>'
                                Visible="false" />
                            <asp:Label ID="lblBatchCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch_Code")%>'
                                Visible="false" />
                            <asp:Label ID="lblPartner_Code" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Partner_Code")%>'
                                Visible="false" />
                            <asp:Label ID="lblSlab_Code" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Slab_Code")%>'
                                Visible="false" />
                            <asp:Label ID="lblBagDispatch_ID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BagDispatch_ID")%>'
                                Visible="false" />
                            <asp:Label ID="lblCenter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Centre_Code")%>'
                                Visible="false" />
                        </td>
                        <td style="width: 15%; text-align: left">
                            <asp:Label ID="lblBatchName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                        </td>
                        <td style="width: 15%; text-align: left">
                            <asp:Label ID="lblDLSubjects" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subjects")%>' />
                        </td>
                        <td style="width: 15%; text-align: left">
                            <asp:Label ID="lblFaculty_Name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Faculty_Name")%>' />
                        </td>                        
                        <td style="width: 10%; text-align: center">
                            <asp:Label ID="lblDLMaxMarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MaxMarks")%>' />
                        </td>
                        <td style="width: 10%; text-align: center">
                            <asp:Label ID="lblIssue_Quantity" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Issue_Quantity")%>' />
                        </td>
                        <td style="width: 10%; text-align: center">
                            <asp:Label ID="lblSlab_Name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Slab_Name")%>' />
                        </td>
                        <td style="width: 10%; text-align: center">
                            <asp:Label ID="lblRate" runat="server" Text='<%#  GetRateValue (Eval("Rate") )%>' />
                        </td>
                        <td style="width: 10%; text-align: center">
                            <asp:Label ID="lblAmount" runat="server" Text='<%#  GetRateValue(Eval("Amount")) %>' />
                        </td>
                        <td style="width: 10%; text-align: center">
                            <asp:LinkButton ID="lnkDLAuthorise" ToolTip="Authorised" class="btn-small btn-warning  icon-lock" Style="background-color: Green!important"
                             runat="server" CommandName="Details" Height="25px" Visible='<%#(int)DataBinder.Eval(Container.DataItem,"PCIsAuthorised") == 1 ? true : false%>' />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dlGridExport" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" Visible="false">
                    <HeaderTemplate>
                       
                       
                      <b>
                            Test Name</b>
                        </th>
                        <th style="width: 17%; text-align: center">
                            Batch
                        </th>
                        <th style="width: 17%; text-align: center">
                            Subjects
                        </th>
                        <th style="width: 15%; text-align: center">
                            Paper Corrector
                        </th>                        
                        <th style="width: 8%; text-align: center">
                            Out of Marks
                        </th>
                        <th style="width: 10%; text-align: center">
                            Paper Count
                        </th>
                        <th style="width: 10%; text-align: center">
                            Slab Name
                        </th>
                        <th style="width: 10%; text-align: center">
                            Rate
                        </th>
                        <th style="width: 10%; text-align: center">
                            Amount
                        </th>
                       
                    </HeaderTemplate>
                    <ItemTemplate>
                       
                        
                            <asp:Label ID="lblTest_Name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                            <asp:Label ID="lblConductNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Conduct_No")%>'
                                Visible="false" />
                            <asp:Label ID="lblTest_ID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_ID")%>'
                                Visible="false" />
                            <asp:Label ID="lblBatchCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch_Code")%>'
                                Visible="false" />
                            <asp:Label ID="lblPartner_Code" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Partner_Code")%>'
                                Visible="false" />
                            <asp:Label ID="lblSlab_Code" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Slab_Code")%>'
                                Visible="false" />
                            <asp:Label ID="lblBagDispatch_ID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BagDispatch_ID")%>'
                                Visible="false" />
                                <asp:Label ID="lblCenter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Centre_Code")%>'
                                Visible="false" />
                        </td>
                        <td style="width: 20%; text-align: left">
                            <asp:Label ID="lblBatchName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                        </td>
                        <td style="width: 20%; text-align: left">
                            <asp:Label ID="lblDLSubjects" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subjects")%>' />
                        </td>
                        <td style="width: 15%; text-align: left">
                            <asp:Label ID="lblFaculty_Name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Faculty_Name")%>' />
                        </td>    
                        <td style="width: 10%; text-align: center">
                            <asp:Label ID="lblDLMaxMarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MaxMarks")%>' />
                        </td>
                        <td style="width: 10%; text-align: center">
                            <asp:Label ID="lblIssue_Quantity" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Issue_Quantity")%>' />
                        </td>
                        <td style="width: 10%; text-align: center">
                            <asp:Label ID="lblSlab_Name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Slab_Name")%>' />
                        </td>
                        <td style="width: 10%; text-align: center">
                            <asp:Label ID="lblRate" runat="server" Text='<%#  GetRateValue (Eval("Rate") )%>' />
                        </td>
                        <td style="width: 10%; text-align: center">
                            <asp:Label ID="lblAmount" runat="server" Text='<%#  GetRateValue(Eval("Amount")) %>' />
                        </td>
                      
                    </ItemTemplate>
                </asp:DataList>
               
                <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                    <!--Button Area -->
                    
                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" 
                        ID="BtnAuthorization" runat="server"
                        Text="Authorization" Width="120px" onclick="BtnAuthorization_Click"/>
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnClose" Visible="true"
                        runat="server" Text="Close" onclick="BtnClose_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
