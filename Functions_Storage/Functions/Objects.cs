using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Functions_Storage;

[StructLayout(LayoutKind.Sequential)]
public readonly struct intN : IComparable, IComparable<intN>, IEquatable<intN>
{
    private readonly int _v;
    public intN(int v) => _v = v;
    public static implicit operator intN(int v) => new(v);
    public static implicit operator int(intN v) => v._v;
    public static intN operator +(intN a, intN b) => a._v + b._v;
    public static intN operator -(intN a, intN b) => a._v - b._v;
    public static intN operator *(intN a, intN b) => a._v * b._v;
    public static intN operator /(intN a, intN b) => a._v / b._v;
    public static intN operator %(intN a, intN b) => a._v % b._v;
    public static intN operator ++(intN a) => new(a._v + 1);
    public static intN operator --(intN a) => new(a._v - 1);
    public static bool operator >(intN a, intN b) => a._v > b._v;
    public static bool operator <(intN a, intN b) => a._v < b._v;
    public static bool operator >=(intN a, intN b) => a._v >= b._v;
    public static bool operator <=(intN a, intN b) => a._v <= b._v;
    public static bool operator ==(intN a, intN b) => a._v == b._v;
    public static bool operator !=(intN a, intN b) => a._v != b._v;
    public static intN operator <<(intN a, int b) => a._v << b;
    public static intN operator >>(intN a, int b) => a._v >> b;
    public static intN operator &(intN a, intN b) => a._v & b._v;
    public static intN operator |(intN a, intN b) => a._v | b._v;
    public static intN operator ^(intN a, intN b) => a._v ^ b._v;
    public static intN operator ~(intN a) => ~a._v;
    public int CompareTo(intN other) => _v.CompareTo(other._v);
    public int CompareTo(object obj) => obj is intN n ? _v.CompareTo(n._v) : 0;
    public bool Equals(intN other) => _v == other._v;
    public override bool Equals(object obj) => obj is intN n && Equals(n);
    public override int GetHashCode() => _v;
    public bool IsEven => _v % 2 == 0;
    public bool this[int bit] => (_v & (1 << bit)) != 0;
    public unsafe void WriteHex(char* buffer)
    {
        const string hexChars = "0123456789ABCDEF";
        buffer[0] = '0';
        buffer[1] = 'x';
        for (int i = 0; i < 8; i++)
        {
            buffer[9 - i] = hexChars[(_v >> (i * 4)) & 0xF];
        }
    }
    public static intN Parse(ReadOnlySpan<char> s) => new(int.Parse(s));
    public static bool TryParse(ReadOnlySpan<char> s, out intN result)
    {
        bool success = int.TryParse(s, out int value);
        result = new intN(value);
        return success;
    }
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct floatN : IComparable<floatN>, IEquatable<floatN>
{
    private readonly float _v;
    public floatN(float v) => _v = v;
    public static implicit operator floatN(float v) => new(v);
    public static implicit operator float(floatN v) => v._v;
    public static floatN operator +(floatN a, floatN b) => a._v + b._v;
    public static floatN operator -(floatN a, floatN b) => a._v - b._v;
    public static floatN operator *(floatN a, floatN b) => a._v * b._v;
    public static floatN operator /(floatN a, floatN b) => a._v / b._v;
    public static bool operator >(floatN a, floatN b) => a._v > b._v;
    public static bool operator <(floatN a, floatN b) => a._v < b._v;
    public static bool operator >=(floatN a, floatN b) => a._v >= b._v;
    public static bool operator <=(floatN a, floatN b) => a._v <= b._v;
    public static bool operator ==(floatN a, floatN b) => a._v == b._v;
    public static bool operator !=(floatN a, floatN b) => a._v != b._v;
    public int CompareTo(floatN other) => _v.CompareTo(other._v);
    public bool Equals(floatN other) => _v == other._v;
    public override bool Equals(object obj) => obj is floatN other && Equals(other);
    public override int GetHashCode() => _v.GetHashCode();
    public bool IsPositive => _v > 0;
    public bool IsNegative => _v < 0;
    public bool IsNearZero => Math.Abs(_v) < 0.0001f;
    public unsafe intN ToRawInt()
    {
        float val = _v;
        return *(intN*)&val;
    }
    public static floatN Parse(ReadOnlySpan<char> s) => new(float.Parse(s));
    public static bool TryParse(ReadOnlySpan<char> s, out floatN result)
    {
        bool success = float.TryParse(s, out float value);
        result = new floatN(value);
        return success;
    }
    public override string ToString() => _v.ToString();
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct doubleN : IComparable<doubleN>, IEquatable<doubleN>, ISpanFormattable
{
    private readonly double _v;
    public doubleN(double v) => _v = v;
    public doubleN(intN v) => _v = (int)v;
    public static implicit operator doubleN(double v) => new doubleN(v);
    public static implicit operator double(doubleN v) => v._v;
    public static implicit operator doubleN(intN v) => new doubleN(v);
    public static doubleN PI => 3.1415926535897931;
    public static doubleN E => 2.7182818284590451;
    public static doubleN MaxValue => double.MaxValue;
    public static doubleN MinValue => double.MinValue;
    public static doubleN Epsilon => double.Epsilon;
    public static doubleN NaN => double.NaN;
    public static doubleN PositiveInfinity => double.PositiveInfinity;
    public static doubleN NegativeInfinity => double.NegativeInfinity;
    public static doubleN operator +(doubleN a, doubleN b) => a._v + b._v;
    public static doubleN operator -(doubleN a, doubleN b) => a._v - b._v;
    public static doubleN operator *(doubleN a, doubleN b) => a._v * b._v;
    public static doubleN operator /(doubleN a, doubleN b) => a._v / b._v;
    public static doubleN operator -(doubleN a) => -a._v;
    public static bool operator >(doubleN a, doubleN b) => a._v > b._v;
    public static bool operator <(doubleN a, doubleN b) => a._v < b._v;
    public static bool operator >=(doubleN a, doubleN b) => a._v >= b._v;
    public static bool operator <=(doubleN a, doubleN b) => a._v <= b._v;
    public static bool operator ==(doubleN a, doubleN b) => a._v == b._v;
    public static bool operator !=(doubleN a, doubleN b) => a._v != b._v;
    public int CompareTo(doubleN other) => _v.CompareTo(other._v);
    public bool Equals(doubleN other) => _v == other._v;
    public override bool Equals(object obj) => obj is doubleN other && Equals(other);
    public override int GetHashCode() => _v.GetHashCode();
    public bool IsNaN => double.IsNaN(_v);
    public bool IsInfinity => double.IsInfinity(_v);
    public bool IsPositive => _v > 0;
    public bool IsNegative => _v < 0;
    public bool IsNearZero => Math.Abs(_v) < 1e-15;
    public doubleN Abs() => Math.Abs(_v);
    public doubleN Sqrt() => Math.Sqrt(_v);
    public doubleN Round() => Math.Round(_v);
    public doubleN Round(intN digits) => Math.Round(_v, (int)digits);
    public doubleN Floor() => Math.Floor(_v);
    public doubleN Ceiling() => Math.Ceiling(_v);
    public doubleN Pow(doubleN power) => Math.Pow(_v, power._v);
    public doubleN Sin() => Math.Sin(_v);
    public doubleN Cos() => Math.Cos(_v);
    public doubleN Tan() => Math.Tan(_v);
    public unsafe long ToRawBits() { double val = _v; return *(long*)&val; }
    public static doubleN Parse(ReadOnlySpan<char> s) => new(double.Parse(s));
    public static bool TryParse(ReadOnlySpan<char> s, out doubleN result)
    {
        bool success = double.TryParse(s, out double value);
        result = new doubleN(value);
        return success;
    }
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider)
    => _v.TryFormat(destination, out charsWritten, format, provider);
    public string ToString(string format, IFormatProvider formatProvider) => _v.ToString(format, formatProvider);
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct charN : IComparable<charN>, IEquatable<charN>
{
    private readonly char _v;
    public charN(char v) => _v = v;
    public static implicit operator charN(char v) => new charN(v);
    public static implicit operator char(charN v) => v._v;
    public static implicit operator intN(charN v) => (int)v._v;
    public static explicit operator charN(intN v) => (char)(int)v;
    public static charN operator +(charN c, intN s) => (char)(c._v + (int)s);
    public static charN operator -(charN c, intN s) => (char)(c._v - (int)s);
    public static bool operator >(charN a, charN b) => a._v > b._v;
    public static bool operator <(charN a, charN b) => a._v < b._v;
    public static bool operator ==(charN a, charN b) => a._v == b._v;
    public static bool operator !=(charN a, charN b) => a._v != b._v;
    public int CompareTo(charN o) => _v.CompareTo(o._v);
    public bool Equals(charN o) => _v == o._v;
    public override bool Equals(object o) => o is charN c && Equals(c);
    public override int GetHashCode() => _v.GetHashCode();
    public override string ToString() => _v.ToString();
    public bool IsDigit => char.IsDigit(_v);
    public bool IsLetter => char.IsLetter(_v);
    public bool IsUpper => char.IsUpper(_v);
    public bool IsLower => char.IsLower(_v);
    public bool IsWhiteSpace => char.IsWhiteSpace(_v);
    public charN ToUpper() => char.ToUpper(_v);
    public charN ToLower() => char.ToLower(_v);
}

public unsafe ref struct stringN
{
    private charN* _v;
    private intN _l;
    public intN Length => _l;
    public stringN(charN* ptr, intN len) { _v = ptr; _l = len; }
    public charN this[intN i] {
        get => _v[(int)i];
        set => _v[(int)i] = value;
    }
    public bool IsPalindrome {
        get {
            for (intN i = 0; i < _l / (intN)2; i++)
                if (_v[(int)i] != _v[(int)(_l - (intN)1 - i)]) return false;
            return true;
        }
    }
    public void Reverse() {
        for (intN i = 0; i < _l / (intN)2; i++) {
            charN t = _v[(int)i];
            _v[(int)i] = _v[(int)(_l - (intN)1 - i)];
            _v[(int)(_l - (intN)1 - i)] = t;
        }
    }
    public static bool operator ==(stringN a, stringN b) {
        if (a._l != b._l) return false;
        for (intN i = 0; i < a._l; i++) if (a[i] != b[i]) return false;
        return true;
    }
    public static bool operator !=(stringN a, stringN b) => !(a == b);
}
public class vectorN
{
    private double[] _d;
    public int Size => _d.Length;
    public vectorN(params double[] v) => _d = v;
    public double this[int i] { get => _d[i]; set => _d[i] = value; }
    public double Dot(vectorN o) {
        double r = 0;
        for (int i = 0; i < Size; i++) r += _d[i] * o._d[i];
        return r;
    }
    public static vectorN operator +(vectorN a, vectorN b) {
        double[] r = new double[a.Size];
        for (int i = 0; i < a.Size; i++) r[i] = a[i] + b[i];
        return new vectorN(r);
    }
    public static vectorN operator *(vectorN a, double s) {
        double[] r = new double[a.Size];
        for (int i = 0; i < a.Size; i++) r[i] = a[i] * s;
        return new vectorN(r);
    }
    public override string ToString() {
        string s = "vec{";
        for (int i = 0; i < Size; i++) s += _d[i].ToString("F2") + (i == Size - 1 ? "" : ",");
        return s + "}";
    }
}
public class ifN
{
    private bool _c, _e = false;
    private ifN(bool c) => _c = c;
    public static ifN That(bool c) => new ifN(c);
    public ifN Then(Action a) { if (_c) { a(); _e = true; } return this; }
    public void Else(Action a) { if (!_c && !_e) a(); }
}
public static class forN
{
    public static void Between(int s, int e, Action<int> a) { for (int i = s; i < e; i++) a(i); }
    public static void Each<T>(T[] items, Action<T> a) { for (int i = 0; i < items.Length; i++) a(items[i]); }
}
public static class whileN
{
    public static void Cycle(Func<bool> c, Action a) { while (c()) a(); }
}
public class fileN
{
    public string Path { get; }
    public fileN(string p) => Path = p;
    public bool Exists => File.Exists(Path);
    public string Read() => File.ReadAllText(Path);
    public void Write(string c) => File.WriteAllText(Path, c);
}
public class hostN
{
    public string Addr { get; }
    public hostN(string a) => Addr = a;
    public bool IsOnline
    {
        get
        {
            try
            {
                using (var p = new System.Net.NetworkInformation.Ping())
                    return p.Send(Addr, 500).Status == System.Net.NetworkInformation.IPStatus.Success;
            }
            catch { return false; }
        }
    }
}
public class timeN
{
    private DateTime _d;
    public timeN(DateTime d) => _d = d;
    public static timeN Now => new timeN(DateTime.Now);
    public long Unix => ((DateTimeOffset)_d).ToUnixTimeSeconds();
    public override string ToString() => _d.ToString("yyyy-MM-dd HH:mm:ss");
}
public class matrixN
{
    private double[,] _data;
    public int Rows => _data.GetLength(0);
    public int Cols => _data.GetLength(1);
    public matrixN(int rows, int cols) => _data = new double[rows, cols];
    public matrixN(double[,] data) => _data = data;
    public double this[int r, int c] {
        get => _data[r, c];
        set => _data[r, c] = value;
    }
    public static matrixN operator *(matrixN a, matrixN b)
        => new matrixN(AiUtils.MultiplyMatrices(a._data, b._data));
    public matrixN Transpose() => new matrixN(AiUtils.Transpose(_data));
    public override string ToString() => $"matrixN[{Rows}x{Cols}]";
}
public class listN<T>
{
    private T[] _items;
    private int _cap;
    public int Count { get; private set; }
    public listN(int c = 4) { _cap = c; _items = new T[_cap]; }
    public listN(IEnumerable<T> col, bool shuf = false)
    {
        T[] t = col.ToArray();
        if (shuf) t = t.OrderBy(x => Guid.NewGuid()).ToArray();
        _cap = t.Length > 0 ? t.Length : 4; _items = new T[_cap];
        foreach (var x in t) Add(x);
    }
    public T this[int i] { get => _items[i]; set => _items[i] = value; }
    public void Add(T x) { if (Count == _cap) Resize(); _items[Count++] = x; }
    private void Resize() { _cap = _cap == 0 ? 4 : _cap * 2; T[] n = new T[_cap]; Array.Copy(_items, n, Count); _items = n; }
    public T RandomItem() => Count == 0 ? default : _items[new Random().Next(0, Count)];
    public T[] ToArray() { T[] r = new T[Count]; Array.Copy(_items, r, Count); return r; }
    public static listN<T> operator +(listN<T> a, listN<T> b)
    {
        var r = new listN<T>(a.ToArray()); foreach (var x in b.ToArray()) r.Add(x); return r;
    }
    public override string ToString() => Count == 0 ? "[]" : string.Join(", ", ToArray());
    public static implicit operator listN<T>(T[] a) => new listN<T>(a, true);
}
public class stackN<T>
{
    private T[] _items;
    private int _cap;
    public int Count { get; private set; }
    public bool IsEmpty => Count == 0;
    public stackN(int c = 4) { _cap = c; _items = new T[_cap]; }
    public void Push(T x) { if (Count == _cap) Resize(); _items[Count++] = x; }
    public T Pop() { if (IsEmpty) return default; T x = _items[--Count]; _items[Count] = default; return x; }
    public T Peek() => IsEmpty ? default : _items[Count - 1];
    private void Resize() { _cap *= 2; T[] n = new T[_cap]; Array.Copy(_items, n, Count); _items = n; }
    public static stackN<T> operator +(stackN<T> s, T x) { s.Push(x); return s; }
    public static stackN<T> operator --(stackN<T> s) { s.Pop(); return s; }
    public static T operator ~(stackN<T> s) => s.Pop();
    public override string ToString()
    {
        if (IsEmpty) return "[]"; T[] r = new T[Count]; Array.Copy(_items, r, Count); Array.Reverse(r);
        return $"[{string.Join(" -> ", r.Take(5))}{(Count > 5 ? " -> ..." : "")}]";
    }
    public static implicit operator stackN<T>(T[] a) { var s = new stackN<T>(a.Length); foreach (var x in a) s.Push(x); return s; }
}
public class hashSetN<T>
{
    private T[][] _buckets;
    private int[] _counts;
    private int _sz = 101;
    public int Count { get; private set; }
    public hashSetN() { _buckets = new T[_sz][]; _counts = new int[_sz]; }
    private int GetIdx(T x) => Math.Abs(x.GetHashCode() % _sz);
    public bool Add(T x) {
        int i = GetIdx(x); if (_buckets[i] == null) _buckets[i] = new T[2];
        for (int j = 0; j < _counts[i]; j++) if (_buckets[i][j].Equals(x)) return false;
        if (_counts[i] == _buckets[i].Length) { T[] n = new T[_counts[i] * 2]; Array.Copy(_buckets[i], n, _counts[i]); _buckets[i] = n; }
        _buckets[i][_counts[i]++] = x; Count++; return true;
    }
    public bool Remove(T x) {
        int i = GetIdx(x); if (_buckets[i] == null) return false;
        for (int j = 0; j < _counts[i]; j++) if (_buckets[i][j].Equals(x)) {
                for (int k = j; k < _counts[i] - 1; k++) _buckets[i][k] = _buckets[i][k + 1];
                _buckets[i][--_counts[i]] = default; Count--; return true;
            }
        return false;
    }
    public static hashSetN<T> operator +(hashSetN<T> s, T x) { s.Add(x); return s; }
    public static hashSetN<T> operator -(hashSetN<T> s, T x) { s.Remove(x); return s; }
    public override string ToString() {
        if (Count == 0) return "{}"; var r = new T[Math.Min(Count, 10)]; int k = 0;
        for (int i = 0; i < _sz && k < r.Length; i++) for (int j = 0; j < _counts[i] && k < r.Length; j++) r[k++] = _buckets[i][j];
        return $"{{ {string.Join(" | ", r)} }}";
    }
    public static implicit operator hashSetN<T>(T[] a) { var s = new hashSetN<T>(); foreach (var x in a) s.Add(x); return s; }
}
public class queueN<T>
{
    private T[] _items;
    private int _head, _tail, _cap;
    public int Count { get; private set; }
    public bool IsEmpty => Count == 0;
    public queueN(int c = 4) { _cap = c; _items = new T[_cap]; }
    public void Enqueue(T x) {
        if (Count == _cap) Resize();
        _items[_tail] = x;
        _tail = (_tail + 1) % _cap;
        Count++;
    }
    public T Dequeue() {
        if (IsEmpty) return default;
        T x = _items[_head];
        _items[_head] = default;
        _head = (_head + 1) % _cap;
        Count--;
        return x;
    }
    public T Peek() => IsEmpty ? default : _items[_head];
    private void Resize() {
        T[] n = new T[_cap * 2];
        for (int i = 0; i < Count; i++) n[i] = _items[(_head + i) % _cap];
        _items = n; _head = 0; _tail = Count; _cap *= 2;
    }
    public static queueN<T> operator +(queueN<T> q, T x) { q.Enqueue(x); return q; }
    public static T operator ~(queueN<T> q) => q.Dequeue();
    public override string ToString() {
        if (IsEmpty) return "[]";
        var r = new T[Math.Min(Count, 5)];
        for (int i = 0; i < r.Length; i++) r[i] = _items[(_head + i) % _cap];
        return $"[{string.Join(" <- ", r)}{(Count > 5 ? " <- ..." : "")}]";
    }
    public static implicit operator queueN<T>(T[] a) {
        var q = new queueN<T>(a.Length);
        foreach (var x in a) q.Enqueue(x);
        return q;
    }
}


public static unsafe class NCore
{
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteConsoleW(IntPtr hConsoleOutput, void* lpBuffer, uint nNumberOfCharsToWrite, out uint lpNumberOfCharsWritten, void* lpReserved);
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetConsoleTextAttribute(IntPtr hConsoleOutput, ushort wAttributes);
    private static readonly intN STD_OUT = (intN)(-11);
    private static readonly IntPtr _hOut = GetStdHandle((int)STD_OUT);
    public static readonly ushort RED = 0x0004;
    public static readonly ushort GREEN = 0x000A;
    public static readonly ushort GRAY = 0x0007;
    public static void SetColor(ushort color) => SetConsoleTextAttribute(_hOut, color);
    public static void WriteRaw(char* buffer, intN length) => WriteConsoleW(_hOut, buffer, (uint)(int)length, out _, null);
    public static void WriteRaw(string s, intN length) { fixed (char* ptr = s) WriteConsoleW(_hOut, ptr, (uint)(int)length, out _, null); }
    public static void Print(charN val, bool newLine = true) {
        char* buf = stackalloc char[2];
        buf[0] = (char)val;
        intN len = 1;
        if (newLine) { buf[1] = '\n'; len = 2; }
        WriteRaw(buf, len);
    }
    public static void Print(intN val, bool newLine = true) {
        char* buf = stackalloc char[12];
        intN v = val, p = 0;
        if (v == 0) buf[p++] = '0';
        else {
            if (v < 0) { buf[p++] = '-'; v = (intN)0 - v; }
            char* t = stackalloc char[10];
            intN tp = 0;
            while (v > 0) { t[tp++] = (charN)(char)('0' + (int)(v % (intN)10)); v /= (intN)10; }
            while (tp > 0) buf[p++] = (char)t[--tp];
        }
        if (newLine) buf[p++] = '\n';
        WriteRaw(buf, p);
    }
    public static void Print(doubleN val, bool newLine = true) {
        Span<char> s = stackalloc char[32];
        if (val.TryFormat(s, out int c, default, null)) {
            fixed (char* ptr = s) {
                if (newLine) ptr[c++] = '\n';
                WriteRaw(ptr, (intN)c);
            }
        }
    }
    public static void PrintHex(intN val, bool newLine = true) {
        intN l = newLine ? (intN)11 : (intN)10;
        char* b = stackalloc char[(int)l];
        val.WriteHex(b);
        if (newLine) b[10] = '\n';
        WriteRaw(b, l);
    }
    public static void PrintRawBits(doubleN val) {
        long bits = val.ToRawBits();
        intN h = (intN)(int)(bits >> 32);
        intN lw = (intN)(int)(bits & 0xFFFFFFFF);
        char* b = stackalloc char[22];
        h.WriteHex(b);
        lw.WriteHex(b + 10);
        b[10] = '_';
        b[21] = '\n';
        WriteRaw(b, (intN)22);
    }
}