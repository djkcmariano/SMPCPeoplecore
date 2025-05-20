<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfAppMREditAppr.aspx.vb" Inherits="SecuredManager_SelfAppMREditAppr" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <script type="text/javascript">
        function Split(obj, index) {
            var items = obj.split("|");
            for (i = 0; i < items.length; i++) {
                if (i == index) {
                    return items[i];
                }
            }
        }

        function disableenable(chk) {

            if (chk.checked) {
                document.getElementById("ctl00_cphBody_Tab_lstPlantilla").disabled = true;

            } else {
                document.getElementById("ctl00_cphBody_Tab_lstPlantilla").disabled = false;

            };
        };

    
    </script>

    <uc:Tab runat="server" ID="Tab">
        <Header>
            <asp:Label runat="server" ID="lbl" />
        </Header>
        <Content>
            <asp:Panel runat="server" ID="Panel1">
                <br />
                <br />
                <fieldset class="form" id="fsMain">
                    <div  class="form-horizontal">
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">
                            Transaction No. :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtMRCode" CssClass="form-control" Enabled="false" />
                                <asp:HiddenField runat="server" ID="hifMRNo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">MR Date :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtRequestedDate" CssClass="form-control required" SkinID="txtdate" />
                                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtRequestedDate" Format="MM/dd/yyyy" />
                                <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender2" TargetControlID="txtRequestedDate" Mask="99/99/9999" MaskType="Date" />
                                <asp:CompareValidator runat="server" ID="CompareValidator2" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtRequestedDate" Display="Dynamic" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">
                            Date Needed :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtNeededDate" CssClass="form-control required" SkinID="txtdate" />
                                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtNeededDate" Format="MM/dd/yyyy" />
                                <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtNeededDate" Mask="99/99/9999" MaskType="Date" />
                                <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtNeededDate" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">
                            MR Type :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboMRTypeNo" DataMember="EMRType" CssClass="form-control required" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">
                            MR Filling Mode :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboMRReasonNo" DataMember="EMRReason" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">
                            Remarks :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">
                            </label>
                            <div class="col-md-6">
                                <asp:CheckBox ID="txtIsForPooling" runat="server" Text="&nbsp;Please tick here if the Manpower Request is for continuous pooling." AutoPostBack="true">
                                </asp:CheckBox>
                            </div>
                        </div>


                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">
                            No. of Vacancy :</label>
                            <div class="col-md-2">
                                <asp:TextBox runat="server" ID="txtNoOfVacancy" CssClass="form-control number" SkinID="txtdate" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">
                            Position Title :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboPositionNo" CssClass="form-control required" AutoPostBack="true" DataMember="EPosition" OnSelectedIndexChanged="cboPositionNo_SelectedIndexChanged" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">
                            Plantilla No. :</label>
                            <div class="col-md-6">
                                <dx:ASPxListBox runat="server" Width="100%" SelectionMode="CheckColumn" ID="lstPlantilla" AutoPostBack="true" /> 
                                <%--<asp:ListBox ID="lstPlantilla" runat="server" SelectionMode="Multiple" CssClass="form-control">
                                </asp:ListBox>--%>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Functional Title :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboTaskNo" DataMember="ETask" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">
                            Job Level :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboSalaryGradeNo" DataMember="ESalaryGrade" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">
                            Facility :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboFacilityNo" DataMember="EFacility" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">
                            Unit :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboUnitNo" DataMember="EUnit" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">
                            Dept/Office/Region :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboDepartmentNo" DataMember="EDepartment" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">
                            Group/Branch :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboGroupNo" DataMember="EGroup" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">
                            Division :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboDivisionNo" DataMember="EDivision" CssClass="form-control" />
                            </div>
                        </div>
                        
                        
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">
                            Section/Unit :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboSectionNo" CssClass="form-control" DataMember="ESection" />
                            </div>
                        </div>
                        

                        
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">
                            Cost Center :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboCostCenterNo" DataMember="ECostCenter" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">
                            Location :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboLocationNo" DataMember="ELocation" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">
                            Employment Status :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboEmployeeStatNo" DataMember="EEmployeeStat" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">
                            Employee Classification :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboEmployeeClassNo" DataMember="EEmployeeClass" CssClass="form-control" />
                            </div>
                        </div>
                        
                        
                        
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">
                            </label>
                            <div class="col-md-6">
                                <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-primary submit fsMain" Text="Save" OnClick="lnkSave_Click" />
                                <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-primary" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" />
                            </div>
                        </div>
                        <br />
                        <br />
                    </div>
                </fieldset>
            </asp:Panel>
        </Content>
    </uc:Tab>
</asp:Content>

