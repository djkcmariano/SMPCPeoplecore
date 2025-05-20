<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="OrgPayLocList_Edit.aspx.vb" Inherits="Secured_OrgPayLocEdit" %>


<asp:content id="cntNo" contentplaceholderid="cphBody" runat="server">
<asp:UpdatePanel runat="server" ID="UpdatePanelFU">
<ContentTemplate>
<asp:Panel runat="server" ID="Panel1">        
<div class="page-content-wrap">
    <div class="row">
        <div class="panel panel-default">
            <br />
            <div class="panel-body">
                <div class="row">
                    <fieldset class="form" id="fsMain">
                    <div  class="form-horizontal">
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Company Code :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtPayLocCode" CssClass="form-control required" />
                            </div>
                        </div>   
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Company Name :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtPayLocDesc" CssClass="form-control required" />
                            </div>
                        </div>        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Address :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">City :</label>
                            <div class="col-md-5">
                                <asp:Dropdownlist ID="cboCityNo" DataMember="ECity" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Email Address :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtEmailAddress" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">URL :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtURL" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Fax No. :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtFaxNo" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Phone No. :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtPhoneNo" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Extension 1 :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtExtension1" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Extension 2 :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtExtension2" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">SSS No. :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtSSSNo" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">PH No. :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtPHNo" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">HDMF No. :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtHDMFNo" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">TIN :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtTINNo" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Branch Code (TIN) :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtTinBranchCode" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">RDO Code :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtRdoCode" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">&nbsp;</label>
                            <div class="col-md-5">
                                <asp:CheckBox runat="server" ID="chkIsGov" Text="&nbsp;Government Agency" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">SSS Branch Code :</label>
                            <div class="col-md-5">
                                <asp:TextBox ID="txtSSSBranchCode" CssClass="form-control" runat="server" /> 
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">HDMF Branch Code :</label>
                            <div class="col-md-5">
                                <asp:TextBox ID="txtHDMFBranchCode" CssClass="form-control" runat="server" /> 
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">PH Branch Code :</label>
                            <div class="col-md-5">
                                <asp:TextBox ID="txtPHBranchCode" CssClass="form-control" runat="server" /> 
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Logo :</label>
                            <div class="col-md-5">
                                <asp:FileUpload ID="fuPhoto" runat="server" Width="100%" />                
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">&nbsp;</label>
                            <div class="col-md-5">
                                <asp:CheckBox runat="server" ID="txtIsTaxMonthly" Text="&nbsp;Monthly/Semi-Monthly Tax Computation." />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">&nbsp;</label>
                            <div class="col-md-5">
                                <asp:CheckBox runat="server" ID="txtIsProrateTax" Text="&nbsp;Prorated Tax Computation." />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Maximum CAP for tax-exempt bonuses and other benefits. :</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtMaxAmtAccumulatedExemp" CssClass="number form-control" runat="server" />      
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtMaxAmtAccumulatedExemp" />                         
                            </div>
                            <label class="col-md-3 control-label has-space"></label>

                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space"></label>
                            <div class="col-md-5">            
                                <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-primary submit fsMain" Text="Save" OnClick="lnkSave_Click" />
                                <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-primary" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" />                            
                            </div>
                        </div>
                        <br /><br />                     
                    </div>                                                
                </fieldset>                                        
                </div>                
            </div>
        </div>
    </div>
</div>
</asp:Panel>
</ContentTemplate>
<Triggers>
    <asp:PostBackTrigger ControlID="lnkSave" />
</Triggers>
</asp:UpdatePanel>    
</asp:content> 