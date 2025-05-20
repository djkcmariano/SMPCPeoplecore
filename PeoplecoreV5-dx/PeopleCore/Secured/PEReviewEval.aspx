<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="PEReviewEval.aspx.vb" Inherits="Secured_PEReviewEval" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
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
                                
                        </div>                           
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <mcn:DataPagerGridView ID="grdMain" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging">
                                    <Columns>                                                                        
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" SkinID="grdEditbtn" CssClass="cancel" OnClick="btnEdit_Click" CommandArgument='<%# Bind("PEReviewEvalNo") %>' />                                           
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Code" HeaderText="Transaction No." SortExpression="EvalTemplateCateNo">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="PEevaluatorDesc" HeaderText="Evaluator" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                    </asp:BoundField>     
                                                                         
                                    <asp:BoundField DataField="FullName" HeaderText="Name of Evaluator" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                    </asp:BoundField>   
                                                                                                             
                                    <asp:BoundField DataField="Weighted" HeaderText="Weighted" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:BoundField>    

                                    <asp:BoundField DataField="OrderLevel" HeaderText="Order Level" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:BoundField>   
                                                                                       
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
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
                                    <asp:Button ID="btnAdd" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnAdd_Click" />
                                    <asp:Button ID="btnDelete" Text="Delete" runat="server" CausesValidation="false"  CssClass="btn btn-default" OnClick="btnDelete_Click" />                                    
                                </div>
                                <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="btnDelete" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?" />
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
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">    
             
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtPEReviewEvalNo" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>      
                                   
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCodeEval" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>                        

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Evaluator :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboPEEvaluatorNo" CssClass="form-control required" DataMember="EPEEvaluator" runat="server" AutoPostBack="true"></asp:DropdownList>
                    </div>
                </div>  

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Name of Evaluator :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboEvaluatorNo" CssClass="form-control" DataMember="EEmployeeL" runat="server"></asp:DropdownList>
                    </div>
                </div>  

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Weighted :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtWeighted" CssClass="form-control required" SkinID="txtdate"  runat="server"></asp:Textbox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" TargetControlID="txtWeighted" ValidChars=".-" />
                    </div>
                </div>  
                              
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Order Level :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtOrderLevel" CssClass="form-control" SkinID="txtdate"  runat="server"></asp:Textbox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" TargetControlID="txtOrderLevel" ValidChars="."  />                       
                    </div>
                </div>

                <br />
            </div>                    
        </fieldset>
    </asp:Panel>
</asp:Content>

