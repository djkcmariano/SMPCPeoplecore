   $(function () {
        InitializeValidation();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(onEachRequest);
    });


    function onEachRequest(sender, args) {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if ($("#aspnetForm").valid() == false) {

            if (args.get_postBackElement().id == 'ctl00_cphBody_btnSave') {
                args.set_cancel(true);
            }
            else {
                prm.abortPostBack();

            }
        }
    }

    //Processing of DTR

    $(document).ready(function () {
        // Allows the div.blockMsg style in CSS to
        //  override BlockUI's defaults.
        $.blockUI.defaults.css = {};
        // Add the BlockUI call as an onclick handler
        //  of the Save button.
        $addHandler($get('ctl00_cphBody_lnkProcess'), 'click', function () {
            document.getElementById('ctl00_cphBody_btnProcessOk').onclick = function () {
                //Get all input elements in Gridview
                var inputList = document.getElementsByTagName("input");
                var fcount = 0;
                for (var i = 0; i < inputList.length; i++) {
                    //The First element is the Header Checkbox
                    var headerCheckBox = inputList[0];
                    //Based on all or none checkboxes
                    //are checked check/uncheck Header Checkbox
                    if (inputList[i].type == "checkbox" && inputList[i].name.indexOf("txtIsSelect") > 0) {
                        if (inputList[i].checked) {
                            fcount = fcount + 1;
                        }
                    }
                }
                if (fcount == 1) {

                    //$('#tableholder').block({ message: null });

                    $.blockUI({ message: 'Processing', bindEvents: false });
                    // Add the progress indicator.

                    $('.blockMsg').addClass('progress');
                    // Get a reference to the PageRequestManager.
                    var prm = Sys.WebForms.PageRequestManager.getInstance();

                    // Unblock the form when a partial postback ends.
                    prm.add_endRequest(function (args, sender) {
                        // Instantly remove the progress indication element.
                        //$('#tableholder').unblock({ fadeOut: 0 });
                        $.unblockUI();
                    });
                }
            };

        });


        $addHandler($get('ctl00_cphBody_lnkProcessDisc'), 'click', function () {
            document.getElementById('ctl00_cphBody_btnProcessOkDisc').onclick = function () {
                //Get all input elements in Gridview
                var inputList = document.getElementsByTagName("input");
                var fcount = 0;
                for (var i = 0; i < inputList.length; i++) {
                    //The First element is the Header Checkbox
                    var headerCheckBox = inputList[0];
                    //Based on all or none checkboxes
                    //are checked check/uncheck Header Checkbox
                    if (inputList[i].type == "checkbox" && inputList[i].name.indexOf("txtIsSelect") > 0) {
                        if (inputList[i].checked) {
                            fcount = fcount + 1;
                        }
                    }
                }
                if (fcount == 1) {

                    //$('#tableholder').block({ message: null });

                    $.blockUI({ message: 'Processing', bindEvents: false });
                    // Add the progress indicator.

                    $('.blockMsg').addClass('progress');
                    // Get a reference to the PageRequestManager.
                    var prm = Sys.WebForms.PageRequestManager.getInstance();

                    // Unblock the form when a partial postback ends.
                    prm.add_endRequest(function (args, sender) {
                        // Instantly remove the progress indication element.
                        //$('#tableholder').unblock({ fadeOut: 0 });
                        $.unblockUI();
                    });
                }
            };

        });



    });

    // Bonus! Progress indicator preloading.
    var preload = document.createElement('img');
    preload.src = 'images/progress-indicator.gif';
    delete preload;

    function chkRequired(pIndex) {
        if (pIndex == 1) {
            document.getElementById("ctl00_cphBody_cboPayTypeNo").rules("add", { required: false });
            document.getElementById("ctl00_cphBody_cboPayClassNo").rules("add", { required: false });
           
        } else if (pIndex == 2) {
            document.getElementById("ctl00_cphBody_cboPayTypeNo").rules("add", { required: true });
            document.getElementById("ctl00_cphBody_cboPayClassNo").rules("add", { required: true });
        }
    }

    $(window).resize(function () {
        adjustPanelEntry();

    });

    $(document).ready(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (args, sender) {
            adjustPanelEntry();
        });

    });