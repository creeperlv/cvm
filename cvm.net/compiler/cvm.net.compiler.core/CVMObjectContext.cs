using System.Text.Json.Serialization;

namespace cvm.net.compiler.core
{
	[JsonSourceGenerationOptions(PreferredObjectCreationHandling = JsonObjectCreationHandling.Populate)]
	[JsonSerializable(typeof(CVMObject))]
	public partial class CVMObjectContext : JsonSerializerContext
	{

	}
}
