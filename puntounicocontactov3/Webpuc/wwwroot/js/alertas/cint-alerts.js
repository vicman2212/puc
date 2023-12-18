

export function insertAlert(container,title,msn) {
    this.container = container
    //Creacion de elementos para el mensaje
    const $div = document.createElement("div")
    const $strong = document.createElement("strong")
    const $titulo = document.createTextNode(title)
    const $mensaje = document.createTextNode(msn)

    //Agregar elementos
    $strong.appendChild($titulo)
    $div.appendChild($strong)
    $div.appendChild($mensaje)
    this.container.appendChild($div)
}

insertAlert.prototype.msnAlert = function () {
    this.$div.setAttibute("class","alert alert-danger")
}