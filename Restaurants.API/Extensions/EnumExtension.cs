using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Restaurants.API.Extensions
{
    public static class EnumExtension
    {
        public static string GetEnumDescription<TEnum>(this TEnum item)
            => item
                .GetType()
                .GetField(item.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault()?.Description ?? string.Empty;

        public static void SeedEnumValues<TEntity, TEnum>(
            this DbSet<TEntity> dbSet,
            Func<TEnum, TEntity> converter) where TEntity : class
            => Enum.GetValues(typeof(TEnum))
                .Cast<object>()
                .Select(value => converter((TEnum)value))
                .ToList()
                .ForEach(instance => dbSet.AddOrUpdate(instance));
    }
}
