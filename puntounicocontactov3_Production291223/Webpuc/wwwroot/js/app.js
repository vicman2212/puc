/* 
 *Capturar y manejar los principales eventos. 
 */
import { submit_eventos } from "./events/evnt-click.js";
import { select_eventos } from "./events/evnt-select.js";
import { keypress_eventos } from "./events/evnt-keypress.js";
import { focusout_eventos } from "./events/evnt-focusout.js"
/*import { ajax } from "/js/soket.js";*/
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

    $body.addEventListener("focusout", (event) => {
        focusout_eventos(event)
    });

    $body.addEventListener("keypress", (event) => {
        if (event.key === "Enter") {
            console.log("Hiciste key")
            keypress_eventos(event)
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