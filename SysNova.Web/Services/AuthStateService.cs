using System.Text;
using System.Text.Json;
using Microsoft.JSInterop;

namespace SysNova.Web.Services
{
    public class AuthStateService
    {
        private readonly IJSRuntime _js;

        private string? _token;
        private string? _rol;
        private string? _correo;
        private string? _nombre;

        public AuthStateService(IJSRuntime js)
        {
            _js = js;
        }

        // ==========================================
        // PROPIEDADES
        // ==========================================

        public bool EstaAutenticado =>
            !string.IsNullOrWhiteSpace(_token);

        public bool EsCliente =>
            string.Equals(
                _rol,
                "Cliente",
                StringComparison.OrdinalIgnoreCase);

        public bool EsAdministrador =>
            string.Equals(
                _rol,
                "Administrador",
                StringComparison.OrdinalIgnoreCase);

        public bool EsPublico =>
            !EstaAutenticado;

        public string Rol =>
            string.IsNullOrWhiteSpace(_rol)
                ? "Publico"
                : _rol;

        public string Correo =>
            _correo ?? string.Empty;

        public string Nombre =>
            _nombre ?? string.Empty;


        // ==========================================
        // INICIALIZAR ESTADO
        // ==========================================

        public async Task InicializarAsync()
        {
            try
            {
                _token = await _js.InvokeAsync<string?>(
                    "localStorage.getItem",
                    "authToken");

                _correo = await _js.InvokeAsync<string?>(
                    "localStorage.getItem",
                    "userEmail");

                _nombre = await _js.InvokeAsync<string?>(
                    "localStorage.getItem",
                    "userName");

                // Si no existe token, el usuario es público.
                if (string.IsNullOrWhiteSpace(_token))
                {
                    LimpiarEstado();
                    return;
                }

                // Leer información del JWT
                LeerToken(_token);

                // Si el JWT contiene el correo y no había
                // userEmail en localStorage, utilizamos el del token.
                if (string.IsNullOrWhiteSpace(_correo))
                {
                    _correo = ObtenerClaim(
                        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");

                    if (!string.IsNullOrWhiteSpace(_correo))
                    {
                        await _js.InvokeVoidAsync(
                            "localStorage.setItem",
                            "userEmail",
                            _correo);
                    }
                }

                // Si no hay nombre guardado, utilizamos la parte
                // anterior al @ del correo.
                if (string.IsNullOrWhiteSpace(_nombre) &&
                    !string.IsNullOrWhiteSpace(_correo))
                {
                    _nombre = _correo.Split('@')[0];

                    await _js.InvokeVoidAsync(
                        "localStorage.setItem",
                        "userName",
                        _nombre);
                }
            }
            catch
            {
                LimpiarEstado();
            }
        }


        // ==========================================
        // LEER JWT
        // ==========================================

        private void LeerToken(string token)
        {
            try
            {
                var partes = token.Split('.');

                // Un JWT válido normalmente tiene:
                // Header.Payload.Signature
                if (partes.Length != 3)
                {
                    LimpiarEstado();
                    return;
                }

                var payload = partes[1];

                // Ajustar Base64URL a Base64 normal
                payload = payload
                    .Replace('-', '+')
                    .Replace('_', '/');

                switch (payload.Length % 4)
                {
                    case 2:
                        payload += "==";
                        break;

                    case 3:
                        payload += "=";
                        break;
                }

                var bytes = Convert.FromBase64String(payload);

                var json = Encoding.UTF8.GetString(bytes);

                using var documento =
                    JsonDocument.Parse(json);

                var root = documento.RootElement;

                // ==========================================
                // ROLE
                // ==========================================

                _rol = ObtenerValorClaim(
                    root,
                    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                    ?? ObtenerValorClaim(root, "role")
                    ?? ObtenerValorClaim(root, "roles");

                // ==========================================
                // CORREO
                // ==========================================

                if (string.IsNullOrWhiteSpace(_correo))
                {
                    _correo = ObtenerValorClaim(
                        root,
                        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                        ?? ObtenerValorClaim(
                            root,
                            "email")
                        ?? ObtenerValorClaim(
                            root,
                            "unique_name");
                }
            }
            catch
            {
                LimpiarEstado();
            }
        }


        // ==========================================
        // OBTENER CLAIM
        // ==========================================

        private string? ObtenerClaim(string nombreClaim)
        {
            if (string.IsNullOrWhiteSpace(_token))
                return null;

            try
            {
                var partes = _token.Split('.');

                if (partes.Length != 3)
                    return null;

                var payload = partes[1]
                    .Replace('-', '+')
                    .Replace('_', '/');

                switch (payload.Length % 4)
                {
                    case 2:
                        payload += "==";
                        break;

                    case 3:
                        payload += "=";
                        break;
                }

                var bytes = Convert.FromBase64String(payload);

                var json = Encoding.UTF8.GetString(bytes);

                using var documento =
                    JsonDocument.Parse(json);

                return ObtenerValorClaim(
                    documento.RootElement,
                    nombreClaim);
            }
            catch
            {
                return null;
            }
        }


        // ==========================================
        // BUSCAR VALOR DEL CLAIM
        // ==========================================

        private static string? ObtenerValorClaim(
            JsonElement root,
            string nombre)
        {
            if (!root.TryGetProperty(nombre, out var propiedad))
                return null;

            if (propiedad.ValueKind == JsonValueKind.String)
                return propiedad.GetString();

            // Por si en algún momento el backend devuelve
            // roles como un arreglo.
            if (propiedad.ValueKind == JsonValueKind.Array)
            {
                foreach (var elemento in propiedad.EnumerateArray())
                {
                    if (elemento.ValueKind == JsonValueKind.String)
                        return elemento.GetString();
                }
            }

            return null;
        }


        // ==========================================
        // CERRAR SESIÓN
        // ==========================================

        public async Task CerrarSesionAsync()
        {
            await _js.InvokeVoidAsync(
                "localStorage.removeItem",
                "authToken");

            await _js.InvokeVoidAsync(
                "localStorage.removeItem",
                "userEmail");

            await _js.InvokeVoidAsync(
                "localStorage.removeItem",
                "userName");

            // Por compatibilidad, eliminamos también
            // el antiguo userRol que utilizaba el Login.
            await _js.InvokeVoidAsync(
                "localStorage.removeItem",
                "userRol");

            LimpiarEstado();
        }


        // ==========================================
        // LIMPIAR ESTADO EN MEMORIA
        // ==========================================

        private void LimpiarEstado()
        {
            _token = null;
            _rol = null;
            _correo = null;
            _nombre = null;
        }
    }
}