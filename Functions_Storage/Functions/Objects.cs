using System;
using System.Collections.Generic;
using System.Linq;

namespace Functions_Storage;

public class stringN
{
    private string _value;
    public stringN(string value) => _value = value ?? "";
    public int Length => _value.Length;
    public int Words => StringUtils.CountWords(_value);
    public bool IsPalindrome => StringUtils.IsPalindrome(_value);
    public bool IsNumeric => StringUtils.IsNumeric(_value);
    public stringN Reverse() => new stringN(StringUtils.Reverse(_value));
    public stringN Capitalize() => new stringN(StringUtils.Capitalize(_value));
    public stringN Sanitize() => new stringN(StringUtils.Sanitize(_value));
    public stringN Truncate(int max) => new stringN(StringUtils.Truncate(_value, max));
    public override string ToString() => _value;
    public static implicit operator stringN(string s) => new stringN(s);
    public static implicit operator string(stringN sn) => sn._value;
    public static stringN operator +(stringN a, stringN b) => new stringN(a._value + b._value);
}
public class vectorN
{
    private double[] _data;
    public vectorN(params double[] values) => _data = values;
    public vectorN(IEnumerable<double> values) => _data = values.ToArray();
    public double this[int index] {
        get => _data[index];
        set => _data[index] = value;
    }
    public int Size => _data.Length;
    public double Max => ArrayUtils.GetMax(_data);
    public double Average => ArrayUtils.GetAverage(_data);
    public vectorN Normalize() => new vectorN(AiUtils.Normalize(_data));
    public vectorN Standardize() => new vectorN(AiUtils.Standardize(_data));
    public vectorN Softmax() => new vectorN(AiUtils.Softmax(_data));
    public double Dot(vectorN other) => AiUtils.DotProduct(this._data, other._data);
    public double DistanceTo(vectorN other) => AiUtils.EuclideanDistance(this._data, other._data);
    public static vectorN operator +(vectorN a, vectorN b) {
        if (a.Size != b.Size) throw new ArgumentException("Розміри векторів мають збігатися");
        return new vectorN(a._data.Zip(b._data, (x, y) => x + y));
    }
    public static vectorN operator *(vectorN a, double scalar)
        => new vectorN(a._data.Select(x => x * scalar));
    public double[] ToArray() => (double[])_data.Clone();
    public override string ToString() => $"vectorN[{Size}] {{ {string.Join(", ", _data.Select(d => d.ToString("F2")))} }}";
}
public class fileN
{
    public string Path { get; }
    public fileN(string path) => Path = path;
    public bool Exists => File.Exists(Path);
    public string Size => FileUtils.GetFriendlyFileSize(Path);
    public string Hash => FileUtils.GetFileHash(Path);
    public bool IsLocked => FileUtils.IsFileLocked(Path);
    public DateTime LastModified => FileUtils.GetLastModified(Path);
    public void Write(string content) => FileUtils.SmartWrite(Path, content);
    public string Read() => File.ReadAllText(Path);
    public void Backup() => FileUtils.BackupFile(Path);
    public void Delete() => File.Delete(Path);
    public void Rename(string newPattern, string replacement) => FileUtils.BatchRename(System.IO.Path.GetDirectoryName(Path), newPattern, replacement);
    public stringN ToSmartString() => new stringN(Read());
    public override string ToString() => $"{System.IO.Path.GetFileName(Path)} ({Size})";
}
public class timeN
{
    private DateTime _dt;
    public timeN(DateTime dt) => _dt = dt;
    public static timeN Now => new timeN(DateTime.Now);
    public bool IsWeekend => TimeUtils.IsWeekend(_dt);
    public bool IsLeap => TimeUtils.IsLeapYear(_dt.Year);
    public string Relative => TimeUtils.GetTimeAgo(_dt); // "5 minutes ago"
    public long Unix => TimeUtils.DateTimeToUnixTimestamp(_dt);
    public int Quarter => TimeUtils.GetQuarter(_dt);
    public timeN Next(DayOfWeek day) => new timeN(TimeUtils.GetNextOccurrence(day));
    public override string ToString() => _dt.ToString("yyyy-MM-dd HH:mm:ss");
    public static implicit operator DateTime(timeN tn) => tn._dt;
    public static implicit operator timeN(DateTime dt) => new timeN(dt);
}
public class hostN
{
    public string Address { get; }
    public hostN(string address) => Address = address;
    public bool IsOnline => NetworkUtils.PingHost(Address);
    public string LocalIp => NetworkUtils.GetLocalIpAddress();
    public string MacAddress => NetworkUtils.GetMacAddress();
    public long Speed => NetworkUtils.GetNetworkSpeed();
    public bool IsPortOpen(int port) => NetworkUtils.IsPortOpen(Address, port, 500);
    public bool IsReachable => NetworkUtils.IsUrlReachable(Address);
    public void Wake() => NetworkUtils.WakeOnLan(Address);
    public override string ToString() => $"{Address} [{(IsOnline ? "ONLINE" : "OFFLINE")}]";
}
public class boolN
{
    private bool _state;
    public boolN(bool state) => _state = state;
    public string Status => _state ? "ACTIVE" : "DISABLED";
    public string Icon => _state ? "✅" : "❌";
    public boolN Toggle() { _state = !_state; return this; }
    public override string ToString() => Status;
    public static implicit operator bool(boolN bn) => bn._state;
    public static implicit operator boolN(bool b) => new boolN(b);
}
public class ifN
{
    private bool _condition;
    private bool _executed = false;
    private ifN(bool condition) => _condition = condition;
    public static ifN That(bool condition) => new ifN(condition);
    public ifN Then(Action action) {
        if (_condition) {
            action();
            _executed = true;
        }
        return this;
    }
    public void Else(Action action) {
        if (!_condition && !_executed) {
            action();
        }
    }
}
public static class forN
{
    public static void Between(int start, int end, Action<int> action) {
        for (int i = start; i < end; i++) {
            action(i);
        }
    }
    public static void Each<T>(T[] items, Action<T> action)
    {
        foreach (var item in items) {
            action(item);
        }
    }
}
public static class whileN
{
    public static void Cycle(Func<bool> condition, Action action) {
        while (condition()) {
            action();
        }
    }
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
    private int _capacity;
    public int Count { get; private set; }
    public listN(int initialCapacity = 4)
    {
        _capacity = initialCapacity;
        _items = new T[_capacity];
    }
    public listN(IEnumerable<T> collection, bool shuffle = false)
    {
        T[] temp = collection.ToArray();
        if (shuffle) temp = temp.OrderBy(x => Guid.NewGuid()).ToArray();
        _capacity = temp.Length > 0 ? temp.Length : 4;
        _items = new T[_capacity];
        foreach (var item in temp) Add(item);
    }
    public T this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }
    public void Add(T item)
    {
        if (Count == _capacity) Resize();
        _items[Count++] = item;
    }
    private void Resize()
    {
        _capacity = _capacity == 0 ? 4 : _capacity * 2;
        T[] newArray = new T[_capacity];
        Array.Copy(_items, newArray, Count);
        _items = newArray;
    }
    public T RandomItem() => Count == 0 ? default : _items[new Random().Next(0, Count)];
    public listN<T> Shuffle() => new listN<T>(this.ToArray(), true);
    public T[] ToArray()
    {
        T[] res = new T[Count];
        Array.Copy(_items, res, Count);
        return res;
    }
    public static listN<T> operator +(listN<T> a, listN<T> b)
    {
        var res = new listN<T>(a.ToArray());
        foreach (var item in b.ToArray()) res.Add(item);
        return res;
    }
    public override string ToString() => Count == 0 ? "[]" : string.Join(", ", ToArray());
    public static implicit operator listN<T>(T[] array) => new listN<T>(array, true);
}
public class stackN<T>
{
    private List<T> _items = new List<T>();
    public int Count => _items.Count;
    public bool IsEmpty => _items.Count == 0;
    public void Push(T item) => _items.Add(item);
    public T Pop() {
        if (IsEmpty) return default;
        T item = _items[_items.Count - 1];
        _items.RemoveAt(_items.Count - 1);
        return item;
    }
    public T Peek() => IsEmpty ? default : _items[_items.Count - 1];
    public void Clear() => _items.Clear();
    public static stackN<T> operator +(stackN<T> s, T item) {
        s.Push(item);
        return s;
    }
    public static stackN<T> operator --(stackN<T> s) {
        s.Pop();
        return s;
    }
    public static T operator ~(stackN<T> s) => s.Pop();
    public override string ToString() {
        if (IsEmpty) return "[ Стек порожній ]";
        var display = new List<T>(_items);
        display.Reverse();
        string content = string.Join(" -> ", display.Take(5));
        string suffix = display.Count > 5 ? " -> ..." : "";
        return $"[ {content}{suffix} ]";
    }
    public static implicit operator stackN<T>(T[] array) {
        var s = new stackN<T>();
        foreach (var item in array) s.Push(item);
        return s;
    }
}
public class hashSetN<T>
{
    private List<T>[] _buckets;
    private int _size = 101;
    public int Count { get; private set; }
    public hashSetN() {
        _buckets = new List<T>[_size];
    }
    private int GetBucketIndex(T item) {
        int hash = item.GetHashCode();
        return Math.Abs(hash % _size);
    }
    public bool Add(T item) {
        int index = GetBucketIndex(item);
        if (_buckets[index] == null) _buckets[index] = new List<T>();
        if (_buckets[index].Contains(item)) return false;
        _buckets[index].Add(item);
        Count++;
        return true;
    }
    public bool Contains(T item) {
        int index = GetBucketIndex(item);
        return _buckets[index] != null && _buckets[index].Contains(item);
    }
    public static hashSetN<T> operator +(hashSetN<T> s, T item) {
        s.Add(item);
        return s;
    }
    public static hashSetN<T> operator -(hashSetN<T> s, T item) {
        int index = s.GetBucketIndex(item);
        if (s._buckets[index] != null && s._buckets[index].Remove(item)) s.Count--;
        return s;
    }
    public override string ToString() {
        var allItems = new List<T>();
        foreach (var bucket in _buckets)
            if (bucket != null) allItems.AddRange(bucket);
        return $"{{ {string.Join(" | ", allItems.Take(10))} }}";
    }
    public static implicit operator hashSetN<T>(T[] array) {
        var set = new hashSetN<T>();
        foreach (var item in array) set.Add(item);
        return set;
    }
}