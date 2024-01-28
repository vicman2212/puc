using System.Security.Cryptography;
using System.Text;

namespace MiscApp
{
    public class StringRandom
    {
        public string GenerarCadenaAleatoria(int longitud)
        {
            const string caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            byte[] randomBytes = new byte[longitud];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            StringBuilder sb = new StringBuilder(longitud);

            foreach (byte b in randomBytes)
            {
                sb.Append(caracteresPermitidos[b % caracteresPermitidos.Length]);
            }

            return sb.ToString();
        }

    }
}