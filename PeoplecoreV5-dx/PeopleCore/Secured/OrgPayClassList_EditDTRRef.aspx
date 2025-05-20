<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="OrgPayClassList_EditDTRRef.aspx.vb" Inherits="Secured_OrgPayClassDTRRefEdit" %>


<asp:content id="cntNo" contentplaceholderid="cphBody" runat="server">

<fieldset class="form" id="fsMain">
    <div class="page-content-wrap">         
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-body">
                        <div  class="form-horizontal">
                            <div class="form-group">
                                    <label class="col-md-2 control-label">Transaction No. :</label>
                                    <div class="col-md-6">
                                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" ></asp:Textbox>
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">Employee class :</label>
                                <div class="col-md-3">
                                        <asp:Dropdownlist ID="cboEmployeeClassNo" DataMember="EEmployeeClass" runat="server" CssClass="form-control" ></asp:Dropdownlist>
                                    </div>
                            
                                <label class="col-md-2 control-label">Employee status :</label>
                                <div class="col-md-3">
                                        <asp:Dropdownlist ID="cboEmployeeStatNo" DataMember="EEmployeeStat" runat="server" CssClass="form-control" ></asp:Dropdownlist>
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">ADJUSTMENT :</label>
                                <div class="col-md-3">
            
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">Late adjustment :</label>
                                <div class="col-md-2">
                                        <asp:TextBox ID="txtMinLate"  
                                        runat="server" SkinID="txtdate" CssClass="form-control"  
                                        ></asp:TextBox>   
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" 
                                        FilterType="Custom, Numbers" ValidChars="."  TargetControlID="txtMinLate" ></ajaxToolkit:FilteredTextBoxExtender> 
                                    </div>
                            
                                <label class="col-md-2 control-label">Undertime adjustment :</label>
                                <div class="col-md-2">
                                        <asp:TextBox ID="txtMinUT"  
                                        runat="server" SkinID="txtdate" CssClass="form-control"  
                                        ></asp:TextBox>   
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" 
                                        FilterType="Custom, Numbers" ValidChars="."  TargetControlID="txtMinUT" ></ajaxToolkit:FilteredTextBoxExtender> 
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">ROUNDING OF LATE :</label>
                                <div class="col-md-3">
            
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">Cut-off late 1 :</label>
                                <div class="col-md-2">
                                        <asp:TextBox ID="txtMaxLate"  
                                        runat="server" SkinID="txtdate" CssClass="form-control"  
                                        ></asp:TextBox>   
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" 
                                        FilterType="Custom, Numbers" ValidChars="."  TargetControlID="txtMaxLate" ></ajaxToolkit:FilteredTextBoxExtender> 
                                    </div>
                            
                                <label class="col-md-2 control-label">Cut-off late 2 :</label>
                                <div class="col-md-2">
                                        <asp:TextBox ID="txtMaxLate2"  
                                        runat="server" SkinID="txtdate" CssClass="form-control"  
                                        ></asp:TextBox>   
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" 
                                        FilterType="Custom, Numbers" ValidChars="."  TargetControlID="txtMaxLate2" ></ajaxToolkit:FilteredTextBoxExtender> 
                                    </div>
                            
                                <label class="col-md-2 control-label">Round off late :</label>
                                <div class="col-md-2">
                                        <asp:TextBox ID="txtRoundOfLate"  
                                        runat="server" SkinID="txtdate" CssClass="form-control"  
                                        ></asp:TextBox>   
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" 
                                        FilterType="Custom, Numbers" ValidChars="."  TargetControlID="txtRoundOfLate" ></ajaxToolkit:FilteredTextBoxExtender> 
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">ROUNDING OF UNDERTIME :</label>
                                <div class="col-md-3">
            
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">Cut-off undertime 1 :</label>
                                <div class="col-md-2">
                                        <asp:TextBox ID="txtMaxUT"  
                                        runat="server" SkinID="txtdate" CssClass="form-control"  
                                        ></asp:TextBox>   
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" 
                                        FilterType="Custom, Numbers" ValidChars="."  TargetControlID="txtMaxUT" ></ajaxToolkit:FilteredTextBoxExtender> 
                                    </div>
                            
                                <label class="col-md-2 control-label">Cut-off undertime 2 :</label>
                                <div class="col-md-2">
                                        <asp:TextBox ID="txtMaxUT2"  
                                        runat="server" SkinID="txtdate" CssClass="form-control"  
                                        ></asp:TextBox>   
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" 
                                        FilterType="Custom, Numbers" ValidChars="."  TargetControlID="txtMaxUT2" ></ajaxToolkit:FilteredTextBoxExtender> 
                                    </div>
                            
                                <label class="col-md-2 control-label">Round off undertime :</label>
                                <div class="col-md-2">
                                        <asp:TextBox ID="txtRoundOfUT"  
                                        runat="server" SkinID="txtdate" CssClass="form-control"  
                                        ></asp:TextBox>   
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" 
                                        FilterType="Custom, Numbers" ValidChars="."  TargetControlID="txtRoundOfUT" ></ajaxToolkit:FilteredTextBoxExtender> 
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">OVERTIME ADJUSTMENT :</label>
                                <div class="col-md-3">
            
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">Minimum advance overtime :</label>
                                <div class="col-md-2">
                                        <asp:TextBox ID="txtMinAdvOTHrs"  
                                        runat="server" SkinID="txtdate" CssClass="form-control"  
                                        ></asp:TextBox>   
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" 
                                        FilterType="Custom, Numbers" ValidChars="."  TargetControlID="txtMinAdvOTHrs" ></ajaxToolkit:FilteredTextBoxExtender> 
                                    </div>
                            
                                <label class="col-md-2 control-label">Minimum overtime (after work) :</label>
                                <div class="col-md-2">
                                        <asp:TextBox ID="txtMinOTHrs"  
                                        runat="server" SkinID="txtdate" CssClass="form-control"  
                                        ></asp:TextBox>   
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" 
                                        FilterType="Custom, Numbers" ValidChars="."  TargetControlID="txtMinOTHrs" ></ajaxToolkit:FilteredTextBoxExtender> 
                                    </div>
                            
                                <label class="col-md-2 control-label">Maximum overtime :</label>
                                <div class="col-md-2">
                                        <asp:TextBox ID="txtMaxOT"  
                                        runat="server" SkinID="txtdate" CssClass="form-control"  
                                        ></asp:TextBox>   
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" 
                                        FilterType="Custom, Numbers" ValidChars="."  TargetControlID="txtMaxOT" ></ajaxToolkit:FilteredTextBoxExtender> 
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">Please check here</label>
                                <div class="col-md-3">
                                        <asp:CheckBox ID="txtIsDeductLateFrOT" 
                                        runat="server" />
                                        <span> to deduct late from overtime.</span>
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">Please check here</label>
                                <div class="col-md-3">
                                        <asp:CheckBox ID="txtIsDeductUnderFrOT" 
                                        runat="server" />
                                        <span> to deduct undertime from overtime.</span>
                                    </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-2 control-label">Please check here</label>
                                <div class="col-md-3">
                                        <asp:CheckBox ID="txtIsApplyToAll" 
                                        runat="server" />
                                        <span> if applicable to all employees.</span>
                                    </div>
                        </div> 
                        <div class="form-group">
                            <label class="col-md-2 control-label "></label>
                            <div class="col-md-9">
                                <div class="btn-group">
                                    <asp:Button runat="server"  ID="lnkSubmit" CssClass="btn btn-default submit fsMain" Text="submit" OnClick= "btnSave_Click" ></asp:Button>
          
                                    <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="modify"></asp:Button>

                                    <asp:Button runat="server"  ID="lnkCancel" style="display:none;" CssClass="btn btn-default" CausesValidation="false" Text="<< Back/Cancel"></asp:Button>
                                </div>
                            </div>
                        </div> 
                      
                    </div>
                </div>
            </div>
        </div>
    </div>
</fieldset >

</asp:content> 