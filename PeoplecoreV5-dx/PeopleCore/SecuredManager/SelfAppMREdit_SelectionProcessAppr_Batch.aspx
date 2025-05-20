<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="SelfAppMREdit_SelectionProcessAppr_Batch.aspx.vb" Inherits="SecuredManager_SelfAppMREdit_SelectionProcessAppr_Batch" %>
<%@ Register Src="~/Include/Info.ascx" TagName="Info" TagPrefix="uc" %>

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

    
</script>

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
                            <mcn:DataPagerGridView ID="grdMain" SkinID="AllowPaging-No" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging" DataKeyNames="MRInterviewNo, Code" >
                                    <Columns>
                                        <%--<asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" CssClass="cancel" OnClick="lnkEdit_Click" SkinID="grdEditbtn" ToolTip="Click here to edit" CommandArgument='<%# Bind("MRInterviewNo") %>'  />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                        </asp:TemplateField>--%>
                                        
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

                                        <%--<asp:TemplateField HeaderText="Total Passed" SortExpression="TotalPassed">
                                            <ItemTemplate >
                                                <asp:LinkButton runat="server" ID="lblPassed" Text='<%# Bind("TotalPassed") %>' CssClass="badge badge-success" OnClick="lnkFilter3_Click" CommandArgument='<%# Bind("MRInterviewNo") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                        </asp:TemplateField>--%>
                          
                                        <asp:TemplateField HeaderText="Details" >
                                            <ItemTemplate >
                                                <asp:ImageButton ID="btnPreview" runat="server" SkinID="grdDetail"  OnClick="lnkView_Click" CausesValidation="false" />                                   
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
                                            
                            </div>
                                        
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
                            <mcn:DataPagerGridView ID="grdDetl" runat="server" AllowSorting="true" OnSorting="grdDetl_Sorting" OnPageIndexChanging="grdDetl_PageIndexChanging" DataKeyNames="MRInterviewNo, MRInterviewDetiNo, ApplicantStandardMainNo, ApplicantNo, EmployeeNo" >
                                    <Columns>
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
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                             
                                        <asp:BoundField DataField="MRCode" SortExpression="Code" HeaderText="MR No." >
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:BoundField>
                                                    
                                        <asp:BoundField DataField="PositionDesc" SortExpression="PositionDesc" HeaderText="Position" >
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
                                                <asp:DropdownList CssClass="form-control" ID="cboActionStatNo" runat="server"  />               
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="12%"/>
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" >
                                            <ItemTemplate >           
                                                <asp:TextBox runat="server" ID="txtRemarks" Enabled='<%# Bind("IsEnabled") %>' Text='<%# Bind("Remarks") %>' TextMode="MultiLine" Rows="3" /> 
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="12%"/>
                                        </asp:TemplateField>--%>

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
                        <div class="col-md-6 col-md-offset-2">
                            <!-- Button here btn-group -->
                            <div class="pull-right">
                                <asp:Button ID="btnUpdateDetl" runat="server" CausesValidation="false" cssClass="btn btn-primary" Text="Save" OnClick="btnUpdateDetl_Click"></asp:Button>                      
                            </div>
                        </div>
                    </div> 
                      
                </div>
                   
            </div>
        </div>
    </div>               
    <uc:Info runat="server" ID="Info1" />
</asp:Content>
