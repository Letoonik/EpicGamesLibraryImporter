using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EGSIT.Core;

public static class JsonExtensions
{
	public static JObject ToJObject<T2>(this IEnumerable<KeyValuePair<string,T2>> kvps)
	{
		var obj = new JObject();
		foreach (var kvp in kvps)
			obj.Add(kvp.Key, JToken.FromObject( kvp.Value));
		return obj;
	}
}

