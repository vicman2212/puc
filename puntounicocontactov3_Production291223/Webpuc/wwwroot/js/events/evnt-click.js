import { ajax } from "../soket.js";
import { urlApp } from "../config.js"
import { deployDirectorio, AccesoPUC } from "../misc/PeticionesHome.js"
export function submit_eventos(ev) {

    if (ev.target.matches('#btn-login')) {
        ev.preventDefault()
        AccesoPUC()

    }
    //Campo para reseteo de password
    else if (ev.target.matches('#btn-reset-passwd')) {
        ev.preventDefault()
        let pass1 = document.getElementById('id-set-pass')
        let pass2 = document.getElementById('id-reset-pass')
        let usuario = document.getElementById('id-reset-usr')
        let alertaMsn = document.getElementById('alertb-reset-pass')
        //Establecer que los campos no pueden estar vacios
        if (pass1.value != "" && pass2.value != "") {
            //Comparar que el campo pass 1 y pass 2 sean iguales
            if (pass1.value === pass2.value) {
                // hacer peticion al servidor
                let datos = new FormData();
                datos.append("Password", pass2.value)
                datos.append("Usuario", usuario.value)

                ajax({
                    url: urlApp + "Home/ResetPasswd",
                    method: "POST",
                    cbSucced(respuesta) {
                        if (respuesta == "1") {//Redireccionar a login principal
                            location.href = urlApp + "Home/Login"
                        } else if (respuesta == "00") {
                            pass1.value = ""
                            pass2.value = ""
                            alertaMsn.innerHTML = "¡Atención! USTED ESTA INGRESANDO LA MISMA CONTRASEÑA. ELIJA OTRA."
                            $("#modalId-resetPwd").modal("show")
                        }
                        else {
                            alert("Ocurrio un error inesperado")
                        }

                    }
                }, datos);
            } else {// Los campos no coiciden error de usuario
                pass2.value = ""
                alertaMsn.innerHTML = "¡Atención! Las contraseñas ingresadas no coinciden"
                $("#modalId-resetPwd").modal("show")
            }
        } else {//Los campos van vacios error de usuario
            alertaMsn.innerHTML = "¡Atención! Ninguno de los campos deben de estar vacios"
            $("#modalId-resetPwd").modal("show")
        }
       

    }
    //Para el proceso del directorio
    else if (ev.target.matches('#btn-busqueda-rgx')) {// BUSQUEDA POR EXPRESION REGULAR
        ev.preventDefault()
        let regx = document.getElementById('Dir-bus-rgx').value
        regx = regx.replace("Á", "A");
        regx = regx.replace("É", "E");
        regx = regx.replace("Í", "I");
        regx = regx.replace("Ó", "O");
        regx = regx.replace("Ú", "U");
        regx = regx.replace("Ü", "U");
        let datos = new FormData()
        datos.append('rgex', regx)
        if (regx.length < 2) {
            alert("La busqueda debe tener mínimo 5 letras.")
            return false
        }
        document.getElementById('Dir-bus-rgx').value = "" //Resetar busqueda
        let urlcontroller = urlApp + "Directorio/LocateByRegex"
        deployDirectorio(datos, urlcontroller)
    } else if (ev.target.matches('#btn-sc-admemp')) {//Cuando se va a guardar un cambio de usuario
        ev.preventDefault()
        let re = /[0-9]/ig
        let html_empleado = document.getElementById("id-no-emp").innerHTML
        let selected_ubicacion = document.getElementById("change-entidad-id").value
        let selected_area = document.getElementById("change-area-id").value
        let selected_puesto = document.getElementById("change-puesto-id").value
        let selected_status = document.getElementById("change-status").value
        let input_extension = document.getElementById("change-ext").value
        let old_dbExt = document.getElementById("db-ext").innerHTML
        let notific_modal = document.getElementById("notific-modal-adminEmp")
        //Establecer los datos a usar
        let datos = new FormData()
        datos.append("NoEmpleado", html_empleado)
        datos.append("IdDirDeparment", selected_area)
        datos.append("IdJob", selected_puesto)
        datos.append("IsActive", selected_status)
        datos.append("Ext", input_extension)

        //alert(selected_ubicacion + selected_area + selected_puesto + "ext" + input_extension)
        if (re.test(input_extension)) {
            //alert("coincide el formato")
            if (selected_ubicacion != 0) {// Se esta modificando la ubicacion
                //Validar que area y puesto no se vayan en 0, es obligatorio asignar parametros
                if (selected_area == 0 && selected_puesto == 0) {
                    //Enviar alerta para que ingresen los datos
                    document.getElementById('msn-alert-adminEmp').innerHTML = `<div class="alert alert-warning">
                                        <strong>¡Atencion!</strong> Recuerda que se esta modificando la ubicacion del empleado, por lo que tendrá que reasignar el <strong>ÁREA</strong> y <strong>PUESTO</strong>
                                        </div>`
                } else {//Se llenaron todos los campos de manera correcta
                    // Realizar la peticion Ajax
                    ajax({
                        url: "/AdminEmpleado/UpdateEmpleado",
                        method: "POST",
                        cbSucced: function (respuesta) {
                            //var json = JSON.parse(respuesta)
                            notific_modal.innerHTML = "¡EXITO! Usted actualizo los datos del empleado"
                            $("#Modal-adminEmp").modal("show");

                        }
                    }, datos)
                }
            }
            else if (selected_area != 0) { //Solo se esta actualizando el Area
                if (selected_puesto == 0) {//Validar que se esta reasignando el puesto
                    //Enviar alerta para que ingresen los datos
                    document.getElementById('msn-alert-adminEmp').innerHTML = `<div class="alert alert-warning">
                                        <strong>¡Atencion!</strong> Solo esta modificando el Área, por lo que tendrá que reasignar el <strong>PUESTO</strong>
                                        </div>`
                } else {
                    ajax({
                        url: "/AdminEmpleado/UpdateEmpleado",
                        method: "POST",
                        cbSucced: function (respuesta) {
                            //var json = JSON.parse(respuesta)
                            notific_modal.innerHTML = "¡EXITO! Usted actualizo el ÁREA del empleado y el PUESTO"
                            $("#Modal-adminEmp").modal("show");
                        }

                    }, datos)
                }
            }
            else if (selected_puesto != 0) {//Solo se modifica el puesto
                //alert("Solo esta modificando el puesto")
                ajax({
                    url: "/AdminEmpleado/UpdateEmpleado",
                    method: "POST",
                    cbSucced: function (respuesta) {
                        //var json = JSON.parse(respuesta)
                        notific_modal.innerHTML = "¡EXITO! Usted actualizo el PUESTO"
                        $("#Modal-adminEmp").modal("show");
                    }

                }, datos)
            }
            else if (selected_status != 1) {//Solo se modifica el status
                ajax({
                    url: "/AdminEmpleado/UpdateEmpleado",
                    method: "POST",
                    cbSucced: function (respuesta) {
                        //var json = JSON.parse(respuesta)
                        notific_modal.innerHTML = "¡EXITO! Usted DESHABILITO AL EMPLEADO"
                        $("#Modal-adminEmp").modal("show");
                    }

                }, datos)
            }
            else if (input_extension != old_dbExt) {//Cuando sea diferente la extension
                ajax({
                    url: urlApp + "AdminEmpleado/UpdateEmpleado",
                    method: "POST",
                    cbSucced: function (respuesta) {
                        //var json = JSON.parse(respuesta)
                        notific_modal.innerHTML = "¡EXITO! Actualizo la extensión"
                        $("#Modal-adminEmp").modal("show");
                    }
                }, datos)
            }
            else {
                document.getElementById('msn-alert-adminEmp').innerHTML = `<div class="alert alert-warning">
                                        <strong>¡Atencion!</strong> No esta enviando datos, CANCELE la operación</strong>
                                        </div>`
            }
        } else {
            alert("no coincide el formato de la extension")
        }
        //Validaciones: No se modificar la ubicacion        
        /**fin proc if/else*/
    }else if (ev.target.matches('#submit-insertEmp')) {
        ev.preventDefault()
        //seleccionar campos del usuario
        let inputNoEmpleado = document.getElementById('id-NoEmpleado')
        let inputFName = document.getElementById('id-FirstName')
        let inputSecondName = document.getElementById('id-SecondName')
        let inputApeP = document.getElementById('id-ApeP')
        let inputApeM = document.getElementById('id-ApeM')
        let inputExt = document.getElementById('id-ext')
        //Validar que no vayan vacios los elementos para insertar un nuevo empleado
        let select_ef = document.getElementById('change-entidad-id').value
        let select_dep = document.getElementById('change-area-id').value
        let select_puesto = document.getElementById('change-puesto-id').value

        if (select_ef === '0' || select_dep === '0' || select_puesto === '0') {
            MsnAlertInsertEmp("!Atencion! Elija la ubicación y puesto del usuario")
        } else if (inputNoEmpleado.value === "" || inputFName.value === "" ||
            inputApeP.value === "" || inputApeM.value === "") {
            MsnAlertInsertEmp("Atención, ningun campo debe estar vacio.")
        } else {
            let nombres = inputFName.value + " " + inputSecondName.value
            let datos = new FormData()
            datos.append("NoEmpleado", inputNoEmpleado.value)
            datos.append("Nombres", nombres)
            datos.append("ApellidoPaterno", inputApeP.value)
            datos.append("ApellidoMaterno", inputApeM.value)
            datos.append("Extension", inputExt.value)
            datos.append("DepartamentoId", select_dep)
            datos.append("PuestoId", select_puesto)
            //monica2023_2023
            //alert("Peticion AJAX")
            ajax({
                url: urlApp + "AdminEmpleado/InsertEmpleado",
                method: "POST",
                cbSucced: function (respuesta) {
                    if (respuesta === "true") {
                        //Limpiar campos 
                        inputNoEmpleado.value = ""
                        inputFName.value = ""
                        inputSecondName.value = ""
                        inputApeP.value = ""
                        inputApeM.value = ""
                        inputExt.value = ""
                        inputNoEmpleado.removeAttribute("style")
                        inputFName.removeAttribute("style")
                        inputSecondName.removeAttribute("style")
                        inputApeP.removeAttribute("style")
                        inputApeM.removeAttribute("style")
                        //Evento para resetear la lista desplegable seleccionada 
                        $('#change-entidad-id option').prop('selected', function () {
                            return this.defaultSelected;
                        });
                        $('#change-area-id option').prop('selected', function () {
                            return this.defaultSelected;
                        });
                        $('#change-puesto-id option').prop('selected', function () {
                            return this.defaultSelected;
                        });
                       
                        MsnAlertInsertEmp(`Se agrego al empleado con exito, en breve recibira la notificación`)
                    }
                }
            }, datos)
        }
    }

}

function MsnAlertInsertEmp(msn) {
    const alertMsn = document.getElementById('alert-insert-emp')
    alertMsn.setAttribute("class","modal-body alert alert-succeded")
    alertMsn.innerHTML = ""
    alertMsn.innerHTML = msn
    $('#modalId-insertEmp').modal("show");
}