<%@ Page Title="" Theme="PCoreStyle" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="TrnTakenAudienceList.aspx.vb" Inherits="Secured_TrnTakenModuleList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<script type="text/javascript">


    function DisableIsUpload(chk) {

        if (chk.checked) {

            document.getElementById("ctl00_cphBody_Tab_txtFullName").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtFullName").value = "";
            document.getElementById("ctl00_cphBody_Tab_hifEmployeeNo").value = "0";

            document.getElementById("ctl00_cphBody_Tab_cboPositionNo").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_cboPositionNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboFacilityNo").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_cboFacilityNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboDivisionNo").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_cboDivisionNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboDepartmentNo").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_cboDepartmentNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboSectionNo").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_cboSectionNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboUnitNo").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_cboUnitNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboGroupNo").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_cboGroupNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboJobGradeNo").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_cboJobGradeNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboEmployeeStatNo").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_cboEmployeeStatNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboEmployeeClassNo").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_cboEmployeeClassNo").value = "";


        } else {

            document.getElementById("ctl00_cphBody_Tab_txtFullName").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtFullName").value = "";
            document.getElementById("ctl00_cphBody_Tab_hifEmployeeNo").value = "0";

            document.getElementById("ctl00_cphBody_Tab_cboPositionNo").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_cboPositionNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboFacilityNo").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_cboFacilityNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboDivisionNo").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_cboDivisionNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboDepartmentNo").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_cboDepartmentNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboSectionNo").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_cboSectionNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboUnitNo").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_cboUnitNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboGroupNo").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_cboGroupNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboJobGradeNo").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_cboJobGradeNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboEmployeeStatNo").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_cboEmployeeStatNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboEmployeeClassNo").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_cboEmployeeClassNo").value = "";
            
        }
    }

</script>


    <uc:Tab runat="server" ID="Tab">
        <Header>
            <asp:Label runat="server" ID="lbl" /> 
        </Header>
        <Content>
        <br />
        <div class="page-content-wrap">         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-2">
                    
                        </div>
                        <div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>                    
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                </ul> 
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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="TrnTakenAudienceNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>                            
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataCheckColumn FieldName="IsCurriculum" Caption="Curriculum" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" />
                                        <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                        <dx:GridViewDataTextColumn FieldName="PositionDesc" Caption="Position" /> 
                                        <dx:GridViewDataTextColumn FieldName="FacilityDesc" Caption="Facility" Visible="false" />                                                                           
                                        <dx:GridViewDataTextColumn FieldName="DivisionDesc" Caption="Division" /> 
                                        <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Department" />    
                                        <dx:GridViewDataTextColumn FieldName="SectionDesc" Caption="Section" />
                                        <dx:GridViewDataTextColumn FieldName="UnitDesc" Caption="Unit" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="GroupDesc" Caption="Group" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="JobGradeDesc" Caption="Job Grade" />
                                        <dx:GridViewDataTextColumn FieldName="EmployeeStatDesc" Caption="Employee Status" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="EmployeeClassDesc" Caption="Employee Class" Visible="false" />                                                                                                                                                                     
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

            <asp:Button ID="Button1" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
            <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none" >
                <fieldset class="form" id="fsMain">                    
                    <div class="cf popupheader">
                        <h4>&nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
                    </div>                                            
                    <div class="entryPopupDetl form-horizontal">   
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:Textbox ID="txtTrnTakenAudienceNo" ReadOnly="true" runat="server" CssClass="form-control" />
                            </div>
                        </div>                     

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                            </div>
                        </div>


                        <div class="form-group">
                            <label class="col-md-4 control-label has-space"></label>
                            <div class="col-md-8">
                                <asp:CheckBox ID="txtIsCurriculum" runat="server" onclick="DisableIsUpload(this);" Text="&nbsp; Target audience from position curriculum and employee curriculum" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Employee Name :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" onblur="ResetEmployee()" style="display:inline-block;" Placeholder="Type here..." /> 
                                <asp:HiddenField runat="server" ID="hifEmployeeNo" />
                                <ajaxToolkit:AutoCompleteExtender ID="aceEmployee" runat="server"
                                TargetControlID="txtFullName" MinimumPrefixLength="2" EnableCaching="true"                    
                                CompletionSetCount="1" CompletionInterval="500" ServiceMethod="PopulateEmployee" ServicePath="~/asmx/WebService.asmx"
                                CompletionListCssClass="autocomplete_completionListElement" 
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                OnClientItemSelected="GetRecord" FirstRowSelected="true" UseContextKey="true" />
                                <script type="text/javascript">
                                    function SplitH(obj, index) {
                                        var items = obj.split("|");
                                        for (i = 0; i < items.length; i++) {
                                            if (i == index) {
                                                return items[i];
                                            }
                                        }
                                    }
                                    function GetRecord(source, eventArgs) {
                                        document.getElementById('<%= hifEmployeeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                                    }

                                    function ResetEmployee() {
                                        if (document.getElementById('<%= txtFullName.ClientID %>').value == "") {
                                            document.getElementById('<%= hifEmployeeNo.ClientID %>').value = "0";
                                        }
                                    } 

                                </script>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label">Position :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboPositionNo" DataMember="EPosition" runat="server" CssClass="form-control" />
                            </div>
                        </div>                      

                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label">Facility :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboFacilityNo" DataMember="EFacility" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label">Division :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboDivisionNo" DataMember="EDivision" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label">Department :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboDepartmentNo" DataMember="EDepartment" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label">Section :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboSectionNo" DataMember="ESection" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label">Unit :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboUnitNo" DataMember="EUnit" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label">Group :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboGroupNo" DataMember="EGroup" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label">Job Grade :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboJobGradeNo" DataMember="EJobGrade" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label">Employee Classification :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboEmployeeClassNo" DataMember="EEmployeeClass" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label">Employee Status :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboEmployeeStatNo" DataMember="EEmployeeStat" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <br />

                    </div>                    
                </fieldset>
            </asp:Panel>

        </Content>
    </uc:Tab>
</asp:Content>

