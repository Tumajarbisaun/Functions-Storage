using System;
using System.Diagnostics;
using System.Drawing;
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
using static System.Net.Mime.MediaTypeNames;

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
    public static double GetMin(double[] array)
    {
        if (array == null || array.Length == 0) throw new ArgumentException("Array is empty!");
        double min = array[0];
        for (int i = 1; i < array.Length; i++) {
            if (array[i] < min) min = array[i];
        }
        return min;
    }
    public static T[] Slice<T>(T[] array, int start, int end)
    {
        if (array == null) return Array.Empty<T>();
        if (start < 0) start = 0;
        if (end > array.Length) end = array.Length;
        int length = end - start;
        if (length <= 0) return Array.Empty<T>();
        T[] result = new T[length];
        Array.Copy(array, start, result, 0, length);
        return result;
    }
    public static TResult[] Map<T, TResult>(T[] array, Func<T, TResult> selector)
    {
        if (array == null || selector == null) return Array.Empty<TResult>();
        TResult[] result = new TResult[array.Length];
        for (int i = 0; i < array.Length; i++) {
            result[i] = selector(array[i]);
        }
        return result;
    }
    public static T[] Distinct<T>(T[] array)
    {
        if (array == null || array.Length == 0) return array;
        HashSet<T> set = new HashSet<T>(array);
        T[] result = new T[set.Count];
        set.CopyTo(result);
        return result;
    }
    public static string Join<T>(T[] array, string separator = ", ")
    {
        if (array == null || array.Length == 0) return string.Empty;
        return string.Join(separator, array);
    }
    public static bool All<T>(T[] array, Func<T, bool> predicate)
    {
        if (array == null || predicate == null) return false;
        foreach (T item in array) {
            if (!predicate(item)) return false;
        }
        return true;
    }
    public static bool Any<T>(T[] array, Func<T, bool> predicate)
    {
        if (array == null || predicate == null) return false;
        foreach (T item in array) {
            if (predicate(item)) return true;
        }
        return false;
    }
    public static int Count<T>(T[] array, Func<T, bool> predicate)
    {
        if (array == null || predicate == null) return 0;
        int count = 0;
        foreach (T item in array) {
            if (predicate(item)) count++;
        }
        return count;
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
    public static double[] SolveGauss(double[,] A, double[] B)
    {
        int n = B.Length;
        double[,] matrix = (double[,])A.Clone();
        double[] b = (double[])B.Clone();
        for (int i = 0; i < n; i++) {
            int pivot = i;
            for (int j = i + 1; j < n; j++)
                if (Math.Abs(matrix[j, i]) > Math.Abs(matrix[pivot, i])) pivot = j;
            for (int k = i; k < n; k++) {
                double t = matrix[i, k]; matrix[i, k] = matrix[pivot, k]; matrix[pivot, k] = t;
            }
            double tempB = b[i]; b[i] = b[pivot]; b[pivot] = tempB;
            for (int j = i + 1; j < n; j++) {
                double factor = matrix[j, i] / matrix[i, i];
                b[j] -= factor * b[i];
                for (int k = i; k < n; k++) matrix[j, k] -= factor * matrix[i, k];
            }
        }
        double[] x = new double[n];
        for (int i = n - 1; i >= 0; i--) {
            double sum = 0;
            for (int j = i + 1; j < n; j++) sum += matrix[i, j] * x[j];
            x[i] = (b[i] - sum) / matrix[i, i];
        }
        return x;
    }
    public static double NewtonMethod(Func<double, double> f, Func<double, double> df, double x0, double eps = 1e-7)
    {
        double x = x0;
        int maxIterations = 1000;
        for (int i = 0; i < maxIterations; i++) {
            double fx = f(x);
            double dfx = df(x);
            if (Math.Abs(dfx) < 1e-12) break;

            double xNext = x - fx / dfx;
            if (Math.Abs(xNext - x) < eps) return xNext;
            x = xNext;
        }
        return x;
    }
    public static double SimpleIteration(Func<double, double> g, double x0, double eps = 1e-7) 
    {
        double x = x0;
        int maxIterations = 1000;
        for (int i = 0; i < maxIterations; i++) {
            double xNext = g(x);
            if (Math.Abs(xNext - x) < eps) return xNext;
            x = xNext;
        }
        return x;
    }
    public static double MonteCarloPi(int points = 1000000)
    {
        Random rnd = new Random();
        int insideCircle = 0;
        for (int i = 0; i < points; i++) {
            double x = rnd.NextDouble();
            double y = rnd.NextDouble();
            if (x * x + y * y <= 1) insideCircle++;
        }
        return (double)insideCircle / points * 4;
    }
    public static double SimulatedAnnealing(Func<double, double> f, double startX, double temp = 1000.0, double coolRate = 0.99)
    {
        Random rnd = new Random();
        double currentX = startX;
        double currentEnergy = f(currentX);
        while (temp > 0.01)
        {
            double nextX = currentX + (rnd.NextDouble() * 2 - 1);
            double nextEnergy = f(nextX);
            if (nextEnergy < currentEnergy || Math.Exp((currentEnergy - nextEnergy) / temp) > rnd.NextDouble()) {
                currentX = nextX;
                currentEnergy = nextEnergy;
            }
            temp *= coolRate;
        }
        return currentX;
    }
    public static double[] AntColonyOptimization(double[,] distances, int antsCount = 10, int iterations = 100)
    {
        int n = distances.GetLength(0);
        double[,] pheromones = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++) pheromones[i, j] = 0.1;
        double bestDist = double.MaxValue;
        Random rnd = new Random();
        for (int iter = 0; iter < iterations; iter++) {
            for (int ant = 0; ant < antsCount; ant++) {
                int[] path = GenerateRandomPath(n, rnd);
                double currentDist = CalculatePathDistance(path, distances);
                if (currentDist < bestDist) {
                    bestDist = currentDist;
                    UpdatePheromones(pheromones, path, currentDist);
                }
            }
            EvaporatePheromones(pheromones, 0.5);
        }
        return new double[] { bestDist };
    }
    private static int[] GenerateRandomPath(int n, Random rnd)
    {
        int[] path = new int[n];
        for (int i = 0; i < n; i++) path[i] = i;
        for (int i = n - 1; i > 0; i--)
        {
            int j = rnd.Next(i + 1);
            int temp = path[i];
            path[i] = path[j];
            path[j] = temp;
        }
        return path;
    }
    private static double CalculatePathDistance(int[] path, double[,] distances)
    {
        double total = 0;
        for (int i = 0; i < path.Length - 1; i++)
        {
            total += distances[path[i], path[i + 1]];
        }
        total += distances[path[path.Length - 1], path[0]];
        return total;
    }
    private static void UpdatePheromones(double[,] ph, int[] path, double dist)
    {
        double deposit = 1.0 / dist;
        for (int i = 0; i < path.Length - 1; i++)
            ph[path[i], path[i + 1]] += deposit;
    }
    private static void EvaporatePheromones(double[,] ph, double rho)
    {
        for (int i = 0; i < ph.GetLength(0); i++)
            for (int j = 0; j < ph.GetLength(1); j++) ph[i, j] *= (1 - rho);
    }
    public static double[] SolveGaussSeidel(double[,] A, double[] f, double epsilon)
    {
        int n = f.Length;
        double[] x = new double[n];
        int itCount = 0;
        while (true) {
            double[] xOld = (double[])x.Clone();
            for (int i = 0; i < n; i++) {
                double sum1 = 0;
                for (int j = 0; j < i; j++) {
                    sum1 += A[i, j] * x[j];
                }
                double sum2 = 0;
                for (int j = i + 1; j < n; j++) {
                    sum2 += A[i, j] * xOld[j];
                }
                x[i] = (f[i] - sum1 - sum2) / A[i, i];
            }
            itCount++;
            double maxDiff = 0;
            for (int i = 0; i < n; i++) {
                double diff = Math.Abs(x[i] - xOld[i]);
                if (diff > maxDiff) maxDiff = diff;
            }
            if (maxDiff < epsilon || itCount > 10000) break;
        }
        return x;
    }
    public static double[] CalculateResidual(double[,] A, double[] x, double[] f)
    {
        int n = f.Length;
        double[] residual = new double[n];
        for (int i = 0; i < n; i++) {
            double ax = 0;
            for (int j = 0; j < n; j++) {
                ax += A[i, j] * x[j];
            }
            residual[i] = ax - f[i];
        }
        return residual;
    }
    public static double[] SolveJacobi(double[,] A, double[] f, double epsilon)
    {
        int n = f.Length;
        double[] x = new double[n];
        double[] xNext = new double[n];
        int itCount = 0;
        while (true) {
            for (int i = 0; i < n; i++) {
                double sum = 0;
                for (int j = 0; j < n; j++) {
                    if (i != j) {
                        sum += A[i, j] * x[j];
                    }
                }
                xNext[i] = (f[i] - sum) / A[i, i];
            }
            itCount++;
            double maxDiff = 0;
            for (int i = 0; i < n; i++) {
                double diff = Math.Abs(xNext[i] - x[i]);
                if (diff > maxDiff) maxDiff = diff;
            }
            Array.Copy(xNext, x, n);
            if (maxDiff < epsilon || itCount > 10000) {
                if (itCount > 10000) Console.WriteLine("Warning: Jacobi did not converge.");
                break;
            }
        }
        return x;
    }
    public static double IntegrateTrapezoidal(Func<double, double> f, double a, double b, int n)
    {
        double h = (b - a) / n;
        double sum = 0.5 * (f(a) + f(b));
        for (int i = 1; i < n; i++) {
            sum += f(a + i * h);
        }
        return sum * h;
    }
    public static double IntegrateSimpson(Func<double, double> f, double a, double b, int n)
    {
        if (n % 2 != 0) n++;
        double h = (b - a) / n;
        double sum = f(a) + f(b);
        for (int i = 1; i < n; i++) {
            double x = a + i * h;
            sum += (i % 2 == 0) ? 2 * f(x) : 4 * f(x);
        }
        return sum * h / 3.0;
    }
    public static void SolveEuler(Func<double, double, double> dydx, double x0, double y0, double xEnd, double h)
    {
        double x = x0;
        double y = y0;
        while (x <= xEnd) {
            Console.WriteLine($"x: {x:F2}, y: {y:F5}");
            y += h * dydx(x, y);
            x += h;
        }
    }
    public static double SolveRungeKutta4(Func<double, double, double> f, double x0, double y0, double h)
    {
        double k1 = h * f(x0, y0);
        double k2 = h * f(x0 + h / 2.0, y0 + k1 / 2.0);
        double k3 = h * f(x0 + h / 2.0, y0 + k2 / 2.0);
        double k4 = h * f(x0 + h, y0 + k3);
        return y0 + (k1 + 2 * k2 + 2 * k3 + k4) / 6.0;
    }
    public static double InterpolateLagrange(double[] xValues, double[] yValues, double x)
    {
        double result = 0;
        int n = xValues.Length;
        for (int i = 0; i < n; i++) {
            double term = yValues[i];
            for (int j = 0; j < n; j++) {
                if (i != j)
                    term *= (x - xValues[j]) / (xValues[i] - xValues[j]);
            }
            result += term;
        }
        return result;
    }
    public static double InterpolateLinearSpline(double[] xValues, double[] yValues, double x)
    {
        int n = xValues.Length;
        if (x <= xValues[0]) return yValues[0];
        if (x >= xValues[n - 1]) return yValues[n - 1];
        for (int i = 0; i < n - 1; i++) {
            if (x >= xValues[i] && x <= xValues[i + 1]) {
                return yValues[i] + (x - xValues[i]) * (yValues[i + 1] - yValues[i]) / (xValues[i + 1] - xValues[i]);
            }
        }
        return 0;
    }
    public static double GetPearsonCorrelation(double[] x, double[] y)
    {
        if (x.Length != y.Length) throw new ArgumentException("Arrays must be of the same length.");
        int n = x.Length;
        double avgX = ArrayUtils.GetAverage(x);
        double avgY = ArrayUtils.GetAverage(y);
        double sumNumerator = 0;
        double sumDenomX = 0;
        double sumDenomY = 0;
        for (int i = 0; i < n; i++) {
            double diffX = x[i] - avgX;
            double diffY = y[i] - avgY;
            sumNumerator += diffX * diffY;
            sumDenomX += diffX * diffX;
            sumDenomY += diffY * diffY;
        }
        if (sumDenomX == 0 || sumDenomY == 0) return 0;
        return sumNumerator / Math.Sqrt(sumDenomX * sumDenomY);
    }
    public static (double a, double b) LeastSquares(double[] x, double[] y)
    {
        if (x.Length != y.Length) throw new ArgumentException("Arrays must be of the same length.");
        int n = x.Length;
        double sumX = 0, sumY = 0, sumXY = 0, sumX2 = 0;
        for (int i = 0; i < n; i++) {
            sumX += x[i];
            sumY += y[i];
            sumXY += x[i] * y[i];
            sumX2 += x[i] * x[i];
        }
        double a = (n * sumXY - sumX * sumY) / (n * sumX2 - sumX * sumX);
        double b = (sumY - a * sumX) / n;
        return (a, b);
    }
    public static double EvaluatePolynomialHorner(double[] coefficients, double x)
    {
        double result = coefficients[0];
        for (int i = 1; i < coefficients.Length; i++) {
            result = result * x + coefficients[i];
        }
        return result;
    }
    public static double InterpolateNewton(double[] xValues, double[] yValues, double x)
    {
        int n = xValues.Length;
        double[,] diff = new double[n, n];
        for (int i = 0; i < n; i++) diff[i, 0] = yValues[i];
        for (int j = 1; j < n; j++) {
            for (int i = 0; i < n - j; i++) {
                diff[i, j] = (diff[i + 1, j - 1] - diff[i, j - 1]) / (xValues[i + j] - xValues[i]);
            }
        }
        double result = diff[0, 0];
        double product = 1;
        for (int i = 1; i < n; i++) {
            product *= (x - xValues[i - 1]);
            result += diff[0, i] * product;
        }
        return result;
    }
    public static double ChebyshevPolynomial(int n, double x)
    {
        if (n == 0) return 1;
        if (n == 1) return x;
        double t0 = 1;
        double t1 = x;
        double tn = 0;
        for (int i = 2; i <= n; i++) {
            tn = 2 * x * t1 - t0;
            t0 = t1;
            t1 = tn;
        }
        return tn;
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
    public static void ResizeImage(string path, int width, int height)
    {
        using (System.Drawing.Image image = System.Drawing.Image.FromFile(path)) {
            using (System.Drawing.Bitmap resized = new System.Drawing.Bitmap(image, width, height)) {
                string tempPath = path + ".tmp";
                resized.Save(tempPath, image.RawFormat);
                image.Dispose();
                System.IO.File.Delete(path);
                System.IO.File.Move(tempPath, path);
            }
        }
    }
    public static string GetImageFormat(string path)
    {
        using (System.Drawing.Image image = System.Drawing.Image.FromFile(path)) {
            return image.RawFormat.ToString();
        }
    }
    public static void ApplyGrayscale(string path)
    {
        ProcessPixels(path, (c) => {
            int gray = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
            return System.Drawing.Color.FromArgb(gray, gray, gray);
        });
    }
    public static void ApplySepia(string path)
    {
        ProcessPixels(path, (c) => {
            int r = (int)((c.R * 0.393) + (c.G * 0.769) + (c.B * 0.189));
            int g = (int)((c.R * 0.349) + (c.G * 0.686) + (c.B * 0.168));
            int b = (int)((c.R * 0.272) + (c.G * 0.534) + (c.B * 0.131));
            return System.Drawing.Color.FromArgb(Math.Min(255, r), Math.Min(255, g), Math.Min(255, b));
        });
    }
    public static void RotateImage(string path, float degrees)
    {
        using (System.Drawing.Image image = System.Drawing.Image.FromFile(path)) {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(image);
            if (degrees == 90) bmp.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
            else if (degrees == 180) bmp.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            else if (degrees == 270) bmp.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);
            string tempPath = path + ".tmp";
            bmp.Save(tempPath, image.RawFormat);
            image.Dispose();
            System.IO.File.Delete(path);
            System.IO.File.Move(tempPath, path);
        }
    }
    public static void FlipImage(string path, bool horizontal, bool vertical)
    {
        using (System.Drawing.Image image = System.Drawing.Image.FromFile(path)) {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(image);
            if (horizontal && vertical) bmp.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipXY);
            else if (horizontal) bmp.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipX);
            else if (vertical) bmp.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY);
            string tempPath = path + ".tmp";
            bmp.Save(tempPath, image.RawFormat);
            image.Dispose();
            System.IO.File.Delete(path);
            System.IO.File.Move(tempPath, path);
        }
    }
    public static void CropImage(string path, int x, int y, int width, int height)
    {
        using (System.Drawing.Image image = System.Drawing.Image.FromFile(path)) {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(image);
            System.Drawing.Rectangle cropArea = new System.Drawing.Rectangle(x, y, width, height);
            using (System.Drawing.Bitmap cropped = bmp.Clone(cropArea, bmp.PixelFormat)) {
                string tempPath = path + ".tmp";
                cropped.Save(tempPath, image.RawFormat);
                image.Dispose();
                System.IO.File.Delete(path);
                System.IO.File.Move(tempPath, path);
            }
        }
    }
    public static void AddWatermark(string path, string text, int x, int y)
    {
        using (System.Drawing.Image image = System.Drawing.Image.FromFile(path)) {
            using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(image)) {
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp)) {
                    using (System.Drawing.Font font = new System.Drawing.Font("Arial", 20, System.Drawing.FontStyle.Bold)) {
                        g.DrawString(text, font, System.Drawing.Brushes.White, new System.Drawing.PointF(x, y));
                    }
                }
                string tempPath = path + ".tmp";
                bmp.Save(tempPath, image.RawFormat);
                image.Dispose();
                System.IO.File.Delete(path);
                System.IO.File.Move(tempPath, path);
            }
        }
    }
    public static void ConvertFormat(string sourcePath, string targetFormat)
    {
        using (System.Drawing.Image image = System.Drawing.Image.FromFile(sourcePath)) {
            string newPath = System.IO.Path.ChangeExtension(sourcePath, targetFormat.ToLower());
            System.Drawing.Imaging.ImageFormat format = targetFormat.ToLower() switch {
                "png" => System.Drawing.Imaging.ImageFormat.Png,
                "gif" => System.Drawing.Imaging.ImageFormat.Gif,
                "bmp" => System.Drawing.Imaging.ImageFormat.Bmp,
                _ => System.Drawing.Imaging.ImageFormat.Jpeg
            };
            image.Save(newPath, format);
        }
    }
    public static (int width, int height) GetImageDimensions(string path)
    {
        using (System.Drawing.Image image = System.Drawing.Image.FromFile(path)) {
            return (image.Width, image.Height);
        }
    }
    public static void AdjustBrightness(string path, int level)
    {
        ProcessPixels(path, (c) => {
            int r = Math.Clamp(c.R + level, 0, 255);
            int g = Math.Clamp(c.G + level, 0, 255);
            int b = Math.Clamp(c.B + level, 0, 255);
            return System.Drawing.Color.FromArgb(r, g, b);
        });
    }
    public static void InvertColors(string path)
    {
        ProcessPixels(path, (c) => System.Drawing.Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
    }
    public static string GetDominantColor(string path)
    {
        using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(path)) {
            long r = 0, g = 0, b = 0;
            int total = bmp.Width * bmp.Height;
            for (int x = 0; x < bmp.Width; x++) {
                for (int y = 0; y < bmp.Height; y++) {
                    System.Drawing.Color clr = bmp.GetPixel(x, y);
                    r += clr.R; g += clr.G; b += clr.B;
                }
            }
            return $"#{r / total:X2}{g / total:X2}{b / total:X2}";
        }
    }
    public static void RemoveExifData(string path)
    {
        using (System.Drawing.Image image = System.Drawing.Image.FromFile(path)) {
            using (System.Drawing.Bitmap clean = new System.Drawing.Bitmap(image)) {
                string tempPath = path + ".tmp";
                clean.Save(tempPath, image.RawFormat);
                image.Dispose();
                System.IO.File.Delete(path);
                System.IO.File.Move(tempPath, path);
            }
        }
    }
    private static void ProcessPixels(string path, Func<System.Drawing.Color, System.Drawing.Color> transformation)
    {
        using (System.Drawing.Image image = System.Drawing.Image.FromFile(path)) {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(image);
            for (int x = 0; x < bmp.Width; x++) {
                for (int y = 0; y < bmp.Height; y++) {
                    bmp.SetPixel(x, y, transformation(bmp.GetPixel(x, y)));
                }
            }
            string tempPath = path + ".tmp";
            bmp.Save(tempPath, image.RawFormat);
            image.Dispose();
            System.IO.File.Delete(path);
            System.IO.File.Move(tempPath, path);
        }
    }
    public static void ApplyContrast(string path, int level)
    {
        double contrast = Math.Pow((100.0 + level) / 100.0, 2);
        ProcessPixels(path, (c) => {
            double r = ((((c.R / 255.0) - 0.5) * contrast) + 0.5) * 255.0;
            double g = ((((c.G / 255.0) - 0.5) * contrast) + 0.5) * 255.0;
            double b = ((((c.B / 255.0) - 0.5) * contrast) + 0.5) * 255.0;
            return System.Drawing.Color.FromArgb(
                Math.Clamp((int)r, 0, 255),
                Math.Clamp((int)g, 0, 255),
                Math.Clamp((int)b, 0, 255)
            );
        });
    }
    public static void CompressImage(string path, int quality)
    {
        using (System.Drawing.Image image = System.Drawing.Image.FromFile(path)) {
            System.Drawing.Imaging.ImageCodecInfo jpgEncoder = GetEncoder(System.Drawing.Imaging.ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            System.Drawing.Imaging.EncoderParameters myEncoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
            myEncoderParameters.Param[0] = new System.Drawing.Imaging.EncoderParameter(myEncoder, (long)quality);
            string tempPath = path + ".tmp";
            image.Save(tempPath, jpgEncoder, myEncoderParameters);
            image.Dispose();
            System.IO.File.Delete(path);
            System.IO.File.Move(tempPath, path);
        }
    }
    public static void ApplyBlur(string path, int radius)
    {
        using (System.Drawing.Image img = System.Drawing.Image.FromFile(path)) {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
            System.Drawing.Bitmap blurred = new System.Drawing.Bitmap(bmp.Width, bmp.Height);
            for (int x = 0; x < bmp.Width; x++) {
                for (int y = 0; y < bmp.Height; y++) {
                    int avgR = 0, avgG = 0, avgB = 0, count = 0;
                    for (int kx = -radius; kx <= radius; kx++) {
                        for (int ky = -radius; ky <= radius; ky++) {
                            int nx = x + kx;
                            int ny = y + ky;
                            if (nx >= 0 && nx < bmp.Width && ny >= 0 && ny < bmp.Height) {
                                System.Drawing.Color pixel = bmp.GetPixel(nx, ny);
                                avgR += pixel.R; avgG += pixel.G; avgB += pixel.B;
                                count++;
                            }
                        }
                    }
                    blurred.SetPixel(x, y, System.Drawing.Color.FromArgb(avgR / count, avgG / count, avgB / count));
                }
            }
            img.Dispose();
            System.IO.File.Delete(path);
            blurred.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
    private static System.Drawing.Imaging.ImageCodecInfo GetEncoder(System.Drawing.Imaging.ImageFormat format)
    {
        System.Drawing.Imaging.ImageCodecInfo[] codecs = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
        foreach (System.Drawing.Imaging.ImageCodecInfo codec in codecs) {
            if (codec.FormatID == format.Guid) return codec;
        }
        return null;
    }
}
public static class AiUtils
{
    public static double Exp(double x) => Math.Exp(x);
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
    public static double[] LayerNormalization(double[] data, double epsilon = 1e-8)
    {
        double mean = data.Average();
        double variance = data.Select(v => Math.Pow(v - mean, 2)).Average();
        double stdDev = Math.Sqrt(variance + epsilon);
        double[] normalized = new double[data.Length];
        for (int i = 0; i < data.Length; i++) {
            normalized[i] = (data[i] - mean) / stdDev;
        }
        return normalized;
    }
    public static double[] ApplyDropout(double[] data, double dropoutRate)
    {
        Random rnd = new Random();
        double[] result = new double[data.Length];
        double scale = 1.0 / (1.0 - dropoutRate);
        for (int i = 0; i < data.Length; i++) {
            if (rnd.NextDouble() > dropoutRate) {
                result[i] = data[i] * scale;
            }
            else {
                result[i] = 0;
            }
        }
        return result;
    }
    public static double[] GenerateHeInitialization(int inputNodes)
    {
        Random rnd = new Random();
        double stdDev = Math.Sqrt(2.0 / inputNodes);
        double[] weights = new double[inputNodes];
        for (int i = 0; i < inputNodes; i++) {
            double u1 = 1.0 - rnd.NextDouble();
            double u2 = 1.0 - rnd.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            weights[i] = randStdNormal * stdDev;
        }
        return weights;
    }
    public static double CalculateCosineSimilarity(double[] vectorA, double[] vectorB)
    {
        if (vectorA.Length != vectorB.Length) throw new ArgumentException("Length mismatch");
        double dotProduct = 0;
        double normA = 0;
        double normB = 0;
        for (int i = 0; i < vectorA.Length; i++) {
            dotProduct += vectorA[i] * vectorB[i];
            normA += vectorA[i] * vectorA[i];
            normB += vectorB[i] * vectorB[i];
        }
        double denominator = Math.Sqrt(normA) * Math.Sqrt(normB);
        if (denominator < 1e-10) return 0;

        return dotProduct / denominator;
    }
    public static double SigmoidDerivative(double sigmoidResult)
    {
        return sigmoidResult * (1.0 - sigmoidResult);
    }
    public static double TanhDerivative(double tanhResult)
    {
        return 1.0 - Math.Pow(tanhResult, 2);
    }
    public static double[] AddArrays(double[] a, double[] b)
    {
        if (a.Length != b.Length) throw new ArgumentException("Розміри мають збігатися");
        return a.Zip(b, (x, y) => x + y).ToArray();
    }
    public static double[] MultiplyArrays(double[] a, double[] b)
    {
        if (a.Length != b.Length) throw new ArgumentException("Розміри мають збігатися");
        return a.Zip(b, (x, y) => x * y).ToArray();
    }
    public static double[] Softmax(double[] values)
    {
        double maxVal = values.Max();
        double[] expValues = values.Select(v => Math.Exp(v - maxVal)).ToArray();
        double sum = expValues.Sum();
        return expValues.Select(v => v / sum).ToArray();
    }
    public static List<T> TopologicalSort<T>(T root, Func<T, IEnumerable<T>> getChildren)
    {
        var topo = new List<T>();
        var visited = new HashSet<T>();
        void BuildTopo(T v) {
            if (!visited.Contains(v)) {
                visited.Add(v);
                foreach (var child in getChildren(v)) {
                    if (child != null) BuildTopo(child);
                }
                topo.Add(v);
            }
        }

        BuildTopo(root);
        topo.Reverse();
        return topo;
    }
    public static double[] CreateRandomWeights(int rows, int cols)
    {
        Random rnd = new Random();
        double scale = Math.Sqrt(2.0 / (rows + cols));
        double[] weights = new double[rows * cols];
        for (int i = 0; i < weights.Length; i++) {
            weights[i] = (rnd.NextDouble() * 2 - 1) * scale;
        }
        return weights;
    }
    public static double[] ComputeGate(double[] x, double[] h, double[] W, double[] U, double[] b, int hSize, bool isSigmoid)
    {
        int inSize = x.Length;
        double[] res = new double[hSize];
        for (int i = 0; i < hSize; i++) {
            double sum = b[i];
            for (int j = 0; j < inSize; j++) sum += W[i * inSize + j] * x[j];
            for (int j = 0; j < hSize; j++) sum += U[i * hSize + j] * h[j];
            res[i] = isSigmoid ? Sigmoid(sum) : Tanh(sum);
        }
        return res;
    }
    public static (double[] h_next, double[] c_next) LstmStep( double[] x, double[] h_prev, double[] c_prev, double[][] weights, double[][] uWeights, double[][] biases, int hSize)
    {
        double[] f = ComputeGate(x, h_prev, weights[0], uWeights[0], biases[0], hSize, true);
        double[] i = ComputeGate(x, h_prev, weights[1], uWeights[1], biases[1], hSize, true);
        double[] c_tilde = ComputeGate(x, h_prev, weights[2], uWeights[2], biases[2], hSize, false);
        double[] o = ComputeGate(x, h_prev, weights[3], uWeights[3], biases[3], hSize, true);

        double[] c_next = new double[hSize];
        double[] h_next = new double[hSize];

        for (int j = 0; j < hSize; j++)
        {
            c_next[j] = (f[j] * c_prev[j]) + (i[j] * c_tilde[j]);
            h_next[j] = o[j] * Tanh(c_next[j]);
        }

        return (h_next, c_next);
    }
    public static double[] CalculateExpertScores(double[] hiddenState, double[] gatingWeights, int numExperts)
    {
        int hSize = hiddenState.Length;
        double[] scores = new double[numExperts];
        for (int e = 0; e < numExperts; e++) {
            double sum = 0;
            for (int j = 0; j < hSize; j++) {
                sum += hiddenState[j] * gatingWeights[e * hSize + j];
            }
            scores[e] = Sigmoid(sum);
        }
        return scores;
    }
    public static double CalculateBlendedCorrection(double[] hiddenState, Dictionary<int, double[]> expertParams, double[] expertWeights)
    {
        double blended = 0;
        int hSize = hiddenState.Length;
        for (int e = 0; e < expertWeights.Length; e++) {
            if (expertParams.ContainsKey(e)) {
                double expertOpinion = 0;
                for (int j = 0; j < hSize; j++) {
                    expertOpinion += hiddenState[j] * expertParams[e][j];
                }
                blended += expertOpinion * expertWeights[e];
            }
        }
        return blended;
    }
    public static double CalculateCoreDelta(double[] hiddenState, double[] outWeights, double outBias, double[] intuition)
    {
        double delta = outBias;
        for (int j = 0; j < hiddenState.Length; j++) {
            delta += hiddenState[j] * (outWeights[j] + intuition[j]);
        }
        return delta;
    }
    public static Dictionary<int, double[]> InitPatternExperts(int numTypes, int hSize)
    {
        var experts = new Dictionary<int, double[]>();
        for (int i = 0; i < numTypes; i++) {
            experts[i] = new double[hSize];
        }
        return experts;
    }
    public static double[][] GenerateData(int n)
    {
        Random rnd = new Random();
        double[][] data = new double[n][];
        for (int i = 0; i < n; i++) {
            data[i] = new double[] { rnd.NextDouble(), rnd.NextDouble() };
        }
        return data;
    }
    public static (double[][] centers, int[] labels) KMeans(double[][] data, int k, int maxIter = 100)
    {
        int n = data.Length;
        int dims = data[0].Length;
        Random rnd = new Random();
        double[][] centers = new double[k][];
        for (int i = 0; i < k; i++) centers[i] = (double[])data[rnd.Next(n)].Clone();
        int[] labels = new int[n];
        bool changed = true;
        int iter = 0;
        while (changed && iter < maxIter) {
            changed = false;
            iter++;
            for (int i = 0; i < n; i++) {
                int bestCluster = 0;
                double minDist = double.MaxValue;
                for (int j = 0; j < k; j++) {
                    double dist = EuclideanDistance(data[i], centers[j]);
                    if (dist < minDist) {
                        minDist = dist;
                        bestCluster = j;
                    }
                }
                if (labels[i] != bestCluster) {
                    labels[i] = bestCluster;
                    changed = true;
                }
            }
            double[][] newCenters = new double[k][];
            int[] counts = new int[k];
            for (int i = 0; i < k; i++) newCenters[i] = new double[dims];
            for (int i = 0; i < n; i++) {
                int cluster = labels[i];
                counts[cluster]++;
                for (int d = 0; d < dims; d++) newCenters[cluster][d] += data[i][d];
            }
            for (int j = 0; j < k; j++) {
                if (counts[j] > 0)
                    for (int d = 0; d < dims; d++) centers[j][d] = newCenters[j][d] / counts[j];
            }
        }
        return (centers, labels);
    }
    public static double EvaluateClustering(double[][] data, int[] labels, double[][] centers)
    {
        double totalDist = 0;
        for (int i = 0; i < data.Length; i++) {
            totalDist += EuclideanDistance(data[i], centers[labels[i]]);
        }
        return totalDist / data.Length;
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
    public static void SaveConfig<T>(string path, T config)
    {
        string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, json);
    }
    public static T LoadConfig<T>(string path)
    {
        if (!File.Exists(path))
        {
            return default(T);
        }
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<T>(json);
    }
    public static string GetAppSetting(string key)
    {
        return Environment.GetEnvironmentVariable(key) ?? "Not Found";
    }
}
public static class ConsoleVisuals
{
    public static void DrawProgressBar(int progress, int total, int width = 20)
    {
        double percentage = (double)progress / total;
        int filledWidth = (int)(percentage * width);

        Console.Write("\r[");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(new string('█', filledWidth));
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(new string('░', width - filledWidth));
        Console.ResetColor();
        Console.Write($"] {(percentage * 100):F0}%");
    }
    public static void WriteInBox(string text)
    {
        int length = text.Length;
        string horizontal = new string('═', length + 2);

        Console.WriteLine($"╔{horizontal}╗");
        Console.WriteLine($"║ {text} ║");
        Console.WriteLine($"╚{horizontal}╝");
    }
    public static void ClearLine(int row)
    {
        int currentCursorTop = Console.CursorTop;
        Console.SetCursorPosition(0, row);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentCursorTop);
    }
}
public static class AutomationUtils
{
    [DllImport("user32.dll")]
    private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int x, int y);
    [DllImport("user32.dll")]
    private static extern void SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    public static void SendKeyPress(short keyCode)
    {
        keybd_event((byte)keyCode, 0, 0, 0); // Натиснути
        keybd_event((byte)keyCode, 0, 2, 0); // Відпустити
    }
    public static void SetMousePosition(int x, int y)
    {
        SetCursorPos(x, y);
    }
    public static void MinimizeAll()
    {
        keybd_event(0x5B, 0, 0, 0); // Left Windows key
        keybd_event(0x44, 0, 0, 0); // 'D' key
        keybd_event(0x44, 0, 2, 0); // Release 'D'
        keybd_event(0x5B, 0, 2, 0); // Release Win
    }
}
public static class UnitConverter
{
    public static double KmHtoMs(double speed)
    {
        return speed / 3.6;
    }
    public static double ConvertCurrency(double amount, double rate)
    {
        return amount * rate;
    }
}

/*
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
*/