var onAdmLoad = function onAdmLoad() {
    AAYW.Admin.EntityInspector.Apply();
    AAYW.Admin.MailSettings.Apply();
    AAYW.Admin.MailTemplates.Apply();
    AAYW.Admin.CustomForms.Apply();
    AAYW.Admin.ContentBlocks.Apply();
    AAYW.Admin.CustomPages.Apply();
}

$(window).load(onAdmLoad);