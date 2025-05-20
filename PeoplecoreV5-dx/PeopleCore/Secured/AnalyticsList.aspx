<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AnalyticsList.aspx.vb" Inherits="Secured_SecCMSTemplateContent" %>
<%@ Register Assembly="DevExpress.XtraCharts.v15.2.Web, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<script type="text/javascript">
    function GetRecord(source, eventArgs) {
        document.getElementById('<%= hifNo.ClientID %>').value = eventArgs.get_value();
    }
</script>
<div class="page-content-wrap">

<div class="row">
    <div class="panel panel-default">
        <div class="panel-body">
            <fieldset class="form" id="fsd">
                <div  class="form-horizontal">
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Title :</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtUserReportTitle" runat="server" CssClass="form-control required"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Button runat="server" ID="lnkSave" CssClass="btn btn-primary submit fsd" Text="Save" OnClick="btnSave_Click"  />  
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Datasource :</label>
                        <div class="col-md-4">
                            <asp:Dropdownlist ID="cboReportNo" runat="server" CssClass="form-control required" AutoPostBack="true" />
                        </div>
                        <div class="col-md-3" style="padding-top:5px;">
                            <asp:LinkButton runat="server" ID="lnkRefresh" OnClick="lnkRefresh_Click"  CausesValidation="false" >
                                <i class="fa fa-refresh fa-1x"></i>&nbsp;Load Data
                            </asp:LinkButton>
                        </div>
                    </div>
                    <%--<div class="form-group">
                        <span><asp:Label runat="server" ID="lblParamTitle" Font-Bold="true" class="col-md-4 control-label has-space"></asp:Label></span>&nbsp;&nbsp
                    </div> 
                    <div class="form-group">
                        <div runat="server" ID="pform" />
                        <asp:HiddenField runat="server" ID="hifNo" />
                    </div>--%>
                </div>
            </fieldset >
        </div>
    </div>
</div>

<div class="row">
    <div class="panel panel-default">
        <div class="panel-body">
            <fieldset class="form" id="fsMain">
                <div  class="form-horizontal">
                    <span><asp:Label runat="server" ID="lblParamTitle" Font-Bold="true"></asp:Label></span>&nbsp;&nbsp
                    <asp:Panel runat="server" ID="pform" />
                    <asp:HiddenField runat="server" ID="hifNo" />
                    <%--<ajaxToolkit:AutoCompleteExtender runat="server" ID="drpAC"></ajaxToolkit:AutoCompleteExtender>--%>
                </div>
            </fieldset >
        </div>
    </div>
</div>

<div class="row">
    
    <div class="col-md-6">
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>Column</b>
            </div>
            <div class="panel-body">

            <div class="col-md-8">
                <div  class="form-horizontal">
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Field</label>
                        <div class="col-md-7">
                            <asp:DropDownList runat="server" ID="ddlField" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlField_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div> 
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Value</label>
                        <div class="col-md-7">
                            <asp:Dropdownlist ID="ddlSummaryType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSummaryType_SelectedIndexChanged" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div  class="form-horizontal">
                    <div class="form-group">
                        <label class="radio-inline">
                            <asp:Checkbox ID="txtShowColumnTotals" Text="&nbsp; Show Sub Total" runat="server" AutoPostBack="true" OnCheckedChanged="txtShowColumnTotals_CheckedChanged"  />
                        </label>
                    </div>
                    <div class="form-group">
                        <label class="radio-inline">
                            <asp:Checkbox ID="txtShowColumnGrandTotals" Text="&nbsp; Show Grand Total" runat="server" AutoPostBack="true" OnCheckedChanged="txtShowColumnGrandTotals_CheckedChanged" />
                        </label>
                    </div>
                        
                </div>
            </div>


            </div>
        </div>
    </div>

    <div class="col-md-6">
       <div class="panel panel-default">
            <div class="panel-heading">
                <b>Row</b>
            </div>
            <div class="panel-body">
                
                <div class="col-md-8">
                    <div  class="form-horizontal">
                        <div class="form-group">
                            <label class="col-md-2 control-label has-space">Field</label>
                            <div class="col-md-9">
                                <asp:DropDownList runat="server" ID="cboFieldNo" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div> 

                        <div class="form-group">
                            <label class="radio-inline">
                                <asp:Checkbox ID="sum" Text="&nbsp; Sum" runat="server" AutoPostBack="true" />&nbsp;&nbsp;
                                <asp:Checkbox ID="average" Text="&nbsp; Average" runat="server" AutoPostBack="true" />&nbsp;&nbsp;
                                <asp:Checkbox ID="count" Text="&nbsp; Count" runat="server" AutoPostBack="true" />&nbsp;&nbsp;
                                <asp:Checkbox ID="max" Text="&nbsp; Max" runat="server" AutoPostBack="true" />&nbsp;&nbsp;
                                <asp:Checkbox ID="min" Text="&nbsp; Min" runat="server" AutoPostBack="true" />
                            </label>
                        </div>


                    </div>
                 </div>
                 
                 <div class="col-md-4">
                    <div  class="form-horizontal">
                        <div class="form-group">
                            <label class="radio-inline">
                                <asp:Checkbox ID="txtShowRowTotals" Text="&nbsp; Show Sub Total" runat="server" AutoPostBack="true" OnCheckedChanged="txtShowRowTotals_CheckedChanged" />
                            </label>
                        </div>
                        <div class="form-group">
                            <label class="radio-inline">
                                <asp:Checkbox ID="txtShowRowGrandTotals" Text="&nbsp; Show Grand Total" runat="server" AutoPostBack="true" OnCheckedChanged="txtShowRowGrandTotals_CheckedChanged" />
                            </label>
                        </div>
                    </div>
                </div>  

            </div>
        </div> 
    </div>
    
</div>

<div class="row">
    <div class="panel panel-default">
        <div class="panel-heading">
            <div class="col-md-4">
                   <div  class="form-horizontal">
                        <div class="form-group">
                                <label class="col-md-3 control-label has-space">Page Size</label>
                                <div class="col-md-3">
                                    <asp:DropDownList runat="server" ID="cboPageSizeNo" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboPageSize_SelectedIndexChanged">
                                        <asp:ListItem Text="50" Value="50" />
                                        <asp:ListItem Text="100" Value="100" />
                                        <asp:ListItem Text="200" Value="200" />
                                        <asp:ListItem Text="300" Value="300" />
                                        <asp:ListItem Text="400" Value="400" />
                                        <asp:ListItem Text="500" Value="500" />
                                        <asp:ListItem Text="1000" Value="1000" />
                                        <asp:ListItem Text="5000" Value="5000" />
                                        <asp:ListItem Text="10000" Value="10000" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                    </div>
            </div>
            <div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>               
                        <ul class="panel-controls">                      
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>                            
                        </ul>      
                        &nbsp;&nbsp;    
                        <ul class="panel-controls">                      
                            <li>
                            <asp:DropDownList runat="server" ID="cboFileTypeExportNo1" CssClass="form-control">
                                        <asp:ListItem Text="Excel" Value="1" />
                                        <asp:ListItem Text="Word" Value="2" />
                                        <asp:ListItem Text="PDF" Value="3" />
                            </asp:DropDownList> 
                            </li>                            
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
                    <dx:ASPxPivotGrid ID="pvtGrid" runat="server" Width="100%" EnableCallBacks="False">
                        <OptionsFilter NativeCheckBoxes="true" />
                        <OptionsView HorizontalScrollBarMode="Auto" />
                    </dx:ASPxPivotGrid> 
                    <dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="pvtGrid" />                           
                </div>
            </div>
                   
        </div>
    </div>
</div>
     
<div class="row">
    <div class="panel panel-default">
        <div class="panel-heading">
            <div class="col-md-4">
                
            </div>
            <div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                    <ContentTemplate>                    
                        <ul class="panel-controls">                           
                            <li><asp:LinkButton runat="server" ID="lnkExport2" OnClick="lnkExport2_Click" Text="Export" CssClass="control-primary" /></li>                            
                        </ul>   
                        &nbsp;&nbsp;    
                        <ul class="panel-controls">                      
                            <li>
                            <asp:DropDownList runat="server" ID="cboFileTypeExportNo2" CssClass="form-control">
                                        <asp:ListItem Text="Excel" Value="1" />
                                        <asp:ListItem Text="Word" Value="2" />
                                        <asp:ListItem Text="PDF" Value="3" />
                            </asp:DropDownList> 
                            </li>                            
                        </ul>                                            
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExport2" />
                    </Triggers>
                </asp:UpdatePanel>                                                                      
            </div>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-3">
                    <div  class="form-horizontal">
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Chart</label>
                        <div class="col-md-8">
                            <asp:Dropdownlist ID="cboChartTypeNo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboChartType_SelectedIndexChanged" />
                        </div>
                    </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div  class="form-horizontal">
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Height</label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtHeight" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtChartSize_OnTextChanged"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers" TargetControlID="txtHeight" />
                        </div>
                    </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div  class="form-horizontal">
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Width</label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtWidth" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtChartSize_OnTextChanged"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" TargetControlID="txtWidth" />
                        </div>
                    </div>
                    </div>
                </div>
                
            </div>  
            <br />       
            <div class="row">
                <div class="table-responsive">
                    <dxchartsui:WebChartControl ID="WebChartControl1" runat="server" CrosshairEnabled="True" DataSourceID="pvtGrid" SeriesDataMember="Series" >
                    </dxchartsui:WebChartControl>                                              
                </div>
            </div>
        </div>
    </div>
</div>
</div>

</asp:Content>

