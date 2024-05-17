using System.Text.RegularExpressions;

namespace T_Shop.Application.Common.Helpers;
public static class ImageHelpers
{
    public static string GetPublicIDFromImageUrl(string imageUrl)
    {
        int startIndex = imageUrl.LastIndexOf('/') + 1;
        if (startIndex != -1 && startIndex < imageUrl.Length)
        {
            return imageUrl.Substring(startIndex);
        }
        return imageUrl;
    }

    public static string RemoveTimestampFromImageUrl(string imageUrl)
    {
        string pattern = @"/v\d+/";
        string replacedUrl = Regex.Replace(imageUrl, pattern, "/");
        return replacedUrl;
    }
}
