<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayMainList_EditOrg.aspx.vb" Inherits="Secured_PayMainList_EditOrg" %>

<asp:Content id="Content2" contentplaceholderid="cphBody" runat="server">


<div class="page-content-wrap">     
    <uc:PayHeader runat="server" ID="PayHeader" />    
    <uc:FormTab runat="server" ID="FormTab" />
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                         
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                        
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                </ul>
                                        
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayMainNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                    <dx:GridViewDataComboBoxColumn FieldName="CostCenterDesc" Caption="Cost Center" />
                                    <dx:GridViewDataTextColumn FieldName="SectionDesc" Caption="Section" />
                                    <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Department" />
                                    
                                </Columns>                            
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />    
                        </div>
                    </div>
                </div>
                   
            </div>
        </div>
    </div>

 <asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupMain"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="lnkSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayMainNo" runat="server" CssClass="form-control" ReadOnly="true" ></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">Employee No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEmployeeCode" runat="server" CssClass="form-control" ReadOnly="true"  ></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">Employee Name :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtFullName" runat="server" CssClass="form-control" ReadOnly="true"  ></asp:Textbox>
                </div>
            </div> 
      
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Cost Center :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboCostCenterNo"  runat="server" DataMember="ECostCenter" CssClass="required form-control" />
               </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Department :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboDepartmentNo"  runat="server" DataMember="EDepartment" CssClass="required form-control" />
               </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Section :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboSectionNo"  runat="server" DataMember="ESection" CssClass="required form-control" />
               </div>
            </div> 
             
            <br />
            </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>
 
</asp:Content>


