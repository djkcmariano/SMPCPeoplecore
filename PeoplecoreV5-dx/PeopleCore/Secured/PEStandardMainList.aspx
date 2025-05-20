<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="PEStandardMainList.aspx.vb" Inherits="Secured_PEStandardMainList" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">



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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PEStandardMainNo">                                                                                   
                            <Columns> 
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("PEStandardMainNo") %>' OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                           
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Template No." />
                                <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Series Year" CellStyle-HorizontalAlign="Left" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="PETemplateDesc" Caption="Template Header" /> 
                                <dx:GridViewDataTextColumn FieldName="DateCreated" Caption="Date Created" /> 
                                <dx:GridViewDataCheckColumn FieldName="IsArchived" Caption="Archived" HeaderStyle-HorizontalAlign="Center" Visible="false" />
                                <dx:GridViewDataColumn Caption="View Content"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="10%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkTemplate" CssClass="fa fa-external-link" Font-Size="Medium" OnClick="lnkTemplate_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Copy Template" HeaderStyle-HorizontalAlign="Center" Width="10%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkCopy" CssClass="fa fa-copy" Font-Size="Medium" OnClick="lnkCopy_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                                                          
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="10%" />
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                    </div>
                </div>                                                           
            </div>
                   
            </div>
       </div>
 </div>


<asp:Button ID="btnShowCopy" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlCopy" runat="server" TargetControlID="btnShowCopy" PopupControlID="pnlPopupCopy" CancelControlID="lnkCloseCopy" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupCopy" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
              <asp:Linkbutton runat="server" ID="lnkCloseCopy" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveCopy" OnClick="lnkSaveCopy_Click" CssClass="fa fa-floppy-o submit fsMain lnkSaveCopy" ToolTip="Save" />
         </div>
         <div  class="entryPopupDetl form-horizontal">

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Copy From :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboPEStandardMainFromNo" runat="server" CssClass="form-control required"></asp:DropdownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Copy To :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboPEStandardMainToNo" runat="server" CssClass="form-control" Enabled="false"></asp:DropdownList>
                </div>
            </div>
       
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>

</asp:content>
