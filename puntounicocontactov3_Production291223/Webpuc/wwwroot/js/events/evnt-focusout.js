//import { ajax } from "../soket.js";
import { usr_cred, empleado_form } from "../validations/client-validation.js"
import { insertAlert } from "../alertas/cint-alerts.js"
import { ajax } from "../soket.js"
import { urlApp } from "../config.js"
export function focusout_eventos(ev) {

    if (ev.target.matches('#id-usuario')) {// BUSQUEDA POR EXPRESION REGULAR
        ev.preventDefault()
        let $inputUsr = document.getElementById('id-usuario')
        let msn = document.getElementById('msn-login')
        //Validar que no se encuentre vacion el elemento de usuario
        if ($inputUsr.value != "") {
            if (!usr_cred.cbUsuario($inputUsr.value)) {//No coincide con la expresion regular del campo
                msn.innerHTML = ""
                const alert = new insertAlert(msn, "¡ATENCIÓN!", "el usuario no es valido")
                alert.msnAlertWarning('msn-login');
                $inputUsr.value = ""
            } else {
                msn.innerHTML = ""
                msn.removeAttribute("class")
            }
        }

    }else if (ev.target.matches('#id-set-pass')) {
        ev.preventDefault()
        let $inputPass = document.getElementById('id-set-pass')
        let msn = document.getElementById('msn-reset-pass')
        if (!usr_cred.cbPass($inputPass.value)) {//No coincide con la expresion regular del campo
            const alert = new insertAlert(msn, "¡ATENCIÓN!", "la contraseña ingresada NO cumple las reglas de seguridad")
            alert.msnAlertDanger('msn-reset-pass');
            $inputPass.value = ""
        } else {
            msn.innerHTML = ""
            msn.removeAttribute("class")
        }
    }
    //************************************************************************************* */
    //Validaciones para ingresar un usuario 
    else if (ev.target.matches('#id-NoEmpleado')) {
        ev.preventDefault()
        let inputNoEmpleado = document.getElementById('id-NoEmpleado')
        let nameField = inputNoEmpleado.getAttribute('placeholder')

        if (inputNoEmpleado.value != "") {
            if (!empleado_form.cbNoEmpleado(inputNoEmpleado.value)) {
                MsnAlertInsertEmp(`No cumple la regla establecida para el campo ${nameField}`)
                inputNoEmpleado.value = ""
            } else {
                //Consultamos en la base de datos que el id ingresado no exista.
                let datos = new FormData()
                datos.append("NoEmpleado", inputNoEmpleado.value)

                ajax({
                    url: urlApp + "AdminEmpleado/ExistNoEmpleado",
                    method: "POST",
                    cbSucced: function (respuesta) {
                        //var json = JSON.parse(respuesta)
                        if (respuesta == "true") {//Si no se encontro el id          
                            inputNoEmpleado.setAttribute("style", "background-color:white")
                            inputNoEmpleado.value = ""
                            MsnAlertInsertEmp("Atencion este número de empleado ya existe")
                        } else {
                            inputNoEmpleado.setAttribute("style", "background-color:#d1e7dd")
                        }
                    }
                }, datos)
            }
        }

    } else if (ev.target.matches('#id-FirstName')) {
        ev.preventDefault()
        let inputFName = document.getElementById('id-FirstName')
        let nameField = inputFName.getAttribute('placeholder')

        if (inputFName.value != "") {
            if (!empleado_form.cbFirstName(inputFName.value)) {
                MsnAlertInsertEmp(`No cumple la regla para el campo ${nameField}`)
                inputFName.value = ""
            } else {
                inputFName.setAttribute("style", "background-color:#d1e7dd")
            }
        }
       
    } else if (ev.target.matches('#id-SecondName')) {
        ev.preventDefault()
        let inputFName = document.getElementById('id-SecondName')
        let nameField = inputFName.getAttribute('placeholder')
        if (inputFName.value != "") {
            if (!empleado_form.cbFirstName(inputFName.value)) {
                MsnAlertInsertEmp(`No cumple la regla para el campo ${nameField}`)
                inputFName.value = ""
            } else {
                inputFName.setAttribute("style", "background-color:#d1e7dd")
            }
        }

    } else if (ev.target.matches('#id-ApeP')) {
        ev.preventDefault()
        let inputApeP = document.getElementById('id-ApeP')
        //let inputApeP = document.getElementsByClassName('class-Apell')
        if (inputApeP.value != "") {
            if (!empleado_form.cbapellidos(inputApeP.value)) {
                MsnAlertInsertEmp(`No cumple la regla para el campo`)
                inputApeP.value = ""
            } else {
                inputApeP.setAttribute("style", "background-color:#d1e7dd")
            }
        }
    } else if (ev.target.matches('#id-ApeM')) {
        ev.preventDefault()
        let inputApeM = document.getElementById('id-ApeM')
        if (inputApeM.value != "") {
            if (!empleado_form.cbapellidos(inputApeM.value)) {
                MsnAlertInsertEmp(`No cumple la regla para el campo`)
                inputApeM.value = ""
            } else {
                inputApeM.setAttribute("style", "background-color:#d1e7dd")
            }
        }
    }

}

//Funciones externas
function MsnAlertInsertEmp(msn) {
    const alertMsn = document.getElementById('alert-insert-emp')
    alertMsn.innerHTML = ""
    alertMsn.innerHTML = msn
    $('#modalId-insertEmp').modal("show");
}
