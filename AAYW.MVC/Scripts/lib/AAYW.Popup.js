﻿$(window).load(function () {
    window.AAYW = window.AAYW || {};

    AAYW.UI = AAYW.UI || {};

	AAYW.UI.Popup = AAYW.UI.Popup || (function (window) {
		return {
			Show: function (content) {
			    var closeRow = document.createElement('div');

			    var closeButton = document.createElement('span');
			    closeButton.className = 'popup-close-button';
			    closeButton.innerHTML = "X";
			    closeButton.style.marginLeft = '50%';
			    closeButton.onclick = function () {
			        mui.overlay('off');
			    };

			    var modalEl = document.createElement('div');
			    modalEl.className = 'popup-containter mui--z3';
				modalEl.style.width = '100%';
				modalEl.style.height = 'auto';
				modalEl.style.margin = '100px auto';
				modalEl.style.backgroundColor = '#fff';

				var popupContent = document.createElement('div');
				popupContent.innerHTML = content;
				popupContent.className = "popup";

				closeRow.appendChild(closeButton);
				modalEl.appendChild(closeRow);
				modalEl.appendChild(popupContent);

				mui.overlay('on', modalEl);
			},
			Close: function () {
			    mui.overlay('off');
			},
		};
	})(this);
});