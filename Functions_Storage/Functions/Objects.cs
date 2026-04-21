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
public class listN<T> : List<T>
{
    public listN() : base() { }
    public listN(IEnumerable<T> collection, bool shuffle = false)
        : base(shuffle ? collection.OrderBy(x => Guid.NewGuid()) : collection) { }
    public T RandomItem() {
        if (this.Count == 0) return default;
        return this[new Random().Next(0, this.Count)];
    }
    public listN<T> Shuffle() => new listN<T>(this, true);
    public override string ToString() {
        string content = string.Join(", ", this);
        return $"{content}";
    }
    public static implicit operator listN<T>(T[] array) => new listN<T>(array, true);
}