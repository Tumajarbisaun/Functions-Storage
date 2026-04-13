using System;
using System.Diagnostics;
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
    public static double Sigmoid(double x) => throw new NotImplementedException();
    public static double Relu(double x) => throw new NotImplementedException();
    public static double Tanh(double x) => throw new NotImplementedException();
    public static double EuclideanDistance(double[] vectorA, double[] vectorB) => throw new NotImplementedException();
    public static double MeanSquaredError(double[] actual, double[] predicted) => throw new NotImplementedException();
    public static double CrossEntropy(double[] actual, double[] predicted) => throw new NotImplementedException();
    public static double CalculateAccuracy(int[] actual, int[] predicted) => throw new NotImplementedException();
    public static double[] Normalize(double[] data) => throw new NotImplementedException();
    public static double[] Standardize(double[] data) => throw new NotImplementedException();
    public static (T[] train, T[] test) SplitData<T>(T[] data, double ratio = 1.0) => throw new NotImplementedException();
    public static double DotProduct(double[] a, double[] b) => throw new NotImplementedException();
    public static double[,] MultiplyMatrices(double[,] matrixA, double[,] matrixB) => throw new NotImplementedException();
    public static double[,] Transpose(double[,] matrix) => throw new NotImplementedException();
    public static int GetLevenshteinDistance(string source, string target) => throw new NotImplementedException();
    public static string[] GetKeywords(string text, int count) => throw new NotImplementedException();
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