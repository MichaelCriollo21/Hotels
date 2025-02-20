using System.Net.Mail;
using System.Net;
using Hotels.Entity.Entities;

namespace Hotels.WebApi.Helpers
{
    public class Email
    {
        private const string SmtpHost = "smtp.gmail.com";
        private const int SmtpPort = 587;
        private const string FromEmail = "testmichael08@gmail.com";
        private const string FromPassword = "Temporal123*"; 

        public static async Task SendEmail(Booking booking)
        {
            try
            {
                var fromAddress = new MailAddress(FromEmail, "Hotel Booking");
                var toAddress = new MailAddress(booking.Passengers.FirstOrDefault().Email, booking.Passengers?.FirstOrDefault().FirstName);
                string subject = "Confirmación de Reserva en " + booking.Room.Hotel.Name;
                string body = $@"
                <h2>¡Hola {booking.Passengers?.FirstOrDefault().FirstName}!</h2>
                <p>Tu reserva en <strong>{booking.Room.Hotel.Name}</strong> ha sido confirmada.</p>
                <p><strong>Fecha de entrada:</strong> {booking.CheckInDate:dd/MM/yyyy}</p>
                <p><strong>Fecha de salida:</strong> {booking.CheckOutDate:dd/MM/yyyy}</p>
                <p>¡Gracias por reservar con nosotros!</p>";

                using (var smtp = new SmtpClient(SmtpHost, SmtpPort))
                {
                    smtp.Credentials = new NetworkCredential(FromEmail, FromPassword);
                    smtp.EnableSsl = true;

                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    })
                    {
                        await smtp.SendMailAsync(message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enviando correo: {ex.Message}");
            }
        }
    }
}
