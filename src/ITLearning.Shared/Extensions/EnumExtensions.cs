using ITLearning.Contract.Data.Model;
using ITLearning.Contract.Enums;
using System;
using System.Collections.Generic;

namespace ITLearning.Shared.Extensions
{
    public static class EnumExtensions
    {
        public static EnumDisplayData GenerateDisplayData(this Enum value)
        {
            return new EnumDisplayData() { Id = Convert.ToInt32(value), Name = value.ToString() };
        }

        public static List<EnumDisplayData> GenerateDisplayDataList(this Enum value)
        {
            var enumValues = Enum.GetValues(value.GetType());

            var list = new List<EnumDisplayData>();

            for (int i = 0; i < enumValues.Length; i++)
            {
                int intValue = (int)enumValues.GetValue(i);

                list.Add(new EnumDisplayData() { Id = intValue, Name = ((LanguageEnum)intValue).ToString() });
            }

            return list;
        }
    }
}
