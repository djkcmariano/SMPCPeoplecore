<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="TrnCurriculumList.aspx.vb" Inherits="Secured_TrnCurriculumList" EnableEventValidation="false" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboTab_Click" ></asp:Dropdownlist>
                    </div>
                    <div >
                        
                    </div>                           
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeNo">                                                                                   
                                <Columns>
                                                             
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />                                                                           
                                    <dx:GridViewDataTextColumn FieldName="PositionDesc" Caption="Position" /> 
                                    <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Department" /> 
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Copy" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkCopy" CssClass="fa fa-copy" Font-Size="Medium" CommandArgument='<%# Bind("EmployeeNo") %>' OnClick="lnkCopy_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>  
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>                                                                                     
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Visible="false" />
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
                <h4 class="panel-title">Required Trainings</h4>
                <asp:UpdatePanel runat="server" ID="UpdatePanel2x">
                    <ContentTemplate>                    
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkAddR" OnClick="btnAddR_Click" Text="Add" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkDeleteR" OnClick="btnDeleteR_Click" Text="Delete" CssClass="control-primary" /></li>
                        </ul> 
                        <uc:ConfirmBox runat="server" ID="ConfirmBox" TargetControlID="lnkDeleteR" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
                    </ContentTemplate>
                 </asp:UpdatePanel> 
                               
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive-vertical">
                        <div class="table-responsive">
                            <dx:ASPxGridView ID="grdTrn" ClientInstanceName="grdTrn" runat="server" Width="100%" KeyFieldName="TrnCurriculumDetlNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditR_Click"  CommandArgument='<%# Bind("TrnCurriculumDetlNo") %>'/>
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>                            
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                        <dx:GridViewDataTextColumn FieldName="TrnTitleDesc" Caption="Training Title" />                                                                           
                                                                                                                             
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                    </Columns>                            
                                </dx:ASPxGridView>
                        </div>
                    </div>
                        
                </div>
            </div>     
        </div>

 </div>
 </div>

<asp:Button ID="btntrn" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdtrn" runat="server" TargetControlID="btntrn" PopupControlID="pnltrn" 
CancelControlID="lnkclosetrn" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnltrn" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="Fieldset2">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkclosetrn" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveTrn" OnClick="btnSaveTrn_Click" CssClass="fa fa-floppy-o submit fsMain lnkSaveTrn" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtTrnCurriculumDetlNo" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtTrnCurriculumDetlCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Training Title :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboTrnTitleNo" DataMember="ETrnTitle" runat="server" CssClass="form-control required"></asp:DropdownList>
                </div>
            </div>

            <br />
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>
 
<asp:Button ID="btnShowCopy" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlCopy" runat="server" TargetControlID="btnShowCopy" PopupControlID="pnlPopupCopy" CancelControlID="lnkCloseCopy" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupCopy" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="Fieldset1">
         <div class="cf popupheader">
              <h4>Copy Training(s)</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseCopy" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveCopy" OnClick="lnkSaveCopy_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">

            <%--<div class="form-group">
                <label class="col-md-4 control-label has-space">To Employee :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboEmployeeToNo" runat="server" CssClass="form-control" Enabled="false"></asp:DropdownList>
                </div>
            </div>--%>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">From Employee :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboEmployeeFromNo" runat="server" CssClass="form-control"></asp:DropdownList>
                </div>
            </div>
       
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>          

</asp:content>
