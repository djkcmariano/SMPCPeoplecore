<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayHDMFTableList.aspx.vb" Inherits="Secured_PayHDMFTableList" %>

<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Textbox ID="txtYear" runat="server" CssClass="number form-control" AutoPostBack="true" OnTextChanged="txtYear_TextChanged" ></asp:Textbox>
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
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayHDMFTableNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                    <dx:GridViewDataTextColumn FieldName="SalaryCredit" Caption="Salary Credit" />
                                    
                                    <dx:GridViewBandColumn Caption="HDMF Share" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="EmployeeHDMF" Caption="EE" />
                                            <dx:GridViewDataTextColumn FieldName="EmployerHDMF" Caption="ER" />
                                        </Columns>
                                    </dx:GridViewBandColumn>
                                    <dx:GridViewBandColumn Caption="Cover Year" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="FromYear" Caption="From" />
                                            <dx:GridViewDataTextColumn FieldName="ToYear" Caption="To" />
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
                <div class="col-md-6">
                    <asp:Textbox ID="txtPayHDMFTableNo" ReadOnly="true" runat="server" CssClass="form-control"
                        ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Reference No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder= "Autonumber"></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Salary Credit :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtSalaryCredit" runat="server" CssClass="required number form-control"></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtSalaryCredit" />
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">HDMF Share EE :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtEmployeeHDMF" runat="server" CssClass="required number form-control" ></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtEmployeeHDMF" />
                   
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">HDMF Share ER :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtEmployerHDMF" runat="server" CssClass="required number form-control"></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtEmployerHDMF" />
                    
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Cover Year From:</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtFromYear" runat="server" CssClass="required number form-control"></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers" TargetControlID="txtFromYear" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Cover Year To:</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtToYear" runat="server" CssClass="required number form-control"></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers" TargetControlID="txtToYear" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Company Name :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass="number form-control" >
                    </asp:Dropdownlist>
                </div>
            </div>
        </div>
        <br />
         </fieldset>
</asp:Panel>
</asp:content>

