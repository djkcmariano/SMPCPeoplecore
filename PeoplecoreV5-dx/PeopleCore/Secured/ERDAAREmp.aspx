<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="ERDAAREmp.aspx.vb" Inherits="Secured_ERDAAREmp" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<uc:Tab runat="server" ID="Tab">
<Header>  
    <center>
        <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
        <br />            
    </center>       
    <asp:Label runat="server" ID="lbl" />     
</Header>
<Content>     

<br />
        <div class="page-content-wrap" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-2">
                            &nbsp;
                        </div>
                        <div>                                                
                            <ul class="panel-controls">                                    
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ul>                                                                                                                                                     
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDXWOSearch" KeyFieldName="DAARDetlNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("DAARDetlNo") %>' OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />                                        
                                        <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />                                        
                                        <dx:GridViewDataTextColumn FieldName="ImmediateSuperiorDesc" Caption="Immediate Superior" />
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                    </Columns>                            
                                </dx:ASPxGridView>                                
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>
        </div> 

</Content>
</uc:Tab>

<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Record No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCodeDeti" ReadOnly="true" runat="server" CssClass="form-control" Placeholder= "Autonumber"/>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Issued To :</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" Placeholder="Type here..." style="display:inline-block;" onblur="ResetEmployeeNo()" /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="1"
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getMain1" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                </div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtEmployeeCode" CssClass="form-control" Enabled="false" ReadOnly="true" style="display:inline-block;" />                                     
                </div>
                <script type="text/javascript">

                    function SplitH(obj, index) {
                        var items = obj.split("|");
                        for (i = 0; i < items.length; i++) {
                            if (i == index) {
                                return items[i];
                            }
                        }
                    }

                    function getMain1(source, eventArgs) {
                        document.getElementById('<%= hifEmployeeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                        document.getElementById('<%= txtEmployeeCode.ClientID %>').value = SplitH(eventArgs.get_value(), 12);
                        document.getElementById('<%= txtImmediateSuperiorDesc.ClientID %>').value = SplitH(eventArgs.get_value(), 14);
                        document.getElementById('<%= hifImmediateSuperiorNo.ClientID %>').value = SplitH(eventArgs.get_value(), 13);
                    }

                    function ResetEmployeeNo() {
                        if (document.getElementById('<%= txtFullName.ClientID %>').value == "") {
                            document.getElementById('<%= hifEmployeeNo.ClientID %>').value = "0";
                            document.getElementById('<%= txtEmployeeCode.ClientID %>').value = "";
                            document.getElementById('<%= txtImmediateSuperiorDesc.ClientID %>').value = "";
                            document.getElementById('<%= hifImmediateSuperiorNo.ClientID %>').value = "0";
                        }
                    } 
                </script>

            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Immediate Superior :</label>
                <div class="col-md-7">
                        <asp:Textbox ID="txtImmediateSuperiorDesc" runat="server"  CssClass="form-control" Enabled="false" ReadOnly="true" />
                        <asp:HiddenField ID="hifImmediateSuperiorNo" runat="server" />
                </div>
            </div>            
            <br />
        </div>                    
    </fieldset>
</asp:Panel>
 
</asp:Content>

