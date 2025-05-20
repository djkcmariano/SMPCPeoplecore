<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PYActivityType.aspx.vb" Inherits="Secured_PYActivityType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<br />
<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control"  runat="server" />     
                </div>
                
                <div>                                                
                    <ul class="panel-controls"> 
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" Visible="false" CssClass="control-primary" /></li>                            
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" Visible="false" CssClass="control-primary" /></li>                                                        
                        <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                    </ul>                                                                                                                                                     
                </div>
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PositionNo">                                                                                   
                        <Columns>    
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("PositionNo") %>' OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                                                    
                            <dx:GridViewDataTextColumn FieldName="PositionCode" Caption="Code" />
                            <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Description"/>
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>                                                        
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                               
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                        
                </div>                            
            </div>
        </div>
    </div>
</div>

<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6">
                    <div class="panel-title">
                        <asp:Label runat="server" ID="lbl" />
                    </div>
                </div>
                <div>                    
                    <ul class="panel-controls">                            
                        <li><asp:LinkButton runat="server" ID="lnkAddD" OnClick="lnkAddD_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDeletD" OnClick="lnkDeleteD_Click" Text="Delete" CssClass="control-primary" /></li>
                    </ul>                       
                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                 
                </div> 
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="PYActivityTypeDetiNo" Width="100%">
                        <Columns>      
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("PYActivityTypeDetiNo") %>' OnClick="lnkEditD_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>                      
                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." />
                            <dx:GridViewDataComboBoxColumn FieldName="PYActivityTypeDetiCode" Caption="Code" />
                            <dx:GridViewDataComboBoxColumn FieldName="PYActivityTypeDetiDesc" Caption="Description" /> 
                            <dx:GridViewDataComboBoxColumn FieldName="PYModePaymentDesc" Caption="Mode of Payment" />
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="grdMain" />                                        
                </div>                            
            </div>
        </div>
    </div>
</div>
<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mdlDetl" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClose" PopupControlID="pnlPopupDetl" TargetControlID="btnShowDetl" />
<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style="display:none;">
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
                    <asp:TextBox ID="txtPYActivityTypeNo" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>
            </div>
                
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Code :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPYActivityTypeCode" runat="server" CssClass="form-control" />              
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Description :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPYActivityTypeDesc" runat="server" CssClass="form-control" />                   
                </div>
            </div>

            <div class="form-group" >
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" />     
                </div>
            </div>
                           
        </div>
        <br />
        </fieldset>
    </asp:Panel>

<asp:Button ID="btnShowRate" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mdlRate" runat="server" BackgroundCssClass="modalBackground" CancelControlID="lnkClose" PopupControlID="pnlPopupRate" TargetControlID="btnShowRate" />
<asp:Panel id="pnlPopupRate" runat="server" CssClass="entryPopup" style="display:none;">
       <fieldset class="form" id="fsDetl">
        <!-- Header here -->
         <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsDetl lnkSave" OnClick="lnkSave_Click"  />      
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
                <label class="col-md-4 control-label has-space">Code :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPYActivityTypeDetiCode" runat="server" CssClass="form-control" />              
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Description :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPYActivityTypeDetiDesc" runat="server" CssClass="form-control" />                   
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Mode payment :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPYModePaymentNo" runat="server" CssClass="form-control" DataMember="EPYModePayment" />                  
                </div>
            </div>
                                         
            <br /><br />
        </div>        
        </fieldset>
</asp:Panel>

<br /><br />
</asp:Content>