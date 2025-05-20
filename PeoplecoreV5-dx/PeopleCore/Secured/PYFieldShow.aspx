<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PYFieldShow.aspx.vb" Inherits="Secured_PYFieldShow" %>



<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-4">
                        <asp:Dropdownlist ID="cboPYActivityTypeNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" style="width:200px;" CssClass="form-control" runat="server" ></asp:Dropdownlist>
                    </div>
                    <div>
                        <%--<asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>--%>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkPost"  Text="Update" OnClick="lnkUpdate_Click" CssClass="control-primary"/></li>
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                                </ul>
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                             <%--   
                            </ContentTemplate>
                            <Triggers>
                                 <asp:PostBackTrigger ControlID="lnkPost" />
                                 <asp:AsyncPostBackTrigger ControlID="lnkPost" EventName="Click"
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </div>                           
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="ColId">                                                                                   
                                <Columns>                            
                                     <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit Name" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("ColId") %>' OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="ColId" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="FieldType" Caption="Field Type" />
                                    <dx:GridViewDataTextColumn FieldName="ColumnDesc" Caption="Column Name" />
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Name="av" Caption="Available Column" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                           <asp:CheckBox runat="server" ID="txtIsAvailable" Checked='<%# Bind("IsWithField") %>' ToolTip='<%# Bind("ColId") %>' />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select to Delete" /> 
                                </Columns>                            
                            </dx:ASPxGridView>
                            
                        </div>
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
                <asp:TextBox ID="txtColId" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
        </div>

        <div class="form-group">
            <label class="col-md-4 control-label has-space">Column Name :</label>
            <div class="col-md-7">
                <asp:TextBox ID="txtColumnDesc" runat="server" CssClass="form-control" />                   
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-4 control-label has-space">Response Type :</label>
            <div class="col-md-7">
                <asp:Dropdownlist ID="cboResponseTypeNo" runat="server" CssClass="form-control" />                   
            </div>
        </div>
        
                           
    </div>
    <br />
    </fieldset>
</asp:Panel>

 </asp:Content>