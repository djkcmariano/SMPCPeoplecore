<%@ Page Title="" Language="VB" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="CliClinicSocialList.aspx.vb" Inherits="Secured_CliClinicSocialList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">
         <Header>
            <center>
                <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
                <br />            
            </center>            
            <asp:Label runat="server" ID="lbl" />        
        </Header>
        <Content>
        <asp:Panel runat="server" ID="Panel1">        
            <br /><br />         
            <fieldset class="form" id="fsMain">
                <div  class="form-horizontal">
                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label">Transaction No. :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtClinicSocialHisCode" CssClass="form-control" Enabled="false" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label">State Food Preference :</label>
                        <div class="col-md-6">
                            <label class="radio-inline">
                                <asp:CheckBox ID="chkIsSalty" Text="&nbsp;Salty" runat="server" />
                            </label>
                            <label class="radio-inline">
                                <asp:CheckBox ID="chkIsFatty" Text="&nbsp;Fatty" runat="server" />
                            </label>
                            <label class="radio-inline">
                                <asp:CheckBox ID="chkIsSweet" Text="&nbsp;Sweet" runat="server" />
                            </label>
                            <label class="radio-inline">
                                <asp:CheckBox ID="chkIsSpicy" Text="&nbsp;Spicy" runat="server" />
                            </label>

                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label">Exposure to Venereal Disease :</label>
                        <div class="col-md-6">
                            <label class="radio-inline">
                                <asp:RadioButton ID="rboIsEVDNo" GroupName="rboIsEVD" Text="No" runat="server" />
                            </label>
                            <label class="radio-inline">
                                <asp:RadioButton ID="rboIsEVDYes" GroupName="rboIsEVD" Text="Yes" runat="server" />
                            </label>
                        </div>
                                
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label">Use of Drug(s) :</label>
                        <div class="col-md-6">
                            <label class="radio-inline">
                                <asp:RadioButton ID="rboIsUDNo" GroupName="rboIsUD" Text="No" runat="server" />
                            </label>
                            <label class="radio-inline">
                                <asp:RadioButton ID="rboIsUDYes" GroupName="rboIsUD" Text="Yes" runat="server" />
                            </label>
                            
                        </div>
                    </div>

                    <br />
                    <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">SMOKING</label>
                        </h5>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label">Duration :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtSmokeD" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label">No. of Packs/Sticks :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtSmokeA" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label">Frequency :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtSmokeF" CssClass="form-control" />
                        </div>
                    </div>

                    <br />
                    <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">ALCOHOL&nbsp;&nbsp;INTAKE</label>
                        </h5>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label">Duration :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtAlcoholD" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label">No. of Bottles :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtAlcoholA" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label">Frequency :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtAlcoholF" CssClass="form-control" />
                        </div>
                    </div>

                    <br />
                    <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">SLEEPING</label>
                        </h5>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label">Sleeping Habits :</label>
                        <div class="col-md-3">
                            <label class="radio-inline">
                                <asp:RadioButton runat="server" ID="rboIsSleepYes" Text="Regular" GroupName="rboIsSleep" />
                            </label>
                            <label class="radio-inline">
                                <asp:RadioButton runat="server" ID="rboIsSleepNo" Text="Irregular" GroupName="rboIsSleep" />
                            </label>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label">Sleep Remarks :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtSleepRemarks" TextMode="MultiLine" Rows="3" CssClass="form-control" />
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

