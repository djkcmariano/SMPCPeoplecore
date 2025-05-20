<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AppMREdit_PreEmployment.aspx.vb" Inherits="Secured_AppMREdit_PreEmployment" Theme="PCoreStyle" %>
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
            if (elm[i].type == "checkbox" && elm[i].name.indexOf("txtxIsSelectMain") > 0 &&
            elm[i].id != theBox.id) {
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
            }
        }

        function SelectAllCheckboxes(spanChk) {

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


        function SubmittedAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].name.indexOf("txtIsSubmitted") > 0 &&
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
                                    <asp:TemplateField Visible="false">
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

                                    <asp:TemplateField HeaderText="Proceed" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="txtIsProceed" runat="server" Text='<%# Bind("IsProceed") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                        <ItemTemplate>
                                            <asp:Label ID="lblMessage" runat="server"   Text='<%# Bind("MessageDialog") %>'></asp:Label>
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
                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:TemplateField>  
                                    
                                    <asp:BoundField DataField="ApplicantTypeDesc" SortExpression="ApplicantTypeDesc" HeaderText="Applicant Type" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:BoundField>
                                    
                                    <asp:TemplateField HeaderText="Pre-Employment Status" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" >
                                        <ItemTemplate >      
                                            <asp:DropDownList  CssClass="form-control" ID="cboRequiredStatNo" Enabled='<%# Bind("IsEnabled") %>'  Text='<%# Bind("RequiredStatNo") %>' AppendDataBoundItems="True"  runat="server" DataSourceID="ObjectDataSource3" DataTextField="tDesc" DataValueField="tNo">
                                            </asp:DropDownList><asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="Lookup_RequiredStat" TypeName="clsLookup">
                                            </asp:ObjectDataSource>                                       
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="15%"/>
                                    </asp:TemplateField> 

                                    <asp:TemplateField HeaderText="Total Records" SortExpression="TotalCount">
                                        <ItemTemplate >
                                            <asp:LinkButton runat="server" ID="lblTotal" Text='<%# Bind("TotalCount") %>' CssClass="badge badge-info" OnClick="lnkFilter1_Click" CommandArgument='<%# Bind("MRHiredMassNo") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                    </asp:TemplateField>  
                                                                                                                                                            
                                    <asp:TemplateField HeaderText="Total Pending" SortExpression="TotalPending">
                                        <ItemTemplate >
                                            <asp:LinkButton runat="server" ID="lblPending" Text='<%# Bind("TotalPending") %>' CssClass="badge badge-warning" OnClick="lnkFilter2_Click" CommandArgument='<%# Bind("MRHiredMassNo") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Total Submitted" SortExpression="TotalSubmitted">
                                        <ItemTemplate >
                                            <asp:LinkButton runat="server" ID="lblSubmitted" Text='<%# Bind("TotalSubmitted") %>' CssClass="badge badge-success" OnClick="lnkFilter3_Click" CommandArgument='<%# Bind("MRHiredMassNo") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Checklist" >
                                        <ItemTemplate >
                                            <asp:ImageButton ID="btnPreview" runat="server" SkinID="grdDetail"  OnClick="lnkView_Click" CausesValidation="false" />                                   
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    </asp:TemplateField> 
                                    
                                    <asp:TemplateField HeaderText="Select" >
                                       <HeaderTemplate>
                                            <center>
                                            Select<br />
                                            <asp:CheckBox ID="txtIsSelectAllMain" onclick ="SelectAllCheckboxesMain(this);"  runat="server" />
                                            </center>
                                        </HeaderTemplate> 
                                        <ItemTemplate>
                                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="txtxIsSelectMain" Enabled='<%# Bind("IsEnabled") %>' runat="server" />
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
                                    <asp:Button ID="btnBatch" runat="server" CausesValidation="false" cssClass="btn btn-primary" Text="Batch Action" OnClick="btnBatch_Click"></asp:Button>
                                    <asp:Button ID="btnUpdate" runat="server" CausesValidation="false" cssClass="btn btn-primary" Text="Save" OnClick="btnUpdate_Click"></asp:Button>                                
                                    <asp:Button ID="btnAdd" runat="server" CausesValidation="false" cssClass="btn btn-primary" Text="Add" OnClick="btnAdd_Click" Visible="false"></asp:Button>
                                    <asp:Button ID="btnDelete" runat="server" CausesValidation="false" cssClass="btn btn-default" Text="Delete" UseSubmitBehavior="false" OnClick="btnDelete_Click" Visible="false" />
                                </div>
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="btnDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </div> 
                                                              
                        </div>                       
                    </div>                   
                </div>
                
                <div class="panel panel-default">
                    <div class="panel-heading">
                            <div class="col-md-6">
                                <h4 class="panel-title"><asp:Label ID="lblDetl" CssClass="lbltextsmall11-color"  runat="server" /></h4>                                
                            </div>
                            <div>
                                <uc:Filter runat="server" ID="Filter2" EnableContent="false">
                                    <Content>
                               
                                    </Content>
                                </uc:Filter>
                            </div>     
                    </div>
                    <div class="panel-body">
                        <div class="row">
                        <div class="table-responsive-vertical" style="max-height:420px;">
                            <div class="table-responsive" style="margin-bottom: 0px;">
                                <mcn:DataPagerGridView ID="grdDetl" runat="server" SkinID="AllowPaging-No" AllowSorting="true" OnSorting="grdDetl_Sorting" OnPageIndexChanging="grdDetl_PageIndexChanging" DataKeyNames="MRHiredMassNo, ApplicantCheckListNo, IsEnabled" >
                                        <Columns>
                                            <asp:TemplateField ShowHeader="false" Visible="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEditDetl" runat="server" CausesValidation="false" CssClass="cancel" OnClick="lnkEditDetl_Click" SkinID="grdEditbtn" ToolTip="Click here to edit" CommandArgument='<%# Bind("ApplicantCheckListNo") %>'  />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server"   Text='<%# Bind("ApplicantCheckListNo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="code"  SortExpression="code" HeaderText="Detail No." Visible="false" >
                                                <HeaderStyle Width="10%"  HorizontalAlign  ="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="ApplicantStandardCheckListDesc"  SortExpression="ApplicantStandardCheckListDesc" HeaderText="Checklist"    >
                                                <HeaderStyle Width="20%"  HorizontalAlign  ="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            
                                            <asp:TemplateField HeaderText="Remarks">
                                                <HeaderTemplate>
                                                    <center>
                                                    Remarks
                                                    </center>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Textbox ID="txtRemarks" Text='<%# Bind("Remarks") %>' runat="server" CssClass="form-control" Enabled='<%# Bind("IsEnabled") %>'></asp:Textbox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                            </asp:TemplateField>                 
                                                          
                                            <asp:BoundField DataField="IsRequired"  SortExpression="IsRequired" HeaderText="Required?" HtmlEncode="false"  >
                                                <HeaderStyle Width="3%"  HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            
                                            <asp:TemplateField HeaderText="Submitted">
                                                <HeaderTemplate>
                                                    <center>
                                                    Date Submitted
                                                    </center>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Textbox ID="txtDateSubmitted" Text='<%# Bind("DateSubmitted") %>' runat="server" CssClass="form-control" SkinID="txtdate" Enabled='<%# Bind("IsEnabled") %>' ></asp:Textbox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateSubmitted" Format="MM/dd/yyyy" />                   
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtDateSubmitted" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />            
                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtDateSubmitted" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                
                                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator2" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="4%" />
                                            </asp:TemplateField> 

                                            <asp:TemplateField HeaderText="Submitted">
                                                <HeaderTemplate>
                                                    <center>
                                                    Submitted?<br />
                                                    <asp:CheckBox ID="txtIsSubmittedAll" onclick ="SubmittedAllCheckboxes(this);"  runat="server" />
                                                    </center>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="txtIsSubmitted" runat="server" Checked='<%# Bind("IsSubmitted") %>' Enabled='<%# Bind("IsEnabled") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="4%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Select">
                                                <HeaderTemplate>
                                                    <center>
                                                    Select<br />
                                                    <asp:CheckBox ID="txtIsSelectAll" onclick ="SelectAllCheckboxes(this);"  runat="server" />
                                                    </center>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="txtIschk" runat="server" Enabled='<%# Bind("IsEnabled") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="4%" />
                                            </asp:TemplateField>

                                        </Columns>
                                    </mcn:DataPagerGridView>
                            </div>
                            </div>
                        </div>
                    
                        <div class="row">
                            <div class="col-md-4">
                                <!-- Paging here -->
                                <asp:DataPager ID="dpDetl" runat="server" PagedControlID="grdDetl" PageSize="10">
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
                            <div class="col-md-6 col-md-offset-2">
                                <!-- Button here btn-group -->
                                <div class="pull-right">
                                    <asp:Button ID="btnUpdateDetl" runat="server" CausesValidation="false" cssClass="btn btn-primary" Text="Save" OnClick="btnUpdateDetl_Click"></asp:Button>
                                    <asp:Button ID="btnAddDetl" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnAddDetl_Click" ToolTip="Click here to add" ></asp:Button>
                                    <asp:Button ID="btnDeleteDetl" Text="Delete" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnDeleteDetl_Click" ToolTip="Click here to delete" ></asp:Button>                       
                                </div>
                                <uc:ConfirmBox ID="ConfirmBox2" runat="server" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?" TargetControlID="btnDeleteDetl" />
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
                <br />                                           
                <div class="entryPopupDetl2 form-horizontal">                        
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Hiring Alternative :</label>
                        <div class="col-md-7">
                            <asp:DropDownList runat="server" ID="cboHiringAlternativeNo" CssClass="form-control" onchange="SetContextKey()">
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
                            <asp:HiddenField runat="server" ID="hidID"/>
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
                </div>
                <br /><br />                    
            </fieldset>
        </asp:Panel>

        <asp:Button ID="btnShowDetl" runat="server" style="display:none" />
        <ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl" CancelControlID="lnkCloseDetl" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
        <asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup2">
            <fieldset class="form" id="fsDetl">
                 <div class="cf popupheader">
                      <h4>&nbsp;</h4>
                      <asp:Linkbutton runat="server" ID="lnkCloseDetl" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveDetl" OnClick="lnkSaveDetl_Click" CssClass="fa fa-floppy-o submit fsDetl lnkSaveDetl" ToolTip="Save" />
                 </div>
                <div  class="entryPopupDetl2 form-horizontal">

                    <div class="form-group" style="display:none;">
                        <label class="col-md-4 control-label has-space">Detail No. :</label>
                        <div class="col-md-7">
                                <asp:TextBox ID="txtApplicantChecklistNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                        
                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Checklist :</label>
                        <div class="col-md-7">
                                <asp:DropdownList ID="cboApplicantStandardChecklistNo"  runat="server" DataMember="EApplicantStandardChecklistL" CssClass="required form-control">
                                </asp:DropdownList>
                         </div>
                    </div>           

                    
                </div>
                <div class="cf popupfooter">
                 </div> 
            </fieldset>
        </asp:Panel>

        <asp:Button ID="Button2" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button2" PopupControlID="Panel2" CancelControlID="lnkClose2" BackgroundCssClass="modalBackground" />
            <asp:Panel id="Panel2" runat="server" CssClass="entryPopup2">
                <fieldset class="form" id="Fieldset1">                    
                    <div class="cf popupheader">
                        <h4>&nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="lnkClose2" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave2" OnClick="lnkSave2_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
                    </div> 
                    <br />                                           
                    <div class="entryPopupDetl2 form-horizontal">
                                                
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Pre-employment Status :</label>
                            <div class="col-md-7">
                                <asp:DropDownList  CssClass="form-control" ID="cboRequiredStatNo" DataMember="ERequiredStat" runat="server" >
                                </asp:DropDownList>
                            </div>
                        </div>                                        
                     
                    </div>
                    <br /><br />                    
                </fieldset>
            </asp:Panel>
                    
        </Content>
    </uc:Tab>

</asp:Content>

