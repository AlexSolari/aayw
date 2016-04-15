$(window).load(function () {
	window.AAYW = window.AAYW || {};

	AAYW.UI = AAYW.UI || {};

	AAYW.UI.HtmlEditor = AAYW.UI.HtmlEditor || (function (window) {
		return {
			Bind: function () {
				$('.html-editor textarea').each(function (index, el) {
					var id = el.id;
					window.nicEditors = window.nicEditors || [];
					var editor = window.nicEditors[id] = new nicEditor(
						{
							buttonList:
								[
									'bold',
									'italic',
									'underline',
									'strikeThrough',
									'left',
									'center',
									'right',
									'justify',
									'html',
									'image',
									'ol',
									'ul',
								]
						})
						.panelInstance(id);
					editor.addEvent('blur', function () {
						var html = $('.html-editor .nicEdit-main').html();
						$('.nicEdit-main').parents('.html-editor').find('textarea').text(html);
					});
				});
			},
		};
	})(this);
});