<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterPage.master" Theme="PCoreStyle" CodeFile="EmpBUDSDisaster.aspx.vb" Inherits="Secured_EmpBUDSDisaster" %>



<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">

     <script type="text/javascript">
         function cbCheckAll_CheckedChanged(s, e) {
             grdMain.PerformCallback(s.GetChecked().toString());
         }

        
    </script>

    <br />
    <div class="page-content-wrap" >
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                   
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                    
                                </ul>
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeBudsDisasterNo"
                           >
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("EmployeeBudsDisasterNo") %>' OnClick="lnkEdit_Click" />
                                         
                                        </DataItemTemplate>

                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." />
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                    <dx:GridViewDataTextColumn FieldName="HomeAddress" Caption="Home Address" />
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

<asp:UpdatePanel runat="server" ID="upUpload">
<ContentTemplate>
<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style=" display:none;">
    <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />     
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Trans. No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtEmployeeBudsDisasterNo" CssClass="form-control" runat="server" 
                        ></asp:Textbox>
                </div>
            </div>
        
            <div class="form-group">
                <label class="col-md-3 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtCode"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber" ></asp:TextBox>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-3 control-label has-required">Name of Employee :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" onblur="ResetRecord" style="display:inline-block;" Placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="cboEmployee" CompletionSetCount="1" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true"/>
                     <script type="text/javascript">
                         function SplitH(obj, index) {
                             var items = obj.split("<");
                             for (i = 0; i < items.length; i++) {
                                 if (i == index) {
                                     return items[i];
                                 }
                             }
                         }
                         function Split(obj, index) {
                             var items = obj.split("|");
                             for (i = 0; i < items.length; i++) {
                                 if (i == index) {
                                     return items[i];
                                 }
                             }
                         }

                         function ResetRecord() {
                             if (document.getElementById('<%= txtFullName.ClientID %>').value == "") {
                                 document.getElementById('<%= hifEmployeeNo.ClientID %>').value = "0";
                                 document.getElementById('<%= hifHomeAddress.ClientID %>').value = "";
                                 document.getElementById('<%= txtHomeHouseNo.ClientID %>').value = "";
                                 document.getElementById('<%= txtHomeStreet.ClientID %>').value = "";
                                 document.getElementById('<%= txtHomeSubd.ClientID %>').value = "";
                                 document.getElementById('<%= txtHomeBarangay.ClientID %>').value = "";
                                 
                             }
                         }

                         function getRecord(source, eventArgs) {
                             document.getElementById('<%= hifEmployeeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                             document.getElementById('<%= hifHomeAddress.ClientID %>').value = SplitH(eventArgs.get_value(), 1);

                             var homeaddress = SplitH(eventArgs.get_value(), 1);
                             document.getElementById('<%= txtHomeHouseNo.ClientID %>').value = Split(homeaddress, 0);
                             document.getElementById('<%= txtHomeStreet.ClientID %>').value = Split(homeaddress, 1);
                             document.getElementById('<%= txtHomeSubd.ClientID %>').value = Split(homeaddress, 2);
                             document.getElementById('<%= txtHomeBarangay.ClientID %>').value = Split(homeaddress, 3);
                             
                                                                                
                         }

                        </script>
                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3 control-label has-space">Residential Address 1:</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtHomeHouseNo" ReadOnly="true" CssClass="form-control " placeholder="House/Block/Lot No."/>
                    <asp:HiddenField runat="server" ID="hifHomeAddress"/>
                </div>
                <div class="col-md-3 ">
                    <asp:TextBox runat="server" ID="txtHomeStreet" ReadOnly="true" CssClass="form-control " placeholder="Street"/>
                </div>   
            </div>           
            <div class="form-group">
                <label class="col-md-3 control-label has-space"></label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtHomeSubd" ReadOnly="true" CssClass="form-control" placeholder="Subdivision/Village" />
                </div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtHomeBarangay" ReadOnly="true" CssClass="form-control " placeholder="Barangay"/>
                </div>                
            </div>            
            <%--<div class="form-group">
                <label class="col-md-3 control-label has-space"></label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtCityHomeDesc" ReadOnly="true" CssClass="form-control " placeholder="City / Municipality" /> 
                    <asp:HiddenField runat="server" ID="hifCityHomeNo"/>
                    </div>
                <div class="col-md-3">                                           
                    <asp:TextBox runat="server" ID="txtProvinceDesc2" ReadOnly="true" CssClass="form-control" placeholder="Province"  />
                    <asp:HiddenField runat="server" ID="hifProvinceNo2"/>
                </div>
            </div>
            <div class="form-group" style="display:block;">
                <label class="col-md-3 control-label has-space"></label>
                <div class="col-md-3">                        
                    <asp:TextBox runat="server" ID="txtPostalCode2" ReadOnly="true" CssClass="form-control" placeholder="Zip Code" />
                </div>
            </div>--%>
           <div class="row">
              <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label has-space"></label>
                    <div class="col-md-7">
                        <asp:CheckBox ID="txtIsRescue" runat="server" Text="&nbsp;Rescue"></asp:CheckBox><br />
                            
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space"></label>
                    <div class="col-md-7">
                        <asp:CheckBox ID="txtIsMedicine" runat="server" Text="&nbsp;Medicine"></asp:CheckBox><br />
                            
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space"></label>
                    <div class="col-md-7">
                        <asp:CheckBox ID="txtIsFoodAndWater" runat="server" Text="&nbsp;Food and water"></asp:CheckBox><br />
                            
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space"></label>
                    <div class="col-md-7">
                        <asp:CheckBox ID="txtIsTranspo" runat="server" Text="&nbsp;Transportation"></asp:CheckBox><br />
                            
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space"></label>
                    <div class="col-md-7">
                        <asp:CheckBox ID="txtIsTempShelter" runat="server" Text="&nbsp;Temporary Shelter"></asp:CheckBox><br />
                            
                    </div>
                </div>
                
               </div>
               <div class="col-md-6">
                    <div class="form-group">
                    <label class="col-md-1 control-label has-space"></label>
                    <div class="col-md-7">
                        <asp:Image runat="server" ID="imgPhoto" ImageAlign="Middle" Width="160" Height="160" />
                        <br />
                        <asp:HiddenField runat="server" ID="hifimagefile"/>
                    </div>
                </div>
               </div>
             </div>
             <div class="form-group">
                <br />
            </div>
            <div class="form-group">
                <label class="col-md-3 control-label">Image File :</label>
                <div class="col-md-6">
                    <asp:FileUpload runat="server" ID="fuPhoto" Width="350" />
                </div>
            </div>  
             <div class="form-group">
                <label class="col-md-3 control-label has-space">Remarks :</label>
                <div class="col-md-6">                        
                    <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="4" CssClass="form-control"  />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3 control-label has-space">Estimated cost of damage(Personal) :</label>
                <div class="col-md-6">                        
                    <asp:TextBox runat="server" ID="txtDamageCost"  CssClass="form-control number"  />
                </div>
            </div>
            
         </div>
          <!-- Footer here -->
         <br />
            
        
    </fieldset>
</asp:Panel>
</ContentTemplate>
<Triggers>
    <asp:PostBackTrigger ControlID="lnkSave" />
</Triggers>
</asp:UpdatePanel>  
</asp:Content>
