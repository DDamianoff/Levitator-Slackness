using System;

namespace Levitator_Slackness;

public static class Utils
{
    public static void Times(this int count, Action action)
    {
        for (int i = 0; i < count; i++)
        {
            action();
        }
    }
}