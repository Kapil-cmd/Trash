
namespace Core
{
    public interface IItemRepository
    {
        public SpResponse<string> AddItem(AddItemViewModel model);
    }
}
