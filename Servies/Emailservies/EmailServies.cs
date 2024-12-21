using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Barber_shops.Servies.Emailservies
{
    public class EmailServies :IEmailSerives
    {
        private static Dictionary<string,string> otpDictionary = new Dictionary<string,string>();


      public async Task<bool> sendOtp(string email)
        {
            try
            {
                string otp = GenerateOTP();

                await SendEmail(email, otp);
                otpDictionary[email] = otp;
                return true;


            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool verifyOtp(string email,string otp)
        {
            if (otpDictionary.ContainsKey(email) && otpDictionary[email]==otp)
            {
                otpDictionary.Remove(email);
                return true;
            }
            else
            {
                return false;
            }
        }










        private string GenerateOTP()
        {
            Random rand = new Random();
            int otp = rand.Next(100000, 999999);
            return otp.ToString();
        }




        public async Task SendEmail(string toAddress, string otp)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential("sadiqueoffical@gmail.com", "xlpw atfs evce mczu");
                smtpClient.EnableSsl = true;


                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("sadiqueoffical@gmail.com");
                mailMessage.To.Add(toAddress);
                mailMessage.Subject = "OTP Verification";
                //mailMessage.Body = "Your OTP for email verification is: " + otp;
                mailMessage.Body =GenerateEmailBody(otp);
                mailMessage.IsBodyHtml = true;

                await smtpClient.SendMailAsync(mailMessage);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        private string GenerateEmailBody(string otp)
        {
            string emailbody=string.Empty;
            emailbody += "<div style='width:100%;background-color:grey'>";
            emailbody += "<h1>Hi User,Thanks for Registing</h1>";
            emailbody += "<h2>Please enter OTP text and complete the Registeration</h2>";
            emailbody += "<h2>OTP text is :" + otp + "</h2>";
            emailbody += "</div>";

            return emailbody;
        }
    }
}
