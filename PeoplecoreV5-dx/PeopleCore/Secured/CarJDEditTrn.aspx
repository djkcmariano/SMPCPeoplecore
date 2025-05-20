<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="CarJDEditTrn.aspx.vb" Inherits="Secured_CarJDEditTrn" Theme="PCoreStyle" %>

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
                                <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="lnkSearch_Click" />
                            </div>
                            <div>                                                
                                <ul class="panel-controls">                                    
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false" /></li>                                                        
                                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                </ul>                                                                                                                                                     
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="JDTrnNo">                                                                                   
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("JDTrnNo") %>' OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                            <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />
                                            <dx:GridViewDataTextColumn FieldName="TrnTitleDesc" Caption="Training Title" />
                                            <dx:GridViewDataTextColumn FieldName="IsQS" Caption="CSC QS" />
                                            <%--<dx:GridViewDataTextColumn FieldName="Hrs" Caption="No. of Hours" />--%>
                                            <%--<dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" HeaderStyle-HorizontalAlign="Center" SelectAllCheckboxMode="Page" />--%>
                                        </Columns>                            
                                    </dx:ASPxGridView>                                
                                </div>
                            </div>                                                           
                        </div>                   
                    </div>
                </div>
            </div>           
        </Content>
    </uc:Tab>
    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
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
                    <label class="col-md-4 control-label has-required">Description :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Training Title :</label>
                    <div class="col-md-7"> 
                        <asp:Dropdownlist ID="cboTrnTitleNo" DataMember="ETrnTitle" runat="server" CssClass="form-control required" AutoPostBack="true" OnSelectedIndexChanged="cboTrnTitleNo_SelectedIndexChanged"  />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">No. of Hours :</label>
                    <div class="col-md-3">                        
                        <asp:TextBox ID="txtNoOfHours" runat="server" CssClass="form-control" MaxLength="5" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtNoOfHours" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Effective Date :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtEffectiveDate" runat="server" CssClass="form-control" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtEffectiveDate" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender2" TargetControlID="txtEffectiveDate" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator3" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtEffectiveDate" Display="Dynamic" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox ID="chkIsQS" runat="server" Text="&nbsp;Tick if for Qualification Standard" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                    </div>
                </div>
                <br />
            </div>                    
        </fieldset>
    </asp:Panel>
</asp:Content>

