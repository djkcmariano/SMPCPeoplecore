<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="DTRUpload.aspx.vb" Inherits="Secured_DTRUpload" %>

<asp:content id="cntNo" contentplaceholderid="cphBody" runat="server">
<asp:UpdatePanel runat="server" ID="UpdatePanelFU">
<ContentTemplate>
<asp:Panel runat="server" ID="Panel1">        
<div class="page-content-wrap">
    <div class="row">
        <div class="panel panel-default">
            <br />
            <div class="panel-body">
                <div class="row">
                    <fieldset class="form" id="fsMain">
                    <div  class="form-horizontal">
                        <div class="form-group">
                                <label class="col-md-2 control-label">DTR Source :</label>
                                <div class="col-md-6">
                                <asp:Dropdownlist ID="cboDatasourceNo" DataMember="EDTRSource" runat="server" CssClass="form-control" 
                                ></asp:Dropdownlist>
                                </div>
                        </div>
   
                        <div class="form-group">
                                <label class="col-md-2 control-label">Machine :</label>
                                <div class="col-md-6">
                                <asp:Dropdownlist ID="cboFPMachineNo" DataMember="EFPMachine" runat="server" CssClass="form-control"
                                ></asp:Dropdownlist>
                            </div>
                        </div>
   
                        <div class="form-group">
                                <label class="col-md-2 control-label">Select filename :</label>
                                <div class="col-md-6">
                                <asp:FileUpload ID="txtFile" runat="server" Width="350" />
                            </div>
                        </div>
   
        
                        <div class="form-group">
                                <label class="col-md-2 control-label"></label>
                                <div class="col-md-6">

                                <asp:Button runat="server"  ID="lnkSubmit" CssClass="form-control-search" OnClick= "btnUpload_Click" Text="Upload"></asp:Button>
   
                            </div>
                        </div>                     
                    </div>                                                
                </fieldset>                                        
                </div>                
            </div>
        </div>
    </div>
</div>
</asp:Panel>
</ContentTemplate>
<Triggers>
    <asp:PostBackTrigger ControlID="lnkSubmit" />
</Triggers>
</asp:UpdatePanel>    
</asp:content>

