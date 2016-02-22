var CharCounterBinder = (function (window) {
    return {
        Bind: function (name) {
            var variableName = ReactiveVariable.Prefix + name;
            if (window[variableName] && window[variableName].Bind)
                window[variableName].Bind(ReactiveVariable.AfterSet, function () {
                    $("#" + name).html(this.length);
                });
        }
    };
})(window);

$(window).load(function () {
    ReactiveVariable.Initialize();

    CharCounterBinder.Bind("QuestionDescriptionCharCounter");
    CharCounterBinder.Bind("QuestionTitleCharCounter");
    CharCounterBinder.Bind("AnswerDescriptionCharCounter");
});