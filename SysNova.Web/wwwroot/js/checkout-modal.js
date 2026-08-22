window.nexoCheckoutModal = {

    scrollState: null,


    // ==========================================
    // BLOQUEAR SCROLL
    // ==========================================

    lockScroll: function () {

        // Si ya está bloqueado,
        // no hacemos nada.
        if (window.nexoCheckoutModal.scrollState) {
            return;
        }


        const html =
            document.documentElement;

        const body =
            document.body;


        // Guardamos los estilos actuales
        // para restaurarlos al cerrar.
        window.nexoCheckoutModal.scrollState = {

            htmlOverflow:
                html.style.overflow,

            bodyOverflow:
                body.style.overflow,

            bodyPaddingRight:
                body.style.paddingRight
        };


        // Calculamos el ancho de la scrollbar
        // para evitar que la página "salte"
        // horizontalmente cuando la ocultamos.
        const scrollbarWidth =
            window.innerWidth -
            html.clientWidth;


        // Bloqueamos scroll global.
        html.style.overflow =
            "hidden";

        body.style.overflow =
            "hidden";


        // Compensamos el espacio
        // que ocupaba la scrollbar.
        if (scrollbarWidth > 0) {

            body.style.paddingRight =
                `${scrollbarWidth}px`;
        }
    },


    // ==========================================
    // RESTAURAR SCROLL
    // ==========================================

    unlockScroll: function () {

        const state =
            window.nexoCheckoutModal
                .scrollState;


        // Si no estaba bloqueado,
        // no hacemos nada.
        if (!state) {
            return;
        }


        const html =
            document.documentElement;

        const body =
            document.body;


        // Restauramos los estilos originales.
        html.style.overflow =
            state.htmlOverflow;

        body.style.overflow =
            state.bodyOverflow;

        body.style.paddingRight =
            state.bodyPaddingRight;


        // Limpiamos el estado.
        window.nexoCheckoutModal.scrollState =
            null;
    }
};