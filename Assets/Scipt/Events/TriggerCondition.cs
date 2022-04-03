using System;

[Serializable]
public class TriggerCondition
{
    public enum TriggerType
    {
        AND, OR
    }

    public TriggerType triggerType = TriggerType.AND;
    public Condition[] conditions;

    public bool Check(string[] traitList, Stat[] statList)
    {
        switch (triggerType)
        {
            case TriggerType.AND:
                foreach (Condition c in conditions)
                {
                    if (c.Check(traitList, statList) == false)
                    {
                        return false;
                    }
                }
                return true;

            case TriggerType.OR:
                foreach (Condition c in conditions)
                {
                    if (c.Check(traitList, statList))
                    {
                        return true;
                    }
                }
                return false;
        }
        return false;
    }
}
