using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Godot;

/// <summary>
/// 2-element structure that can be used to represent 2D grid coordinates or pairs of integers.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Vector2I : IEquatable<Vector2I>
{
    /// <summary>
    /// Enumerated index values for the axes.
    /// Returned by <see cref="MaxAxisIndex"/> and <see cref="MinAxisIndex"/>.
    /// </summary>
    public enum Axis
    {
        /// <summary>
        /// The vector's X axis.
        /// </summary>
        X,
        /// <summary>
        /// The vector's Y axis.
        /// </summary>
        Y,
    }

    /// <summary>
    /// The vector's X component. Also accessible by using the index position <c>[0]</c>.
    /// </summary>
    public int X;

    /// <summary>
    /// The vector's Y component. Also accessible by using the index position <c>[1]</c>.
    /// </summary>
    public int Y;

    /// <summary>
    /// Access vector components using their index.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="index"/> is not 0 or 1.
    /// </exception>
    /// <value>
    /// <c>[0]</c> is equivalent to <see cref="X"/>,
    /// <c>[1]</c> is equivalent to <see cref="Y"/>.
    /// </value>
    public int this[int index]
    {
        readonly get
        {
            return index switch
            {
                0 => X,
                1 => Y,
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };
        }
        set
        {
            switch (index)
            {
                case 0:
                    X = value;
                    return;
                case 1:
                    Y = value;
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
    }

    /// <summary>
    /// Helper method for deconstruction into a tuple.
    /// </summary>
    public readonly void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }

    /// <summary>
    /// Returns a new vector with all components in absolute values (i.e. positive).
    /// </summary>
    /// <returns>A vector with <see cref="int.Abs(int)"/> called on each component.</returns>
    public readonly Vector2I Abs()
    {
        return new Vector2I(int.Abs(X), int.Abs(Y));
    }

    /// <summary>
    /// Returns the aspect ratio of this vector, the ratio of <see cref="X"/> to <see cref="Y"/>.
    /// </summary>
    /// <returns>The <see cref="X"/> component divided by the <see cref="Y"/> component.</returns>
    public readonly real_t Aspect()
    {
        return X / (real_t)Y;
    }

    /// <summary>
    /// Returns a new vector with all components clamped between the
    /// components of <paramref name="min"/> and <paramref name="max"/> using
    /// <see cref="int.Clamp(int, int, int)"/>.
    /// </summary>
    /// <param name="min">The vector with minimum allowed values.</param>
    /// <param name="max">The vector with maximum allowed values.</param>
    /// <returns>The vector with all components clamped.</returns>
    public readonly Vector2I Clamp(Vector2I min, Vector2I max)
    {
        return new Vector2I
        (
            int.Clamp(X, min.X, max.X),
            int.Clamp(Y, min.Y, max.Y)
        );
    }

    /// <summary>
    /// Returns a new vector with all components clamped between the
    /// <paramref name="min"/> and <paramref name="max"/> using
    /// <see cref="int.Clamp(int, int, int)"/>.
    /// </summary>
    /// <param name="min">The minimum allowed value.</param>
    /// <param name="max">The maximum allowed value.</param>
    /// <returns>The vector with all components clamped.</returns>
    public readonly Vector2I Clamp(int min, int max)
    {
        return new Vector2I
        (
            int.Clamp(X, min, max),
            int.Clamp(Y, min, max)
        );
    }

    /// <summary>
    /// Returns the squared distance between this vector and <paramref name="to"/>.
    /// This method runs faster than <see cref="DistanceTo"/>, so prefer it if
    /// you need to compare vectors or need the squared distance for some formula.
    /// </summary>
    /// <param name="to">The other vector to use.</param>
    /// <returns>The squared distance between the two vectors.</returns>
    public readonly int DistanceSquaredTo(Vector2I to)
    {
        return (to - this).LengthSquared();
    }

    /// <summary>
    /// Returns the distance between this vector and <paramref name="to"/>.
    /// </summary>
    /// <seealso cref="DistanceSquaredTo(Vector2I)"/>
    /// <param name="to">The other vector to use.</param>
    /// <returns>The distance between the two vectors.</returns>
    public readonly real_t DistanceTo(Vector2I to)
    {
        return (to - this).Length();
    }

    /// <summary>
    /// Returns the length (magnitude) of this vector.
    /// </summary>
    /// <seealso cref="LengthSquared"/>
    /// <returns>The length of this vector.</returns>
    public readonly real_t Length()
    {
        int x2 = X * X;
        int y2 = Y * Y;

        return real_t.Sqrt(x2 + y2);
    }

    /// <summary>
    /// Returns the squared length (squared magnitude) of this vector.
    /// This method runs faster than <see cref="Length"/>, so prefer it if
    /// you need to compare vectors or need the squared length for some formula.
    /// </summary>
    /// <returns>The squared length of this vector.</returns>
    public readonly int LengthSquared()
    {
        int x2 = X * X;
        int y2 = Y * Y;

        return x2 + y2;
    }

    /// <summary>
    /// Returns the result of the component-wise maximum between this vector
    /// and <paramref name="with"/> using <see cref="int.Max(int, int)"/>.
    /// </summary>
    /// <param name="with">The other vector to use.</param>
    /// <returns>The resulting maximum vector.</returns>
    public readonly Vector2I Max(Vector2I with)
    {
        return new Vector2I
        (
            int.Max(X, with.X),
            int.Max(Y, with.Y)
        );
    }

    /// <summary>
    /// Returns the result of the component-wise maximum between this vector
    /// and <paramref name="with"/> using <see cref="int.Max(int, int)"/>.
    /// </summary>
    /// <param name="with">The other value to use.</param>
    /// <returns>The resulting maximum vector.</returns>
    public readonly Vector2I Max(int with)
    {
        return new Vector2I
        (
            int.Max(X, with),
            int.Max(Y, with)
        );
    }

    /// <summary>
    /// Returns the result of the component-wise minimum between this vector
    /// and <paramref name="with"/> using <see cref="int.Min(int, int)"/>.
    /// </summary>
    /// <param name="with">The other vector to use.</param>
    /// <returns>The resulting minimum vector.</returns>
    public readonly Vector2I Min(Vector2I with)
    {
        return new Vector2I
        (
            int.Min(X, with.X),
            int.Min(Y, with.Y)
        );
    }

    /// <summary>
    /// Returns the result of the component-wise minimum between this vector
    /// and <paramref name="with"/> using <see cref="int.Min(int, int)"/>.
    /// </summary>
    /// <param name="with">The other value to use.</param>
    /// <returns>The resulting minimum vector.</returns>
    public readonly Vector2I Min(int with)
    {
        return new Vector2I
        (
            int.Min(X, with),
            int.Min(Y, with)
        );
    }

    /// <summary>
    /// Returns the axis of the vector's highest value. See <see cref="Axis"/>.
    /// If both components are equal, this method returns <see cref="Axis.X"/>.
    /// </summary>
    /// <returns>The index of the highest axis.</returns>
    public readonly Axis MaxAxisIndex()
    {
        return X < Y ? Axis.Y : Axis.X;
    }

    /// <summary>
    /// Returns the axis of the vector's lowest value. See <see cref="Axis"/>.
    /// If both components are equal, this method returns <see cref="Axis.Y"/>.
    /// </summary>
    /// <returns>The index of the lowest axis.</returns>
    public readonly Axis MinAxisIndex()
    {
        return X < Y ? Axis.X : Axis.Y;
    }

    /// <summary>
    /// Returns a vector with each component set to one or negative one, depending
    /// on the signs of this vector's components, or zero if the component is zero,
    /// by calling <see cref="int.Sign(int)"/> on each component.
    /// </summary>
    /// <returns>A vector with all components as either <c>1</c>, <c>-1</c>, or <c>0</c>.</returns>
    public readonly Vector2I Sign()
    {
        return new Vector2I
        (
            int.Sign(X),
            int.Sign(Y)
        );
    }

    /// <summary>
    /// Returns a new vector with each component snapped to the closest multiple of the corresponding component
    /// in <paramref name="step"/>.
    /// </summary>
    /// <param name="step">A vector value representing the step size to snap to.</param>
    /// <returns>The snapped vector.</returns>
    public readonly Vector2I Snapped(Vector2I step)
    {
        return new Vector2I
        (
            (int)Mathf.Snapped(X, step.X),
            (int)Mathf.Snapped(Y, step.Y)
        );
    }

    /// <summary>
    /// Returns a new vector with each component snapped to the closest multiple of <paramref name="step"/>.
    /// </summary>
    /// <param name="step">The step size to snap to.</param>
    /// <returns>The snapped vector.</returns>
    public readonly Vector2I Snapped(int step)
    {
        return new Vector2I
        (
            (int)Mathf.Snapped(X, step),
            (int)Mathf.Snapped(Y, step)
        );
    }

    /// <summary>
    /// Min vector, a vector with all components equal to <see cref="int.MinValue"/>. Can be used as a negative integer equivalent of <see cref="Vector2.Inf"/>.
    /// </summary>
    /// <value>Equivalent to <c>new Vector2I(int.MinValue, int.MinValue)</c>.</value>
    public static Vector2I MinValue => new Vector2I(int.MinValue, int.MinValue);
    /// <summary>
    /// Max vector, a vector with all components equal to <see cref="int.MaxValue"/>. Can be used as an integer equivalent of <see cref="Vector2.Inf"/>.
    /// </summary>
    /// <value>Equivalent to <c>new Vector2I(int.MaxValue, int.MaxValue)</c>.</value>
    public static Vector2I MaxValue => new Vector2I(int.MaxValue, int.MaxValue);

    /// <summary>
    /// Zero vector, a vector with all components set to <c>0</c>.
    /// </summary>
    /// <value>Equivalent to <c>new Vector2I(0, 0)</c>.</value>
    public static Vector2I Zero => new Vector2I(0, 0);
    /// <summary>
    /// One vector, a vector with all components set to <c>1</c>.
    /// </summary>
    /// <value>Equivalent to <c>new Vector2I(1, 1)</c>.</value>
    public static Vector2I One => new Vector2I(1, 1);

    /// <summary>
    /// Up unit vector. Y is down in 2D, so this vector points -Y.
    /// </summary>
    /// <value>Equivalent to <c>new Vector2I(0, -1)</c>.</value>
    public static Vector2I Up => new Vector2I(0, -1);
    /// <summary>
    /// Down unit vector. Y is down in 2D, so this vector points +Y.
    /// </summary>
    /// <value>Equivalent to <c>new Vector2I(0, 1)</c>.</value>
    public static Vector2I Down => new Vector2I(0, 1);
    /// <summary>
    /// Right unit vector. Represents the direction of right.
    /// </summary>
    /// <value>Equivalent to <c>new Vector2I(1, 0)</c>.</value>
    public static Vector2I Right => new Vector2I(1, 0);
    /// <summary>
    /// Left unit vector. Represents the direction of left.
    /// </summary>
    /// <value>Equivalent to <c>new Vector2I(-1, 0)</c>.</value>
    public static Vector2I Left => new Vector2I(-1, 0);

    /// <summary>
    /// Constructs a new <see cref="Vector2I"/> with the given components.
    /// </summary>
    /// <param name="x">The vector's X component.</param>
    /// <param name="y">The vector's Y component.</param>
    public Vector2I(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Adds each component of the <see cref="Vector2I"/>
    /// with the components of the given <see cref="Vector2I"/>.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The added vector.</returns>
    public static Vector2I operator +(Vector2I left, Vector2I right)
    {
        return new Vector2I
        (
            left.X + right.X,
            left.Y + right.Y
        );
    }

    /// <summary>
    /// Subtracts each component of the <see cref="Vector2I"/>
    /// by the components of the given <see cref="Vector2I"/>.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The subtracted vector.</returns>
    public static Vector2I operator -(Vector2I left, Vector2I right)
    {
        return new Vector2I
        (
            left.X - right.X,
            left.Y - right.Y
        );
    }

    /// <summary>
    /// Returns the negative value of the <see cref="Vector2I"/>.
    /// This is the same as writing <c>new Vector2I(-v.X, -v.Y)</c>.
    /// This operation flips the direction of the vector while
    /// keeping the same magnitude.
    /// </summary>
    /// <param name="vec">The vector to negate/flip.</param>
    /// <returns>The negated/flipped vector.</returns>
    public static Vector2I operator -(Vector2I vec)
    {
        return new Vector2I
        (
            -vec.X,
            -vec.Y
        );
    }

    /// <summary>
    /// Multiplies each component of the <see cref="Vector2I"/>
    /// by the given <see langword="int"/>.
    /// </summary>
    /// <param name="vec">The vector to multiply.</param>
    /// <param name="scale">The scale to multiply by.</param>
    /// <returns>The multiplied vector.</returns>
    public static Vector2I operator *(Vector2I vec, int scale)
    {
        return new Vector2I
        (
            vec.X * scale,
            vec.Y * scale
        );
    }

    /// <summary>
    /// Multiplies each component of the <see cref="Vector2I"/>
    /// by the given <see langword="int"/>.
    /// </summary>
    /// <param name="scale">The scale to multiply by.</param>
    /// <param name="vec">The vector to multiply.</param>
    /// <returns>The multiplied vector.</returns>
    public static Vector2I operator *(int scale, Vector2I vec)
    {
        return new Vector2I
        (
            scale * vec.X,
            scale * vec.Y
        );
    }

    /// <summary>
    /// Multiplies each component of the <see cref="Vector2I"/>
    /// by the components of the given <see cref="Vector2I"/>.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The multiplied vector.</returns>
    public static Vector2I operator *(Vector2I left, Vector2I right)
    {
        return new Vector2I
        (
            left.X * right.X,
            left.Y * right.Y
        );
    }

    /// <summary>
    /// Divides each component of the <see cref="Vector2I"/>
    /// by the given <see langword="int"/>.
    /// </summary>
    /// <param name="vec">The dividend vector.</param>
    /// <param name="divisor">The divisor value.</param>
    /// <returns>The divided vector.</returns>
    public static Vector2I operator /(Vector2I vec, int divisor)
    {
        return new Vector2I
        (
            vec.X / divisor,
            vec.Y / divisor
        );
    }

    /// <summary>
    /// Divides each component of the <see cref="Vector2I"/>
    /// by the components of the given <see cref="Vector2I"/>.
    /// </summary>
    /// <param name="vec">The dividend vector.</param>
    /// <param name="divisorv">The divisor vector.</param>
    /// <returns>The divided vector.</returns>
    public static Vector2I operator /(Vector2I vec, Vector2I divisorv)
    {
        return new Vector2I
        (
            vec.X / divisorv.X,
            vec.Y / divisorv.Y
        );
    }

    /// <summary>
    /// Gets the remainder of each component of the <see cref="Vector2I"/>
    /// with the components of the given <see langword="int"/>.
    /// This operation uses truncated division, which is often not desired
    /// as it does not work well with negative numbers.
    /// Consider using <see cref="Mathf.PosMod(int, int)"/> instead
    /// if you want to handle negative numbers.
    /// </summary>
    /// <example>
    /// <code>
    /// GD.Print(new Vector2I(10, -20) % 7); // Prints "(3, -6)"
    /// </code>
    /// </example>
    /// <param name="vec">The dividend vector.</param>
    /// <param name="divisor">The divisor value.</param>
    /// <returns>The remainder vector.</returns>
    public static Vector2I operator %(Vector2I vec, int divisor)
    {
        return new Vector2I
        (
            vec.X % divisor,
            vec.Y % divisor
        );
    }

    /// <summary>
    /// Gets the remainder of each component of the <see cref="Vector2I"/>
    /// with the components of the given <see cref="Vector2I"/>.
    /// This operation uses truncated division, which is often not desired
    /// as it does not work well with negative numbers.
    /// Consider using <see cref="Mathf.PosMod(int, int)"/> instead
    /// if you want to handle negative numbers.
    /// </summary>
    /// <example>
    /// <code>
    /// GD.Print(new Vector2I(10, -20) % new Vector2I(7, 8)); // Prints "(3, -4)"
    /// </code>
    /// </example>
    /// <param name="vec">The dividend vector.</param>
    /// <param name="divisorv">The divisor vector.</param>
    /// <returns>The remainder vector.</returns>
    public static Vector2I operator %(Vector2I vec, Vector2I divisorv)
    {
        return new Vector2I
        (
            vec.X % divisorv.X,
            vec.Y % divisorv.Y
        );
    }

    /// <summary>
    /// Returns <see langword="true"/> if the vectors are equal.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>Whether or not the vectors are equal.</returns>
    public static bool operator ==(Vector2I left, Vector2I right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Returns <see langword="true"/> if the vectors are not equal.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>Whether or not the vectors are not equal.</returns>
    public static bool operator !=(Vector2I left, Vector2I right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    /// Compares two <see cref="Vector2I"/> vectors by first checking if
    /// the X value of the <paramref name="left"/> vector is less than
    /// the X value of the <paramref name="right"/> vector.
    /// If the X values are exactly equal, then it repeats this check
    /// with the Y values of the two vectors.
    /// This operator is useful for sorting vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>Whether or not the left is less than the right.</returns>
    public static bool operator <(Vector2I left, Vector2I right)
    {
        if (left.X == right.X)
        {
            return left.Y < right.Y;
        }
        return left.X < right.X;
    }

    /// <summary>
    /// Compares two <see cref="Vector2I"/> vectors by first checking if
    /// the X value of the <paramref name="left"/> vector is greater than
    /// the X value of the <paramref name="right"/> vector.
    /// If the X values are exactly equal, then it repeats this check
    /// with the Y values of the two vectors.
    /// This operator is useful for sorting vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>Whether or not the left is greater than the right.</returns>
    public static bool operator >(Vector2I left, Vector2I right)
    {
        if (left.X == right.X)
        {
            return left.Y > right.Y;
        }
        return left.X > right.X;
    }

    /// <summary>
    /// Compares two <see cref="Vector2I"/> vectors by first checking if
    /// the X value of the <paramref name="left"/> vector is less than
    /// or equal to the X value of the <paramref name="right"/> vector.
    /// If the X values are exactly equal, then it repeats this check
    /// with the Y values of the two vectors.
    /// This operator is useful for sorting vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>Whether or not the left is less than or equal to the right.</returns>
    public static bool operator <=(Vector2I left, Vector2I right)
    {
        if (left.X == right.X)
        {
            return left.Y <= right.Y;
        }
        return left.X < right.X;
    }

    /// <summary>
    /// Compares two <see cref="Vector2I"/> vectors by first checking if
    /// the X value of the <paramref name="left"/> vector is greater than
    /// or equal to the X value of the <paramref name="right"/> vector.
    /// If the X values are exactly equal, then it repeats this check
    /// with the Y values of the two vectors.
    /// This operator is useful for sorting vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>Whether or not the left is greater than or equal to the right.</returns>
    public static bool operator >=(Vector2I left, Vector2I right)
    {
        if (left.X == right.X)
        {
            return left.Y >= right.Y;
        }
        return left.X > right.X;
    }

    /// <summary>
    /// Converts this <see cref="Vector2I"/> to a <see cref="Vector2"/>.
    /// </summary>
    /// <param name="value">The vector to convert.</param>
    public static implicit operator Vector2(Vector2I value)
    {
        return new Vector2
        (
            value.X,
            value.Y
        );
    }

    /// <summary>
    /// Converts a <see cref="Vector2"/> to a <see cref="Vector2I"/> by truncating
    /// components' fractional parts (rounding towards zero). For a different
    /// behavior consider passing the result of <see cref="Vector2.Ceil"/>,
    /// <see cref="Vector2.Floor"/> or <see cref="Vector2.Round"/> to this conversion operator instead.
    /// </summary>
    /// <param name="value">The vector to convert.</param>
    public static explicit operator Vector2I(Vector2 value)
    {
        return new Vector2I
        (
            (int)value.X,
            (int)value.Y
        );
    }

    /// <summary>
    /// Returns <see langword="true"/> if the vector is equal
    /// to the given object (<paramref name="obj"/>).
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns>Whether or not the vector and the object are equal.</returns>
    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Vector2I other && Equals(other);
    }

    /// <summary>
    /// Returns <see langword="true"/> if the vectors are equal.
    /// </summary>
    /// <param name="other">The other vector.</param>
    /// <returns>Whether or not the vectors are equal.</returns>
    public readonly bool Equals(Vector2I other)
    {
        return X == other.X
            && Y == other.Y;
    }

    /// <summary>
    /// Serves as the hash function for <see cref="Vector2I"/>.
    /// </summary>
    /// <returns>A hash code for this vector.</returns>
    public override readonly int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    /// <summary>
    /// Converts this <see cref="Vector2I"/> to a string.
    /// </summary>
    /// <returns>A string representation of this vector.</returns>
    public override readonly string ToString() => ToString(null);

    /// <summary>
    /// Converts this <see cref="Vector2I"/> to a string with the given <paramref name="format"/>.
    /// </summary>
    /// <returns>A string representation of this vector.</returns>
    public readonly string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format)
    {
        return $"({X.ToString(format, CultureInfo.InvariantCulture)}, {Y.ToString(format, CultureInfo.InvariantCulture)})";
    }
}
