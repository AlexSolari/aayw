var CharCounterBinder = {
    Bind: function (name) {
        var variableName = "__rv-" + name;
        if (window[variableName] && window[variableName].Bind)
            window[variableName].Bind(ReactiveVariable.AfterSet, function () {
                $("#"+name).html(this.length);
            });
    }
};

$(window).load(function () {
    ReactiveVariable.Initialize();

    CharCounterBinder.Bind("QuestionDescriptionCharCounter");
    CharCounterBinder.Bind("QuestionTitleCharCounter");
    CharCounterBinder.Bind("AnswerDescriptionCharCounter");
});