using Application.Common.interfaces;
using Application.Requests;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace StudenServiceApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Post([FromBody] StudentRequestModel studentRequestModel)
    {
        var respons = await _studentService.Create(studentRequestModel, CancellationToken.None);
        return Ok(respons);
    }
}