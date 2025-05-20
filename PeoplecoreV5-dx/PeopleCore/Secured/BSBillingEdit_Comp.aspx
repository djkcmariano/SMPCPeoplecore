<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BSBillingEdit_Comp.aspx.vb" Inherits="Secured_BSBillingEdit_Comp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<uc:Tab runat="server" ID="Tab" HeaderVisible="true">

<Content>
 <br />
<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <h3>&nbsp;</h3>
                </div>
                <div>
                    &nbsp;                  
                </div> 
                <div>                                                
                    <ul class="panel-controls"> 
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                        <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                    </ul>                                                                                                                                                     
                </div>
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BSCompNo">                                                                                   
                        <Columns>         
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("BSCompNo") %>' OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="BSCompTypeDesc" Caption="Component"/>
                            <dx:GridViewDataCheckColumn FieldName="IsTax" Caption="Tax?" /> 
                            <dx:GridViewDataCheckColumn FieldName="IsASF" Caption="ASF?" />                                                    
                            <dx:GridViewDataCheckColumn FieldName="IsIncludeBillRate" Caption="Integrate Billing Rate?" Visible="false" />   
                            <dx:GridViewDataCheckColumn FieldName="IsAccrued" Caption="For Accrual?" />
                            <dx:GridViewDataTextColumn FieldName="PercentAccrued" Caption="Percentage for Accrual"/>                          
                            <dx:GridViewDataTextColumn FieldName="ContributionFormulaDesc" Caption="Basis for Accrual"/>
                            <dx:GridViewDataTextColumn FieldName="OrderNo" Caption="Order No."/>
                            
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />                     
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                        
                </div>                            
            </div>
        </div>
    </div>
</div>

</Content>
</uc:Tab>
<br /><br />

<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mdlPopupMain" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClose" PopupControlID="pnlPopupMain" TargetControlID="btnShow" />
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none;">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>
            </div>
                
            <div class="form-group">
                <label class="col-md-4 control-label has-space-required">Component :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboBSCompTypeNo" runat="server" CssClass="form-control required" DataMember="BBSCompType" />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">With Tax?</label>
                <div class="col-md-6">
                    <label class="check">
                        <asp:Checkbox ID="txtIsTax" Visible="True"  runat="server" CssClass="icheckbox"></asp:Checkbox>
                    </label>
                </div>
            </div> 
             <div class="form-group">
                <label class="col-md-4 control-label has-space">With ASF?</label>
                <div class="col-md-6">
                    <label class="check">
                        <asp:Checkbox ID="txtIsASF" Visible="True"  runat="server" CssClass="icheckbox"></asp:Checkbox>
                    </label>
                </div>
            </div> 
            <div class="form-group" runat="server" visible="false">
                <label class="col-md-4 control-label has-space">Integrate Billing Rate?</label>
                <div class="col-md-6">
                    <label class="check">
                        <asp:Checkbox ID="txtIsIncludeBillRate" Visible="True"  runat="server" CssClass="icheckbox"></asp:Checkbox>
                    </label>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">For Accrual?</label>
                <div class="col-md-6">
                    <label class="check">
                        <asp:Checkbox ID="txtIsAccrued"   runat="server" CssClass="icheckbox"></asp:Checkbox>
                    </label>
                </div>
            </div> 
            <div id="Div1" class="form-group" >
                <label class="col-md-4 control-label has-space">Percentage use for accrual :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPercentAccrued" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtPercentAccrued" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Basis for Accrual :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboAccruedBasisNo" runat="server" CssClass="form-control" DataMember="EContributionFormula" />                    
                </div>
            </div>

            <div class="form-group" >
                <label class="col-md-4 control-label has-space">Order No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtOrderNo" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" TargetControlID="txtOrderNo" />
                </div>
            </div>
            
                           
        </div>
        <br />
        </fieldset>
    </asp:Panel>

</asp:Content>