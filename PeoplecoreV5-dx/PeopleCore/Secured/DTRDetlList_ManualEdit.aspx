<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="DTRDetlList_ManualEdit.aspx.vb" Inherits="Secured_DTRDetiListSummaryEdit" %>


<asp:content id="cntNo" contentplaceholderid="cphBody" runat="server">
<fieldset class="form" id="fsMain">
<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="table-responsive">
                        
                           <div  class="form-horizontal">
                            <div class="form-group">
                                <label class="col-md-2 control-label  has-required">Name of Employee :</label>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" /> 
                                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                    CompletionListCssClass="autocomplete_completionListElement" 
                                    CompletionListItemCssClass="autocomplete_listItem" 
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                        <script type="text/javascript">
                                            function getRecord(source, eventArgs) {
                                                document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                                            }
                                            </script>
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label">Working hrs. :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtWorkingHrs" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
    
                                    <label class="col-md-2 control-label">Absent hrs. :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtAbsHrs" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
    
                                    <label class="col-md-2 control-label">Late hrs. :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtLate" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
    
                                    <label class="col-md-2 control-label">Undertime hrs. :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtUnder" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
                            </div>
                            <div class="form-group" runat="server" visible="false">
                                    <label class="col-md-2 control-label">Holiday pay hrs. :</label>
                                    <div class="col-md-6">
                                    <asp:Textbox ID="txtRegH" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label">LEAVE</label>
                                    <div class="col-md-6">
            
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label">Force Leave (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtFL" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
    
                                    <label class="col-md-2 control-label">Official business (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtOB" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
    
                                    <label class="col-md-2 control-label">Vacation leave (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtVL" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
    
                                    <label class="col-md-2 control-label">Sick Leave (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtSL" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label">Emergency Leave (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtEL" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
  
                                    <label class="col-md-2 control-label">Maternity Leave (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtML" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
    
                                    <label class="col-md-2 control-label">Paternity Leave (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtPTL" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
    
                                    <label class="col-md-2 control-label">Special Leave (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtSPL" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label">Personal Leave (hrs.) :</label>
                                    <div class="col-md-6">
                                    <asp:Textbox ID="txtPL" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label">REGULAR NIGHT PREMIUM :</label>
                                    <div class="col-md-6">
            
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label">NP (hrs.) :</label>
                                    <div class="col-md-6">
                                    <asp:Textbox ID="txtNP" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label">REGULAR OVERTIME</label>
                                    <div class="col-md-6">
            
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label">OT (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtOvt" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
    
                                    <label class="col-md-2 control-label">OT>8 (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtOvt8" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
    
                                    <label class="col-md-2 control-label">NP-OT (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtNPOvt" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
    
                                    <label class="col-md-2 control-label">NP-OT>8 (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtNPOvt8" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label">REST DAY OVERTIME</label>
                                    <div class="col-md-6">
            
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label">RD-OT (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtRDOvt" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
    
                                    <label class="col-md-2 control-label">RD-OT>8 (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtRDOvt8" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
   
                                    <label class="col-md-2 control-label">NP (RDOT) (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtRDOvtNP" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
   
                                    <label class="col-md-2 control-label">NP (RDOT>8) (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtRDOvt8NP" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label">REGULAR HOLIDAY NON-REST DAY OVERTIME</label>
                                    <div class="col-md-6">
            
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label">RHNR OT (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtRHNROvt" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
    
                                    <label class="col-md-2 control-label">RHNR OT>8 (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtRHNROvt8" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
    
   
                                    <label class="col-md-2 control-label">NP (RHNR OT) (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtRHNROvtNP" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
    
                                    <label class="col-md-2 control-label">NP (RHNR OT>8) (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtRHNROvt8NP" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label">REGULAR HOLIDAY REST DAY</label>
                                    <div class="col-md-6">
            
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label">RHRD OT (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtRHRDOvt" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>

                                    <label class="col-md-2 control-label">RHRD OT>8 (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtRHRDOvt8" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>

                                    <label class="col-md-2 control-label">NP (RHRD OT) (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtRHRDOvtNP" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>

                                    <label class="col-md-2 control-label">NP (RHRD OT>8) (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtRHRDOvt8NP" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label">SPECIAL HOLIDAY NON-REST DAY OVERTIME</label>
                                    <div class="col-md-6">
            
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label">SHNR OT (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtSHNROvt" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
 
                                    <label class="col-md-2 control-label">SHNR OT>8 (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtSHNROvt8" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>

                                    <label class="col-md-2 control-label">NP SHNR OT (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtSHNROvtNP" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>

                                    <label class="col-md-2 control-label">NP SHNR OT>8 (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtSHNROvt8NP" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label">SPECIAL HOLIDAY REST-DAY OVERTIME</label>
                                    <div class="col-md-6">
            
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label">SHRD OT (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtSHRDOvt" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>

                                    <label class="col-md-2 control-label">SHRD OT>8 (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtSHRDOvt8" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>

                                    <label class="col-md-2 control-label">NP SHRD OT (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtSHRDOvtNP" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>

                                    <label class="col-md-2 control-label">NP SHRD OT>8 (hrs.) :</label>
                                    <div class="col-md-1">
                                    <asp:Textbox ID="txtSHRDOvt8NP" CssClass="number form-control"  runat="server" SkinID="txtdate"
                                        ></asp:Textbox>
                                </div>
                            </div>
                            <div class="form-group" runat="server" visible="false">
                                    <label class="col-md-2 control-label"></label>
                                    <div class="col-md-6">
                                    <asp:Checkbox ID="txtIsServed"  runat="server" 
                                        ></asp:Checkbox>
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-md-2 control-label"></label>
                                    <div class="col-md-6">
            
                                        <div class="btn-group">
                                            <asp:Button runat="server"  ID="lnkSubmit" CssClass="btn btn-default submit fsMain" Text="submit" OnClick= "btnSave_Click" ></asp:Button>
          
                                            <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="modify"></asp:Button>

                                            <asp:Button runat="server"  ID="lnkCancel" CssClass="btn btn-default" CausesValidation="false" Text="<< Back/Cancel"></asp:Button>
                                        </div>
          
                                </div>
                            </div> 
                            </div>
                       
                    </div>
                </div>
            </div>
        </div>
    </div>
 </fieldset>
</asp:content>
