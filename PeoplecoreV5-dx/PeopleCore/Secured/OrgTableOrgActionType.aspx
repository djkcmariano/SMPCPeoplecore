<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="OrgTableOrgActionType.aspx.vb" Inherits="Secured_OrgTableOrgActionType" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<div class="page-content-wrap">
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">                    
                    &nbsp;
                </div>
                <div>                                     
                    <ul class="panel-controls">                                                                            
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                    </ul>                    
                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                            
                </div>                                                                                                   
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="TableOrgActionTypeNo" >                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("TableOrgActionTypeNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." Width="20%" />
                                <dx:GridViewDataTextColumn FieldName="TableOrgActionTypeCode" Caption="Code" />                                                                
                                <dx:GridViewDataTextColumn FieldName="TableOrgActionTypeDesc" Caption="Description" />                                                                
                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" Width="5%" />                                                                
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                        
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>             
 </div>
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
                    <label class="col-md-4 control-label has-space">Reference No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>                        
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Code :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtTableOrgActionTypeCode" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Description :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtTableOrgActionTypeDesc" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Remarks :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" Rows="4" TextMode="MultiLine" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Action :</label>
                    <div class="col-md-7">
                        <div class="row">
                            <asp:RadioButton runat="server" ID="chkIsNew" Text="&nbsp;New" GroupName="Action" Checked="true" /><br />
                            <asp:RadioButton runat="server" ID="chkIsEdit" Text="&nbsp;Modify" GroupName="Action" /><br />
                            <asp:RadioButton runat="server" ID="chkIsDelete" Text="&nbsp;Delete" GroupName="Action" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Event :</label>
                    <div class="col-md-7">
                        <div class="row">
                            <asp:Checkbox runat="server" ID="chkIsEffectiveDate" Text="&nbsp;Tick if base on effective date" /><br />
                            <asp:Checkbox runat="server" ID="chkIsVacated" Text="&nbsp;Tick if upon vacancy" /><br />                            
                        </div>
                    </div>
                </div>                                             
                <br />
            </div>                    
        </fieldset>
    </asp:Panel>            
</asp:Content>

