namespace Barber_shops.Servies.Emailservies
{
    public interface IEmailSerives
    {

        Task<bool> sendOtp(string email);
        bool verifyOtp(string email, string otp);

    }
}
