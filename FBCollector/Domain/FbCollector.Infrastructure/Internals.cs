using System;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace FbCollector.Infrastructure
{
    public static class Internals
    {
        /// <summary>
        /// Creates log4net Logger
        /// </summary>
        //private static readonly ILog Log = LogManager.GetLogger("WEB");

        /// <summary>
        /// Checks if string property is null or is longer than specific size
        /// </summary>
        /// <param name="value">String Value</param>
        /// <param name="size">MAX size</param>
        /// <param name="paramName">Parameter Name</param>
        public static void CheckNullOrSize(string value, int size, string paramName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(paramName);

            if (value.Length > size)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Checks if string is longer than specific size
        /// </summary>
        /// <param name="value">String Value</param>
        /// <param name="size">MAX size</param>
        /// <param name="paramName">Parameter Name</param>
        public static void CheckSize(string value, int size, string paramName)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > size)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Checks if string is null
        /// </summary>
        /// <param name="value">String Value</param>
        /// <param name="paramName">Parameter Name</param>
        public static void CheckNull(string value, string paramName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Checks if object is null
        /// </summary>
        /// <param name="value">Object Value</param>
        /// <param name="paramName">Parameter Name</param>
        public static void CheckNull(object value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Functions that returns Description from Enum property
        /// </summary>
        /// <param name="en">Enum property</param>
        /// <returns>Enum property description</returns>
        public static string GetEnumDescription(Enum en)
        {
            var enumType = en.GetType();
            var membersInfo = enumType.GetMember(en.ToString());

            if (membersInfo.Length <= 0)
            {
                return string.Empty;
            }

            object[] attrs = membersInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attrs.Length <= 0)
            {
                return string.Empty;
            }

            return ((DescriptionAttribute)attrs[0]).Description;
        }

        /// <summary>
        /// Function that serialize object as xml
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="overrides">Xml Attribute Overrides</param>
        /// <param name="namespaces">Xml Serializer Namespaces</param>
        /// <returns>Serialize object</returns>
        public static string SerializeObjectAsXml(object obj, XmlAttributeOverrides overrides = null, XmlSerializerNamespaces namespaces = null)
        {
            var serializer = new XmlSerializer(obj.GetType(), overrides);

            var stream = new MemoryStream();
            serializer.Serialize(stream, obj, namespaces);

            stream.Position = 0;
            var reader = new StreamReader(stream, Encoding.UTF8);

            return reader.ReadToEnd();
        }

        /// <summary>
        /// Deserialize xml string as generic object
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="xml">Xml string</param>
        /// <returns>Generic object</returns>
        public static T DeserializeXml<T>(string xml) where T : class
        {
            try
            {
                // set appropriate serializer
                var xmlSerializer = new XmlSerializer(typeof(T));

                // deserialize post data
                var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
                var postDataDeserialized = (T)xmlSerializer.Deserialize(memoryStream);

                return postDataDeserialized;
            }
            catch (Exception ex)
            {
                // Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Checks for valid email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static string PasswordGenerator(int passwordLength)
        {
            try
            {
                var random = new Random();
                int seed = random.Next(1, int.MaxValue);
                const string allowedChars = "abcdefghjkmnpqrstuvwxyzABCDEFGHJKMNPQRSTUVWXYZ123456789";
                const string allowBigChars = "ABCDEFGHJKMNPQRSTUVWXYZ";
                const string allowSmallChars = "abcdefghjkmnpqrstuvwxyz";

                var chars = new char[passwordLength];
                var rd = new Random(seed);
                var smallLetter = allowSmallChars[rd.Next(0, allowSmallChars.Length)];
                var bigLetter = allowBigChars[rd.Next(0, allowBigChars.Length)];
                chars[0] = bigLetter;
                for (var i = 1; i < passwordLength - 1; i++)
                {
                    chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
                }
                chars[5] = smallLetter;
                return new string(chars);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static byte[] GenerateHash(string password)
        {
            try
            {
                // se konvertira password od string vo niza od bajti
                byte[] plainText = new byte[password.Length * sizeof(char)];
                Buffer.BlockCopy(password.ToCharArray(), 0, plainText, 0, plainText.Length);

                HashAlgorithm algorithm = new SHA256Managed();

                byte[] plainTextWithSaltBytes = new byte[plainText.Length];

                for (int i = 0; i < plainText.Length; i++)
                {
                    plainTextWithSaltBytes[i] = plainText[i];
                }
                return algorithm.ComputeHash(plainTextWithSaltBytes);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool CompareByteArrays(byte[] value1, byte[] value2)
        {
            try
            {
                if (value1 == value2) return true;
                if (value1 == null || value2 == null) return false;
                if (value1.Length != value2.Length) return false;

                for (int i = 0; i < value1.Length; i++)
                {
                    if (value1[i] != value2[i]) return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
