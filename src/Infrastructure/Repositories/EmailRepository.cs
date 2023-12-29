using Common.CustomErrorResponses;
using Common.Enums;
using Domain.DAL.Interface;
using Domain.Models;
using Infrastructure.Repositories.Interface;
using System.Text;

namespace Infrastructure.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IEmailTemplateDAL _emailDAL;

        public EmailRepository(IEmailTemplateDAL emailDAL)
        {
            _emailDAL = emailDAL;          
        }

        public async Task<ValidationModel> User_Successfully_Registered(User userDto)
        {
            try
            {
                var validation = new ValidationModel();

                var inputparam = new Dictionary<string, object?>();
                inputparam.Add("EmailTypeId", (int)EmailTypeEnum.USER_SUCCESSFULLY_REGISTERED_EMAILER);

                var result = await _emailDAL.Get(inputparam);
                if (result == null)
                {
                    validation.Errors = new List<string> { "Internal server error" };
                    return validation;
                }

                var body = new StringBuilder(result.Body);
                body = body.Replace("[DATA_1]", userDto.FirstName)
                    .Replace("[DATA_2]", userDto.LastName);
                  


                //var sendgridDtoPost = new SendgridDtoPost
                //{
                //    FromEmailId = result.FromAddress,
                //    FromName = result.FromName,
                //    ToEmailIdList = userDto.EmailAddress,
                //    htmlContent = body,
                //    Subject = result.Subject,
                //    ToFirstName = result.FromName,
                //    ToLastname = ""
                //};

                //var mailResult = await _sendGrid.SendMailMulti(sendgridDtoPost);
                //if (!mailResult.Status)
                {
                    // Something went wrong while sending mail to user
                }

                return validation;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
