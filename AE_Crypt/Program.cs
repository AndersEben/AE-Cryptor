using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_Crypt
{
    static class Program
    {


        [STAThread]
        static void Main()
        {
            IO_Handler _io_handler = new IO_Handler();
            int number = 0;

            while (number < 5)
            {

                _io_handler.WConsole("Willkommen bei dem Ver/Entschlüsselungs Tool von AndersEben.", IO_Handler.C_Stats.grün, IO_Handler.C_Type.NewLine);
                _io_handler.WConsole("Was genau möchtest du tun ?", IO_Handler.C_Stats.grün, IO_Handler.C_Type.NewLine);
                _io_handler.WConsole("[1] - F - Verschlüsseln", IO_Handler.C_Stats.grün, IO_Handler.C_Type.NewLine);
                _io_handler.WConsole("[2] - F - Entschlüsseln ", IO_Handler.C_Stats.grün, IO_Handler.C_Type.NewLine);

                _io_handler.WConsole("[5] - Ende ", IO_Handler.C_Stats.grün, IO_Handler.C_Type.NewLine);
                _io_handler.WConsole("Auswahl : ", IO_Handler.C_Stats.grün, IO_Handler.C_Type.Line);

                try
                {
                    number = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    number = 0;
                }

                switch (number)
                {
                    case 1:

                        OpenFileDialog _dialog = new OpenFileDialog();
                        if (_dialog.ShowDialog() == DialogResult.OK)
                        {
                            string sss = _dialog.SafeFileName;

                            _io_handler.WConsole("Schlüssel eingeben : ", IO_Handler.C_Stats.grün, IO_Handler.C_Type.Line);

                            ConsoleKey key;

                            do
                            {
                                var keyInfo = Console.ReadKey(intercept: true);
                                key = keyInfo.Key;

                                _io_handler.WConsole("*", IO_Handler.C_Stats.rot, IO_Handler.C_Type.Line);

                            } while (key != ConsoleKey.Enter);

                            _io_handler.WConsole("", IO_Handler.C_Stats.grün, IO_Handler.C_Type.NewLine);

                            string _key = key.ToString();

                            string safe_folder = _dialog.FileName.Replace(_dialog.SafeFileName, "");
                            bool encoded = DEENC.EncryptFileToBytes_Aes(_key, sss, safe_folder);

                            if (encoded)
                            {
                                _io_handler.WConsole("Verschlüsselung Abgeschlossen!", IO_Handler.C_Stats.grün, IO_Handler.C_Type.NewLine);
                            }
                            else
                            {
                                _io_handler.WConsole("Verschlüsselung Fehlgeschlagen!", IO_Handler.C_Stats.rot, IO_Handler.C_Type.NewLine);
                            }
                        }
                        Console.ReadLine();


                        break;
                    case 2:

                        OpenFileDialog _dialog_de = new OpenFileDialog();
                        if (_dialog_de.ShowDialog() == DialogResult.OK)
                        {
                            string safe_name_de = _dialog_de.SafeFileName;
                            string safe_folder_de = _dialog_de.FileName.Replace(_dialog_de.SafeFileName, "");

                            _io_handler.WConsole("Schlüssel eingeben : ", IO_Handler.C_Stats.grün, IO_Handler.C_Type.Line);

                            ConsoleKey key;

                            do
                            {
                                var keyInfo = Console.ReadKey(intercept: true);
                                key = keyInfo.Key;

                                _io_handler.WConsole("*", IO_Handler.C_Stats.rot, IO_Handler.C_Type.Line);

                            } while (key != ConsoleKey.Enter);

                            string _enc_key = key.ToString();

                            _io_handler.WConsole("", IO_Handler.C_Stats.grün, IO_Handler.C_Type.NewLine);

                            bool decoded = DEENC.DecryptFileFromBytes_Aes(_enc_key, safe_name_de, safe_folder_de);

                            if (decoded)
                            {
                                _io_handler.WConsole("Entschlüsselung Abgeschlossen!", IO_Handler.C_Stats.grün, IO_Handler.C_Type.NewLine);
                            }
                            else
                            {
                                _io_handler.WConsole("Entschlüsselung Fehlgeschlagen!", IO_Handler.C_Stats.rot, IO_Handler.C_Type.NewLine);
                            }

                        }
                        Console.ReadLine();

                        break;
                }

                Console.Clear();

            }
        }
    }
}
