<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_default" ValidateRequest="true" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en" class="body-full-height">
    <head runat="server">        
        <!-- META SECTION -->
        <title>PeopleCore Version 5</title>            
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />        
        <link rel="icon" href="favicon.ico" type="image/x-icon" />
        <!-- END META SECTION -->        
        <!-- CSS INCLUDE -->        
        <link rel="stylesheet" type="text/css" id="theme" href="css/theme-default.css"/>
        <link rel="stylesheet" type="text/css" id="Link1" href="css/pcore/CSS.css" />
        <!-- EOF CSS INCLUDE 
        
        <!-- START PRELOADS -->
        <audio id="audio-alert" src="audio/alert.mp3" preload="auto"></audio>
        <audio id="audio-fail" src="audio/fail.mp3" preload="auto"></audio>
        <!-- END PRELOADS -->                        
        
        <!-- START SCRIPTS -->
        <!-- START PLUGINS -->
        <script type="text/javascript" src="js/plugins/jquery/jquery.min.js"></script>
        <script type="text/javascript" src="js/plugins/jquery/jquery-ui.min.js"></script>
        <script type="text/javascript" src="js/plugins/bootstrap/bootstrap.min.js"></script>        
        <!-- END PLUGINS -->
        
        <!-- START THIS PAGE PLUGINS-->        
        <script type='text/javascript' src="js/plugins/icheck/icheck.min.js"></script>
        <script type="text/javascript" src="js/plugins/mcustomscrollbar/jquery.mCustomScrollbar.min.js"></script>               
        <!-- END THIS PAGE PLUGINS-->  
        
        <!-- START TEMPLATE -->
        <script type="text/javascript" src="js/settings.js"></script>        
        <script type="text/javascript" src="js/plugins.js"></script>        
        <script type="text/javascript" src="js/actions.js"></script>        
        <!-- END TEMPLATE -->

          <!--START VALIDATOR-->
        <script type="text/javascript" src="js/pcore/jquery.validate.js"></script>
        <script type="text/javascript" src="js/jquery.validation.net.webforms.min.js"></script>  

        <!-- END SCRIPTS -->         
        <script type="text/javascript">
            function Success(msg) {
                $("#message-box-success").toggleClass("open");
                $('#message-box-success .mb-container .mb-middle .mb-content').text(msg);
            }
            function Information(msg) {
                $("#message-box-info").toggleClass("open");
                $('#message-box-info .mb-container .mb-middle .mb-content').text(msg);
            }
            function Warning(msg) {
                $("#message-box-warning").toggleClass("open");
                $('#message-box-warning .mb-container .mb-middle .mb-content').text(msg);
            }
            function Critical(msg) {
                $("#message-box-danger").toggleClass("open");
                $('#message-box-danger .mb-container .mb-middle .mb-content').text(msg);
            }
        </script>
                                           
    </head>
    <body>                                                    
        <!-- success -->
        <div class="message-box message-box-info animated fadeIn" id="message-box-success">
        <%--<div class="message-box message-box-success animated fadeIn" id="message-box-success">--%>
            <div class="mb-container">
                <div class="mb-middle">
                    <div class="mb-title"><span class="fa fa-check"></span>Success</div>
                    <div class="mb-content">                    
                    </div>
                    <div class="mb-footer">
                        <button class="btn btn-default btn-lg pull-right mb-control-close">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- success -->
        <!-- info -->
        <div class="message-box message-box-info animated fadeIn" id="message-box-info">
            <div class="mb-container">
                <div class="mb-middle">
                    <div class="mb-title"><span class="fa fa-info"></span>Information</div>
                    <div class="mb-content">                        
                    </div>
                    <div class="mb-footer">
                        <button class="btn btn-default btn-lg pull-right mb-control-close">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- end info -->
        <!-- warning -->
        <div class="message-box message-box-warning animated fadeIn" id="message-box-warning">
            <div class="mb-container">
                <div class="mb-middle">
                    <div class="mb-title"><span class="fa fa-warning"></span>Warning</div>
                    <div class="mb-content">                        
                    </div>
                    <div class="mb-footer">
                        <button class="btn btn-default btn-lg pull-right mb-control-close">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- end warning -->
        <!-- danger -->
        <div class="message-box message-box-danger animated fadeIn" id="message-box-danger">
            <div class="mb-container">
                <div class="mb-middle">
                    <div class="mb-title"><span class="fa fa-times"></span>Error</div>
                    <div class="mb-content">                        
                    </div>
                    <div class="mb-footer">
                        <button class="btn btn-default btn-lg pull-right mb-control-close">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- end danger --> 
        <div class="login-container lightmode">
            <form id="aspnetForm" runat="server" class="form-horizontal"  autocomplete="off">                
            <asp:ScriptManager runat="server" ID="ScriptManager1" />
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>     
            <div class="login-box animated fadeInDown">
                <div class="login-logo"></div>
                <div class="login-body">
                    <div class="login-title">Log In to your account</div>                                                           

                    <div class="form-group">
                        <div class="col-md-12">                            
                            <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" placeholder="Username" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password" placeholder="Password" />                            
                        </div>
                    </div>
                    <div class="form-group" runat="server" id="divCode" style="display:none;">
                        <div class="col-md-12">
                            <asp:DropdownList runat="server" ID="cboPayLocNo" CssClass="form-control" />                            
                        </div>
                    </div>
                    <div class="form-group">
                       <div class="col-md-6">
                            <a href="frmforgotpassword.aspx" class="btn btn-link btn-block">Forgot your password?</a>
                        </div>
                        <div class="col-md-6">                            
                            <asp:Button runat="server" ID="btnLogin" OnClick="btnLogin_Click" CssClass="btn btn-info btn-block fsHeader" Text="Log In" Visible="false" />                            
                            <asp:Button runat="server" ID="Button2" OnClick="Button2_Click" CssClass="btn btn-info btn-block fsHeader" Text="Log In" />
                            <asp:Button runat="server" ID="Button3" OnClick="Button3_Click" CssClass="btn btn-info btn-block fsHeader" Text="PrincipalContext" Visible="false" />
                        </div>
                    </div>                                                                         
                    <div class="login-footer">
                        <div class="pull-left">                        
                        </div>
                        <div class="pull-right">                            
                            <asp:LinkButton runat="server" ID="lnkAbout" OnClick="lnkAbout_Click" Text="About" />&nbsp;|&nbsp;<asp:LinkButton runat="server" ID="lnkPrivacy" OnClick="lnkPrivacy_Click" Text="Privacy" />&nbsp;|&nbsp;<asp:LinkButton runat="server" ID="lnkContactUs" OnClick="lnkContactUs_Click" Text="Contact Us" />                            
                        </div>
                    </div>                                                                                                                                                                                                                                             
                </div>
                <div style="color:#fff;text-align:center"><asp:Label runat="server" ID="lblVersion" /></div>
            </div>            

            <asp:Button ID="Button1" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" BackgroundCssClass="modalBackground" />
            <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none;height:460px">
                <fieldset class="form" id="fsMain">                    
                    <div class="cf popupheader">
                        <h4>&nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" OnClick="lnkClose_Click" />
                    </div>                    
                    <div class="col-md-12" style=" overflow:auto;height:350px">                        
                        <asp:Label runat="server" ID="lblContent" />                        
                    </div>
                    <div class="col-md-12" style="text-align:center" runat="server" id="divButton">
                        <br />
                        <asp:Button runat="server" ID="btnAccept" OnClick="btnAccept_Click" CssClass="btn btn-info" Text="Accept" />&nbsp;
                        <asp:Button runat="server" ID="btnCancel" OnClick="btnCancel_Click" CssClass="btn btn-info" Text="Cancel" />
                    </div>                                                              
                    <br /><br />
                </fieldset>
            </asp:Panel> 

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="Button2" />
                <asp:PostBackTrigger ControlID="btnLogin" />                
            </Triggers>
            </asp:UpdatePanel>

            </form>            
        </div>           
    </body>
</html>
