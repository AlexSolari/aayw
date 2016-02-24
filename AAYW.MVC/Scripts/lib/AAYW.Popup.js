$(window).load(function () {
	window.AAYW = window.AAYW || {};

	AAYW.Popup = AAYW.Popup || (function (window) {
		return {
			Show: function (content) {
				var modalEl = document.createElement('div');

				modalEl.style.width = '100%';
				modalEl.style.height = 'auto';
				modalEl.style.margin = '100px auto';
				modalEl.style.backgroundColor = '#fff';
				modalEl.innerHTML = content;

				mui.overlay('on', modalEl);
			},
			Close: function (id) {
				if (id.indexOf("popup-") != 0)
					return;

				$("#" + id).remove();
			},
		};
	})(this);
});