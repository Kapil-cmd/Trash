
using Core;
using System.Web.Mvc;

namespace Application
{
    public class ListService
    {
        private readonly IListRepository _listRepository;
        public ListService(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }
       public BaseResponseModel<List<SelectListItem>> GetItemTypeList()
        {
            var response = new BaseResponseModel<List<SelectListItem>>();
            try
            {
                var list = _listRepository.GetItemTypeList();
                if (list.ErrorCode == "000")
                {
                    response.Status = list.ErrorCode;
                    response.Data = list.Data;
                    response.Message = list.Message;
                    return response;
                }
                else
                {
                    response.Status = "1";
                    response.Message = "UNABLE TO GET LIST!!!";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = "TECHNICAL ERROR OCCURRED WHILE PROCESSING REQUEST!!!";
                return response;
            }
        }
    }
}
