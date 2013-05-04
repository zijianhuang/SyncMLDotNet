using System;
using System.Collections.Generic;
using System.Text;

namespace Fonlow.SyncML.Common
{
    /// <summary>
    /// Common little functions used in SyncML projects
    /// </summary>
    public sealed class Utility
    {
        private Utility()
        {

        }
        /// <summary>
        /// Convert utf8 text to Base64 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ConvertUtf8TextToBase64(string text)
        {
            Encoding conv = Encoding.UTF8;
            return Convert.ToBase64String(conv.GetBytes(text));
        }

        /// <summary>
        /// Get text from base64 encoded text.
        /// </summary>
        /// <param name="base64Text"></param>
        /// <returns>Utf8 text</returns>
        public static string ConvertFromBase64(string base64Text)
        {
            Encoding conv = Encoding.UTF8;
            return conv.GetString(Convert.FromBase64String(base64Text));
        }

        /// <summary>
        /// Generate MD5 digest for SyncML authentication
        /// </summary>
        /// <param name="user">User name of SyncML server</param>
        /// <param name="password"></param>
        /// <param name="nonce"></param>
        /// <returns>MD5 digest</returns>
        public static string GenerateSyncMLMD5(string user, string password, byte[] nonce)
        {
            if (nonce == null)
            {
                throw new ArgumentNullException("nonce");
            }

            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                md5.ComputeHash(Encoding.ASCII.GetBytes(user + ":" + password));

                string s = Convert.ToBase64String(md5.Hash) + ":";
                byte[] sb = Encoding.ASCII.GetBytes(s);
                byte[] concat = new byte[sb.Length + nonce.Length];

                Buffer.BlockCopy(sb, 0, concat, 0, sb.Length);
                Buffer.BlockCopy(nonce, 0, concat, sb.Length, nonce.Length);

                md5.ComputeHash(concat);
                return Convert.ToBase64String(md5.Hash);
            }
        }

    }

    public static class DateFunctions
    {
        public static DateTime StartDateOfLastWeek
        {
            get
            {
                return GetStartDateOfLastWeek(DateTime.Today);
            }
        }

        public static DateTime GetStartDateOfLastWeek(DateTime today)
        {
            return today.AddDays(-(int)today.DayOfWeek - 7);
        }

        public static DateTime StartDateOfLast2Weeks
        {
            get
            {
                return GetStartDateOfLast2Weeks(DateTime.Today);
            }
        }

        public static DateTime GetStartDateOfLast2Weeks(DateTime today)
        {
            return today.AddDays(-(int)today.DayOfWeek - 14);
        }

        public static DateTime StartDateOfLastMonth
        {
            get
            {
                return GetStartDateOfLastMonth(DateTime.Today);
            }
        }

        public static DateTime GetStartDateOfLastMonth(DateTime today)
        {
            return today.AddMonths(-1).AddDays(-today.Day+1);
        }

        public static DateTime StartDateOfLast3Months
        {
            get
            {
                return GetStartDateOfLast3Months(DateTime.Today);
            }
        }

        public static DateTime GetStartDateOfLast3Months(DateTime today)
        {
            return today.AddMonths(-3).AddDays(-today.Day+1);
        }

        public static DateTime StartDateOfLast6Months
        {
            get
            {
                return GetStartDateOfLast6Months(DateTime.Today);
            }
        }

        public static DateTime GetStartDateOfLast6Months(DateTime today)
        {
            return today.AddMonths(-6).AddDays(-today.Day+1);
        }
    }

}
