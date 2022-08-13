
using CustomEncryptor;

var p = new PassEncryptor();

// var encrypt1 = p.NextEncrypt_1("ermek~", 12);
// Console.WriteLine($"Encrypt: {encrypt1}");
//
// var decrypt1 = p.NextDecrypt_1(encrypt1);
// Console.WriteLine($"Decrypt: {decrypt1}");
//
// var byteBaseEn = p.ByteBaseEnc("ermek");
// Console.WriteLine($"ByteBaseEnc: {byteBaseEn}");
//
// var byteBaseEn2 = p.ByteBaseEnc2("ermek");
// Console.WriteLine($"ByteBaseEnc2: {byteBaseEn2}");

// var byteBaseEnAdvanced = p.ByteBaseEncAdvanced("ermek");
// Console.WriteLine($"byteBaseEnAdvanced: {byteBaseEnAdvanced}");
//
// var byteBaseDecAdvanced = p.ByteBaseDecAdvanced(byteBaseEnAdvanced);
// Console.WriteLine($"byteBaseDecAdvanced: {byteBaseDecAdvanced}");


var randomByteBaseEnc = p.RandomByteBaseDec("w12345","my kwey");
Console.WriteLine($"randomByteBaseEnc: {randomByteBaseEnc}");

var sha256 = p.SHA256_Enc("w12345");
Console.WriteLine($"sha256: {sha256}");

var md5 = p.MD5_Enc("w12345");
Console.WriteLine($"md5: {md5}");

Console.WriteLine("Hello, World!");
Console.ReadLine();
