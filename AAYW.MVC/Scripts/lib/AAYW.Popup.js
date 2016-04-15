$(window).load(function () {
    window.AAYW = window.AAYW || {};

    AAYW.UI = AAYW.UI || {};

	AAYW.UI.Popup = AAYW.UI.Popup || (function (window) {
		return {
			Show: function (content) {
			    var closeRow = document.createElement('div');
			    closeRow.style.display = 'inline-block';
			    closeRow.style.maxWidth = '500px';
			    closeRow.style.width = '100%';

			    var closeButton = document.createElement('span');
			    closeButton.className = 'popup-close-button';
			    closeButton.innerHTML = '×';
			    closeButton.style.fontSize = '1.75em';
			    closeButton.style.cssFloat = 'right';
			    closeButton.onclick = function () {
			        mui.overlay('off');
			    };

			    var modalEl = document.createElement('div');
			    modalEl.className = 'popup-containter mui--z3';
				modalEl.style.width = '100%';
				modalEl.style.height = 'auto';
				modalEl.style.margin = '100px auto';
				modalEl.style.textAlign = 'center';
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