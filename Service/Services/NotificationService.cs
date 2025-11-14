using Core;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Application
{
    public class NotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task<BaseResponseModel<string>> AddNotification(AddNotificationViewModel model)
        {
            var response  = new BaseResponseModel<string>();
            try
            {
                if(model.UserId == 0)
                {
                    response.Status = "1";
                    response.Message = "UNABLE TO ADD NOTIFICATION!!!";
                    return response;
                }
                if (string.IsNullOrWhiteSpace(model.Title))
                {
                    response.Status = "1";
                    response.Message = "TITLE IS REQUIRED!!!";
                    return response;
                }
                if (string.IsNullOrWhiteSpace(model.Message))
                {
                    response.Status = "1";
                    response.Message = "MESSAGE IS REQUIRED!!!";
                    return response;
                }
                model.Status = "Unseen";

                //await _hubContext.Clients.All.SendAsync("NotificationStatus", new
                //{
                //    model.Title,
                //    model.UserId,
                //    model.Message,
                //});
                await _hubContext.Clients.User(model.UserId.ToString()).SendAsync("NotificationStatus", new
                {
                    model.Title,
                    model.UserId,
                    model.Message,
                });
                return response;
            }
            catch(Exception ex)
            {
                response.Status = "1";
                response.Message = "UNABLE TO PROCESS REQUEST!!!";
                return response;
            }
        }
    }
}
