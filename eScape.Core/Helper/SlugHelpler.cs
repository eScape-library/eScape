using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace eScape.Core.Helper
{
    public static class SlugHelpler
    {
        public static string CreateSlug(string input)
        {
            // Chuyển sang chữ thường
            string slug = input.ToLowerInvariant();

            // Loại bỏ dấu tiếng Việt
            slug = RemoveDiacritics(slug);

            // Thay thế các ký tự không phải chữ cái hoặc số thành dấu gạch ngang
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");

            // Thay khoảng trắng và dấu gạch ngang liền kề thành một dấu gạch ngang
            slug = Regex.Replace(slug, @"\s+", "-").Trim();

            // Xóa các dấu gạch ngang ở đầu và cuối chuỗi
            slug = slug.Trim('-');

            return slug;
        }

        // Hàm loại bỏ dấu tiếng Việt
        private static string RemoveDiacritics(string text)
        {
            text = text.Replace("đ", "d");
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
