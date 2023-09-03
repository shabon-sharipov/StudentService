using Application.Requests;
using Application.Responses;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace StudentServiceApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly ILogger<StudentController> _logger;

    public StudentController(IStudentService studentService, ILogger<StudentController> logger)
    {
        _studentService = studentService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Post([FromBody] StudentRequestModel studentRequestModel)
    {
        try
        {
            var respons = await _studentService.Create(studentRequestModel, CancellationToken.None);
            return Ok(respons);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw ex;
        }
    }

    [HttpGet]
    public async Task<ActionResult<StudentResponseModel>> GetById(string id)
    {
        return new StudentResponseModel();
        //var respose = await _studentService.Get(id, CancellationToken.None);
        //return Ok(respose);
    }

    [HttpPut]
    public async Task<ActionResult<string>> Put(string id, StudentRequestModel requestModel,
        CancellationToken cancellationToken)
    {
        var respons = await _studentService.Update(requestModel, id, cancellationToken);
        return Ok(respons);
    }

    [HttpDelete]
    public async Task<ActionResult<string>> Delete(string id, CancellationToken cancellationToken)
    {
        var result = await _studentService.Delete(id, cancellationToken);
        return Ok(result);
    }
}