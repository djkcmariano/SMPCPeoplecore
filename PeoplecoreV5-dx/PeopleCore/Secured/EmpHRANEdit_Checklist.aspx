<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpHRANEdit_Checklist.aspx.vb" Inherits="Secured_EmpHRANEdit_Checklist" Theme="PCoreStyle" %>
<%@ Register Src="~/Include/FileUpload.ascx" TagName="FileUpload" TagPrefix="uc" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
            <div style="display:none;">
                <asp:CheckBox ID="txtIsPosted" runat="server"></asp:CheckBox>
            </div>
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
                                
                        </div>                           
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <mcn:DataPagerGridView ID="grdMain" SkinID="AllowPaging-No" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging">
                                    <Columns>    
                                    <asp:TemplateField HeaderText="Id" Visible="False">
                            
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("tNo") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                                                                                    
                                    <asp:TemplateField ShowHeader="false" Visible="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" SkinID="grdEditbtn" CssClass="cancel" OnClick="btnEdit_Click" CommandArgument='<%# Bind("tno") %>' />                                           
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Code" HeaderText="Transaction No."  HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign ="LEFT" ItemStyle-HorizontalAlign="left" SortExpression="Code" />

                                    <asp:BoundField DataField="tDesc" HeaderText="Checklist" SortExpression="tDesc">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="50%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Download File" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" >
                                        <ItemTemplate >
                                            <asp:LinkButton ID="lnkDownload"  OnPreRender="addTrigger_PreRender" runat="server" ToolTip='<%# Bind("filepath") %>' Text="Click here..." OnClick="lnkDownload_Click">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="File Upload">
                                        <ItemTemplate>
                                            <asp:Linkbutton ID="btnUpload" runat="server" CausesValidation="false"  OnClick="btnEdit_Click" Text="Click here..."  Enabled='<%# Bind("IsEnabledx") %>'  CommandArgument='<%# Bind("tno") %>' />                                           
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <center>
                                                Submitted<br />
                                                <asp:CheckBox ID="txtIsSelectAll" onclick="SelectAllCheckboxes(this);" runat="server" />
                                            </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsSubmitted" Checked='<%# Bind("IsSubmitted") %>' runat="server" Enabled='<%# Bind("IsEnabled") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    </asp:TemplateField>

                                </Columns>    
                                </mcn:DataPagerGridView>                                
                            </div>
                        </div>                    
                        <div class="row">
                            <div class="col-md-4">                                
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
                                <div class="pull-right">
                                    <asp:Button ID="btnUpdate" runat="server" CausesValidation="false" cssClass="btn btn-primary" Text="Save" OnClick="btnUpdate_Click"></asp:Button>
                                    <asp:Button ID="btnAdd" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnAdd_Click" Visible="false" />
                                    <asp:Button ID="btnDelete" Text="Delete" runat="server" CausesValidation="false"  CssClass="btn btn-default" OnClick="btnDelete_Click" Visible="false" />                                    
                                </div>
                                <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="btnDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?" />
                            </div>
                        </div>                       
                    </div>                   
                </div>
            </div>
        </div>                                
    </Content>        
    </uc:Tab>
     <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <Triggers>
                <asp:PostBackTrigger ControlID="lnkSave" />
            </Triggers>
            <ContentTemplate>   
                <asp:Button ID="Button1" runat="server" style="display:none" />
                <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
                <asp:Panel id="Panel1" runat="server" CssClass="entryPopup">
                    <fieldset class="form" id="fsMain">                    
                        <div class="cf popupheader">
                            <h4>&nbsp;</h4>
                            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
                        </div>                                            
                        <div class="entryPopupDetl form-horizontal">    
             
                            <div class="form-group" style="display:none;">
                                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                                <div class="col-md-7">
                                    <asp:Textbox ID="txttno" ReadOnly="true" runat="server" CssClass="form-control" />
                                </div>
                            </div>      
                                   
                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                                <div class="col-md-7">
                                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                                </div>
                            </div>                        

                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">File :</label>
                                <div class="col-md-7">
                                    <asp:FileUpload runat="server" ID="fuDoc" Width="100%" CssClass="required" />
                                </div>
                            </div>  
                             

                            <br />
                        </div>                    
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

--%>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

    <uc:Tab runat="server" ID="Tab">
        <Header>               
            <asp:Label runat="server" ID="lbl" /> 
            <div style="display:none;">
                <asp:CheckBox ID="txtIsPosted" runat="server"></asp:CheckBox>
            </div>
        </Header>        
        <Content>                    
        <br />
        <div class="page-content-wrap" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-2">
                            &nbsp;
                        </div>
                        <div>                                                
                            <ul class="panel-controls">   
                                <li><asp:LinkButton runat="server" ID="lnkUpdate" OnClick="lnkUpdate_Click" Text="Save" CssClass="control-primary" /></li>                                   
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ul>                                                                                                                                                     
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="HRANChecklistNo" >                                                                                   
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No."  Width="5%" />
                                        <dx:GridViewDataTextColumn FieldName="tdesc" Caption="Checklist" />  
                                        <dx:GridViewDataCheckColumn FieldName="IsAttachedRequired" Caption="Required"  Width="2%" />    
                                        <%--<dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Download File" HeaderStyle-HorizontalAlign="Center"  Width="5%">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" OnPreRender="addTrigger_PreRender" ID="lnkDownload" CssClass="fa fa-download" Font-Size="Medium" OnClick="lnkDownload_Click" CommandArgument='<%# Bind("FilePath") %>' />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>--%>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Attachment" HeaderStyle-HorizontalAlign="Center"  Width="5%">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkUpload" CssClass="fa fa-paperclip" Font-Size="Medium"  OnClick="lnkEdit_Click" CommandArgument='<%# Eval("HRANChecklistNo") & "|" & Eval("Code") %>' />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>                                   
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Submitted?" Width="2%" />
                                    </Columns>                  
                                </dx:ASPxGridView>                                
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>
        </div>                            
    </Content>        
    </uc:Tab>
    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none;">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>
                        <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;
                        <asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkSave" />
                    </Triggers>
                </asp:UpdatePanel>    
            </div>                                            
            <div class="entryPopupDetl form-horizontal">    
             
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtHRANChecklistNo" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>      
                                   
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"/>
                    </div>
                </div>                        

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Checklist :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboApplicantStandardCheckListNo" CssClass="form-control required" DataMember="EHRANCheckListTypeL" runat="server"></asp:DropdownList>
                    </div>
                </div>  

                <div class="form-group">
                    <label class="col-md-4 control-label"></label>
                    <div class="col-md-7">              
                        <asp:CheckBox ID="txtIsAttachedRequired" runat="server" Text="&nbsp; Checklist is required." />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">File :</label>
                    <div class="col-md-7">
                        <asp:FileUpload runat="server" ID="fuDoc" Width="100%" CssClass="required" />
                    </div>
                </div>  

                <br />
            </div>                    
        </fieldset>
    </asp:Panel>
    <uc:FileUpload runat="server" ID="FileUpload" />
</asp:Content>