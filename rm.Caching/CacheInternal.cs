using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace rm.Caching
{
	/// <summary>
	/// Internal wrapper class around a built-in cache implementation.
	/// </summary>
	internal class CacheInternal
	{
		#region members

		private readonly Cache cache;
		private readonly int expiration;

		#endregion

		#region ctors

		public CacheInternal(Cache cache, int expiration = 60)
		{
			this.cache = cache;
			this.expiration = expiration;
		}

		#endregion

		#region methods

		public bool TryGet<T>(string key, out T t, Func<T> fnCacheMiss, object olock)
		{
			var result = true;
			var o = cache[key];
			if (o == null)
			{
				lock (olock)
				{
					o = cache[key];
					if (o == null)
					{
						try
						{
							o = fnCacheMiss();
						}
						catch (Exception)
						{
							result = false;
							// todo: log ex or throw
						}
						// note: result is true even though fnCacheMiss() returns null
						if (o != null)
						{
							cache[key] = o;
						}
					}
				}
			}
			t = (T)(o ?? default(T));
			return result;
		}

		public T Get<T>(string key, Func<T> fnCacheMiss, object olock)
		{
			T t;
			TryGet(key, out t, fnCacheMiss, olock);
			return t;
		}

		public T Get<T>(string key)
		{
			return (T)(cache[key] ?? default(T));
		}

		public void Set<T>(string key, T t)
		{
			if (t != null)
			{
				cache.Insert(key, t, null, DateTime.UtcNow.AddSeconds(expiration), Cache.NoSlidingExpiration);
			}
		}

		public bool Has<T>(string key)
		{
			return (T)cache[key] != null;
		}

		public T Remove<T>(string key)
		{
			return (T)(cache.Remove(key) ?? default(T));
		}

		#endregion
	}
}
