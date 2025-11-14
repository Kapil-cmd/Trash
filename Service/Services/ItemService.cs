
using Core;
using Microsoft.AspNetCore.Http;

namespace Application
{
    public class ItemService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IItemRepository _itemRepository;
        public ItemService(IHttpContextAccessor httpContextAccessor, IItemRepository itemRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _itemRepository = itemRepository;
        }
        public BaseResponseModel<List<ImageDetailViewModel>> ItemList()
        {
            BaseResponseModel<List<ImageDetailViewModel>> response = new BaseResponseModel<List<ImageDetailViewModel>>();
            try
            {
                var list = _itemRepository.GetItemList();
                if(list.ErrorCode == "000")
                {
                    response.Status = list.ErrorCode;
                    response.Data = list.Data;
                    response.Message = list.Message;
                    return response;
                }
                else
                {
                    response.Status = "1";
                    response.Message = "TECHNICAL ERROR OCCURRED WHILE PROCESSING YOUR REQUEST!!!";
                    return response;
                }
            }catch(Exception ex)
            {
                response.Status = "1";
                response.Message = "TECHNICAL ERROR OCCURRED WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }
        public async Task<BaseResponseModel<string>> AddItem(AddItemViewModel model)
        {
            BaseResponseModel<string> response = new BaseResponseModel<string>();
            try
            {
                if(model.UserId == 0)
                {
                    response.Status = "1";
                    response.Message = "UNABLE TO ADD ITEMS!!!";
                    return response;
                }
                if (model.ItemTypeId == 0)
                {
                    response.Status = "1";
                    response.Message = "PLEASE CHOOSE THE ITEM TYPES!!!";
                    return response;
                }
                if (string.IsNullOrWhiteSpace(model.ItemName))
                {
                    response.Status = "1";
                    response.Message = "PLEASE PROVIDE THE ITEM NAME!!!";
                    return response;
                }
                if (string.IsNullOrWhiteSpace(model.Description))
                {
                    response.Status = "1";
                    response.Message = "PLEASE PROVIDE THE ITEM DESCRIPTION!!!";
                    return response;
                }
                List<ItemImageViewModel> imageList = new List<ItemImageViewModel>();
                if (model.Images.Count() == 0 || model.Images == null)
                {
                    response.Status = "1";
                    response.Message = "PLEASE UPLOAD THE IMAGE!!!";
                    return response;
                }
                else
                {
                    var path = DefaultConfiguration.StaticConfiguration.GetSection("Images:ItemImages").Value;
                    ItemImageViewModel imageModels = new ItemImageViewModel();
                    var file = model.Images;

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    for (int i = 0; i < file.Count; i++)
                    {
                        var name = $"{Guid.NewGuid()}_{Path.GetFileName(file[i].FileName)}";
                        var imgPath = Path.Combine(path, name);

                        using (var stream = new FileStream(imgPath, FileMode.Create))
                        {
                            await file[i].CopyToAsync(stream);
                        }
                        var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
                        var imageUrl = $"{baseUrl}/Images/ItemImages/{name}";

                        imageModels.ImageSource = imageUrl;
                        imageModels.ImageName = name;

                        imageList.Add(imageModels);
                    }
                }
                model.Image = imageList;
                model.Status = "Active";
                model.CreatedDateTime = DateTime.Now;
                var dataResponse = _itemRepository.AddItem(model);

                response.Status = dataResponse.ErrorCode;
                response.Message = dataResponse.Message;
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
