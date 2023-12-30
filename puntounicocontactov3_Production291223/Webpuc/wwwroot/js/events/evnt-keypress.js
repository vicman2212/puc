import { ajax } from "../soket.js";
import { urlApp } from "../config.js"
import { deployDirectorio, AccesoPUC } from "../misc/PeticionesHome.js"
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
        let urlcontroller = urlApp + "Directorio/LocateByRegex"
        deployDirectorio(datos, urlcontroller)
    }

    else if (ev.target.matches('#id-pass')) {
        ev.preventDefault()
        AccesoPUC()
    }

}
