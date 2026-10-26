using System;
using System.IO;
using System.Security.Cryptography;

namespace SkrinshoterApp;

internal class SkrAes128
{
	private byte[] cipherText;

	private string plainText;

	private readonly byte[] key = new byte[16]
	{
		49, 50, 61, 52, 53, 54, 55, 56, 57, 90,
		49, 115, 51, 52, 47, 54
	};

	public SkrAes128(byte[] aes128UserData)
	{
		cipherText = aes128UserData;
	}

	public SkrAes128(byte[] key, byte[] aes128UserData)
	{
		this.key = key;
		cipherText = aes128UserData;
	}

	public SkrAes128(string text)
	{
		plainText = text;
	}

	public SkrAes128(byte[] key, string text)
	{
		this.key = key;
		plainText = text;
	}

	internal string Decode()
	{
		if (cipherText == null)
		{
			throw new InvalidOperationException("Initialized for encoding only");
		}
		if (cipherText == null || cipherText.Length == 0)
		{
			throw new ArgumentNullException("cipherText");
		}
		if (key == null || key.Length != 16)
		{
			throw new ArgumentException("Key needs to be 16 bytes for AES128.");
		}
		string text = null;
		using Aes aes = Aes.Create();
		aes.KeySize = 128;
		aes.Key = key;
		aes.Mode = CipherMode.CBC;
		aes.Padding = PaddingMode.None;
		aes.IV = new byte[16];
		ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);
		using MemoryStream stream = new MemoryStream(cipherText);
		using CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
		using StreamReader streamReader = new StreamReader(stream2);
		return streamReader.ReadToEnd().TrimEnd(default(char));
	}

	public byte[] Encode()
	{
		if (plainText == null || plainText.Length <= 0)
		{
			throw new ArgumentNullException("plainText");
		}
		if (key == null || key.Length != 16)
		{
			throw new ArgumentException("Key needs to be 16 bytes for AES128.", "key");
		}
		using Aes aes = Aes.Create();
		aes.KeySize = 128;
		aes.Key = key;
		aes.Mode = CipherMode.CBC;
		aes.Padding = PaddingMode.Zeros;
		aes.IV = new byte[16];
		ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);
		using MemoryStream memoryStream = new MemoryStream();
		using (CryptoStream stream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
		{
			using StreamWriter streamWriter = new StreamWriter(stream);
			streamWriter.Write(plainText);
		}
		return memoryStream.ToArray();
	}
}
