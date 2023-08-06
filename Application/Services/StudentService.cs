using Application.Common.interfaces;
using Application.Common.interfaces.Enum;
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

    public async Task<StudentResponseModel> Get(string id, CancellationToken cancellationToken)
    {
        var respons = await _studentReposiroty.FindAsync(id, new Student(), cancellationToken);

        return new StudentResponseModel()
        {
            Id = respons.Id,
            GuidId = respons.GuidId,
            FirstName = respons.FirstName,
            LastName = respons.LastName,
            Address = respons.Address,
            Course = respons.Course,
            Email = respons.Email,
            Group = respons.Group,
            Birthday = respons.Birthday,
        };
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

        if (result == "successful")
        {
            _studentConsumer.SendToRabbitMq(student, EntityChangeEventType.Insert);
        }
        else
        {
            throw new Exception();
        }

        return result;
    }

    public async Task<string> Update(StudentRequestModel request, string id, CancellationToken cancellationToken)
    {
        var respons = await _studentReposiroty.FindAsync(id, new Student(), cancellationToken);

        respons.FirstName = request.FirstName;
        respons.LastName = request.LastName;
        respons.Address = request.Address;
        respons.Course = request.Course;
        respons.Email = request.Email;
        respons.Group = request.Group;
        respons.Birthday = request.Birthday;

        var result = await _studentReposiroty.Update(respons, id, cancellationToken);

        if (result == "successful")
            _studentConsumer.SendToRabbitMq(respons, EntityChangeEventType.Update);
        else
            throw new Exception();

        return result;
    }

    public async Task<string> Delete(string id, CancellationToken cancellationToken)
    {
        var result = _studentReposiroty.Delete(id);

        if (result == "successful")
            _studentConsumer.SendToRabbitMq(new Student { Id = id }, EntityChangeEventType.Delete);
        else
            throw new Exception();

        return result;
    }
}