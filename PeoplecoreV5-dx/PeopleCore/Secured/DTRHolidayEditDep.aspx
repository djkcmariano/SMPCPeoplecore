<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="DTRHolidayEditDep.aspx.vb" Inherits="Secured_DTRHolidayEditDep" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc:Tab runat="server" ID="Tab" HeaderVisible="true">
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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="HolidayLocNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                        <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Department" />
                                        <dx:GridViewDataTextColumn FieldName="NoOfHour" Caption="No. of Hours" Visible="false" />                                        
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
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

<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShow" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none;">
        <fieldset class="form" id="fsDetl1">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsDetl1 lnkSave" OnClick="lnkSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber" />
                </div>
            </div>
           <div class="form-group">
                <label class="col-md-4 control-label has-required">Department :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboDepartmentNo" DataMember="EDepartment"   runat="server" CssClass="form-control required" />                                                                                         
                </div>
            </div>
           <div class="form-group" style=" display:none;">
                <label class="col-md-4 control-label has-space">No. of Hours :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtNoOfHour" runat="server" CssClass="number form-control" />
                </div>
            </div>
        </div>
        <br />
        </fieldset>
</asp:Panel>

</asp:Content>

