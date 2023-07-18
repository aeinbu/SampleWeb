
public interface IMyService
{
    DateTimeOffset CreatedTime { get; init; }
    int Number { get; init; }
}

public interface IMyScopedService: IMyService
{
}

public interface IMySingletonService: IMyService
{
}

public interface IMyTransientService: IMyService
{
}

public class MyService : IMyScopedService, IMySingletonService, IMyTransientService
{
    private static int Counter = 0;
    public DateTimeOffset CreatedTime { get; init; } = DateTimeOffset.Now;
    public int Number { get; init; } = Counter++;

    public override string ToString()
    {
        return $"{CreatedTime} - {Number}";
    }
}