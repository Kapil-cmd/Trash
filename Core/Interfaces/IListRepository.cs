using System.Web.Mvc;

namespace Core
{
    public interface IListRepository
    {
        public SpResponse<List<SelectListItem>> GetItemTypeList();
    }
}
