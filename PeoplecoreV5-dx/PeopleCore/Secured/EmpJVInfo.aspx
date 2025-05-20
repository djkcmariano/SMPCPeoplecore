<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EmpJVInfo.aspx.vb" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Secured_EmpJVInfo" %>

<asp:content id="cntNo" contentplaceholderid="cphBody" runat="server">
<uc:Tab runat="server" ID="Tab">
        <Header>
            <center>
                <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />                
            </center>            
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
                                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                <ContentTemplate>                                              
                                <ul class="panel-controls">                                    
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                </ul>                                                                                                                                                     
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
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeJVInfoNo">                                                                                   
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("EmployeeJVInfoNo") %>' OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Record No." />                                            
                                            <dx:GridViewDataTextColumn FieldName="JVInfo1" Caption="JV Info 1" />
                                            <dx:GridViewDataTextColumn FieldName="JVInfo2" Caption="JV Info 2" />
                                            <dx:GridViewDataTextColumn FieldName="JVInfo3" Caption="JV Info 3" />
                                            <%--<dx:GridViewDataTextColumn FieldName="EmployeeStatDesc" Caption="Employment Status" />
                                            <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Department" />
                                            <dx:GridViewDataTextColumn FieldName="CurrentSalary" Caption="Salary" />--%>                                                                                                                                   
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                        </Columns>                            
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                                           
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
            <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                    </div>
            </div>
 
            <div class="form-group">
                    <label class="col-md-4 control-label has-space">JV Info 1 :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtJVInfo1" CssClass="form-control"/>
                    </div>
            </div>
            <div class="form-group">
                    <label class="col-md-4 control-label has-space">JV Info 2 :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtJVInfo2" CssClass="form-control number" MaxLength="7"/>
                    </div>
            </div>
            <div class="form-group" style="display:none">
                    <label class="col-md-4 control-label has-space">JV Info 3 :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtJVInfo3" CssClass="form-control" SkinID="txtdate"/>
                    </div>
            </div>  
            <div class="form-group">
                    <label class="col-md-4 control-label has-space">Remarks :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtRemark" CssClass="form-control"/>
                    </div>
            </div>  
            <br />                        
        </fieldset>
    </asp:Panel> 
</asp:content> 