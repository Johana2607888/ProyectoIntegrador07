

$("#btnEditProd").magnificPopup({
    tClose: 'Cerrar',
    type: 'ajax',
    settings: {
        cache: false,
        async: false
    },
    callbacks: {
        ajaxContentAdded: function () {

        },
        close: function () {

        }
    },
    closeOnBgClick: false,
    enableEscapeKey: true,
    showCloseBtn: false,
    tLoading: 'Cargando contenido...',
    tError: 'El contenido no ha podido ser cargado.'
})