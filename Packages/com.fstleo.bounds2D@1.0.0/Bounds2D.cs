using System;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine;

[Serializable]
public struct Bounds2D
{
    [SerializeField]
    private Vector2 m_Center;
    
    [SerializeField]
    private Vector2 m_Extents;

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        Vector2 vector2 = center;
        int hashCode = vector2.GetHashCode();
        vector2 = extents;
        int num = vector2.GetHashCode() << 2;
        return hashCode ^ num;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object other) => other is Bounds2D other1 && Equals(other1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Bounds2D other) => center.Equals(other.center) && extents.Equals(other.extents);

    public Vector2 center
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get => m_Center;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set => m_Center = value;
    }

    public Vector2 size
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get => m_Extents * 2f;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set => m_Extents = value * 0.5f;
    }

    public Vector2 extents
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get => m_Extents;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set => m_Extents = value;
    }

    public Vector2 min
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get => center - extents;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set => SetMinMax(value, max);
    }

    public Vector2 max
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get => center + extents;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set => SetMinMax(min, value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Bounds2D(Vector2 center, Vector2 size)
    {
        m_Center = center;
        m_Extents = size * 0.5f;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetMinMax(Vector2 min, Vector2 max)
    {
        extents = (max - min) * 0.5f;
        center = min + extents;
    }

    /// <summary> Does another bounding box intersect with this bounding box? </summary>
    public bool Intersects(Bounds2D bounds)
    {
        Vector2 maxDelta = extents + bounds.extents;
        Vector2 delta = center - bounds.center;
        return (Mathf.Abs(delta.x) <= Mathf.Abs(maxDelta.x) && Mathf.Abs(delta.y) <= Mathf.Abs(maxDelta.y));
    }

    /// <summary> Does this bounding box contain given point? </summary>
    public readonly bool Contains(Vector2 point)
    {
        point -= center;
        return Mathf.Abs(point.x) <= extents.x && Mathf.Abs(point.y) <= extents.y;
    }

    public readonly Vector2 Warp(Vector2 point)
    {
        if (Contains(point))
        {
            return point;
        }
        if (point.x > max.x)
        {
            point = new Vector2(point.x - size.x, point.y);
        } 
        else if (point.x < min.x)
        {
            point = new Vector2(point.x + size.x, point.y);
        }
        if (point.y < min.y)
        {
            point = new Vector2(point.x ,point.y + size.y);
        }
        else if (point.y > max.y)
        {
            point = new Vector2(point.x ,point.y - size.y);
        }
        return point;
    }

    #region Overrides

    public override string ToString()
    {
        return "Bounds2D[" + center + ", " + size + "]";
    }

    public static bool operator ==(Bounds2D a, Bounds2D b)
    {
        return (a.center == b.center) && (a.extents == b.extents);
    }

    public static bool operator !=(Bounds2D a, Bounds2D b)
    {
        return !(a == b);
    }

    #endregion

}