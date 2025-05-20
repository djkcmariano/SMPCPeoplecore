<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpEmployeeStatList.aspx.vb" Inherits="Secured_EmpEmployeeStatList" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">                    
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control"  runat="server" />            
                    </div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>             
                        <li><asp:LinkButton runat="server" ID="lnkArchive" OnClick="lnkArchive_Click" Text="Archive" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false"/></li>
                                <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                            </ul>
                            <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkDelete" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?"  />
                            <uc:ConfirmBox runat="server" ID="cfbArchive" TargetControlID="lnkArchive" ConfirmMessage="Selected items will be archived. Proceed?"  />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkExport" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeStatNo">                                                                                   
                        <Columns>                            
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                            <dx:GridViewDataTextColumn FieldName="EmployeeStatCode" Caption="Code" />
                            <dx:GridViewDataTextColumn FieldName="EmployeeStatDesc" Caption="Description" />
                            <dx:GridViewDataComboBoxColumn FieldName="BIRCategoryStatDesc" Caption="BIR Category Status"/>
                            <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By" /> 
                            <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" /> 
                            <dx:GridViewDataTextColumn FieldName="ModifiedBy" Caption="Last Modified By" Visible="false"/> 
                            <dx:GridViewDataTextColumn FieldName="ModifiedDate" Caption="Last Modified Date" Visible="false"/> 
                            <dx:GridViewDataComboBoxColumn FieldName="PayLocDesc" Caption="Company" />
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" /> 
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />    
                        
                    </div> 
                         
                </div>
            </div>
       </div>
 </div>

<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopup"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup" style="display:none">
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
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEmployeeStatNo" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" ></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEmployeeStatCode"   CssClass="required form-control" runat="server" 
                        ></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEmployeeStatDesc" CssClass="required form-control" runat="server" ></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">BIR Category Status :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboBIRCategoryStatNo" DataMember="EBIRCategoryStat" CssClass="form-control" runat="server" ></asp:Dropdownlist>
                </div>
            </div>
            <div class="form-group" style="visibility:hidden; position:absolute;">
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
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>

      
</asp:Content>