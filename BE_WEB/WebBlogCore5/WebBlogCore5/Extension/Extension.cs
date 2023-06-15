using System.Text.RegularExpressions;

namespace WebBlogCore5.Extension
{
    public static class Extension
    {
        //Extension chuyển từ dạng: 5000 -> 5.000đ
        public static string ToVnd(this double donGia)
        {
            return donGia.ToString("#,##0") + " đ";
        }

        //Tạo url thân thiện nè: chào mày -> chao-may
        public static string ToUrlFriendly(this string url)
        {
            var result = url.ToLower().Trim();
            result = Regex.Replace(result, "áàạảãâấầậẩẫăắằặẳẵ", "a");
            result = Regex.Replace(result, "éèẹẻẽêếềệểễ", "e");
            result = Regex.Replace(result, "óòọỏõôốồộổỗơớờợởỡ", "o");
            result = Regex.Replace(result, "úùụủũưứừựửữ", "u");
            result = Regex.Replace(result, "íìịỉĩ", "i");
            result = Regex.Replace(result, "ýỳỵỷỹ", "y");
            result = Regex.Replace(result, "đ", "d");
            result = Regex.Replace(result, "[^a-z0-9-]", "");
            result = Regex.Replace(result, "(-)+", "-");

            return result;
        }
    }
}
