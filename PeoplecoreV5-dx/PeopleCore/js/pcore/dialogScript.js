/// <reference path="../jquery-1.4.1-vsdoc.js" />
var isDragged = false;
var dialogTopPos = 0;
var dialogLeftPos; 0
var counter = 0;
var initcounter = 0;
var errmessage = null;
$(function () {
    UpgradeASPNETValidation();
    initializeControls();
});

function initializeControls() {
    initializedialog();
}

/* Initialize the dialog plugin */
function initializedialog() {
    $("#error-messages").dialog({
         autoOpen: false
        ,hide: 'blind'
        ,minHeight: 125
        ,maxWidth: 350
        ,minWidth:300
        ,show: 'blind'
        ,title: 'Error(s)!'
        ,close:  function () { $("#error-messages ul").remove(); }
    });
}
/** 
* Extends the classic ASP.NET validation to add a class to the parent span when invalid 
*/
function UpgradeASPNETValidation() {

    // Hi-jack the ASP.NET error display only if required 
    if (typeof (Page_ClientValidate) != "undefined") {
        ValidatorUpdateDisplay = NicerValidatorUpdateDisplay;
        AspPage_ClientValidate = Page_ClientValidate;
        Page_ClientValidate = NicerPage_ClientValidate;
    }
}

/** 
* Extends the classic ASP.NET validation to add a class to the parent span when invalid 
*/
function NicerValidatorUpdateDisplay(val) {

    if (val.isvalid) {
        // do custom removing 
        var spans = $('span').filter(function () { return this.id == val.id; });
        if (spans.length > 0) {
            var errmessage1 = spans[0].errormessage;
            if (counter == 0) {
                //validation is successful, so remove the message from error dialog
                //call back for fadeOut checks if all messages are cleared then close the dialog
                $("#error-messages ul li").filter(function () { return $(this).text() == errmessage1; })
                                     .fadeOut(500, function () {
                                         if ($("#error-messages ul li:visible").length == 0) {
                                             closeErrorDialog();
                                         }
                                     });
            }
        }

        $(val).fadeOut('fast');

    }
    else {
        // do custom show
        var spans = $('span').filter(function () { return this.id == val.id; });
        if (spans.length > 0) {
            var errmessage1 = spans[0].errormessage;
            if (counter == 0 || counter == 1) {
                addMessagesToErrorDiv(errmessage1);
                if ($("#message-content ul").length > 0) {
                    if (!$("#error-messages").is(':visible')) {
                        $("#error-messages").dialog('open');
                    }
                }
            }
        }
        $(val).fadeIn('slow');
    }
}
/** 
* Extends classic ASP.NET validation to include parent element styling 
*/

function NicerPage_ClientValidate(validationGroup) {
    counter++;

    $("#message-content ul").remove();
    if (validationGroup != null) {
        var valid = AspPage_ClientValidate(validationGroup);
        if (!valid) {
            if ($("#message-content ul").length > 0) {
                if ($("#error-messages:visible").length ==0) {
                    $("#error-messages").dialog('open');
                }
            }
        }
        else {
            closeErrorDialog();
        }
    }
    counter = 0;
}

//Genereate the UI for message-content div
function addMessagesToErrorDiv(message) {
    if (message != null) {
        //do not append the same message again if it is already being display. So simply return.
        if ($("#error-messages ul li").filter(function () {
            var isequal = $(this).text() == message;
            if (isequal && !$(this).is(':visible')) {
                $(this).show();
            }
            return isequal;
            //return ($(this).text() == message && $(this).is(':visible'));        
        }).length > 0) {
            return;
        }

        //if there is an ul tag in message-content div, append li to that ul
        if ($("#message-content ul").length > 0) {
            var appstring = "<li>" + message + "</li>";
            $("#message-content ul").append(appstring);
        }
        //since there is no ul in message-content div, append one
        else {
            var appstring = "<ul><li>" + message + "</li></ul>";
            $("#message-content").append(appstring);
        }
    }
}

//Clear all error messages content
function clearErrorContent() {
    $("#error-messages ul").remove();
}

//Close the dialog and clear the messages
function closeErrorDialog() {
    if ($("#error-messages").is(':visible')) {
        $("#error-messages").dialog('close');
        $("#error-messages ul").remove();
    }
}
