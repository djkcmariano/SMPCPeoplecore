<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmForgotPassword.aspx.vb" Inherits="frmForgotPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en" class="body-full-height">
    <head>        
        <!-- META SECTION -->
        <title>PeopleCore Version 5</title>            
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />        
        <link rel="icon" href="favicon.ico" type="image/x-icon" />
        <!-- END META SECTION -->        
        <!-- CSS INCLUDE -->        
        <link rel="stylesheet" type="text/css" id="theme" href="css/theme-default.css"/>
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
        <div class="message-box message-box-success animated fadeIn" id="message-box-success">
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
            <div class="login-box animated fadeInDown" style="width:450px;">
                <div class="login-logo"></div>
                <div class="login-body">
                    <div class="login-title">Forgot password!</div>
                    <form id="aspnetForm" runat="server" class="form-horizontal"  autocomplete="off">
                    <div class="form-group">
                        <div class="col-md-12">                            
                            <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" placeholder="Username" />
                        </div>
                    </div>
                    
                    <div class="form-group">
                        <div class="col-md-12">                            
                            <dx:ASPxCaptcha ID="Captcha" runat="server">
                                <ValidationSettings SetFocusOnError="true" ErrorDisplayMode="Text" />
                            </dx:ASPxCaptcha>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-6">
                            
                        </div>
                        <div class="col-md-6">                            
                            <asp:Button runat="server" ID="btnLogin" OnClick="btnSave_Click" CssClass="btn btn-info btn-block" Text="Submit" />                            
                        </div>
                    </div>                    
                    </form>                
                    <%--<div class="login-footer">
                        <div class="pull-left">                        
                        </div>
                        <div class="pull-right">
                            <a href="http://www.peoplecore.net/">About</a> |
                            <a href="#">Privacy</a> |
                            <a href="http://www.peoplecore.net/index.php/contact-us/">Contact Us</a>                             
                        </div>
                    </div>--%>
                </div>
            </div>            
        </div>                       
    </body>
</html>

