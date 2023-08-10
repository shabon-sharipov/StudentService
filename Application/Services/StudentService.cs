using Application.Common.interfaces;
using Application.Common.interfaces.Enum;
using Application.Common.interfaces.Repositoties;
using Application.Consumer;
using Application.Requests;
using Application.Responses;
using AutoMapper;
using Domain.Models;

namespace Application.Services;

public class StudentService : IStudentService
{
    private IMapper _mapper;
    private IRepository<Student> _studentReposiroty;
    private StudentConsumer _studentConsumer;

    public StudentService(IRepository<Student> studentReposiroty, IMapper mapper)
    {
        _studentReposiroty = studentReposiroty;
        _mapper = mapper;
        _studentConsumer = new StudentConsumer();
    }

    public async Task<StudentResponseModel> Get(string id, CancellationToken cancellationToken)
    {
       var respons = await _studentReposiroty.FindAsync(id, nameof(Student), cancellationToken);

        return _mapper.Map<StudentResponseModel>(respons);
    }

    public async Task<string> Create(StudentRequestModel request, CancellationToken cancellationToken)
    {
        var student = _mapper.Map<Student>(request);

        var result = await _studentReposiroty.AddAsync(student, cancellationToken);

        if (result == "successful")
            _studentConsumer.SendToRabbitMq(student, EntityChangeEventType.Insert);
        else
            throw new Exception();

        return result;
    }

    public async Task<string> Update(StudentRequestModel request, string id, CancellationToken cancellationToken)
    {
        var respons = await _studentReposiroty.FindAsync(id, nameof(Student), cancellationToken);

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
        var result =await _studentReposiroty.Delete(id,nameof(Student));

        if (result == "successful")
            _studentConsumer.SendToRabbitMq(new Student { Id = id }, EntityChangeEventType.Delete);
        else
            throw new Exception();

        return result;
    }
}