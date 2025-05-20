<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="PayLastBonusDetiList.aspx.vb" Inherits="Secured_PayLastBonusDetiList" Theme="PCoreStyle" %>
<%@ Register Src="~/Include/HeaderInfo.ascx" TagName="HeaderInfo" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">


<script type="text/javascript">

    function grid_ContextMenu(s, e) {
        if (e.objectType == "row")
            hiddenfield.Set('VisibleIndex', parseInt(e.index));
        pmRowMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
    }

</script>

    <uc:HeaderInfo runat="server" ID="HeaderInfo1" />

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
                                        <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                                    </ul>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkExportDetl" />
                                </Triggers>
                            </asp:UpdatePanel>                                                                                                                                                    
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="PayBonusDetiNo" Width="100%"
                                OnFillContextMenuItems="MyGridView_FillContextMenuItems">                                                                                   
                                    <Columns>   
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." />                       
                                        <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="HiredDate" Caption="Date Hired" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="RegularizedDate" Caption="Date Regularized" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="SeparatedDate" Caption="Separated Date" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="BonusTypeDesc" Caption="Bonus Type" />
                                        <dx:GridViewDataTextColumn FieldName="PercentFactor" Caption="Factor" />
                                        <dx:GridViewDataTextColumn FieldName="YearsServe" Caption="LS" />
                                        <dx:GridViewDataTextColumn FieldName="DaysServeCutOffDate" Caption="No. of Days Served" />
                                        <dx:GridViewDataTextColumn FieldName="lwop" Caption="LWOP" />
                                        <dx:GridViewDataTextColumn FieldName="GrossAmount" Caption="Gross" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        <dx:GridViewDataTextColumn FieldName="Adjustment" Caption="Adjustment" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        <dx:GridViewDataTextColumn FieldName="NetPay" Caption="Net Pay" PropertiesTextEdit-DisplayFormatString="{0:N2}" /> 
                                    </Columns>    
                                    <ClientSideEvents ContextMenu="grid_ContextMenu" />                        
                                </dx:ASPxGridView> 
                                <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />  
                                
                                <dx:ASPxPopupMenu ID="pmRowMenu" runat="server" ClientInstanceName="pmRowMenu">
                                    <Items>
                                        <dx:MenuItem Text="Report" Name="Name">
                                            <Template>
                                                <asp:LinkButton runat="server" ID="lnkPrint" OnClick="lnkPrint_Click" CssClass="control-primary" Font-Size="small" OnPreRender="lnkPrint_PreRender" Text="Bonus Basis Report" />
                                            </Template>
                                        </dx:MenuItem>
                                    </Items>
                                    <ItemStyle Width="250px"></ItemStyle>
                                </dx:ASPxPopupMenu>
                                <dx:ASPxHiddenField ID="hf" runat="server" ClientInstanceName="hiddenfield" />  
                                                         
                            </div>
                        </div>                                                   
                    </div>                   
                </div>
            </div>
        </div>            
    </Content>
</uc:Tab>

     
</asp:Content>

