using System;

[Serializable]
public abstract class Effect
{
    public abstract void Play(Character[] targets);
    public abstract void Update(Character[] targets);
}
