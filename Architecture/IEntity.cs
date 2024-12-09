namespace Sokoban.Architecture;

public interface IEntity
{
	EntityCommand Act(int x, int y);
    string GetImageFileName();
    int GetDrawingPriority();
}