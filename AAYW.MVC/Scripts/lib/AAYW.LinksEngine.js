$(window).load(function () {
	window.AAYW = window.AAYW || {};

	AAYW.Engine = AAYW.Engine || {};

	AAYW.Engine.Links = AAYW.Engine.Links || (function (window) {
		function handler(e) {
			e.preventDefault();

			var self = this;
			if (AAYW.Settings.DebugMode)
				var startingTime = new Date().getTime();
			var interval = null;
			var loaded = false;

			if (self.href && self.href != document.location.href && self.href != document.location.href + "#") {
				$.ajax({
					method: 'GET',
					url: self.href,
					success: function (data) {
						var body = data
							.substring(data.indexOf("body>"), data.indexOf("</body"))
							.replace("body>", "");
						var title = data
							 .substring(data.indexOf("title>"), data.indexOf("</title"))
							 .replace("title>", "");

						history.pushState(null, title, self.href);

						$("body").html(body);
						$("title").html(title);
						onLoad();
						onAdmLoad();
						completion = 100;
						loaded = true;

						$(".click-bar").show();
						$(".click-bar").css("width", 100 + "%");

						if (AAYW.Settings.DebugMode) {
							console.log({
								pageLoadingTime: new Date().getTime() - startingTime,
							});
						}
					},
					error: function (data) {
						document.location.href = self.href;
					}
				});

				$(".click-bar").show();
				var completion = 0;
				interval = setInterval(function () {

					completion++;
					if (loaded == true) {
						clearInterval(interval);
						setTimeout(function () { $(".click-bar").fadeOut(700); }, 300);
					}
					else if (completion < AAYW.Settings.AjaxLinksTimeout) {
						var displayedCompletion = Math.min(75, completion);
						$(".click-bar").css("width", displayedCompletion + "%");
					}
					else {
						document.location.href = self.href;
					}

				}, 15);
			}
		};

		function popstate(event) {
			location.reload();
		}

		return {
			Apply: function () {
				$("a").click(handler);

				window.onpopstate = popstate;
			},
		};
	})(this);
});