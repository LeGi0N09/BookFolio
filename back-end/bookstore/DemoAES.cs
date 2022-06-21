
using System.Security.Cryptography;
using System.Text;

namespace bookstore
{
   public  class DemoAES
    {
        public  string ComputeMD5Hash(string input)
        {   
            StringBuilder stringBuilder = new StringBuilder();
            byte[] textbytes = Encoding.ASCII.GetBytes(input);
            using (MD5 md5=MD5.Create())
            {   
                byte[] computehash = md5.ComputeHash(textbytes);
                for (int i=0;i<computehash.Length; i++)
                {
                    stringBuilder.Append(computehash[i].ToString("x2"));
                }
            }
            return stringBuilder.ToString();
        }
    }
}
