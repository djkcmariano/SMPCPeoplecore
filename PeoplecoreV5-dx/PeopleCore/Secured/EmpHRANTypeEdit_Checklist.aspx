<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpHRANTypeEdit_Checklist.aspx.vb" Inherits="Secured_EmpHRANTypeEdit_Checklist" Theme="PCoreStyle" %>

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
                            &nbsp;
                        </div>
                        <div>                                                
                            <ul class="panel-controls">                                    
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ul>                                                                                                                                                     
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="tno">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                        <dx:GridViewDataTextColumn FieldName="tdesc" Caption="Checklist" />
                                        <dx:GridViewDataTextColumn FieldName="CheckListTypeCateDesc" Caption="Checklist Category" />
                                        <dx:GridViewDataTextColumn FieldName="SubmittedByDesc" Caption="Submitted By (Title)" />    
                                        <dx:GridViewDataCheckColumn FieldName="IsAttachedRequired" Caption="Document Required?"/>                                    
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" SelectAllCheckboxMode="Page" />
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
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                    </div>
                </div>                        

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Checklist :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboApplicantStandardCheckListNo" CssClass="form-control required" DataMember="EHRANCheckListTypeL" runat="server"></asp:DropdownList>
                    </div>
                </div>  

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Checklist Category :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCheckListTypeCateDesc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" ></asp:Textbox>
                    </div>
                </div> 

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Submitted By (Title) :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtSubmittedByDesc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" ></asp:Textbox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label"></label>
                    <div class="col-md-7">              
                        <asp:CheckBox ID="txtIsAttachedRequired" runat="server" Text="&nbsp; Document Required? " />
                    </div>
                </div>

                <br />
            </div>                    
        </fieldset>
    </asp:Panel>
</asp:Content>