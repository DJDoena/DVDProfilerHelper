using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public static class ProfilePhotoHelper
    {
        public static String FileNameFromCreditName(String firstName
            , String middleName
            , String lastName
            , Int32 birthYear)
        {
            String fileName;
            StringBuilder cleanName;
            bool nameWasCleaned;

            fileName = lastName + "_" + firstName + "_" + middleName;

            nameWasCleaned = false;
            cleanName = new StringBuilder();

            foreach (Char c in fileName)
            {
                const String InvalidChars = "<>:\"\\/|?*";

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
                Int32 hash;

                hash = MiniHashFromString(fileName);
                hash = Math.Abs(hash);

                cleanName.Append("_");
                cleanName.Append(hash);

                fileName = cleanName.ToString();
            }

            if (birthYear > 0)
            {
                fileName += "_" + birthYear;
            }

            return (fileName);
        }

        private static Int32 MiniHashFromString(String source)
        {
            Int32 hash;
            HashAlgorithm hasher;
            Byte[] sourceBytes;
            Byte[] hashBytes;

            hasher = new MD5CryptoServiceProvider();
            hash = 0;

            sourceBytes = Encoding.ASCII.GetBytes(source);
            hashBytes = hasher.ComputeHash(sourceBytes);

            for (Int32 b = 0; b < hashBytes.Length; b += 4)
            {
                Int32 section;

                section = (hashBytes[b] << 24) + (hashBytes[b + 1] << 16) + (hashBytes[b + 2] << 8) + hashBytes[b + 3];
                hash ^= section;
            }

            if (hash == 0)
            {
                hash = 1;
            }

            return (hash);
        }

        public static String CleanupFilename(String badFileName)
        {
            badFileName = badFileName.Replace(":", "-");
            badFileName = badFileName.Replace("\"", "'");
            badFileName = badFileName.Replace("?", "");
            badFileName = badFileName.Replace("/", "");
            badFileName = badFileName.Replace("\\", "");
            badFileName = badFileName.Replace("*", "");
            badFileName = badFileName.Replace(">", "");
            badFileName = badFileName.Replace("<", "");
            foreach (Char ipc in Path.GetInvalidFileNameChars())
            {
                badFileName = badFileName.Replace(ipc, ' ');
            }
            return (badFileName);
        }
    }
}
