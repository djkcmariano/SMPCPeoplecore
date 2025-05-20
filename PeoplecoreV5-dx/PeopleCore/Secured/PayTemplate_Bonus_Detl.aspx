<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="PayTemplate_Bonus_Detl.aspx.vb" Inherits="Secured_PayTemplate_Bonus_Detl" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<uc:Tab runat="server" menustyle="TabRef" ID="Tab">
    <Content>
        
        <div class="page-content-wrap" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-4">
                            <h4 class="panel-title">13th Month/Bonus Entitlement</h4>
                            &nbsp;
                        </div>
                        <div>                                                
                            <ul class="panel-controls">                                                                                
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                                      
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ul>                                                                                                                                                     
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdDetl" runat="server" KeyFieldName="PayTemplateBonusDetiNo" Width="100%">
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="SFrom" Caption="From (LS in month)" />
                                        <dx:GridViewDataTextColumn FieldName="STo" Caption="To (LS in month)" />
                                        <dx:GridViewDataTextColumn FieldName="PercentFactor" Caption="Factor Rate" />

                                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="5%" />                                                             
                                    </Columns>
                        
                                    <Settings ShowFooter="true" />       
                                   
                                </dx:ASPxGridView>    
                                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                  
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>
        </div> 
        
        
    </Content>
</uc:Tab> 
    
<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShow" PopupControlID="Panel2" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />

<asp:Panel id="Panel2" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">        
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />   
        </div>         
        <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtPayTemplateBonusDetiNo" runat="server" ReadOnly="true" CssClass="form-control"></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" />                        
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">From (LS in month):</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtSFrom" runat="server" CssClass="required form-control number" /> 
                              
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">To (LS in month):</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtSTo" runat="server" CssClass="required form-control number" /> 
                              
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Percent factor :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtPercentFactor" runat="server" CssClass="required form-control number" /> 
                              
                </div>
            </div>


            <br />
        </div>                
    </fieldset>
</asp:Panel>



</asp:Content>

