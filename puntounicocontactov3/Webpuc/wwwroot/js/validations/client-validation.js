

export let usr_cred = {
    rgx_usr: /^[a-z]{3,}\.[a-z]{3,}$/g,
    rgx_pass: /^[\w\W]{17,}$/g,
    cbUsuario: function (html) {
        if (this.rgx_usr.test(html)) {
            return true
        } else {
            return false
        }
    },
    cbPass: function (html) {
        if (this.rgx_pass.test(html)) {
            return true
        } else {
            return false
        }
    }

};