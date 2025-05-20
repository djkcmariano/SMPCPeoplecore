<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="OrgPayClassList_Edit.aspx.vb" Inherits="Secured_OrgPayClassEdit" %>


<asp:content id="cntNo" contentplaceholderid="cphBody" runat="server">

<fieldset class="form" id="fsMain">
    <div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-body">
                   
                        <div  class="form-horizontal">
                            <div class="form-group">
                                    <label class="col-md-2 control-label">Transaction No. :</label>
                                    <div class="col-md-6">
                                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" ></asp:Textbox>
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label  has-required">Code :</label>
                                <div class="col-md-3">
                                        <asp:Textbox ID="txtPayClassCode" runat="server" CssClass="required form-control" ></asp:Textbox>
                                 </div>
                            
                                <label class="col-md-2 control-label  has-required">Description :</label>
                                <div class="col-md-4">
                                        <asp:Textbox ID="txtPayClassdesc" runat="server" CssClass="required form-control" ></asp:Textbox>
                                 </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label has-required">Company :</label>
                                <div class="col-md-9">
                                        <asp:Dropdownlist ID="cboPayLocNo" DataMember="EPayLoc" CssClass="required form-control" runat="server" ></asp:Dropdownlist>
                                    </div>
                            </div> 
                            <%--<div class="form-group">
                                                           
                                <label class="col-md-2 control-label">Please check here</label>
                                <div class="col-md-4">
                                        <asp:CheckBox ID="txtIsCrew" 
                                        runat="server" /> 
                                        <span> for crew employee.</span>
                                    </div>
                            </div>--%> 

                            <div class="form-group">
                                <label class="col-md-2 control-label">Noted by :</label>
                                <div class="col-md-4">
                                        <asp:TextBox runat="server" ID="txtnotedby" CssClass="form-control" style="display:inline-block;" /> 
                                        <asp:HiddenField runat="server" ID="hifnotedbyno"/>
                                        <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                                        TargetControlID="txtnotedby" MinimumPrefixLength="2" 
                                        CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                        CompletionListCssClass="autocomplete_completionListElement" 
                                        CompletionListItemCssClass="autocomplete_listItem" 
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="1"
                                        OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                         <script type="text/javascript">
                                             function getRecord(source, eventArgs) {
                                                 document.getElementById('<%= hifnotedbyno.ClientID %>').value = eventArgs.get_value();
                                             }
                                         </script>
                                    </div>
                            
                                <label class="col-md-1 control-label">Noted by 2 :</label>
                                <div class="col-md-4">
                                        <asp:TextBox runat="server" ID="txtnotedby2" CssClass="form-control" style="display:inline-block;" /> 
                                        <asp:HiddenField runat="server" ID="hifnotedbyno2"/>
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                                        TargetControlID="txtnotedby2" MinimumPrefixLength="2" 
                                        CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                        CompletionListCssClass="autocomplete_completionListElement" 
                                        CompletionListItemCssClass="autocomplete_listItem" 
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="1"
                                        OnClientItemSelected="getRecord1" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                         <script type="text/javascript">
                                             function getRecord1(source, eventArgs) {
                                                 document.getElementById('<%= hifnotedbyno2.ClientID %>').value = eventArgs.get_value();
                                             }
                                         </script>
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">Prepared by :</label>
                                <div class="col-md-4">
                                        <asp:TextBox runat="server" ID="txtpreparedby" CssClass="form-control" style="display:inline-block;" /> 
                                        <asp:HiddenField runat="server" ID="hifpreparedbyno"/>
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"  
                                        TargetControlID="txtpreparedby" MinimumPrefixLength="2" 
                                        CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                        CompletionListCssClass="autocomplete_completionListElement" 
                                        CompletionListItemCssClass="autocomplete_listItem" 
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="1"
                                        OnClientItemSelected="getRecord2" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                         <script type="text/javascript">
                                             function getRecord2(source, eventArgs) {
                                                 document.getElementById('<%= hifpreparedbyno.ClientID %>').value = eventArgs.get_value();
                                             }
                                         </script>
                                    </div>
                            
                                <label class="col-md-1 control-label">Prepared by 2:</label>
                                <div class="col-md-4">
                                        <asp:TextBox runat="server" ID="txtpreparedby2" CssClass="form-control" style="display:inline-block;" /> 
                                        <asp:HiddenField runat="server" ID="hifpreparedbyno2"/>
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"  
                                        TargetControlID="txtpreparedby2" MinimumPrefixLength="2" 
                                        CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                        CompletionListCssClass="autocomplete_completionListElement" 
                                        CompletionListItemCssClass="autocomplete_listItem" 
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="1"
                                        OnClientItemSelected="getRecord3" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                         <script type="text/javascript">
                                             function getRecord3(source, eventArgs) {
                                                 document.getElementById('<%= hifpreparedbyno2.ClientID %>').value = eventArgs.get_value();
                                             }
                                         </script>
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">Reviewed by :</label>
                                <div class="col-md-4">
                                        <asp:TextBox runat="server" ID="txtreviewedby" CssClass="form-control " style="display:inline-block;" /> 
                                        <asp:HiddenField runat="server" ID="hifreviewedbyno"/>
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"  
                                        TargetControlID="txtreviewedby" MinimumPrefixLength="2" 
                                        CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                        CompletionListCssClass="autocomplete_completionListElement" 
                                        CompletionListItemCssClass="autocomplete_listItem" 
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="1"
                                        OnClientItemSelected="getRecord4" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                         <script type="text/javascript">
                                             function getRecord4(source, eventArgs) {
                                                 document.getElementById('<%= hifreviewedbyno.ClientID %>').value = eventArgs.get_value();
                                             }
                                         </script>
                                    </div>
                            
                                <label class="col-md-1 control-label">Reviewed by 2 :</label>
                                <div class="col-md-4">
                                        <asp:TextBox runat="server" ID="txtreviewedby2" CssClass="form-control " style="display:inline-block;" /> 
                                        <asp:HiddenField runat="server" ID="hifreviewedbyno2"/>
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"  
                                        TargetControlID="txtreviewedby2" MinimumPrefixLength="2" 
                                        CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                        CompletionListCssClass="autocomplete_completionListElement" 
                                        CompletionListItemCssClass="autocomplete_listItem" 
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="1"
                                        OnClientItemSelected="getRecord5" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                         <script type="text/javascript">
                                             function getRecord5(source, eventArgs) {
                                                 document.getElementById('<%= hifreviewedbyno2.ClientID %>').value = eventArgs.get_value();
                                             }
                                         </script>
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">Approved by :</label>
                                <div class="col-md-4">
                                        <asp:TextBox runat="server" ID="txtapprovedby" CssClass="form-control " style="display:inline-block;" /> 
                                        <asp:HiddenField runat="server" ID="hifapprovedbyno"/>
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server"  
                                        TargetControlID="txtapprovedby" MinimumPrefixLength="2" 
                                        CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                        CompletionListCssClass="autocomplete_completionListElement" 
                                        CompletionListItemCssClass="autocomplete_listItem" 
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="1"
                                        OnClientItemSelected="getRecord6" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                         <script type="text/javascript">
                                             function getRecord6(source, eventArgs) {
                                                 document.getElementById('<%= hifapprovedbyno.ClientID %>').value = eventArgs.get_value();
                                             }
                                         </script>
                                    </div>
                           
                                <label class="col-md-1 control-label">Approved by 2 :</label>
                                <div class="col-md-4">
                                        <asp:TextBox runat="server" ID="txtapprovedby2" CssClass="form-control " style="display:inline-block;" /> 
                                        <asp:HiddenField runat="server" ID="hifapprovedbyno2"/>
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server"  
                                        TargetControlID="txtapprovedby2" MinimumPrefixLength="2" 
                                        CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                        CompletionListCssClass="autocomplete_completionListElement" 
                                        CompletionListItemCssClass="autocomplete_listItem" 
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="1"
                                        OnClientItemSelected="getRecord7" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                         <script type="text/javascript">
                                             function getRecord7(source, eventArgs) {
                                                 document.getElementById('<%= hifapprovedbyno2.ClientID %>').value = eventArgs.get_value();
                                             }
                                         </script>
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">SSS No. :</label>
                                <div class="col-md-2">
                                        <asp:Textbox ID="txtSSSNo" runat="server" CssClass="form-control" ></asp:Textbox>
                                    </div>
                            
                                <label class="col-md-1 control-label">PH No. :</label>
                                <div class="col-md-2">
                                        <asp:Textbox ID="txtPHNo" runat="server" CssClass="form-control" ></asp:Textbox>
                                    </div>
                            
                                <label class="col-md-1 control-label">HDMF No. :</label>
                                <div class="col-md-2">
                                        <asp:Textbox ID="txtHDMFNo" runat="server" CssClass="form-control" ></asp:Textbox>
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">Tin No. :</label>
                                <div class="col-md-2">
                                        <asp:Textbox ID="txtTINNo" runat="server" CssClass="form-control" ></asp:Textbox>
                                    </div>
                            
                                <label class="col-md-1 control-label">BIR code :</label>
                                <div class="col-md-2">
                                        <asp:Textbox ID="txtTINBranchCode" runat="server" CssClass="form-control" ></asp:Textbox>
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">Contribution base formula :</label>
                                <div class="col-md-5">
                                        <asp:Dropdownlist ID="cboSSSBaseFormulaNo" DataMember="EContributionFormula" runat="server" CssClass="form-control" ></asp:Dropdownlist>
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">SSS schedule :</label>
                                <div class="col-md-2">
                                        <asp:Dropdownlist ID="cboSSSPayScheduleNo" DataMember="EPaySchedule" runat="server" CssClass="form-control" ></asp:Dropdownlist>
                                    </div>
                            
                                <label class="col-md-2 control-label">Please check here</label>
                                <div class="col-md-4">
                                        <asp:CheckBox ID="txtIsSSSEEPaNobyER" 
                                        runat="server" /> 
                                        <span> if the SSS Contibution paid by the Employer.</span>
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">PH schedule :</label>
                                <div class="col-md-2">
                                        <asp:Dropdownlist ID="cboPHPayScheduleNo" DataMember="EPaySchedule" runat="server" CssClass="form-control" ></asp:Dropdownlist>
                                    </div>
                            
                                <label class="col-md-2 control-label">Please check here</label>
                                <div class="col-md-4">
                                        <asp:CheckBox ID="txtIsPHEEPaNobyER" 
                                        runat="server" />
                                        <span> if the PH Contibution paid by the Employer.</span>
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">HDMF schedule :</label>
                                <div class="col-md-2">
                                        <asp:Dropdownlist ID="cboHDMFPayScheduleNo" DataMember="EPaySchedule" runat="server" CssClass="form-control" ></asp:Dropdownlist>
                                    </div>
                            
                                <label class="col-md-2 control-label">Please check here</label>
                                <div class="col-md-4">
                                        <asp:CheckBox ID="txtIsHDMFEEPaNobyER" 
                                        runat="server" />
                                        <span> if the HDMF Contibution paid by the Employer.</span>
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">PAG-IBIG CONTRIBUTION REFERENCES :</label>
                                <div class="col-md-5">
                                        
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">Please check here</label>
                                <div class="col-md-3">
                                        <asp:CheckBox ID="txtIsHDMFFlatRate" 
                                        runat="server" />
                                        <span> if the HDMF Contribution is Flat Rate.</span>
                                    </div>
                            
                                <label class="col-md-1 control-label">HDMF Amount :</label>
                                <div class="col-md-3">
                                        <asp:TextBox ID="txtHDMFAmount"  
                                        runat="server" SkinID="txtdate"   CssClass="number form-control" 
                                        ></asp:TextBox>   
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">Percent in HDMF Contribution :</label>
                                <div class="col-md-5">
                                        <asp:TextBox ID="txtPercentHDMF"  
                                        runat="server" SkinID="txtdate"  CssClass="number form-control" 
                                        ></asp:TextBox>   
           
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">Maximum income :</label>
                                <div class="col-md-2">
                                        <asp:TextBox ID="txtMaxAmtHDMF"  
                                        runat="server" SkinID="txtdate"  CssClass="number form-control" 
                                        ></asp:TextBox>   
            
                                    </div>
                            
                                <label class="col-md-2 control-label">Maximum accumulated exemption :</label>
                                <div class="col-md-2">
                                        <asp:TextBox ID="txtMaxAmtAccumulatedExemp"  
                                        runat="server" SkinID="txtdate"  CssClass="number form-control" 
                                        ></asp:TextBox>   
           
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">Minimu take home pay :</label>
                                <div class="col-md-2">
                                        <asp:TextBox ID="txtMinNetPayForDedu"  
                                        runat="server" SkinID="txtdate"   CssClass="number form-control"
                                        ></asp:TextBox>   
            
                                    </div>
                            
                                <label class="col-md-2 control-label">Please check here</label>
                                <div class="col-md-2">
                                        <asp:CheckBox ID="txtIsMinNetPayInPercent" 
                                        runat="server" />
                                        <span> if the Minimum Take home pay is base on Percentage.</span>
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">Currency :</label>
                                <div class="col-md-5">
                                        <asp:Dropdownlist ID="cboCurrencyNo" DataMember="ECurrency" runat="server" CssClass="form-control" ></asp:Dropdownlist>
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">Contribution signatory :</label>
                                <div class="col-md-5">
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
                        </div> 
                        <div class="form-group">
                            <label class="col-md-2 control-label "></label>
                            <div class="col-md-9">
                                <div class="btn-group">
                                    <asp:Button runat="server"  ID="lnkSubmit" CssClass="btn btn-default submit fsMain" Text="submit" OnClick= "btnSave_Click" ></asp:Button>
          
                                    <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="modify"></asp:Button>

                                    <asp:Button runat="server"  ID="lnkCancel" style="display:none;" CssClass="btn btn-default" CausesValidation="false" Text="<< Back/Cancel"></asp:Button>
                                </div>
                            </div>
                        </div> 
                      
                    </div>
                </div>
            </div>
        </div>
    </div>
</fieldset >

</asp:content> 