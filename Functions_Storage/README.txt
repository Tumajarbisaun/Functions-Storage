=====================================================================================================
  
   ________  ___  ___  ________   ________  _________  ___  ________  ________   ________      
  |\  _____\|\  \|\  \|\   ___  \|\   ____\|\___   ___\\  \|\   __  \|\   ___  \|\   ____\     
  \ \  \__/ \ \  \\\  \ \  \\ \  \ \  \___|\|___ \  \_\ \  \ \  \|\  \ \  \\ \  \ \  \___|_    
   \ \   __\ \ \  \\\  \ \  \\ \  \ \  \       \ \  \ \ \  \ \  \\\  \ \  \\ \  \ \_____  \   
    \ \  \_|  \ \  \\\  \ \  \\ \  \ \  \____   \ \  \ \ \  \ \  \\\  \ \  \\ \  \|____|\  \  
     \ \__\    \ \_______\ \__\\ \__\ \_______\  \ \__\ \ \__\ \_______\ \__\\ \__\____\_\  \ 
      \|__|     \|_______|\|__| \|__|\|_______|   \|__|  \|__|\|_______|\|__| \|__|\_________\
                                                                                  \|_________|
                                                                                              
   ________  _________  ________  ________  ________  ________  _______                       
  |\   ____\|\___   ___\\   __  \|\   __  \|\   __  \|\   ____\|\  ___ \                      
  \ \  \___|\|___ \  \_\ \  \|\  \ \  \|\  \ \  \|\  \ \  \___|\ \   __/|                     
   \ \_____  \   \ \  \ \ \  \\\  \ \   _  _\ \   __  \ \  \  __\ \  \_|/__                   
    \|____|\  \   \ \  \ \ \  \\\  \ \  \\  \\ \  \ \  \ \  \|\  \ \  \_|\ \                  
      ____\_\  \   \ \__\ \ \_______\ \__\\ _\\ \__\ \__\ \_______\ \_______\                 
     |\_________\   \|__|  \|_______|\|__|\|__|\|__|\|__|\|_______|\|_______|                 
     \|_________|                                                                             
                                                                                              
                                [ PERSONAL SDK LIBRARY ]
=====================================================================================================
   > Version:         4.0.0 (Mega-Library Edition)
   > Lead Developer:  Nazar Luchynin (Luchynin N.V.)
   > Status:          Active Development / Production Ready
   > Environment:     .NET 8.0 / C# 12 / Cross-Platform Core
   > Repository:      Functions_Storage Distribution
   > Created:         November 19, 2025
   > Updated:         April 22, 2026
=====================================================================================================

--- [ PROJECT VISION & PHILOSOPHY ] -----------------------------------------------------------------
Functions_Storage is not just a collection of snippets; it is a high-performance, universal C# 
toolkit (SDK) meticulously engineered to bridge the gap between academic complexity and practical 
application. This library serves as a unified backbone for diverse technical domains:

   * GAME DEVELOPMENT: Advanced RPG mechanics, procedural generation, and physics-based math.
   * CYBERSECURITY: Network diagnostics, cryptographically secure RNG, and data obfuscation.
   * ARTIFICIAL INTELLIGENCE: Core NN layers, clustering, and dataset optimization algorithms.
   * NUMERICAL ANALYSIS: Specialized SLAE solvers, integration, and differential equations.
   * SYSTEM AUTOMATION: Low-level OS interaction and robust file-system management.

--- [ CORE ARCHITECTURE ] ---------------------------------------------------------------------------
The library is built on the "Zero-Dependency" principle. By utilizing only native .NET namespaces, 
Functions_Storage ensures lightning-fast execution and 100% portability. Whether you are 
deploying on a high-end Windows workstation, a specialized BlackArch Linux terminal, or an 
embedded system, the SDK remains stable, lightweight, and efficient.

Key design patterns include:
   - STATIC PROVIDERS: Instant access to utilities without memory-intensive instantiation.
   - GENERIC ABSTRACTION: Support for all data types through C# Generics <T>.
   - SAFETY FIRST: Strict input validation and exception handling for production stability.

--- [ API REFERENCE & METHOD CATALOG ] --------------------------------------------------------------
  This section provides a detailed breakdown of 160+ available methods. Each entry is optimized 
  for readability and includes technical metadata to assist in rapid integration.
-----------------------------------------------------------------------------------------------------

[ LEGEND ]
  (f) - Functional approach (Lambdas/Delegates support)
  (g) - Generic-ready (Supports any Type <T>)
  (m) - Mathematical/Numerical core algorithm
  (i) - In-place transformation (Memory-efficient, modifies original)

=====================================================================================================

[ CLASS: ArrayUtils ]
---------------------------------------------------------------------------
Description: A core utility class for advanced array operations. 
Includes sorting, functional filtering, and statistical analysis.
---------------------------------------------------------------------------

--- [ SEARCH & STATISTICS ] ---

01. GetMax(double[] array)
    - Description: Finds the highest value in a numeric array.
    - Safety: Throws ArgumentException if the array is null or empty.

02. GetMin(double[] array)
    - Description: Finds the lowest value in a numeric array.
    - Safety: Throws ArgumentException if the array is null or empty.

03. GetAverage(double[] array)
    - Description: Calculates the arithmetic mean.
    - Returns: 0 if empty; otherwise the average value.

04. FindIndex<T>(T[] array, T value) (g)
    - Description: Searches for an element and returns its 0-based index.
    - Returns: Index of the first occurrence or -1 if not found.

05. Contains<T>(T[] array, T value) (g)
    - Description: Checks if the specified value exists in the collection.

--- [ MODIFICATION & ORDERING ] ---

06. CustomSort(int[] array) (i)
    - Description: Standard Bubble Sort implementation for integer arrays.
    - Note: In-place transformation.

07. Shuffle<T>(T[] array) (g)(i)
    - Description: Randomizes element positions using Fisher-Yates logic.
    - Context: Essential for procedural generation and game mechanics.

08. Reverse<T>(T[] array) (g)(i)
    - Description: Flips the array order (first becomes last).

09. Fill<T>(T[] array, T value) (g)(i)
    - Description: Overwrites every element with the provided value.

--- [ TRANSFORMATION & GENERATION ] ---

10. Slice<T>(T[] array, int start, int end) (g)
    - Description: Extracts a portion of the array.
    - Safety: Auto-clamps 'start' and 'end' to prevent overflow.

11. Merge<T>(T[] array1, T[] array2) (g)
    - Description: Combines two arrays into a single new collection.

12. Distinct<T>(T[] array) (g)
    - Description: Removes all duplicate entries using a HashSet.

13. GetRandomElement<T>(T[] array) (g)
    - Description: Returns a random item from the array.

--- [ FUNCTIONAL PROGRAMMING ] ---

14. Filter<T>(T[] array, Func<T, bool> predicate) (f)(g)
    - Description: Returns elements that satisfy a specific condition.

15. Map<T, TResult>(T[] array, Func<T, TResult> selector) (f)(g)
    - Description: Transforms each element into a new type/form.

16. Join<T>(T[] array, string separator) (g)
    - Description: Converts an array to a single string with delimiters.

17. All<T>(T[] array, Func<T, bool> predicate) (f)(g)
    - Description: True if ALL elements pass the condition.

18. Any<T>(T[] array, Func<T, bool> predicate) (f)(g)
    - Description: True if AT LEAST ONE element passes the condition.

19. Count<T>(T[] array, Func<T, bool> predicate) (f)(g)
    - Description: Returns the number of elements matching the predicate.

20. AreEqual<T>(T[] array1, T[] array2) (g)
    - Description: Performs a deep equality check (length and content).

===========================================================================

[ CLASS: MathUtils ]
---------------------------------------------------------------------------
Description: The core computational engine of the SDK. Contains basic 
arithmetic, high-level numerical solvers, and stochastic algorithms.
---------------------------------------------------------------------------

--- [ BASIC ARITHMETIC & NUMBER THEORY ] ---

21. IsEven(int number)
    - Description: Returns true if the number is divisible by 2.

22. Factorial(int n)
    - Description: Calculates n! iteratively.
    - Safety: Throws ArgumentException for negative inputs.

23. IsPrime(int number)
    - Description: Efficiently checks if a number is prime using square 
      root boundary and skip-even logic.

24. GetGCD(int a, int b) / GetLCM(int a, int b)
    - Description: Calculates Greatest Common Divisor (Euclidean algorithm) 
      and Least Common Multiple.

25. IsPowerOfTwo(int n)
    - Description: Fast bitwise check to determine if n is a power of 2.

--- [ GEOMETRY & TRIGONOMETRY ] ---

26. GetHypotenuse(double a, double b)
    - Description: Calculates the hypotenuse using the Pythagorean theorem.

27. Round(double value, int digits)
    - Description: Standard rounding wrapper for System.Math.

--- [ LINEAR ALGEBRA (SLAE SOLVERS) ] ---

28. SolveGauss(double[,] A, double[] B) (m)
    - Description: Direct solver for linear systems using Gaussian 
      elimination with partial pivoting.

29. SolveGaussSeidel(double[,] A, double[] f, double eps) (m)
    - Description: Iterative SLAE solver. Faster convergence than Jacobi.

30. SolveJacobi(double[,] A, double[] f, double eps) (m)
    - Description: Classic iterative solver. Stable for diagonally 
      dominant matrices.

31. CalculateResidual(double[,] A, double[] x, double[] f) (m)
    - Description: Calculates the error vector (Ax - f) to verify accuracy.

--- [ NUMERICAL ANALYSIS & CALCULUS ] ---

32. NewtonMethod(Func f, Func df, double x0, double eps) (f)(m)
    - Description: Finds function roots f(x)=0 using derivatives.

33. SimpleIteration(Func g, double x0, double eps) (f)(m)
    - Description: Solves x=g(x) through repeated substitution.

34. IntegrateTrapezoidal / IntegrateSimpson (f)(m)
    - Description: Numerical integration. Simpson's rule provides 
      higher accuracy for smooth functions.

35. SolveEuler / SolveRungeKutta4 (f)(m)
    - Description: ODE solvers (y'=f(x,y)). RK4 is the industry 
      standard for physics engines and simulations.

--- [ INTERPOLATION & REGRESSION ] ---

36. InterpolateLagrange / InterpolateNewton / InterpolateLinearSpline (m)
    - Description: Various methods to estimate values between known points.

37. EvaluatePolynomialHorner(double[] coeff, double x) (m)
    - Description: Optimized polynomial evaluation (minimizes multiplications).

38. LeastSquares(double[] x, double[] y) (m)
    - Description: Linear regression (y = ax + b) that minimizes the sum 
      of squared residuals.

39. GetPearsonCorrelation(double[] x, double[] y) (m)
    - Description: Measures the linear correlation strength (-1 to 1).

--- [ STOCHASTIC & AI ALGORITHMS ] ---

40. MonteCarloPi(int points) (m)
    - Description: Estimates PI using random point distribution.

41. SimulatedAnnealing(Func f, double startX, ...) (f)(m)
    - Description: Global optimization algorithm inspired by metallurgy.

42. AntColonyOptimization(double[,] dist, ...) (m)
    - Description: Bio-inspired heuristic for solving the Traveling 
      Salesman Problem (TSP).

--- [ UTILITY & GAMEDEV MATH ] ---

43. GetRandomInt / GetRandomDouble
    - Description: Thread-safe wrappers for generating random values.

44. Clamp(double value, double min, double max)
    - Description: Restricts a value within a specified range.

45. Lerp(double start, double end, double amount)
    - Description: Linear interpolation between two values (0.0 to 1.0).

46. Map(double val, double sMin, double sMax, double tMin, double tMax)
    - Description: Re-maps a number from one range to another.

===========================================================================

[ CLASS: StringUtils ]
---------------------------------------------------------------------------
Description: A collection of methods for string manipulation, cleaning, 
and validation. Essential for UI development and data parsing.
---------------------------------------------------------------------------

--- [ MANIPULATION & FORMATTING ] ---

47. Reverse(string text)
    - Description: Inverts the character sequence of a string.
    - Safety: Handles null or empty strings gracefully.

48. Capitalize(string text)
    - Description: Converts the first character to uppercase and the 
      rest to lowercase (e.g., "nazar" -> "Nazar").

49. Truncate(string text, int maxLength)
    - Description: Cuts a string to a specified length and appends "..." 
      if it exceeds the limit. Ideal for UI previews.

50. RemoveWhitespace(string text)
    - Description: Completely strips all whitespace characters from the 
      string (spaces, tabs, newlines).

--- [ ANALYSIS & VALIDATION ] ---

51. CountWords(string text)
    - Description: Returns the total number of words, accounting for 
      various delimiters (spaces, tabs, newlines).

52. IsPalindrome(string text)
    - Description: Checks if a string reads the same forwards and backwards.
    - Logic: Case-insensitive and ignores internal spaces.

53. IsNumeric(string text)
    - Description: Validates if the string consists entirely of digits.
    - Context: Useful for quick input validation before parsing.

--- [ SECURITY & CLEANING ] ---

54. Sanitize(string text)
    - Description: Performs basic text "cleaning" by trimming edges and 
      removing HTML/XML tags using Regex.
    - Context: Basic protection for data display or logging.

===========================================================================
[ CLASS: NetworkUtils ]
---------------------------------------------------------------------------
Description: Essential network tools for connectivity testing, hardware 
identification, and remote management.
---------------------------------------------------------------------------

--- [ CONNECTIVITY & PING ] ---

55. PingHost(string ipAddress)
    - Description: Sends an ICMP echo request to a target IP.
    - Returns: True if host is reachable (within 1s timeout).

56. IsUrlReachable(string url)
    - Description: Checks if a web server is up by sending an HTTP GET.
    - Context: Fast website/API availability monitoring.

57. IsPortOpen(string host, int port, int timeoutMs)
    - Description: Attempts a TCP connection to a specific port.
    - Context: Core logic for building a custom Port Scanner.

--- [ IP & HARDWARE ADDRESSING ] ---

58. GetLocalIpAddress()
    - Description: Retrieves the current internal IPv4 of the machine.
    - Default: Returns "127.0.0.1" if no interface is found.

59. GetPublicIpAddress()
    - Description: Fetches the external (WAN) IP via the ipify service.

60. GetMacAddress()
    - Description: Returns the physical (MAC) address of the primary 
      active network interface.

61. ResolveDns(string hostName)
    - Description: Performs a DNS lookup to find the IP of a domain.

--- [ INTERFACE & HARDWARE STATUS ] ---

62. IsWifiConnected()
    - Description: Specifically checks if the machine is using a 
      Wireless (802.11) connection.

63. GetNetworkSpeed()
    - Description: Returns the theoretical link speed in bits per second.

--- [ REMOTE MANAGEMENT ] ---

64. WakeOnLan(string macAddress)
    - Description: Sends a "Magic Packet" to a target MAC address via 
      UDP broadcast (Port 9) to wake up a computer on the local network.
    - Context: Hardware automation and remote lab management.

===========================================================================

[ CLASS: SecurityUtils ]
---------------------------------------------------------------------------
Description: Advanced security module for encryption, hashing, and 
cryptographically secure random generation.
---------------------------------------------------------------------------

--- [ CRYPTOGRAPHY & HASHING ] ---

65. HashString(string input)
    - Description: Computes a SHA-256 hash of the input string.
    - Output: A 64-character hexadecimal string.
    - Context: Ideal for storing file checksums or verifying data integrity.

66. Encrypt(string plainText, string key)
    - Description: Symmetric encryption using the AES (Advanced Encryption 
      Standard) algorithm. The key is automatically hashed with SHA-256 
      to ensure correct bit-length.
    - Security: Uses a fixed IV for simplicity (recommended to upgrade to 
      random IV for high-stakes production).

--- [ SECURE GENERATION ] ---

67. GeneratePassword(int length)
    - Description: Creates a complex password including uppercase, lowercase, 
      digits, and special characters.
    - Note: Powered by RandomNumberGenerator for true randomness.

68. GetSecureRandomInt(int min, int max)
    - Description: Generates a cryptographically strong random integer 
      within a specified range.

69. GenerateToken(int length)
    - Description: Generates a secure Base64-encoded token.
    - Context: Authentication systems, session IDs, or unique identifiers.

70. GenerateSaltBytes(int length)
    - Description: Generates a raw byte array of secure random data.
    - Context: Necessary for password salting to prevent rainbow table attacks.

--- [ VALIDATION ] ---

71. CheckPasswordStrength(string password)
    - Description: Evaluates a password and returns a score from 0 to 5.
    - Criteria: Length (8/12 chars), casing, digits, and special characters.

===========================================================================

[ CLASS: FileUtils ]
---------------------------------------------------------------------------
Description: Streamlined file system operations including smart writing, 
file analysis, and batch management.
---------------------------------------------------------------------------

--- [ WRITING & DIRECTORY MANAGEMENT ] ---

72. SmartWrite(string path, string content)
    - Description: Writes text to a file. Unlike standard methods, it 
      automatically creates the entire directory path if it doesn't exist.
    - Context: Prevents DirectoryNotFoundException during logging or export.

73. EnsureDirectory(string path)
    - Description: Verifies the existence of a directory and creates it 
      if necessary. Safe to call multiple times.

--- [ FILE ANALYSIS ] ---

74. GetFriendlyFileSize(string path)
    - Description: Converts file size in bytes to a human-readable format 
      (e.g., "1.2 MB", "450.0 KB").
    - Supports: B, KB, MB, GB, TB.

75. GetFileHash(string path)
    - Description: Generates a SHA-256 checksum for a file.
    - Context: Essential for verifying file integrity or detecting 
      unauthorized modifications (Anti-tamper).

76. IsFileLocked(string path)
    - Description: Checks if a file is currently being used by another 
      process or is otherwise inaccessible for writing.

77. GetLastModified(string path)
    - Description: Retrieves the exact date and time of the last write 
      operation on a file.

--- [ BATCH OPERATIONS & BACKUP ] ---

78. BackupFile(string path)
    - Description: Creates a copy of the target file with a ".bak" 
      extension. Overwrites existing backups.

79. GetFilesByExtension(string directory, string extension)
    - Description: Scans a directory and returns a list of files matching 
      the specified extension (e.g., "txt" or ".json").

80. BatchRename(string directory, string pattern, string replacement)
    - Description: Scans a directory and replaces specific substrings 
      in all filenames.
    - Context: Organizing large datasets or logs.

===========================================================================

[ CLASS: ColorUtils ]
---------------------------------------------------------------------------
Description: Comprehensive console UI toolkit for color-coded logging, 
text highlighting, and visual effects.
---------------------------------------------------------------------------

--- [ STANDARDIZED LOGGING ] ---

81. WriteColored(string message, ConsoleColor color)
    - Description: Prints text in a specific color and resets back to default.
    - Safety: Ensures the console doesn't "stay" in the selected color.

82. WriteError / WriteSuccess / WriteWarning / WriteInfo
    - Description: Shortcut methods for standardized system messages.
    - Colors: Red (Error), Green (Success), Yellow (Warning), Cyan (Info).
    - Format: Includes a prefix tag (e.g., [ERROR] Message).

--- [ VISUAL EFFECTS ] ---

83. WriteRainbow(string message)
    - Description: Cycles through a spectrum of colors for each character.
    - Context: Great for fun easter eggs or "elite" hacker-style branding.

84. PrintGradient(string text, ConsoleColor start, ConsoleColor end)
    - Description: Alternates colors for every character to create a 
      stepped visual transition.

85. HighlightText(string text, string target, ConsoleColor highlightColor)
    - Description: Scans a string and changes the color of specific keywords.
    - Context: Essential for log analysis or highlighting search results.

--- [ UTILITIES ] ---

86. GetRandomColor()
    - Description: Picks a random ConsoleColor (skips Black/Default).
    - Context: Dynamic UI elements or randomized visualizations.

87. ResetColor()
    - Description: Manual override to restore default console colors.

===========================================================================

[ CLASS: TimeUtils ]
---------------------------------------------------------------------------
Description: Comprehensive time-management tools for formatting, 
conversions, and date logic analysis.
---------------------------------------------------------------------------

--- [ FORMATTING & TIMESTAMPS ] ---

88. GetTimestamp()
    - Description: Returns the current system time in a standardized 
      format (yyyy-MM-dd HH:mm:ss).
    - Context: Essential for logging events and database entries.

89. FormatTimeSpan(TimeSpan duration)
    - Description: Converts a duration into a human-readable string 
      (e.g., "2d 4h 30m 15s").

90. GetTimeAgo(DateTime dateTime)
    - Description: Calculates the relative time passed since a specific date.
    - Output: "just now", "5 minutes ago", "3 days ago", etc.
    - Context: Dynamic UI elements and social media-style feeds.

--- [ DATE LOGIC & ANALYSIS ] ---

91. GetAge(DateTime birthDate)
    - Description: Accurately calculates age based on the current date, 
      accounting for the day and month of birth.

92. IsWeekend(DateTime date)
    - Description: Checks if the provided date falls on a Saturday or Sunday.

93. GetNextOccurrence(DayOfWeek day)
    - Description: Finds the date of the next upcoming specific day of 
      the week (e.g., the next Monday).

94. IsLeapYear(int year) / GetDaysInMonth(int year, int month)
    - Description: Wrappers for native .NET logic to determine leap years 
      and specific monthly day counts.

95. GetQuarter(DateTime date)
    - Description: Identifies which fiscal/calendar quarter (1-4) the 
      date belongs to.

--- [ SYSTEM CONVERSIONS ] ---

96. UnixTimestampToDateTime(long unixTime)
    - Description: Converts a standard Unix Epoch timestamp (seconds) 
      into a .NET DateTime object.
    - Context: Critical for cross-platform network communication.

97. DateTimeToUnixTimestamp(DateTime dateTime)
    - Description: Encodes a .NET DateTime into a Unix Epoch long value.

===========================================================================

[ CLASS: ValidationUtils ]
---------------------------------------------------------------------------
Description: Input validation logic for ensuring data integrity and 
conforming to standard security formats.
---------------------------------------------------------------------------

--- [ DATA FORMAT VALIDATION ] ---

98. IsValidEmail(string email)
    - Description: Uses Regex to verify if a string follows the standard 
      email format (user@domain.com).
    - Logic: Case-insensitive, checks for '@' and domain dots.

99. IsValidPhoneNumber(string number)
    - Description: Validates international phone numbers (E.164 format).
    - Note: Supports optional '+' prefix and 1-14 digits.

100. IsUrl(string url)
     - Description: Checks if a string is a valid absolute URL.
     - Support: Specifically validates for HTTP and HTTPS schemes.

--- [ CONTENT & SECURITY CHECKS ] ---

101. IsSecurePassword(string password)
     - Description: Strict check for password requirements.
     - Criteria: Min 8 characters, must include Upper, Lower, and Digit.

102. IsOnlyLetters(string text)
     - Description: Ensures the string contains only alphabetic characters.
     - Context: Useful for Name/Surname field validation.

103. IsValidBirthDate(DateTime date)
     - Description: Validates if a birth date is realistic (post-1900 
       and not in the future).

===========================================================================

[ CLASS: ConverterUtils ]
---------------------------------------------------------------------------
Description: A versatile utility for data transformation, serialization, 
and unit conversion (Imperial/Metric/Binary).
---------------------------------------------------------------------------

--- [ SERIALIZATION & ENCODING ] ---

104. ToJson<T>(T obj) / FromJson<T>(string json) (g)
     - Description: Standard JSON serialization and deserialization. 
     - Context: Ideal for API communication and saving user profiles.

105. ToXml<T>(T obj) (g)
     - Description: Converts a generic object into an XML string.
     - Context: Legacy system support or structured configuration files.

106. ToBase64(string text) / FromBase64(string base64)
     - Description: Encodes and decodes strings into Base64 format.
     - Context: Safe transmission of binary-like data over text protocols.

--- [ UNIT CONVERSION ] ---

107. CelsiusToFahrenheit / FahrenheitToCelsius
     - Description: Temperature conversion logic for global applications.

108. KilometersToMiles / MilesToKilometers
     - Description: Distance conversion between Metric and Imperial systems.

109. DegreesToRadians / RadiansToDegrees
     - Description: Essential for trigonometry and rotating objects in 
       3D engines or game development.

110. BytesToReadable(long bytes)
     - Description: Translates raw byte counts into human-friendly strings 
       (e.g., "12.5 GB").

--- [ PARSING & TYPE CASTING ] ---

111. StringToInt(string value, int defaultValue)
     - Description: Safe integer parsing that returns a default value 
       instead of throwing an exception on failure.

112. StringToDateTime(string value, DateTime defaultValue)
     - Description: Safe DateTime parsing with fallback support for 
       malformed strings.

===========================================================================

[ CLASS: GameUtils ]
---------------------------------------------------------------------------
Description: A specialized module for game mechanics, RPG systems, 
and basic procedural generation.
---------------------------------------------------------------------------

--- [ DICE & RNG MECHANICS ] ---

113. RollDice(int sides) / RollSuccess(int chance)
     - Description: Simulates dice rolls (d6, d20, etc.) and percentage-based 
       success checks.

114. RollWithAdvantage / RollWithDisadvantage
     - Description: Standard D&D 5e mechanics (rolling two d20s and picking 
       the highest or lowest value).

115. Chance(double probability)
     - Description: Highly precise success check using double-precision values.

116. PickWeightedItem<T>(T[] items, double[] weights) (g)
     - Description: Randomly selects an item where each item has a different 
       probability (weight). Essential for loot drops and spawn rates.

--- [ COMBAT & PROGRESSION ] ---

117. CalculateDamage(double base, double defense, double multiplier)
     - Description: Computes final damage after armor mitigation. 
       Prevents negative results.

118. IsCriticalHit(int roll, int threshold)
     - Description: Checks if a die roll meets or exceeds the crit limit.

119. CalculateExperience(int enemyLv, int playerLv)
     - Description: Calculates XP gain based on the level gap between 
       the player and the target.

120. GetLevelFromExperience(long experience)
     - Description: Reverse-calculates player level using a square-root 
       progression formula.

121. CalculateManaRegen(double base, double intelligence)
     - Description: RPG-style stat calculation for resource recovery.

--- [ WORLD & ENTITIES ] ---

122. CalculateDistance / IsInRange
     - Description: 2D geometry checks for proximity and entity interaction.

123. DegreesToAngle(x1, y1, x2, y2)
     - Description: Calculates the angle between two points. Useful for 
       making projectiles face their target.

124. GenerateRandomName()
     - Description: Simple procedural generator that combines fantasy 
       prefixes and suffixes.

125. SpawnEntity(string id, double x, double y)
     - Description: Logs entity placement for debugging or command tracking.

126. ShakeScreen(int intensity, int duration)
     - Description: Simulates feedback (using Console.Beep frequency steps) 
       for combat or environmental effects.

--- [ COLLISION & LOGIC ] ---

127. GetRandomLoot(string[] lootTable)
     - Description: Picks a random item from a provided array.

128. CheckLineOfSight(x1, y1, x2, y2)
     - Description: Placeholder for visibility logic (Raycasting).

===========================================================================

[ CLASS: SystemUtils ]
---------------------------------------------------------------------------
Description: Direct interaction with the Operating System. Manage processes, 
system power states, hardware info, and environment variables.
---------------------------------------------------------------------------

--- [ HARDWARE & OS INFO ] ---

129. GetOsVersion() / GetDotNetVersion()
     - Description: Returns strings identifying the OS build and current 
       .NET Runtime version.

130. GetMachineName() / GetUserName()
     - Description: Retrieves the computer's network name and the current 
       active user profile.

131. GetTotalRam() / GetAvailableRam()
     - Description: Monitors memory usage. 
     - Note: GetAvailableRam currently tracks the Managed Heap (GC).

132. Is64BitOperatingSystem()
     - Description: Boolean check to determine system architecture.

133. GetSystemUptime()
     - Description: Returns a TimeSpan representing how long the system 
       has been running since the last boot.

--- [ SYSTEM CONTROL & AUTOMATION ] ---

134. OpenUrl(string url)
     - Description: Launches the default web browser to the specified address.

135. CopyToClipboard(string text)
     - Description: Injects text into the Windows Clipboard using a 
       stealthy PowerShell background command.

136. IsRunningAsAdmin()
     - Description: Security check to see if the current process has 
       Elevated (Administrator) privileges.

137. GetProcessId() / GetCurrentProcessPath()
     - Description: Returns metadata about the currently running instance 
       of the application.

--- [ POWER MANAGEMENT ] ---

138. ShutdownSystem() / RestartSystem() / SleepSystem()
     - Description: Executes system-level commands to change the power 
       state immediately.
     - Note: Use with caution in production environments.

--- [ CONFIGURATION ] ---

139. GetEnvironmentVariable(string var) / SetEnvironmentVariable(...)
     - Description: Reads or modifies system/process environment variables.

140. TakeScreenshot / SetSystemVolume / IsScreenLocked
     - Description: Placeholders for extended system interaction logic.

===========================================================================

[ CLASS: ImageUtils ]
---------------------------------------------------------------------------
Description: Professional image processing module. Supports resizing, 
filtering, metadata cleaning, and watermarking.
---------------------------------------------------------------------------

--- [ BASIC TRANSFORMATIONS ] ---

141. ResizeImage(string path, int w, int h)
     - Description: Changes image dimensions. 
     - Note: Replaces the original file with the resized version.

142. RotateImage(string path, float degrees) / FlipImage(...)
     - Description: Rotates (90, 180, 270) or flips (XY axes) the image.

143. CropImage(string path, int x, int y, int w, int h)
     - Description: Extracts a rectangular sub-section of the image.

--- [ FILTERS & EFFECTS ] ---

144. ApplyGrayscale / ApplySepia(string path)
     - Description: Professional color filters using standard luminosity 
       and sepia weighting algorithms.

145. AdjustBrightness / ApplyContrast(string path, int level)
     - Description: Fine-tunes image exposure and color depth.

146. InvertColors(string path)
     - Description: Creates a "negative" effect by inverting RGB values.

147. ApplyBlur(string path, int radius)
     - Description: Simple box-blur algorithm for softening images.

--- [ METADATA & ANALYSIS ] ---

148. GetImageDimensions(string path) / GetImageFormat(string path)
     - Description: Retrieves metadata without loading the entire bitmap 
       into memory.

149. GetDominantColor(string path)
     - Description: Analyzes pixels to find the average HEX color of the image.

150. RemoveExifData(string path)
     - Description: Strips all metadata (GPS, Camera info, timestamps). 
     - Context: Critical for privacy and security before uploading files.

--- [ PRODUCTION TOOLS ] ---

151. AddWatermark(string path, string text, int x, int y)
     - Description: Overlays a custom text string onto the image.

152. ConvertFormat(string path, string targetFormat)
     - Description: Changes file format (e.g., BMP to PNG or JPEG).

153. CompressImage(string path, int quality)
     - Description: Reduces JPEG file size by adjusting quality (1-100).

===========================================================================

[ CLASS: AiUtils ]
---------------------------------------------------------------------------
Description: Advanced Artificial Intelligence module. Implements neural 
network layers, clustering algorithms, and NLP metrics.
---------------------------------------------------------------------------

--- [ NEURAL NETWORKS CORE ] ---

154. Sigmoid / Relu / Tanh / Softmax (f)
     - Description: Standard activation functions for neural layers. 
     - Context: Essential for introducing non-linearity into models.

155. SigmoidDerivative / TanhDerivative (f)
     - Description: Derivatives used during backpropagation for weight 
       optimization.

156. LstmStep(...) (m)
     - Description: A single step calculation of a Long Short-Term Memory 
       cell. Manages Forget, Input, and Output gates.
     - Context: Used for sequential data analysis (text, time-series).

157. Dropout / LayerNormalization (f)
     - Description: Regularization techniques to prevent overfitting 
       and stabilize training.

158. GenerateHeInitialization(int inputNodes) (m)
     - Description: Optimized weight initialization for layers using ReLU.

--- [ CLUSTERING & UNSUPERVISED LEARNING ] ---

159. KMeans(double[][] data, int k) (m)
     - Description: Classic partitioning algorithm to group data into 
       K distinct clusters.

160. RunCentNN(...) (m)
     - Description: Implementation of Centroid Neural Network for 
       competitive learning and adaptive clustering.

161. GetKNearestNeighbors(double[] target, double[][] data, int k) (m)
     - Description: KNN algorithm for classification based on proximity.

162. EvaluateClustering(...) (m)
     - Description: Computes the average distance to centers to assess 
       cluster quality.

--- [ MATHEMATICS & VECTOR OPS ] ---

163. EuclideanDistance / CosineSimilarity (m)
     - Description: Metrics to measure similarity/distance between vectors.

164. DotProduct / MultiplyMatrices / Transpose (m)
     - Description: Fundamental linear algebra operations for AI training.

165. MeanSquaredError / CrossEntropy (m)
     - Description: Loss functions to measure model prediction accuracy.

--- [ DATA PREPROCESSING ] ---

166. Normalize / Standardize(double[] data)
     - Description: Scales data to [0,1] or Z-score (mean=0, std=1).

167. SplitData<T>(T[] data, double ratio) (g)
     - Description: Shuffles and divides datasets into Training and 
       Testing sets (default 80/20).

--- [ NATURAL LANGUAGE PROCESSING (NLP) ] ---

168. GetLevenshteinDistance(string s1, string s2)
     - Description: Measures "edit distance" between strings.
     - Context: Spell checkers and fuzzy string matching.

169. GetKeywords(string text, int count)
     - Description: Statistical keyword extraction based on term frequency.

=====================================================================================================
=====================================================================================================

--- [ DEPLOYMENT & INTEGRATION GUIDE ] --------------------------------------------------------------

1. COMPILATION:
   To package the SDK into a high-performance binary, execute the following in your CLI:
   > dotnet build -c Release --output ./bin/dist

2. LINKING:
   Add a direct reference to 'Functions_Storage.dll' within your target .NET solution. 
   For CLI-based projects, use:
   > dotnet add reference path/to/Functions_Storage.dll

3. IMPLEMENTATION WORKFLOW:
   All modules are globally accessible via static providers. No instantiation required.
   
   [ CODE EXAMPLE: SECURE AUDIT LOGGING ]
   -----------------------------------------------------------------------------------------------
   string secret = "Root_Access_2026";
   
   if (ValidationUtils.IsSecurePassword(secret)) 
   {
       // Encrypt and Hash for storage
       string encrypted = SecurityUtils.Encrypt(secret, "MasterKey_01");
       string integrityHash = SecurityUtils.HashString(encrypted);
       
       // System interaction and logging
       FileUtils.SmartWrite("sys/vault.db", encrypted);
       ColorUtils.WriteInfo($"[LOG] Node: {SystemUtils.GetMachineName()} | User: {SystemUtils.GetUserName()}");
       ColorUtils.WriteSuccess($"[DONE] Security layer synchronized at {TimeUtils.GetTimestamp()}");
   }
   -----------------------------------------------------------------------------------------------
  
  "In the world of fragments, a well-structured library is the only truth."
  This SDK is a testament to the pursuit of clean code and mathematical precision.

=====================================================================================================
                      [ SYSTEM HALTED. END OF DOCUMENT - SDK v4.0.0 ]
=====================================================================================================