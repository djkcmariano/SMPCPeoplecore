<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayAutoJVList.aspx.vb" Inherits="Secured_PayAutoJVList" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

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
                                    <li><asp:LinkButton runat="server" ID="lnkCreateDisk" OnClick="lnkCreateDisk_Click" Text="Send to disk" CssClass="control-primary" Visible="false" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkProcess" OnClick="lnkProcess_Click" Text="Process" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                </ul>
                                <uc:ConfirmBox runat="server" ID="cfbPost" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be modified, Proceed?" MessageType="Post"  />
                                <uc:ConfirmBox runat="server" ID="cfblnkProcess" TargetControlID="lnkProcess" ConfirmMessage="Do you want to proceed?" MessageType="Process"  /> 
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
                      
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayAutoJVNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="PayCode" Caption="Payroll No." />
                                    <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group"  />
                                    <dx:GridViewDataTextColumn FieldName="DateFrom" Caption="Start Date" />
                                    <dx:GridViewDataTextColumn FieldName="DateTo" Caption="End Date" />
                                    <dx:GridViewDataTextColumn FieldName="TotalCredit" Caption="Total Credit" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="TotalDebit" Caption="Total Debit" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="TotalGross" Caption="Total Gross" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" />
                                    <dx:GridViewDataTextColumn FieldName="DatePosted" Caption="Posted Date" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="PostedBy" Caption="Posted By" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="DateProcessed" Caption="Processed Date"/>
                                    <dx:GridViewDataTextColumn FieldName="ProcessedBy" Caption="Processed By" Visible="false" />
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Detail" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <%--<dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Process" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkProcess_Detail" CssClass='<%# Bind("Icon") %>' Enabled='<%# Bind("IsEnabled") %>' OnClick="lnkProcess_Detail_Click" />
                                            <uc:ConfirmBox runat="server" ID="cfProcess_Detail" TargetControlID="lnkProcess_Detail" ConfirmMessage='<%# Bind("ConfirmMessage") %>' Visible='<%# Bind("IsEnabled") %>'  /> 
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>--%>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Create Disk" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDiskDetl" CssClass="fa fa-floppy-o" OnClick="lnkCreateDiskD_Click" />
                                       
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>    
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


  <div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                        <div class="col-md-4 panel-title">
                            Transaction No. :&nbsp;&nbsp;<asp:Label ID="lblDetl" runat="server"></asp:Label>
                        </div>
                        <div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                <ContentTemplate>
                                    <ul class="panel-controls">
                                        <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                                    </ul>
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
                            <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="PayAutoJVDetiNo" Width="100%">                                                                                   
                                <Columns>                     
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                    <dx:GridViewDataTextColumn FieldName="AccountCode" Caption="Account Code" />
                                    <dx:GridViewDataTextColumn FieldName="AccountName" Caption="Account Name" />
                                    <dx:GridViewDataTextColumn FieldName="Credit" Caption="Credit" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="Debit" Caption="Debit" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                </Columns>   
                                <SettingsContextMenu Enabled="true">                                
                                    <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                                </SettingsContextMenu>                                                                                            
                                <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="True" /> 
                                <SettingsSearchPanel Visible="true" />                          
                            </dx:ASPxGridView>

                            <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />    
                        </div>
                    </div>
                </div>
                   
            </div>
       </div>
 </div>    


<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click" />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayAutoJVNo" runat="server" CssClass="form-control" ReadOnly="true" ></asp:Textbox>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" ></asp:Textbox>
                </div>
            </div> 
            
            <div class="form-group" >
                <label class="col-md-4 control-label has-required">Payroll No. :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayNo" runat="server" CssClass="form-control required"></asp:Dropdownlist>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDescription" TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control" ></asp:Textbox>
                </div>
            </div>
            <br />
            </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>

<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="Fieldset1">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="Linkbutton1" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="LinkButton2" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  Visible="false" />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="Textbox1" runat="server" CssClass="form-control" ReadOnly="true" ></asp:Textbox>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtId" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" ></asp:Textbox>
                </div>
            </div> 
            
            <div class="form-group" >
                <label class="col-md-4 control-label has-required">Journal Category :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboJVDefCate" runat="server" DataMember="EJVDefCate" CssClass="form-control required"></asp:Dropdownlist>
                </div>
            </div> 
            
            <br />
            <div class="form-group" >
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-7">
                    <asp:LinkButton runat="server" ID="LinkButton3" OnClick="lnkSendDisk_Click" OnPreRender="lnkOpenFile_PreRender" Text="Send to disk" CssClass="control-primary" />
                </div>
            </div> 
            <br />
            <br />

            </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>


</asp:Content>
