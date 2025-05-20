<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayContList.aspx.vb" Inherits="Secured_PayContList" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

 <div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" ></asp:Dropdownlist>
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkProcess" OnClick="lnkProcess_Click" Text="Process" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                </ul>
                                <uc:ConfirmBox runat="server" ID="cfbPost" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be modified, Proceed?" MessageType="Post"  />
                                <uc:ConfirmBox runat="server" ID="cfblnkProcess" TargetControlID="lnkProcess" ConfirmMessage="Do you want to proceed?" MessageType="Process"  /> 
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
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayContNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="PayLocDesc" Caption="Company Name" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="FacilityDesc" Caption="Facility" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="MonthDesc" Caption="Month" />
                                    <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Year" CellStyle-HorizontalAlign="Left" />

                                    <dx:GridViewBandColumn Caption="SSS Cont." HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="SSSSBR" Caption="SBR" />
                                            <dx:GridViewDataTextColumn FieldName="SSSDate" Caption="Date" />
                                            <dx:GridViewDataTextColumn FieldName="SSSBank" Caption="Bank" />
                                        </Columns>
                                    </dx:GridViewBandColumn>

                                    <dx:GridViewBandColumn Caption="PH Cont." HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="PHSBR" Caption="SBR" />
                                            <dx:GridViewDataTextColumn FieldName="PHDate" Caption="Date" />
                                            <dx:GridViewDataTextColumn FieldName="PHBank" Caption="Bank" />
                                        </Columns>
                                    </dx:GridViewBandColumn>

                                    <dx:GridViewBandColumn Caption="HDMF Cont." HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="HDMFSBR" Caption="SBR" />
                                            <dx:GridViewDataTextColumn FieldName="HDMFDate" Caption="Date" />
                                            <dx:GridViewDataTextColumn FieldName="HDMFBank" Caption="Bank" />
                                        </Columns>
                                    </dx:GridViewBandColumn>

                                    <dx:GridViewBandColumn Caption="SSS Loan" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="SSSLoanSBR" Caption="SBR" />
                                            <dx:GridViewDataTextColumn FieldName="SSSLoanDate" Caption="Date" />
                                            <dx:GridViewDataTextColumn FieldName="SSSLoanBank" Caption="Bank" />
                                        </Columns>
                                    </dx:GridViewBandColumn>

                                    <dx:GridViewBandColumn Caption="HDMF Loan" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="HDMFLoanSBR" Caption="SBR" />
                                            <dx:GridViewDataTextColumn FieldName="HDMFLoanDate" Caption="Date" />
                                            <dx:GridViewDataTextColumn FieldName="HDMFLoanBank" Caption="Bank" />
                                        </Columns>
                                    </dx:GridViewBandColumn>

                                    <dx:GridViewDataTextColumn FieldName="DatePosted" Caption="Date Posted" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="PostedBy" Caption="Posted By" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="DateProcessed" Caption="Date Processed" />
                                    <dx:GridViewDataTextColumn FieldName="ProcessedBy" Caption="Processed By" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" />
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Contributions" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkCont" CssClass="fa fa-external-link" Font-Size="Medium" OnClick="lnkCont_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Loans" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkLoan" CssClass="fa fa-external-link" Font-Size="Medium" OnClick="lnkLoan_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <%--<dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Process" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkProcess_Detail" CssClass='<%# Bind("Icon") %>' Enabled='<%# Bind("IsEnabled") %>' OnClick="lnkProcess_Detail_Click" />
                                            <uc:ConfirmBox runat="server" ID="cfProcess_Detail" TargetControlID="lnkProcess_Detail" ConfirmMessage='<%# Bind("ConfirmMessage") %>' Visible='<%# Bind("IsEnabled") %>'  /> 
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

</asp:Content>

