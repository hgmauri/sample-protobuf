using Microsoft.AspNetCore.Mvc;
using Sample.Protobuf.WebApi.Core.Contracts;
using Sample.Protobuf.WebApi.Core.Extensions;

namespace Sample.Protobuf.WebApi.Controllers;

[Route("api/[controller]")]
public class ProtobufController : Controller
{
    [HttpPost("serialize")]
    public IActionResult Serialize([FromBody] Person model)
    {
        var result = model.SerializeToStringProtobuf();

        Serilog.Log.Information($"Protobuf serialized: {result}");

        return Ok(result);
    }

    [HttpPost("deserialize")]
    public IActionResult Deserialize([FromBody] string protobuf)
    {
        var result = protobuf.DeserializeFromStringProtobuf<Person>();

        return Ok(result);
    }
}