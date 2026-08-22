window.nexoUiScroll = {

    lock: function () {

        const body =
            document.body;

        const html =
            document.documentElement;


        if (!body || !html) {
            return;
        }


        // ==========================================
        // EVITAR BLOQUEAR DOS VECES
        // ==========================================

        if (
            body.dataset.nexoScrollLocked === "true"
        ) {
            return;
        }


        body.dataset.nexoScrollLocked =
            "true";


        // ==========================================
        // GUARDAR ESTADO ACTUAL
        // ==========================================

        body.dataset.nexoPrevOverflow =
            body.style.overflow || "";

        body.dataset.nexoPrevPaddingRight =
            body.style.paddingRight || "";

        body.dataset.nexoPrevOverscroll =
            body.style.overscrollBehavior || "";

        html.dataset.nexoPrevOverflow =
            html.style.overflow || "";

        html.dataset.nexoPrevOverscroll =
            html.style.overscrollBehavior || "";


        // ==========================================
        // CALCULAR ANCHO DEL SCROLLBAR
        // ==========================================
        //
        // Esto evita que la página "salte"
        // horizontalmente cuando desaparece
        // la barra de scroll.
        // ==========================================

        const scrollbarWidth =
            window.innerWidth -
            document.documentElement.clientWidth;


        if (scrollbarWidth > 0) {

            body.style.paddingRight =
                `${scrollbarWidth}px`;
        }


        // ==========================================
        // BLOQUEAR SCROLL DEL DOCUMENTO
        // ==========================================

        body.style.overflow =
            "hidden";

        html.style.overflow =
            "hidden";


        body.style.overscrollBehavior =
            "none";

        html.style.overscrollBehavior =
            "none";
    },


    unlock: function () {

        const body =
            document.body;

        const html =
            document.documentElement;


        if (!body || !html) {
            return;
        }


        // ==========================================
        // SI NO ESTABA BLOQUEADO, NO HACER NADA
        // ==========================================

        if (
            body.dataset.nexoScrollLocked !== "true"
        ) {
            return;
        }


        // ==========================================
        // RESTAURAR BODY
        // ==========================================

        body.style.overflow =
            body.dataset.nexoPrevOverflow || "";

        body.style.paddingRight =
            body.dataset.nexoPrevPaddingRight || "";

        body.style.overscrollBehavior =
            body.dataset.nexoPrevOverscroll || "";


        // ==========================================
        // RESTAURAR HTML
        // ==========================================

        html.style.overflow =
            html.dataset.nexoPrevOverflow || "";

        html.style.overscrollBehavior =
            html.dataset.nexoPrevOverscroll || "";


        // ==========================================
        // LIMPIAR DATOS TEMPORALES
        // ==========================================

        delete body.dataset.nexoScrollLocked;

        delete body.dataset.nexoPrevOverflow;

        delete body.dataset.nexoPrevPaddingRight;

        delete body.dataset.nexoPrevOverscroll;

        delete html.dataset.nexoPrevOverflow;

        delete html.dataset.nexoPrevOverscroll;
    }
};