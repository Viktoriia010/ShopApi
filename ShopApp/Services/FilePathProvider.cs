using Shop.Application.Interfaces.Configurations;

namespace Shop.Api.Services;

public class FilePathProvider(IConfiguration _configuration) : IFilePathProvider
{
    private string _dirname = "DirnameForFiles";
    public string Categories =>
        _configuration[$"{_dirname}:Categories"]
        ?? throw new InvalidOperationException();

    public string Products =>
        _configuration[$"{_dirname}:Products"]
        ?? throw new InvalidOperationException();

}