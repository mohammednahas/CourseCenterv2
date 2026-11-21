
using System.ServiceModel;

namespace CourseCenterv2.Soap;

[ServiceContract(Namespace = "http://coursecenterv2.com/soap")]
public interface ICourseCenterSoapService
{
    [OperationContract]
    Task<List<StudentSoapDto>> GetAllStudentsAsync();

    [OperationContract]
    Task<List<CourseSoapDto>> GetAllCoursesAsync();
}
