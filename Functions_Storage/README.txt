===========================================================
          FUNCTIONS STORAGE - PERSONAL LIBRARY
===========================================================
Version: 4.0 (Mega-Library Edition)
Last Updated: April 10, 2026

--- [ PROJECT DESCRIPTION ] ---
Functions_Storage is a universal Software Development Kit (SDK) 
for C# development. This library was created to automate 
routine tasks in academic studies, gamedev, and system programming.

--- [ DETAILED METHOD CATALOG ] ---

📦 DATA PROCESSING & STRUCTURES
• ArrayUtils
  - GetMax: Returns the largest number in an array.
  - CustomSort: A manual implementation of a sorting algorithm.
  - Filter<T>: Returns elements that match a specific condition.
  - Shuffle<T>: Randomly reorders elements using Fisher-Yates.
  - GetAverage: Calculates the mean value of a numeric array.
  - FindIndex<T>: Returns the first index of a specified value.
  - Contains<T>: Checks if a value exists within the array.
  - Reverse<T>: Reverses the order of elements in the array.
  - Merge<T>: Combines two arrays into a single new array.
  - Fill<T>: Sets every element in the array to a specific value.
  - GetRandomElement<T>: Picks a single random item from the array.
  - AreEqual<T>: Compares two arrays for content equality.
• StringUtils
  - Reverse: Flips a string (abc -> cba).
  - CountWords: Returns the number of words in a text.
  - IsPalindrome: Checks if text reads the same forwards and backwards.
  - Capitalize: Makes the first letter uppercase.
  - RemoveWhitespace: Deletes all spaces/tabs from a string.
  - Truncate: Cuts text to a limit and adds "...".
  - Sanitize: Removes special characters for clean data.
  - IsNumeric: Checks if a string contains only numbers.
• CollectionUtils
  - IsEmpty: Checks if a list/collection has zero items.
  - ForEach: Performs an action on every item in a list.
  - ToUniqueList: Removes all duplicate items from a collection.
  - PickRandom: Grabs one random item from a collection.
  - Chunk: Splits a large list into smaller sub-lists.
  - GetMedian: Finds the middle value in a sorted numeric array.
  - GetMode: Finds the most frequently occurring item.
• ListExtensions
  - RemoveDuplicates: (Extension) Cleans the list in-place.
  - GetLast: (Extension) Quickly returns the final item.

🔢 MATHEMATICS & AI
• MathUtils
  - IsEven: Returns true if a number is divisible by 2.
  - Factorial: Calculates n! (1*2*3...*n).
  - GetPercentage: Calculates X% of a value.
  - GetRandomInt: Returns a random number between Min and Max.
  - Clamp: Forces a value to stay within a [Min, Max] range.
  - IsPrime: Checks if a number has only two divisors.
  - Round: Rounds a double to a specific number of decimal places.
  - Lerp: Linear interpolation between two values (animation/UI).
  - GetGCD/GetLCM: Finds Greatest Common Divisor and Least Common Multiple.
  - IsPowerOfTwo: Bitwise check for memory/optimization efficiency.
  - Map: Scales a number from one range to another.
  - GetRandomDouble: Returns a random double between 0.0 and 1.0.
  - GetHypotenuse: Pythagorean theorem (a² + b² = c²).
• AiUtils
  - Sigmoid/Relu/Tanh: Mathematical activation functions.
  - EuclideanDistance: Calculates straight-line distance between vectors.
  - MeanSquaredError: Measures average error for regression models.
  - CrossEntropy: Measures loss for classification models.
  - CalculateAccuracy: Compares predicted vs actual labels.
  - Normalize/Standardize: Scales data for neural network input.
  - SplitData: Divides a dataset into Training and Testing sets.
  - DotProduct/MultiplyMatrices/Transpose: Linear algebra essentials.
  - GetLevenshteinDistance: Measures text similarity (edit distance).
  - GetKeywords: Extracts most frequent important words.
• UnitConverter
  - KmHtoMs: Converts speed from km/h to meters per second.
  - ConvertCurrency: Multiplies an amount by an exchange rate.
  - FahrenheitToCelsius/CelsiusToFahrenheit: Temp conversion.
  - DegreesToRadians/RadiansToDegrees: Angle conversion for math/physics.

🎮 GAMEDEV & CONSOLE
• GameUtils
  - RollDice: Simulates a dice throw (d6, d20, etc.).
  - Chance: Returns true based on a 0.0-1.0 probability.
  - CalculateDistance: Distance between two 2D points (x,y).
  - GenerateRandomName: Creates a random fantasy-style name.
  - RollSuccess: Percentage-based success check (1-100).
  - RollWithAdvantage/Disadvantage: Returns best/worst of two d20 rolls.
  - CalculateDamage/Experience/ManaRegen: RPG system formulas.
  - ShakeScreen/SpawnEntity: Engine-level interaction placeholders.
  - IsInRange/CheckLineOfSight: Spatial AI logic.
  - PickWeightedItem: Picks an item based on custom probability weights.
• ConsoleVisuals
  - DrawProgressBar: Renders a visual loading bar in the console.
  - WriteInBox: Draws a frame around a specific message.
  - ClearLine: Deletes a specific line of text in the terminal.
• ColorUtils
  - WriteColored/WriteRainbow/PrintGradient: Advanced text styling.
  - WriteError/WriteSuccess/WriteWarning: Standardized colored output.
  - HighlightText: Colors a specific word within a larger string.
• AudioUtils
  - PlayBeep: Generates a specific PC frequency sound.
  - PlaySystemSound: Triggers Windows system notifications.

🛡 SECURITY & NETWORK
• SecurityUtils
  - HashString: Converts text to a secure SHA-256 string.
  - GeneratePassword: Creates a random strong password.
  - Encrypt: Basic symmetric encryption for data.
  - GetSecureRandomInt/GenerateToken: Cryptographically safe RNG.
  - GenerateSaltBytes: Random salt generation for password security.
  - CheckPasswordStrength: Rates a password from 0 to 100.
• EncryptionUtils
  - Base64Encode/Decode: Converts text to/from Base64 format.
  - GenerateSalt: Creates a random string "salt".
• NetworkUtils
  - PingHost: Checks if a server/IP is reachable.
  - GetLocalIpAddress/GetPublicIpAddress: Finds network addresses.
  - IsPortOpen: Scans if a specific network port is active.
  - GetMacAddress: Retrieves hardware network ID.
  - WakeOnLan: Sends a magic packet to wake a remote PC.
• HttpUtils
  - GetRequest: Fetches HTML/Data from a URL.
  - DownloadFile: Saves a web file directly to your disk.
  - IsInternetAvailable: Fast check for web connectivity.

💻 SYSTEM, HARDWARE & IMAGES
• HardwareUtils
  - GetGpuName/GetCpuName: Returns hardware model names.
  - IsBatteryCharging: Checks power status for mobile devices.
  - GetDiskSpaceInfo: Lists free/total space on all drives.
  - IsNetworkConnected: Checks for WiFi/Ethernet connection.
• SystemUtils
  - GetOsVersion/GetDotNetVersion: OS and Framework identification.
  - GetCpuUsage/GetAvailableRam: Real-time hardware monitoring.
  - OpenUrl: Launches a link in the default browser.
  - CopyToClipboard: Sends text to the system clipboard.
  - TakeScreenshot: Captures the screen to a local image file.
  - Restart/Shutdown/Sleep: System power management.
• ImageUtils
  - Resize/Rotate/Flip/Crop: Basic image manipulation.
  - ApplyBlur/Grayscale/Sepia/Invert: Visual filtering.
  - CompressImage: Reduces file size via quality adjustment.
  - AddWatermark: Overlays text on image data.
  - GetDominantColor: Analyzes an image for its main color.
• FileUtils
  - SmartWrite: Saves text and creates missing folders automatically.
  - GetFriendlyFileSize: Converts bytes to KB, MB, or GB strings.
  - BackupFile: Creates a copy of a file with a timestamp.
  - GetFileHash: Returns a unique MD5/SHA signature of a file.
  - IsFileLocked/BatchRename: File system management tools.
• ProcessUtils/ZipUtils
  - KillProcess/IsProcessRunning: Application management.
  - CreateZip/ExtractZip: Handles .zip archive operations.
• AutomationUtils
  - SendKeyPress/SetMousePosition: Hardware input simulation.
  - MinimizeAll: Minimizes all open windows.

🔧 CONFIG & DEBUG
• TimeUtils
  - GetTimestamp/GetTimeAgo: Temporal formatting.
  - GetAge: Calculates years from a birth date.
  - UnixTimestampToDateTime: Converts Unix time to C# DateTime.
• ValidationUtils
  - IsValidEmail/Phone/Url: Regex-based format checking.
  - IsSecurePassword: Complexity validation.
• ConfigUtils
  - SaveConfig/LoadConfig: Handles JSON-based settings files.
• DebugUtils
  - MeasureExecutionTime: Returns how many ms a function took.
  - DumpObject: Prints all properties of an object for inspection.

===========================================================