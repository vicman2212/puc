import { ajax } from "../soket.js";

export function submit_eventos(ev) {

    if (ev.target.matches('#btn-busqueda-rgx')) {// BUSQUEDA POR EXPRESION REGULAR
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
        $("#tres-personal").DataTable().destroy()//Aplicar para evitar error en el datatable; esto destruye el contenido anterior 
        ajax({//Ida al servidor
            url: "/Directorio/LocateByRegex",
            method: "POST",
            cbSucced: function (respuesta) {//Fue exitosa la peticion
                var json = JSON.parse(respuesta)
                const $tbody = document.getElementById("succeded-spersonal")
                //Limpiamos el contenido en tbody
                console.log(json)
                $tbody.innerHTML = "";

                if (json.length == 0) {//El resultado esta vacio
                    document.getElementById("ContenidoModalNotificaciones").innerHTML = `<div class="alert alert-warning">Actualmente no hay resultado para este filtro</div>`;
                    $("#ModalNotificaciones").modal("show");
                } else {//El resultado trae datos
                    json.forEach((item) => {
                        console.log(item)
                        $tbody.innerHTML += `<tr>
                                            <td>${item.nombres} ${item.apellidoPaterno} ${item.apellidoMaterno}</td>
                                            <td>${item.puesto}</td>
                                            <td>${item.nombreDepartamento}</td>
                                            <td>${item.correo}</td>
                                            <td>${item.extension}</td>
                                         </tr>`
                    })

                    let dataTable = $("#tres-personal").DataTable({
                        fixedHeader: {
                            header: true,
                            footer: true
                        },
                        language: {
                            url: '/lib/DataTables/Spanish.json'
                        },
                        "lengthMenu": [
                            [10, 20, 50, 100, -1],
                            [10, 20, 50, 100, "Todos"] // change per page values here
                        ],
                        buttons: [
                            'excel', 'pdf'
                        ],
                        dom:
                            "<'row'<'col-sm-6'B><'col-sm-6'f>>" +
                            "<'row'<'col-sm-12'tr>>" +
                            "<'row'<'col-sm-4'i><'col-sm-4 text-center'l><'col-sm-4'p>>",
                        "order": [
                            [0, "asc"]
                        ],
                    });
                    $("#Modal").modal("show");
                }
            }
        }, datos);
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
                    url: "/AdminEmpleado/UpdateEmpleado",
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
    }
    //else if (ev.target.matches('#submit-insertEmp')) {
    //    ev.preventDefault()
    //    //Validar que no vayan vacios los elementos para insertar un nuevo empleado
    //    let select_ef = document.getElementById('select-EntidadFederativa').value
    //    let select_dep = document.getElementById('select-departamento').value
    //    let select_puesto = document.getElementById('select-puesto').value

    //    if (select_ef === '0' || select_dep === '0' || select_puesto === '0') {
    //        alert("!Atencion! Elija la ubicación y puesto del usuario")
    //    }
    //}

}
