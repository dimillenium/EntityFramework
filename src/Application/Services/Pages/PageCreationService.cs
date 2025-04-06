using Application.Interfaces.FileStorage;
using Application.Interfaces.Services.Pages;
using Domain.Entities.Pages;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Pages;

public class PageCreationService : IPageCreationService
{
    private readonly IPageRepository _pageRepository;
    private readonly IFileStorageApiConsumer _fileStorageApiConsumer;

    public PageCreationService(
        IPageRepository pageRepository,
        IFileStorageApiConsumer fileStorageApiConsumer)
    {
        _pageRepository = pageRepository;
        _fileStorageApiConsumer = fileStorageApiConsumer;
    }

    public async Task CreatePage(Page page)
    {
        
        await _pageRepository.CreatePage(page);
    }
}