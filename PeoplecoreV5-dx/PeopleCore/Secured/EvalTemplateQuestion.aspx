<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EvalTemplateQuestion.aspx.vb" Inherits="Secured_EvalTemplateQuestion" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">
        <Header>                       
            <asp:Label runat="server" ID="lbl" />            
        </Header>
        <Content>
            <br />
            <div class="page-content-wrap" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-2">
                            
                        </div>
                        <div>
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
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EvalTemplateDetlNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="EvalTemplateCateDesc" Caption="Category" />                                                                           
                                        <dx:GridViewDataTextColumn FieldName="Question" Caption="Question" /> 
                                        <dx:GridViewDataComboBoxColumn FieldName="ResponseTypeDesc" Caption="Response Type" />                  
                                        <dx:GridViewDataComboBoxColumn FieldName="OrderBy" Caption="Order" /> 
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>                                                                                                                                                    
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" SelectAllCheckboxMode="Page" />
                                    </Columns>                            
                                </dx:ASPxGridView>   
                                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                             
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>
        </div>

            <br /><br />
            <div class="page-content-wrap" >         
                <div class="row">
                        <div class="panel panel-default"> 
                            <div class="panel-heading">
                                <div class="col-md-6">
                                    <h4 class="panel-title">Transaction No.: <asp:Label ID="lblDetl" runat="server"></asp:Label></h4>
                                </div>
                                <div>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                    <ContentTemplate>                    
                                        <ul class="panel-controls">
                                            <li><asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" /></li>
                                            <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>
                                            <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                                        </ul> 
                                        <uc:ConfirmBox runat="server" ID="cfbDeleteDetl" TargetControlID="lnkDeleteDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lnkExportDetl" />
                                    </Triggers>
                                    </asp:UpdatePanel> 
                                </div>                          
                            </div>                           
                            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdDetail" ClientInstanceName="grdDetail" runat="server" Width="100%" KeyFieldName="EvalTemplateDetlChoiceNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="CodeDetl" Caption="Detail No." />
                                <dx:GridViewDataTextColumn FieldName="EvalTemplateDetlChoiceDesc" Caption="Description" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="OrderBy" Caption="Order" />                                                                                        
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" SelectAllCheckboxMode="Page" />
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetail" />
                    </div>
                </div>                                                           
            </div>                   
                        </div>
                   </div>
             </div>                   
        </Content>
    </uc:Tab>

    <asp:Button ID="btnShowMain" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" /></ajaxtoolkit:ModalPopupExtender>
    <asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">                        
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                    </div>
                </div>                        
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Category :</label>
                    <div class="col-md-7">
                        <asp:DropDownList runat="server" ID="cboEvalTemplateCateNo" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Question :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtQuestion" CssClass="form-control required" TextMode="MultiLine" Rows="4" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Response Type :</label>
                    <div class="col-md-7">
                        <asp:DropDownList runat="server" ID="cboResponseTypeNo" CssClass="form-control" DataMember="EResponseType" />
                    </div>
                </div>   
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkHasComment" Text="&nbsp;Check to add comment box." />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsRequired" Text="&nbsp;Check to make require field." />
                    </div>
                </div>                            
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Order :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtOrderBy" CssClass="form-control number" />                        
                    </div>
                </div>
                <br />
            </div>                    
        </fieldset>
    </asp:Panel>


    <asp:Button ID="btnShowDetl" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl" CancelControlID="lnkCloseDetl" BackgroundCssClass="modalBackground" />
    <asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsDetail">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkCloseDetl" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveDetl" OnClick="lnkSaveDetl_Click" CssClass="fa fa-floppy-o submit fsDetail" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">                        
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Detail No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCodeDetl" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                    </div>
                </div>                        
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Description :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtEvalTemplateDetlChoiceDesc" CssClass="form-control required" />
                    </div>
                </div>                                     
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Order :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtOrderByDetl" CssClass="form-control number" />                        
                    </div>
                </div>
                <br />
            </div>                    
        </fieldset>
    </asp:Panel>

</asp:Content>

