﻿using Application.Interfaces.Services.Books;
using Domain.Constants.User;
using Domain.Entities.Books;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Tests.Common.Mapping;
using Web.Features.Members.Books.EditBook;
using Web.Mapping.Profiles;

namespace Tests.Web.Features.Admins.Books.CreateBook;

public class EditBookEndpointTests
{
    private const string NameFr = "Guide de la réglementation en copropriété";
    private const string NameEn = "Guide to condominium regulations";
    private const string DescriptionFr = "On vise, en copropriété, à assurer aux occupant un milieu de vie paisible.";
    private const string DescriptionEn = "We aim, in co-ownership, to provide occupants with a peaceful living environment.";
    private const string Isbn = "978-2-89689-559-5";
    private const string Author = "Christine Gagnon, Yves Papineau";
    private const string Editor = "Wilson & Lafleur";
    private const int YearOfPublication = 2023;
    private const int NumberOfPages = 346;
    private const decimal Price = 20;

    private readonly Mock<IBookUpdateService> _bookUpdateService;

    private readonly EditBookEndpoint _endPoint;

    public EditBookEndpointTests()
    {
        _bookUpdateService = new Mock<IBookUpdateService>();

        _endPoint = Factory.Create<EditBookEndpoint>(
            new MapperBuilder().WithProfile<RequestMappingProfile>().Build(),
            _bookUpdateService.Object
        );
    }

    [Fact]
    public void WhenConfigure_ThenConfigureVerbToBePut()
    {
        // Act
        _endPoint.Configure();

        // Assert
        _endPoint.Definition.Verbs.ShouldContain(Http.PUT.ToString());
    }

    [Fact]
    public void WhenConfigure_ThenConfigureRoute()
    {
        // Act
        _endPoint.Configure();

        // Assert
        _endPoint.Definition.Routes.ShouldContain("books/{id}");
    }

    [Fact]
    public void WhenConfigure_ThenConfigureAllowedRoles()
    {
        // Act
        _endPoint.Configure();

        // Assert
        _endPoint.Definition.AllowedRoles!.ShouldContain(Roles.MEMBER);
    }

    [Fact]
    public void WhenConfigure_ThenConfigureAuthSchemeToBeCookieAuthenticationScheme()
    {
        // Act
        _endPoint.Configure();

        // Assert
        _endPoint.Definition.AuthSchemeNames!.ShouldContain(JwtBearerDefaults.AuthenticationScheme);
    }

    [Fact]
    public async Task WhenHandleAsync_ThenReturnOkResult()
    {
        // Arrange
        var request = BuildValidRequest();

        // Act
        await _endPoint.HandleAsync(request, CancellationToken.None);

        // Assert
        _endPoint.HttpContext.Response.StatusCode.ShouldBe(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task WhenHandleAsync_ThenDelegateCreatingBookToBookCreationService()
    {
        // Arrange
        var request = BuildValidRequest();

        // Act
        await _endPoint.HandleAsync(request, CancellationToken.None);

        // Assert
        _bookUpdateService.Verify(x => x.UpdateBook(It.IsAny<Book>(), It.IsAny<IFormFile>()));
    }

    [Fact]
    public async Task WhenHandleAsync_ThenReturnSucceededOrNotResponseWithSucceededTrue()
    {
        // Arrange
        var request = BuildValidRequest();

        // Act
        await _endPoint.HandleAsync(request, default);

        // Assert
        _endPoint.Response.Succeeded.ShouldBeTrue();
    }

    private EditBookRequest BuildValidRequest()
    {
        return new EditBookRequest
        {
            Id = Guid.NewGuid(),
            NameFr = NameFr,
            NameEn = NameEn,
            DescriptionFr = DescriptionFr,
            DescriptionEn = DescriptionEn,
            Isbn = Isbn,
            Author = Author,
            Editor = Editor,
            YearOfPublication = YearOfPublication,
            NumberOfPages = NumberOfPages,
            Price = Price,
            CardImage = new FormFile(new MemoryStream(new byte[5]), long.MaxValue, long.MaxValue,
                "card image name", "any_filename")
        };
    }
}