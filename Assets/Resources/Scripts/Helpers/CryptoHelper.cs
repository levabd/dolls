using System;
using System.Text;
using System.Security.Cryptography;

// ReSharper disable once CheckNamespace
public static class CryptoHelper
{
    public static string GetPasswordHash(string input, string login, int timestamp)
    {
        MD5 md5Hash = MD5.Create();

        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input + "GLRSHguewuni4e" + timestamp));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        foreach (byte t in data) sBuilder.Append(t.ToString("x2"));

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }
}
