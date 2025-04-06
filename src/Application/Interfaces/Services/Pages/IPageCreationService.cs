using Domain.Entities.Pages;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Services.Pages;

public interface IPageCreationService
{
    Task CreatePage(Page page);
    

}