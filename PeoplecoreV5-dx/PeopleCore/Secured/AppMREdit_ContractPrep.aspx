<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AppMREdit_ContractPrep.aspx.vb" Inherits="Secured_AppMREdit_ContractPrep" Theme="PCoreStyle" %>
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
                                <mcn:DataPagerGridView ID="grdMain" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging">
                                    <Columns>                                                                        
                                    <asp:TemplateField ShowHeader="false">
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

                                    <%--<asp:BoundField DataField="ReviewStatDesc" HeaderText="Other Info." SortExpression="ReviewStatDesc" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:BoundField>--%>

                                    <asp:BoundField DataField="HRANTypeDesc" HeaderText="HRAN Type" SortExpression="HRANTypeDesc" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="PlantillaCode" HeaderText="Plantilla No." SortExpression="PlantillaCode" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:BoundField>

                                    <%--<asp:TemplateField HeaderText="HRAN Type" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" >
                                        <ItemTemplate >      
                                            <asp:DropDownList  CssClass="form-control" ID="cboHRANTypeNo" runat="server" >
                                            </asp:DropDownList>                                    
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="12%"/>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Item No." HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" >
                                        <ItemTemplate >           
                                            <asp:DropdownList CssClass="form-control" ID="cboPlantillaNo" runat="server" />               
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="12%"/>
                                    </asp:TemplateField>--%>

                                    <asp:BoundField DataField="tStatus" SortExpression="tStatus" HeaderText="Status">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                    </asp:BoundField>

                                    <asp:TemplateField HeaderText="Select" >
                                        <HeaderTemplate>
                                            Select<br />
                                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="txtIsSelectAll" onclick ="SelectAllCheckboxes(this);"  runat="server" />
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
                                    <asp:Button ID="btnPost" runat="server" CausesValidation="false" cssClass="btn btn-primary" Text="Post to HRAN" OnClick="btnPost_Click"></asp:Button>                                
                                    <asp:Button ID="btnAdd" runat="server" CausesValidation="false" cssClass="btn btn-primary" Text="Add" OnClick="btnAdd_Click" Visible="false"></asp:Button>
                                    <asp:Button ID="btnDelete" runat="server" CausesValidation="false" cssClass="btn btn-default" Text="Delete" UseSubmitBehavior="false" OnClick="btnDelete_Click" Visible="false"/>
                                </div>
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="btnDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
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
        <asp:Panel id="Panel1" runat="server" CssClass="entryPopup2" style="display:none">
            <fieldset class="form" id="fsMain">                    
                <div class="cf popupheader">
                    <h4>&nbsp;Other Information</h4>
                    <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
                </div> 
                <br />  
                                                                     
                <div class="entryPopupDetl2 form-horizontal">     
                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">HRAN Type :</label>
                        <div class="col-md-7">
                        
                            <asp:DropDownList runat="server" ID="cboHRANTypeNo"  CssClass="form-control required">
                            </asp:DropDownList>
                        </div>
                    </div>         
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Plantilla No. :</label>
                        <div class="col-md-7">
                            <asp:HiddenField runat="server" ID="hifEmployeeNo" />
                            <asp:HiddenField runat="server" ID="hifMRPlantillaNo" />
                            <asp:HiddenField runat="server" ID="hifMRHiredMassNo" />
                            <asp:DropDownList runat="server" ID="cboPlantillaNo"  CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div> 
                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Payroll Group :</label>
                        <div class="col-md-7">
                            <asp:DropDownList runat="server" ID="cboPayClassNo" CssClass="form-control required">
                            </asp:DropDownList>
                        </div>
                    </div> 
                    
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">&nbsp;</label>
                        <div class="col-md-7">
                            <asp:CheckBox runat="server" ID="txtIsReviewed" Text="&nbsp;Tick here to confirm review." />
                        </div>
                    </div>    
                </div>
                <br /><br />                    
            </fieldset>
        </asp:Panel>
                    
        </Content>
    </uc:Tab>

</asp:Content>

