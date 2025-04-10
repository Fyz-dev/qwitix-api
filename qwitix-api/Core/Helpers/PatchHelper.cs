using System.Reflection;

namespace qwitix_api.Core.Helpers
{
    public static class PatchHelper
    {
        public static void ApplyPatch<TSource, TTarget>(
            TSource source,
            TTarget target,
            params string[] excludedProperties
        )
        {
            var sourceProps = typeof(TSource).GetProperties(
                BindingFlags.Public | BindingFlags.Instance
            );
            var targetProps = typeof(TTarget).GetProperties(
                BindingFlags.Public | BindingFlags.Instance
            );

            foreach (var sourceProp in sourceProps)
            {
                if (excludedProperties.Contains(sourceProp.Name))
                    continue;

                var value = sourceProp.GetValue(source);
                if (value == null)
                    continue;

                var targetProp = targetProps.FirstOrDefault(p =>
                    p.Name == sourceProp.Name
                    && (
                        p.PropertyType == sourceProp.PropertyType
                        || IsNullableToNonNullableMatch(sourceProp.PropertyType, p.PropertyType)
                    )
                );

                if (targetProp != null && targetProp.CanWrite)
                {
                    try
                    {
                        object convertedValue = value;

                        if (
                            IsNullableToNonNullableMatch(
                                sourceProp.PropertyType,
                                targetProp.PropertyType
                            )
                        )
                        {
                            convertedValue = Convert.ChangeType(value, targetProp.PropertyType);
                        }

                        targetProp.SetValue(target, convertedValue);
                    }
                    catch (TargetInvocationException ex)
                        when (ex.InnerException is ArgumentException argEx)
                    {
                        throw new ArgumentException(argEx.Message, argEx);
                    }
                }
            }
        }

        private static bool IsNullableToNonNullableMatch(Type sourceType, Type targetType)
        {
            return Nullable.GetUnderlyingType(sourceType) == targetType;
        }
    }
}
