<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle"  MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="SecAuditList02.aspx.vb" Inherits="Secured_SecAuditList02" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-4">
                    <label class="col-md-4 control-label has-space">Name of Table :</label><asp:Dropdownlist ID="cboTableName" runat="server" CssClass="form-control" AutoPostBack="true" />
                    
                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                    
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkRefresh" OnClick="lnkRefresh_Click" Text="Load Data" CssClass="control-primary" /></li>                            
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>                            
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="TableAuditNo" >  
                            <Columns>                                                                                 
                                <dx:GridViewDataTextColumn FieldName="RecordNo" Caption="Record No." />
                                <dx:GridViewDataTextColumn FieldName="TableRefDesc" Caption="Description"/>
                                <dx:GridViewDataTextColumn FieldName="UserName" Caption="User" />   
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encode Date" />   
                                <dx:GridViewDataTextColumn FieldName="AuditType" Caption="Audit Type" />  
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />
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
</div>        
<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="panel-title" >
                    Detail:&nbsp; <asp:Label runat="server" ID="lblDetl" />
                </div>                   
                
            </div>
            <div class="panel-body">
                <div class="panel-body">
                    <div class="panel-body" style="margin-top:0px;padding-top:0px;">
                        <div class="row">
                                <h3> Details</h3>
                                <div class="table-responsive">                    
                                    <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="TableAuditDetiNewNo" Width="100%" >                                                                                   
                                        <Columns>                           
                       
                                            <dx:GridViewDataTextColumn FieldName="columname" Caption="Column Name" />                                                        
                                            <dx:GridViewDataTextColumn FieldName="old_desc" Caption="Old" />
                                            <dx:GridViewDataTextColumn FieldName="new_desc" Caption="New" />              
                                        </Columns>
                                        <SettingsPager Mode="ShowAllRecords" />                            
                                        <SettingsBehavior AllowSort="true" />    
                                        <Settings ShowFooter="true" />  
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="Amount" SummaryType="Sum" />   
                                        </TotalSummary>                        
                                    </dx:ASPxGridView>

                                </div>                            
                                <br />
                         </div>
                </div>
            </div>
            </div>
        </div>
    </div>
    
        
    
</div>

</asp:Content>

