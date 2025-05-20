<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="SelfEmpStandardHeader_EI.aspx.vb" Inherits="SecuredSelf_SelfEmpStandardHeader_EI" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<br />
<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" Visible="false" />
                </div>
                <div>                
                    <ul class="panel-controls">
                        <li><asp:LinkButton runat="server" ID="lnkSubmit" OnClick="lnkSubmit_Click" Text="Submit" CssClass="control-primary" /></li>                        
                        <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkSubmit" ConfirmMessage="Are you sure you want to submit?"  />                        
                    </ul>                                                                                                                                                                    
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">                    
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeEINo">                                                                                   
                            <Columns>
                              
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                <dx:GridViewDataTextColumn FieldName="fullname" Caption="Employee Name" />
                                <dx:GridViewDataTextColumn FieldName="tDesc" Caption="Exit Interview Template" />
                                <dx:GridViewDataDateColumn FieldName="Effectivity" Caption="Effective Date" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Form" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkForm" CssClass="fa fa-external-link" Font-Size="Medium" CommandArgument='<%# Eval("ApplicantStandardMainNo") & "|" & Eval("EmployeeEINo") & "|" &  Eval("EmployeeNo") & "|" & Eval("IsEnabled")  %>' OnClick="lnkTemplate_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <%--<dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Clearance" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />                                        
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>--%>

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
<br />
<div id="Div1" class="page-content-wrap" runat="server" style=" display:none">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6 panel-title">
                    <asp:Label runat="server" ID="lbl" />
                </div>
                <div>                
                    <ul class="panel-controls"> 
                        &nbsp;                                                       
                        <li><asp:LinkButton runat="server" ID="lnkSaveDetl"  Text="Save" CssClass="control-primary" Visible="false" /></li>                        
                    </ul>                                                                                                                                                                         
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">                    
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="EmployeeEIClearanceNo" Width="100%">
                            <Columns>                                
                                
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="IfullName" Caption="Superior" />
                                <dx:GridViewDataTextColumn FieldName="EmployeeClearanceTypeDesc" Caption="Clearance Type" />
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" />                                                                            
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />
                            </Columns>                            
                        </dx:ASPxGridView>                        
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>

</asp:Content>