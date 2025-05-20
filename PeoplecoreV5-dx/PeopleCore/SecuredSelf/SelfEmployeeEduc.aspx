<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle"  MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SelfEmployeeEduc.aspx.vb" Inherits="SecuredSelf_SelfEmployeeEduc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc:TabSelf runat="server" ID="TabSelf" HeaderVisible="true">
    <Header>
        <center>
            <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
            <br />            
        </center>            
        <asp:Label runat="server" ID="lbl" />        
    </Header>    
    <Content>
        <br />
        <div class="page-content-wrap" >         
            <div class="row">
                <i>Please include Elementary , Secondary education (if applicable), College/University (if applicable), and Graduate studies (if applicable).</i>
            </div>
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-2">
                            &nbsp;
                        </div>
                        <div>                                                
                            <ul class="panel-controls">                                    
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ul>                                                                                                                                                     
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeEducNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("EmployeeEducNo") %>' OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="EducLevelDesc" Caption="Level" />
                                        <dx:GridViewDataTextColumn FieldName="SchoolDesc" Caption="School" />
                                        <dx:GridViewDataTextColumn FieldName="CourseDesc" Caption="Course" />
                                        <dx:GridViewDataTextColumn FieldName="FromDate" Caption="From" />
                                        <dx:GridViewDataTextColumn FieldName="ToDate" Caption="To" />                                        
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                    </Columns>                            
                                </dx:ASPxGridView>                                
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>
        </div>               
    </Content>
</uc:TabSelf>
<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder= "Autonumber"/>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Level :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboEducLevelNo" DataMember="EEducLevel" runat="server" CssClass="form-control required" OnSelectedIndexChanged="cboEducLevelNo_SelectedIndexChanged" AutoPostBack="true" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required" runat="server" id="lblSchool">Name of School :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboSchoolNo" CssClass="form-control" DataMember="ESchool" AutoPostBack="true" OnSelectedIndexChanged="cboSchoolNo_SelectedIndexChanged" />
                    <asp:TextBox runat="server" ID="txtOtherSchool" CssClass="form-control" />                    
                </div>
            </div> 
            <div class="form-group" runat="server" id="divSchoolCampus" visible="false">
                <label class="col-md-4 control-label has-required" runat="server" id="Label1">Campus / Branch :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboSchoolDetiNo" CssClass="form-control" />                    
                </div>
            </div>
            <div class="form-group" runat="server" visible="false">
                <label class="col-md-4 control-label has-space"><asp:CheckBox runat="server" ID="txtIsOtherSchool" AutoPostBack="true" OnCheckedChanged="txtIsOtherSchool_CheckedChanged" />  If others (please specify) :</label>
                <div class="col-md-7">
                    
                </div>
            </div>                      
            <div class="form-group">
                <label class="col-md-4 control-label has-space">School Address :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" CssClass="form-control" Rows="2" />
                </div>
            </div>         
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Field of Study :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFieldOfStudy" CssClass="form-control" Visible="false" />
                    <asp:DropDownList runat="server" ID="cboFieldOfStudyNo" CssClass="form-control" DataMember="EFieldOfStudy" />
                </div>
            </div>
            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space">Industry :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtIndustry" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required" runat="server" id="lblEduc">Basic Education / Degree / Course :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboCourseNo" CssClass="form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><asp:CheckBox runat="server" ID="txtIsOtherCourse" AutoPostBack="true" OnCheckedChanged="txtIsOtherCourse_CheckedChanged"/>  If others (please specify) :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtOtherCourse" CssClass="form-control" TextMode="MultiLine" Rows="2" Placeholder="Course / Major / Degree" />                    
                </div>
            </div>

                           
            <div class="form-group">
                <label class="col-md-4 control-label">PERIOD OF ATTENDANCE</label>
                <div class="col-md-7">
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Attended From :</label>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="cboFromMonth" CssClass="form-control required" />
                </div>
                <div class="col-md-2">
                    <asp:DropDownList runat="server" ID="cboFromDay" CssClass="form-control" />                   
                </div>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtFromYear" CssClass="form-control required" placeholder="Year" /> 
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="txtFromYear" />                               
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Attended To :</label>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="cboToMonth" CssClass="form-control required" />
                </div>
                <div class="col-md-2">
                    <asp:DropDownList runat="server" ID="cboToDay" CssClass="form-control" />                    
                </div>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtToYear" CssClass="form-control required" placeholder="Year" /> 
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtToYear" />                     
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Year Graduated :</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtYearGrad" CssClass="form-control number" />                                
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Highest Level / Units Earned (if not graduated) :</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtUnitEarned" CssClass="form-control number" />                                
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Academic Honors Received :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboHonorNo" DataMember="EHonor" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Scholarship :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtScholarship" CssClass="form-control" />
                </div>
            </div>
            <br />
        </div>                    
    </fieldset>
</asp:Panel>
</asp:Content> 