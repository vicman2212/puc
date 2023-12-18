//import { ajax } from "../soket.js";
import { usr_cred } from "../validations/client-validation.js"
import { insertAlert } from "../alertas/cint-alerts.js"
export function focusout_eventos(ev) {

    if (ev.target.matches('#id-usuario')) {// BUSQUEDA POR EXPRESION REGULAR
        ev.preventDefault()
        let $inputUsr = document.getElementById('id-usuario')
        let msn = document.getElementById('msn-login')
        if (!usr_cred.cbUsuario($inputUsr.value)) {//No coincide con la expresion regular del campo
            const alert = new insertAlert(msn, "¡ATENCIÓN!", "el usuario no es valido")
            alert.msnAlert();

            //msn.innerHTML = `<div class="alert alert-danger">
            //                    <strong>¡Atencion!</strong> el usuario ingresado no es valido. intentelo de nuevo con nombre.apellidoPaterno
            //                </div>`
            $inputUsr.value = ""
        } else {
            msn.innerHTML = ""
        }
        
    } 

}
