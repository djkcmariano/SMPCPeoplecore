<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayList_MainIncomeMassCrit.aspx.vb" Inherits="Secured_PayMainIncomeMassList_Crit" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<script type="text/javascript">
    function cbCheckAll_CheckedChanged(s, e) {
        grdMain.PerformCallback(s.GetChecked().toString());
    }
</script>

<div class="page-content-wrap">  
    <uc:PayHeader runat="server" ID="PayHeader" />
    <uc:FormTab runat="server" ID="FormTab" /> 
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">     
                <div class="panel-title">                                           
                    Template Income
                </div>                           
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
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
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayMainIncomeMassCritNo"
                        OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataComboBoxColumn FieldName="IncomeDescription" Caption="Template Income" />
                                <dx:GridViewDataComboBoxColumn FieldName="PayIncomeTypeDesc" Caption="Income Type" />
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
					                <HeaderTemplate>
                                        <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                        </dx:ASPxCheckBox>
                                    </HeaderTemplate>
				                </dx:GridViewCommandColumn>
                            </Columns>                     
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>

<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" CancelControlID="lnkClose" PopupControlID="Panel2" TargetControlID="btnShow" />
<asp:Panel id="Panel2" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">      
         <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />   
         </div>         
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" />
                </div>
            </div> 
            
           <div class="form-group">
                <label class="col-md-4 control-label  has-required">Template Income :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPayMainIncomeMassNo" runat="server" CssClass="required form-control" />                    
                </div>
            </div> 
           
            <br /> 
        </div>         
        <div class="cf popupfooter">
        </div> 
    </fieldset>
</asp:Panel>

</asp:Content> 