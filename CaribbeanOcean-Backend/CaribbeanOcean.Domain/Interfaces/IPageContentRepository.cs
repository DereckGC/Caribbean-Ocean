using CaribbeanOcean.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Domain.Interfaces
{
    public interface IPageContentRepository
    {
        Task<IEnumerable<PageContent>> GetAllPageContents();
        Task<PageContent> GetPageContentById(int id);
        Task<PageContent> GetPageContentByTitle(string title);
        Task<int> CreatePageContent(PageContent pageContent);
        Task<bool> UpdatePageContent(PageContent pageContent);
        Task<bool> DeletePageContent(int id);
        Task<bool> UpdateGalleryContent(List<MultimediaContent> images, int adminId);
    }
}
