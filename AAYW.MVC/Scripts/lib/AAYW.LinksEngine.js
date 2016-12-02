$(window).load(function () {
	window.AAYW = window.AAYW || {};

	AAYW.Engine = AAYW.Engine || {};

	AAYW.Engine.Links = AAYW.Engine.Links || (function (window) {
	    function redirect(href) {
	        var loaded = false;
	        $.ajax({
	            method: 'GET',
	            url: href,
	            success: function (data) {
	                var body = data
                        .substring(data.indexOf("body>"), data.indexOf("</body"))
                        .replace("body>", "");
	                var title = data
                         .substring(data.indexOf("title>"), data.indexOf("</title"))
                         .replace("title>", "");

	                history.pushState(null, title, href);

	                $("body").html(body);
	                $("title").html(title);
	                onLoad();
	                onAdmLoad();
	                completion = 100;
	                loaded = true;

	                $(".click-bar").show();
	                $(".click-bar").css("width", 100 + "%");
	            },
	            error: function (data) {
	                document.location.href = href;
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
	                document.location.href = href;
	            }

	        }, 15);
	    };

	    function handler(e) {
            if (e)
			    e.preventDefault();

			var self = this;
			if (AAYW.Settings.DebugMode)
				var startingTime = new Date().getTime();
			var interval = null;

			if (self.href && self.href != document.location.href && self.href != document.location.href + "#") {
			    redirect(self.href);

			    if (AAYW.Settings.DebugMode) {
			        console.log({
			            pageLoadingTime: new Date().getTime() - startingTime,
			        });
			    }
			}
		};

	    function popstate(event) {
	        location.reload();
	    };

		return {
		    RedirectTo: function (url) {
		        redirect(url);
		    },
			Apply: function () {
				$("a").click(handler);

				window.onpopstate = popstate;
			},
		};
	})(this);
});