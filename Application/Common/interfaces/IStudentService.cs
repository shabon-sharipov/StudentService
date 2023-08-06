using Application.Requests;
using Application.Responses;

namespace Application.Common.interfaces;

public interface IStudentService
{
    Task<StudentResponseModel> Get(string id, CancellationToken cancellationToken);
    Task<IEnumerable<StudentResponseModel>> GetAll(int pageSize, int pageNumber, CancellationToken cancellationToken);
    Task<string> Create(StudentRequestModel request, CancellationToken cancellationToken);
    Task<string> Update(StudentRequestModel request, string id, CancellationToken cancellationToken);
    Task<string> Delete(string id, CancellationToken cancellationTok);
}