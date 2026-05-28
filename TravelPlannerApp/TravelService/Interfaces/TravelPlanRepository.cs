using TravelService.Models;

public interface ITravelPlanRepository
{
    Task<TravelPlan> AddAsync(TravelPlan plan);
    Task<List<TravelPlan>> GetAllByUserId(int userId);
    Task<TravelPlan> GetById(int id);
    Task Update(TravelPlan plan);
    Task Delete(TravelPlan plan);
}