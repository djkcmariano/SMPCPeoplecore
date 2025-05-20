<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="AppMREdit_SelectionProcess.aspx.vb" Inherits="Secured_AppMREdit_SelectionProcess" %>

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
            if (elm[i].type == "checkbox" && elm[i].name.indexOf("txtIsSelect") > 0 &&
            elm[i].id != theBox.id) {
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
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

    function SetContextKey() {
        var e = document.getElementById('<%= cboHiringAlternativeNo.ClientID %>');
        var str = e.options[e.selectedIndex].value + '|' + document.getElementById('<%= hidTransNo.ClientID %>').value;
        $find('<%= AutoCompleteExtender1.ClientID %>').set_contextKey(str);
    }

    function getMain(source, eventArgs) {
        document.getElementById('<%= hifFacilitatorNo.ClientID %>').value = Split(eventArgs.get_value(), 0);        
    }

    function getMain2(source, eventArgs) {        
        document.getElementById('<%= hifScreeningByNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
    }

    function getMain3(source, eventArgs) {
        document.getElementById('<%= hifScreeningByNo2.ClientID %>').value = Split(eventArgs.get_value(), 0);
    }

    function getDetl(source, eventArgs) {
        document.getElementById('<%= hidID.ClientID %>').value = Split(eventArgs.get_value(), 0);
    }

    function getSched(source, eventArgs) {
        document.getElementById('<%= hifInterviewByNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
    }

    function getSched2(source, eventArgs) {
        document.getElementById('<%= hifInterviewByNo2.ClientID %>').value = Split(eventArgs.get_value(), 0);
    }

    function ResetFac() {
        if (document.getElementById('<%= txtFacilitatorName.ClientID %>').value == "") {
            document.getElementById('<%= hifFacilitatorNo.ClientID %>').value = "0";
        }
    }

    function ResetScre() {
        if (document.getElementById('<%= txtScreeningByName.ClientID %>').value == "") {
            document.getElementById('<%= hifScreeningByNo.ClientID %>').value = "0";
        }
    }

    function ResetScre() {
        if (document.getElementById('<%= txtScreeningByName2.ClientID %>').value == "") {
            document.getElementById('<%= hifScreeningByNo2.ClientID %>').value = "0";
        }
    }

    function ResetInt() {
        if (document.getElementById('<%= txtInterviewByName.ClientID %>').value == "") {
            document.getElementById('<%= hifInterviewByNo.ClientID %>').value = "0";
        }
    }

    function ResetInt2() {
        if (document.getElementById('<%= txtInterviewByName2.ClientID %>').value == "") {
            document.getElementById('<%= hifInterviewByNo2.ClientID %>').value = "0";
        }
    }

    function checkDate(sender, args) {
        if (sender._selectedDate < new Date()) {
            alert("You cannot select a day earlier than today!");
            sender._selectedDate = new Date();
            // set the date back to the current date
            sender._textbox.set_Value(sender._selectedDate.format(sender._format))
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
                                <div class="col-md-6">
                                    <h4 class="panel-title">&nbsp;</h4>
                                </div>
                                <div>

                                </div>                           
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                       <mcn:DataPagerGridView ID="grdMain" SkinID="AllowPaging-No" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging" DataKeyNames="MRInterviewNo, Code, ApplicantStandardMainDesc" >
                                                <Columns>
                                                    <asp:TemplateField ShowHeader="false">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" CssClass="cancel" OnClick="lnkEdit_Click" SkinID="grdEditbtn" ToolTip="Click here to edit" CommandArgument='<%# Bind("MRInterviewNo") %>'  />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                    </asp:TemplateField>
                                        
                                                    <asp:BoundField DataField="Code" HeaderText="Transaction No." SortExpression="Code" Visible="false">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                                    </asp:BoundField> 

                                                    <asp:BoundField DataField="OrderLevel" HeaderText="Step" SortExpression="OrderLevel">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="ApplicantStandardMainDesc" HeaderText="Screening Process" SortExpression="ApplicantStandardMainDesc">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                                    </asp:BoundField> 

                                                    <asp:BoundField DataField="DateFrom" HeaderText="Date From" SortExpression="DateFrom">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="DateTo" HeaderText="Date To" SortExpression="DateTo">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:BoundField> 
                                        
                                                    <asp:BoundField DataField="FacilitatorName" HeaderText="Facilitator" SortExpression="FacilitatorName">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                                    </asp:BoundField>  

                                                    <asp:TemplateField HeaderText="Total Candidates" SortExpression="TotalCandidates">
                                                        <ItemTemplate >
                                                            <asp:LinkButton runat="server" ID="lblCandidates" Text='<%# Bind("TotalCandidates") %>' CssClass="badge badge-info" OnClick="lnkFilter1_Click" CommandArgument='<%# Bind("MRInterviewNo") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                                    </asp:TemplateField>    
                                                                                                                                                            
                                                    <asp:TemplateField HeaderText="Total Pending" SortExpression="TotalPending">
                                                        <ItemTemplate >
                                                            <asp:LinkButton runat="server" ID="lblPending" Text='<%# Bind("TotalPending") %>' CssClass="badge badge-warning" OnClick="lnkFilter2_Click" CommandArgument='<%# Bind("MRInterviewNo") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total Passed" SortExpression="TotalPassed">
                                                        <ItemTemplate >
                                                            <asp:LinkButton runat="server" ID="lblPassed" Text='<%# Bind("TotalPassed") %>' CssClass="badge badge-success" OnClick="lnkFilter3_Click" CommandArgument='<%# Bind("MRInterviewNo") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total Failed" SortExpression="TotalFailed">
                                                        <ItemTemplate >
                                                            <asp:LinkButton runat="server" ID="lblFailed" Text='<%# Bind("TotalFailed") %>' CssClass="badge badge-danger" OnClick="lnkFilter4_Click" CommandArgument='<%# Bind("MRInterviewNo") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="No Show" SortExpression="TotalNoShow">
                                                        <ItemTemplate >
                                                            <asp:LinkButton runat="server" ID="lblNoShow" Text='<%# Bind("TotalNoShow") %>' CssClass="badge badge-primary" OnClick="lnkFilter5_Click" CommandArgument='<%# Bind("MRInterviewNo") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                                    </asp:TemplateField>
                          
                                                    <asp:TemplateField HeaderText="Details" >
                                                        <ItemTemplate >
                                                            <asp:ImageButton ID="btnPreview" runat="server" SkinID="grdDetail"  OnClick="lnkView_Click" CausesValidation="false" CommandArgument='<%# Bind("IsOverride") %>'  />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Select" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="txtIsSelect" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="4%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </mcn:DataPagerGridView>
                                    </div>
                                </div>
                    
                                <div class="row">
                                    <div class="col-md-4">
                                        <!-- Paging here -->
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
                                    <div class="col-md-6 col-md-offset-2">
                                        <!-- Button here btn-group -->
                                        <div class="pull-right" style="display:none;">
                                            
                                            <asp:Button ID="btnAdd" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnAdd_Click" ToolTip="Click here to add" ></asp:Button>
                                            <asp:Button ID="btnDelete" Text="Delete" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnDelete_Click" ToolTip="Click here to delete" ></asp:Button>                       
                                        </div>
                                        <uc:ConfirmBox ID="ConfirmBox1" runat="server" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?" TargetControlID="btnDelete" />
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
                                <div class="col-md-6">
                                    <h4 class="panel-title"><asp:Label ID="lblDetl" CssClass="lbltextsmall11-color"  runat="server" /></h4>
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
                                       <mcn:DataPagerGridView ID="grdDetl" runat="server" AllowSorting="true" OnSorting="grdDetl_Sorting" OnPageIndexChanging="grdDetl_PageIndexChanging" 
                                       DataKeyNames="MRInterviewNo, MRInterviewDetiNo, ApplicantStandardMainNo, ApplicantNo, EmployeeNo, EvalTemplateNo" >
                                                <Columns>
                                                    <asp:TemplateField ShowHeader="false">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEditDetl" runat="server" CausesValidation="false" CssClass="cancel" OnClick="lnkEditDetl_Click" SkinID="grdEditbtn" ToolTip="Click here to edit" CommandArgument='<%# Bind("MRInterviewDetiNo") %>' Enabled='<%# Bind("IsEnabled") %>'  />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInterviewDetiNo" runat="server"   Text='<%# Bind("MRInterviewDetiNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNo" runat="server"   Text='<%# Bind("MRHiredMassNo") %>'></asp:Label>
                                                            <asp:Label ID="lblOverride" runat="server" Text='<%# Bind("IsOverride") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                             
                                                    <asp:BoundField DataField="Code" SortExpression="Code" HeaderText="ID No." Visible="false" >
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:BoundField>      
                                                            
                                                    <asp:TemplateField HeaderText="Name" SortExpression="Fullname">
                                                        <ItemTemplate >
                                                            <asp:LinkButton runat="server" ID="lnk" Text='<%# Bind("Fullname") %>' CommandArgument='<%# Eval("ID") & "|" & Eval("IsApplicant") & "|" & Eval("Fullname") %>' OnClick="lnk_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Reviewed" SortExpression="Reviewed">
                                                        <ItemTemplate >
                                                            <asp:LinkButton runat="server" ID="lnkHistory" CssClass="fa fa-history" style="font-size:1.5em;" CommandArgument='<%# Eval("ID") & "|" & Eval("IsApplicant") & "|" & Eval("Fullname") %>' OnClick="lnkHistory_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="With Schedule"  >
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="txtIsSchedule" Checked='<%# Bind("IsSchedule") %>' Enabled="false" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    </asp:TemplateField>      
                                                                                                        
                                                    <asp:BoundField DataField="InterviewBy" SortExpression="InterviewBy" HeaderText="Screened By" >
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="ApplicantTypeDesc" SortExpression="ApplicantTypeDesc" HeaderText="Applicant Type" >
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:BoundField>

                                                    <asp:TemplateField HeaderText="Screening Answer Sheet" >
                                                        <ItemTemplate >                                        
                                                            <asp:LinkButton runat="server" ID="lnkForm" OnClick="lnkForm_Click" CssClass="fa fa-print text-info" style="font-size:1.5em;" ToolTip="Click here to view the screening." />
                                                        </ItemTemplate>                 
                                                        <ItemStyle HorizontalAlign="Left" />               
                                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                    </asp:TemplateField> 

                                                    <asp:TemplateField HeaderText="Screening Result" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" >
                                                        <ItemTemplate >      
                                                            <asp:DropDownList  CssClass="form-control" ID="cboInterviewStatNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboInterviewStatNo_SelectedIndexChanged" >
                                                            </asp:DropDownList>                                    
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="12%"/>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" >
                                                        <ItemTemplate >           
                                                            <asp:DropdownList CssClass="form-control" ID="cboActionStatNo" runat="server" />               
                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="*" ForeColor="Red" ControlToValidate="cboActionStatNo" ValidationGroup="action" InitialValue="" Display="Dynamic" />                                                            
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="12%"/>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" >
                                                        <ItemTemplate >           
                                                            <asp:TextBox runat="server" ID="txtRemarks" Enabled='<%# Bind("IsEnabled") %>' Text='<%# Bind("Remarks") %>' TextMode="MultiLine" Rows="3" /> 
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" Width="12%"/>
                                                    </asp:TemplateField> 

                                                    <asp:TemplateField HeaderText="Select"  >
                                                        <HeaderTemplate>
                                                            <center>
                                                            Select<br />
                                                            <asp:CheckBox ID="txtIsSelectAll" onclick ="SelectAllCheckboxes(this);"  runat="server" />
                                                            </center>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="txtIsSelect" Enabled='<%# Bind("IsEnabled") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
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
                                    <div class="col-md-8">
                                        <!-- Button here btn-group -->
                                        <div class="pull-right">
                                            <asp:Button ID="btnBatch2" runat="server" CausesValidation="false" cssClass="btn btn-primary" Text="Batch Schedule" OnClick="btnBatch2_Click"></asp:Button>
                                            <asp:Button ID="btnBatch" runat="server" CausesValidation="false" cssClass="btn btn-primary" Text="Batch Action" OnClick="btnBatch_Click"></asp:Button>
                                            <asp:Button ID="btnUpdateDetl" runat="server" CausesValidation="false" cssClass="btn btn-primary" Text="Save" OnClick="btnUpdateDetl_Click" ValidationGroup="action"></asp:Button>
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
             <uc:History runat="server" ID="History" />

            <asp:Button ID="btnShowMain" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
            <asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup2">
                <fieldset class="form" id="fsMain">
                     <div class="cf popupheader">
                          <h4>&nbsp;</h4>
                          <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
                     </div>
                    <div  class="entryPopupDetl2 form-horizontal">

                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                    <asp:TextBox ID="txtMRInterviewNo"  runat="server" Enabled="false" ReadOnly="true"></asp:TextBox>
                             </div>
                        </div>

                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-7">
                                    <asp:TextBox ID="txtCode"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                             </div>
                        </div>

                                                
                       <div class="form-group">
                            <label class="col-md-4 control-label has-space">Facilitator :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtFacilitatorName" CssClass="form-control" style="display:inline-block;" onblur="ResetFac()" /> 
                                <asp:HiddenField runat="server" ID="hifFacilitatorNo"/>
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"  
                                TargetControlID="txtFacilitatorName" MinimumPrefixLength="2" 
                                CompletionInterval="500" ServiceMethod="PopulateEmployee" ServicePath="~/asmx/WebService.asmx"
                                CompletionListCssClass="autocomplete_completionListElement" 
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                OnClientItemSelected="getMain" FirstRowSelected="true" UseContextKey="true" />
                            
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Screening Date :</label>
                            <div class="col-md-7 col-no-padding">
                                <div class="col-md-3">
                                    <asp:Textbox ID="txtScreeningDateFrom" runat="server" CssClass="form-control" placeholder="From"></asp:Textbox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtScreeningDateFrom" Format="MM/dd/yyyy" />                   
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtScreeningDateFrom" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />            
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtScreeningDateFrom" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator2" /> 
                                
                                </div>
                                <div class="col-md-3 col-md-offset-3">
                                    <asp:Textbox ID="txtScreeningDateTo" runat="server" CssClass="form-control" placeholder="To"></asp:Textbox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtScreeningDateTo" Format="MM/dd/yyyy" />                   
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtScreeningDateTo" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />            
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtScreeningDateTo" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender4" TargetControlID="RangeValidator1" /> 
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Screening Time :</label>
                            <div class="col-md-2">
                                <asp:Textbox ID="txtScreeningTime" runat="server" CssClass="form-control"></asp:Textbox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtScreeningTime" Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Time" AcceptAMPM="false" CultureName="en-US" />
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender3" ControlToValidate="txtScheduleTime" IsValidEmpty="true" EmptyValueMessage="Time is required" InvalidValueMessage="" ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="Input a time" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Screening Venue :</label>
                            <div class="col-md-7">
                                    <asp:TextBox ID="txtScreeningVenue"  runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                             </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Name of Screener :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtScreeningByName" CssClass="form-control" style="display:inline-block;" onblur="ResetScre()" /> 
                                <asp:HiddenField runat="server" ID="hifScreeningByNo"/>
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"  
                                TargetControlID="txtScreeningByName" MinimumPrefixLength="2" 
                                CompletionInterval="500" ServiceMethod="PopulateEmployee" ServicePath="~/asmx/WebService.asmx"
                                CompletionListCssClass="autocomplete_completionListElement" 
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                OnClientItemSelected="getMain2" FirstRowSelected="true" UseContextKey="true" />
                                
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Name of Screener :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtScreeningByName2" CssClass="form-control" style="display:inline-block;" onblur="ResetScre()" /> 
                                <asp:HiddenField runat="server" ID="hifScreeningByNo2"/>
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"  
                                TargetControlID="txtScreeningByName2" MinimumPrefixLength="2" 
                                CompletionInterval="500" ServiceMethod="PopulateEmployee" ServicePath="~/asmx/WebService.asmx"
                                CompletionListCssClass="autocomplete_completionListElement" 
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                OnClientItemSelected="getMain3" FirstRowSelected="true" UseContextKey="true" />
                                
                            </div>
                        </div>
                                                        
                    </div>
                    <div class="cf popupfooter">
                     </div> 
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

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Applicant Type :</label>
                            <div class="col-md-7">
                                <asp:DropDownList runat="server" ID="cboHiringAlternativeNo" CssClass="form-control required" onchange="SetContextKey()">
                                    <asp:ListItem Text="External Applicant" Value="1" Selected="True" />
                                    <asp:ListItem Text="Internal Applicant" Value="0" />
                                </asp:DropDownList>
                                <asp:HiddenField runat="server" ID="hidTransNo" />
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
                                CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="1"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                OnClientItemSelected="getDetl" FirstRowSelected="true" UseContextKey="true" />
                           

                            </div> 
                        </div>   

                    
                    </div>
                    <div class="cf popupfooter">
                     </div> 
                </fieldset>
            </asp:Panel>


            <asp:Button ID="btnShowSched" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="mdlSched" runat="server" TargetControlID="btnShowSched" PopupControlID="pnlPopupSched" CancelControlID="lnkCloseSched" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
            <asp:Panel id="pnlPopupSched" runat="server" CssClass="entryPopup2">
                <fieldset class="form" id="fsSched">
                     <div class="cf popupheader">
                          <h4>&nbsp;</h4>
                          <asp:Linkbutton runat="server" ID="lnkCloseSched" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveSched" OnClick="lnkSaveSched_Click" CssClass="fa fa-floppy-o submit fsSched lnkSaveSched" ToolTip="Save" />
                     </div>
                    <div  class="entryPopupDetl2 form-horizontal">

                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                    <asp:TextBox ID="txtMRInterviewDetiNo"  runat="server" CssClass="form-control" Enabled="false" ReadOnly="true" ></asp:TextBox>
                             </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Name of Applicant :</label>
                            <div class="col-md-7">
                                    <asp:TextBox ID="txtApplicantName"  runat="server" CssClass="form-control" Enabled="false" ReadOnly="true" ></asp:TextBox>
                             </div>
                        </div>

                        <div class="form-group" >
                            <label class="col-md-4 control-label"></label>
                            <div class="col-md-7">
                                <asp:Checkbox ID="txtIsSchedule" runat="server" Text="&nbsp;Please check here to define screening schedule." OnCheckedChanged="txtIsScreening_CheckedChanged" AutoPostBack="true"></asp:Checkbox>
                            </div>
                        </div> 

                       <div class="form-group">
                            <label class="col-md-4 control-label has-space">Screening Date :</label>
                            <div class="col-md-7 col-no-padding">
                                <div class="col-md-3">
                                    <asp:Textbox ID="txtScheduleDateFrom" runat="server" CssClass="form-control" placeholder="From"></asp:Textbox>
                                    <ajaxToolkit:CalendarExtender ID="ceDateFrom" runat="server" TargetControlID="txtScheduleDateFrom" Format="MM/dd/yyyy" />                   
                                    <ajaxToolkit:MaskedEditExtender ID="meDateFrom" runat="server" TargetControlID="txtScheduleDateFrom" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />            
                                    <asp:RangeValidator ID="rvDateFrom" runat="server" ControlToValidate="txtScheduleDateFrom" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="vcDateFrom" TargetControlID="rvDateFrom" /> 
                                
                                </div>
                                <div class="col-md-3 col-md-offset-3">
                                    <asp:Textbox ID="txtScheduleDateTo" runat="server" CssClass="form-control" placeholder="To"></asp:Textbox>
                                    <ajaxToolkit:CalendarExtender ID="ceDateTo" runat="server" TargetControlID="txtScheduleDateTo" Format="MM/dd/yyyy" />                   
                                    <ajaxToolkit:MaskedEditExtender ID="meDateTo" runat="server" TargetControlID="txtScheduleDateTo" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />            
                                    <asp:RangeValidator ID="rvDateTo" runat="server" ControlToValidate="txtScheduleDateTo" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="vcDateTo" TargetControlID="rvDateTo" /> 
                                
                                </div>                       
                            </div>
                        </div> 

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Screening Time :</label>
                            <div class="col-md-2">
                                <asp:Textbox ID="txtScheduleTime" runat="server" CssClass="form-control"></asp:Textbox>
                                <ajaxToolkit:MaskedEditExtender ID="meeTime" runat="server" TargetControlID="txtScheduleTime" Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Time" AcceptAMPM="false" CultureName="en-US" />
                                <ajaxToolkit:MaskedEditValidator ID="mevTime" runat="server" ControlExtender="MaskedEditExtender5" ControlToValidate="txtScheduleTime" IsValidEmpty="true" EmptyValueMessage="Time is required" InvalidValueMessage="" ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="Input a time" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Screening Venue :</label>
                            <div class="col-md-7">
                                    <asp:TextBox ID="txtScheduleVenue"  runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                             </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Name of Screener :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtInterviewByName" CssClass="form-control" style="display:inline-block;" onblur="ResetInt()" /> 
                                <asp:HiddenField runat="server" ID="hifInterviewByNo"/>
                                <ajaxToolkit:AutoCompleteExtender ID="acInterviewByName" runat="server"  
                                TargetControlID="txtInterviewByName" MinimumPrefixLength="2" 
                                CompletionInterval="500" ServiceMethod="PopulateEmployee" ServicePath="~/asmx/WebService.asmx"
                                CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="1"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                OnClientItemSelected="getSched" FirstRowSelected="true" UseContextKey="true" />
                                <script type="text/javascript">

                                    function ResetHead(source, eventArgs) {
                                        if (document.getElementById('<%= txtInterviewByName.ClientID %>').value == "") {
                                            document.getElementById('<%= hifInterviewByNo.ClientID %>').value = "0";
                                        }
                                    }

                                </script>
                            </div>
                        </div>
                                                                                     
                    
                    </div>
                    <div class="cf popupfooter">
                     </div> 
                </fieldset>
            </asp:Panel>

            <!-- Add main -->
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
                            <label class="col-md-4 control-label has-required">Screening Result :</label>
                            <div class="col-md-7">
                                <asp:DropDownList  CssClass="form-control" ID="cboInterviewStatNo" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                                                
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Action :</label>
                            <div class="col-md-7">
                                <asp:DropDownList  CssClass="form-control" ID="cboActionStatNo" runat="server" >
                                </asp:DropDownList>
                            </div>
                        </div>                                        
                     
                    </div>
                    <br /><br />                    
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button1" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose3" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
            <asp:Panel id="Panel1" runat="server" CssClass="entryPopup2">
                <fieldset class="form" id="Fieldset2">
                     <div class="cf popupheader">
                          <h4>&nbsp;</h4>
                          <asp:Linkbutton runat="server" ID="lnkClose3" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave3" OnClick="lnkSave3_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
                     </div>
                    <div  class="entryPopupDetl2 form-horizontal">
         
                       <div class="form-group">
                            <label class="col-md-4 control-label has-space">Screening Date :</label>
                            <div class="col-md-7 col-no-padding">
                                <div class="col-md-3">
                                    <asp:Textbox ID="txtScheduleDateFrom2" runat="server" CssClass="form-control" placeholder="From"></asp:Textbox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtScheduleDateFrom2" Format="MM/dd/yyyy" />                   
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender7" runat="server" TargetControlID="txtScheduleDateFrom2" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />            
                                    <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="txtScheduleDateFrom2" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender5" TargetControlID="RangeValidator6" /> 
                                
                                </div>
                                <div class="col-md-3 col-md-offset-3">
                                    <asp:Textbox ID="txtScheduleDateTo2" runat="server" CssClass="form-control" placeholder="To"></asp:Textbox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtScheduleDateTo2" Format="MM/dd/yyyy" />                   
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtScheduleDateTo2" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />            
                                    <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtScheduleDateTo2" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender3" TargetControlID="RangeValidator4" /> 
                                
                                </div>                       
                            </div>
                        </div> 

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Screening Time :</label>
                            <div class="col-md-2">
                                <asp:Textbox ID="txtScheduleTime2" runat="server" CssClass="form-control"></asp:Textbox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtScheduleTime2" Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Time" AcceptAMPM="false" CultureName="en-US" />
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender5" ControlToValidate="txtScheduleTime" IsValidEmpty="true" EmptyValueMessage="Time is required" InvalidValueMessage="" ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="Input a time" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Screening Venue :</label>
                            <div class="col-md-7">
                                    <asp:TextBox ID="txtScheduleVenue2"  runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                             </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Name of Screener :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtInterviewByName2" CssClass="form-control" style="display:inline-block;" onblur="ResetInt2()" /> 
                                <asp:HiddenField runat="server" ID="hifInterviewByNo2"/>
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"  
                                TargetControlID="txtInterviewByName2" MinimumPrefixLength="2" 
                                CompletionInterval="500" ServiceMethod="PopulateEmployee" ServicePath="~/asmx/WebService.asmx"
                                CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="1"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                OnClientItemSelected="getSched2" FirstRowSelected="true" UseContextKey="true" />
                            </div>
                        </div>
                       
                    </div>
                    <div class="cf popupfooter">
                     </div> 
                </fieldset>
            </asp:Panel>


        </Content>
    </uc:Tab>    
</asp:Content>
