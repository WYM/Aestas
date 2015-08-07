using UnityEngine;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

public class Encrypt : ScriptableObject {


    public static void EncryptFile(string filePath, string savePath, string keyStr)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        FileStream fs = File.OpenRead(filePath);
        byte[] inputByteArray = new byte[fs.Length];
        fs.Read(inputByteArray, 0, (int)fs.Length);
        fs.Close();

        byte[] keyByteArray = Encoding.Default.GetBytes(keyStr);
        SHA1 ha = new SHA1Managed();
        byte[] hb = ha.ComputeHash(keyByteArray);
        byte[] sKey = new byte[8];
        byte[] sIV = new byte[8];
        for (int i = 0; i < 8; i++)
            sKey[i] = hb[i];
        for (int i = 8; i < 16; i++)
            sIV[i - 8] = hb[i];
        des.Key = sKey;
        des.IV = sIV;

        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();

        fs = File.OpenWrite(savePath);
        foreach (byte b in ms.ToArray())
        {
            fs.WriteByte(b);
        }
        fs.Close();
        cs.Close();
        ms.Close();
    }

    public static void DecryptFile(string filePath, string savePath, string keyStr)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        FileStream fs = File.OpenRead(filePath);
        byte[] inputByteArray = new byte[fs.Length];
        fs.Read(inputByteArray, 0, (int)fs.Length);
        fs.Close();

        byte[] keyByteArray = Encoding.Default.GetBytes(keyStr);
        SHA1 ha = new SHA1Managed();
        byte[] hb = ha.ComputeHash(keyByteArray);
        byte[] sKey = new byte[8];
        byte[] sIV = new byte[8];
        for (int i = 0; i < 8; i++)
            sKey[i] = hb[i];
        for (int i = 8; i < 16; i++)
            sIV[i - 8] = hb[i];
        des.Key = sKey;
        des.IV = sIV;

        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();

        fs = File.OpenWrite(savePath);
        foreach (byte b in ms.ToArray())
        {
            fs.WriteByte(b);
        }
        fs.Close();
        cs.Close();
        ms.Close();
    }
}
