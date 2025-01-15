using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace UnclewoodCleanArchitectur.Presentation.Controllers;



    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseApiController : ControllerBase
    {

    }
