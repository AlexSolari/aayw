$(window).load(function () {
    ReactiveVariable.Initialize();

    if (window.QuestionDescriptionCharCounter)
        window.QuestionDescriptionCharCounter.Bind(ReactiveVariable.AfterSet, function () {
            $("#question-descr-char-counter").html(this.length);
        });

    if (window.QuestionTitleCharCounter)
        window.QuestionTitleCharCounter.Bind(ReactiveVariable.AfterSet, function () {
            $("#question-title-char-counter").html(this.length);
        });

    if (window.AnswerDescriptionCharCounter)
        window.AnswerDescriptionCharCounter.Bind(ReactiveVariable.AfterSet, function () {
            $("#reply-char-counter").html(this.length);
        });
});