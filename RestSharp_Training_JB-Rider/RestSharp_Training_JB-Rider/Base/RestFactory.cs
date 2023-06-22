namespace RestSharp_Training_JB_Rider.Base;

public interface IRestFactory
{
    IRestBuilder Create();
}

public class RestFactory : IRestFactory
{
    private readonly IRestBuilder _restBuilder;
    public RestFactory(IRestBuilder restBuilder)
    {
        _restBuilder = restBuilder;
    }

    public IRestBuilder Create()
    {
        return _restBuilder;
    }
}