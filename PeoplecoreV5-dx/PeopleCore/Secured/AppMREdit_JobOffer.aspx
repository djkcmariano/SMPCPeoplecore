<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AppMREdit_JobOffer.aspx.vb" Inherits="Secured_AppMREdit_JobOffer" Theme="PCoreStyle" %>
<%@ Register Src="~/Include/Info.ascx" TagName="Info" TagPrefix="uc" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<script type="text/javascript">
    function SelectAllCheckboxesMain(spanChk) {

        // Added as ASPX uses SPAN for checkbox
        var oItem = spanChk.children;
        var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
        xState = theBox.checked;
        elm = theBox.form.elements;

        for (i = 0; i < elm.length; i++)
            if (elm[i].type == "checkbox" && elm[i].name.indexOf("txtIsSelect") > 0 &&
            elm[i].id != theBox.id) {
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
            }
    }

    function SelectAllCheckboxesDetl(spanChk) {

        // Added as ASPX uses SPAN for checkbox
        var oItem = spanChk.children;
        var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
        xState = theBox.checked;
        elm = theBox.form.elements;

        for (i = 0; i < elm.length; i++)
            if (elm[i].type == "checkbox" && elm[i].name.indexOf("txtIschk") > 0 &&
            elm[i].id != theBox.id) {
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
            }
    }
</script> 

    <uc:Tab runat="server" ID="Tab">
        <Header>            
            <asp:Label runat="server" ID="lbl" />                                                    
        </Header>
        <Content>
            <br />
            <div class="page-content-wrap">         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-2">
                            <h4>&nbsp;</h4>                                
                        </div>
                        <div>
                            <uc:Filter runat="server" ID="Filter1" EnableContent="false">
                                <Content>
                               
                                </Content>
                            </uc:Filter>
                        </div>                           
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <mcn:DataPagerGridView ID="grdMain" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging" DataKeyNames="MRHiredMassNo, Fullname, IsEnabled">
                                    <Columns>                                                                        
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" SkinID="grdEditbtn" CssClass="cancel" OnClick="btnEdit_Click" CommandArgument='<%# Bind("MRHiredMassNo") %>' />                                           
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server"   Text='<%# Bind("MRHiredMassNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Code" HeaderText="Transaction No." SortExpression="Code" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:BoundField>  

                                    <asp:TemplateField HeaderText="Name" SortExpression="Fullname">
                                        <ItemTemplate >
                                            <asp:LinkButton runat="server" ID="lnk" Text='<%# Bind("Fullname") %>' CommandArgument='<%# Eval("ID") & "|" & Eval("IsApplicant") & "|" & Eval("Fullname") %>' OnClick="lnk_Click" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>  
                                    
                                    <asp:BoundField DataField="PayOffer" HeaderText="Pay Offer" SortExpression="PayOffer" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="EmployeeRateClassDesc" HeaderText="Rate Class" SortExpression="EmployeeRateClassDesc" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:BoundField>

                                    <%--<asp:BoundField DataField="ApplicantTypeDesc" SortExpression="ApplicantTypeDesc" HeaderText="Applicant Type" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:BoundField>--%>

                                   
                                    <asp:TemplateField HeaderText="Offer Status" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" >
                                        <ItemTemplate >      
                                            <asp:DropDownList  CssClass="form-control" ID="cboJobOfferStatNo" Enabled='<%# Bind("IsEnabled") %>'  Text='<%# Bind("JobOfferStatNo") %>' AppendDataBoundItems="True"  runat="server" DataSourceID="ObjectDataSource3" DataTextField="tDesc" DataValueField="tNo">
                                            </asp:DropDownList><asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="Lookup_JobOfferStat" TypeName="clsLookup">
                                            </asp:ObjectDataSource>                                       
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="20%"/>
                                    </asp:TemplateField> 

                                     <%--<asp:TemplateField HeaderText="Form" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" >
                                        <ItemTemplate >      
                                            <asp:LinkButton runat="server" ID="lnkPrint" CssClass="fa fa-print" OnClick="lnkPrint_Click" Font-Size="Medium" OnPreRender="lnkPrint_PreRender" CommandArgument='<%# Bind("MRHiredMassNo") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" Width="5%"/>
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Offer Details" >
                                        <ItemTemplate >
                                            <asp:ImageButton ID="btnPreview" runat="server" SkinID="grdDetail"  OnClick="lnkView_Click" CausesValidation="false" />                                   
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>

                                    <%--<asp:TemplateField HeaderText="JO Form">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkPrint" CssClass="fa fa-print" OnClick="lnkPrint_Click" Font-Size="Medium" OnPreRender="lnkPrint_PreRender" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                    </asp:TemplateField>
                                    --%>
                                    <asp:TemplateField HeaderText="Select"  >
                                        <HeaderTemplate>
                                            Select<br />
                                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="txtIsSelectAllMain" onclick ="SelectAllCheckboxesMain(this);"  runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="txtIsSelect" Enabled='<%# Bind("IsEnabled") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                    </asp:TemplateField>
                                                                                                                                                                            
                                </Columns>                                
                                </mcn:DataPagerGridView>                                
                            </div>
                        </div>                    
                        <div class="row">
                            <div class="col-md-6">                                
                                <asp:DataPager ID="dpMain" runat="server" PagedControlID="grdMain" PageSize="10">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Image" FirstPageImageUrl="~/images/arrow_first.png" PreviousPageImageUrl="~/images/arrow_previous.png" ShowFirstPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" ShowPreviousPageButton="true" />
                                            <asp:TemplatePagerField>
                                                <PagerTemplate>Page
                                                    <asp:Label ID="CurrentPageLabel" runat="server" Text="<%# IIf(Container.TotalRowCount>0,  (Container.StartRowIndex / Container.PageSize) + 1 , 0) %>" /> of
                                                    <asp:Label ID="TotalPagesLabel" runat="server" Text="<%# Math.Ceiling (System.Convert.ToDouble(Container.TotalRowCount) / Container.PageSize) %>" /> (
                                                    <asp:Label ID="TotalItemsLabel" runat="server" Text="<%# Container.TotalRowCount%>" /> records )
                                                </PagerTemplate>
                                            </asp:TemplatePagerField>
                                        <asp:NextPreviousPagerField ButtonType="Image" LastPageImageUrl="~/images/arrow_last.png" NextPageImageUrl="~/images/arrow_next.png" ShowFirstPageButton="false" ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" />                              
                                    </Fields>
                                </asp:DataPager>
                            </div>  
                            
                            <div class="col-md-4 col-md-offset-2">
                                <!-- Button here btn-group -->
                                <div class="pull-right">
                                    <asp:Button ID="btnUpdate" runat="server" CausesValidation="false" cssClass="btn btn-primary" Text="Save" OnClick="btnUpdate_Click"></asp:Button>                                
                                    <asp:Button ID="btnAdd" runat="server" CausesValidation="false" cssClass="btn btn-primary" Text="Add" OnClick="btnAdd_Click"></asp:Button>
                                    <asp:Button ID="btnDelete" runat="server" CausesValidation="false" cssClass="btn btn-default" Text="Delete" UseSubmitBehavior="false" OnClick="btnDelete_Click" />
                                </div>
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="btnDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </div> 
                                                              
                        </div>                       
                    </div>                   
                </div>



            </div>


            <div class="row">
                  <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="panel-title">
                              <asp:Label ID="lblName" runat="server"></asp:Label>               
                        </div>
                        <div>
                            <ul class="panel-controls">     
                                <li><asp:LinkButton runat="server" ID="lnkJobOffer" OnClick="lnkPrint_Click" Text="Generate Job Offer" CssClass="control-primary" OnPreRender="lnkPrint_PreRender" /></li>                           
                            </ul>                                                     
                        </div> 
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                                                                   
                                    <%--<li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false" /></li>--%>
                                </ul>
                               <%-- <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="panel-body">
                    <div class="row">
                        <div class="panel-body">
                             <div class="row">
                                <div class="col-md-6">
                                    <h3>Allowance</h3> 
                                </div>
                                <div class="col-md-6">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                        <ContentTemplate>
                                                <ul class="panel-controls">
                                                    <li><asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                                    <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>
                                                    <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                                                </ul>
                                                <uc:ConfirmBox runat="server" ID="cfbDeleteDetl" TargetControlID="lnkExportDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lnkExportDetl" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="MROfferNo" Width="100%">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" Enabled='<%# Bind("IsEnabled") %>' />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                    <dx:GridViewDataComboBoxColumn FieldName="PayIncomeTypeDesc" Caption="Allowance Type" />
                                    <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" /> 
                                    <dx:GridViewDataCheckColumn FieldName="IsPerDay" Caption="Per Day" ReadOnly="true" />  
                                    <dx:GridViewDataTextColumn FieldName="PayScheduleDesc" Caption="Payroll Schedule" />
                                    <dx:GridViewDataTextColumn FieldName="EncodedBy" Caption="Encoded By" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date Encoded" Visible="false" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" /> 
                                </Columns>     
                                <SettingsContextMenu Enabled="true">                                
                                    <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                                </SettingsContextMenu>                                                                                            
                                <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="true"  />   
                                <SettingsSearchPanel Visible="false" />  
                                                       
                            </dx:ASPxGridView>
                            
                            <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />   
                        </div>
                        </div>
                    </div>

                     <div class="row">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <h3>Benefit Package</h3>
                                </div>
                                <div class="col-md-6">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                        <ContentTemplate>
                                                <ul class="panel-controls">
                                                    <li><asp:LinkButton runat="server" ID="lnkAddBen" OnClick="lnkAddBen_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                                    <li><asp:LinkButton runat="server" ID="lnkDeleteBen" OnClick="lnkDeleteBen_Click" Text="Delete" CssClass="control-primary" /></li>
                                                    <li><asp:LinkButton runat="server" ID="lnkExportBen" OnClick="lnkExportBen_Click" Text="Export" CssClass="control-primary" /></li>
                                                </ul>
                                                <uc:ConfirmBox runat="server" ID="cfbDeleteBen" TargetControlID="lnkDeleteBen" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lnkExportBen" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdBen" ClientInstanceName="grdBen" runat="server" Width="100%" KeyFieldName="MRBenefitPackageNo"
                            OnCustomCallback="grdBen_CustomCallback" OnCustomColumnSort="grdBen_CustomColumnSort" OnCustomColumnDisplayText="grdBen_CustomColumnDisplayText">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEditBen" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditBen_Click" Enabled='<%# Bind("IsEnabled") %>'/>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="CodeDeti" Caption="Detail No." />
                                    <dx:GridViewDataTextColumn FieldName="BenefitPackageTypeDesc" Caption="Benefit Package Type" GroupIndex="0" >
                                        <Settings SortMode="Custom" />
					                </dx:GridViewDataTextColumn>  
                                    <dx:GridViewDataTextColumn FieldName="MRBenefitPackageDesc" Caption="Description" />
                                    <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" />
                                    <dx:GridViewDataTextColumn FieldName="OrderLevel" Caption="Order" />
                                    <dx:GridViewDataTextColumn FieldName="EncodedBy" Caption="Encoded By" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date Encoded"  Visible="false" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" /> 
                                </Columns>     
                                <SettingsContextMenu Enabled="true">                                
                                        <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                                </SettingsContextMenu>                                                                                            
                                <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="True" /> 
                                <SettingsSearchPanel Visible="false" />                       
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExportBen" runat="server" GridViewID="grdBen" />  
                        </div>
                        </div>
                    </div>



                    <div class="row">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <h3>Document Signatory</h3>
                                </div>
                                <div class="col-md-6">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                        <ContentTemplate>
                                                <ul class="panel-controls">
                                                    <li><asp:LinkButton runat="server" ID="lnkAddApp" OnClick="lnkAddApp_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                                    <li><asp:LinkButton runat="server" ID="lnkDeleteApp" OnClick="lnkDeleteApp_Click" Text="Delete" CssClass="control-primary" /></li>
                                                    <li><asp:LinkButton runat="server" ID="lnkExportApp" OnClick="lnkExportApp_Click" Text="Export" CssClass="control-primary" /></li>
                                                </ul>
                                                <uc:ConfirmBox runat="server" ID="cfbDeleteApp" TargetControlID="lnkDeleteApp" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lnkExportApp" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdApp" ClientInstanceName="grdApp" runat="server" Width="100%" KeyFieldName="MRApprovalRoutingNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEditApp" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditApp_Click" Enabled='<%# Bind("IsEnabled") %>'/>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                    <dx:GridViewDataTextColumn FieldName="HRANApprovalTypeDesc" Caption="Approval Type" />
                                    <dx:GridViewDataTextColumn FieldName="SignatoryName" Caption="Signatory Name" />
                                    <dx:GridViewDataTextColumn FieldName="ImmediateName" Caption="Immediate Name" />
                                    <dx:GridViewDataTextColumn FieldName="xApproveDate" Caption="Date Approved" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="Remark" Caption="Remarks"  Visible="false"/>
                                    <dx:GridViewDataTextColumn FieldName="OrderNo" Caption="Order No." />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" /> 
                                </Columns>     
                                <SettingsContextMenu Enabled="true">                                
                                        <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                                </SettingsContextMenu>                                                                                            
                                <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="True" /> 
                                <SettingsSearchPanel Visible="false" />                       
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExportApp" runat="server" GridViewID="grdApp" />  
                        </div>
                        </div>
                    </div>

                </div>
                   
                   </div>
            </div>

        </div>    



        
        
        <uc:Info runat="server" ID="Info1" />
        
        
        <!-- Add main -->
        <asp:Button ID="Button1" runat="server" style="display:none" />
        <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
        <asp:Panel id="Panel1" runat="server" CssClass="entryPopup2">
            <fieldset class="form" id="fsMain">                    
                <div class="cf popupheader">
                    <h4>&nbsp;</h4>
                    <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
                </div>                                           
                <div class="entryPopupDetl2 form-horizontal">                        
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Applicant Type :</label>
                        <div class="col-md-7">
                            <asp:DropDownList runat="server" ID="cboHiringAlternativeNo" CssClass="form-control" onchange="SetContextKey()" OnSelectedIndexChanged="cboHiringAlternativeNo_SelectedIndexChanged" AutoPostBack="true" >
                                <asp:ListItem Text="External Applicant" Value="1" Selected="True" />
                                <asp:ListItem Text="Internal Applicant" Value="0" />
                            </asp:DropDownList>
                            <asp:HiddenField runat="server" ID="hidTransNo" />
                            <script type="text/javascript">
                                function SetContextKey() {
                                    var e = document.getElementById('<%= cboHiringAlternativeNo.ClientID %>');
                                    var str = e.options[e.selectedIndex].value + '|' + document.getElementById('<%= hidTransNo.ClientID %>').value;
                                    $find('<%= AutoCompleteExtender1.ClientID %>').set_contextKey(str);
                                }
                            </script>
                        </div>
                    </div>                                        
                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Name :</label>
                        <div class="col-md-7">
                            
                            <asp:TextBox runat="server" ID="txtFullname" CssClass="form-control required" style="display:inline-block;" placeholder="Type here..." /> 
                            <asp:HiddenField runat="server" ID="hidID" />
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                            TargetControlID="txtFullname" MinimumPrefixLength="2" 
                            CompletionInterval="500" ServiceMethod="ApplicantType" ServicePath="~/asmx/WebService.asmx"
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListItemCssClass="autocomplete_listItem" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            OnClientItemSelected="getID" FirstRowSelected="true" UseContextKey="true" />
                            
                            <script type="text/javascript">
                                function Split(obj, index) {
                                    var items = obj.split("|");
                                    for (i = 0; i < items.length; i++) {
                                        if (i == index) {
                                            return items[i];
                                        }
                                    }
                                }

                                function getID(source, eventArgs) {
                                    document.getElementById('<%= hidID.ClientID %>').value = Split(eventArgs.get_value(), 0);
                                }
                            </script>

                        </div> 
                    </div> 
                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Pay Offer :</label>
                        <div class="col-md-7">
                            <asp:TextBox ID="txtPayOffer" runat="server" CssClass="required form-control"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtPayOffer" />
                        </div>
                    </div> 
                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Rate Class :</label>
                        <div class="col-md-7">
                                <asp:DropdownList ID="cboEmployeeRateClassNo"  runat="server" DataMember="EEmployeeRateClass" CssClass="required form-control">
                                </asp:DropdownList>
                         </div>
                    </div>   

                    <div class="form-group">
                <label class="col-md-4 control-label has-required">Contract Duration :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control required" Placeholder="From" ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                        TargetControlID="txtStartDate"
                        Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                        TargetControlID="txtStartDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" 
                        ErrorTooltipEnabled ="true" 
                        ClearTextOnInvalid="true"  
                        />
                                                                        
                        <asp:RangeValidator
                        ID="RangeValidator3"
                        runat="server"
                        ControlToValidate="txtStartDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                                                        
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" 
                        ID="ValidatorCalloutExtender6"
                        TargetControlID="RangeValidator3"
                        />   
                </div>

                <%--<label class="col-md-1 control-label">To :</label>--%>
                <div class="col-md-3">
                    <asp:TextBox ID="txtEndDate" runat="server"  CssClass="form-control required" Placeholder="To" ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                        TargetControlID="txtEndDate"
                        Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                        TargetControlID="txtEndDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" 
                        ErrorTooltipEnabled ="true" 
                        ClearTextOnInvalid="true"  
                        />
                                                                        
                     <asp:RangeValidator
                        ID="RangeValidator4"
                        runat="server"
                        ControlToValidate="txtEndDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />

                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" 
                        ID="ValidatorCalloutExtender2"
                        TargetControlID="RangeValidator4"
                        />       

                </div>
            </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Remarks :</label>
                        <div class="col-md-7">
                            <asp:TextBox ID="txtJobOfferRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>

                <div class="form-group">
                <label class="col-md-4 control-label has-space">JO Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtJOEncodeDate" runat="server" CssClass="form-control" Placeholder="JO Start Date" ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                        TargetControlID="txtJOEncodeDate"
                        Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                        TargetControlID="txtJOEncodeDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" 
                        ErrorTooltipEnabled ="true" 
                        ClearTextOnInvalid="true"  
                        />
                                                                        
                        <asp:RangeValidator
                        ID="RangeValidator1"
                        runat="server"
                        ControlToValidate="txtJOEncodeDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                                                        
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" 
                        ID="ValidatorCalloutExtender1"
                        TargetControlID="RangeValidator3"
                        />   
                </div>

                <div class="col-md-3">
                    <asp:TextBox ID="txtJOEndDate" runat="server"  CssClass="form-control" Placeholder="JO End Date" ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                        TargetControlID="txtJOEndDate"
                        Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                        TargetControlID="txtJOEndDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" 
                        ErrorTooltipEnabled ="true" 
                        ClearTextOnInvalid="true"  
                        />
                                                                        
                     <asp:RangeValidator
                        ID="RangeValidator2"
                        runat="server"
                        ControlToValidate="txtJOEndDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />

                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" 
                        ID="ValidatorCalloutExtender3"
                        TargetControlID="RangeValidator4"
                        />       

                    </div>
                 </div>

                    <br /><br />
                </div>                   
            </fieldset>
        </asp:Panel>



        <asp:Button ID="btnShowDetl" runat="server" style="display:none" />
        <ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl" CancelControlID="lnkCloseDetl" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
        <asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup2">
            <fieldset class="form" id="fsDetl">
                 <div class="cf popupheader">
                      <h4>&nbsp;</h4>
                      <asp:Linkbutton runat="server" ID="lnkCloseDetl" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveDetl" OnClick="btnSaveDetl_Click" CssClass="fa fa-floppy-o submit fsDetl lnkSaveDetl" ToolTip="Save" />
                 </div>
                <div  class="entryPopupDetl2 form-horizontal">

                    <div class="form-group" style="display:none;">
                        <label class="col-md-4 control-label has-space">Detail No. :</label>
                        <div class="col-md-7">
                                <asp:TextBox ID="txtMROfferNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                        
                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Allowance Type :</label>
                        <div class="col-md-7">
                                <asp:DropdownList ID="cboPayIncomeTypeNo"  runat="server" CssClass="required form-control">
                                </asp:DropdownList>
                         </div>
                    </div>    
      
                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Amount :</label>
                        <div class="col-md-3">
                                <asp:TextBox ID="txtAmount" runat="server" CssClass="required form-control"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtAmount" />
                         </div>
                    </div>        

                    <div class="form-group">
                        <label class="col-md-4 control-label has-space"></label>
                        <div class="col-md-8">
                            <asp:CheckBox ID="txtIsPerDay" runat="server" Text="&nbsp; Amount is per day" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Payroll Schedule :</label>
                        <div class="col-md-7">
                            <asp:DropDownList ID="cboPayScheduleNo" runat="server" CssClass="form-control required" DataMember="EPaySchedule">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    
                </div>
                <div class="cf popupfooter">
                 </div> 
            </fieldset>
        </asp:Panel>
       
       
       
<asp:Button ID="btnShowBen" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlShowBen" runat="server" TargetControlID="btnShowBen" PopupControlID="pnlPopupBen" CancelControlID="imgClosed" BackgroundCssClass="modalBackground" >
    </ajaxtoolkit:ModalPopupExtender>

    <asp:Panel id="pnlPopupBen" runat="server" CssClass="entryPopup" style="display:none">
            <fieldset class="form" id="fsBen">
            <!-- Header here -->
             <div class="cf popupheader">
                    <asp:Linkbutton runat="server" ID="imgClosed" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                    <asp:LinkButton runat="server" ID="btnSaveBen" CssClass="fa fa-floppy-o submit fsBen btnSaveBen" OnClick="btnSaveBen_Click"  />   
             </div>
             <!-- Body here -->
             <div  class="entryPopupBen form-horizontal">
                <br />

                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label">Reference No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtMRBenefitPackageNo" runat="server" CssClass="form-control" 
                            ></asp:Textbox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Detail No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCodeDeti" runat="server" CssClass="form-control" Enabled="false" Placeholder="Autonumber"></asp:Textbox>
                     </div>
                </div>
             
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Benefit Package Type :</label>
                    <div class="col-md-7">
                        <asp:DropDownList ID="cboBenefitPackageTypeNo" AutoPostBack="true" runat="server" DataMember="EBenefitPackageType" CssClass="form-control" OnTextChanged="cboBenefitPackageTypeNo_TextChanged" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Benefit Description :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtMRBenefitPackageDesc" runat="server" CssClass="required form-control" TextMode="MultiLine" Rows="3" />
                    </div>
                </div> 

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Remarks :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                    </div>
                </div> 

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Order :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtOrderLevel" runat="server" CssClass="form-control number" />
                    </div>
                </div> 

                <br />
                </div>
              <!-- Footer here -->
         
             </fieldset>
    </asp:Panel>
    

    <asp:Button ID="btnShowApp" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlShowApp" runat="server" TargetControlID="btnShowApp" PopupControlID="pnlPopupApp" CancelControlID="imgClosedApp" BackgroundCssClass="modalBackground" >
    </ajaxtoolkit:ModalPopupExtender>

    <asp:Panel id="pnlPopupApp" runat="server" CssClass="entryPopup" style="display:none">
            <fieldset class="form" id="fsApp">
            <!-- Header here -->
             <div class="cf popupheader">
                    <asp:Linkbutton runat="server" ID="imgClosedApp" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                    <asp:LinkButton runat="server" ID="btnSaveApp" CssClass="fa fa-floppy-o submit fsApp btnSaveApp" OnClick="btnSaveApp_Click"  />   
             </div>
             <!-- Body here -->
             <div  class="entryPopupDetl form-horizontal">
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">
                    Detail No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtMRApprovalRoutingNo" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Detail No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" Enabled="false" Placeholder="Autonumber"></asp:Textbox>
                     </div>
                </div>

                <div class="form-group" style="visibility:hidden; position:absolute;">
                    <label class="col-md-4 control-label has-space">
                    &nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox ID="chkIsApproved" runat="server" Text="&nbsp;Please tick if you want to approve this transaction" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Approval Type :</label>
                    <div class="col-md-7">
                        <asp:DropDownList ID="cboHRANApprovalTypeNo" runat="server" CssClass="required form-control" DataMember="EHRANApprovalType" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Signatory Name :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtSignatoryName" CssClass="form-control" Placeholder="Type here..." />
                        <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"  
                        TargetControlID="txtSignatoryName" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getRecordS" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                        <script type="text/javascript">
                            function SplitS(obj, index) {
                                var items = obj.split("|");
                                for (i = 0; i < items.length; i++) {
                                    if (i == index) {
                                        return items[i];
                                    }
                                }
                            }

                            function getRecordS(source, eventArgs) {
                                document.getElementById('<%= hifEmployeeNo.ClientID %>').value = SplitS(eventArgs.get_value(), 0);
                            }
                        </script>
                    </div>
                </div>
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">
                    Date Approved :</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtApproveDate" runat="server" CssClass="form-control" />
                        <ajaxToolkit:CalendarExtender ID="customCalendarExtender" runat="server" Format="MM/dd/yyyy" TargetControlID="txtApproveDate" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedSeparatedDate" runat="server" 
                        AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" 
                        MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" 
                        OnInvalidCssClass="MaskedEditError" TargetControlID="txtApproveDate" />
                    </div>
                </div>
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">
                    Remarks :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Name :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtImmediateName" runat="server" CssClass="form-control"/>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Order No. :</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtOrderNo" runat="server" CssClass="number required form-control" />
                    </div>
                </div>
                <br />
                </div>
              <!-- Footer here -->
         
             </fieldset>
    </asp:Panel>
                 
        </Content>
    </uc:Tab>

</asp:Content>

