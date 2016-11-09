var onAdmLoad = function onAdmLoad() {
    (function EntityInspector(window) {
        $(".admin-inspector-edit").click(function () {
            AAYW.UI.LoadingIndicator.Show();
            $.get("[Route:EditEntity]".replace("{type}", $(this).data("type")).replace("{id}", $(this).data("target")), function (data) {
                AAYW.UI.LoadingIndicator.Close();
                AAYW.UI.Popup.Show(data);
                AAYW.UI.HtmlEditor.Bind();
                $(".popup-editor form input").each(function (index, el) {
                    var $el = $(el);
                    if ($el.data("locked") == "True")
                        $el.parents(".row").addClass("hidden");
                });

                $('.popup-editor .delete').click(function (e) {
                    e.preventDefault();
                    var id = $('form input[name="modelData[Id@System.Guid]"]').val();
                    var type = $("form input[name=type]").val();
                    AAYW.UI.LoadingIndicator.Show();
                    $.post("[Route:DeleteEntity]", { id: id, type: type }, function () {
                        AAYW.UI.LoadingIndicator.Close();
                        AAYW.UI.Popup.Close();
                        $("tr:has(#" + id + "_Id)").remove();
                    })
                });

                $('.popup-editor .submit').click(function () {
                    AAYW.UI.LoadingIndicator.Show();
                    $.post("[Route:SaveEntity]", $(".popup-editor form").serialize(), function () {
                        AAYW.UI.LoadingIndicator.Close();
                        var id = $(".popup-editor form input[name='modelData[Id@System.Guid]']").val();
                        var row = $("#" + id);

                        $(".popup-editor form input").each(function (index, el) {
                            var $el = $(el);
                            $("#" + $el.prop("id").split("edit-")[1]).html($el.val());
                        });

                        AAYW.UI.Popup.Close();
                    })
                });
            });
        });
    })(this);

    (function MailSettings(window) {
        $(".admin-mail-settings .submit").click(function () {
            AAYW.UI.LoadingIndicator.Show();
            $.post("[Route:MailSettings]", $(".admin-mail-settings form").serialize(), function () {
                AAYW.UI.LoadingIndicator.Close();
            })
        });
    })(this);

    (function MailTemplates(window) {
        var showTemplatePopup = function (data) {
            var callback = function (data) {
                function onsubmit() {
                    AAYW.UI.LoadingIndicator.Show();
                    $.post("[Route:CreateMailTemplate]", $(".popup-editor form").serialize(), function (result) {
                        AAYW.UI.LoadingIndicator.Close();
                        AAYW.UI.Popup.Close();

                        if (!result) {
                            document.location.href = "[Route:MailTemplates]".replace("{page}", 0);
                        }
                        else {
                            AAYW.UI.Popup.Show(result);
                            $('.popup-editor .submit').click(onsubmit);
                        }

                    })
                    return false;
                }

                AAYW.UI.LoadingIndicator.Close();
                AAYW.UI.Popup.Show(data);
                AAYW.UI.HtmlEditor.Bind();

                $('.popup-editor .submit').click(onsubmit);
            };

            AAYW.UI.LoadingIndicator.Show();

            if (typeof data == "string") {
                $.get("[Route:EditMailTemplate]".replace("{id}", data), callback);
            }
            else {
                $.get("[Route:CreateMailTemplate]", callback);
            }
        }

        $('.admin-mail-template .create').click(showTemplatePopup);

        $('.admin-mail-template .edit-template').click(function () {
            showTemplatePopup($(this).parents("tr").data("id"));
            return false;
        });
    })(this);

    (function CustomForms(window) {
        var lastFieldIndex = 0;

        var bindFormConstructor = function () {

            $('.form-constructor .add').click(function () {
                AAYW.UI.LoadingIndicator.Show();
                $.post("[Route:CustomFormField]", { index: lastFieldIndex }, function (result) {
                    AAYW.UI.LoadingIndicator.Close();
                    $(".form-fields").append(result);
                    $(window).scrollTop($("body").height());
                });
                lastFieldIndex++;
                return false;
            });

            $('.form-constructor .remove').click(function (e) {
                e.preventDefault();
                $(".form-fields .field").last().remove();
                lastFieldIndex--;
                if (lastFieldIndex < 0)
                    lastFieldIndex = 0;
                return false;
            });
        };

        var showFormPopup = function (data) {
            var callback = function (data) {
                function onsubmit() {
                    AAYW.UI.LoadingIndicator.Show();
                    $.post("[Route:CreateCustomForm]", $(".popup-editor form").serialize(), function (result) {
                        AAYW.UI.LoadingIndicator.Close();
                        AAYW.UI.Popup.Close();

                        if (typeof result == "string") {
                            AAYW.UI.Popup.Show(result);
                            $('.popup-editor .submit').click(onsubmit);
                        }
                        else {
                            document.location.href = "[Route:CustomFormsList]".replace("{page}", 0);
                        }

                    })
                    return false;
                }

                AAYW.UI.LoadingIndicator.Close();
                AAYW.UI.Popup.Show(data);

                bindFormConstructor();

                $('.popup-editor .submit').click(onsubmit);
            };

            AAYW.UI.LoadingIndicator.Show();

            if (typeof data == "string") {
                $.get("[Route:EditUserForm]".replace("{id}", data), callback);
            }
            else {
                $.get("[Route:CreateCustomForm]", callback);
            }
        }

        $('.admin-custom-form .create').click(showFormPopup);

        $('.admin-custom-form .edit-form').click(function () {
            showFormPopup($(this).parents("tr").data("id"));
            return false;
        });
    })(this);

    (function ContentBlocks(window) {
        var showContentsPopup = function (data) {
            var callback = function (data) {
                function onsubmit() {
                    AAYW.UI.LoadingIndicator.Show();
                    $.post("[Route:CreateContentBlock]", $(".popup-editor form").serialize(), function (result) {
                        AAYW.UI.LoadingIndicator.Close();
                        AAYW.UI.Popup.Close();

                        if (typeof result == "string") {
                            AAYW.UI.Popup.Show(result);
                            $('.popup-editor .submit').click(onsubmit);
                        }
                        else {
                            document.location.href = "[Route:ContentBlockList]".replace("{page}", 0);
                        }

                    })
                    return false;
                }

                function ontypeselect() {
                    if ($('.type-dropdown select').val() == "html") {
                        $('.content-row').show();
                    }
                    else if ($('.type-dropdown select').val() == "redirect") {
                        $('.content-row').show();
                    }
                    else {
                        $('.content-row').hide();
                    }

                }

                AAYW.UI.LoadingIndicator.Close();
                AAYW.UI.Popup.Show(data);
                AAYW.UI.HtmlEditor.Bind();

                $('.popup-editor .submit').click(onsubmit);
                $('.type-dropdown select').change(ontypeselect);

                ontypeselect();
            };

            AAYW.UI.LoadingIndicator.Show();

            if (typeof data == "string") {
                $.get("[Route:EditContentBlock]".replace("{id}", data), callback);
            }
            else {
                $.get("[Route:CreateContentBlock]", callback);
            }
        }

        $('.admin-contents .create').click(showContentsPopup);

        $('.admin-contents .edit-contents').click(function () {
            showContentsPopup($(this).parents("tr").data("id"));
            return false;
        });

        

    })(this);

    (function CustomPages(window) {
        var showPagesPopup = function (data) {
            var callback = function (data) {
                function onsubmit() {
                    AAYW.UI.LoadingIndicator.Show();
                    $.post("[Route:CreatePage]", $(".popup-editor form").serialize(), function (result) {
                        AAYW.UI.LoadingIndicator.Close();
                        AAYW.UI.Popup.Close();

                        if (typeof result == "string") {
                            AAYW.UI.Popup.Show(result);
                            $('.popup-editor .submit').click(onsubmit);
                        }
                        else {
                            document.location.href = "[Route:PageList]".replace("{page}", 0);
                        }

                    })
                    return false;
                }

                AAYW.UI.LoadingIndicator.Close();
                AAYW.UI.Popup.Show(data);
                AAYW.UI.HtmlEditor.Bind();

                $('.popup-editor .submit').click(onsubmit);
            };

            AAYW.UI.LoadingIndicator.Show();

            if (typeof data == "string") {
                $.get("[Route:EditPage]".replace("{id}", data), callback);
            }
            else {
                $.get("[Route:CreatePage]", callback);
            }
        }

        $('.admin-pages .create').click(showPagesPopup);

        $('.admin-pages .edit-pages').click(function () {
            showPagesPopup($(this).parents("tr").data("id"));
            return false;
        });
    })(this);
}

$(window).load(onAdmLoad);