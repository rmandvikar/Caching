using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace rm.Caching
{
	/// <summary>
	/// Cache helper class.
	/// </summary>
	public static class Cacher
	{
		private static readonly CacheInternal cache;

		static Cacher()
		{
			cache = new CacheInternal(HttpRuntime.Cache, 60);
		}

		public static bool TryGet<T>(string key, out T t, Func<T> fnCacheMiss, object olock)
		{
			return cache.TryGet<T>(key, out t, fnCacheMiss, olock);
		}
		public static T Get<T>(string key)
		{
			return cache.Get<T>(key);
		}
		public static void Set<T>(string key, T t)
		{
			cache.Set<T>(key, t);
		}
		public static bool Has<T>(string key)
		{
			return cache.Has<T>(key);
		}
		public static T Remove<T>(string key)
		{
			return cache.Remove<T>(key);
		}
	}
}
