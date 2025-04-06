using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Pages;

namespace Domain.Repositories
{
   public interface IPageRepository
    {
        List<Page> GetAll();
        Page FindById(Guid id);
        Page GetPage(string id);
        Task CreatePage(Page page);
        Task UpdatePage(Page page);
        Task DeletePageWithId(Guid id);
    }
}
