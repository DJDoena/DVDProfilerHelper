using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public static class ProfilePhotoHelper
    {
        public static string FileNameFromCreditName(string firstName, string middleName, string lastName, int birthYear)
        {
            var fileName = lastName + "_" + firstName + "_" + middleName;

            var nameWasCleaned = false;

            var cleanName = new StringBuilder();

            foreach (var c in fileName)
            {
                const string InvalidChars = "<>:\"\\/|?*";

                if (InvalidChars.Contains(c.ToString()))
                {
                    nameWasCleaned = true;
                }
                else
                {
                    cleanName.Append(c);
                }
            }

            if (nameWasCleaned)
            {
                var hash = MiniHashFromString(fileName);

                hash = Math.Abs(hash);

                cleanName.Append("_");
                cleanName.Append(hash);

                fileName = cleanName.ToString();
            }

            if (birthYear > 0)
            {
                fileName += "_" + birthYear;
            }

            return fileName;
        }

        private static int MiniHashFromString(string source)
        {
            var hasher = new MD5CryptoServiceProvider();

            var hash = 0;

            var sourceBytes = Encoding.ASCII.GetBytes(source);

            var hashBytes = hasher.ComputeHash(sourceBytes);

            for (int b = 0; b < hashBytes.Length; b += 4)
            {
                var section = (hashBytes[b] << 24) + (hashBytes[b + 1] << 16) + (hashBytes[b + 2] << 8) + hashBytes[b + 3];

                hash ^= section;
            }

            if (hash == 0)
            {
                hash = 1;
            }

            return hash;
        }

        public static string CleanupFilename(string badFileName)
        {
            badFileName = badFileName.Replace(":", "-");
            badFileName = badFileName.Replace("\"", "'");
            badFileName = badFileName.Replace("?", "");
            badFileName = badFileName.Replace("/", "");
            badFileName = badFileName.Replace("\\", "");
            badFileName = badFileName.Replace("*", "");
            badFileName = badFileName.Replace(">", "");
            badFileName = badFileName.Replace("<", "");

            foreach (var ipc in Path.GetInvalidFileNameChars())
            {
                badFileName = badFileName.Replace(ipc, ' ');
            }

            return badFileName;
        }
    }
}