<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="AppMREdit_SelectionProcess.aspx.vb" Inherits="Secured_AppMREdit_SelectionProcess" Async="true" %>

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

    <%--function getMain3(source, eventArgs) {
        document.getElementById('<%= hifScreeningByNo2.ClientID %>').value = Split(eventArgs.get_value(), 0);
    }--%>

    function getDetl(source, eventArgs) {
        document.getElementById('<%= hidID.ClientID %>').value = Split(eventArgs.get_value(), 0);
    }

    function getSched(source, eventArgs) {
        document.getElementById('<%= hifInterviewByNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
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

    function ResetInt() {
        if (document.getElementById('<%= txtInterviewByName.ClientID %>').value == "") {
            document.getElementById('<%= hifInterviewByNo.ClientID %>').value = "0";
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
            
            <%--Detail View--%>
 
              <div class="page-content-wrap">
                  <div class="row">
                      <div class="panel panel-default">
                          <div class="panel-heading">
                              <div class="col-md-6">
                                  <h4 class="panel-title">
                                      <asp:Label ID="lblDetl" CssClass="lbltextsmall11-color" runat="server" /></h4>
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
                                          DataKeyNames="MRInterviewNo, MRInterviewDetiNo, ApplicantStandardMainNo">
                                          <Columns>
                                              <asp:TemplateField ShowHeader="false" Visible="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEditDetl" runat="server" CausesValidation="false" CssClass="cancel" OnClick="lnkEditDetl_Click" SkinID="grdEditbtn" ToolTip="Click here to edit" CommandArgument='<%# Bind("MRInterviewDetiNo") %>'  />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Id" Visible="False">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblInterviewDetiNo" runat="server" Text='<%# Bind("MRInterviewDetiNo") %>'></asp:Label>
                                                  </ItemTemplate>
                                                  <ItemStyle HorizontalAlign="Left" />
                                                  <HeaderStyle HorizontalAlign="Left" />
                                              </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Id" Visible="False">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblNo" runat="server" Text='<%# Bind("MRHiredMassNo") %>'></asp:Label>
                                                      <asp:Label ID="lblOverride" runat="server" Text='<%# Bind("IsOverride") %>'></asp:Label>
                                                  </ItemTemplate>
                                                  <ItemStyle HorizontalAlign="Left" />
                                                  <HeaderStyle HorizontalAlign="Left" />
                                              </asp:TemplateField>

                                              <asp:BoundField DataField="Code" SortExpression="Code" HeaderText="ID No." Visible="false">
                                                  <ItemStyle HorizontalAlign="Left" />
                                                  <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                              </asp:BoundField>

                                              <asp:TemplateField HeaderText="" SortExpression="Fullname" HeaderStyle-VerticalAlign="Middle">
                                                  <ItemTemplate>
                                                      <div class="row" style="padding-left: 10px;">
                                                          <asp:LinkButton runat="server" ID="lnk" Text='<%# Bind("Fullname") %>' CommandArgument='<%# Eval("ID") & "|" & Eval("IsApplicant") & "|" & Eval("Fullname") %>' OnClick="lnk_Click" />
                                                      </div>
                                                      <br />
                                                      <div class="row" style="position: inherit; bottom: 0px; display: none;">
                                                          <div class="col-md-12" style="padding-left: 0px;">
                                                              <asp:LinkButton runat="server" ID="lnkForm" CssClass="list-group-item btn-xs" OnClick="lnkForm_Click" Visible='<%# Bind("IsShowTemplate") %>'>
                                                                                <i class="fa fa-star-o fa-fw"></i>Exam<span class="pull-right text-muted"><em></em></span>
                                                              </asp:LinkButton>
                                                              <asp:LinkButton runat="server" ID="lnkComments" CssClass="list-group-item btn-xs" Visible='<%# Bind("IsShowComments") %>'>
                                                                                <i class="fa fa-comment-o fa-fw"></i> Comments<span class="pull-right text-muted"><em></em></span>
                                                              </asp:LinkButton>
                                                          </div>
                                                      </div>
                                                  </ItemTemplate>
                                                  <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                  <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20%" />
                                              </asp:TemplateField>

                                              <asp:TemplateField HeaderText="" SortExpression="Schedule">
                                                  <ItemTemplate>
                                                    <div class="row local_dl" style="display: none;">
                                                        <asp:Label CssClass="local_dd" ID="lblScheduleNo" runat="server" Text='<%# Bind("MRScheduleNo") %>' />
                                                    </div>
                                                      <div class="row local_dl">
                                                          <label class="local_dt">Screening Schedule</label><br />
                                                          <asp:Label CssClass="local_dd" ID="lblSchedule" runat="server" Text='<%# Bind("ScheduleStr") %>' />
                                                      </div>
                                                      <br />
                                                      <div class="row local_dl">
                                                          <label class="local_dt">Status</label><br />
                                                          <asp:Label CssClass="local_dd" ID="lblStatus" runat="server" Text='<%# Bind("MRScheduleStatDesc") %>' />
                                                      </div>
                                                      <br />
                                                      <div class="row local_menu">
                                                          <asp:LinkButton runat="server" ID="lnkAddSched" OnClick="lnkAddSched_Click" Visible='<%# Bind("IsShowSchedule") %>' CommandArgument='<%# Eval("MRInterviewDetiNo") & "|" & Eval("MRScheduleNo") & "|" & Eval("FullName") %>' ToolTip="Click here to set screening schedule"><i class="fa fa-calendar fa-fw"></i> Set Schedule</asp:LinkButton>
                                                          <asp:LinkButton runat="server" ID="lnkMove" OnClick="lnkMove_Click" Visible='<%# Bind("IsShowMove") %>' CommandArgument='<%# Eval("MRInterviewDetiNo") & "|" & Eval("MRScheduleNo") & "|" & Eval("FullName") %>' ToolTip="Click here to move schedule"><i class="fa fa-calendar fa-fw"></i> Move Schedule</asp:LinkButton>
                                                          <asp:LinkButton runat="server" ID="lnkHistory" OnClick="lnkHistory_Click" Text="Sent" Visible='<%# Bind("IsShowHistory") %>' CommandArgument='<%# Eval("MRInterviewDetiNo") & "|" & Eval("MRScheduleNo") & "|" & Eval("FullName") %>' ToolTip="Click here to view the sent schedules" />
                                                          <asp:LinkButton runat="server" ID="lnkAccept" OnClick="lnkAccept_Click" Text="Accept" Visible='<%# Bind("IsShowAccept") %>' CommandArgument='<%# Eval("MRInterviewDetiNo") & "|" & Eval("MRScheduleNo") & "|" & Eval("FullName") %>' ToolTip="Click here to accept schedule" />
                                                          <asp:LinkButton runat="server" ID="lnkDecline" OnClick="lnkDecline_Click" Text="Decline" Visible='<%# Bind("IsShowDecline") %>' CommandArgument='<%# Eval("MRInterviewDetiNo") & "|" & Eval("MRScheduleNo") & "|" & Eval("FullName") %>' ToolTip="Click here to decline schedule" />
                                                          <asp:LinkButton runat="server" ID="lnkCancel" OnClick="lnkCancel_Click" Text="Cancel" Visible='<%# Bind("IsShowCancel") %>' CommandArgument='<%# Eval("MRInterviewDetiNo") & "|" & Eval("MRScheduleNo") & "|" & Eval("FullName") %>' ToolTip="Click here to cancel schedule" />
                                                      </div>
                                                      <%--<uc:ConfirmBox ID="cfAccept" runat="server" ConfirmMessage="Are you sure you want to proceed?" TargetControlID="lnkAccept" />--%>
                                                  </ItemTemplate>
                                                  <ItemStyle HorizontalAlign="Left" />
                                                  <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                              </asp:TemplateField>

                                              <asp:TemplateField HeaderText="">
                                                  <ItemTemplate>
                                                      <div class="form-horizontal">
                                                          <div class="form-group local_margin">
                                                              <label class="col-md-5 control-label has-space">Screener Name</label>
                                                              <div class="col-md-7">
                                                                  <label style="font-weight: normal; padding-top: 5px;">
                                                                      <asp:LinkButton ID="lnkScreener" OnClick="lnkScreener_Click" runat="server" CommandArgument='<%# Eval("MRInterviewDetiNo") & "|" & Eval("FullName") %>' Visible='<%# Bind("IsShowScreener") %>'><i class="fa fa-pencil fa-fw"></i></asp:LinkButton>
                                                                      <asp:Label ID="lblName" runat="server" Text='<%# Bind("InterviewByName") %>' />&nbsp;
                                                                  </label>
                                                              </div>
                                                          </div>
                                                          <div class="form-group local_margin">
                                                              <label class="col-md-5 control-label has-space">Screening Result</label>
                                                              <div class="col-md-7">
                                                                  <asp:DropDownList CssClass="form-control" ID="cboInterviewStatNo" runat="server" OnSelectedIndexChanged="cboInterviewStatNo_SelectedIndexChanged">
                                                                  </asp:DropDownList>
                                                              </div>
                                                          </div>

                                                          <div class="form-group local_margin">
                                                              <label class="col-md-5 control-label has-space">Remarks</label>
                                                              <div class="col-md-7">
                                                                  <asp:TextBox CssClass="form-control" ID="txtRemarks" runat="server" Enabled='<%# Bind("IsEnabled") %>' Text='<%# Bind("Remarks") %>' TextMode="MultiLine" Rows="2" />
                                                              </div>
                                                          </div>
                                                          <div class="form-group local_margin">
                                                              <label class="col-md-5 control-label has-space">Date Served</label>
                                                              <div class="col-md-7">
                                                                  <label style="font-weight: normal; padding-top: 5px;">
                                                                      <asp:Label ID="lblDateServed" runat="server" Text='<%# Bind("DateServed") %>' />&nbsp;
                                                                  </label>
                                                              </div>
                                                          </div>
                                                          <div class="form-group local_margin" style="display: none;">
                                                              <label class="col-md-5 control-label has-space">Action</label>
                                                              <div class="col-md-7">
                                                                  <asp:DropDownList CssClass="form-control" ID="cboActionStatNo" runat="server" />
                                                                  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="*" ForeColor="Red" ControlToValidate="cboActionStatNo" ValidationGroup="action" InitialValue="" Display="Dynamic" />
                                                              </div>
                                                          </div>
                                                      </div>
                                                  </ItemTemplate>
                                                  <ItemStyle HorizontalAlign="Left" />
                                                  <HeaderStyle HorizontalAlign="Center" Width="40%" />
                                              </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Select">
                                                  <HeaderTemplate>
                                                      <center>
                                                          Select
                                                                <asp:CheckBox ID="txtIsSelectAll" onclick="SelectAllCheckboxes(this);" runat="server" />
                                                      </center>
                                                  </HeaderTemplate>
                                                  <ItemTemplate>
                                                      <asp:CheckBox ID="txtIsSelect" Enabled='<%# Bind("IsEnabled") %>' runat="server" />
                                                  </ItemTemplate>
                                                  <ItemStyle HorizontalAlign="Center" />
                                                  <HeaderStyle HorizontalAlign="Center" Width="2%" />
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
                                                  <PagerTemplate>
                                                      Page
                                                                <asp:Label ID="CurrentPageLabel" runat="server" Text="<%# IIf(Container.TotalRowCount>0,  (Container.StartRowIndex / Container.PageSize) + 1 , 0) %>" />
                                                      of
                                                                <asp:Label ID="TotalPagesLabel" runat="server" Text="<%# Math.Ceiling (System.Convert.ToDouble(Container.TotalRowCount) / Container.PageSize) %>" />
                                                      (
                                                                <asp:Label ID="TotalItemsLabel" runat="server" Text="<%# Container.TotalRowCount%>" />
                                                      records )
                                                  </PagerTemplate>
                                              </asp:TemplatePagerField>
                                              <asp:NextPreviousPagerField ButtonType="Image" LastPageImageUrl="~/images/arrow_last.png" NextPageImageUrl="~/images/arrow_next.png" ShowFirstPageButton="false" ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" />
                                          </Fields>
                                      </asp:DataPager>
                                  </div>
                                  <div class="col-md-6 col-md-offset-2">
                                      <!-- Button here btn-group -->
                                      <div class="pull-right">
                                          <asp:Button ID="btnUpdateDetl" runat="server" CausesValidation="false" CssClass="btn btn-primary" Text="Save" OnClick="btnUpdateDetl_Click"></asp:Button>
                                          <asp:Button ID="btnAddDetl" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnAddDetl_Click" ToolTip="Click here to add"></asp:Button>
                                          <asp:Button ID="btnDeleteDetl" Text="Delete" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnDeleteDetl_Click" ToolTip="Click here to delete"></asp:Button>
                                      </div>
                                      <uc:ConfirmBox ID="ConfirmBox2" runat="server" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?" TargetControlID="btnDeleteDetl" />
                                  </div>
                              </div>
                          </div>
                      </div>
                  </div>
              </div>

            <%--End Detail View--%>

             <uc:Info runat="server" ID="Info1" />
             <uc:History runat="server" ID="History" />

            <%--Main Form--%>

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
                        <%--<div class="form-group">
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
                        </div>--%>                        
                    </div>
                    <div class="cf popupfooter">
                     </div> 
                </fieldset>
            </asp:Panel>

            <%--End Main Form--%>


            <%--Detl Form--%>

            <asp:Button ID="btnShowDetl" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl" CancelControlID="lnkCloseDetl" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
            <asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup2" style="display:none">
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

            <%--End Detl Form--%>


            <%--Add Schedule--%>

            <asp:Button ID="btnShowSched" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="mdlSched" runat="server" TargetControlID="btnShowSched" PopupControlID="pnlPopupSched" CancelControlID="lnkCloseSched" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
            <asp:Panel id="pnlPopupSched" runat="server" CssClass="entryPopup2" style="display:none">
                <fieldset class="form" id="fsSched">
                    <div class="cf popupheader">
                        <h4><asp:Label ID="lblTitleSched" runat="server" /> &nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="lnkCloseSched" CssClass="fa fa-times" ToolTip="Close" />
                    </div>
                    <div  class="entryPopupDetl2 form-horizontal">
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                    <asp:TextBox ID="txtMRScheduleNo"  runat="server" CssClass="form-control" Enabled="false" ReadOnly="true" ></asp:TextBox>
                            </div>
                        </div>
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
                        <div class="form-group" style="position:absolute;visibility:hidden;">
                            <label class="col-md-4 control-label"></label>
                            <div class="col-md-7">
                                <asp:Checkbox ID="txtIsSchedule" runat="server" Text="&nbsp;Please check here to define screening schedule." OnCheckedChanged="txtIsScreening_CheckedChanged" AutoPostBack="true"></asp:Checkbox>
                            </div>
                        </div> 
                       <div class="form-group">
                            <label class="col-md-4 control-label has-required">Screening Date :</label>
                            <div class="col-md-6 col-no-padding">
                                <div class="col-md-4">
                                    <asp:Textbox ID="txtScheduleDateFrom" runat="server" CssClass="form-control required" placeholder="From"></asp:Textbox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtScheduleDateFrom" Format="MM/dd/yyyy" />                   
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="txtScheduleDateFrom" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />            
                                    <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtScheduleDateFrom" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender5" TargetControlID="RangeValidator5" /> 
                     
                                </div>
                                <div class="col-md-4 col-md-offset-1">
                                    <asp:Textbox ID="txtScheduleDateTo" runat="server" CssClass="form-control required" placeholder="To"></asp:Textbox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtScheduleDateTo" Format="MM/dd/yyyy" />                   
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtScheduleDateTo" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />            
                                    <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtScheduleDateTo" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender3" TargetControlID="RangeValidator4" /> 
                                </div>                       
                            </div>
                        </div> 
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Screening Time :</label>
                            <div class="col-md-6 col-no-padding">
                                <div class="col-md-4">
                                    <asp:Textbox ID="txtScheduleTime" runat="server" CssClass="form-control required"></asp:Textbox>
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtScheduleTime" Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Time" AcceptAMPM="false" CultureName="en-US" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender5" ControlToValidate="txtScheduleTime" IsValidEmpty="true" EmptyValueMessage="Time is required" InvalidValueMessage="" ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="Input a time" />
                                </div>
                                <div class="col-md-4 col-md-offset-1">
                                    <asp:DropDownList runat="server" ID="cboPeriod" CssClass="form-control">
                                        <asp:ListItem Text="AM" Value="AM" />
                                        <asp:ListItem Text="PM" Value="PM" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Screening Venue :</label>
                            <div class="col-md-7">
                                    <asp:TextBox ID="txtScheduleVenue"  runat="server" CssClass="form-control required" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Remarks :</label>
                            <div class="col-md-7">
                                    <asp:TextBox ID="txtScheduleNotes"  runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space"></label>
                            <div class="col-md-7">
                                <asp:Button runat="server" ID="lnkSaveSched" OnClick="lnkSaveSched_Click" CssClass="btn btn-primary submit fsSched lnkSaveSched" CausesValidation="true" Text="Send" />
                            </div>
                        </div>           
                        <br />
                    </div>
                    <div class="cf popupfooter">
                    </div> 
                </fieldset>
            </asp:Panel>

       <%--End Add Schedule--%>


       <%-- Add Screener--%>

        <asp:Button ID="btnShowScreener" runat="server" style="display:none" />
        <ajaxtoolkit:ModalPopupExtender ID="mdlScreener" runat="server" TargetControlID="btnShowScreener" PopupControlID="pnlPopupScreener" CancelControlID="lnkCloseScreener" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
        <asp:Panel id="pnlPopupScreener" runat="server" CssClass="entryPopup2" style="display:none">
            <fieldset class="form" id="fsScreener">
                 <div class="cf popupheader">
                      <h4><asp:Label ID="lblTitleScreener" runat="server" /> &nbsp;</h4>
                      <asp:Linkbutton runat="server" ID="lnkCloseScreener" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveScreener" OnClick="lnkSaveScreener_Click" CssClass="fa fa-floppy-o submit fsScreener lnkSaveScreener" ToolTip="Save" />
                 </div>
                <div  class="entryPopupDetl2 form-horizontal"> 
            
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Name of Applicant :</label>
                        <div class="col-md-7">
                                <asp:TextBox ID="txtApplicantName2"  runat="server" CssClass="form-control" Enabled="false" ReadOnly="true" ></asp:TextBox>
                         </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Name of Screener :</label>
                        <div class="col-md-7">
                            <asp:TextBox runat="server" ID="txtInterviewByName" CssClass="form-control required" style="display:inline-block;" onblur="ResetInt()" Placeholder="Type here..." /> 
                            <asp:HiddenField runat="server" ID="hifInterviewByNo"/>
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"  
                            TargetControlID="txtInterviewByName" MinimumPrefixLength="2" 
                            CompletionInterval="500" ServiceMethod="PopulateEmployee" ServicePath="~/asmx/WebService.asmx"
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="1"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            OnClientItemSelected="getSched" FirstRowSelected="true" UseContextKey="true" />
                        </div>
                    </div>
                    <br /><br />
                </div>
                <div class="cf popupfooter">
                 </div> 
            </fieldset>
        </asp:Panel>

        <%-- End Add Screener--%>

        <%--Show Cancel--%>

         <asp:Button ID="btnShowCancel" runat="server" Style="display: none" />
             <ajaxToolkit:ModalPopupExtender ID="mdlCancel" runat="server" TargetControlID="btnShowCancel" PopupControlID="pnlPopupCancel" CancelControlID="lnkCloseCancel" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
             <asp:Panel ID="pnlPopupCancel" runat="server" CssClass="entryPopup2" Style="display: none">
                 <fieldset class="form" id="fsCancel">
                     <div class="cf popupheader">
                         <h4>
                             <asp:Label ID="lblTitleCancel" runat="server" />
                             &nbsp;</h4>
                         <asp:LinkButton runat="server" ID="lnkCloseCancel" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:LinkButton runat="server" ID="lnkSaveCancel" OnClick="lnkSaveCancel_Click" CssClass="fa fa-floppy-o submit fsCancel lnkSaveCancel" ToolTip="Save" />
                     </div>
                     <div class="entryPopupDetl2 form-horizontal">

                         <div class="row py-2">
                             <div class="col-md-4">
                                 <label class="control-label has-space">Name of Applicant :</label>
                             </div>
                             <div class="col-md-7">
                                 <asp:TextBox ID="txtApplicantName1" runat="server" CssClass="form-control" Enabled="false" ReadOnly="true"></asp:TextBox>
                             </div>
                         </div>
                         <div class="row py-2">
                             <div class="col-md-4">
                                 <label class="control-label has-space">
                                   <asp:Label ID="lblRemarks" runat="server" CssClass="control-label has-space"/>
                                 </label>
                             </div>
                             <div class="col-md-7">
                                 <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                     <div class="cf popupfooter"> </div>
                 </fieldset>
             </asp:Panel>
        <%--End Show Cancel--%>


        <%--Show History--%>

        <asp:Button ID="btnShowHistory" runat="server" style="display:none" />
           <ajaxToolkit:ModalPopupExtender ID="mdlHistory" runat="server" TargetControlID="btnShowHistory" PopupControlID="pnlPopupHistory" CancelControlID="lnkCloseHistory" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
           <asp:Panel ID="pnlPopupHistory" runat="server" CssClass="entryPopup2" Style="display: none">
               <fieldset class="form" id="fsHistory">
                   <div class="cf popupheader">
                       <h4>Sent Invites&nbsp;</h4>
                       <asp:LinkButton runat="server" ID="lnkCloseHistory" CssClass="fa fa-times" ToolTip="Close" />
                   </div>
                   <div class="container-fluid entryPopupDetl2">
                   <div class="page-content-wrap form-horizontal">
                       <div class="card mx-3">
                           <div class="card-header py-3 row">
                               <div class="row g-0">
                                   <div class="col-md-6 panel-title">
                                       <asp:Label ID="lblApplicantName" runat="server" />
                                   </div>
                               </div>
                           </div>
                           <br />
                           <div class="card-body">
                               <div class="table-responsive mx-3">
                                   <dx:ASPxGridView ID="grdSched" ClientInstanceName="grdSched" runat="server" Width="100%" KeyFieldName="MRScheduleNo">
                                       <Columns>
                                           <dx:GridViewDataTextColumn FieldName="EncodedBy" Caption="Sent By" PropertiesTextEdit-EncodeHtml="false" CellStyle-VerticalAlign="Top" />
                                           <dx:GridViewDataTextColumn FieldName="Schedule" Caption="Screening Schedule" PropertiesTextEdit-EncodeHtml="false" CellStyle-VerticalAlign="Top" />
                                           <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" PropertiesTextEdit-EncodeHtml="false" CellStyle-VerticalAlign="Top" />
                                           <dx:GridViewDataTextColumn FieldName="ReplyBy" Caption="Response By" PropertiesTextEdit-EncodeHtml="false" CellStyle-VerticalAlign="Top" />
                                           <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" Visible="false" />
                                       </Columns>
                                   </dx:ASPxGridView>
                               </div>
                           </div>
                       </div>
                   </div>
                   </div>
                   <div class="cf popupfooter"></div>
               </fieldset>
           </asp:Panel>

        <%--End Show History--%>

        </Content>
    </uc:Tab>    
</asp:Content>
