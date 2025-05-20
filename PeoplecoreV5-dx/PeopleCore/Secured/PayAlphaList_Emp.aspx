<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayAlphaList_Emp.aspx.vb" Inherits="Secured_PayAlphaList_Emp" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<style type="text/css">

.table th, .table td { 
     border-top: none !important; 
     border: 1px dotted gray;
     border-left:none;
 }
 
</style>

    <uc:Tab runat="server" ID="Tab">
        <Header>                       
            <asp:Label runat="server" ID="lbl" />            
        </Header>
        <Content>
            <div class="row">
                <div class="panel panel-default" style="margin-bottom:10px;">
                    <div class="table-responsive">
                        <table class="table table-condensed"> 
                            <tbody> 
                            <tr> 
                                <td style="width:15%;text-align:left;"><strong>Transaction No.</strong></td> 
                                <td style="width:35%;"><asp:label ID="lblCode" runat="server" class="col-md-8 control-label" /></td>
                                <td style="width:15%;text-align:left;"><strong>Company Name</strong></td> 
                                <td style="width:35%;"><asp:label ID="lblPayLocDesc" runat="server" class="col-md-8 control-label" /></td>
                            </tr> 
                            <tr> 
                                <td style="text-align:left;"><strong>Applicable Year</strong></td> 
                                <td ><asp:label ID="lblApplicableYear" runat="server" class="col-md-8 control-label" /></td>
                                <td style="text-align:left;"><strong>Applicable Month</strong></td> 
                                <td ><asp:label ID="lblApplicableMonth" runat="server" class="col-md-8 control-label" /></td>
                            </tr> 
                            <tr style="display:none;"> 
                                <td style="text-align:left;"><strong>Facility</strong></td> 
                                <td ><asp:label ID="lblFacilityDesc" runat="server" class="col-md-8 control-label" /></td>
                                <td style="text-align:left;"><strong></strong></td> 
                                <td ><asp:label ID="Label2" runat="server" class="col-md-8 control-label" /></td>
                            </tr> 
                            </tbody> 
                        </table> 
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">                                
                        <div class="col-md-10">                                           
             
                        </div>               
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
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" KeyFieldName="AlphaEmployeeNo" Width="100%">
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate >
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="FullName" Caption="Name of Employee" />
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="5%" />                                                             
                                    </Columns>
                                    <SettingsPager EllipsisMode="OutsideNumeric" NumericButtonCount="7" />
                                    <SettingsSearchPanel Visible="true" />                     
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                    
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>

            <asp:Button ID="btnShow" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShow" PopupControlID="Panel2" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />

            <asp:Panel id="Panel2" runat="server" CssClass="entryPopup" style="display:none">
                <fieldset class="form" id="fsMain">        
                    <div class="cf popupheader">
                        <h4>&nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="lnkClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                        <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />   
                    </div>         
                    <div  class="entryPopupDetl form-horizontal">
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:HiddenField runat="server" ID="hifAlphaEmployeeNo"/>
                                <asp:Textbox ID="txtCode" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                            </div>
                        </div> 

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Name of Employee :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                                <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                                <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                                TargetControlID="txtFullName" MinimumPrefixLength="2" CompletionSetCount="0" 
                                CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                CompletionListCssClass="autocomplete_completionListElement" 
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true"  ServicePath="~/asmx/WebService.asmx" />
                                 <script type="text/javascript">
                                     function getRecord(source, eventArgs) {
                                         document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                                     }
                                     </script>
                           </div>
                        </div> 

                        
                        <br />
                    </div>                
                </fieldset>
            </asp:Panel>
        </Content>
    </uc:Tab>


</asp:Content> 
