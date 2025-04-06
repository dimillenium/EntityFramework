using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions.Books;
using Domain.Entities.Pages;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Slugify;

namespace Infrastructure.Repositories.Pages
{
    public class PageRepository : IPageRepository
    {
        private readonly ISlugHelper _slugHelper;
        private readonly EmNoJoyauxDbContext _context;

        public PageRepository(EmNoJoyauxDbContext context, ISlugHelper slugHelper)
        {
            _context = context;
            _slugHelper = slugHelper;
        }

        public List<Page> GetAll()
        {
            return _context.Pages.AsNoTracking().ToList();
        }

        public Page FindById(Guid id)
        {
            var page = _context.Pages
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
            if (page == null)
                throw new BookNotFoundException($"Could not find Page with id {id}.");
            return page;
        }
        public Page GetPage(string pSlug)
        {
            var page = _context.Pages
                .AsNoTracking()
                .FirstOrDefault(x => x.Slug == pSlug);
            if (page == null)
                throw new BookNotFoundException($"Could not find Page with id {pSlug}.");
            return page;
        }

        public async Task CreatePage(Page page)
        {
            var existingPage = await _context.Pages
                .FirstOrDefaultAsync(x => x.Slug.Trim() == page.Slug.Trim());

            if (existingPage != null)
            {
                throw new InvalidOperationException($"Une page avec ce Slug '{page.Slug}' existe déjà.");
            }
            GenerateSlug(page);
            _context.Pages.Add(page);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePage(Page page)
        {
            if (_context.Pages.Any(x => x.Slug == page.Slug.Trim() && x.Slug != page.Slug))
                throw new BookWithIsbnAlreadyExistsException($"Another Page with Slug {page.Slug} already exists.");

            if (string.IsNullOrWhiteSpace(page.Slug))
                GenerateSlug(page);

            _context.Pages.Update(page);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePageWithId(Guid id)
        {
            var page = _context.Pages.FirstOrDefault(x => x.Id == id);
            if (page == null)
                throw new BookNotFoundException($"Could not find page with id {id}.");

            _context.Pages.Remove(page);
            await _context.SaveChangesAsync();
        }

        private void GenerateSlug(Page page)
        {
            var slug = page.Slug;
            var slugs = _context.Pages.AsNoTracking().Where(x => x.Slug == slug).ToList();
            if (slugs.Any())
                slug = $"{slug}-{slug.Length + 1}";
            page.SetSlug(_slugHelper.GenerateSlug(slug).Replace(".", ""));
        }
    }
}
