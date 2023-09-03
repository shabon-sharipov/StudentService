using Application.Consumer;
using Application.Repositories;
using Application.Requests;
using Application.Responses;
using AutoMapper;

namespace Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentPublisher _publisher;
    private IMapper _mapper;
    private IRepository<Student> _studentRepository;

    public StudentService(IRepository<Student> repository, IStudentPublisher publisher, IMapper mapper)
    {
        _studentRepository = repository;
        _publisher = publisher;
        _mapper = mapper;
    }

    public async Task<StudentResponseModel> Get(string id, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _studentRepository.FindAsync(id, nameof(Student), cancellationToken);

            return _mapper.Map<StudentResponseModel>(response);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<string> Create(StudentRequestModel request, CancellationToken cancellationToken)
    {
        try
        {
            var student = _mapper.Map<Student>(request);

            var result = await _studentRepository.AddAsync(student, cancellationToken);

            if (result == "successful")
                _publisher.SendInsert(student.Id);

            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<string> Update(StudentRequestModel request, string id, CancellationToken cancellationToken)
    {
        var response = await _studentRepository.FindAsync(id, nameof(Student), cancellationToken);

        response.FirstName = request.FirstName;
        response.LastName = request.LastName;
        response.Address = request.Address;
        response.Course = request.Course;
        response.Email = request.Email;
        response.Group = request.Group;
        response.Birthday = request.Birthday;

        try
        {
            var result = await _studentRepository.Update(response, id, cancellationToken);

            if (result == "successful")
                _publisher.SendUpdated(response.Id);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<string> Delete(string id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _studentRepository.Delete(id, nameof(Student));

            if (result == "successful")
                _publisher.SendDeleted(id);
            else
                throw new Exception();

            return result;
        }
        catch (Exception e)
        {
            throw;
        }
    }
}