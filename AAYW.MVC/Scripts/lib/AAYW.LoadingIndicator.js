$(window).load(function () {
    window.AAYW = window.AAYW || {};

    AAYW.UI = AAYW.UI || {};

    AAYW.UI.LoadingIndicator = AAYW.UI.LoadingIndicator || (function (window) {
		return {
			Show: function () {
			    $("#loading-indicator").show();
			},
			Close: function () {
			    $("#loading-indicator").hide();
			},
		};
	})(this);
});