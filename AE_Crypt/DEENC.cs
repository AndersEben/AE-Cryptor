using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AE_Crypt
{
    public class DEENC
    {

        public static bool EncryptFileToBytes_Aes(string _user_key, string filename, string filepath)
        {
            byte[] encrypted;

            string _ext = Path.GetExtension(filepath + filename);
            string _repl_fiename = filename.Replace(_ext, "");

            byte[] _read = File.ReadAllBytes(filepath + filename);

            if (_read == null || _read.Length <= 0)
            {
                return false;
                throw new ArgumentNullException("InputFehler");
            }
            if (_user_key == null || _user_key.Length <= 0)
            {
                return false;
                throw new ArgumentNullException("InputFehler");
            }

            RijndaelManaged myAlg = new RijndaelManaged();

            int lenght = _read.Length;
            int erw = 0;

            while (lenght % 16 != 0)
            {
                erw++;
                lenght++;
            }

            byte[] test = Encoding.UTF8.GetBytes(erw.ToString());

            int number = lenght * 12345678;

            byte[] salt = Encoding.ASCII.GetBytes(number.ToString());

            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(_user_key, salt);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Padding = PaddingMode.Zeros;
                aesAlg.Key = key.GetBytes(myAlg.KeySize / 8);
                aesAlg.IV = key.GetBytes(myAlg.BlockSize / 8);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.BaseStream.Write(_read, 0, _read.Length);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            byte[] bArray = addByteToArray(encrypted, test);
            byte[] test_ext = Encoding.UTF8.GetBytes("||" + _ext);
            byte[] bArray_ext = addByteToArray(bArray, test_ext);

            File.WriteAllBytes(filepath + _repl_fiename + "-enc" + ".aec", bArray_ext);

            return true;

        }

        public static bool DecryptFileFromBytes_Aes(string _user_key, string filename, string filepath)
        {

            byte[] _read2 = File.ReadAllBytes(filepath + filename);

            int zahl = 0;
            string fileext = "";
            byte[] _read = removeBytefromArray(_read2, ref zahl, ref fileext);


            string _ext = Path.GetExtension(filepath + filename);
            string _repl_fiename = filename.Replace(_ext, "").Replace("-enc", "");

            if (_read == null || _read.Length <= 0)
            {
                return false;
                throw new ArgumentNullException("InputFehler");
            }
            if (_user_key == null || _user_key.Length <= 0)
            {
                return false;
                throw new ArgumentNullException("InputFehler");
            }

            byte[] output = new byte[_read.Length];



            RijndaelManaged myAlg = new RijndaelManaged();

            int number = _read.Length * 12345678;
            byte[] salt = Encoding.ASCII.GetBytes(number.ToString());

            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(_user_key, salt);


            if (_read.Length % 16 == 0)
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Padding = PaddingMode.Zeros;
                    aesAlg.Key = key.GetBytes(myAlg.KeySize / 8);
                    aesAlg.IV = key.GetBytes(myAlg.BlockSize / 8);

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(_read))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                srDecrypt.BaseStream.Read(output, 0, _read.Length);
                            }
                        }
                    }
                }
            }


            byte[] rem_pad_out = new byte[output.Length - zahl];
            Array.Copy(output, rem_pad_out, output.Length - zahl);

            File.WriteAllBytes(filepath + _repl_fiename + "-dec" + fileext, rem_pad_out);

            return true;

        }


        public static byte[] EncryptBytes_Aes(string _user_key, byte[] File)
        {
            byte[] encrypted;
            byte[] _read = File;

            if (_read == null || _read.Length <= 0)
            {
                return null;
                throw new ArgumentNullException("InputFehler");
            }
            if (_user_key == null || _user_key.Length <= 0)
            {
                return null;
                throw new ArgumentNullException("InputFehler");
            }

            RijndaelManaged myAlg = new RijndaelManaged();

            int lenght = _read.Length;
            int number = lenght * 12345678;

            byte[] salt = Encoding.ASCII.GetBytes(number.ToString());

            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(_user_key, salt);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Padding = PaddingMode.Zeros;
                aesAlg.Key = key.GetBytes(myAlg.KeySize / 8);
                aesAlg.IV = key.GetBytes(myAlg.BlockSize / 8);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.BaseStream.Write(_read, 0, _read.Length);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;

        }

        public static byte[] DecryptBytes_Aes(string _user_key, byte[] File)
        {

            byte[] _read = File;

            if (_read == null || _read.Length <= 0)
            {
                return null;
                throw new ArgumentNullException("InputFehler");
            }
            if (_user_key == null || _user_key.Length <= 0)
            {
                return null;
                throw new ArgumentNullException("InputFehler");
            }

            byte[] output = new byte[_read.Length];



            RijndaelManaged myAlg = new RijndaelManaged();

            int number = _read.Length * 12345678;
            byte[] salt = Encoding.ASCII.GetBytes(number.ToString());

            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(_user_key, salt);


            if (_read.Length % 16 == 0)
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Padding = PaddingMode.Zeros;
                    aesAlg.Key = key.GetBytes(myAlg.KeySize / 8);
                    aesAlg.IV = key.GetBytes(myAlg.BlockSize / 8);

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(_read))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                srDecrypt.BaseStream.Read(output, 0, _read.Length);
                            }
                        }
                    }
                }
            }

            return output;

        }


        static byte[] addByteToArray(byte[] bArray, byte[] newBytes)
        {
            byte[] newArray = new byte[bArray.Length + newBytes.Length];
            Array.Copy(bArray, newArray, bArray.Length);

            for (int i = 0; i < newBytes.Length; i++)
            {
                newArray[bArray.Length + i] = newBytes[i];
            }

            return newArray;

        }

        static byte[] removeBytefromArray(byte[] bArray, ref int zauberzahl, ref string fileext)
        {

            int lenght = bArray.Length;
            while (lenght % 16 != 0)
            {
                lenght--;
            }

            int count = bArray.Length - lenght;

            if (count > 0)
            {
                byte[] _counter = new byte[count];
                for (int i = 0; i < count; i++)
                {
                    _counter[i] = bArray[bArray.Length - (count - i)];
                }


                try
                {
                    string _ext = System.Text.Encoding.UTF8.GetString(_counter);

                    string[] stringSeparators = new string[] { "||" };
                    string[] _files = _ext.Split(stringSeparators, StringSplitOptions.None);

                    zauberzahl = Convert.ToInt32(_files[0]);
                    fileext = _files[1];
                }
                catch (Exception)
                {
                    zauberzahl = 0;
                    fileext = ".txt";
                }
            }

            byte[] newArray = new byte[lenght];

            Array.Copy(bArray, newArray, lenght);

            return newArray;

        }

    }
}
