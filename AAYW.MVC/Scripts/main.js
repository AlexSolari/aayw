$(window).load(function () {
    var bindHtmlEditors = function () {
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
    }

    var PostEditCallback = function (data) {
        function onsubmit() {
            AAYW.UI.LoadingIndicator.Show();
            var form = $(".popup-editor form").serialize();
            form += "&returnUrl=" + document.location.href;
            $.post("[Route:CreateOrUpdatePost]", form, function (result) {
                document.location.href = result.split('#')[0];
            })
            return false;
        }

        AAYW.UI.LoadingIndicator.Close();
        AAYW.UI.Popup.Show(data);
        bindHtmlEditors();

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
});