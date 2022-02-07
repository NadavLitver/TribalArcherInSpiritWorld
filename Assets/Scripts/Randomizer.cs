using UnityEngine;

public static class Randomizer
{

    public static int ReturnRandomNum(Vector2Int Range) => Random.Range(Range.x, Range.y);
    public static float ReturnRandomFloat(Vector2 Range) => Random.Range(Range.x, Range.y);
    public static int GetOneOrMinusOne() => Random.Range(0, 2) * 2 - 1;
    public static float GetOneOrMinusOneFloat() => Random.Range(0, 2f) * 2 - 1;

}
