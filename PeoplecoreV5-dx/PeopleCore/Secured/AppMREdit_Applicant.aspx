<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AppMREdit_Applicant.aspx.vb" Inherits="Secured_AppMREdit_Applicant" Theme="PCoreStyle" %>
<%@ Register Src="~/Include/Info.ascx" TagName="Info" TagPrefix="uc" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<script type="text/javascript">
    function SelectAllCheckboxes(spanChk) {

        // Added as ASPX uses SPAN for checkbox
        var oItem = spanChk.children;
        var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
        xState = theBox.checked;
        elm = theBox.form.elements;

        for (i = 0; i < elm.length; i++)
            if (elm[i].type == "checkbox" && elm[i].name.indexOf("txtIsSelect") > 0 &&
            elm[i].id != theBox.id) {
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
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
                            <h4>&nbsp;</h4>                                
                        </div>
                        <div>
                            
                        </div>                           
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">                                
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" KeyFieldName="MRHiredMassNo" SkinID="grdDX">
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("MRHiredMassNo") %>' OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="Name" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnk" Text='<%# Bind("Fullname") %>' CommandArgument='<%# Eval("ID") & "|" & Eval("IsApplicant") & "|" & Eval("Fullname") %>' OnClick="lnk_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="ServedDate" Caption="Date Invited" />
                                        <dx:GridViewDataTextColumn FieldName="ApplicantTypeDesc" Caption="Applicant Type" />
                                        <dx:GridViewDataTextColumn FieldName="ApplicantStatDesc" Caption="Applicant Status" />
                                        <dx:GridViewDataTextColumn FieldName="ActionStatDesc" Caption="Next Step" />
                                        <dx:GridViewDataColumn Caption="Application<br />Form" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" Visible="false">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkForm" CommandArgument='<%# Eval("ID") & "|" & Eval("IsApplicant") & "|" & Eval("Fullname") %>' OnClick="lnkForm_Click" CssClass="fa fa-print" OnPreRender="lnkPrint_PreRender" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="PDS" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" Visible="false">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkPDS" CommandArgument='<%# Eval("ID") & "|" & Eval("IsApplicant") & "|" & Eval("Fullname") %>' OnClick="lnkPDS_Click" CssClass="fa fa-print" OnPreRender="lnkPrint_PreRender" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="CAF" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" Visible="false">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkCAF" CommandArgument='<%# Eval("ID") & "|" & Eval("IsApplicant") & "|" & Eval("Fullname") %>' OnClick="lnkCAF_Click" CssClass="fa fa-print" OnPreRender="lnkPrint_PreRender" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>                                        
                                    </Columns>                            
                                </dx:ASPxGridView>                                                                   
                            </div>
                        </div>                                                                   
                    </div>                   
                </div>
            </div>
        </div>

        <uc:Info runat="server" ID="Info1" />

        <asp:Button ID="Button1" runat="server" style="display:none" />
        <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
        <asp:Panel id="Panel1" runat="server" CssClass="entryPopup2">
            <fieldset class="form" id="fsMain">                    
                <div class="cf popupheader">
                    <h4>&nbsp;</h4>
                    <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
                </div> 
                <br />  
                                                                     
                <div class="entryPopupDetl2 form-horizontal">                        
                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Applicant Status :</label>
                        <div class="col-md-7">
                            <asp:HiddenField runat="server" ID="hifMRHiredMassNo" />
                            <asp:DropDownList runat="server" ID="cboApplicantStatNo" DataMember="EApplicantStat"  CssClass="form-control required">
                            </asp:DropDownList>
                        </div>
                    </div>                                         
                    
                </div>
                <br /><br />                    
            </fieldset>
        </asp:Panel>
        
        </Content>
    </uc:Tab>

</asp:Content>

