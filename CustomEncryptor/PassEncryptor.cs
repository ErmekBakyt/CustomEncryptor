using System.Security.Cryptography;
using System.Text;

namespace CustomEncryptor;

public class PassEncryptor
{
    private char[] fullChar = new[]
    {
        '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
        'W', 'X', 'Y', 'Z'
    };

    public string NextEncrypt_1(string password, int index = 1)
    {
        var sb = new StringBuilder();
        foreach (var ch in password)
        {
            var byteCode = (byte)ch;
            var newIndex = byteCode + index + 3;
            var newChar = (char)newIndex;
            sb.Append(newChar);
        }

        sb.Append($".{index}");
        return sb.ToString();
    }

    public string NextDecrypt_1(string encrypted)
    {
        var nextIndex = encrypted.Contains('.') ? int.Parse(encrypted.Substring(encrypted.LastIndexOf('.') + 1)) : 1;

        encrypted = encrypted.Substring(0, encrypted.LastIndexOf('.'));

        var sb = new StringBuilder();
        foreach (var ch in encrypted)
        {
            var byteCode = (byte)ch;
            var newIndex = byteCode - nextIndex - 3;
            var newChar = (char)newIndex;
            sb.Append(newChar);
        }

        return sb.ToString();
    }

    #region ByteBaseEnc

    public string ByteBaseEnc(string password) // Custom
    {
        var sb = new StringBuilder();
        foreach (var ch in password)
        {
            var byteCode = (byte)ch;
            sb.Append(byteCode);
        }

        return sb.ToString();
    }

    public string ByteBaseEnc2(string password) // Built in
    {
        var ascii = Encoding.ASCII.GetBytes(password);
        return string.Join("", ascii);
    }

    #endregion


    public string ByteBaseEncAdvanced(string password)
    {
        List<string> randomNumbers = new();
        var sb = new StringBuilder();
        foreach (var ch in password)
        {
            var r = new Random().Next(fullChar.Length);

            randomNumbers.Add(r.ToString().PadLeft(3, 'x'));
            var randomChar = fullChar[r];

            var byteCode = (byte)ch + r;
            var value = byteCode.ToString().PadLeft(3, '0');
            sb.Append(value);
            sb.Append(randomChar);
        }

        return $"{sb}.{string.Join("", randomNumbers)}";
    }

    public string ByteBaseDecAdvanced(string encrypted)
    {
        var arr = encrypted.Split('.', StringSplitOptions.RemoveEmptyEntries);
        var randomNumbers = arr[^1];
        var numbers = randomNumbers.Chunk(3)
            .Select(x=>new string(x).Replace("x",""))
            .Select(int.Parse).ToList();
        var pass = arr.First();
        
        var byteArray = pass.Chunk(4)
            .Select(x => new string(x))
            .Select(x=>x.Substring(0, x.Length - 1))
            .Select(int.Parse).ToList();

        var sb = new StringBuilder();
        for (int i = 0; i < byteArray.Count; i++)
        {
            sb.Append((char)(byteArray[i] - numbers[i]));
        }
        return sb.ToString();
    }

    public string RandomByteBaseDec(string password, string key)
    {
        var sb = new StringBuilder();
        var seed = key.ToCharArray().Select(x => (byte)x).Sum(x => x);
        var r = new Random(seed);
        foreach (var ch in password)
        {
            var newR = r.Next(fullChar.Length);
            var charValue = fullChar[newR];
            
            var newCh = (char)(byte)ch + newR;
            sb.Append(charValue);
            sb.Append(Math.Abs((byte)ch - newR - key.Length));
            sb.Append(newCh);
        }

        return sb.ToString();
    }

    public string MD5_Enc(string password)
    {
        using var md5 = MD5.Create();
        var byteArr = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(byteArr).Replace("-","");
    }
    
    public string SHA256_Enc(string password)
    {
        using var sha256 = SHA256.Create();
        var byteArr = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(byteArr).Replace("-","");
    }
}