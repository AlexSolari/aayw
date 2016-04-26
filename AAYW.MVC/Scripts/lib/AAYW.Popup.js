$(window).load(function () {
    window.AAYW = window.AAYW || {};

    AAYW.UI = AAYW.UI || {};

	AAYW.UI.Popup = AAYW.UI.Popup || (function (window) {
		return {
			Show: function (content) {
			    var closeRow = $("<div></div>");
			    closeRow.addClass("close-row");

			    var closeButton = $("<span></span>");
			    closeButton.addClass("close-button");
			    closeButton.click(AAYW.UI.Popup.Close);
			    closeButton.html("×");

			    var modalEl = $("<div></div>");
			    modalEl.addClass("popup-containter mui--z3");

			    var popupContent = $("<div></div>");
			    popupContent.addClass("popup");
			    popupContent.html(content);

				closeRow.append(closeButton);
				modalEl.append(closeRow);
				modalEl.append(popupContent);

				mui.overlay('on', modalEl[0]);
			},
			Close: function () {
			    mui.overlay('off');
			},
		};
	})(this);
});