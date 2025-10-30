
using Core;
using Microsoft.IdentityModel.Tokens.Experimental;

namespace Application
{
    public class ItemTypeService
    {
        private readonly IItemTypeRepository _itemTypeRepository;
        public ItemTypeService(IItemTypeRepository itemTypeRepository)
        {
            _itemTypeRepository = itemTypeRepository;
        }
        public BaseResponseModel<string> AddItemType(AddItemTypeViewModel model)
        {
            BaseResponseModel<string> response = new BaseResponseModel<string>();
            try
            {
                if (string.IsNullOrWhiteSpace(model.ItemTypeName))
                {
                    response.Status = "1";
                    response.Message = "ITEM TYPE NAME IS REQUIRED.";
                    return response;
                }
                if(string.IsNullOrWhiteSpace(model.Description))
                {
                    response.Status = "1";
                    response.Message = "DESCRIPTION IS REQUIRED.";
                    return response;
                }
                if(string.IsNullOrWhiteSpace(model.CreatedBy))
                {
                    model.CreatedBy = "SYSTEM";
                }
                if(string.IsNullOrWhiteSpace(model.Status))
                {
                    model.Status = "Active";
                }
                model.CreatedDateTime = DateTime.Now;
                var spResponse = _itemTypeRepository.AddItemType(model);
                response.Status = spResponse.ErrorCode;
                response.Message = spResponse.Message;
                return response;
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