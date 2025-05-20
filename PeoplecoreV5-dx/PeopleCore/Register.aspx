<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Register.aspx.vb" Inherits="Register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<head id="Head1" runat="server">        
    <!-- META SECTION -->
    <title>PeopleCore Version 5</title>            
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />        
    <link rel="icon" href="favicon.ico" type="image/x-icon" />

    <link href="css/pcore/css.css" rel="stylesheet" type="text/css" />    
    <link href="css/pcore/cssPager.css" rel="stylesheet" type="text/css" />
    <%--<link href="css/pcore/JQueryVal.css" rel="stylesheet" type="text/css" />--%>
    <link href="css/pcore/jqdialog.css" rel="stylesheet" type="text/css" />
    <link href="css/pcore/jquery-ui.css" rel="stylesheet" type="text/css" />   
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
    <script type='text/javascript' src='js/plugins/icheck/icheck.min.js'></script>
    <script type="text/javascript" src="js/plugins/mcustomscrollbar/jquery.mCustomScrollbar.min.js"></script>  
    
    <script type='text/javascript' src='js/plugins/noty/jquery.noty.js'></script>
    <script type='text/javascript' src='js/plugins/noty/layouts/topCenter.js'></script>
    <script type='text/javascript' src='js/plugins/noty/layouts/topLeft.js'></script>
    <script type='text/javascript' src='js/plugins/noty/layouts/topRight.js'></script>  
     <script type='text/javascript' src='js/plugins/noty/themes/default.js'></script>              
    <script type="text/javascript" src="js/demo_tables.js"></script>     
    <!-- END THIS PAGE PLUGINS-->  
        
    <!-- START TEMPLATE -->
    <script type="text/javascript" src="js/settings.js"></script>        
    <script type="text/javascript" src="js/plugins.js"></script>        
    <script type="text/javascript" src="js/actions.js"></script>                        
    <!-- END TEMPLATE -->

    <!--START VALIDATOR-->
    <script type="text/javascript" src="js/pcore/jquery.validate.js"></script>
    <script type="text/javascript" src="js/jquery.validation.net.webforms.min.js"></script>        

    <!--START LISTBOX MULTI-SELECT-->
    <link href="../css/bootstrap/bootstrap-multiselect.css"
        rel="stylesheet" type="text/css" />
    <script src="../css/bootstrap/bootstrap-multiselect.js"
        type="text/javascript"></script>
    <!-- END LISTBOX MULTI-SELECT -->

    <!-- END META SECTION -->            
    <asp:Literal runat="server" ID="lTheme" />                                           
    <script  type="text/javascript">
        $(function () {
            $("#aspnetForm").validateWebForm();
            $('.dropdown-toggle').dropdown();
            $('.dropdown-menu input, .dropdown-menu label, .dropdown-menu select').click(function (e) {
                e.stopPropagation();
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#aspnetForm").validateWebForm();
                //rebind dropdownmenu
                $('.dropdown-toggle').dropdown();
                $('.dropdown-menu input, .dropdown-menu label, .dropdown-menu select').click(function (e) {
                    e.stopPropagation();
                });
                adjustPanelEntry();
            });
        });

        $(window).resize(function () {
            adjustPanelEntry();
        });


        $.validator.setDefaults({
            errorPlacement: function (error, element) {
                if (element.parent('.input-group').length) {
                    error.insertAfter(element.parent());
                } else if (element.parent('.radio-inline').length) {
                    error.insertAfter(element.parent().parent());
                } else {
                    error.insertAfter(element);
                }
            }
        });
                         
    </script>
    <style type="text/css">
            .component a
            {
            	margin: 5px;             	
            	display: inline-block;            	           	            	
            }
    </style>
</head>
<body>
    <!-- START PAGE CONTAINER -->
    <form id="aspnetForm" runat="server">
        
    <!-- MESSAGE BOX-->
    <script type="text/javascript">
        function Success(msg) {
            $("#message-box-success").toggleClass("open");
            $('#message-box-success .mb-container .mb-middle .mb-content').text(msg);
        }
        function SuccessResponse(msg, url) {

            $("#message-box-success-response").toggleClass("open");
            $('#message-box-success-response .mb-container .mb-middle .mb-content').text(msg);
            $('#message-box-success-response .mb-container .mb-middle .mb-footer #btnresponse').click(function (e) {
                window.location.href = url;
            });
        }
        function PopupMessage(msg, frm) {

            $("#message-box-popup-message").toggleClass("open");
            $('#message-box-popup-message .mb-container .mb-middle .mb-content').text(msg);
            $('#message-box-popup-message .mb-container .mb-middle .mb-footer #btnok').click(function (e) {
                $find(frm).show();

            });
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

        function dialogResponseMDL(dialogTitle, fmessage, frm) {
            noty({
                text: fmessage,
                layout: 'topRight',
                buttons: [
                                { addClass: 'btn btn-default', text: 'Ok', onClick: function ($noty) {
                                    $find(frm).show();
                                    $noty.close();

                                }
                                }
                            ]
            })
            return false;
        }


        function dialogResponseAlert(fmessage, ftype) {
            noty({
                text: fmessage,
                layout: 'topRight',
                type: ftype,
                buttons: [
                                { addClass: 'btn btn-default', text: 'Ok', onClick: function ($noty) {
                                    $noty.close();

                                }
                                }
                            ]
            })
            return false;
        }


        function adjustPanelEntry() {
            $('.entryPopup').css({ 'height': 'auto' }); //Reset subpanel and ul height
            $('.entryPopupDetl').css({ 'height': 'auto' }); //Reset subpanel and ul height
            var windowHeight = $(window).height(); //Get the height of the browser viewport
            var panelsub = $('.entryPopup').height(); //Get the height of subpanel	
            var ulsub = $('.entryPopupDetl').height(); //Get the height of subpanel	
            var panelAdjust = windowHeight - 20; //Viewport height - 100px (Sets max height of subpanel)
            var ulAdjust = panelAdjust - 55; //Calculate ul size after adjusting sub-panel (27px is the height of the base panel)
            if (panelsub >= panelAdjust) {	 //If subpanel is taller than max height...
                $('.entryPopup').css({ 'height': panelAdjust }); //Adjust subpanel to max height
                $('.entryPopupDetl').css({ 'height': ulAdjust }); //Adjust subpanel ul to new size
            }
            else if (panelsub < panelAdjust) { //If subpanel is smaller than max height...
                $('.entryPopupDetl').css({ 'height': 'Auto' }); //Set subpanel ul to auto (default size)
            }
            var windowWidth = $(window).width(); //Get the width of the browser viewport
            var panelwidth = $('.entryPopup').width(); //Get the width of subpanel
            var panelwidthdetl = $('.entryPopupDetl').width();
            panelwidth = windowWidth - panelwidth;
            panelwidth = panelwidth / 2;
            $('.entryPopup').css({ 'left': panelwidth });
        };

        function adjustPanelEntry2() {
            $('.entryPopup2').css({ 'height': 'auto' }); //Reset subpanel and ul height
            $('.entryPopupDetl2').css({ 'height': 'auto' }); //Reset subpanel and ul height
            var windowHeight = $(window).height(); //Get the height of the browser viewport
            var panelsub = $('.entryPopup2').height(); //Get the height of subpanel	
            var ulsub = $('.entryPopupDetl2').height(); //Get the height of subpanel	
            var panelAdjust = windowHeight - 20; //Viewport height - 100px (Sets max height of subpanel)
            var ulAdjust = panelAdjust - 55; //Calculate ul size after adjusting sub-panel (27px is the height of the base panel)
            if (panelsub >= panelAdjust) {	 //If subpanel is taller than max height...
                $('.entryPopup2').css({ 'height': panelAdjust }); //Adjust subpanel to max height
                $('.entryPopupDetl2').css({ 'height': ulAdjust }); //Adjust subpanel ul to new size
            }
            else if (panelsub < panelAdjust) { //If subpanel is smaller than max height...
                $('.entryPopupDetl2').css({ 'height': 'Auto' }); //Set subpanel ul to auto (default size)
            }
            var windowWidth = $(window).width(); //Get the width of the browser viewport
            var panelwidth = $('.entryPopup2').width(); //Get the width of subpanel
            var panelwidthdetl = $('.entryPopupDetl2').width();
            panelwidth = windowWidth - panelwidth;
            panelwidth = panelwidth / 2;
            $('.entryPopup2').css({ 'left': panelwidth });
        };

    </script>      
    <!-- success response-->
    <div class="message-box message-box-success animated fadeIn" id="message-box-success-response">
        <div class="mb-container">
            <div class="mb-middle">
                <div class="mb-title"><span class="fa fa-check"></span>Success</div>
                <div class="mb-content">                    
                </div>
                <div class="mb-footer">
                    <button class="btn btn-default btn-lg pull-right mb-control-close" id="btnresponse">Ok</button>
                </div>
            </div>
        </div>
    </div>
    <!-- End success response -->

    <!-- Popup message-->
    <div class="message-box message-box-warning animated fadeIn" id="message-box-popup-message">
            <div class="mb-container">
                <div class="mb-middle">
                    <div class="mb-title"><span class="fa fa-warning"></span>Warning</div>
                    <div class="mb-content" style="font-size:14px;">                        
                    </div>
                    <div class="mb-footer">
                        <button class="btn btn-default btn-lg pull-right mb-control-close" id="btnok">Ok</button>
                    </div>
                </div>
            </div>
        </div>
    <!-- End popup message -->
          
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


        <!-- END MESSAGE BOX-->
        
        <!-- PAGE CONTENT -->            
            <div class="page-content">                
                <!-- START X-NAVIGATION VERTICAL -->                
                <ul class="x-navigation x-navigation-horizontal x-navigation-panel">
                    <!-- TOGGLE NAVIGATION -->
                    <li class="xn-icon-button">
                        <%--<a href="#" class="x-navigation-minimize"><span class="fa fa-dedent"></span></a>--%>
                    </li>
                    <!-- END TOGGLE NAVIGATION -->
                    <!-- SEARCH -->
                    <li class="xn-logo">
					    <div class="client"></div>
                        <img src="img/client_logo.png" class="pull-left" alt="Client Logo"/>
                    </li>   
                    <!-- END SEARCH -->                                        
                </ul>                
                <!-- END X-NAVIGATION VERTICAL -->                     
                
                <!-- START BREADCRUMB -->
                <ul class="breadcrumb">
                    <asp:PlaceHolder runat="server" ID="phBreadCrump" />                                        
                </ul>
                <!-- END BREADCRUMB -->
                             
                <!-- PAGE CONTENT WRAPPER -->                                                                 
                <div id="page-content-wrap-id" class="panel-body">
                    <div class="panel panel-default">
                    <asp:ScriptManager runat="server" ID="ScriptManager1" />
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                        <ContentTemplate>
                            <asp:Panel runat="server" ID="Panel1">        
                            <br /><br />            
                            <fieldset class="form" id="fsMain">
                                <div  class="form-horizontal">                                    
                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-required">Last Name :</label>
                                        <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control required" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-required">First Name :</label>
                                        <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control required" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-required">Middle Name :</label>
                                        <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtMiddleName" CssClass="form-control required" />
                                        </div>
                                    </div>                                                                                                            
                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-required">Date Of Birth :</label>
                                        <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtBirthDate" CssClass="form-control required" SkinID="txtdate" />
                                            <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtBirthDate" Format="MM/dd/yyyy" />
                                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtBirthDate" Mask="99/99/9999" MaskType="Date" />
                                            <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtBirthDate" Display="Dynamic" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space">Mobile No. :</label>
                                        <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control" />
                                        </div>
                                    </div>                                    
                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space">Email Address :</label>
                                        <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space">Password :</label>
                                        <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtPassword1" CssClass="form-control" TextMode="Password" MaxLength="22" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space">Confirm Password :</label>
                                        <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtPassword2" CssClass="form-control" TextMode="Password" MaxLength="22" />
                                        </div>
                                    </div>                                                                           
                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space"></label>
                                        <div class="col-md-4">            
                                            <asp:Button runat="server"  ID="btnSave" CssClass="btn btn-primary submit fsMain" Text="Submit" OnClick="btnSave_Click" />                                            
                                        </div>
                                    </div>
                                    <br /><br />                     
                                </div>                                                
                            </fieldset>
                        </asp:Panel>
                            <!-- Content -->                                                                                                                                                                                                             
                        </ContentTemplate>                                    
                    </asp:UpdatePanel>
                    <asp:UpdateProgress runat="server" ID="UpdateProgress1">
                        <ProgressTemplate>
                            <div style="background-color:#A6A6A6;filter:alpha(opacity=50);opacity:0.50; width: 100%; top: 0px; left: 0px; position:fixed; height:100%; z-index: 100002 !important;"></div>
                            <div style="margin:auto;filter: alpha(opacity=100);opacity: 1;vertical-align: middle;top: 45%;position:fixed;right: 45%;height: 60px;padding-left:15px;padding-right:15px;z-index:100003!important;">
                                <img src="../img/ajax-loader.gif" alt="progress" />
                            </div>                                            
                        </ProgressTemplate>                                    
                    </asp:UpdateProgress>
                    </div>
                </div>                                 
            </div>                        
            <!-- END PAGE CONTENT -->                                   
        </form>
    <!-- END PAGE CONTAINER -->                                                      
    </body>
