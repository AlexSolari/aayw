$(window).load(function () {
    window.AAYW = window.AAYW || {};

    AAYW.UI = AAYW.UI || {};

    AAYW.UI.ColorPickers = AAYW.UI.ColorPickers || {
        DefaultSelector: ".aayw-color-picker",
        Apply: function (selector) {
            selector = selector || AAYW.UI.ColorPickers.DefaultSelector;

            var pickers = $(selector);
            var textViews = $(selector + " + input");

            pickers.each(function (index, element) {
                $(element).change(function (e) {
                    textViews[index].value = this.value;
                });
            });

            textViews.each(function (index, element) {
                $(element).bind("input", function (e) {
                    pickers[index].value = this.value;
                });
            });
        }
    };
});