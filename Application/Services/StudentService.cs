using Application.Common.interfaces;
using Application.Common.interfaces.Repositoties;
using Application.Consumer;
using Application.Requests;
using Application.Responses;
using Domain.Models;

namespace Application.Services;

public class StudentService : IStudentService
{
    private IRepository<Student> _studentReposiroty;
    private StudentConsumer _studentConsumer;

    public StudentService(IRepository<Student> studentReposiroty)
    {
        _studentReposiroty = studentReposiroty;
        _studentConsumer = new StudentConsumer();
    }

    public Task<StudentResponseModel> Get(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<StudentResponseModel>> GetAll(int pageSize, int pageNumber,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<string> Create(StudentRequestModel request, CancellationToken cancellationToken)
    {
        var student = new Student()
        {
            GuidId = Guid.NewGuid().ToString(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Address = request.Address,
            Course = request.Course,
            Email = request.Email,
            Group = request.Group,
            Birthday = request.Birthday,
            
        };
        var result = await _studentReposiroty.AddAsync(student, cancellationToken);

        if (result == "successfull")
        {
            _studentConsumer.SendToRabbitMq(student);
        }
        else
        {
            throw new Exception();
        }

        return result;
    }

    public Task<string> Update(StudentRequestModel request, string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public bool Delete(ulong id, CancellationToken cancellationTok)
    {
        throw new NotImplementedException();
    }
}