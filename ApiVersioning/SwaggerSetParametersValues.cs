using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;

namespace ApiVersioning
{
    public class SwaggerSetParametersValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //apiDescription è una classe di ApiExplorer
            var apiDescription = context.ApiDescription;

            operation.Deprecated |= apiDescription.IsDeprecated();

            foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
            {
                var responseKey = responseType.IsDefaultResponse
                                  ? "default"
                                  : responseType.StatusCode.ToString();
                var response = operation.Responses[responseKey];

                foreach (var contentType in response.Content.Keys)
                {
                    if (!responseType.ApiResponseFormats.Any(x => x.MediaType == contentType))
                    {
                        response.Content.Remove(contentType);
                    }
                }
            }

            if (operation.Parameters == null)
            {
                return;
            }

            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions
                                                .First(p => p.Name == parameter.Name);

                parameter.Description ??= description.ModelMetadata?.Description;

                //Se description.DefaultValue, che è stata generata da ApiExplorer contiene un valore di default
                //e parameter.Schema.Default generato da swagger non ha valore di default, viene usato il valore di default di api explorer.
                //Questo permette di mettere in api-version, di querystring o header, il valore della versione V2.0 per esempio
                if (parameter.Schema.Default == null && description.DefaultValue != null)
                {
                    var json = JsonSerializer.Serialize(
                        description.DefaultValue,
                        description.ModelMetadata.ModelType);
                    parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson(json);
                }

                //Dal momento che usiamo due reader delle versioni, ossia HeaderApiVersionReader e QueryStringApiVersionReader
                //Allora i parametri in header e querystring non sono obbligatori, ma uno solo dei due è obbligatorio.
                //Swagger li imposta di default obbligatori entrambe, per cui inseriamo questa porzione di codice.
                if (parameter.Name == "api-version")
                {
                    parameter.Required = false;
                }
                else
                {
                    parameter.Required |= description.IsRequired;
                }
            }
        }
    }
}
