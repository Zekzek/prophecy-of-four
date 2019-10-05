using System;
using System.Collections.Generic;

public class Stat
{
    private string type;
    private List<string> tags;
    private float value;

    public Stat(StatData data)
    {
        type = data.Type;
        tags = data.Tags;
        value = data.Value;
    }

    public StatData AsStatData()
    {
        return new StatData(type, tags, value);
    }
}