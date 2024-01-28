/* 
 *Capturar y manejar los principales eventos. 
 */
import { submit_eventos } from "./events/evnt-click.js";
import { select_eventos } from "./events/evnt-select.js";
import { ajax } from "/js/soket.js";
/*import { blur_eventos } from "./events/evnt-blur.js";*/

export function app() {
    //Implementacion de funcion importada
    const $body = document.querySelector("body");
    //-----------Deteccion del navegador---------------------
    let navegador = window.navigator.vendor;
    console.log("vendor: " + navegador);

    /*Acomodar estos eventos*/
    //Eventos jQuiery
    //Evento para detectar en el campo de busqueda de directorio y convertir a mayusculas $('#Dir-bus-rgx')
    $(function () {
        $('.input-mayus').keyup(function () {
            this.value = this.value.toUpperCase();
        });
    });
    //Evento para resetear el puesto de la lista desplegable seleccionada
    $("#change-close-tik").on("click", function () {
        $('#Busqueda-pues option').prop('selected', function () {
            return this.defaultSelected;
        });
    });
    $("#change-close-btn").on("click", function () {
        $('#Busqueda-pues option').prop('selected', function () {
            return this.defaultSelected;
        });
    });
    /* Agrega un event listener para el evento "keypress"*/
    $body.addEventListener("keypress", function (event) {
        // Verifica si la tecla presionada es Enter (código 13)
        if (event.key === "Enter") {
            // Llama a la función buscar() cuando se presiona Enter
            event.preventDefault();
            let regx = document.getElementById('Dir-bus-rgx').value
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
    });

    $body.addEventListener("click", (event) => {//Delegacion de eventos para el "click"
        console.log("EVENTO DE BODY:---" + event.target);
        console.log("Hiciste clic")
        //------------------Ejecucion de los eventos modulos de eventos-------------------------
        submit_eventos(event);
    });

    $body.addEventListener("change", (event) => {//Delegacion de eventos para el "click"
        console.log("EVENTO DE BODY LISTAS DESPLEGABLES:---" + event.target);
        //------------------Ejecucion de los eventos modulos de eventos-------------------------
        select_eventos(event);
    });
}