<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterPage.master" Theme="PCoreStyle" CodeFile="DTR_Employee.aspx.vb" Inherits="Secured_DTR_Employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<script type="text/javascript">
    function cbCheckAll_CheckedChanged(s, e) {
        grdMain.PerformCallback(s.GetChecked().toString());
    }
</script>


<div class="page-content-wrap">   

<uc:DTRHeader runat="server" ID="DTRHeader" />
      
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">                                
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DTREmployeeNo"
                        OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />                                                                           
                                
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
                <label class="col-md-3 control-label">Transaction No. :</label>
                <div class="col-md-8">
                    <asp:Textbox ID="txtDTREmployeeNo" CssClass="form-control" runat="server" 
                        ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3 control-label has-space">Transaction No. :</label>
                <div class="col-md-8">
                    <asp:TextBox ID="txtCode"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"   Placeholder="Autonumber"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-required">Name of Employee :</label>
                <div class="col-md-8">
                     <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;"  Placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="cboEmployee" CompletionSetCount="0"
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" />
                     <script type="text/javascript">
                         function getRecord(source, eventArgs) {
                             document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                         }
                            </script>
                </div>
            </div>
            
            

            <div class="form-group">
                <br />
                <br />
                <br />
                <br />
                <br />
            </div>

        </div>
        
         </fieldset>
</asp:Panel>


</asp:UpdatePanel>   

</asp:Content>


