window.nexoGoogleAuth = {

    // Aquí guardaremos el cliente de Google
    // correspondiente a Login o Register.
    clients: {},


    // ==========================================
    // INICIALIZAR GOOGLE
    // ==========================================

    initializeCodeClient: function (
        clientId,
        dotNetRef,
        key
    ) {

        const inicializar = () => {

            // ==========================================
            // ESPERAR A QUE GOOGLE GIS ESTÉ DISPONIBLE
            // ==========================================

            if (
                !window.google ||
                !google.accounts ||
                !google.accounts.oauth2
            ) {

                setTimeout(
                    inicializar,
                    50
                );

                return;
            }


            // ==========================================
            // CREAR CLIENTE DE GOOGLE
            // ==========================================

            const client =
                google.accounts.oauth2.initCodeClient({

                    client_id:
                        clientId,


                    // Queremos identificar al usuario
                    // y obtener correo + perfil.
                    scope:
                        "openid email profile",


                    // Abrimos Google en popup.
                    ux_mode:
                        "popup",


                    // Permite escoger cuenta.
                    select_account:
                        true,


                    // ==================================
                    // GOOGLE DEVUELVE EL CODE
                    // ==================================

                    callback: function (
                        response
                    ) {

                        if (
                            !response ||
                            !response.code
                        ) {

                            console.error(
                                "Google no devolvió un código válido.",
                                response
                            );

                            return;
                        }


                        // Mandamos el código a Blazor.
                        dotNetRef
                            .invokeMethodAsync(
                                "ProcesarGoogleCode",
                                response.code
                            )
                            .catch(
                                function (error) {

                                    console.error(
                                        "Error Google → Blazor:",
                                        error
                                    );
                                }
                            );
                    },


                    // ==================================
                    // ERROR DEL POPUP
                    // ==================================

                    error_callback: function (
                        error
                    ) {

                        console.error(
                            "Error al abrir Google:",
                            error
                        );
                    }
                });


            // ==========================================
            // GUARDAR CLIENTE
            // ==========================================

            window.nexoGoogleAuth
                .clients[key] =
            {
                client:
                    client,

                dotNetRef:
                    dotNetRef
            };


            console.log(
                "Google preparado:",
                key
            );
        };


        inicializar();
    },


    // ==========================================
    // ABRIR GOOGLE DESDE NUESTRO BOTÓN NEGRO
    // ==========================================

    requestCode: function (
        key
    ) {

        const item =
            window.nexoGoogleAuth
                .clients[key];


        if (
            !item ||
            !item.client
        ) {

            console.error(
                "Google todavía no está inicializado:",
                key
            );

            return;
        }


        // ESTE es el que abrirá Google
        // cuando pulsemos nuestro botón negro.
        item.client.requestCode();
    },


    // ==========================================
    // LIMPIAR REFERENCIA
    // ==========================================

    release: function (
        key
    ) {

        if (
            window.nexoGoogleAuth
                .clients[key]
        ) {

            delete window
                .nexoGoogleAuth
                .clients[key];
        }
    }
};