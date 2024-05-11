﻿using System.IO;

namespace Fischless.GamepadCapture.RawInput;

public class Input : IEquatable<Input>, IBinary
{
    public readonly string Key;
    public readonly long Source;

    public Input(string key, long source)
    {
        Key = key;
        Source = source;
    }

    public Input(string key, IntPtr source)
    {
        Key = key;
        Source = source.ToInt64();
    }

    public bool Equals(Input other)
        => !(other is null) && Key.Equals(other.Key) && Source.Equals(other.Source);

    public override bool Equals(object obj)
        => Equals(obj as Input);

    public static bool operator ==(Input left, Input right)
        => left.Equals(right);

    public static bool operator !=(Input left, Input right)
        => !(left == right);

    public override int GetHashCode()
        => Tuple.Create(Source, Key).GetHashCode();

    public override string ToString()
        => Key.ToString();

    public void ToBinary(BinaryWriter bw)
    {
        bw.Write(Key);
        bw.Write(Source);
    }

    public static Input FromBinary(BinaryReader br, uint fileVersion)
    {
        if (fileVersion == 0)
        {
            char id = br.ReadChar();

            if (id == 'k')
                return new Input(((Keys)br.ReadInt32()).ToString(), 0);

            if (id == 'g' || id == 'w')
                return new Input(((GamepadKeys)br.ReadInt32()).ToString(), 1);

            if (id == 'm')
                return new Input(((MouseKeys)br.ReadInt32()).ToString(), 2);
        }

        return new Input(br.ReadString(), br.ReadInt64());
    }
}
