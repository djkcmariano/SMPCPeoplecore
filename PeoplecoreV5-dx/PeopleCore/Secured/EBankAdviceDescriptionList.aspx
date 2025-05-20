<%@ Page Title="" Language="VB" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EBankAdviceDescriptionList.aspx.vb" Inherits="Secured_EBankAdviceDescriptionList" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">    
 <br />
    <div class="page-content-wrap" >         
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                    </div>
                    <div>   
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>                                              
                        <ul class="panel-controls">                                    
                            <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                            <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                            <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        </ul>                                                                                                                                                     
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BankDescriptionNo">                                                                                   
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." /> 
                                    <dx:GridViewDataTextColumn FieldName="BankDescriptionCode" Caption="Code" />                                                                                
                                    <dx:GridViewDataTextColumn FieldName="BankDescriptionDesc" Caption="Description" />
                                    <dx:GridViewDataTextColumn FieldName="IncomeDescription" Caption="Income Description" />                                  
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
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlpopupMain"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="lnkSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">

            <div class="form-group">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" Enabled="false" runat="server" CssClass="form-control" ></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtBankDescriptionCode" runat="server" CssClass="required form-control"></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtBankDescriptionDesc" runat="server" CssClass="required form-control"></asp:Textbox>
                </div>
            </div> 
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Income Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtIncomeDesc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:Textbox>
                </div>
           </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Company Name :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                    </asp:Dropdownlist>
                </div>
            </div> 

            <div class="form-group" style="visibility:hidden;">
                <label class="col-md-4 control-label">Please check here</label>
                <div class="col-md-7">
                        <asp:CheckBox ID="txtIsApplyToAll" runat="server" />  &nbsp;
                        <span >if visible to all company. </span>
                </div>
            </div>
            <br />
            </div>
          <!-- Footer here -->
         </fieldset>
</asp:Panel> 
</asp:Content>

