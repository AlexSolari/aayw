﻿$(window).load(function myfunction() {
	window.AAYW = window.AAYW || {};

	AAYW.Pages = AAYW.Pages || {};

	AAYW.Pages.Posts = AAYW.Pages.Posts || (function (window) {
		return {
			Apply: function () {
				var PostEditCallback = function (data) {
					function onsubmit() {
						AAYW.UI.LoadingIndicator.Show();
						var form = $(".popup-editor form").serialize();
						form += "&returnUrl=" + document.location.href;
						$.post("[Route:CreateOrUpdatePost]", form, function (result) {
							if (result instanceof Array) {
								var errors = result
									.map(function (x) { return x.errors[0].ErrorMessage.replace("[", "").replace("]", "") })
									.map(function (x) { return "<li>" + x + "</li>" });
								$(".custom-validation ul").html(errors);
								$(".custom-validation").show();
								AAYW.UI.LoadingIndicator.Close();
							}
							else {
								AAYW.Engine.Links.RedirectTo(result.split('#')[0]);
							}
						})
						return false;
					}

					AAYW.UI.LoadingIndicator.Close();
					AAYW.UI.Popup.Show(data);
					AAYW.UI.HtmlEditor.Bind();

					$('.popup-editor .submit').click(onsubmit);
				};

				$('.add-post a').click(function () {
					var id = $(this).parent().data("feed-id");

					AAYW.UI.LoadingIndicator.Show();

					$.get("[Route:CreatePost]".replace("{feedId}", id), PostEditCallback);
					return false;
				});

				$('.post .edit').click(function () {
					var id = $(this).parents('.post-controls').data("id");

					AAYW.UI.LoadingIndicator.Show();

					$.get("[Route:EditPost]".replace("{postId}", id), PostEditCallback);
					return false;
				});

				$('.post .delete').click(function () {
					var $el = $(this);
					var id = $el.parents('.post-controls').data("id");

					AAYW.UI.LoadingIndicator.Show();

					$.post("[Route:DeletePost]".replace("{id}", id), function () {
						AAYW.UI.LoadingIndicator.Close();
						$el.parents('.post').remove();
					});
					return false;
				});
			},
		};
	})(this);
});