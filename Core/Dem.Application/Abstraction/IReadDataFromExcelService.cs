namespace Dem.Application.Abstraction;

public interface IReadDataFromExcelService
{
    public Task<Dictionary<string, string>> ReadDataFromExcel(string filePath);
}