using Common.CustomErrorResponses;
using Domain.Models;

namespace Infrastructure.Repositories.Interface
{
    public interface IEmailRepository
    {
        Task<ValidationModel> User_Successfully_Registered(User userDto);
    }
}
