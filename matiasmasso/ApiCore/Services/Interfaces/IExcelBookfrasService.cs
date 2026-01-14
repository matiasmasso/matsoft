using DTO;

namespace Api.Services.Interfaces
{
    public interface IExcelBookfrasService
    {
        byte[] Excel(EmpModel.EmpIds emp, int year, string apiBaseUrl);

    }
}
