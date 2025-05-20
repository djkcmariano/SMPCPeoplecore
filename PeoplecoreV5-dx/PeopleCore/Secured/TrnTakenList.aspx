<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="TrnTakenList.aspx.vb" Inherits="Secured_TrnTakenList" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">


     <script type="text/javascript">

         function grid_ContextMenu(s, e) {
             if (e.objectType == "row")
                 hiddenfield.Set('VisibleIndex', parseInt(e.index));
             pmRowMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
         }

         function OnContextMenuItemClick(sender, args) {
             if (args.item.name == "Refresh") {
                 args.processOnServer = true;
                 args.usePostBack = true;
             }
         }

    </script>


<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" ></asp:Dropdownlist>
                    </div>
                    <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>                    
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkEnrollment" OnClick="lnkEnrollment_Click" Text="Enrollment" CssClass="control-primary" /></li>
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="TrnTakenNo"
                        OnFillContextMenuItems="MyGridView_FillContextMenuItems" OnContextMenuItemVisibility="Grid_ContextMenuItemVisibility"
                        OnContextMenuItemClick="Grid_ContextMenuItemClick">                                                                                   
                            <Columns> 
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("TrnTakenNo") %>' OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                           
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Training No." />
                                <dx:GridViewDataTextColumn FieldName="TrnTitleDesc" Caption="Training Title" />  
                                <dx:GridViewDataDateColumn FieldName="StartDate" Caption="Start Date" /> 
                                <dx:GridViewDataDateColumn FieldName="EndDate" Caption="End Date" /> 
                                <dx:GridViewBandColumn Caption="Enrollment Period" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                    <Columns>
                                        <dx:GridViewDataDateColumn FieldName="EnrollDateOpen" Caption="Open"/>
                                        <dx:GridViewDataDateColumn FieldName="EnrollDateClosed" Caption="Closed"/>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="Enrolled" Caption="Enrolled" CellStyle-HorizontalAlign="Right"/>
                                <dx:GridViewDataTextColumn FieldName="Hrs" Caption="Hour(s)" Visible="false" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="TrnProviderDesc" Caption="Facilitator" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="TrnVenueDesc" Caption="Venue" Visible="false" />   
                                <dx:GridViewDataComboBoxColumn FieldName="TrnEnrollTypeDesc" Caption="Enrollment Type" Visible="false" /> 
                                <dx:GridViewDataComboBoxColumn FieldName="TrnTypeDesc" Caption="Training Type" Visible="false"/>
                                <dx:GridViewDataComboBoxColumn FieldName="TrnCategoryDesc" Caption="Training Category" Visible="false"/> 
                                <dx:GridViewDataComboBoxColumn FieldName="TrnStatDesc" Caption="Status" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="Reason" Caption="Reason" Visible="false" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Participants" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkView" CssClass="fa fa-external-link" Font-Size="Medium" OnClick="lnkView_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                                                                                    
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                            </Columns>   
                            <ClientSideEvents ContextMenuItemClick="function(s,e) { OnContextMenuItemClick(s, e); }" />
                            <ClientSideEvents ContextMenu="grid_ContextMenu" />                         
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />


                        <dx:ASPxPopupMenu ID="pmRowMenu" runat="server" ClientInstanceName="pmRowMenu">
                                <Items>
                                    <dx:MenuItem Text="Report" Name="Name">
                                        <Template>
                                        </Template>
                                    </dx:MenuItem>
                                </Items>
                                <ItemStyle Width="270px"></ItemStyle>
                       </dx:ASPxPopupMenu>
                       <dx:ASPxHiddenField ID="hf" runat="server" ClientInstanceName="hiddenfield" />

                    </div>
                </div>                                                           
            </div>
                   
            </div>
       </div>
 </div>

<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdl" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopup" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup" style="display:none" >
    <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
              <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtCode" CssClass="form-control" Enabled="false" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Reason of Cancellation/Postponed :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtReason" TextMode="MultiLine" Rows="4" CssClass="form-control required" />
                </div>
            </div>
       
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>
 
</asp:content>
