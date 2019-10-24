using System;
using UtiliZek;

public class ThingResource : Resource
{
    public const string HEALTH_ID = "health";

    public const float TICK_PERIOD = 0.1f;

    public ThingResource(ResourceData data)
    {
        Type = data.Type;
        Min = data.Min;
        Max = data.Max;
        Current = data.Current;
    }

    public ResourceData AsResourceData()
    {
        return new ResourceData(Type, Min, Max, Current);
    }

    public void Update(float time)
    {
        //TODO
    }
}