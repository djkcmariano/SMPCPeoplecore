<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="PayLastEntitledList_Loan.aspx.vb" Inherits="Secured_PayLastEntitledList_Loan" %>
<%@ Register Src="~/Include/HeaderInfo.ascx" TagName="HeaderInfo" TagPrefix="uc" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<uc:HeaderInfo runat="server" ID="HeaderInfo1" />

<uc:Tab runat="server" ID="Tab">
    <Header>        
            <asp:Label runat="server" ID="lbl" /> 
            <div style="display:none;">
                <asp:CheckBox ID="txtIsPosted" runat="server"></asp:CheckBox>
            </div>      
    </Header>
     <Content>
        <br />    
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">                                
                    <div class="col-md-10">                                           
                        <div class="form-group">

                        </div>                
                    </div>               
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
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdDetl" runat="server" KeyFieldName="PayLastEntitledLoanNo" Width="100%">
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="PayDeductTypeDesc" Caption="Loan Type" />
                                    <dx:GridViewDataTextColumn FieldName="opt" Caption="Option" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="5%" />                                                             
                                </Columns>
                                <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="true"  />       
                            </dx:ASPxGridView>       
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />               
                        </div>
                    </div>                                                           
                </div>                   
            </div>
        </div>
    </div>
    <asp:Button ID="btnShow" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="Panel2" CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
    </ajaxtoolkit:ModalPopupExtender>

    <asp:Panel id="Panel2" runat="server" CssClass="entryPopup" style="display:none">
            <fieldset class="form" id="fsMain">
            <!-- Header here -->
                <div class="cf popupheader">
                    <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                    <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />   
                </div>
                <!-- Body here -->
                <div  class="entryPopupDetl form-horizontal">
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtPayLastEntitledLoanNo" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                    </div>
                </div> 

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtCode" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                    </div>
                </div> 
                        
                       
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Loan Type :</label>
                    <div class="col-md-6">
                        <asp:Dropdownlist ID="cboPayDeductTypeNo"  runat="server" CssClass="form-control required" ></asp:Dropdownlist>
                    </div>
                </div> 
                        
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Options :</label>
                    <div class="col-md-6">
                        <asp:Radiobutton ID="txtIsDeductInFull" runat="server" GroupName="opt" Text="&nbsp; Deduct in Full "></asp:Radiobutton><br />
                        <asp:Radiobutton ID="txtIsSuspend" runat="server" GroupName="opt" Text="&nbsp; Suspend"></asp:Radiobutton>
                       
                    </div>
                </div> 
                        
           
                <br />
                </div>
                <!-- Footer here -->
         
                </fieldset>
    </asp:Panel>
</Content>
</uc:Tab>

</asp:Content> 