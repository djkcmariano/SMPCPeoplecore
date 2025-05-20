<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="AppMREdit_SelectionProcess_Batch_JobOffer.aspx.vb" Inherits="Secured_AppMREdit_SelectionProcess_Batch_Offer" %>

<%@ Register Src="~/Include/Info.ascx" TagName="Info" TagPrefix="uc" %>
<%@ Register Src="~/Include/applicationhistory.ascx" TagName="apphistory" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<script type="text/javascript">
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
</script> 


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
                        <mcn:DataPagerGridView ID="grdMain" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging" DataKeyNames="MRHiredMassNo, Fullname">
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
                            <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                        <ItemTemplate>
                                            <asp:Label ID="lblmrActivityHiredNo" runat="server"   Text='<%# Bind("mrActivityHiredNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                            <asp:BoundField DataField="Code" HeaderText="Transaction No." SortExpression="Code" Visible="false">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="15%" />
                            </asp:BoundField>  

                            <asp:BoundField DataField="MRCode" SortExpression="MRCode" HeaderText="MR No." >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PositionDesc" SortExpression="PositionDesc" HeaderText="Position Applied" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="History" SortExpression="Fullname">
                                <ItemTemplate >
                                    <asp:LinkButton runat="server" ID="lnkhistory" Text="Click here..." CommandArgument='<%# Eval("ID") & "|" & Eval("IsApplicant") & "|" & Eval("Fullname") %>' OnClick="lnkHistory_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="5%" />
                            </asp:TemplateField>      
                            <asp:TemplateField HeaderText="Name" SortExpression="Fullname">
                                <ItemTemplate >
                                    <asp:LinkButton runat="server" ID="lnk" Text='<%# Bind("Fullname") %>' CommandArgument='<%# Eval("ID") & "|" & Eval("IsApplicant") & "|" & Eval("Fullname") %>' OnClick="lnk_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="30%" />
                            </asp:TemplateField>  
                                    
                            <asp:BoundField DataField="ApplicantTypeDesc" SortExpression="ApplicantTypeDesc" HeaderText="Applicant Type" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="15%" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Offer Status" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" >
                                <ItemTemplate >      
                                    <asp:DropDownList  CssClass="form-control" ID="cboJobOfferStatNo" Enabled='<%# Bind("IsEnabled") %>'  Text='<%# Bind("JobOfferStatNo") %>' AppendDataBoundItems="True"  runat="server" DataSourceID="ObjectDataSource3" DataTextField="tDesc" DataValueField="tNo">
                                    </asp:DropDownList><asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="Lookup_JobOfferStat" TypeName="clsLookup">
                                    </asp:ObjectDataSource>                                       
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="20%"/>
                            </asp:TemplateField> 

                            <asp:TemplateField HeaderText="Pay Offer" >
                                <ItemTemplate >
                                    <asp:ImageButton ID="btnPreview" runat="server" SkinID="grdDetail"  OnClick="lnkView_Click" CausesValidation="false" />                                   
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                            </asp:TemplateField>
                                    
                            <asp:TemplateField HeaderText="Select"  >
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


        <div class="panel panel-default">
            <div class="panel-heading">
                    <h4 class="panel-title"><asp:Label ID="lblDetl" CssClass="lbltextsmall11-color"  runat="server" /></h4>
                         
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <mcn:DataPagerGridView ID="grdDetl" runat="server" SkinID="AllowPaging-No" AllowSorting="true" OnSorting="grdDetl_Sorting" OnPageIndexChanging="grdDetl_PageIndexChanging" DataKeyNames="MRHiredMassNo, MROfferNo" >
                                <Columns>
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEditDetl" runat="server" CausesValidation="false" CssClass="cancel" OnClick="lnkEditDetl_Click" SkinID="grdEditbtn" ToolTip="Click here to edit" CommandArgument='<%# Bind("MROfferNo") %>'  />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="code"  SortExpression="code" HeaderText="Detail No." Visible="false" >
                                        <HeaderStyle Width="10%"  HorizontalAlign  ="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                            
                                    <asp:BoundField DataField="PayIncomeTypeDesc"  SortExpression="PayIncomeTypeDesc" HeaderText="Income Type"    >
                                        <HeaderStyle Width="40%"  HorizontalAlign  ="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>                 
                                                          
                                    <asp:BoundField DataField="Amount"  SortExpression="Amount" HeaderText="Amount"    >
                                        <HeaderStyle Width="20%"  HorizontalAlign  ="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField> 

                                    <asp:TemplateField HeaderText="Select">
                                        <HeaderTemplate>
                                            Select<br />
                                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="txtIsSelectAll" onclick ="SelectAllCheckboxes(this);"  runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="txtIschk" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="4%" />
                                    </asp:TemplateField>

                                </Columns>
                            </mcn:DataPagerGridView>
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
 <uc1:apphistory runat="server" ID="apphistory1" />       
        
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
                <label class="col-md-4 control-label has-space">Applicant Type :</label>
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
                        <asp:TextBox ID="txtMROfferNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
                        
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Income Type :</label>
                <div class="col-md-7">
                        <asp:DropdownList ID="cboPayIncomeTypeNo"  runat="server" DataMember="EPayIncomeType" CssClass="required form-control">
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

                    
        </div>
        <div class="cf popupfooter">
            </div> 
    </fieldset>
</asp:Panel>
                    


</asp:Content>

