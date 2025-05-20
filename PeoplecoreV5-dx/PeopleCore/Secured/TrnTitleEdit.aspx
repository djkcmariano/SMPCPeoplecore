<%@ Page Title="" Language="VB" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="TrnTitleEdit.aspx.vb" Inherits="Secured_TrnTitleEdit" %>

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
                    <div class="form-group" style="display:none">
                        <label class="col-md-3 control-label has-space">Transaction No. :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtTrnTitleNo" CssClass="form-control" Enabled="false" ReadOnly="true" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Transaction No. :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtCode" CssClass="form-control" Enabled="false" ReadOnly="true" Placeholder="Autonumber" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Code :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtTrnTitleCode" CssClass="form-control required" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Title :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtTrnTitleDesc" CssClass="form-control required" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Training Category :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboTrnCategoryNo" DataMember="ETrnCategory" runat="server"  CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Training Type :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboTrnTypeNo" DataMember="ETrnType" runat="server" CssClass="form-control" AutoPostBack="false" OnSelectedIndexChanged="cboTrnTypeNo_SelectedIndexChanged" />
                        </div>
                    </div>


                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Description :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Objectives :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtObjectives" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Hour(s) :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtHrs" CssClass="number form-control required" />
                        </div>
                    </div>

                    <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">OTHER DETAILS&nbsp;&nbsp;</label>
                        </h5>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Professional Fee(s) :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtCost" CssClass="number form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">No. of Month(s) :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtNoOfMonths" CssClass="number form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Service Contract :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtServiceContract" CssClass="number form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Re-taken Schedule :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboTrnRetakenNo" DataMember="ETrnRetaken" runat="server"  CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Guidelines :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Company Name :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                            </asp:Dropdownlist>
                        </div>
                    </div> 

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-6">
                            <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                        </div>
                    </div>
        
                    <div class="form-group">
                        <label class="col-md-3 control-label"></label>
                        <div class="col-md-6">            
                            <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="lnkSave_Click"></asp:Button>          
                            <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click"></asp:Button>                        
                        </div>
                    </div> 
                </div>                                
                <br />
            </fieldset >
        </asp:Panel>
        </Content>
    </uc:Tab>    
</asp:Content>

