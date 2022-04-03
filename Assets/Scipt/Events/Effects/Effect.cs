using System;

[Serializable]
public abstract class Effect
{
    public abstract void Play(Character[] targets);
}
