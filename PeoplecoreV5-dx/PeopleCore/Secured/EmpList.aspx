<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" EnableEventValidation="false" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpList.aspx.vb" Inherits="Secured_EmpList" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
<div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" ></asp:Dropdownlist>
                </div>
                <div>
                    <uc:Filter runat="server" ID="Filter1" EnableContent="true">
                        <Content>
                                <div class="form-group">
                                <label class="col-md-4 control-label">Filter By :</label>
                                <div class="col-md-8">
                                    <asp:DropDownList runat="server" ID="cbofilterby"  CssClass="form-control" />
                                    </div>
                                    <ajaxToolkit:CascadingDropDown ID="cdlfilterby" TargetControlID="cbofilterby" PromptValue="" ServicePath="~/asmx/WebService.asmx" ServiceMethod="GetFilterBy" runat="server" Category="tNo" LoadingText="Loading..." />
                                </div>
		                        <div class="form-group">
                                <label class="col-md-4 control-label">Filter Value :</label> 
                                <div class="col-md-8">
                                    <asp:DropDownList runat="server" ID="cbofiltervalue" CssClass="form-control" />
                                </div>
                                <ajaxToolkit:CascadingDropDown ID="cdlfiltervalue" TargetControlID="cbofiltervalue" PromptValue="" ServicePath="~/asmx/WebService.asmx" ServiceMethod="GetFilterValue" runat="server" Category="tNo" ParentControlID="cbofilterby" LoadingText="Loading..." PromptText="-- Select --" />
                                </div>
                        </Content>
                    </uc:Filter>
                </div>                           
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <mcn:DataPagerGridView ID="grdMain" runat="server" OnPageIndexChanging="grdMain_PageIndexChanging">
                        <Columns>                                                      
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkPhoto" ToolTip="Upload Photo" OnClick="lnkPhoto_Click" CommandArgument='<%# Bind("EmployeeNo") %>'>
                                        <asp:Image Width="70" Height="80" runat="server" ID="img" ImageUrl='<%# Eval("EmployeeNo", "frmShowImage.ashx?tNo={0}")+"&tIndex=2"%>' />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate >                                  
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text='<%# Bind("Fullname") %>' OnClick="lnkEdit_Click" CommandArgument='<%# Bind("EmployeeNo") %>' />
                                        <br />
                                        <asp:Label runat="server" ID="Label16" Text='<%# Bind("Col1") %>' />
                                    </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="25%"  />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="false" >
                                <ItemTemplate >                                        
                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("Col2") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="25%" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="false" >
                                <ItemTemplate >                                        
                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("Col3") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="25%" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="false" >
                                <ItemTemplate >                                        
                                    Attachment :&nbsp;&nbsp;&nbsp;<asp:LinkButton runat="server" ID="lnkAttachment" OnClick="lnkAttachment_Click" CssClass="fa fa-paperclip" CommandArgument='<%# Eval("EmployeeNo") & "|" & Eval("Fullname")  %>' /><br />
                                    Service Record :&nbsp;&nbsp;<asp:LinkButton runat="server" ID="LinkButton1" CssClass="fa fa-print" />
                                </ItemTemplate>                                
                                <HeaderStyle HorizontalAlign="Left" Width="25%" />
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
                        <!-- Button here -->
                        <div class="btn-group pull-right">
                            <asp:Button ID="btnAdd" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                        </div>                        
                    </div>
                </div> 
                      
            </div>                   
        </div>
</div>
</div>
<asp:UpdatePanel runat="server" ID="UpdatePanel2">
    <Triggers>
        <asp:PostBackTrigger ControlID="lnkSave" />
    </Triggers>
    <ContentTemplate>                            
        <asp:Button ID="Button1" runat="server" style="display:none" />
        <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel2" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
        <asp:Panel id="Panel2" runat="server" CssClass="entryPopup">
            <fieldset class="form" id="Fieldset1">                    
                <div class="cf popupheader">
                    <h4>&nbsp;</h4>
                    <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o" ToolTip="Save" />
                </div>                                            
                <div class="entryPopupDetl form-horizontal">                        
                    <div class="form-group">
                        <asp:HiddenField runat="server" ID="hifEmployeeNo" />
                        <label class="col-md-4 control-label">Filename :</label>
                        <div class="col-md-7">
                            <asp:FileUpload runat="server" ID="fuPhoto" Width="350" />
                        </div>
                    </div>                        
                </div>
                <br /><br />                    
            </fieldset>
        </asp:Panel> 
    </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>