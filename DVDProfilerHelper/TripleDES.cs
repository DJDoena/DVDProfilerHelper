using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    internal class TripleDES
    {
        // define the triple des provider

        private readonly System.Security.Cryptography.TripleDES _des = System.Security.Cryptography.TripleDES.Create();

        // define the string handler

        private readonly UTF8Encoding _utf8 = new UTF8Encoding();

        // define the local property arrays

        private readonly byte[] _key;

        private readonly byte[] _iv;

        public TripleDES(byte[] key, byte[] iv)
        {
            _key = key;
            _iv = iv;
        }

        public byte[] Encrypt(byte[] input)
            => this.Transform(input, _des.CreateEncryptor(_key, _iv));

        public byte[] Decrypt(byte[] input)
            => this.Transform(input, _des.CreateDecryptor(_key, _iv));

        public string Encrypt(string text)
        {
            var input = _utf8.GetBytes(text);

            var output = Transform(input, _des.CreateEncryptor(_key, _iv));

            var result = Convert.ToBase64String(output);

            return result;
        }

        public string Decrypt(string text)
        {
            var input = Convert.FromBase64String(text);

            var output = Transform(input, _des.CreateDecryptor(_key, _iv));

            var result = _utf8.GetString(output);

            return result;
        }

        private byte[] Transform(byte[] input, ICryptoTransform CryptoTransform)
        {
            // create the necessary streams

            using (var memStream = new MemoryStream())
            {
                using (var cryptStream = new CryptoStream(memStream, CryptoTransform, CryptoStreamMode.Write))
                {
                    // transform the bytes as requested
                    cryptStream.Write(input, 0, input.Length);

                    cryptStream.FlushFinalBlock();

                    // Read the memory stream and
                    // convert it back into byte array
                    memStream.Position = 0;

                    var result = memStream.ToArray();

                    // close and release the streams
                    memStream.Close();

                    cryptStream.Close();

                    // hand back the encrypted buffer
                    return result;
                }
            }
        }
    }
}