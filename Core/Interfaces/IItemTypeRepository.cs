
namespace Core
{
    public interface IItemTypeRepository
    {
        public SpResponse<string> AddItemType(AddItemTypeViewModel model);
    }
}
