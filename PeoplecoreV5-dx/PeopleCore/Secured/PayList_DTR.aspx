<%@ Page Language="VB" Theme="PCoreStyle"  MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="PayList_DTR.aspx.vb" Inherits="Secured_PayList_DTR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<script type="text/javascript">
  
</script>
  
       
 <div class="page-content-wrap">     
 <uc:PayHeader runat="server" ID="PayHeader" />
    <uc:FormTab runat="server" ID="FormTab" />       
        <div class="row">              
            <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-2">
                            &nbsp;
                        </div>
                        <div>                                                
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                <ContentTemplate>
                                    <ul class="panel-controls">
                                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                    </ul>
                                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?"  />
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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" SkinID="grdDX" runat="server" KeyFieldName="DTRDetiNo" >                                                                                   
                                    <Columns>      
                                    <dx:GridViewDataTextColumn FieldName="DTRCode" Caption="DTR No." />                      
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name"  />  
                                    <dx:GridViewDataTextColumn FieldName="Hrs" Caption="Req. Hrs" Width="2%" />                                                                       
                                    <dx:GridViewDataTextColumn FieldName="WorkingHrs" Caption="Work Hrs" Width="2%" />                  
                                    <dx:GridViewDataTextColumn FieldName="PaidLeave" Caption="Leave Hrs" Width="2%"  />
                                    <dx:GridViewDataTextColumn FieldName="HolidayHrs" Caption="Paid Hol." Width="2%"  />
                                    <dx:GridViewDataTextColumn FieldName="DOvt" Caption="OT" />
                                    <dx:GridViewDataTextColumn FieldName="DOvt8" Caption="OT8" />
                                    <dx:GridViewDataTextColumn FieldName="DNP" Caption="NP" />
                                    <dx:GridViewDataTextColumn FieldName="DNP8" Caption="NP8" />
                                    <dx:GridViewDataTextColumn FieldName="AbsHrs" Caption="Absent" />
                                    <dx:GridViewDataTextColumn FieldName="Late" Caption="Late" />
                                    <dx:GridViewDataTextColumn FieldName="Under" Caption="Under" />
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="2%">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>  
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%" /> 
                                </Columns>    
                                <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="true" />
                                    
                                </dx:ASPxGridView> 
                                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                   
                            </div>
                        </div>                                                       
                    </div>                   
                </div>

         </div>
     </div>

     <div class="page-content-wrap">         
        <div class="row">
              <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="panel-title" >
                            DTR No. :&nbsp; <asp:Label runat="server" ID="lblDetl" />
                        </div>                   
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                                </ul>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lnkExportDetl" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                                <div class="table-responsive">                    
                                    <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="DTRDetiLogNo" Width="100%" >                                                                                   
                                        <Columns>                           
                                            <dx:GridViewDataColumn FieldName="DTRDate" Caption="Date" />
                                            <%--<dx:GridViewDataColumn FieldName="ShiftCode" Caption="Shift" /> --%>   
                                            <dx:GridViewDataColumn Caption="Shift Code" CellStyle-HorizontalAlign="Center" Width="10">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkViewShift"  Font-Size="X-Small" Text='<%# Bind("ShiftCode") %>' OnClick="lnkViewShift_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>                                                                        
                                            <dx:GridViewDataColumn FieldName="DayTypeCode" Caption="D-Type" /> 
                                            <dx:GridViewDataColumn FieldName="DayTypeCode2" Caption="D-Type2" Visible="false" /> 
                                            <dx:GridViewDataColumn FieldName="DayOffDesc" Caption="Day" Visible="false"/> 
                                            <dx:GridViewDataColumn FieldName="Hrs" Caption="Req.<br/>Hrs" />   
                                            <dx:GridViewDataColumn FieldName="ActualHrs" Caption="Actual Hrs" Visible="false" />
                                            <dx:GridViewDataColumn FieldName="In1" Caption="In1" />
                                            <dx:GridViewDataColumn FieldName="Out1" Caption="Out1" />
                                            <dx:GridViewDataColumn FieldName="In2" Caption="In2" />
                                            <dx:GridViewDataColumn FieldName="Out2" Caption="Out2" />
                                            <dx:GridViewDataColumn FieldName="OvtIn1" Caption="OT<br/>In1" />
                                            <dx:GridViewDataColumn FieldName="OvtOut1" Caption="OT<br/>Out1" />
                                            <dx:GridViewDataColumn FieldName="OvtIn2" Caption="OT<br/>In2" />
                                            <dx:GridViewDataColumn FieldName="OvtOut2" Caption="OT<br/>Out2" />
                                            <dx:GridViewDataColumn FieldName="WorkingHrs" Caption="Work<br/>Hrs" />
                                            <dx:GridViewDataColumn FieldName="Ovt" Caption="OT" />
                                            <dx:GridViewDataColumn FieldName="Ovt8" Caption="OT8" />        
                                            <dx:GridViewDataColumn FieldName="NP" Caption="NP" />
                                            <dx:GridViewDataColumn FieldName="NP8" Caption="NP8" />                        
                                            <dx:GridViewDataTextColumn FieldName="AbsHrs" Caption="Abs." PropertiesTextEdit-EncodeHtml="false" />
                                            <dx:GridViewDataTextColumn FieldName="Late" Caption="Late" PropertiesTextEdit-EncodeHtml="false" />
                                            <dx:GridViewDataTextColumn FieldName="Under" Caption="Under" PropertiesTextEdit-EncodeHtml="false" />
                                            <dx:GridViewDataColumn FieldName="LeaveTypeCode" Caption="L-Type" />
                                            <dx:GridViewDataColumn FieldName="LeaveHrs" Caption="L-Hrs" />  
                                            <dx:GridViewDataColumn FieldName="HolidayHrs" Caption="Hol. Hrs" />                                                                         
                                        </Columns>             
                                        <SettingsPager PageSize="60" />                     
                                    </dx:ASPxGridView>
                                            
                                    <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />  
                                </div>                            
                            </div>
                    </div>
                </div>

        </div>
     </div>


<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mdlShow" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" PopupControlID="pnlPopup" TargetControlID="btnShow"></ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlPopup" runat="server"  CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsd">
    <!-- Header here -->
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="btnCancel" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsd btnSave" OnClick="btnSave_Click"  />
        </div>
        <!-- Body here -->
        <div  class="entryPopupDetl form-horizontal">

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayDTRNo" CssClass="form-control" runat="server" Placeholder="Autonumber" ReadOnly="true"></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">DTR No. :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboDTRNo" runat="server" CssClass="required form-control"></asp:Dropdownlist>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label  has-space">Name of Employee :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" Placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo" />
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
            <br />
            <br />
        </div>
        
        </fieldset>
</asp:Panel>

</asp:Content>

