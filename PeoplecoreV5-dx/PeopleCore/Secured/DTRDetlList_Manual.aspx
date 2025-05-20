<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="DTRDetlList_Manual.aspx.vb" Inherits="Secured_DTRDetlList_Manual" Theme="PCoreStyle" %>

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
                        <li><asp:LinkButton runat="server" ID="lnkUpload" OnClick="lnkUpload_Click" Text="Upload" CssClass="control-primary" /></li>  
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DTRDetiEditedNo"
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
                                <dx:GridViewDataTextColumn FieldName="WorkingHrs" Caption="Hrs Work" />
                                <dx:GridViewDataTextColumn FieldName="PaidLeave" Caption="Leave Hrs" />
                                <dx:GridViewDataTextColumn FieldName="Ovt" Caption="OT" />
                                <dx:GridViewDataTextColumn FieldName="Ovt8" Caption="OT8" />
                                <dx:GridViewDataTextColumn FieldName="NP" Caption="NP" />
                                <dx:GridViewDataTextColumn FieldName="NP8" Caption="NP8" />
                                <dx:GridViewDataTextColumn FieldName="AbsHrs" Caption="Absent" />
                                <dx:GridViewDataTextColumn FieldName="Late" Caption="Late" />
                                <dx:GridViewDataTextColumn FieldName="Under" Caption="Undertime" />  
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
                    <asp:Textbox ID="txtDTRDetiEditedNo" CssClass="form-control" runat="server" 
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
                    CompletionInterval="250" ServiceMethod="PopulateEmployee_Encoder" CompletionSetCount="0"
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
                <label class="col-md-3 control-label has-space">Working Hrs :</label>
                <div class="col-md-8">
                    <asp:TextBox ID="txtWorkingHrs" SkinID="txtdate" runat="server" CssClass="form-control" 
                        ></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Night Premium Hrs :</label>
                <div class="col-md-8">
                    <asp:TextBox ID="txtNP" SkinID="txtdate" runat="server" CssClass="form-control" 
                        ></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Deduction Hrs :</label>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">Absent</label><br />
                        <asp:TextBox ID="txtAbsHrs" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">Late</label><br />
                        <asp:TextBox ID="txtLate" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">Undertime</label><br />
                        <asp:TextBox ID="txtUnder" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Leave Hrs :</label>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">VL</label><br />
                        <asp:TextBox ID="txtVL" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">SL</label><br />
                        <asp:TextBox ID="txtSL" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">OB</label><br />
                        <asp:TextBox ID="txtOB" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space"></label>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">EL</label><br />
                        <asp:TextBox ID="txtEL" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">ML</label><br />
                        <asp:TextBox ID="txtML" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">PTL</label><br />
                        <asp:TextBox ID="txtPTL" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space"></label>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">FL</label><br />
                        <asp:TextBox ID="txtFL" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">PL</label><br />
                        <asp:TextBox ID="txtPL" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">SPL</label><br />
                        <asp:TextBox ID="txtSPL" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Overtime Hrs :</label>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">OT</label><br />
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">OT>8</label><br />
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">OT NP</label><br />
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">OT NP>8</label><br />
                    </center>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Regular Working Day :</label>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtOvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtOvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtNPOvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtNPOvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Rest Day :</label>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRDOvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRDOvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRDOvtNP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRDOvt8NP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Regular Holiday RWD :</label>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRHNROvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRHNROvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRHNROvtNP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRHNROvt8NP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Regular Holiday RD :</label>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRHRDOvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRHRDOvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRHRDOvtNP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRHRDOvt8NP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3 control-label has-space">Special Holiday RWD :</label>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtSHNROvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtSHNROvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtSHNROvtNP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtSHNROvt8NP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Special Holiday RD :</label>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtSHRDOvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtSHRDOvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtSHRDOvtNP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtSHRDOvt8NP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
            </div>
            <div class="form-group">
                <br />
            </div>

        </div>
        
         </fieldset>
</asp:Panel>

<asp:UpdatePanel runat="server" ID="upUpload">
<ContentTemplate>
<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup2">
    <fieldset class="form" id="Fieldset1">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl2 form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-7">
                   
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Select file name :</label>
                <div class="col-md-7">
                    <asp:FileUpload runat="server" ID="fuDoc" Width="100%" CssClass="required" />
                </div>
            </div>                        
        </div>
        <br /><br />
    </fieldset>
</asp:Panel>
</ContentTemplate>
<Triggers>
    <asp:PostBackTrigger ControlID="lnkSave" />
</Triggers>
</asp:UpdatePanel>   

</asp:Content>

