using WMS.data.repository;
using WMS.domain.entity;

namespace WMS.domain.use_case;

public class AuthorizationUseCase
{
    private AuthorizedUserRepository _authorizationUserRepository;
    public User GetUser()
    {
        return _authorizationUserRepository.Download();
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