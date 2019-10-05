using System;

public class Resource
{
    public const string HEALTH_ID = "health";

    public const float TICK_PERIOD = 0.1f;

    // Callbacks
    public Action<float> onFull;
    public Action<float> onEmpty;
    public Action<float> onChange;
    public Action<float> onChangePercent;

    string type;
    private float min;
    private float max;
    private float current;

    public string Type { get { return type; } set { type = value; } }
    public float Current
    {
        get { return current; }
        set
        {
            if (Util.CloseEnough(value, Current))
                return;

            current = value < Min ? Min : value > Max ? Max : value;

            if (onChange != null)
                onChange(Current);
            if (onChangePercent != null)
                onChangePercent(Percent);
            if (Full && onFull != null)
                onFull(value - Max);
            if (Empty && onEmpty != null)
                onEmpty(Min - value);
        }
    }
    public float Min { get { return min; } private set { min = value; } }
    public float Max { get { return max; } protected set { max = value; } }
    public float Percent { get { return Current / Max; } set { Current = Max * value; } }
    public bool Full { get { return Util.CloseEnough(Current, Max); } private set { if (value) Current = Max; } }
    public bool Empty { get { return Util.CloseEnough(Current, Min); } private set { if (value) Current = Min; } }

    public Resource(ResourceData data)
    {
        type = data.Type;
        min = data.Min;
        max = data.Max;
        current = data.Current;
    }

    public ResourceData AsResourceData()
    {
        return new ResourceData(type, min, max, current);
    }

    public void Update(float time)
    {
        //TODO
    }
}