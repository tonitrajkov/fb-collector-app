using FbCollector.Models;

namespace FbCollector.Intefraces
{
    public interface IPageService
    {
        void CreatePage(PageModel model);

        void UpdatePage(PageModel model);

        void DeletePage(int pageId);

        SearchResult<PageModel> GetPagesFiltered(PageSearchModel model);
    }
}
