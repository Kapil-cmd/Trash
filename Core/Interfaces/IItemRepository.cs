
namespace Core
{
    public interface IItemRepository
    {
        public SpResponse<string> AddItem(AddItemViewModel model);
        public SpResponse<List<ImageDetailViewModel>> GetItemList();
    }
}
