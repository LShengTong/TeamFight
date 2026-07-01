using System;

[Serializable]
public struct SlotIndex : IEquatable<SlotIndex>
{
    public SlotType slotType;
    public int x;
    public int y;

    public bool Equals(SlotIndex other)
    {
        return slotType == other.slotType && x == other.x && y == other.y;
    }

    public override bool Equals(object obj)
    {
        return obj is SlotIndex other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)slotType, x, y);
    }
}