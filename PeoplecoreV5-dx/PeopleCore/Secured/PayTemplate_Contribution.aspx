<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayTemplate_Contribution.aspx.vb" Inherits="Secured_PayTemplate_Contribution" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<uc:Tab runat="server" menustyle="TabRef" ID="Tab">
    <Content>
    <br />
    <div class="page-content-wrap">            
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        &nbsp;
                    </div>
                    <div>                                  
                        <ul class="panel-controls">
                       
                        </ul>

                    
                    </div>                                                                                                   
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayClassNo">                                                                                   
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                                
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                    <dx:GridViewDataTextColumn FieldName="PayClassCode" Caption="Code" />                                                                           
                                    <dx:GridViewDataTextColumn FieldName="PayClassDesc" Caption="Description" />                                
                                    <dx:GridViewBandColumn Caption="Contribution Formula" HeaderStyle-HorizontalAlign="Center">
                                        <Columns> 
                                            <dx:GridViewDataTextColumn FieldName="SSSBaseFormulaDesc" Caption="SSS" />  
                                            <dx:GridViewDataTextColumn FieldName="PHBaseFormulaDesc" Caption="PH"/> 
                                            <dx:GridViewDataTextColumn FieldName="HDMFBaseFormulaDesc" Caption="HDMF"/> 
                                         </Columns>
                                    </dx:GridViewBandColumn> 
                                    <dx:GridViewBandColumn Caption="Contribution Schedule" HeaderStyle-HorizontalAlign="Center">
                                        <Columns> 
                                            <dx:GridViewDataTextColumn FieldName="SSSPayScheduleDesc" Caption="SSS" />  
                                            <dx:GridViewDataTextColumn FieldName="PHPayScheduleDesc" Caption="PH"/> 
                                            <dx:GridViewDataTextColumn FieldName="HDMFPayScheduleDesc" Caption="HDMF"/> 
                                         </Columns>
                                    </dx:GridViewBandColumn> 
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />                            
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
<br /><br />

<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPayClassCode" runat="server" Enabled="false" CssClass="form-control required" />
                </div>
            </div>                        
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPayClassDesc" runat="server" Enabled="false" CssClass="form-control required" />
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">SSS Contribution Base Formula :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboSSSBaseFormulaNo" DataMember="EContributionFormula" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">SSS Contribution :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboSSSPayScheduleNo" DataMember="EPaySchedule" CssClass="form-control" placeholder="Schedule" />
                </div>
                <div class="col-md-3">
                    <asp:CheckBox ID="chkIsSSSEEPaNobyER" runat="server" Text="&nbsp;Tick if paid by employer" /> 
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">PH Contribution Base Formula :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboPHBaseFormula" DataMember="EContributionFormula" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">PH Contribution :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboPHPayScheduleNo" DataMember="EPaySchedule" CssClass="form-control" placeholder="Schedule" />
                </div>
                <div class="col-md-4">
                    <asp:CheckBox ID="chkIsPHEEPaNobyER" runat="server" Text="&nbsp;Tick if paid by employer" /> 
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">HDMF Contribution Base Formula :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboHDMFBaseFormula" DataMember="EContributionFormula" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">HDMF Contribution :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboHDMFPayScheduleNo" DataMember="EPaySchedule" CssClass="form-control" placeholder="Schedule" />
                </div>
                <div class="col-md-4">
                    <asp:CheckBox ID="chkIsHDMFEEPaNobyER" runat="server" Text="&nbsp;Tick if paid by employer" /> 
                </div>
            </div>
            <br />
            <div runat="server" visible="false">
                <h5><b>PAG-IBIG CONTRIBUTION REFERENCES</b></h5>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsHDMFFlatRate" Text="&nbsp;HDMF Contribution is Flat Rate" />
                    </div>                
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">HDMF Amount :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtHDMFAmount" runat="server" CssClass="form-control number" />               
                    </div>                
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Percent in HDMF Contribution :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtPercentHDMF" runat="server" CssClass="form-control number" />               
                    </div>                
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Maximum Income :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtMaxAmtHDMF" runat="server" CssClass="form-control number" />               
                    </div>                
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label has-space">Contribution Signatory :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtsignatory" CssClass="form-control " style="display:inline-block;" /> 
                    <asp:HiddenField runat="server" ID="hifsignatoryno"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server"  
                    TargetControlID="txtsignatory" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateManager" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="1"
                    OnClientItemSelected="getSignatory" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                        <script type="text/javascript">
                            function getSignatory(source, eventArgs) {
                                document.getElementById('<%= hifsignatoryno.ClientID %>').value = eventArgs.get_value();
                            }
                        </script>                    
                </div>
            </div 
                            
            <br /><br />
            <br /><br />

        </div>                    
    </fieldset>
</asp:Panel>

</asp:Content>

