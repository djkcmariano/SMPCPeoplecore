<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="PayContEdit_Disk.aspx.vb" Inherits="Secured_PayContEdit" Theme="PCoreStyle" %>

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

                                       <div class="form-group">
                                            <label class="col-md-3 control-label has-space">Source of Disketting :</label>
                                            <div class="col-md-6">
                                                <label class="radio-inline">
                                                <asp:RadioButton GroupName="Contribution" ID="rdoGsisC"    Text="SSS Contribution" runat="server" Visible="false" /><br />
                                                <asp:RadioButton GroupName="Contribution" ID="rdoSSSOrig"  Text="SSS Contribution (Old)" runat="server" Visible="false" /><br />
                                                <asp:RadioButton GroupName="Contribution" ID="rdoSSSEPF"   Text="SSS EPF (ALL)" runat="server" Visible="false" /><br />
                                                <asp:RadioButton GroupName="Contribution" ID="rdoSSSEPFnew" Text="SSS EPF (NEW)" runat="server" Visible="false" /><br />
                                                <asp:RadioButton GroupName="Contribution" ID="rdoGSISL"    Text="SSS Loan" runat="server" Visible="false" /><br />
                                                
                                                <asp:RadioButton GroupName="Contribution" ID="rdoSSSNew"  Text="SSS Contribution (New)" runat="server" /><br />
                                                <asp:RadioButton GroupName="Contribution" ID="rdoPHM"      Text="PH Contribution (Monthly)" runat="server" /><br />
                                                <asp:RadioButton GroupName="Contribution" ID="rdoPHc"      Text="PH Contribution (Quarterly)" runat="server" /><br />
                                                <asp:RadioButton GroupName="Contribution" ID="rdoHDMFc"    Text="HDMF Premium (DBF)" runat="server" /><br />
                                                <%--<asp:RadioButton GroupName="Contribution" ID="rdoHDMFT"    Text="HDMF Premium (TEXT)" runat="server" /><br />--%>
                                                <asp:RadioButton GroupName="Contribution" ID="rdoHDMFT"    Text="HDMF Premium (MCL)" runat="server" /><br />
                                                <asp:RadioButton GroupName="Contribution" ID="rdoHDMFMP2"  Text="HDMF MP2 (MCL)" runat="server" /><br />
                                                <asp:RadioButton GroupName="Contribution" ID="rdoHDMFSAL"  Text="HDMF Salary Loan (MCL)" runat="server" /><br />
                                                <asp:RadioButton GroupName="Contribution" ID="rdoHDMFCAL"  Text="HDMF Calamity Loan (MCL)" runat="server" /><br />
                                                <asp:RadioButton GroupName="Contribution" ID="rdoHDMFCSV"  Text="HDMF Premium (CSV)" runat="server" /><br />
                                                <asp:RadioButton GroupName="Contribution" ID="rdoHDMFNew"  Text="HDMF New (DBF)" runat="server" /><br />
                                                <asp:RadioButton GroupName="Contribution" ID="rdoHDMFSum"  Text="HDMF Summary (DBF)" runat="server" /><br />
                                                <asp:RadioButton GroupName="Contribution" ID="rdoHDMFL"    Text="HDMF Loan (DBF)" runat="server" /><br />
                                                <asp:RadioButton GroupName="Contribution" ID="rdoHDMFLxls"  Text="HDMF Loan (CSV)" runat="server" /><br />
                                                <asp:RadioButton GroupName="Contribution" ID="rdoHDMFPremiumLoan" Text="HDMF Premium and Loan (DBF)" runat="server" /><br />
                                                <asp:RadioButton GroupName="Contribution" ID="rdoHDMFCalamityLoan" Text="HDMF Calamity Loan (DBF)" runat="server" /><br />
                                                </label>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label has-space">Reference No. :</label>
                                            <div class="col-md-6">
                                                <asp:Textbox ID="txtRefNo" runat="server" CssClass="form-control"></asp:Textbox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="-" TargetControlID="txtRefNo" />
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label has-space">Signatory :</label>
                                            <div class="col-md-6">
                                                <asp:DropdownList ID="cboEmployeeNo" DataMember="EEmployeeL" runat="server" CssClass="form-control"></asp:DropdownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label has-space"></label>
                                            <div class="col-md-6">
                                                <asp:Button runat="server"  ID="lnkSubmit" OnPreRender="lnk_PreRender" CssClass="btn btn-default submit fsMain" Text="Create Disk" OnClick="lnkCreate_Click" />
                                                
                                            </div>
                                        </div>

                                        <br />
                                    </div>
                                </fieldset>
                            </asp:Panel>
       
       </Content>
</uc:Tab>
</asp:Content>

