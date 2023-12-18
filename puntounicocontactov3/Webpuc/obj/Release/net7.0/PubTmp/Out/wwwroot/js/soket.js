/* 
 Modulo para las peticiones asincronas
 */
/*La peticion acepta las propidades (obligatorio)
 * datos: Los datos capturados en los formulrios(opcional)
 * msnError parametro Rest para ingresar un conjunto de n datos para enviar mensaje de alerta(opcional)*/
export function ajax(props, datos) {
    let { url, method, cbSucced } = props;//Destructuracion del objeto

    async function to_server() {
        try {
            //Peticion de datos al servidor
            let peticion = await fetch(url, {
                method: method,
                body: datos
            });
            //Manejo de error 
            if (!peticion.ok) {//Cuando la peticion es rechazada o el servidor lanza un error
                console.log("no procede la peticion");
                throw new Error(peticion);//Se envia el rechazo de error  de la peticion
            } else {//Fue exitosa la peticion
                let respuesta = await peticion.text();
                cbSucced(respuesta);//Enviamos el contenido del objeto
            }
        } catch (error) {
            console.log(error);
        }
    }
    to_server();
}