<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpStandardHeader_Cate.aspx.vb" Inherits="Secured_EmpStandardHeader_Cate" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">
        <Header>               
            <asp:Label runat="server" ID="lbl" /> 
        </Header>        
            <Content>            
        <br />
        <div class="page-content-wrap" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-2">

                        </div>
                        <div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>                    
                            <ul class="panel-controls">
                              <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" /></li>
                              <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>
                             <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                            </ul> 
                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
                        </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExport" />
                    </Triggers>
                    </asp:UpdatePanel> 
                </div>  
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="ApplicantStandardCateNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="ApplicantStandardCateCode" Caption="Code" />                                                                           
                                        <dx:GridViewDataTextColumn FieldName="ApplicantStandardCateDesc" Caption="Description" />                  
                                        <dx:GridViewDataComboBoxColumn FieldName="OrderBy" Caption="Order" />                                                                                                                                                     
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                    </Columns>                            
                                </dx:ASPxGridView>   
                                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdDetl" />                             
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>
        </div>            
    </Content>        
    </uc:Tab>


<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
BackgroundCssClass="modalBackground" CancelControlID="lnkClose" 
PopupControlID="Panel1" TargetControlID="Button1">
</ajaxToolkit:ModalPopupExtender>

<asp:Panel ID="Panel1" runat="server"  CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsd">
    <!-- Header here -->
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsd btnSaveDetl" OnClick="lnkSave_Click"  />
        </div>
        <!-- Body here -->
        <div  class="entryPopupDetl form-horizontal">

            <div class="form-group">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCode" runat="server" Enabled="false" CssClass="form-control" Placeholder="Autonumber" 
                        ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                    <label class="col-md-4 control-label has-required">Code :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtApplicantStandardCateCode" CssClass="form-control required" />
                    </div>
            </div>
            <div class="form-group">
                    <label class="col-md-4 control-label has-required">Description :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtApplicantStandardCateDesc" CssClass="form-control required" />
                    </div>
                </div>                
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Order :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtOrderBy" CssClass="form-control number" />                        
                    </div>
                </div>
                <br />
            </div>                    
        </fieldset>
    </asp:Panel>
</asp:Content>

