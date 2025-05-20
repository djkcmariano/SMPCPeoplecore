<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="SecCMSTemplateList.aspx.vb" Inherits="Secured_SecCMSTemplateList" EnableEventValidation="false" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        &nbsp;&nbsp;
                    </div>
                    <div>
                        
                    </div>                           
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="CMSTemplateNo">                                                                                   
                                <Columns>                            
                                    <%--<dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>--%>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="CMSTemplateTitle" Caption="Title" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="CMSCategoryDesc" Caption="Category" />
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Detail" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                </Columns>                            
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />    
                        </div>
                    </div>
                </div>
                   
            </div>
       </div>       
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6 panel-title">
                    <asp:Label ID="lblDetl" runat="server"></asp:Label>
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                            <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>
                        </ul>
                        <uc:ConfirmBox runat="server" ID="cfbDeleteDetl" TargetControlID="lnkDeleteDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" SkinID="grdDX" KeyFieldName="DashboardNo">                                                                                   
                            <Columns>                            
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                <dx:GridViewDataTextColumn FieldName="DashboardDesc" Caption="Description" />
                                <dx:GridViewDataTextColumn FieldName="DashboardTitle" Caption="Title" />
                                <dx:GridViewDataTextColumn FieldName="GroupBy" Caption="Group" />
                                <dx:GridViewDataTextColumn FieldName="Width" Caption="Width" />
                                <dx:GridViewDataTextColumn FieldName="IsVisible" Caption="Visible" />
                                <dx:GridViewDataTextColumn FieldName="IsGraph" Caption="Graph" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Content" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkContent" CssClass="fa fa-external-link" Font-Size="Medium" OnClick="lnkContent_Click" OnPreRender="lnkContent_PreRender" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" /> 
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />    
                    </div>
                </div>
            </div>                   
        </div>
    </div>
    
    
    <asp:Button ID="btnShowMain" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
    <asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsMain">
             <div class="cf popupheader">
                  <h4>&nbsp;</h4>
                  <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveDetl" OnClick="lnkSaveDetl_Click" CssClass="fa fa-floppy-o submit fsMain lnkSaveDetl" ToolTip="Save" />
             </div>
            <div  class="entryPopupDetl form-horizontal">
                
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true"  runat="server" CssClass="form-control" />
                     </div>
                </div>                
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Description :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtDashboardDesc" runat="server" CssClass="form-control required" />                    
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Title :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtDashboardTitle" runat="server" CssClass="form-control" />                    
                    </div>
                </div> 
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Group :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtGroupBy" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Width :</label>
                    <div class="col-md-7">
                        <asp:DropDownList runat="server" ID="cboWidthNo" CssClass="form-control">
                            <asp:ListItem Text="-- Select --" Value="" />
                            <asp:ListItem Text="1" Value="1" />
                            <asp:ListItem Text="2" Value="2" />
                            <asp:ListItem Text="3" Value="3" />
                            <asp:ListItem Text="4" Value="4" />
                            <asp:ListItem Text="5" Value="5" />
                            <asp:ListItem Text="6" Value="6" />
                            <asp:ListItem Text="7" Value="7" />
                            <asp:ListItem Text="8" Value="8" />
                            <asp:ListItem Text="9" Value="9" />
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="11" Value="11" />
                            <asp:ListItem Text="12" Value="12" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsVisible" Text="&nbsp;Visible" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsGraph" Text="&nbsp;Display as chart" AutoPostBack="true" OnCheckedChanged="chkIsGraph_CheckedChanged" />
                    </div>
                </div>
                <div class="form-group" runat="server" visible="false" id="divGraphType">
                    <label class="col-md-4 control-label has-space">Chart Type :</label>
                    <div class="col-md-7">
                        <asp:DropDownList runat="server" ID="cboGraphTypeNo" CssClass="form-control" />                        
                    </div>
                </div>
                <div class="form-group" runat="server" visible="false" id="divChartWidth">
                    <label class="col-md-4 control-label has-space">Chart Width :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtGraphWidth" runat="server" CssClass="form-control number" />                        
                    </div>
                </div>
                <div class="form-group" runat="server" visible="false" id="divDatasource">
                    <label class="col-md-4 control-label has-space">Datasource :</label>
                    <div class="col-md-7">
                        <asp:DropDownList runat="server" ID="cboDatasource" CssClass="form-control" />                        
                    </div>
                </div>                
            </div>
            <div class="cf popupfooter">
            </div>            
        </fieldset>
    </asp:Panel>     
 </div>


</asp:content>
