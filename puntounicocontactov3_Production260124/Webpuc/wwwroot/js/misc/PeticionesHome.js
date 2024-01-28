import { ajax } from "../soket.js"
import { insertAlert } from "../alertas/cint-alerts.js"
import { urlApp } from "../config.js"
export function deployDirectorio(datos,controller) {
    $("#tres-personal").DataTable().destroy()//Aplicar para evitar error en el datatable; esto destruye el contenido anterior 
    ajax({//Ida al servidor
        url: controller,
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
}

export function AccesoPUC() {
    let $inpUsuario = document.getElementById('id-usuario').value
    let $inpPass = document.getElementById('id-pass').value
    let msn = document.getElementById('msn-login')
    //validar que las credenciales no sean vacias
    let datos = new FormData()
    datos.append('Usuario', $inpUsuario)
    datos.append('Password', $inpPass)
    if ($inpUsuario != "" && $inpPass != "") {
        //Realizamos peticion al servidor
        ajax({
            url: urlApp + "Home/Login",
            method: "POST",
            cbSucced: function (respuesta) {
                if (respuesta != "0") {
                    location.href = respuesta
                    console.log("Se redirige a la pantalla principal")
                } else {
                    //reiniciar credenciales
                    document.getElementById('id-usuario').value = ""
                    document.getElementById('id-pass').value = ""
                    //Enviar alerta al usuario
                    const alert = new insertAlert(msn, "¡ATENCIÓN!", "Credenciales de acceso invalidas")
                    alert.msnAlertDanger('msn-login');
                }
            }
        }, datos);
    }
}