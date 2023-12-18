import { ajax } from "../soket.js";

export function keypress_eventos(ev) {

    if (ev.target.matches('#Dir-bus-rgx')) {// BUSQUEDA POR EXPRESION REGULAR
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
    }

    else if (ev.target.matches('#id-pass')) {
        ev.preventDefault()
        alert("Acceso")
    }

}
