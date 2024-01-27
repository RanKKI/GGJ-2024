using UnityEngine;

public class Buff
{

    public string name;

    public float speed = 1;  // 1.3 = +30%, -0.7 = -30%

    public double jump = 1;

    public bool vertigo = false; // 眩晕


    public double duration = 0;

    public double validTo = -1; // Timestamp, -1 alway valid

    // 眩晕
    public static Buff banana = new()
    {
        name = "Banana",
        duration = 5,
        vertigo = true,
    };

    public Buff Start()
    {
        if (duration > 0)
        {
            validTo = Time.timeAsDouble + duration;
            Debug.Log("Set valid to " + validTo);
        }
        return new Buff
        {
            name = name,
            speed = speed,
            jump = jump,
            vertigo = vertigo,
            validTo = validTo,
            duration = duration,
        };
    }
}