using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using KeePassLib;
using KeePassLib.Cryptography;
using KeePassLib.Cryptography.PasswordGenerator;
using KeePassLib.Security;

namespace Cockroach
{
    public sealed class CockroachGen:CustomPwGenerator
    {
        // Cockroach rule
        private Int16[] consonants = new Int16[] { 98,99,100,102,103,104,106,107,108,109,110,112,113,114,115,116,118,119,120,121,122 };
        private Int16[] vowels = new Int16[] { 97, 101, 105, 111, 117 };
        
        private string cockPwdGenerator(int l,Random r)
        {            
            string strSpecialLjm = "ljm";
            string strSpecialDate = DateTime.Now.ToString("yyMMdd");
            string strPwd=strSpecialLjm+strSpecialDate;
            for (int i = 0; i < l; i++)
            {
                if (i % 2 == 0)                
                    strPwd += (char)consonants[r.Next(21)];                
                else
                    strPwd += (char)vowels[r.Next(5)];
            }
            return strPwd;

        }
        //

        private static readonly PwUuid m_uuid = new PwUuid(new byte[] {
			0x53, 0x81, 0x36, 0xCE, 0xAE, 0xFC, 0x48, 0x3F,
			0x9E, 0x90, 0xA4, 0x4F, 0x1A, 0xF0, 0x58, 0x95 });
        
        public override PwUuid Uuid
        {
            get { return m_uuid; }
        }

        public override string Name
        {
            get { return "Cockroach"; }
        }

        public override ProtectedString Generate(PwProfile prf, CryptoRandomStream crsRandomSource)
        {
            if (prf == null) { Debug.Assert(false); }
            else
            {
                Debug.Assert(prf.CustomAlgorithmUuid == Convert.ToBase64String(
                    m_uuid.UuidBytes, Base64FormattingOptions.None));
            }
            Random keylen = new Random((int)crsRandomSource.GetRandomUInt64());
            int k = keylen.Next(3, 7);
            return new ProtectedString(false, cockPwdGenerator(k,keylen));
        }
        
    }
}
