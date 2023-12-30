import { deployDirectorio } from "../misc/PeticionesHome.js"
import { urlApp } from "../config.js"
import { ajax } from "../soket.js";

export function select_eventos(ev) {
    /*Reinicia las aretas en cada evento*/
   // document.getElementById('msn-alert-adminEmp').innerHTML = ""

    if (ev.target.matches('#Busqueda-pues'))
    {// Para el caso de obtener el puesto
        ev.preventDefault()        
        let datos = new FormData()
        datos.append('idPuesto', document.getElementById('Busqueda-pues').value)
        let controllerurl = urlApp + "Directorio/LocateByPuesto"
        deployDirectorio(datos, controllerurl)

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
