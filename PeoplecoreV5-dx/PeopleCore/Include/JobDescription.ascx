<%@ Control Language="VB" AutoEventWireup="false" CodeFile="JobDescription.ascx.vb" Inherits="Include_JobDescription" %>

<style type="text/css">
    .header
    {
        background-color: #f7f7f7;
        border-top: solid 1px #c1c1c1;
        border-bottom: solid 1px #c1c1c1;
        padding:5px;
        font-weight:bold;
        margin-bottom:10px;            
    }                             
</style>

<asp:Button ID="Button2" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button2" PopupControlID="pInfomation"  BackgroundCssClass="modalBackground" />
<asp:Panel id="pInfomation" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />
        </div>
        <div class="container-fluid entryPopupDetl">        
            <div class="page-content-wrap">              
                <div class="col-md-12">                                                        
                    <div class="form-horizontal">                     
                        <asp:Literal runat="server" ID="lContent" />
                        <br />
                    </div>                    
                </div>
            </div>
        </div>
    </fieldset>
</asp:Panel>