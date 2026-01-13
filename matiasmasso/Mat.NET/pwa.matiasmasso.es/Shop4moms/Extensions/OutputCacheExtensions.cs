using Microsoft.AspNetCore.OutputCaching;

namespace Shop4moms.Extensions
{
    /// <summary>
    /// Loads Cache policies (like Duration and Tag for cache clearing) on Program.cs
    /// </summary>
    public static class OutputCacheExtensions
    {
        public enum Tags
        {
            MediaResources
        }

        /// <summary>
        /// Adds a set of enumerated policies to the Cache store, in order to allow for selective eviction 
        /// </summary>
        /// <param name="options"></param>
        public static void AddCustomPolicies(this OutputCacheOptions options)
        {
            foreach (var tag in Enum.GetValues(typeof(Tags)).Cast<Tags>())
            {
                options.AddPolicy(tag);
            }
        }

        /// <summary>
        /// Adds a policy with a Tag name and Duration
        /// </summary>
        /// <param name="options"></param>
        /// <param name="cod"></param>
        public static void AddPolicy(this OutputCacheOptions options, Tags tag)
        {
            options.AddPolicy(tag.ToString(), policyBuilder => policyBuilder
            .Expire(TimeSpan.FromDays(365))
            .Tag(tag.ToString())
            );
        }

        public static async Task<bool> Clear(this IOutputCacheStore store)
        {
            foreach (var tag in Enum.GetValues(typeof(Tags)).Cast<Tags>())
            {
                await store.Clear(tag);
            }
            return true;
        }
        public static async Task Clear(this IOutputCacheStore cacheStore, Tags tag)
        {
            await cacheStore.EvictByTagAsync(tag.ToString(), default);
        }

    }
}
