namespace Application.Consumer;

public interface IStudentPublisher
{
    void SendInsert(string id);
    void SendUpdated(string id);
    void SendDeleted(string id);
}
