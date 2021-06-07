using System.Collections.Generic;

namespace Calabonga.Facts.Contracts
{
    /// <summary>
    /// // Calabonga: update summary (2021-05-29 12:32 ITagService)
    /// </summary>
    public interface ISearchService
    {
        List<string> SearchTags(string term);

        List<FactViewModel> SearchContent(string term);
    }
}
