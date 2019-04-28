using BCrypt.Net;

namespace Authentication.Providers
{
    public static class CryptoProvider
    {
        public class Cipher
        {
            public string Hash { get; set; }
            public string Salt { get; set; }
        }

        public class Plain
        {
            public string Password { get; set; }
        }

        public static Cipher  Encrypt(Plain plain)
        {
            var cipher = new Cipher();
            
            cipher.Salt = BCrypt.Net.BCrypt.GenerateSalt(SaltRevision.Revision2B);
            cipher.Hash = BCrypt.Net.BCrypt.HashPassword(plain.Password, cipher.Salt);

            return cipher;
        }

        public static bool VerifyPassword(Plain plain, Cipher cipher)
        {
            return BCrypt.Net.BCrypt.Verify(plain.Password, cipher.Hash);
        }
    }
}