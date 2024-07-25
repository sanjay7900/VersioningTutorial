using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VersioningTutorial.ApiVersioningModel
{
    public class RemoveVersionFromParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters.Count > 0)
            {


                var version = operation.Parameters.Single(n => n.Name == "version");
                if (version != null)
                {
                    operation.Parameters.Remove(version);
                }
            }

        }
    }
}
