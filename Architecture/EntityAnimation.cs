using Avalonia;

namespace Sokoban.Architecture;

public class EntityAnimation
{
	public EntityCommand Command;
	public IEntity Entity;
	public Point TargetLocation;
	public Point TargetLogicalLocation;
}