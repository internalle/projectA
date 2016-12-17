using System.Resources;

namespace ProjectA.i18n
{
    public static class ResourceProvider
    {
        public static string ByString(string key)
        {
            var file = key.Split('.')[0];
            var resourceKey = key.Remove(0, file.Length + 1);
            var resourceManager = new ResourceManager($@"{typeof(ResourceProvider).Namespace}.{file}", typeof(ResourceProvider).Assembly);

            try
            {
                var result = resourceManager.GetString(resourceKey);
                return !string.IsNullOrEmpty(result) ? result : key;
            }
            catch
            {
                return key;
            }
        }
    }
}
