using System;
using System.Collections.Generic;
using System.Linq;

namespace Functions_Storage;

public class stringN
{
    private char[] _v;
    public int Length { get; private set; }

    public stringN(string s)
    {
        if (s == null) { _v = new char[0]; Length = 0; return; }
        Length = s.Length;
        _v = new char[Length];
        for (int i = 0; i < Length; i++) _v[i] = s[i];
    }

    public bool IsPalindrome
    {
        get
        {
            for (int i = 0; i < Length / 2; i++)
                if (_v[i] != _v[Length - 1 - i]) return false;
            return true;
        }
    }

    public stringN Reverse()
    {
        char[] a = new char[Length];
        for (int i = 0; i < Length; i++) a[i] = _v[Length - 1 - i];
        return new stringN(new string(a));
    }

    public override string ToString() => new string(_v);
    public static implicit operator stringN(string s) => new stringN(s);
    public static stringN operator +(stringN a, stringN b)
    {
        char[] res = new char[a.Length + b.Length];
        for (int i = 0; i < a.Length; i++) res[i] = a._v[i];
        for (int i = 0; i < b.Length; i++) res[a.Length + i] = b._v[i];
        return new stringN(new string(res));
    }
}
public class vectorN
{
    private double[] _d;
    public int Size => _d.Length;
    public vectorN(params double[] v) => _d = v;
    public double this[int i] { get => _d[i]; set => _d[i] = value; }

    public double Dot(vectorN o)
    {
        double r = 0;
        for (int i = 0; i < Size; i++) r += _d[i] * o._d[i];
        return r;
    }

    public static vectorN operator +(vectorN a, vectorN b)
    {
        double[] r = new double[a.Size];
        for (int i = 0; i < a.Size; i++) r[i] = a[i] + b[i];
        return new vectorN(r);
    }

    public static vectorN operator *(vectorN a, double s)
    {
        double[] r = new double[a.Size];
        for (int i = 0; i < a.Size; i++) r[i] = a[i] * s;
        return new vectorN(r);
    }

    public override string ToString()
    {
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