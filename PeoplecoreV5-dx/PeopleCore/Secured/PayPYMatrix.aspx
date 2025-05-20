<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayPYMatrix.aspx.vb" Inherits="Secured_PayPYMatrix" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">     
<br />
<div class="page-content-wrap">         
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
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PYMatrixNo">                                                                                   
                        <Columns>
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("PYMatrixNo") %>' OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>                                
                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />                                                                         
                            <dx:GridViewDataTextColumn FieldName="PYCateDesc" Caption="Category" />
                            <dx:GridViewDataTextColumn FieldName="PYDistanceDesc" Caption="Distance"/>
                            <dx:GridViewDataTextColumn FieldName="PYTruckTypeDesc" Caption="Trucking Type" />
                            <dx:GridViewDataTextColumn FieldName="PYJobTypeDesc" Caption="Driver/Helper" />
                            <dx:GridViewDataTextColumn FieldName="Rate" Caption="Rate" />
                            <dx:GridViewDataTextColumn FieldName="UserName" Caption="Encoder" />
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
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPYMatrixNo" ReadOnly="true" runat="server" CssClass="form-control" placeholder="Autonumber" />
                </div>
            </div>
                                            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" placeholder="Autonumber" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Pakyaw Category :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPYCateNo" runat="server" DataMember="EPYCate" CssClass="form-control" />
                </div>
            </div>
           <div class="form-group">
                <label class="col-md-4 control-label has-space">Distance :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPYDistanceNo" runat="server" DataMember="EPYDistance" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Trucking Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPYTruckTypeNo" runat="server" DataMember="EPYTruckType" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Driver/Helper :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPYJobTypeNo" runat="server" DataMember="EPYJobType" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Rate :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtRate" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">
                Company Name :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                    </asp:Dropdownlist>
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