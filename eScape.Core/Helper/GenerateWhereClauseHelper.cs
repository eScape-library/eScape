using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eScape.Core.Helper
{
    public static class GenerateWhereClauseHelper
    {

        public static string? Generate(params Criteria[] criterias)
        {
            StringBuilder whereClause = new StringBuilder();
            for (int i = 0; i < criterias.Length; i++)
            {
                var item = criterias[i];
                if (item.Key != null && item.Value != null)
                {
                    if (i > 0 && whereClause.Length > 0)
                    {
                        whereClause.Append(" AND ");
                    }

                    whereClause.Append(item.Key);

                    if (item.Key == "Price")
                    {
                        whereClause.Append(item.Value);
                    }
                    else
                    {
                        whereClause.Append(" IN ");
                        whereClause.Append('(');
                        whereClause.Append(item.Value);
                        whereClause.Append(')');
                    }
                    
                }
            }
            return whereClause.ToString() == "" ? null : whereClause.ToString();
        }

        public static string? GetPriceClause(string pattern = "")
        {
            string[] result = pattern.Split('-');
            switch (result[0])
            {
                case "lt":
                    return " < " + result[1];
                case "gt":
                    return " > " + result[1];
                case "bt":
                    string[] numbers = result[1].Split('_');
                    return " BETWEEN " + numbers[0] + " AND " + numbers[1];
                default:
                    return null;
            }
        }
    }

    //Các tiêu chí để so sánh (Color, Size, Price)
    public class Criteria
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
    }
}
