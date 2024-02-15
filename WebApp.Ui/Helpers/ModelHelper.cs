using System.Globalization;
using System.Net.Mail;
using System.Text;
using Murmur;

namespace WebApp.Ui.Helpers;

public static class ModelHelper
{
    private static readonly Random Random = new();

    public static string RandomAlphabet(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    public static string CreateGuid(string initial = "")
    {
        if (string.IsNullOrWhiteSpace(initial)) return EnsureLength(32, HashKey());

        return initial + EnsureLength(29, HashKey());
    }

    public static string RandomLength(int length = 5)
    {
        return EnsureLength(length, HashKey());
    }

    private static string EnsureLength(int length, string hashKey)
    {
        if (hashKey.Length > length)
            return hashKey.Substring(0, length);

        while (hashKey.Length < length)
            hashKey = hashKey + ShortHashKey();

        return hashKey.Substring(0, length);
    }

    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static string AddSpacesToSentence(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return "";

        var textLength = text.Length;
        var newText = new StringBuilder(textLength * 2);
        newText.Append(text[0]);
        for (var i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                newText.Append(' ');
            newText.Append(text[i]);
        }

        return newText.ToString();
    }

    public static bool IsValidEmail(string email)
    {
        if (email.Trim().EndsWith(".")) return false; // suggested by @TK-421
        try
        {
            var mail = new MailAddress(email);
            return mail.Address == email;
        }
        catch
        {
            return false;
        }
    }

    //Split List
    //example: var query = hotelByDestination.SplitIntoSets<string>(200).ToList();
    public static IEnumerable<IEnumerable<T>> SplitIntoSets<T>(this IEnumerable<T> source, int itemsPerSet = 200)
    {
        var sourceList = source as List<T> ?? source.ToList();
        for (var index = 0; index < sourceList.Count; index += itemsPerSet)
            yield return sourceList.Skip(index).Take(itemsPerSet);
    }

    //PagingExtensions
    //example: var query = placeByDestination.AsEnumerable().PageExtension(pageIndex, 200);
    public static IEnumerable<TSource> PageExtension<TSource>(this IEnumerable<TSource> source, int page, int pageSize)
    {
        return source.Skip((page - 1) * pageSize).Take(pageSize);
    }

    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }
    public static double ToDouble(this string text)
    {
        try
        {
            var result = double.Parse(text, CultureInfo.InvariantCulture);
            return result;
        }
        catch
        {
            return 0;
        }
    }


    //murmur

    #region MurMurHas

    public static string ShortHashKey()
    {
        return MurmurStr32();
    }

    public static string HashKey()
    {
        return MurmurStr32() + MurmurStr32() + MurmurStr32();
    }

    private static string MurmurStr32()
    {
        var murmur = MurmurHash.Create32();
        var bytes = Encoding.UTF8.GetBytes(DateTimeOffset.UtcNow + ShortKey());
        var hash = murmur.ComputeHash(bytes);
        var numericHash = BitConverter.ToUInt32(hash, 0);
        return numericHash.ToString("X");
    }

    public static string ShortKey()
    {
        var base64Guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        base64Guid = base64Guid.Replace("/", "q");
        base64Guid = base64Guid.Replace("+", "Q");
        base64Guid = base64Guid.Replace("==", "");

        return base64Guid;
    }

    #endregion
}