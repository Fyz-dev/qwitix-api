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
                    p.Name == sourceProp.Name && p.PropertyType == sourceProp.PropertyType
                );

                if (targetProp != null && targetProp.CanWrite)
                {
                    try
                    {
                        targetProp.SetValue(target, value);
                    }
                    catch (TargetInvocationException ex)
                        when (ex.InnerException is ArgumentException argEx)
                    {
                        throw new ArgumentException(argEx.Message, argEx);
                    }
                }
            }
        }
    }
}
