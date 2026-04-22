=====================================================================================================
  
   ________  ___  ___  ________   ________  _________  ___  ________  ________   ________      
  |\  _____\|\  \|\  \|\   ___  \|\   ____\|\___   ___\\  \|\   __  \|\   ___  \|\   ____\     
  \ \  \__/ \ \  \\\  \ \  \\ \  \ \  \___|\|___ \  \_\ \  \ \  \|\  \ \  \\ \  \ \  \___|_    
   \ \   __\ \ \  \\\  \ \  \\ \  \ \  \       \ \  \ \ \  \ \  \\\  \ \  \\ \  \ \_____  \   
    \ \  \_|  \ \  \\\  \ \  \\ \  \ \  \____   \ \  \ \ \  \ \  \\\  \ \  \\ \  \|____|\  \  
     \ \__\    \ \_______\ \__\\ \__\ \_______\  \ \__\ \ \__\ \_______\ \__\\ \__\____\_\  \ 
      \|__|     \|_______|\|__| \|__|\|_______|   \|__|  \|__|\|_______|\|__| \|__|\_________\
                                                                                  \|_________|
                                                                                              
                                [ CORE N-SERIES RUNTIME ]
=====================================================================================================
   > Module:          Functions_Storage.Core
   > Build:           N-Series (Custom Types & Wrappers)
   > Target:          .NET 8.0 / C# 12
   > Dev:             Nazar Luchynin
   > Features:        Heavy Operator Overloading / Fluent Logic / Custom DS
=====================================================================================================

--- [ ARCHITECTURAL OVERVIEW ] ----------------------------------------------------------------------
N-Series is a specialized collection of classes designed to redefine standard C# development. 
By replacing verbose native patterns with custom wrappers and expressive operators, this module 
transforms C# into a more fluent, mathematical, and data-driven language.

Key Principles:
   - EXPRESSIVE SYNTAX: Use of ~, ++, --, and + for high-level logic flow.
   - DATA ENCAPSULATION: Robust internal management of memory and state.
   - MATHEMATICAL ELEGANCE: Matrix and Vector operations built directly into the types.

--- [ CORE TYPE REISTRY ] ---------------------------------------------------------------------------

[ LOGIC & CONTROL FLOW ]

1. ifN (Fluent Conditional)
   - Usage: ifN.That(condition).Then(() => action).Else(() => action);
   - Logic: A chainable replacement for standard 'if' blocks with state tracking.

2. forN (Loop Wrappers)
   - Between(s, e, action): Direct index-based iteration.
   - Each<T>(items, action): Optimized 'foreach' abstraction.

3. whileN (Functional Cycle)
   - Cycle(Func<bool>, Action): Functional wrapper for continuous execution loops.

[ MATHEMATICAL STRUCTURES ]

4. vectorN (Linear Vector)
   - Capabilities: Dot products, scalar multiplication (*), and vector addition (+).
   - Display: Custom ToString() with precision formatting (vec{1.00, 2.00}).

5. matrixN (Neural Matrix)
   - Capabilities: High-speed multiplication (*) and Transposition.
   - Context: Fully compatible with AiUtils for neural network layers.

[ DATA STRUCTURES (THE N-COLLECTIONS) ]

6. listN<T> (Enhanced Dynamic List)
   - Features: Automatic shuffling on creation, operator-based merging (+).
   - Casting: Implicitly converts from T[] with built-in randomization.

7. stackN<T> (LIFO Engine)
   - Logic: True Last-In-First-Out implementation.
   - Operators: '+' (Push), '--' (Pop), '~' (Destructive Peek/Pop).

8. queueN<T> (FIFO Engine)
   - Logic: Circular buffer optimization for constant-time (O1) Enqueue/Dequeue.
   - Operators: '+' (Enqueue), '~' (Dequeue).

9. hashSetN<T> (Unique Registry)
   - Logic: Custom bucket-based hashing for high-speed unique item checks.
   - Operators: '+' (Add), '-' (Remove).

[ SYSTEM WRAPPERS ]

10. stringN (Smart String)
    - Features: Fast Palindrome checking, character-array based manipulation.
    - Casting: Implicitly converts to/from native string.

11. fileN / hostN / timeN
    - fileN: Encapsulated I/O operations (Read/Write/Exists).
    - hostN: Real-time network endpoint with 'IsOnline' ping check.
    - timeN: Simplified DateTime management with Unix Epoch integration.

--- [ IMPLEMENTATION GUIDE ] ------------------------------------------------------------------------

The N-Series is designed for rapid prototyping and cleaner production code.

[ EXAMPLE: FLOW & DATA HANDLING ]
-----------------------------------------------------------------------------------------------
// Implicit casting from array to listN (with shuffle)
listN<int> numbers = new int[] { 1, 2, 3, 4, 5 };

// Fluent logic check
ifN.That(numbers.Count > 0)
   .Then(() => {
       int picked = numbers.RandomItem();
       ColorUtils.WriteInfo($"Picked: {picked}");
   });

// Stack manipulation via operators
stackN<string> logs = new stackN<string>();
logs += "UserJoined"; 
logs += "SecurityAlert";
string last = ~logs; // Destructive pop via tilde operator
-----------------------------------------------------------------------------------------------

--- [ CREDITS & LEGAL ] -----------------------------------------------------------------------------

  > LEAD ARCHITECT  : Nazar Luchynin
  > CORE SPEC       : N-Series Functional Extension
  > SOURCE          : Functions_Storage Namespace
  > LICENSE         : Proprietary Academic (c) 2026

=====================================================================================================
                      [ SYSTEM HALTED. END OF CORE DOCUMENTATION ]
=====================================================================================================