<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BSProjectList_ClassRateDetl.aspx.vb" Inherits="Secured_BSProjectList_ClassRateDetl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<uc:Tab runat="server" ID="Tab" HeaderVisible="true">
<Content>
<br />
<div class="page-content-wrap">    
    <div class="row">
    
        <div class="panel panel-default" style="margin-bottom:10px;">
            <div class="table-responsive">
                <table class="table table-condensed"> 
                    <tbody> 
                    <tr> 
                        <td style="width:15%;text-align:left;"><strong>Project</strong></td> 
                        <td style="width:35%;"><asp:label ID="lblProjectDesc" runat="server" class="col-md-12 control-label" /></td>
                        <td style="width:15%;text-align:left;"><strong>Billing/Payroll Group</strong></td> 
                        <td style="width:35%;"><asp:label ID="lblPayClassDesc" runat="server" class="col-md-12 control-label" /></td>
                    </tr> 
                    <tr> 
                        <td style="text-align:left;"><strong>Position</strong></td> 
                        <td ><asp:label ID="lblPositionDesc" runat="server" class="col-md-12 control-label" /></td>
                        <td style="text-align:left;"><strong>Department</strong></td> 
                        <td ><asp:label ID="lblDepartmentDesc" runat="server" class="col-md-12 control-label" /></td>
                    </tr> 
                    
                    </tbody> 
                </table> 
            </div>
        </div>
    </div>     
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <h3>&nbsp;</h3>
                </div>
                <div>
                    &nbsp;                  
                </div> 
                <div>                                                
                    <ul class="panel-controls"> 
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                        <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                    </ul>                                                                                                                                                     
                </div>
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BSProjectClassRateDetiNo">                                                                                   
                        <Columns>    
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("BSProjectClassRateDetiNo") %>' OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                                                    
                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                            <dx:GridViewDataComboBoxColumn FieldName="BSCompTypeDesc" Caption="Component Type"/>
                            <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />                                                        
                            <dx:GridViewDataTextColumn FieldName="Rate" Caption="Rate" />   
                            <dx:GridViewDataTextColumn FieldName="OTRate" Caption="Computation for OT Rate" />   
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />                      
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                        
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
                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>
            </div>
                
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Component Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboBSCompTypeNo" runat="server" CssClass="form-control" DataMember="BBSCompType" />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDescription" runat="server" CssClass="form-control" />                    
                </div>
            </div>

            <div class="form-group" >
                <label class="col-md-4 control-label has-space">Rate :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtRate" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtRate" />
                </div>
            </div>
            <div class="form-group" >
                <label class="col-md-4 control-label has-space">Computation for OT Rate :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtOTRate" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtOTRate" />
                </div>
            </div>
                           
        </div>
        <br />
        </fieldset>
    </asp:Panel>



</Content>
</uc:Tab>
<br /><br />
</asp:Content>