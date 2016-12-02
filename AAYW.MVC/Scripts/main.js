var onLoad = function onLoad() {
    AAYW.Settings.AjaxLinksMode = !!history;

    AAYW.Pages.Posts.Apply();

    if (AAYW.Settings.AjaxLinksMode){
        AAYW.Engine.Links.Apply();
    }
    
    AAYW.UI.ColorPickers.Apply();
};

$(window).load(onLoad);