<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactImageUpload.aspx.cs" Inherits="ContactImageUpload" MasterPageFile="~/Menu.master" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
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
            <li>
                <h5 class="smaller">
                    Upload Contact<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            
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
        

        <table cellpadding="0" style="border-style: none;" class="table-hover" width="50%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 19.75%;">
                                                            <asp:Label runat="server" ID="Label10">Select File</asp:Label>&nbsp;<span class="help-button ace-popover"
                                                                data-trigger="hover" data-placement="right" data-content="Select a Image file using Browse button"
                                                                title="Select File">?</span>
                                                            
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 80.25%;">
                                                            <div class="row-fluid">
                                                                <asp:FileUpload ID="fileContact" runat="server" Width="70%" />
                                                                <asp:Button class="btn btn-app btn-info btn-mini radius-4" ID="btnUpload" runat="server"
                                                                    Text="Upload" onclick="btnUpload_Click"  />
                                                            </div>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </table>
    </div>



        
    
    </asp:Content>
