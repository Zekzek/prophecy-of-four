public class Cooldown
{
    public delegate void OnCooldown();
    private OnCooldown callback;

    private float maxTime;
    private float timeRemaining;

    public Cooldown(float maxTime, OnCooldown callback)
    {
        this.maxTime = maxTime;
        timeRemaining = 0;
        this.callback = callback;
    }

    public Cooldown(float maxTime) : this(maxTime, () => { }) { }

    public void Start()
    {
        timeRemaining = maxTime;
    }

    public void Update(float deltaTime)
    {
        if (!IsUp())
        {
            timeRemaining -= deltaTime;
            if (IsUp())
                callback();
        }
    }

    public bool IsUp()
    {
        return timeRemaining <= 0;
    }
}
