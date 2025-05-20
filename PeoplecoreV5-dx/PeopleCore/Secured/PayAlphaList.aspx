<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayAlphaList.aspx.vb" Inherits="Secured_PayAlphaList" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
    
     <script type="text/javascript">

         function grid_ContextMenu(s, e) {
             if (e.objectType == "row")
                 hiddenfield.Set('VisibleIndex', parseInt(e.index));
             pmRowMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
         }

         function OnContextMenuItemClick(sender, args) {
             if (args.item.name == "Refresh") {
                 args.processOnServer = true;
                 args.usePostBack = true;
             }
         }

    </script>

    <div class="page-content-wrap">
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" >
                        </asp:Dropdownlist>
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkConso" OnClick="lnkConso_Click" Text="Consolidate" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkProcess" OnClick="lnkProcess_Click" Text="Process" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                </ul>
                                <uc:ConfirmBox runat="server" ID="cfbPost" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be modified, Proceed?" MessageType="Post"  />
                                <uc:ConfirmBox runat="server" ID="cfblnkProcess" TargetControlID="lnkProcess" ConfirmMessage="Do you want to proceed?" MessageType="Process"  /> 
                                <uc:ConfirmBox runat="server" ID="cfbConso" TargetControlID="lnkConso" ConfirmMessage="Do you want to proceed?" MessageType="Consolidate"  /> 
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="AlphaNo">
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Applicable Year" CellStyle-HorizontalAlign="Left" />
                                    <dx:GridViewDataTextColumn FieldName="MonthDesc" Caption="Applicable Month" CellStyle-HorizontalAlign="Left" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="PayLocDesc" Caption="Company Name" />
                                    <dx:GridViewDataTextColumn FieldName="FacilityDesc" Caption="Facility" Visible="false"/>
                                    <dx:GridViewDataTextColumn FieldName="AlphaDesc" Caption="Remarks" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="DateCreated" Caption="Date Created" Visible="false"/>
                                    <dx:GridViewDataTextColumn FieldName="LastGeneratedDate" Caption="Date Processed"  />
                                    <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="DatePosted" Caption="Posted Date" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="PostedBy" Caption="Posted By" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="DateProcessed" Caption="Processed Date" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="ProcessedBy" Caption="Processed By" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="MaxAmtAccumulatedExemp" Caption="Maximum accumulated exemption" Visible="false"/>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Exemption" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkExemption" CssClass="fa fa-external-link control-primary" Font-Size="Medium" OnClick="lnkExemption_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <%--<dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Process" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkProcess_Detail" CssClass='<%# Bind("Icon") %>' Enabled='<%# Bind("IsEnabled") %>' OnClick="lnkProcess_Detail_Click" />
                                            <uc:ConfirmBox runat="server" ID="cfProcess_Detail" TargetControlID="lnkProcess_Detail" ConfirmMessage='<%# Bind("ConfirmMessage") %>' Visible='<%# Bind("IsEnabled") %>'  />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>--%>
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                </Columns>
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
                        <div class="col-md-4 panel-title">
                            Transaction No. :&nbsp;&nbsp;<asp:Label ID="lblDetl" runat="server"></asp:Label>
                        </div>
                        <div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                <ContentTemplate>
                                    <ul class="panel-controls">
                                        <li><asp:LinkButton runat="server" ID="lnkUpload" OnClick="lnkUpload_Click" Text="Upload" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>
                                    </ul>
                                    <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDeleteDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkExportDetl" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="AlphaDetiNo;AlphaNo" Width="100%"
                           OnFillContextMenuItems="grdMain_FillContextMenuItems">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                    <dx:GridViewDataDateColumn FieldName="HiredDate" Caption="Date Hired" Visible="false" />
                                    <dx:GridViewDataDateColumn FieldName="RegularizedDate" Caption="Date Regularized" Visible="false" />
                                    <dx:GridViewDataDateColumn FieldName="SeparatedDate" Caption="Separated Date" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="TaxCode" Caption="Tax Code" />
                                    <dx:GridViewDataTextColumn FieldName="ScheduleNo" Caption="Schedule" />
                                    <dx:GridViewBandColumn Caption="A" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="TotalTaxableIncome" Caption="Taxable Income" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        </Columns>
                                    </dx:GridViewBandColumn>
                                    <dx:GridViewBandColumn Caption="B" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="TotalSalaryExempt" Caption="Salary Exemption" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        </Columns>
                                    </dx:GridViewBandColumn>
                                    <dx:GridViewBandColumn Caption="C" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="PersonalExemption" Caption="Personal Exemption" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        </Columns>
                                    </dx:GridViewBandColumn>
                                    <dx:GridViewBandColumn Caption="Tax Annualization" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="GrossTaxableIncome" Caption="Net Taxable (A - B - C)" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                            <dx:GridViewDataTextColumn FieldName="OverAmount" Caption="Over Amount (-)" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                            <dx:GridViewDataTextColumn FieldName="PercentTax" Caption="Percent Tax (*)" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                            <dx:GridViewDataTextColumn FieldName="BaseTax" Caption="Base Tax (+)" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        </Columns>
                                    </dx:GridViewBandColumn>
                                    <dx:GridViewBandColumn Caption="D" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="TaxDue" Caption="Tax Due" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        </Columns>
                                    </dx:GridViewBandColumn>
                                    <dx:GridViewBandColumn Caption="E" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="TotalTaxWithHeld" Caption="Tax WithHeld" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        </Columns>
                                    </dx:GridViewBandColumn>
                                    <dx:GridViewBandColumn Caption="F = D - E" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="Refund" Caption="Payable / Refund" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        </Columns>
                                    </dx:GridViewBandColumn>
                                    <dx:GridViewBandColumn Caption="Dependent 1" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="Dependent1" Caption="Name" />
                                            <dx:GridViewDataDateColumn FieldName="BirthDateDep1" Caption="Birth Date" />
                                            <dx:GridViewDataTextColumn FieldName="AgeDep1" Caption="Age" />
                                        </Columns>
                                    </dx:GridViewBandColumn>
                                    <dx:GridViewBandColumn Caption="Dependent 2" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="Dependent2" Caption="Name" />
                                            <dx:GridViewDataDateColumn FieldName="BirthDateDep2" Caption="Birth Date" />
                                            <dx:GridViewDataTextColumn FieldName="AgeDep2" Caption="Age" />
                                        </Columns>
                                    </dx:GridViewBandColumn>
                                    <dx:GridViewBandColumn Caption="Dependent 3" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="Dependent3" Caption="Name" />
                                            <dx:GridViewDataDateColumn FieldName="BirthDateDep3" Caption="Birth Date" />
                                            <dx:GridViewDataTextColumn FieldName="AgeDep3" Caption="Age" />
                                        </Columns>
                                    </dx:GridViewBandColumn>
                                    <dx:GridViewBandColumn Caption="Dependent 4" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="Dependent4" Caption="Name" />
                                            <dx:GridViewDataDateColumn FieldName="BirthDateDep4" Caption="Birth Date" />
                                            <dx:GridViewDataTextColumn FieldName="AgeDep4" Caption="Age" />
                                        </Columns>
                                    </dx:GridViewBandColumn>
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                </Columns>  
                                <ClientSideEvents ContextMenu="grid_ContextMenu" />   
                                <SettingsContextMenu Enabled="true">                                
                                    <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                                </SettingsContextMenu>                                                                                            
                                <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="True" /> 
                                <SettingsSearchPanel Visible="true" />                          
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />  
                            
                            <dx:ASPxPopupMenu ID="pmRowMenu" runat="server" ClientInstanceName="pmRowMenu">
                                    <Items>
                                        <dx:MenuItem Text="Report" Name="Name">
                                            <Template>
                                                <asp:LinkButton runat="server" ID="lnkPrint" OnClick="lnkPrint_Click" CssClass="control-primary" Font-Size="small" OnPreRender="lnkPrint_PreRender" Text="BIR 2316 Report" /><br />
                                            </Template>
                                        </dx:MenuItem>
                                    </Items>
                                    <ItemStyle Width="200px"></ItemStyle>
                           </dx:ASPxPopupMenu>
                           <dx:ASPxHiddenField ID="hf" runat="server" ClientInstanceName="hiddenfield" />
                             
                        </div>
                    </div>
                </div>
                   
            </div>
       </div>
    </div>   


    <asp:Button ID="btnShowMain" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">
            <!-- Header here -->
            <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />
                &nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />
            </div>
            <!-- Body here -->
            <div  class="entryPopupDetl form-horizontal">
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtAlphaNo" runat="server" CssClass="form-control" ReadOnly="true" ></asp:Textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber"></asp:Textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Name of Company :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboPayLocNo" runat="server" CssClass="form-control required" AutoPostBack="true" OnSelectedIndexChanged="cboPayLocNo_SelectedIndexChanged" ></asp:DropdownList>
                    </div>
                </div>

                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Facility :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboFacilityNo" DataMember="EFacility" runat="server" CssClass="form-control" ></asp:DropdownList>
                    </div>
                </div>
                
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Applicable Year :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtApplicableYear" runat="server" CssClass="form-control required"></asp:Textbox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtApplicableYear" FilterType="Numbers" ValidChars="." />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Applicable Month :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboApplicableMonth" DataMember="EMonth" runat="server" CssClass="form-control" ></asp:DropdownList>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Maximum accumulated exemption :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtMaxAmtAccumulatedExemp" runat="server" CssClass="form-control required"></asp:Textbox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMaxAmtAccumulatedExemp" FilterType="Numbers, Custom" ValidChars="." />
                    </div>
                </div>

                <div class="form-group">
                <label class="col-md-4 control-label has-space">Signatory (No. 56 From 2316) :</label>
                <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtSignatory" CssClass="form-control" style="display:inline-block;" Placeholder="Type here..." /> 
                        <asp:HiddenField runat="server" ID="hifsignatoryNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server" EnableCaching="false"  
                        TargetControlID="txtSignatory" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />

                        <script type="text/javascript">
                            function Split(obj, index) {
                                var items = obj.split("|");
                                for (i = 0; i < items.length; i++) {
                                    if (i == index) {
                                        return items[i];
                                    }
                                }
                            }

                            function getRecord(source, eventArgs) {
                                document.getElementById('<%= hifsignatoryNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
                            }
                                </script>

                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Signatory (No. 58 From 2316) :</label>
                    <div class="col-md-7">
                
                        <asp:TextBox runat="server" ID="txtSignatory2" CssClass="form-control" style="display:inline-block;" Placeholder="Type here..." /> 
                        <asp:HiddenField runat="server" ID="hifsignatoryno2"/>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" EnableCaching="false"  
                        TargetControlID="txtSignatory2" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getRecordx" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />

                        <script type="text/javascript">
                            function Splitx(obj, index) {
                                var items = obj.split("|");
                                for (i = 0; i < items.length; i++) {
                                    if (i == index) {
                                        return items[i];
                                    }
                                }
                            }

                            function getRecordx(source, eventArgs) {
                                document.getElementById('<%= hifsignatoryNo2.ClientID %>').value = Splitx(eventArgs.get_value(), 0);
                            }
                                </script>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Remarks :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtAlphaDesc" TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control" ></asp:Textbox>
                    </div>
                </div>
                <br />
            </div>
            <!-- Footer here -->
        </fieldset>
    </asp:Panel>


</asp:Content>