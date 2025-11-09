using System.Web.Mvc;

namespace Core
{
    public interface IListRepository
    {
        public SpResponse<List<SelectListItem>> GetItemTypeList();
        public SpResponse<List<SelectListItem>> GetCountryList();
        public SpResponse<List<SelectListItem>> GetCountyList(long? countryId);
    }
}
