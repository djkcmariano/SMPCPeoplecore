<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpHRANMassEdit_Employee.aspx.vb" Inherits="Secured_EmpHRANEdit_Allowance" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">


     <script type="text/javascript">
         function cbCheckAll_CheckedChanged(s, e) {
             grdMain.PerformCallback(s.GetChecked().toString());
         }
    </script>


    <uc:Tab runat="server" ID="Tab">
        <Header>               
            <asp:Label runat="server" ID="lbl" /> 
            <div style="display:none;">
                <asp:CheckBox ID="txtIsPosted" runat="server"></asp:CheckBox>
            </div>     
        </Header>        
        <Content>
        <br />
        <div class="page-content-wrap" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-2">
                            &nbsp;
                        </div>
                        <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkUpload" OnClick="lnkUpload_Click" Text="Upload" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkGenerate" OnClick="lnkGenerate_Click" Text="Generate Employee List" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                    <uc:ConfirmBox runat="server" ID="cfbGenerate" TargetControlID="lnkGenerate" ConfirmMessage="Generate employee list. Proceed?"  />
                                </ul>
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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="HRANMassDetiNo"
                                OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                   
                                    <Columns>                                        
                                        <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                        <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                        <dx:GridViewDataTextColumn FieldName="EmployeeStatDesc" Caption="Employee Status" Visible="false" />
                                        <dx:GridViewDataDateColumn FieldName="HiredDate" Caption="Date Hired" />
                                        <dx:GridViewDataDateColumn FieldName="RegularizedDate" Caption="Date Regularized" />
                                        <dx:GridViewDataTextColumn FieldName="LS" Caption="LS" />
                                        <dx:GridViewDataTextColumn FieldName="OCurrentSalary" Caption="Old Rate"  PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        <dx:GridViewDataTextColumn FieldName="NCurrentSalary" Caption="New Rate"  PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status"/>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
					                        <HeaderTemplate>
                                                <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                                </dx:ASPxCheckBox>
                                            </HeaderTemplate>
				                        </dx:GridViewCommandColumn>
                                    </Columns>                            
                                </dx:ASPxGridView> 
                                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                 
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>
        </div>                                      
    </Content>        
    </uc:Tab>  
    
     <asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" CancelControlID="lnkClose2" PopupControlID="Panel3" TargetControlID="Button1" />
<asp:Panel id="Panel3" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsUpload">      
         <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <asp:Linkbutton runat="server" ID="lnkClose2" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                    <asp:LinkButton runat="server" ID="lnkSave2" CssClass="fa fa-floppy-o submit fsUpload lnkSave2" OnClick="lnkSave2_Click"  />   
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkSave2" />
                </Triggers>
            </asp:UpdatePanel>            
         </div>         
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group">
                <label class="col-md-9 control-label has-space"><code>File must be .csv with following column : Employee No., New Rate</code></label>                
                <br />
            </div>            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Filename :</label>
                <div class="col-md-7">
                    <asp:FileUpload runat="server" ID="fuFilename" Width="100%" CssClass="required" />                   
                </div>
            </div> 
           <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtDescription2" runat="server" Rows="4" textmode="MultiLine" CssClass="form-control" />
                </div>
            </div>            
            <br /> 
        </div>         
        <div class="cf popupfooter">
        </div> 
    </fieldset>
</asp:Panel> 
      
</asp:Content>

