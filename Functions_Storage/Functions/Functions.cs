using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Functions_Storage;
public static class ArrayUtils
{
    public static double GetMax(double[] array)
    {
        if (array == null || array.Length == 0) {
            throw new ArgumentException("Array is empty!");
        }
        double max = array[0];
        for (int i = 1; i < array.Length; i++) {
            if (array[i] > max) {
                max = array[i];
            }
        }

        return max;
    }
    public static void CustomSort(int[] array)
    {
        if (array == null || array.Length <= 1) return;
        int n = array.Length;
        for (int i = 0; i < n - 1; i++) {
            for (int j = 0; j < n - i - 1; j++) {
                if (array[j] > array[j + 1]) {
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }
    public static T[] Filter<T>(T[] array, Func<T, bool> predicate) {
        if (array == null || predicate == null) return Array.Empty<T>();
        List<T> result = new List<T>();
        foreach (T item in array) {
            if (predicate(item)) {
                result.Add(item);
            }
        }
        return result.ToArray();
    }
    public static void Shuffle<T>(T[] array)
    {
        if (array == null || array.Length <= 1) return;
        Random rnd = new Random();
        int n = array.Length;
        for (int i = n - 1; i > 0; i--) {
            int j = rnd.Next(0, i + 1);
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
    public static double GetAverage(double[] array)
    {
        if (array == null || array.Length == 0) { return 0; }
        double sum = 0;
        foreach (double number in array) { sum += number; }
        return sum / array.Length;
    }
    public static int FindIndex<T>(T[] array, T value)
    {
        if (array == null) return -1;

        for (int i = 0; i < array.Length; i++) {
            if (Equals(array[i], value)) { return i; }
        }
        return -1; // Пройшли весь масив і нічого не знайшли
    }
    public static bool Contains<T>(T[] array, T value)
    {
        return FindIndex(array, value) != -1;
    }
    public static void Reverse<T>(T[] array)
    {
        if (array == null || array.Length <= 1) return;
        int left = 0;
        int right = array.Length - 1;
        while (left < right) {
            T temp = array[left];
            array[left] = array[right];
            array[right] = temp;
            left++;
            right--;
        }
    }
    public static T[] Merge<T>(T[] array1, T[] array2) {
        if (array1 == null) return array2 ?? Array.Empty<T>();
        if (array2 == null) return array1;
        T[] result = new T[array1.Length + array2.Length];
        Array.Copy(array1, 0, result, 0, array1.Length);
        Array.Copy(array2, 0, result, array1.Length, array2.Length);
        return result;
    }
    public static void Fill<T>(T[] array, T value)
    {
        if (array == null) return;
        for (int i = 0; i < array.Length; i++) {
            array[i] = value;
        }
    }
    public static T GetRandomElement<T>(T[] array)
    {
        if (array == null || array.Length == 0)
            throw new ArgumentException("Масив порожній.");
        Random rnd = new Random();
        return array[rnd.Next(0, array.Length)];
    }
    public static bool AreEqual<T>(T[] array1, T[] array2)
    {
        if (ReferenceEquals(array1, array2)) return true;
        if (array1 == null || array2 == null) return false;
        if (array1.Length != array2.Length) return false;
        for (int i = 0; i < array1.Length; i++) {
            if (!Equals(array1[i], array2[i])) return false;
        }
        return true;
    }
}
public static class MathUtils
{
    public static bool IsEven(int number)
    {
        return number % 2 == 0;
    }
    public static long Factorial(int n)
    {
        if (n < 0) throw new ArgumentException("Число має бути невід'ємним.");
        long result = 1;
        for (int i = 2; i <= n; i++) {
            result *= i;
        }
        return result;
    }
    public static double GetPercentage(double value, double percentage)
    {
        return (value * percentage) / 100.0;
    }
    public static int GetRandomInt(int min, int max)
    {
        if (min > max) throw new ArgumentException("Min is bigger than Max!");
        Random rnd = new Random();
        return rnd.Next(min, max + 1);
    }
    public static double Clamp(double value, double min, double max)
    {
        if (min > max) throw new ArgumentException("Min is bigger than Max!");
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
    public static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;
        int boundary = (int)Math.Sqrt(number);
        for (int i = 3; i <= boundary; i += 2){
            if (number % i == 0) return false;
        }
        return true;
    }
    public static double Round(double value, int digits)
    {
        return Math.Round(value, digits);
    }
    public static double Lerp(double start, double end, double amount)
    {
        if (amount < 0) amount = 0;
        if (amount > 1) amount = 1;
        return start + (end - start) * amount;
    }
    public static int GetGCD(int a, int b)
    {
        a = Math.Abs(a);
        b = Math.Abs(b);
        while (b != 0) {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
    public static int GetLCM(int a, int b)
    {
        if (a == 0 || b == 0) return 0;
        return Math.Abs(a * b) / GetGCD(a, b);
    }
    public static bool IsPowerOfTwo(int n)
    {
        return n > 0 && (n & (n - 1)) == 0;
    }
    public static double Map(double value, double fromSource, double toSource, double fromTarget, double toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }
    public static double GetRandomDouble()
    {
        Random rnd = new Random();
        return rnd.NextDouble();
    }
    public static double GetHypotenuse(double a, double b)
    {
        return Math.Sqrt(a * a + b * b);
    }
}
public static class StringUtils
{
    public static string Reverse(string text)
    {
        if (string.IsNullOrEmpty(text)) return text;
        char[] charArray = text.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
    public static int CountWords(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return 0;
        return text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }
    public static bool IsPalindrome(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return false;
        string cleaned = text.Replace(" ", "").ToLower();
        string reversed = Reverse(cleaned);
        return cleaned == reversed;
    }
    public static string Capitalize(string text)
    {
        if (string.IsNullOrEmpty(text)) return text;
        return char.ToUpper(text[0]) + text.Substring(1).ToLower();
    }
    public static string RemoveWhitespace(string text)
    {
        if (string.IsNullOrEmpty(text)) return text;
        return new string(text.Where(c => !char.IsWhiteSpace(c)).ToArray());
    }
    public static string Truncate(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text) || text.Length <= maxLength) return text;
        return text.Substring(0, maxLength) + "...";
    }
    public static string Sanitize(string text)
    {
        if (string.IsNullOrEmpty(text)) return text;
        string result = text.Trim();
        return System.Text.RegularExpressions.Regex.Replace(result, "<.*?>", string.Empty);
    }
    public static bool IsNumeric(string text)
    {
        if (string.IsNullOrEmpty(text)) return false;
        return text.All(char.IsDigit);
    }
}
public static class NetworkUtils
{
    public static bool PingHost(string ipAddress)
    {
        try {
            using (Ping ping = new Ping()) {
                PingReply reply = ping.Send(ipAddress, 1000);
                return reply.Status == IPStatus.Success;
            }
        }
        catch { return false; }
    }
    public static string GetLocalIpAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList) {
            if (ip.AddressFamily == AddressFamily.InterNetwork) {
                return ip.ToString();
            }
        }
        return "127.0.0.1";
    }
    public static string GetPublicIpAddress()
    {
        try {
            using (HttpClient client = new HttpClient()) {
                return client.GetStringAsync("https://api.ipify.org").Result;
            }
        }
        catch { return "Unknown"; }
    }
    public static bool IsPortOpen(string host, int port, int timeoutMs)
    {
        try {
            using (TcpClient client = new TcpClient()) {
                var result = client.BeginConnect(host, port, null, null);
                bool success = result.AsyncWaitHandle.WaitOne(timeoutMs);
                return success && client.Connected;
            }
        }
        catch { return false; }
    }
    public static string GetMacAddress()
    {
        return NetworkInterface.GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(nic => nic.GetPhysicalAddress().ToString())
            .FirstOrDefault() ?? "00:00:00:00:00:00";
    }
    public static bool IsWifiConnected()
    {
        return NetworkInterface.GetAllNetworkInterfaces()
            .Any(nic => nic.OperationalStatus == OperationalStatus.Up &&
                        nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211);
    }
    public static long GetNetworkSpeed()
    {
        return NetworkInterface.GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up)
            .Select(nic => nic.Speed)
            .FirstOrDefault();
    }
    public static string ResolveDns(string hostName)
    {
        try {
            var addresses = Dns.GetHostAddresses(hostName);
            return addresses.FirstOrDefault()?.ToString() ?? "Not Resolved";
        }
        catch { return "Error"; }
    }
    public static bool IsUrlReachable(string url)
    {
        try {
            using (HttpClient client = new HttpClient()) {
                client.Timeout = TimeSpan.FromSeconds(5);
                var response = client.GetAsync(url).Result;
                return response.IsSuccessStatusCode;
            }
        }
        catch { return false; }
    }
    public static void WakeOnLan(string macAddress)
    {
        byte[] magicPacket = new byte[102];
        for (int i = 0; i < 6; i++) magicPacket[i] = 0xff;
        byte[] macBytes = Enumerable.Range(0, 6)
            .Select(i => Convert.ToByte(macAddress.Replace(":", "").Replace("-", "").Substring(i * 2, 2), 16))
            .ToArray();
        for (int i = 1; i <= 16; i++)
            Array.Copy(macBytes, 0, magicPacket, i * 6, 6);

        using (UdpClient client = new UdpClient()) {
            client.Connect(IPAddress.Broadcast, 9);
            client.Send(magicPacket, magicPacket.Length);
        }
    }
}
public static class SecurityUtils
{
    public static string HashString(string input)
    {
        using (SHA256 sha256 = SHA256.Create()) {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes);
        }
    }
    public static string GeneratePassword(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-=";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
    }
    public static string Encrypt(string plainText, string key)
    {
        byte[] iv = new byte[16];
        byte[] array;
        using (Aes aes = Aes.Create()) {
            aes.Key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(key));
            aes.IV = iv;
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (MemoryStream memoryStream = new MemoryStream()) {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)) {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream)) {
                        streamWriter.Write(plainText);
                    }
                    array = memoryStream.ToArray();
                }
            }
        }
        return Convert.ToBase64String(array);
    }
    public static int GetSecureRandomInt(int min, int max)
    {
        return RandomNumberGenerator.GetInt32(min, max + 1);
    }
    public static string GenerateToken(int length = 32)
    {
        byte[] randomNumber = new byte[length];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create()) {
            rng.GetBytes(randomNumber);
        }
        return Convert.ToBase64String(randomNumber).Substring(0, length);
    }
    public static byte[] GenerateSaltBytes(int length = 16)
    {
        byte[] salt = new byte[length];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create()) {
            rng.GetBytes(salt);
        }
        return salt;
    }
    public static int CheckPasswordStrength(string password) {
        if (string.IsNullOrEmpty(password)) return 0;
        int score = 0;
        if (password.Length >= 8) score++;
        if (password.Length >= 12) score++;
        if (password.Any(char.IsUpper) && password.Any(char.IsLower)) score++;
        if (password.Any(char.IsDigit)) score++;
        if (password.Any(c => !char.IsLetterOrDigit(c))) score++;
        return score;
    }
}
public static class FileUtils
{
    public static void SmartWrite(string path, string content)
    {
        EnsureDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path, content);
    }
    public static string GetFriendlyFileSize(string path)
    {
        if (!File.Exists(path)) return "0 B";
        long bytes = new FileInfo(path).Length;
        string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
        int counter = 0;
        decimal number = bytes;
        while (Math.Round(number / 1024) >= 1) {
            number /= 1024;
            counter++;
        }
        return $"{number:n1} {suffixes[counter]}";
    }
    public static bool BackupFile(string path)
    {
        if (!File.Exists(path)) return false;
        try {
            string backupPath = path + ".bak";
            File.Copy(path, backupPath, true);
            return true;
        }
        catch { return false; }
    }
    public static void EnsureDirectory(string path)
    {
        if (!string.IsNullOrEmpty(path) && !Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }
    }
    public static string[] GetFilesByExtension(string directory, string extension)
    {
        if (!Directory.Exists(directory)) return Array.Empty<string>();
        string searchPattern = $"*.{extension.TrimStart('.')}";
        return Directory.GetFiles(directory, searchPattern);
    }
    public static string GetFileHash(string path)
    {
        if (!File.Exists(path)) return string.Empty;
        using (var sha256 = SHA256.Create()) {
            using (var stream = File.OpenRead(path)) {
                byte[] hash = sha256.ComputeHash(stream);
                return Convert.ToHexString(hash);
            }
        }
    }
    public static DateTime GetLastModified(string path)
    {
        return File.Exists(path) ? File.GetLastWriteTime(path) : DateTime.MinValue;
    }
    public static bool IsFileLocked(string path)
    {
        if (!File.Exists(path)) return false;
        try {
            using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None)) {
                stream.Close();
            }
        }
        catch (IOException) { return true; }
        return false;
    }
    public static void BatchRename(string directory, string pattern, string replacement)
    {
        if (!Directory.Exists(directory)) return;
        var files = Directory.GetFiles(directory);
        foreach (var file in files) {
            string fileName = Path.GetFileName(file);
            if (fileName.Contains(pattern)) {
                string newFileName = fileName.Replace(pattern, replacement);
                string newPath = Path.Combine(directory, newFileName);
                File.Move(file, newPath);
            }
        }
    }
}
public static class ColorUtils
{
    public static void WriteColored(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    public static void WriteError(string message)
    {
        WriteColored($"[ERROR] {message}", ConsoleColor.Red);
    }
    public static void WriteSuccess(string message)
    {
        WriteColored($"[SUCCESS] {message}", ConsoleColor.Green);
    }
    public static void WriteWarning(string message)
    {
        WriteColored($"[WARNING] {message}", ConsoleColor.Yellow);
    }
    public static void WriteInfo(string message)
    {
        WriteColored($"[INFO] {message}", ConsoleColor.Cyan);
    }
    public static void WriteRainbow(string message)
    {
        ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Magenta };
        for (int i = 0; i < message.Length; i++) {
            Console.ForegroundColor = colors[i % colors.Length];
            Console.Write(message[i]);
        }
        Console.WriteLine();
        Console.ResetColor();
    }
    public static void HighlightText(string text, string target, ConsoleColor highlightColor)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(target)) {
            Console.WriteLine(text);
            return;
        }
        string[] parts = text.Split(new[] { target }, StringSplitOptions.None);
        for (int i = 0; i < parts.Length; i++) {
            Console.Write(parts[i]);
            if (i < parts.Length - 1) {
                Console.ForegroundColor = highlightColor;
                Console.Write(target);
                Console.ResetColor();
            }
        }
        Console.WriteLine();
    }
    public static void PrintGradient(string text, ConsoleColor startColor, ConsoleColor endColor)
    {
        for (int i = 0; i < text.Length; i++) {
            Console.ForegroundColor = (i % 2 == 0) ? startColor : endColor;
            Console.Write(text[i]);
        }
        Console.WriteLine();
        Console.ResetColor();
    }
    public static ConsoleColor GetRandomColor()
    {
        Array values = Enum.GetValues(typeof(ConsoleColor));
        return (ConsoleColor)values.GetValue(Random.Shared.Next(1, values.Length));
    }
    public static void ResetColor()
    {
        Console.ResetColor();
    }
}
public static class TimeUtils
{
    public static string GetTimestamp()
    {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
    public static int GetAge(DateTime birthDate)
    {
        DateTime today = DateTime.Today;
        int age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age)) {
            age--;
        }
        return age;
    }
    public static string FormatTimeSpan(TimeSpan duration)
    {
        return $"{duration.Days}d {duration.Hours}h {duration.Minutes}m {duration.Seconds}s";
    }
    public static bool IsWeekend(DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
    }
    public static DateTime GetNextOccurrence(DayOfWeek day)
    {
        int daysToAdd = ((int)day - (int)DateTime.Today.DayOfWeek + 7) % 7;
        if (daysToAdd == 0) {
            daysToAdd = 7;
        }
        return DateTime.Today.AddDays(daysToAdd);
    }
    public static bool IsLeapYear(int year)
    {
        return DateTime.IsLeapYear(year);
    }
    public static int GetDaysInMonth(int year, int month)
    {
        return DateTime.DaysInMonth(year, month);
    }
    public static string GetTimeAgo(DateTime dateTime)
    {
        TimeSpan span = DateTime.Now - dateTime;
        if (span.TotalMinutes < 1) {
            return "just now";
        }
        if (span.TotalMinutes < 60) {
            return $"{(int)span.TotalMinutes} minutes ago";
        }
        if (span.TotalHours < 24) {
            return $"{(int)span.TotalHours} hours ago";
        }
        if (span.TotalDays < 30) {
            return $"{(int)span.TotalDays} days ago";
        }
        return dateTime.ToString("yyyy-MM-dd");
    }
    public static DateTime UnixTimestampToDateTime(long unixTime)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTime).DateTime;
    }
    public static long DateTimeToUnixTimestamp(DateTime dateTime)
    {
        return ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
    }
    public static int GetQuarter(DateTime date)
    {
        return (date.Month + 2) / 3;
    }
}
public static class ValidationUtils
{
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) {
            return false;
        }
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
    }
    public static bool IsValidPhoneNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number)) {
            return false;
        }
        return Regex.IsMatch(number, @"^\+?[1-9]\d{1,14}$");
    }
    public static bool IsSecurePassword(string password)
    {
        if (string.IsNullOrEmpty(password)) {
            return false;
        }
        return password.Length >= 8 &&
               password.Any(char.IsUpper) &&
               password.Any(char.IsLower) &&
               password.Any(char.IsDigit);
    }
    public static bool IsOnlyLetters(string text)
    {
        return !string.IsNullOrEmpty(text) && text.All(char.IsLetter);
    }
    public static bool IsValidBirthDate(DateTime date)
    {
        int year = date.Year;
        return year > 1900 && date < DateTime.Now;
    }
    public static bool IsUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
public static class ConverterUtils
{
    public static double CelsiusToFahrenheit(double celsius)
    {
        return (celsius * 9 / 5) + 32;
    }
    public static string BytesToReadable(long bytes)
    {
        string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
        int counter = 0;
        decimal number = bytes;
        while (Math.Round(number / 1024) >= 1) {
            number /= 1024;
            counter++;
        }
        return $"{number:n1} {suffixes[counter]}";
    }
    public static int StringToInt(string value, int defaultValue = 0)
    {
        return int.TryParse(value, out int result) ? result : defaultValue;
    }
    public static string ToJson<T>(T obj)
    {
        return JsonSerializer.Serialize(obj);
    }
    public static T FromJson<T>(string json)
    {
        try {
            return JsonSerializer.Deserialize<T>(json);
        }
        catch { return default; }
    }
    public static double FahrenheitToCelsius(double fahrenheit)
    {
        return (fahrenheit - 32) * 5 / 9;
    }
    public static double KilometersToMiles(double km)
    {
        return km * 0.621371;
    }
    public static double MilesToKilometers(double miles)
    {
        return miles / 0.621371;
    }
    public static string ToBase64(string text)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
    }
    public static string FromBase64(string base64)
    {
        try {
            byte[] bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(bytes);
        }
        catch { return string.Empty; }
    }
    public static DateTime StringToDateTime(string value, DateTime defaultValue = default)
    {
        return DateTime.TryParse(value, out DateTime result) ? result : defaultValue;
    }
    public static string ToXml<T>(T obj)
    {
        try {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringWriter textWriter = new StringWriter()) {
                xmlSerializer.Serialize(textWriter, obj);
                return textWriter.ToString();
            }
        }
        catch { return string.Empty; }
    }
    public static double DegreesToRadians(double degrees)
    {
        return degrees * (Math.PI / 180.0);
    }
    public static double RadiansToDegrees(double radians)
    {
        return radians * (180.0 / Math.PI);
    }
}
public static class GameUtils
{
    public static int RollDice(int sides = 6)
    {
        return Random.Shared.Next(1, sides + 1);
    }
    public static bool Chance(double probability)
    {
        return Random.Shared.NextDouble() * 100.0 < probability;
    }
    public static double CalculateDistance(double x1, double y1, double x2, double y2)
    {
        return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
    }
    public static string GenerateRandomName()
    {
        string[] prefixes = { "Thor", "Gland", "El", "Bel", "Nar", "Korg", "Zor" };
        string[] suffixes = { "in", "dar", "ius", "eth", "on", "morn", "ax" };
        return prefixes[Random.Shared.Next(prefixes.Length)] + suffixes[Random.Shared.Next(suffixes.Length)];
    }
    public static bool RollSuccess(int chance)
    {
        return Random.Shared.Next(1, 101) <= chance;
    }
    public static int RollWithAdvantage(int sides = 20)
    {
        return Math.Max(RollDice(sides), RollDice(sides));
    }
    public static int RollWithDisadvantage(int sides = 20)
    {
        return Math.Min(RollDice(sides), RollDice(sides));
    }
    public static double CalculateDamage(double baseDamage, double defense, double multiplier = 1.0)
    {
        double damage = (baseDamage - defense) * multiplier;
        return Math.Max(0, damage);
    }
    public static bool IsCriticalHit(int roll, int criticalThreshold = 20)
    {
        return roll >= criticalThreshold;
    }
    public static string GetRandomLoot(string[] lootTable)
    {
        if (lootTable == null || lootTable.Length == 0) {
            return "Nothing";
        }
        return lootTable[Random.Shared.Next(lootTable.Length)];
    }
    public static double CalculateExperience(int enemyLevel, int playerLevel)
    {
        int diff = enemyLevel - playerLevel;
        return Math.Max(10, 50 + (diff * 15));
    }
    public static void ShakeScreen(int intensity, int duration)
    {
        for (int i = 0; i < intensity; i++) {
            Console.Beep(150 + (i * 40), duration / intensity);
        }
    }
    public static bool IsInRange(double x1, double y1, double x2, double y2, double range)
    {
        return CalculateDistance(x1, y1, x2, y2) <= range;
    }
    public static double DegreesToAngle(double x1, double y1, double x2, double y2)
    {
        return Math.Atan2(y2 - y1, x2 - x1) * (180 / Math.PI);
    }
    public static int GetLevelFromExperience(long experience)
    {
        return (int)(Math.Floor(Math.Sqrt(experience / 100.0)) + 1);
    }
    public static T PickWeightedItem<T>(T[] items, double[] weights)
    {
        if (items.Length != weights.Length) return default;
        double totalWeight = weights.Sum();
        double randomValue = Random.Shared.NextDouble() * totalWeight;
        double currentSum = 0;
        for (int i = 0; i < items.Length; i++) {
            currentSum += weights[i];
            if (randomValue <= currentSum) return items[i];
        }
        return items[0];
    }
    public static void SpawnEntity(string entityId, double x, double y)
    {
        Console.WriteLine($"[ENTITY] {entityId} spawned at X:{x:F1} Y:{y:F1}");
    }
    public static double CalculateManaRegen(double baseRegen, double intelligence)
    {
        return baseRegen + (intelligence * 0.15);
    }
    public static bool CheckLineOfSight(double x1, double y1, double x2, double y2)
    {
        return true;
    }
}
public static class SystemUtils
{
    public static string GetOsVersion()
    {
        return Environment.OSVersion.ToString();
    }
    public static double GetCpuUsage()
    {
        return 0.0;
    }
    public static long GetAvailableRam()
    {
        return GC.GetTotalMemory(false);
    }
    public static void OpenUrl(string url)
    {
        Process.Start(new ProcessStartInfo {
            FileName = url,
            UseShellExecute = true
        });
    }
    public static void CopyToClipboard(string text)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
            Process.Start(new ProcessStartInfo {
                FileName = "powershell",
                Arguments = $"-Command \"Set-Clipboard -Value '{text}'\"",
                CreateNoWindow = true,
                UseShellExecute = false
            });
        }
    }
    public static bool IsRunningAsAdmin()
    {
        using (WindowsIdentity identity = WindowsIdentity.GetCurrent()) {
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
    public static string GetDotNetVersion()
    {
        return Environment.Version.ToString();
    }
    public static string GetUserName()
    {
        return Environment.UserName;
    }
    public static string GetMachineName()
    {
        return Environment.MachineName;
    }
    public static void RestartSystem()
    {
        Process.Start("shutdown", "/r /t 0");
    }
    public static void ShutdownSystem()
    {
        Process.Start("shutdown", "/s /t 0");
    }
    public static void SleepSystem()
    {
        Process.Start("rundll32.exe", "powprof.dll,SetSuspendState 0,1,0");
    }
    public static long GetTotalRam()
    {
        return Environment.WorkingSet;
    }
    public static TimeSpan GetSystemUptime()
    {
        return TimeSpan.FromMilliseconds(Environment.TickCount64);
    }
    public static string GetEnvironmentVariable(string variable)
    {
        return Environment.GetEnvironmentVariable(variable) ?? "Not Found";
    }
    public static void SetEnvironmentVariable(string variable, string value)
    {
        Environment.SetEnvironmentVariable(variable, value);
    }
    public static bool Is64BitOperatingSystem()
    {
        return Environment.Is64BitOperatingSystem;
    }
    public static string GetCurrentProcessPath()
    {
        return Environment.ProcessPath ?? string.Empty;
    }
    public static int GetProcessId()
    {
        return Environment.ProcessId;
    }
    public static void TakeScreenshot(string savePath)
    {
        throw new NotImplementedException("Needs System.Drawing.Common reference");
    }
    public static void SetSystemVolume(int volume)
    {

    }
    public static bool IsScreenLocked()
    {
        return false;
    }
}
public static class ImageUtils
{
    public static void ResizeImage(string path, int width, int height) => throw new NotImplementedException();
    public static string GetImageFormat(string path) => throw new NotImplementedException();
    public static void ApplyGrayscale(string path) => throw new NotImplementedException();
    public static void ApplyBlur(string path, int radius) => throw new NotImplementedException();
    public static void ApplySepia(string path) => throw new NotImplementedException();
    public static void RotateImage(string path, float degrees) => throw new NotImplementedException();
    public static void FlipImage(string path, bool horizontal, bool vertical) => throw new NotImplementedException();
    public static void CropImage(string path, int x, int y, int width, int height) => throw new NotImplementedException();
    public static void AddWatermark(string path, string text, int x, int y) => throw new NotImplementedException();
    public static void CompressImage(string path, int quality) => throw new NotImplementedException();
    public static void ConvertFormat(string sourcePath, string targetFormat) => throw new NotImplementedException();
    public static (int width, int height) GetImageDimensions(string path) => throw new NotImplementedException();
    public static void AdjustBrightness(string path, int level) => throw new NotImplementedException();
    public static void AdjustContrast(string path, int level) => throw new NotImplementedException();
    public static void InvertColors(string path) => throw new NotImplementedException();
    public static string GetDominantColor(string path) => throw new NotImplementedException();
    public static void RemoveExifData(string path) => throw new NotImplementedException();
}
public static class AiUtils
{
    public static double Sigmoid(double x)
    {
        return 1.0 / (1.0 + Math.Exp(-x));
    }
    public static double Relu(double x)
    {
        return Math.Max(0, x);
    }
    public static double Tanh(double x)
    {
        return Math.Tanh(x);
    }
    public static double EuclideanDistance(double[] vectorA, double[] vectorB)
    {
        if (vectorA.Length != vectorB.Length) throw new ArgumentException("Vectors must have the same length");
        double sum = 0;
        for (int i = 0; i < vectorA.Length; i++)
            sum += Math.Pow(vectorA[i] - vectorB[i], 2);
        return Math.Sqrt(sum);
    }
    public static double MeanSquaredError(double[] actual, double[] predicted)
    {
        if (actual.Length != predicted.Length) throw new ArgumentException("Arrays must have the same length");
        double sum = 0;
        for (int i = 0; i < actual.Length; i++)
            sum += Math.Pow(actual[i] - predicted[i], 2);
        return sum / actual.Length;
    }
    public static double CrossEntropy(double[] actual, double[] predicted)
    {
        double sum = 0;
        for (int i = 0; i < actual.Length; i++)
            sum += actual[i] * Math.Log(predicted[i] + 1e-15);
        return -sum;
    }
    public static double CalculateAccuracy(int[] actual, int[] predicted)
    {
        int correct = 0;
        for (int i = 0; i < actual.Length; i++)
            if (actual[i] == predicted[i]) correct++;
        return (double)correct / actual.Length;
    }
    public static double[] Normalize(double[] data)
    {
        double min = data.Min();
        double max = data.Max();
        if (Math.Abs(max - min) < 1e-9) return data;
        return data.Select(x => (x - min) / (max - min)).ToArray();
    }
    public static double[] Standardize(double[] data)
    {
        double avg = data.Average();
        double stdDev = Math.Sqrt(data.Select(x => Math.Pow(x - avg, 2)).Sum() / data.Length);
        if (stdDev < 1e-9) return data;
        return data.Select(x => (x - avg) / stdDev).ToArray();
    }
    public static (T[] train, T[] test) SplitData<T>(T[] data, double ratio = 0.8)
    {
        int trainSize = (int)(data.Length * ratio);
        T[] train = data.Take(trainSize).ToArray();
        T[] test = data.Skip(trainSize).ToArray();
        return (train, test);
    }
    public static double DotProduct(double[] a, double[] b)
    {
        if (a.Length != b.Length) throw new ArgumentException("Lengths must match");
        return a.Zip(b, (x, y) => x * y).Sum();
    }
    public static double[,] MultiplyMatrices(double[,] matrixA, double[,] matrixB)
    {
        int rA = matrixA.GetLength(0), cA = matrixA.GetLength(1);
        int rB = matrixB.GetLength(0), cB = matrixB.GetLength(1);
        if (cA != rB) throw new ArgumentException("Invalid matrix dimensions");
        double[,] result = new double[rA, cB];
        for (int i = 0; i < rA; i++)
            for (int j = 0; j < cB; j++)
                for (int k = 0; k < cA; k++)
                    result[i, j] += matrixA[i, k] * matrixB[k, j];
        return result;
    }
    public static double[,] Transpose(double[,] matrix)
    {
        int rows = matrix.GetLength(0), cols = matrix.GetLength(1);
        double[,] result = new double[cols, rows];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                result[j, i] = matrix[i, j];
        return result;
    }
    public static int GetLevenshteinDistance(string source, string target)
    {
        if (string.IsNullOrEmpty(source)) return target?.Length ?? 0;
        if (string.IsNullOrEmpty(target)) return source.Length;
        int[,] d = new int[source.Length + 1, target.Length + 1];
        for (int i = 0; i <= source.Length; i++) d[i, 0] = i;
        for (int j = 0; j <= target.Length; j++) d[0, j] = j;
        for (int i = 1; i <= source.Length; i++)
            for (int j = 1; j <= target.Length; j++)
            {
                int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;
                d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
            }
        return d[source.Length, target.Length];
    }
    public static string[] GetKeywords(string text, int count)
    {
        if (string.IsNullOrWhiteSpace(text)) return Array.Empty<string>();
        char[] separators = { ' ', '.', ',', '!', '?', ':', ';', '-', '\n', '\r' };
        return text.ToLower().Split(separators, StringSplitOptions.RemoveEmptyEntries)
            .Where(w => w.Length > 3)
            .GroupBy(w => w)
            .OrderByDescending(g => g.Count())
            .Take(count)
            .Select(g => g.Key).ToArray();
    }
    public static double[][] RunCentNN(double[][] data, int mClusters, double epsilonPercent = 0.05)
    {
        if (data == null || data.Length == 0) return Array.Empty<double[]>();
        int dims = data[0].Length;
        double[] meanCenter = new double[dims];
        for (int j = 0; j < dims; j++) meanCenter[j] = data.Average(row => row[j]);

        double epsilon = (data.Sum(d => d.Max() - d.Min()) / dims) * epsilonPercent;
        List<double[]> weights = new List<double[]> { meanCenter };
        Random rnd = new Random();

        while (weights.Count < mClusters)
        {
            for (int epoch = 0; epoch < 50; epoch++)
            {
                int[] nCounts = new int[weights.Count];
                foreach (var x in data)
                {
                    int win = 0; double minDist = double.MaxValue;
                    for (int i = 0; i < weights.Count; i++)
                    {
                        double d = EuclideanDistance(x, weights[i]);
                        if (d < minDist) { minDist = d; win = i; }
                    }
                    nCounts[win]++;
                    double lr = 1.0 / (nCounts[win] + 1);
                    for (int j = 0; j < dims; j++) weights[win][j] += lr * (x[j] - weights[win][j]);
                }
            }
            if (weights.Count >= mClusters) break;
            double[] errors = new double[weights.Count];
            foreach (var x in data)
            {
                int win = 0; double minDist = double.MaxValue;
                for (int i = 0; i < weights.Count; i++)
                {
                    double d = EuclideanDistance(x, weights[i]);
                    if (d < minDist) { minDist = d; win = i; }
                }
                errors[win] += Math.Pow(minDist, 2);
            }
            int worst = Array.IndexOf(errors, errors.Max());
            double[] nw = new double[dims];
            for (int j = 0; j < dims; j++) nw[j] = weights[worst][j] + (rnd.NextDouble() * 2 - 1) * epsilon;
            weights.Add(nw);
        }
        return weights.ToArray();
    }
    public static int[] GetKNearestNeighbors(double[] target, double[][] data, int k)
    {
        int n = data.Length;
        var distances = new (int index, double dist)[n];
        for (int i = 0; i < n; i++)
        {
            distances[i].index = i;
            distances[i].dist = EuclideanDistance(target, data[i]);
        }
        Array.Sort(distances, (a, b) =>
        {
            return a.dist.CompareTo(b.dist);
        });
        int resultCount = Math.Min(k, n);
        int[] neighborIndices = new int[resultCount];
        for (int i = 0; i < resultCount; i++)
        {
            neighborIndices[i] = distances[i].index;
        }
        return neighborIndices;
    }
}
public static class HardwareUtils
{
    public static string GetGpuName() => throw new NotImplementedException();
    public static string GetCpuName() => throw new NotImplementedException();
    public static bool IsBatteryCharging() => throw new NotImplementedException();
    public static string GetMotherboardSerial() => throw new NotImplementedException();
    public static string GetDiskSpaceInfo() => throw new NotImplementedException();
    public static bool IsNetworkConnected() => throw new NotImplementedException();
}
public static class EncryptionUtils
{
    public static string Base64Encode(string text) => throw new NotImplementedException();
    public static string Base64Decode(string base64) => throw new NotImplementedException();
    public static string GenerateSalt(int size = 16) => throw new NotImplementedException();
}
public static class ProcessUtils
{
    public static void KillProcess(string processName) => throw new NotImplementedException();
    public static bool IsProcessRunning(string processName) => throw new NotImplementedException();
    public static void RunShellCommand(string command) => throw new NotImplementedException();
}
public static class CollectionUtils
{
    public static bool IsEmpty<T>(IEnumerable<T> collection) => throw new NotImplementedException();
    public static void ForEach<T>(IEnumerable<T> collection, Action<T> action) => throw new NotImplementedException();
    public static List<T> ToUniqueList<T>(IEnumerable<T> collection) => throw new NotImplementedException();
    public static T PickRandom<T>(IEnumerable<T> collection) => throw new NotImplementedException();
    public static IEnumerable<IEnumerable<T>> Chunk<T>(IEnumerable<T> collection, int size) => throw new NotImplementedException();
    public static double GetMedian(double[] array) => throw new NotImplementedException();
    public static T GetMode<T>(T[] array) => throw new NotImplementedException();
}
public static class DebugUtils
{
    public static void LogWithTime(string message) => throw new NotImplementedException();
    public static void DumpObject<T>(T obj) => throw new NotImplementedException();
    public static long MeasureExecutionTime(Action action) => throw new NotImplementedException();
}
public static class HttpUtils
{
    public static string GetRequest(string url) => throw new NotImplementedException();
    public static void DownloadFile(string url, string destinationPath) => throw new NotImplementedException();
    public static bool IsInternetAvailable() => throw new NotImplementedException();
}
public static class ZipUtils
{
    public static void CreateZip(string sourceDirectory, string zipPath) => throw new NotImplementedException();
    public static void ExtractZip(string zipPath, string extractPath) => throw new NotImplementedException();
}
public static class RegexUtils
{
    public static string ExtractEmails(string text) => throw new NotImplementedException();
    public static string RemoveHtmlTags(string html) => throw new NotImplementedException();
    public static bool MatchesPattern(string text, string pattern) => throw new NotImplementedException();
}
public static class DatabaseUtils
{
    public static string BuildConnectionString(string server, string database) => throw new NotImplementedException();
    public static bool TestConnection(string connectionString) => throw new NotImplementedException();
}
public static class ListExtensions
{
    public static void RemoveDuplicates<T>(this List<T> list) => throw new NotImplementedException();
    public static T GetLast<T>(this List<T> list) => throw new NotImplementedException();
}
public static class AudioUtils
{
    public static void PlayBeep(int frequency, int duration) => throw new NotImplementedException();
    public static void PlaySystemSound(string soundName) => throw new NotImplementedException();
}
public static class ConfigUtils
{
    public static void SaveConfig<T>(string path, T config) => throw new NotImplementedException();
    public static T LoadConfig<T>(string path) => throw new NotImplementedException();
    public static string GetAppSetting(string key) => throw new NotImplementedException();
}
public static class ConsoleVisuals
{
    public static void DrawProgressBar(int progress, int total, int width = 20) => throw new NotImplementedException();
    public static void WriteInBox(string text) => throw new NotImplementedException();
    public static void ClearLine(int row) => throw new NotImplementedException();
}
public static class AutomationUtils
{
    public static void SendKeyPress(short keyCode) => throw new NotImplementedException();
    public static void SetMousePosition(int x, int y) => throw new NotImplementedException();
    public static void MinimizeAll() => throw new NotImplementedException();
}
public static class UnitConverter
{
    public static double KmHtoMs(double speed) => throw new NotImplementedException();
    public static double ConvertCurrency(double amount, double rate) => throw new NotImplementedException();
}
public class Value
{
    public double Data { get; set; }
    public double Grad { get; set; } = 0;
    private Action _backward;
    private HashSet<Value> _prev;
    public Value(double data, IEnumerable<Value> children = null)
    {
        Data = data;
        _prev = children != null ? new HashSet<Value>(children) : new HashSet<Value>();
        _backward = () => { };
    }
    public static Value operator +(Value a, Value b)
    {
        var outV = new Value(a.Data + b.Data, new[] { a, b });
        outV._backward = () => {
            lock (a) a.Grad += outV.Grad;
            lock (b) b.Grad += outV.Grad;
        };
        return outV;
    }
    public static Value operator *(Value a, Value b)
    {
        var outV = new Value(a.Data * b.Data, new[] { a, b });
        outV._backward = () => {
            lock (a) a.Grad += b.Data * outV.Grad;
            lock (b) b.Grad += a.Data * outV.Grad;
        };
        return outV;
    }
    public static Value operator -(Value a, Value b) => a + (b * new Value(-1));
    public Value Exp()
    {
        double e = Math.Exp(Data);
        var outV = new Value(e, new[] { this });
        outV._backward = () => {
            lock (this) Grad += e * outV.Grad;
        };
        return outV;
    }
    public Value Sigmoid()
    {
        double s = 1.0 / (1.0 + Math.Exp(-Data));
        var outV = new Value(s, new[] { this });
        outV._backward = () => { lock (this) Grad += s * (1.0 - s) * outV.Grad; };
        return outV;
    }
    public Value Tanh()
    {
        double t = Math.Tanh(Data);
        var outV = new Value(t, new[] { this });
        outV._backward = () => { lock (this) Grad += (1.0 - t * t) * outV.Grad; };
        return outV;
    }
    public void Backward()
    {
        var topo = new List<Value>();
        var visited = new HashSet<Value>();
        void BuildTopo(Value v)
        {
            if (!visited.Contains(v)) { visited.Add(v); foreach (var child in v._prev) BuildTopo(child); topo.Add(v); }
        }
        BuildTopo(this);
        this.Grad = 1.0;
        topo.Reverse();
        foreach (var v in topo) v._backward();
    }
}
public class LstmCell
{
    public List<Value> Wf, Uf, Wi, Ui, Wc, Uc, Wo, Uo;
    public List<Value> bf, bi, bc, bo;
    private int _inSize, _hSize;
    private static Random rnd = new Random();

    public LstmCell(int inputSize, int hiddenSize)
    {
        _inSize = inputSize; _hSize = hiddenSize;
        Wf = InitM(hiddenSize, inputSize); Uf = InitM(hiddenSize, hiddenSize); bf = InitV(hiddenSize);
        Wi = InitM(hiddenSize, inputSize); Ui = InitM(hiddenSize, hiddenSize); bi = InitV(hiddenSize);
        Wc = InitM(hiddenSize, inputSize); Uc = InitM(hiddenSize, hiddenSize); bc = InitV(hiddenSize);
        Wo = InitM(hiddenSize, inputSize); Uo = InitM(hiddenSize, hiddenSize); bo = InitV(hiddenSize);
    }

    private List<Value> InitM(int rows, int cols)
    {
        double scale = Math.Sqrt(2.0 / (rows + cols));
        return Enumerable.Range(0, rows * cols).Select(_ => new Value((rnd.NextDouble() * 2 - 1) * scale)).ToList();
    }

    private List<Value> InitV(int size) => Enumerable.Range(0, size).Select(_ => new Value(0)).ToList();

    public List<Value> Parameters() =>
        Wf.Concat(Uf).Concat(bf).Concat(Wi).Concat(Ui).Concat(bi)
          .Concat(Wc).Concat(Uc).Concat(bc).Concat(Wo).Concat(Uo).Concat(bo).ToList();

    public (List<Value> h_next, List<Value> c_next) Forward(List<Value> x, List<Value> h_prev, List<Value> c_prev)
    {
        List<Value> f = Gate(x, h_prev, Wf, Uf, bf, true);
        List<Value> i = Gate(x, h_prev, Wi, Ui, bi, true);
        List<Value> c_tilde = Gate(x, h_prev, Wc, Uc, bc, false);

        List<Value> c_next = new List<Value>();
        for (int j = 0; j < _hSize; j++)
            c_next.Add((f[j] * c_prev[j]) + (i[j] * c_tilde[j]));

        List<Value> o = Gate(x, h_prev, Wo, Uo, bo, true);
        List<Value> h_next = new List<Value>();
        for (int j = 0; j < _hSize; j++)
            h_next.Add(o[j] * c_next[j].Tanh());

        return (h_next, c_next);
    }

    private List<Value> Gate(List<Value> x, List<Value> h, List<Value> W, List<Value> U, List<Value> b, bool isSigmoid)
    {
        List<Value> res = new List<Value>();
        for (int i = 0; i < _hSize; i++)
        {
            Value sum = b[i];
            for (int j = 0; j < _inSize; j++) sum = sum + (W[i * _inSize + j] * x[j]);
            for (int j = 0; j < _hSize; j++) sum = sum + (U[i * _hSize + j] * h[j]);
            res.Add(isSigmoid ? sum.Sigmoid() : sum.Tanh());
        }
        return res;
    }
}
public static class PatternAnalyzer
{
    public enum SequenceType { Linear, Oscillating, Fibonacci, Chaotic }

    public static SequenceType Identify(double[] data)
    {
        if (data.Length < 3) return SequenceType.Chaotic;

        double[] deltas = new double[data.Length - 1];
        for (int i = 0; i < deltas.Length; i++) deltas[i] = data[i + 1] - data[i];

        bool isLinear = true;
        for (int i = 0; i < deltas.Length - 1; i++)
            if (Math.Abs(deltas[i] - deltas[i + 1]) > 0.1) isLinear = false;
        if (isLinear) return SequenceType.Linear;

        bool isOsc = true;
        for (int i = 0; i < deltas.Length - 1; i++)
        {
            if (Math.Sign(deltas[i]) == Math.Sign(deltas[i + 1]) && deltas[i] != 0) isOsc = false;
        }
        if (isOsc) return SequenceType.Oscillating;

        bool isFib = true;
        for (int i = 2; i < data.Length; i++)
            if (Math.Abs((data[i - 2] + data[i - 1]) - data[i]) > 0.5) isFib = false;
        if (isFib) return SequenceType.Fibonacci;

        return SequenceType.Chaotic;
    }
}
public class EncoderDecoderModel
{
    public LstmCell encL1_F, encL1_B, decL1;
    public List<Value> outW;
    public Value outB;
    public Dictionary<PatternAnalyzer.SequenceType, List<Value>> patternExperts;
    public List<Value> gatingW;
    private int _hSize;

    public EncoderDecoderModel(int hiddenSize = 64)
    {
        _hSize = hiddenSize;
        encL1_F = new LstmCell(1, hiddenSize);
        encL1_B = new LstmCell(1, hiddenSize);
        decL1 = new LstmCell(1, hiddenSize);

        double scale = Math.Sqrt(2.0 / (hiddenSize + 1));
        var rnd = new Random();

        outW = Enumerable.Range(0, hiddenSize).Select(_ => new Value((rnd.NextDouble() * 2 - 1) * scale)).ToList();
        outB = new Value(0);

        int numExperts = Enum.GetValues(typeof(PatternAnalyzer.SequenceType)).Length;
        double gateScale = Math.Sqrt(2.0 / (hiddenSize + numExperts));
        gatingW = Enumerable.Range(0, hiddenSize * numExperts)
            .Select(_ => new Value((rnd.NextDouble() * 2 - 1) * gateScale)).ToList();

        patternExperts = new Dictionary<PatternAnalyzer.SequenceType, List<Value>>();
        foreach (PatternAnalyzer.SequenceType type in Enum.GetValues(typeof(PatternAnalyzer.SequenceType)))
        {
            patternExperts[type] = Enumerable.Range(0, hiddenSize).Select(_ => new Value(0)).ToList();
        }
    }
    public List<Value> Parameters()
    {
        var p = encL1_F.Parameters()
            .Concat(encL1_B.Parameters())
            .Concat(decL1.Parameters())
            .Concat(outW)
            .Concat(gatingW)
            .Append(outB).ToList();

        foreach (var expertParams in patternExperts.Values)
            p.AddRange(expertParams);

        return p;
    }
    public double[] GetWeightsSnapshot() => Parameters().Select(p => p.Data).ToArray();

    public void ApplyWeightsSnapshot(double[] snapshot)
    {
        var p = Parameters();
        if (snapshot.Length != p.Count) return;
        for (int i = 0; i < p.Count; i++) p[i].Data = snapshot[i];
    }
    public List<Value> Forward(double[] normalizedInput, int nFuture)
    {
        double baseStep = normalizedInput.Length > 1 ? (normalizedInput.Last() - normalizedInput.First()) / (normalizedInput.Length - 1) : 0;
        double[] intuition = ContextMemory.GetMemoryInfluence(normalizedInput, _hSize);

        var hf = Enumerable.Range(0, _hSize).Select(_ => new Value(0)).ToList();
        var cf = Enumerable.Range(0, _hSize).Select(_ => new Value(0)).ToList();
        foreach (var v in normalizedInput) (hf, cf) = encL1_F.Forward(new List<Value> { new Value(v) }, hf, cf);

        var hb = Enumerable.Range(0, _hSize).Select(_ => new Value(0)).ToList();
        var cb = Enumerable.Range(0, _hSize).Select(_ => new Value(0)).ToList();
        foreach (var v in normalizedInput.Reverse()) (hb, cb) = encL1_B.Forward(new List<Value> { new Value(v) }, hb, cb);

        var h_dec = new List<Value>();
        var c_dec = new List<Value>();
        for (int i = 0; i < _hSize; i++)
        {
            h_dec.Add((hf[i] + hb[i]) * new Value(0.5));
            c_dec.Add((cf[i] + cb[i]) * new Value(0.5));
        }

        var expertTypes = Enum.GetValues(typeof(PatternAnalyzer.SequenceType)).Cast<PatternAnalyzer.SequenceType>().ToList();
        var expertWeights = new List<Value>();

        var scores = new List<Value>();
        for (int e = 0; e < expertTypes.Count; e++)
        {
            Value score = new Value(0);
            for (int j = 0; j < _hSize; j++)
                score = score + (h_dec[j] * gatingW[e * _hSize + j]);
            scores.Add(score);
        }

        foreach (var s in scores) expertWeights.Add(s.Sigmoid());

        List<Value> preds = new List<Value>();
        Value lastValue = new Value(normalizedInput.Last());

        for (int i = 0; i < nFuture; i++)
        {
            (h_dec, c_dec) = decL1.Forward(new List<Value> { lastValue }, h_dec, c_dec);

            Value blendedExpertCorrection = new Value(0);
            for (int e = 0; e < expertTypes.Count; e++)
            {
                var expertParams = patternExperts[expertTypes[e]];
                Value expertOpinion = new Value(0);
                for (int j = 0; j < _hSize; j++)
                    expertOpinion = expertOpinion + (h_dec[j] * expertParams[j]);

                blendedExpertCorrection = blendedExpertCorrection + (expertOpinion * expertWeights[e]);
            }

            Value coreDelta = outB;
            for (int j = 0; j < _hSize; j++)
                coreDelta = coreDelta + (h_dec[j] * (outW[j] + new Value(intuition[j])));

            Value nextVal = lastValue + new Value(baseStep) + coreDelta + blendedExpertCorrection;
            preds.Add(nextVal);
            lastValue = nextVal;
        }
        return preds;
    }

    public List<double[]> ForwardVariants(double[] normalizedInput, int nFuture, int count = 3)
    {
        var results = new List<double[]>();
        Random rnd = new Random();
        double[] originalWeights = GetWeightsSnapshot();
        for (int v = 0; v < count; v++)
        {
            double[] noisyWeights = originalWeights.Select(w => w + (rnd.NextDouble() * 2 - 1) * (w * 0.03)).ToArray();
            ApplyWeightsSnapshot(noisyWeights);
            results.Add(Forward(normalizedInput, nFuture).Select(p => p.Data).ToArray());
        }
        ApplyWeightsSnapshot(originalWeights);
        return results;
    }

    public void SaveWeights(string file)
    {
        try
        {
            var p = Parameters();
            File.WriteAllLines(file, p.Select(v => v.Data.ToString("G17", CultureInfo.InvariantCulture)));
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n[SYSTEM] Збережено {p.Count} параметрів.");
            Console.ResetColor();
        }
        catch (Exception ex) { Console.WriteLine($"\n[!] ПОМИЛКА: {ex.Message}"); }
    }

    public void LoadWeights(string file)
    {
        if (!File.Exists(file) || new FileInfo(file).Length == 0) return;
        try
        {
            var lines = File.ReadAllLines(file);
            var p = Parameters();
            if (lines.Length != p.Count) return;
            for (int i = 0; i < p.Count; i++) p[i].Data = double.Parse(lines[i], CultureInfo.InvariantCulture);
            Console.WriteLine($"\n[+] MoE-Engine ({p.Count} точок) завантажено.");
        }
        catch { Console.WriteLine("\n[!] Помилка weights.csv."); }
    }
}
public static class Visualizer
{
    public static void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(@"
    ███╗  ██╗███████╗██╗██████╗  ██████╗ ███╗  ██╗██╗  ██╗ █████╗ 
    ████╗ ██║██╔════╝██║██╔══██╗██╔═══██╗████╗ ██║██║ ██╔╝██╔══██╗
    ██╔██╗██║█████╗  ██║██████╔╝██║   ██║██╔██╗██║█████╔╝ ███████║
    ██║╚████║██╔══╝  ██║██╔══██╗██║   ██║██║╚████║██╔═██╗ ██║  ██║
    ██║ ╚███║███████╗██║██║  ██║╚██████╔╝██║ ╚███║██║  ██╗██║  ██║
    ╚═╝  ╚══╝╚══════╝╚═╝╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚══╝╚═╝  ╚═╝╚═╝  ╚═╝");
        Console.WriteLine("      Stacked Bidirectional LSTM | Neural Pulse Monitor\n");
        Console.ResetColor();
    }
    public static void PrintNeuralPulse(double gradNorm, double velNorm)
    {
        Console.Write(" [Pulse: ");
        if (gradNorm > 0.5) { Console.ForegroundColor = ConsoleColor.Red; Console.Write("HOT"); }
        else if (gradNorm > 0.05) { Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("ACTIVE"); }
        else { Console.ForegroundColor = ConsoleColor.Green; Console.Write("STABLE"); }

        Console.ResetColor();
        Console.Write($" | Flow: {gradNorm:F4} | Inertia: {velNorm:F4}]");
    }

    public static void PrintPrediction(double[] input, double[] forecast)
    {
        var pattern = PatternAnalyzer.Identify(input);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n--- ВІЗУАЛІЗАЦІЯ ПАТТЕРНУ: {pattern.ToString().ToUpper()} ---");
        Console.ResetColor();

        double min = Math.Min(input.Min(), forecast.Min());
        double max = Math.Max(input.Max(), forecast.Max());
        int height = 10;

        for (int y = height; y >= 0; y--)
        {
            double threshold = min + (max - min) * y / height;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"{threshold,5:F1} │ ");
            Console.ResetColor();

            for (int x = 0; x < input.Length; x++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(input[x] >= threshold ? "█ " : "  ");
            }
            for (int x = 0; x < forecast.Length; x++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(forecast[x] >= threshold ? "★ " : "  ");
            }
            Console.WriteLine();
        }
        Console.ResetColor();
        Console.WriteLine("      └─" + new string('─', (input.Length + forecast.Length) * 2));
        double avgStep = (forecast.Last() - input.First()) / (input.Length + forecast.Length - 1);
        string type = avgStep > 0.3 ? "ЗРОСТАЮЧА" : (avgStep < -0.3 ? "СПАДАЮЧА" : "СТАБІЛЬНА");
        Console.WriteLine($"\n[АНАЛІЗ]: Тип: {type} | Середній крок: {avgStep:F2}");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(new string('-', 45));
        Console.ResetColor();
    }
}
public static class IntelLog
{
    private static string logFile = "history.log";
    public static void Log(double[] input, double[] output)
    {
        string entry = $"[{DateTime.Now:HH:mm:ss}] IN: {string.Join(",", input)} -> OUT: {string.Join(",", output.Select(x => Math.Round(x, 1)))}\n";
        File.AppendAllText(logFile, entry);
    }
}
public static class JewelryTrainer
{
    public static void Train(EncoderDecoderModel model, List<string> lines, string weightsFile)
    {
        var parameters = model.Parameters();
        var bestSessionWeights = model.GetWeightsSnapshot();
        Random rnd = new Random();

        double initialLR = 0.01;
        double momentum = 0.9;
        int totalEpochs = 1000;
        int redRowCounter = 0;

        double bestLoss = double.MaxValue;
        double[] velocity = new double[parameters.Count];
        object lockObj = new object();

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"\n[SOFT LANDING] Mode Active. Ядер: {Environment.ProcessorCount}. Сценаріїв: {lines.Count}");
        Console.WriteLine("[!] КЕРУВАННЯ: [S]-Зберегти | [W]-Множинний Тест");
        Console.ResetColor();

        for (int epoch = 1; epoch <= totalEpochs; epoch++)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.S)
                {
                    model.ApplyWeightsSnapshot(bestSessionWeights);
                    model.SaveWeights(weightsFile);
                    Console.WriteLine("\n[+] weights.csv оновлено. Вихід.");
                    return;
                }

                if (key == ConsoleKey.W)
                {
                    RunMultiVariantTest(model, epoch, weightsFile, parameters, velocity, ref redRowCounter, initialLR, rnd);
                }
            }

            double totalLoss = 0;
            double lr = initialLR * 0.5 * (1 + Math.Cos(Math.PI * epoch / totalEpochs));
            foreach (var p in parameters) p.Grad = 0;

            Parallel.ForEach(lines, line => {
                try
                {
                    var data = line.Split(',').Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToArray();

                    double rowScale = data.Select(Math.Abs).Max();
                    if (rowScale < 1e-6) rowScale = 1.0;

                    var input = data.Take(5).Select(v => v / rowScale).ToArray();
                    var target = data.Skip(5).Take(5).Select(v => v / rowScale).ToArray();

                    var preds = model.Forward(input, 5);
                    Value loss = new Value(0);

                    for (int i = 0; i < 5; i++)
                    {
                        var diff = preds[i] - new Value(target[i]);
                        double weight = 1.0 + (i * 0.5);
                        loss = loss + (diff * diff * new Value(weight));
                    }

                    loss.Backward();

                    ContextMemory.Store(input, preds.Select(p => p.Grad).ToArray(), loss.Data);

                    lock (lockObj) { totalLoss += loss.Data; }
                }
                catch { }
            });

            double avgLoss = totalLoss / lines.Count;

            if (avgLoss > 1000.0)
            {
                ContextMemory.Clear();
                model.ApplyWeightsSnapshot(bestSessionWeights);
                for (int i = 0; i < velocity.Length; i++) velocity[i] = 0;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\n[!] КРИТИЧНИЙ ЗБІЙ (Loss: {avgLoss:F2}): Відкат.");
                Console.ResetColor();
                continue;
            }

            double gradSum = 0;
            double velSum = 0;

            for (int i = 0; i < parameters.Count; i++)
            {
                double grad = Math.Clamp(parameters[i].Grad / lines.Count, -1.0, 1.0);
                velocity[i] = momentum * velocity[i] - lr * grad;
                parameters[i].Data += velocity[i];

                gradSum += Math.Abs(grad);
                velSum += Math.Abs(velocity[i]);
            }

            double gradNorm = gradSum / parameters.Count;
            double velNorm = velSum / parameters.Count;

            if (avgLoss < bestLoss)
            {
                bestLoss = avgLoss; redRowCounter = 0;
                bestSessionWeights = model.GetWeightsSnapshot();
                if (epoch % 10 == 0) model.SaveWeights(weightsFile);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{lines.Count}/{lines.Count} ━ 🟢 Epoch {epoch:D3} ━ loss: {avgLoss:F6}");
                Visualizer.PrintNeuralPulse(gradNorm, velNorm);
                Console.WriteLine(" ━ RECORD SAVED");
            }
            else
            {
                redRowCounter++;
                model.ApplyWeightsSnapshot(bestSessionWeights);
                for (int i = 0; i < velocity.Length; i++) velocity[i] *= 0.2;

                if (redRowCounter > 5)
                {
                    double shakeForce = (redRowCounter > 50) ? 0.15 : 0.03;
                    for (int i = 0; i < parameters.Count; i++)
                        parameters[i].Data += (rnd.NextDouble() * 2 - 1) * (lr * shakeForce);

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write($"{lines.Count}/{lines.Count} ━ ⚡ Epoch {epoch:D3} ━ loss: {avgLoss:F6}");
                    Visualizer.PrintNeuralPulse(gradNorm, velNorm);
                    Console.WriteLine($" ━ {(redRowCounter > 50 ? "[FINE TUNING]" : "[STABILIZING]")}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{lines.Count}/{lines.Count} ━ 🔴 Epoch {epoch:D3} ━ loss: {avgLoss:F6}");
                    Visualizer.PrintNeuralPulse(gradNorm, velNorm);
                    Console.WriteLine(" ━ [ROLLBACK]");
                }
            }
            Console.ResetColor();
        }
    }
    private static void RunMultiVariantTest(EncoderDecoderModel model, int epoch, string weightsFile, List<Value> parameters, double[] velocity, ref int redRowCounter, double initialLR, Random rnd)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n\n{'═',-20} MULTI-VARIANT TEST (Епоха {epoch}) {'═',-20}");
        Console.Write("[?] Введіть 5 чисел через пробіл: ");
        Console.ResetColor();

        string inputStr = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(inputStr))
        {
            try
            {
                double[] raw = inputStr.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                     .Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToArray();
                if (raw.Length == 5)
                {
                    double rowScale = raw.Select(Math.Abs).Max();
                    if (rowScale < 1e-6) rowScale = 1.0;

                    var variants = model.ForwardVariants(raw.Select(v => v / rowScale).ToArray(), 5, 3);

                    for (int i = 0; i < variants.Count; i++)
                    {
                        double[] fore = variants[i].Select(v => v * rowScale).ToArray();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"\n--- ВАРІАНТ #{i + 1} ---");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(string.Join("  ", fore.Select(x => x.ToString("F2"))));
                        Visualizer.PrintPrediction(raw, fore);
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n[ КЕРУВАННЯ ]: [1,2,3]-Зберегти варіант | [F]-Шок | [Enter]-Далі");
                    var choice = Console.ReadKey(true).Key;

                    if (choice >= ConsoleKey.D1 && choice <= ConsoleKey.D3)
                    {
                        model.SaveWeights(weightsFile);
                        Console.WriteLine($"\n[+] Варіант {choice - ConsoleKey.D0} обрано.");
                    }
                    else if (choice == ConsoleKey.F)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n[!] ШОК! Руйную зв'язки...");
                        for (int i = 0; i < parameters.Count; i++)
                        {
                            parameters[i].Data += (rnd.NextDouble() * 2 - 1) * (initialLR * 2.5);
                            velocity[i] = 0;
                        }
                        redRowCounter = 0;
                    }
                }
            }
            catch { }
        }
        Console.WriteLine("\n[!] Натисніть ENTER...");
        Console.ReadLine();
    }
}
public static class ContextMemory
{
    private static List<(double[] Input, double[] Grads)> _memoryStore = new List<(double[], double[])>();
    private const int MaxMemorySlots = 1000;
    private static object _lock = new object();
    public static void Store(double[] input, double[] grads, double currentLoss)
    {
        if (currentLoss > 5.0) return;

        lock (_lock)
        {
            if (_memoryStore.Count >= MaxMemorySlots) _memoryStore.RemoveAt(0);
            _memoryStore.Add((input.ToArray(), grads.ToArray()));
        }
    }
    public static double[] GetMemoryInfluence(double[] currentInput, int hiddenSize)
    {
        double[] intuition = new double[hiddenSize];
        lock (_lock)
        {
            if (_memoryStore.Count == 0) return intuition;

            var similar = _memoryStore
                .Select(m => new { Entry = m, Sim = ComputeCosineSimilarity(currentInput, m.Input) })
                .OrderByDescending(x => x.Sim)
                .Take(3)
                .Where(x => x.Sim > 0.85)
                .ToList();

            if (!similar.Any()) return intuition;

            for (int i = 0; i < hiddenSize; i++)
            {
                intuition[i] = similar.Average(s => s.Entry.Grads.Length > i % s.Entry.Grads.Length
                    ? s.Entry.Grads[i % s.Entry.Grads.Length]
                    : 0) * 0.05;
            }
        }
        return intuition;
    }

    public static int GetSize() => _memoryStore.Count;
    public static void Clear() { lock (_lock) _memoryStore.Clear(); }
    private static double ComputeCosineSimilarity(double[] a, double[] b)
    {
        double dot = 0, mA = 0, mB = 0;
        for (int i = 0; i < a.Length; i++)
        {
            dot += a[i] * b[i];
            mA += a[i] * a[i];
            mB += b[i] * b[i];
        }
        return dot / (Math.Sqrt(mA) * Math.Sqrt(mB) + 1e-10);
    }
}
public static class DatasetGenerator
{
    public static void Generate(string filePath, int count = 1000)
    {
        var rnd = new Random();
        var lines = new List<string>();

        for (int i = 0; i < count; i++)
        {
            double[] full = new double[10];
            int type = rnd.Next(5);

            if (type == 0)
            {
                double start = rnd.Next(-50, 50);
                double step = (rnd.NextDouble() * 20) - 10;
                for (int j = 0; j < 10; j++) full[j] = start + j * step;
            }
            else if (type == 1)
            {
                full[0] = rnd.Next(1, 10);
                full[1] = rnd.Next(1, 10);
                for (int j = 2; j < 10; j++) full[j] = full[j - 1] + full[j - 2];
            }
            else if (type == 2)
            {
                double baseVal = rnd.Next(-10, 20);
                double amp = rnd.Next(2, 15);
                for (int j = 0; j < 10; j++) full[j] = baseVal + (j % 2 == 0 ? amp : -amp);
            }
            else if (type == 3)
            {
                double start = rnd.Next(1, 5);
                double ratio = 1.1 + (rnd.NextDouble() * 0.4);
                for (int j = 0; j < 10; j++) full[j] = start * Math.Pow(ratio, j);
            }
            else
            {
                double current = rnd.Next(-20, 20);
                for (int j = 0; j < 10; j++)
                {
                    full[j] = current;
                    current += (rnd.NextDouble() * 10) - 5;
                }
            }

            lines.Add(string.Join(",", full.Select(v => v.ToString("F3", CultureInfo.InvariantCulture))));
        }

        File.WriteAllLines(filePath, lines);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n[GENERATOR] Створено {count} сценаріїв (Linear, Fib, Osc, Geo, Walk).");
        Console.ResetColor();
    }
}