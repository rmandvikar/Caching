using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rm.Caching;

namespace rm.CachingTestConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Cacher.Set("key", 1);
			var v1 = Cacher.Get<int>("key");
			//var v2 = Cacher.Get<string>("key"); // throws cast ex

			var v3 = Cacher.Get<int>("nokey");

			var b1 = Cacher.Has<int>("key");
			var b2 = Cacher.Has<string>("key"); // throws cast ex

			Cacher.Set<string>("key2", null);
			var v4 = Cacher.Get<string>("key2");
		}
	}
}
