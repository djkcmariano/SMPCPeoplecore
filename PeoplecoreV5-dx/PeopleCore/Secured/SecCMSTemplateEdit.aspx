<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="SecCMSTemplateEdit.aspx.vb" Inherits="Secured_SecCMSTemplateEdit" EnableEventValidation="false" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

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
                                <%--<SettingsDialogs>
                                    <InsertImageDialog>
                                        <SettingsImageUpload UploadFolder="~/UploadFiles/Images/">
                                            <ValidationSettings AllowedFileExtensions=".jpe,.jpeg,.jpg,.gif,.png" MaxFileSize="500000">
                                            </ValidationSettings>
                                        </SettingsImageUpload>
                                    </InsertImageDialog>
                                </SettingsDialogs>--%>
                            </dx:ASPxHtmlEditor>
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
                


</asp:content>
