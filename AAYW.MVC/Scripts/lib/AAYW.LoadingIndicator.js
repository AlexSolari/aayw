$(window).load(function () {
    window.AAYW = window.AAYW || {};

    AAYW.UI = AAYW.UI || {};

    AAYW.UI.LoadingIndicator = AAYW.UI.LoadingIndicator || (function (window) {
		return {
			Show: function () {
			    $("#loading-indicator").fadeIn(300);
			},
			Close: function () {
			    $("#loading-indicator").fadeOut(300);
			},
		};
	})(this);
});