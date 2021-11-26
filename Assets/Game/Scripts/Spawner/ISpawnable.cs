using JetBrains.Annotations;

public interface ISpawnable
{
   [CanBeNull] Spawner Spawner { get; set; }
}