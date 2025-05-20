Sys.Application.add_init(function() {
  // Allows the div.blockMsg style in CSS to
  //  override BlockUI's defaults.
  $.blockUI.defaults.css = {};

  // Add the BlockUI call as an onclick handler
  //  of the Save button.
  $addHandler($get('ctl00_cphBody_lnkProcess'), 'click', function () {
      $('#tableholder').block({ message: null });

    // Add the progress indicator.
    $('.blockMsg').addClass('progress');
  });

  // Get a reference to the PageRequestManager.
  var prm = Sys.WebForms.PageRequestManager.getInstance();

  // Unblock the form when a partial postback ends.
  prm.add_endRequest(function(args, sender) {
    // Instantly remove the progress indication element.
      $('#tableholder').unblock({ fadeOut: 0 });

    // DOM voodoo required to correctly handle multiple
    //  sequential partial postbacks. Don't ask me why...
    //  I'm all ears if you have a better solution!
      $('#ctl00_cphBody_pnlProcess').hide();

    // Block the form, using our Panel's content
    //  as the block "message".
      $('#tableholder').block({ message: $('#ctl00_cphBody_pnlProcess') });

    $('#ctl00_cphBody_OkButtonClose').click(function (evt) {
      // Prevent navigation when the link is clicked.
      evt.preventDefault();

      // Unblock the form, and remove the confirmation
      //  div afterward. This prevents it from showing
      //  up below the form after BlockUI releases it.
      $('#tableholder').unblock({
        onUnblock: function() {
            $('#ctl00_cphBody_pnlProcess').remove();
        }
      });
    });
  });
});

// Bonus! Progress indicator preloading.
var preload = document.createElement('img');
preload.src = 'images/progress-indicator.gif';
delete preload;