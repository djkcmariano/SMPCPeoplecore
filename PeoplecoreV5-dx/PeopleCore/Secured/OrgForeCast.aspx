<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="OrgForeCast.aspx.vb" Inherits="Secured_OrgForeCast" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">     
<br />
<div class="page-content-wrap">         
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
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="ForeCastNo">                                                                                   
                        <Columns>
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("ForeCastNo") %>' OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>                                
                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />                                                                         
                            <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position"/>
                            <dx:GridViewDataComboBoxColumn FieldName="ForeCastTypeDesc" Caption="Manpower Forecast"/>
                            <dx:GridViewDataTextColumn FieldName="HeadCount" Caption="Head Count" />
                            <dx:GridViewDataComboBoxColumn FieldName="MonthDesc" Caption="Month"/>
                            <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Year" />
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
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" placeholder="Autonumber" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Position :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPositionNo" runat="server" DataMember="EPosition" CssClass="form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Forecast Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboForeCastTypeNo" runat="server" DataMember="EForeCastType" CssClass="form-control required" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Head Count :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtHeadCount" runat="server" CssClass="form-control required" SkinID="txtnumber" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Month :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboMonthNo" runat="server" DataMember="EMonth" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Year :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtApplicableYear" runat="server" CssClass="form-control" SkinID="txtnumber" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                </div>
            </div>
        </div>                    
    </fieldset>
</asp:Panel>

</asp:Content>
