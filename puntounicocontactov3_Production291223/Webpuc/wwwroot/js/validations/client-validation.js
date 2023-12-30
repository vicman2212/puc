

export let usr_cred = {
    rgx_usr: /^[a-z]{3,}\.[a-z]{3,}$/g,
    rgx_pass: /^(?=.*[A-Za-z])(?=.*\d)[a-zA-Z0-9!@#$%^&*()_+{}\[\]:;<>,.?~\\/`'"\-=|]{14,}$/g,
    cbUsuario: function (html) {
        this.rgx_usr.lastIndex = 0
        if (this.rgx_usr.test(html)) {
            return true
        } else {
            return false
        }
    },
    cbPass: function (html) {
        this.rgx_pass.lastIndex = 0
        if (this.rgx_pass.test(html)) {
            return true
        } else {
            return false
        }
    },
};

export let empleado_form = {
    rgx_NoEmpleado: /^\d{2,5}$/g,
    rgx_NombresProp: /^[a-zA-Z·ÈÌÛ˙¸Ò¡…Õ”⁄‹—'¥]{3,}$/g,
    rgx_Apellidos: /^[a-zA-Z·ÈÌÛ˙¸Ò¡…Õ”⁄‹—\s]{3,}$/g,
    cbNoEmpleado: function (html) {
        this.rgx_NoEmpleado.lastIndex = 0
        if (this.rgx_NoEmpleado.test(html)) {
            return true
        } else {
            return false
        }
    },
    cbFirstName: function (html) {
        this.rgx_NombresProp.lastIndex = 0
        if (this.rgx_NombresProp.test(html)) {
            return true
        } else {
            return false
        }
    },
    cbapellidos: function (html) {
        this.rgx_Apellidos.lastIndex = 0
        if (this.rgx_Apellidos.test(html)) {
            return true
        } else {
            return false
        }
    }

}