<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SecCMSTemplateContent.aspx.vb" Inherits="Secured_SecCMSTemplateContent" %>
<%@ Register Assembly="DevExpress.XtraCharts.v15.2.Web, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <asp:Label runat="server" ID="lblTitle"></asp:Label>
                    </h4>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                            <dx:ASPxHtmlEditor ID="ASPxHtmlEditor1" runat="server" Width="100%">
                                <Settings AllowHtmlView="False" />
                                <SettingsDialogs>
                                    <InsertImageDialog>
                                        <SettingsImageUpload UploadFolder="~/UploadFiles/Images/">
                                            <ValidationSettings AllowedFileExtensions=".jpe,.jpeg,.jpg,.gif,.png" MaxFileSize="500000">
                                            </ValidationSettings>
                                        </SettingsImageUpload>
                                    </InsertImageDialog>
                                </SettingsDialogs>
                            </dx:ASPxHtmlEditor>

                            <dx:ASPxPivotGrid ID="pvtGrid" runat="server" Width="100%" EnableCallBacks="False">
                                <OptionsFilter NativeCheckBoxes="true" />
                            </dx:ASPxPivotGrid>                            
                        </div>
                    </div>
                    <br /><br />
                    <div class="row">
                        <div class="table-responsive">
                            <dxchartsui:WebChartControl ID="WebChartControl1" runat="server" CrosshairEnabled="True" DataSourceID="pvtGrid" SeriesDataMember="Series" Width="800px" />
                        </div> 
                    </div>
                    <br />
                    <div class="row pull-right">
                        <div class="col-md-12">
                            <asp:Button ID="btnAdd" Text="Save" runat="server" CausesValidation="false" CssClass="btn btn-primary" ToolTip="Click here to save changes" OnClick="btnSave_Click" >
                            </asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

