<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="PayBankDiskList.aspx.vb" Inherits="Secured_PayBankDiskList" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<script type="text/javascript">
   
    function grid_ContextMenu(s, e) {
        if (e.objectType == "row")
            hiddenfield.Set('VisibleIndex', parseInt(e.index));
        pmRowMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
    }

</script>

<div class="row">
    <div class="panel panel-default">
        <div class="panel-heading">                                
            <div class="col-md-2">
                <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
            </div>                
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>                    
                    <ul class="panel-controls">                        
                        
                        <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkProcess" OnClick="lnkProcess_Click" Text="Process" CssClass="control-primary" /></li>   
                        <li><asp:LinkButton runat="server" ID="lnkBankRef" OnClick="lnkBankRef_Click" Text="Bank Reference" CssClass="control-primary" /></li>                     
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                    </ul>
                    <uc:ConfirmBox runat="server" ID="cfbPost" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be modified, Proceed?" MessageType="Post"  />
                    <uc:ConfirmBox runat="server" ID="cfblnkProcess" TargetControlID="lnkProcess" ConfirmMessage="Do you want to proceed?" MessageType="Process"  />                    
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkExport" />
                </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayNo"
                            OnFillContextMenuItems="MyGridView_FillContextMenuItems">                                                                                   
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="PayCode" Caption="Payroll No." />
                                <dx:GridViewDataTextColumn FieldName="DTRCode" Caption="DTR No." Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="PayCateDesc" Caption="Category" />
                                <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group" />
                                <dx:GridViewDataTextColumn FieldName="PayDate" Caption="Pay Date" />
                                <dx:GridViewDataTextColumn FieldName="PayStartDate" Caption="Start Date" />
                                <dx:GridViewDataTextColumn FieldName="PayEndDate" Caption="End Date" />
                                <dx:GridViewDataTextColumn FieldName="PayTypeDesc" Caption="Payroll Type" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="PayPeriod" Caption="Period" Width="5%" CellStyle-HorizontalAlign="Center" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="MonthDesc" Caption="Month" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Year" Visible="false" />
                                <dx:GridViewDataCheckColumn FieldName="Isspecialpay" Caption="Special<br />Pay"  HeaderStyle-HorizontalAlign="Center" Width="5%" Visible="false"/>
                                <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" PropertiesTextEdit-EncodeHtml="false" />
                                <dx:GridViewDataTextColumn FieldName="BankLastDateProcess" Caption="Date Processed" Visible="false"/>
                                <dx:GridViewDataColumn Caption="Summary"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkSummary" CssClass="fa fa-external-link control-primary" Font-Size="Medium" OnClick="lnkSummary_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <%--<dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Process" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkProcess_Detail" CssClass='<%# Bind("Icon") %>' Enabled='<%# Bind("IsEnabled") %>' OnClick="lnkProcess_Detail_Click" />
                                        <uc:ConfirmBox runat="server" ID="cfProcess_Detail" TargetControlID="lnkProcess_Detail" ConfirmMessage='<%# Bind("ConfirmMessage") %>' Visible='<%# Bind("IsEnabled") %>'  /> 
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>--%>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="5%" />     
                            </Columns>    
                            <ClientSideEvents ContextMenu="grid_ContextMenu" />                   
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />

                        <dx:ASPxPopupMenu ID="pmRowMenu" runat="server" ClientInstanceName="pmRowMenu">
                            <Items>
                                <dx:MenuItem Text="Report" Name="Name">
                                    <Template>
                                        <asp:LinkButton runat="server" ID="lnkPrint" OnClick="lnkPrint_Click" CssClass="control-primary" Font-Size="small" OnPreRender="lnkPrint_PreRender" Text="Payroll Register Report" Visible="false" /><br />
                                    </Template>
                                </dx:MenuItem>
                            </Items>
                            <ItemStyle Width="250px"></ItemStyle>
                        </dx:ASPxPopupMenu>
                        <dx:ASPxHiddenField ID="hf" runat="server" ClientInstanceName="hiddenfield" />  
                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>

<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShow" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">        
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />   
            </div>         
            <div  class="entryPopupDetl form-horizontal">            
                <div class="form-group">
                    <label class="col-md-4 control-label">Payroll No. :</label>
                    <div class="col-md-6">
                        <asp:HiddenField runat="server" ID="hifPayNo" />
                        <asp:Textbox ID="txtPayCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                    </div>
                </div> 
            </div>
            <br />          
    </fieldset>
</asp:Panel>   
</asp:Content>
