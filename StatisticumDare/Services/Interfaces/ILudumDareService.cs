using StatisticumDare.Models;

namespace StatisticumDare.Services.Interfaces
{
    public interface ILudumDareService
    {
        Task<LudumDareGameData?> GetGameDataByUsername(string username);
    }
}
