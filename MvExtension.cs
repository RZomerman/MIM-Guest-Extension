using System;
using System.Text;
using Microsoft.MetadirectoryServices;

namespace AAD_B2B_Guest_MIM_Extensions
{
    public class MvExtension : IMVSynchronization
    {
        public void Initialize()
        {
        }

        public static class Log
        {
            public static void Debug(string message, params object[] args)
            {
                message = string.Format(message, args);

                System.Diagnostics.Debug.WriteLine("[predica] " + message);
            }
        }
        public void Provision(MVEntry mventry)
        {
            string maName = "ADMA";
            ManagementAgent ma = mventry.ConnectedMAs[maName];
            //begin  changes
            if(mventry.ConnectedMAs[maName].Connectors.Count == 0)
            {
                //provision new AD object
                if(mventry["uid"].IsPresent && mventry["accountName"].IsPresent && mventry["ou"].IsPresent)
                {
                    string cn = string.Format("CN={0}", mventry["uid"].StringValue);
                    ReferenceValue dn = mventry.ConnectedMAs[maName].EscapeDNComponent(cn).Concat(mventry["ou"].StringValue);
                    CSEntry adCSentry = mventry.ConnectedMAs[maName].Connectors.StartNewConnector("user");
                    adCSentry.DN = dn;
                    string pwd = GenerateRandomString(32);
                    adCSentry["unicodePwd"].Value = pwd;
                    Log.Debug(pwd);

                    adCSentry["SamAccountName"].StringValue = mventry["AccountName"].StringValue;

                    adCSentry.CommitNewConnector();
                }
            }
            else
            {
                //rename existing AD object 
                CSEntry adCSentry = mventry.ConnectedMAs[maName].Connectors.ByIndex[0];
                ReferenceValue newDn = mventry.ConnectedMAs[maName].EscapeDNComponent(string.Format("Cn={0}", mventry["uid"].StringValue)).Concat(mventry["ou"].StringValue);
                adCSentry.DN = newDn;
            }
        }
        //endchanges
        public bool ShouldDeleteFromMV(CSEntry csentry, MVEntry mventry)
        {
            throw new NotImplementedException();
        }

        public void Terminate()
        {
        }

        const string LOWER_CASE = "abcdefghijklmnopqursuvwxyz";
        const string UPPER_CAES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string NUMBERS = "123456789";
        const string SPECIALS = @"!@£$%^&*()#€";
        private string GenerateRandomString(int len)
        {
            //source: https://seanmccammon.com/random-password-generator-in-c/
            char[] _password = new char[len];
            string charSet = ""; // Initialise to blank
            System.Random _random = new Random();
            int counter;

            // Build up the character set to choose from
            charSet += LOWER_CASE;
            charSet += UPPER_CAES;
            charSet += NUMBERS;
            charSet += SPECIALS;

            for (counter = 0; counter < len; counter++)
            {
                _password[counter] = charSet[_random.Next(charSet.Length - 1)];
            }
            //return String
            return String.Join("", _password);
        }

    }
}
