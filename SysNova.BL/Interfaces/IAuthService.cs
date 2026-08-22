using SysNova.DTO;

namespace SysNova.BL.Interfaces
{
    public interface IAuthService
    {
        // ==========================================
        // LOGIN NORMAL
        // ==========================================

        Task<string?> LoginAsync(
            LoginDTO login);


        // ==========================================
        // REGISTRO NORMAL
        // ==========================================

        Task<bool> RegisterAsync(
            RegisterDTO register);


        // ==========================================
        // LOGIN / REGISTRO CON GOOGLE
        // ==========================================

        Task<GoogleLoginResultDTO?> LoginGoogleAsync(
            GoogleCodeDTO googleLogin);
    }
}