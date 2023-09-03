using Application.Requests;
using Application.Responses;

namespace Application.Services;

public interface IStudentService
{
    Task<StudentResponseModel> Get(string id, CancellationToken cancellationToken);
    Task<string> Create(StudentRequestModel request, CancellationToken cancellationToken);
    Task<string> Update(StudentRequestModel request, string id, CancellationToken cancellationToken);
    Task<string> Delete(string id, CancellationToken cancellationTok);
}