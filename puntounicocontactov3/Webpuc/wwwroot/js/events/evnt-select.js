import { ajax } from "../soket.js";

export function select_eventos(ev) {
    /*Reinicia las aretas en cada evento*/
   // document.getElementById('msn-alert-adminEmp').innerHTML = ""

    if (ev.target.matches('#Busqueda-pues'))
    {// Para el caso de obtener el puesto
        ev.preventDefault()
        
        let datos = new FormData()
        datos.append('idPuesto', document.getElementById('Busqueda-pues').value)
        $("#tres-personal").DataTable().destroy()//Aplicar para evitar error en el datatable; esto destruye el contenido anterior 
        /*alert("Elemento seleccionado para puesto " + document.getElementById('Busqueda-pues').value)*/
        
        ajax({//Ida al servidor
            url: "/Directorio/LocateByPuesto",
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
    } else if (ev.target.matches('.change-area')) {
        ev.preventDefault()
        let datos = new FormData()
        datos.append('idDep', document.getElementById('change-area-id').value)

        ajax({
            url: "/AdminEmpleado/ProcChangeArea",
            method: "POST",
            cbSucced: function (respuesta) {
                var json = JSON.parse(respuesta)
                const $select_puesto = document.getElementById("change-puesto-id")
                $select_puesto.innerHTML = ""
                $select_puesto.innerHTML = `<option value=0>Selecciona el Puesto</option>`
                json.forEach((item) => {
                    console.log(item)
                    
                    $select_puesto.innerHTML += `<option value=${item.idPuesto}>${item.puesto1}</option>`
                })                
            }
        }, datos);
    } else if (ev.target.matches('.change-entidad')) {
        ev.preventDefault()
        let datos = new FormData()
        datos.append('idEF', document.getElementById('change-entidad-id').value)
        document.getElementById('msn-alert-adminEmp').innerHTML = `<div class="alert alert-info">
                                        <strong>¡Atencion!</strong> Va a modificar la ubicacion del empleado, por lo que tendrá que reasignar el <strong>ÁREA</strong> y <strong>PUESTO</strong>
                                        </div>`
        ajax({
            url: "/AdminEmpleado/ProcChangeEntidadFederativa",
            method: "POST",
            cbSucced: function (respuesta) {
                var json = JSON.parse(respuesta)
                const $select_area = document.getElementById("change-area-id")
                const $select_puesto = document.getElementById("change-puesto-id")
                $select_area.innerHTML = ""
                $select_area.innerHTML = `<option value=0>Selecciona el Área</option>`
                json.forEach((item) => {
                    console.log(item)
                    $select_area.innerHTML += `<option value=${item.idDepartamento}>${item.nombreDepartamento}</option>`
                })
                $select_puesto.innerHTML = `<option value=0>Selecciona el Área primero</option>`
            }
        }, datos);
    } 
}
