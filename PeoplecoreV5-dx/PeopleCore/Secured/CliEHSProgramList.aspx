<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="CliEHSProgramList.aspx.vb" Inherits="Secured_EHSProgramList" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<script type="text/javascript">

    function grid_ContextMenu(s, e) {
        if (e.objectType == "row")
            hiddenfield.Set('VisibleIndex', parseInt(e.index));
        pmRowMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
    }

    function gridDetl_ContextMenu(s, e) {
        if (e.objectType == "row")
            hiddenfield1.Set('VisibleIndex', parseInt(e.index));
        pmRowMenuDetl.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
    }
</script>
 
<div class="row">
    <div class="panel panel-default">
        <div class="panel-heading">                                
            <div class="col-md-2">
                <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
            </div>                
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>                    
                    <ul class="panel-controls">                        
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                    </ul>              
                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                           
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkExport" />
                </Triggers>
                </asp:UpdatePanel>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EHSProgramNo"
                        OnFillContextMenuItems="grdMain_FillContextMenuItems">                                                                                   
                        <Columns>
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                            <dx:GridViewDataTextColumn FieldName="EHSProgramDesc" Caption="Program Name" />  
                            <dx:GridViewDataComboBoxColumn FieldName="EHSProgramTypeDesc" Caption="Program Type" />
                            <dx:GridViewDataTextColumn FieldName="FromDate" Caption="Date From" />
                            <dx:GridViewDataTextColumn FieldName="ToDate" Caption="Date To" />
                            <dx:GridViewDataTextColumn FieldName="NoOfDay" Caption="No. of Days" Visible="false" /> 
                            <dx:GridViewDataTextColumn FieldName="Budget" Caption="Budget" PropertiesTextEdit-DisplayFormatString="{0:N2}" />                                                                         
                            <dx:GridViewDataColumn Caption="Participants"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkSummary" CssClass="fa fa-external-link control-primary" Font-Size="Medium" OnClick="lnkSummary_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="5%" />     
                        </Columns>       
                        <ClientSideEvents ContextMenu="grid_ContextMenu" />                
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />

                    <dx:ASPxPopupMenu ID="pmRowMenu" runat="server" ClientInstanceName="pmRowMenu">
                        <Items>
                            <dx:MenuItem Text="Report" Name="Name">
                                <Template>
                                    <asp:LinkButton runat="server" ID="lnkPrint" OnClick="lnkPrint_Click" CssClass="control-primary" Font-Size="small" OnPreRender="lnkPrint_PreRender" Text="Payroll Register Report" /><br />
                                </Template>
                            </dx:MenuItem>
                        </Items>
                        <ItemStyle Width="250px"></ItemStyle>
                    </dx:ASPxPopupMenu>
                    <dx:ASPxHiddenField ID="hf" runat="server" ClientInstanceName="hiddenfield" />

                </div>
            </div>                                                           
        </div> 
    </div>
</div>


</asp:Content>

