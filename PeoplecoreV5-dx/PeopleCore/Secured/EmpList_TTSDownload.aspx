<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpList_TTSDownload.aspx.vb" Inherits="Secured_EmpList_TTSDownload" %>

<asp:content id="cntNo" contentplaceholderid="cphBody" runat="server">

    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkSubmit" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel runat="server" ID="Panel1">
                <br />
                <br />
                <fieldset class="form" id="fsMain">
                    <div  class="form-horizontal">
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                            Section / Store :</label>
                            <div class="col-md-6">
                                <asp:Dropdownlist ID="cboSectionNo" DataMember="ESection" runat="server" CssClass="form-control" 
                    >
                                </asp:Dropdownlist>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                            </label>
                            <div class="col-md-6">
                                <asp:Button runat="server"  ID="lnkSubmit" CssClass="form-control-search" OnClick= "btnUpload_Click" Text="Create Text (201)">
                                </asp:Button>
                            </div>
                        </div>
                        <br />
                        <br />
                    </div>
                </fieldset>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>

