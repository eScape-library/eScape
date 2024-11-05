using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eScape.Core.Const
{
    public static class StoredProcedureName
    {
        public static readonly string UspGetCategory = "uspGetCategory";
        public static readonly string UspInsUpdCategory = "uspInsUpdCategory";
        public static readonly string UspGetSubCategory = "uspGetSubCategory";
        public static readonly string UspInsUpdSubCategory = "uspInsUpdSubCategory";
        public static readonly string UspGetProduct = "uspGetProduct";
        public static readonly string UspInsUpdProduct = "uspInsUpdProduct";
        public static readonly string UspGetProductAttribute = "uspGetProductAttribute";
        public static readonly string UspInsUpdProductAttribute = "uspInsUpdProductAttribute";
        public static readonly string UspGetProductDetails = "uspGetProductDetails";
        public static readonly string UspInsUpdProductDetails = "uspInsUpdProductDetails";
        public static readonly string UspGetCart = "uspGetCart";
        public static readonly string UspInsUpdCart = "uspInsUpdCart";
        public static readonly string UspGetPromotion = "uspGetPromotion";
        public static readonly string UspInsUpdPromotion = "uspInsUpdPromotion";
        public static readonly string UspGetProductsByCategory = "uspGetProductsByCategory";
        public static readonly string UspGetProductDetailsById = "uspGetProductDetailsById";
        public static readonly string UspGetVariantsBySize = "uspGetVariantsBySize";
    }

    public static class Actions
    {
        public static readonly string Insert = "Insert";
        public static readonly string Update = "Update";
        public static readonly string Delete = "Delete";
    }
}
