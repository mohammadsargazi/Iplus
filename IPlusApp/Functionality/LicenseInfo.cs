using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IPlusApp.Functionality
{
    public sealed class LicenseInfo
    {
        private const int magic_pub_idx = 20;
        private const int magic_size = 4;
        private LicenseInfo.Info I;

        /// <summary>Determines if component has activated license key.</summary>
        public bool IsLicensed
        {
            get
            {
                return this.I.IsLicensed;
            }
        }

        /// <summary>License owner identifier.</summary>
        public string LicenseOwner
        {
            get
            {
                return this.I.Owner;
            }
        }

        internal LicenseInfo()
        {
            this.I = new LicenseInfo.Info();
            this.I.IsLicensed = false;
        }

        internal void Check()
        {
            if (!this.IsLicensed || string.IsNullOrEmpty(this.LicenseOwner))
            {
                string appSetting1 = ConfigurationSettings.AppSettings["NReco.PdfGenerator.LicenseKey"];
                string appSetting2 = ConfigurationSettings.AppSettings["NReco.PdfGenerator.LicenseOwner"];
                if (!string.IsNullOrEmpty(appSetting1) && !string.IsNullOrEmpty(appSetting2))
                {
                    this.SetLicenseKey(appSetting2, appSetting1);
                    if (this.IsLicensed && !string.IsNullOrEmpty(this.LicenseOwner))
                        return;
                }
                throw new Exception("This feature requires PdfGenerator commercial license key: http://www.nrecosite.com/pdf_generator_net.aspx");
            }
        }

        /// <summary>
        /// Activate component license and enable restricted features.
        /// </summary>
        /// <param name="owner">license owner ID</param>
        /// <param name="key">unique license key from component order's page</param>
        public void SetLicenseKey(string owner, string key)
        {
            byte[] licenseKeyBytes = this.GetLicenseKeyBytes(key);
            byte[] publicKey = typeof(LicenseInfo).Assembly.GetName().GetPublicKey();
            if (publicKey == null)
                throw new Exception("PdfGenerator is not strongly signed");
            using (RSACryptoServiceProvider cryptoServiceProvider1 = new RSACryptoServiceProvider(new CspParameters()
            {
                Flags = CspProviderFlags.UseMachineKeyStore
            }))
            {
                RSAParameters keyRsaParameters = LicenseInfo.GetPublicKeyRSAParameters(publicKey);
                cryptoServiceProvider1.PersistKeyInCsp = false;
                cryptoServiceProvider1.ImportParameters(keyRsaParameters);
                SHA1CryptoServiceProvider cryptoServiceProvider2 = new SHA1CryptoServiceProvider();
                try
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(owner);
                    if (!cryptoServiceProvider1.VerifyData(bytes, (object)cryptoServiceProvider2, licenseKeyBytes))
                        throw new Exception();
                    this.I.Owner = owner;
                    this.I.IsLicensed = !string.IsNullOrEmpty(this.I.Owner);
                }
                catch (Exception ex)
                {
                    throw new Exception("Invalid license owner or key");
                }
            }
        }

        private byte[] GetLicenseKeyBytes(string key)
        {
            try
            {
                return Convert.FromBase64String(key);
            }
            catch
            {
                throw new Exception("Invalid license key");
            }
        }

        private static RSAParameters GetPublicKeyRSAParameters(byte[] keyBytes)
        {
            RSAParameters rsaParameters = new RSAParameters();
            if (keyBytes == null || keyBytes.Length < 1)
                throw new ArgumentNullException(nameof(keyBytes));
            bool flag = true;
            int startAt1 = 20 + 4 + 4;
            int size1 = 4;
            int startAt2 = startAt1 + size1;
            int size2 = 128;
            int startAt3 = startAt2 + size2;
            int size3 = 64;
            int startAt4 = startAt3 + size3;
            int size4 = 64;
            int startAt5 = startAt4 + size4;
            int size5 = 64;
            int startAt6 = startAt5 + size5;
            int size6 = 64;
            int startAt7 = startAt6 + size6;
            int size7 = 64;
            int startAt8 = startAt7 + size7;
            int size8 = 128;
            rsaParameters.Exponent = LicenseInfo.BlockCopy(keyBytes, startAt1, size1);
            Array.Reverse((Array)rsaParameters.Exponent);
            rsaParameters.Modulus = LicenseInfo.BlockCopy(keyBytes, startAt2, size2);
            Array.Reverse((Array)rsaParameters.Modulus);
            if (flag)
                return rsaParameters;
            rsaParameters.P = LicenseInfo.BlockCopy(keyBytes, startAt3, size3);
            Array.Reverse((Array)rsaParameters.P);
            rsaParameters.Q = LicenseInfo.BlockCopy(keyBytes, startAt4, size4);
            Array.Reverse((Array)rsaParameters.Q);
            rsaParameters.DP = LicenseInfo.BlockCopy(keyBytes, startAt5, size5);
            Array.Reverse((Array)rsaParameters.DP);
            rsaParameters.DQ = LicenseInfo.BlockCopy(keyBytes, startAt6, size6);
            Array.Reverse((Array)rsaParameters.DQ);
            rsaParameters.InverseQ = LicenseInfo.BlockCopy(keyBytes, startAt7, size7);
            Array.Reverse((Array)rsaParameters.InverseQ);
            rsaParameters.D = LicenseInfo.BlockCopy(keyBytes, startAt8, size8);
            Array.Reverse((Array)rsaParameters.D);
            return rsaParameters;
        }

        private static byte[] BlockCopy(byte[] source, int startAt, int size)
        {
            if (source == null || source.Length < startAt + size)
                return (byte[])null;
            byte[] numArray = new byte[size];
            Buffer.BlockCopy((Array)source, startAt, (Array)numArray, 0, size);
            return numArray;
        }

        internal sealed class Info
        {
            internal bool IsLicensed;
            internal string Owner;

            internal Info()
            {
            }
        }
    }
}
