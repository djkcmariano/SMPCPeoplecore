<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="PayBankFile.aspx.vb" Inherits="Secured_PayBankFile" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<uc:Tab runat="server" ID="Tab">
        <Header>        
            <asp:Label runat="server" ID="lbl" />     
        </Header>
        <Content>

           <asp:Panel runat="server" ID="Panel1">
                                <br /><br />
                                <fieldset class="form" id="fsMain">
                                    <div  class="form-horizontal">

                                        <div class="row">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <div class="col-md-3">
                                                         <asp:Dropdownlist ID="cboCompNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearchGrp_Click" CssClass="form-control" runat="server"/>
                                                         <asp:Dropdownlist ID="cboGrpNo" AutoPostBack="true" CssClass="form-control" runat="server"/>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-2 control-label has-required">Pay Date :</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox runat="server" ID="txtPayDate" CssClass="form-control required" AutoPostBack="true" />
                                                            <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtPayDate" Format="MM/dd/yyyy" />
                                                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtPayDate" Mask="99/99/9999" MaskType="Date" />
                                                            <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtPayDate" Display="Dynamic" />
                                                        </div>
                                                    </div>
                                                    <br/>
                                                    <div class="form-group">
                                                        <label class="col-md-2 control-label has-required">Credit Date :</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox runat="server" ID="txtCreditDate" CssClass="form-control required" AutoPostBack="true" />
                                                            <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtCreditDate" Format="MM/dd/yyyy" />
                                                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender2" TargetControlID="txtCreditDate" Mask="99/99/9999" MaskType="Date" />
                                                            <asp:CompareValidator runat="server" ID="CompareValidator2" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtCreditDate" Display="Dynamic" />
                                                        </div>
                                                    </div>

                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="table-responsive">
                                                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" KeyFieldName="EmpCode"  >                                                                                   
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="Code" Caption = "Bank No"/> 
                                                                        <dx:GridViewDataTextColumn FieldName="Bank" Caption="Bank" />                                            
                                                                        <dx:GridViewDataTextColumn FieldName="PayGrp" Caption="Payroll Group" />
                                                                        <dx:GridViewDataTextColumn FieldName="FileType" Caption="File Type" />
                                                                        <dx:GridViewDataTextColumn FieldName="FileName" Caption="File Name" />
                                                                        <dx:GridViewDataTextColumn FieldName="Count" Caption="Emp Count" />
                                                                        <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" />
                                                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="2%">
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
                                                    <div class="col-md-2">
                                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click" Text="View" CssClass="control-primary" />  
                                                    </div>  
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </fieldset>
                            </asp:Panel>
       
       </Content>
</uc:Tab>
</asp:Content>

