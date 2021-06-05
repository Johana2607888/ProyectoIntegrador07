$("#btnNuevo").click(function(eve) {
    $("#modal-content").load("/Cliente/registrarCliente");
});


function Registrarse() {

    var url = $('#dvModalRegistrar').data('url');
    $.get(url, function (data) {

        $('#dvModalRegistrar').html(data);
        $('#dvModalRegistrar').modal('show');
    })

}