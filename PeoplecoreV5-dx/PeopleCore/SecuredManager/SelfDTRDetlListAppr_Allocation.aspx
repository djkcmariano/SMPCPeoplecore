<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfDTRDetlListAppr_Allocation.aspx.vb" Inherits="SecuredManager_SelfDTRDetlListAppr_Allocation" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<script type="text/javascript">
    function cbCheckAll_CheckedChanged(s, e) {
        grdMain.PerformCallback(s.GetChecked().toString());
    }
</script>


<div class="page-content-wrap">   

<uc:DTRDetailHeader runat="server" ID="DTRDetailHeader" />     
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading"> 
                <div class="col-md-2">
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DTRDetiLogAllocNo" OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." HeaderStyle-Wrap="true" />
                                <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Department" HeaderStyle-Wrap="true" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="CostCenterDesc" Caption="Cost Center" HeaderStyle-Wrap="true" Visible="false" /> 
                                <dx:GridViewDataTextColumn FieldName="ProjectDesc" Caption="Project" HeaderStyle-Wrap="true" Visible="true" /> 
                                <dx:GridViewDataTextColumn FieldName="Hrs" Caption="Hours Worked" HeaderStyle-Wrap="true" />
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Task" HeaderStyle-Wrap="true"  PropertiesTextEdit-EncodeHtml="false"/>
                                <dx:GridViewDataTextColumn FieldName="ApprovalStatDesc" Caption="Approval Status" HeaderStyle-Wrap="true" />
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
                                    <HeaderTemplate>
                                        <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                        </dx:ASPxCheckBox>
                                    </HeaderTemplate>
                                </dx:GridViewCommandColumn>                                 
                            </Columns>                     
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>
</div>

<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopup"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup" style="display:none;">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4></h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtDTRDetiLogAllocNo" CssClass="form-control" runat="server" 
                        ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtCode"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"   Placeholder="Autonumber"></asp:TextBox>
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Department :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboDepartmentNo" DataMember="EDepartment" CssClass="form-control" runat="server" 
                        ></asp:Dropdownlist>
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Cost Center :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboCostCenterNo" DataMember="ECostCenter" CssClass="form-control" runat="server" 
                        ></asp:Dropdownlist>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Project :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboProjectNo" DataMember="EProject" CssClass="form-control" runat="server" 
                        ></asp:Dropdownlist>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Hours Work :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtHrs" SkinID="txtdate" runat="server" CssClass="form-control required"
                        ></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Task :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtRemarks" TextMode="MultiLine" Rows="3"  CssClass="form-control required" runat="server" 
                        ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <br />
            </div>

        </div>
        
         </fieldset>
</asp:Panel>
 

</asp:Content>

