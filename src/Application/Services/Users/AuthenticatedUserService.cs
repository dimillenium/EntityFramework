using Application.Interfaces.Services;
using Application.Interfaces.Services.Users;
using Application.Services.Users.Exceptions;
using Domain.Entities.Identity;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Users;

public class AuthenticatedUserService : IAuthenticatedUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextUserService _httpContextUserService;

    public AuthenticatedUserService(
        UserManager<User> userManager,
        IUserRepository userRepository,
        IHttpContextUserService httpContextUserService)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _httpContextUserService = httpContextUserService;
    }

    public User? GetAuthenticatedUser()
    {
        return _userRepository.FindByEmail(_httpContextUserService.Username!);
    }

    public async Task<IdentityResult> ChangeUserPassword(string currentPassword, string newPassword)
    {
        if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(newPassword))
            throw new ChangeAuthenticatedUserPasswordException("Current and new password cannot be null");

        var currentUserEmail = _httpContextUserService.Username;
        var user = _userRepository.FindByEmail(currentUserEmail!);
        if (user == null)
            throw new ChangeAuthenticatedUserPasswordException($"Could not find user with email {currentUserEmail}");

        return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    }

    public async Task<IdentityResult> ChangeUserEmail(string newEmail)
    {
        var user = GetAuthenticatedUser();
        if (user == null)
            throw new ChangeAuthenticatedUserEmailException($"Could not find user with email {_httpContextUserService.Username}");

        user.Email = newEmail;
        user.UserName = newEmail;
        user.EmailConfirmed = true;
        return await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> ChangeUserPhoneNumber(string newPhoneNumber, int? newPhoneExtension = null)
    {
        var user = GetAuthenticatedUser();
        if (user == null)
            throw new ChangeAuthenticatedPhoneNumberException($"Could not find user with email {_httpContextUserService.Username}");

        user.SetPhoneExtension(newPhoneExtension);
        var changePhoneNumberToken = await _userManager.GenerateChangePhoneNumberTokenAsync(user, newPhoneNumber);
        return await _userManager.ChangePhoneNumberAsync(user, newPhoneNumber, changePhoneNumberToken);
    }

    public void Dispose()
    {
        _userManager.Dispose();
    }
}