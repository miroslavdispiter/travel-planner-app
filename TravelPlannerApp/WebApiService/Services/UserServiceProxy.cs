using Microsoft.ServiceFabric.Services.Remoting.Client;
using Shared.Interfaces;

public class UserServiceProxy
{
    public IUserService GetUserServiceProxy()
        => ServiceProxy.Create<IUserService>(
            new Uri("fabric:/TravelPlannerApp/UserService")
        );
}