<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayTaxAnnualizedList.aspx.vb" Inherits="Secured_PayTaxAnnualizedList" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        
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
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="TaxAnnualizedNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                    <dx:GridViewDataTextColumn FieldName="OverAmount" Caption="Over Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="NotOverAmount" Caption="Not Over Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="BaseTax" Caption="Base Tax" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="PercentTax" Caption="Percent Tax" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewBandColumn Caption="Year" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="FromYear" Caption="From" HeaderStyle-HorizontalAlign="Center"/>
                                            <dx:GridViewDataTextColumn FieldName="ToYear" Caption="To" HeaderStyle-HorizontalAlign="Center"/>
                                        </Columns>
                                    </dx:GridViewBandColumn> 
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" /> 
                                </Columns>                            
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />    
                        </div>
                    </div>
                </div>
                   
            </div>
       </div>
 </div>


 <asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="lnkSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtTaxAnnualizedNo" ReadOnly="true" runat="server"  CssClass="form-control"></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server"  CssClass="form-control" Placeholder = "Autonumber"></asp:Textbox>
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Over Amount :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtOverAmount" runat="server" CssClass="number form-control required" ></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtOverAmount" />
               </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Not Over Amount :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtNotOverAmount" runat="server" CssClass="number form-control required" ></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtNotOverAmount" />
               </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Base Tax :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtBaseTax" runat="server" CssClass="number form-control required" ></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtBaseTax" />
               </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Percent Tax :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtPerCentTax" runat="server" CssClass="number form-control required" ></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtPerCentTax" />
               </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">From Year :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtFromYear" runat="server" CssClass="number form-control required" ></asp:Textbox>
               </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">To Year :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtToYear" runat="server" CssClass="number form-control required" ></asp:Textbox>
                    
               </div>
            </div>


        </div>
        <br />

         </fieldset>
</asp:Panel>

</asp:content>

