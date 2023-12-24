using WMS.data.repository;
using WMS.domain.entity;
using WMS.domain.enumerate;

namespace WMS.domain.use_case;

public class AuthorizationUseCase
{
    private AuthorizedUserRepository _authorizationUserRepository;
    public User GetUser()
    {
        return _authorizationUserRepository.Download();
    }


    public AuthorizationErrors CheckIsEmpty(string login, string password)
    {
        if (string.IsNullOrEmpty(login)) return AuthorizationErrors.NO_LOGIN;
        if (string.IsNullOrEmpty((password))) return AuthorizationErrors.NO_PASSWORD;
        return AuthorizationErrors.SUCCESS;
    }
    
    public void SaveUser(User user)
    {
        _authorizationUserRepository.Add(user);
    }

    public AuthorizationUseCase(AuthorizedUserRepository authorizedUserRepository)
    {
        _authorizationUserRepository = authorizedUserRepository;
    }
}