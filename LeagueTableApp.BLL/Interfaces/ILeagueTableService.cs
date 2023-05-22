using LeagueTableApp.BLL.DTOs;

namespace LeagueTableApp.BLL.Interfaces;
public interface ILeagueTableService<Entity>
{
    public Entity GetOne(int entityId);
    public IEnumerable<Entity> GetAll();
    public Entity InsertOne(Entity newEntity);
    public void UpdateOne(int entityId, Entity updatedEntity);
    public void DeleteOne(int entityId);
}
