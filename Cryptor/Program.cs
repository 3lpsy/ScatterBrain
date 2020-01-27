﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Cryptor
{
    class Program
    {
        static byte[] XorByteArray(byte[] origBytes, char[] cryptor)
        {
            byte[] result = new byte[origBytes.Length];
            int j = 0;
            for (int i = 0; i < origBytes.Length; i++)
            {
                if (j == cryptor.Length - 1)
                {
                    j = 0;
                }
                byte res = (byte)(origBytes[i] ^ Convert.ToByte(cryptor[j]));
                result[i] = res;
                j += 1;
            }
            return result;
        }

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("ERROR: Need to pass only the path to the shell code file to encrypt.");
                Environment.Exit(1);
            }
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Could not find path to shellcode bin file: {0}", args[0]);
                Environment.Exit(1);
            }
            byte[] shellcodeBytes = File.ReadAllBytes(args[0]);
            char[] cryptor = new char[] { 'S', 'e', 'c', 'r', 'e', 't', 'K', 'e', 'y', '\0' };
            byte[] encShellcodeBytes = XorByteArray(shellcodeBytes, cryptor);
            File.WriteAllBytes("encrypted.bin", encShellcodeBytes);
            Console.WriteLine("Wrote encoded binary to encrypted.bin. View it in HxD and copy to RawData.h of ScatterBrain.");
        }
    }
}
