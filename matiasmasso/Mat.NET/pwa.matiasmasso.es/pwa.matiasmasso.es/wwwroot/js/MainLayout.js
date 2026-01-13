
function StylePwAuth() {
    try {
        var pwaAuth = document.getElementById('pwa-auth');
        var shadowroot = pwaAuth.shadowRoot;
        if (shadowroot == null)
            alert('null shadowroot');
        var providers = shadowroot.querySelectorAll('.provider');
        for (var i = 0; i < providers.length;i++) {
            providers[i].setAttribute('style', 'width:100%!important');
        };
    } catch (err) {
        alert('error styling shadowroot' + err);
    }
}