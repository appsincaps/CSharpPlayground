using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using static System.Console;

namespace CSharpPlayground
{
    class Program
    {
        delegate int FuncInt(int number);
        static void Main(string[] args)
        {
            //StringConversion();
            //Literals();
            //StringManipulation();
            //Arrays();
            Lists();
            //FunctionFun(1,2,3);
            //Tuples();
            //Debugging();
            //Exceptions();
            //DisposableObjects();
            //Iterators();
            //CustomObjects();
            //CustomExceptions();
            //Events();
            WriteLine("--- Done");
            ReadKey();
        }

        static void StringConversion()
        {
            /* You convert a string to a number by calling the Parse or TryParse method found on numeric types(int, long, double, and so on), 
             * or by using methods in the System.Convert class. It's slightly more efficient and straightforward to call a TryParse method 
             * (for example, int.TryParse("11", out number)) or Parse method (for example, var number = int.Parse("11")). 
             * Using a Convert method is more useful for general objects that implement IConvertible. The Convert.ToInt32 method uses Parse internally.*/

            // Parse
            string input = String.Empty;
            try
            {
                int result = Int32.Parse(input);
                Console.WriteLine(result);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{input}'");
            }
            // Output: Unable to parse ''

            try
            {
                int numVal = Int32.Parse("-105");
                Console.WriteLine(numVal);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            // Output: -105

            if (Int32.TryParse("-105", out int j))
            {
                Console.WriteLine(j);
            }
            else
            {
                Console.WriteLine("String could not be parsed.");
            }
            // Output: -105

            try
            {
                int m = Int32.Parse("abc");
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            // Output: Input string was not in a correct format.

            const string inputString = "abc";
            if (Int32.TryParse(inputString, out int numValue))
            {
                Console.WriteLine(numValue);
            }
            else
            {
                Console.WriteLine($"Int32.TryParse could not parse '{inputString}' to an int.");
            }
            // Output: Int32.TryParse could not parse 'abc' to an int.

            string numericString = "10FF";

            if (int.TryParse(numericString, System.Globalization.NumberStyles.HexNumber, null, out int i))
            {
                Console.WriteLine($"'{numericString}' --> {i}");
            }
            // Output: '  10FFxxx' --> '  10FF' --> 4351

        }

        static void Literals()
        {
            // Binary
            int binaryInt = 0b00010001;
            WriteLine($"Binary int: {Convert.ToString(binaryInt, 2)} = {binaryInt}");

            // No octal literals in C#

            // Decimal
            const double Pi = 3.141_592_653_589_793_238_462_643_383_279_502;
            WriteLine($"Constant Pi: {Pi,20:N12}");

            // Hexadecimal
            int hex = 0xFFAA;
            WriteLine($"Hexadecimal int: {hex.ToString("X")} = {hex}");

            // Strings
            FormattableString pi_string = $"Constant Pi x 1000: {Pi * 1e3,20:N12}";
            WriteLine(pi_string.ToString());
            // Display the name of the current thread culture.
            Console.WriteLine("CurrentCulture is {0}.", CultureInfo.CurrentCulture.Name);
            WriteLine(pi_string.ToString(CultureInfo.GetCultureInfo("ru-RU")));
            Console.WriteLine("CurrentCulture is {0}.", CultureInfo.GetCultureInfo("ru-RU").Name);
            WriteLine(FormattableString.Invariant(pi_string));

            string literal = $@"Long list: {"\n"}\{"Big file",20}{"\n"}\{"Bigger file",20}";
            WriteLine($"{literal}");
        }

        static void StringManipulation()
        {
            string original = " TRim Me **#";
            char[] trimChars = { ' ', '*', '#' };
            string final = original.Trim(trimChars).ToLower();
            WriteLine($"From {original} to {final}");

            var chars = original.ToCharArray();
            chars[4] = 'X';
            final = String.Join("", chars).ToUpper();
            WriteLine($"From {original} to {final}");

            var str1 = "123".PadLeft(10,'_');
            var str2 = "12345".PadLeft(10, '_');
            WriteLine($"{str1}\n{str2}");

            var line = " word1,word2, word3;  word4---, end  ";
            var words = String.Join(" ", line.Split(new char[] { ' ', ',', '-', ';' }, StringSplitOptions.RemoveEmptyEntries));
            WriteLine($@"From ""{line}"" to ""{words}""");
        }

        static void Arrays()
        {
            // initialize array
            int[] myIntArray1 = { 5, 9, 10, 2, 99 };
            int[] myIntArray2 = new int[5];
            int[] myIntArray3 = new int[5] { 5, 9, 10, 2, 99 };
            foreach (var x in myIntArray1) // read-only access
            {
                Write($"{x} ");
            }
            WriteLine();

            // multi-dimensional arrays = rectangular
            double[,] hillHeight1 = new double[3, 4];
            double[,] hillHeight2 = { { 1, 2, 3, 4 }, { 2, 3, 4, 5 }, { 3, 4, 5, 6 } };
            foreach (var x in hillHeight2) 
            {
                Write($"{x} ");
            }
            WriteLine();

            // jagged array 
            int[][] jaggedIntArray1 = new int[3][];
            int[][] jaggedIntArray2 = { new int[] { 1, 2, 3 }, new int[] { 1 }, new int[] { 1, 2 } };
            foreach (var x in jaggedIntArray2) 
            {
                foreach (var y in x)
                    Write($"{y} ");
            }
            WriteLine();
        }

        static void Lists()
        {
            var list = Enumerable.Range(1, 10).ToList();
            foreach (var item in list)
                WriteLine($"{item}");
        }

        static void FunctionFun(params int[] parameters)
        {
            if (parameters.Length == 0)
                WriteLine("No parameters");
            else
            {
                foreach (var parameter in parameters)
                    WriteLine($"{parameter}");
            }

            int x = 5;
            ref int y = ref x;
            y = 42;
            WriteLine($"New numbers: {x} and {y}");

            var input = " 123 ";
            if (!int.TryParse(input, out int result))
                WriteLine("Parsing failure");
            else
                WriteLine($"Number = {result}");

            int AddOne(int n) => n + 1;

            FuncInt funRef = AddOne; // delegate = reference to a function

            WriteLine($"Add one to 9 = {funRef(9)}");

        }

        static void Tuples()
        {
            var t1 = (false, 2, 3.0, "string");
            WriteLine($"In {t1} the 1st item is {t1.Item1}");

            (int id, string name) t2 = (12, "Jack");
            WriteLine($"In {t2}: the 1st item is {t2.id}");

            var location = new Location(100.0, 30.0);
            (var x, var y) = location;
            WriteLine($"Location x = {x} y = {y}");
        }

        static void Debugging()
        {
            Debug.WriteLine("Starting...", "Debugging");
            int i = 10;
            for (int j = 1; j <= 5; ++j)
            {
                i += j;
                WriteLine($"{i} and {j}");
                Debug.WriteLine($"{i} and {j}", "Checking");
            }
        }

        static void Exceptions()
        {
            string[] eTypes = { "none", "simple", "index", "nested index", "filter" };

            foreach (string eType in eTypes)
            {
                try
                {
                    WriteLine("Main() try block reached.");
                    WriteLine($"ThrowException(\"{eType}\") called.");
                    ThrowException(eType);
                    WriteLine("Main() try block continues.");
                }
                catch (System.IndexOutOfRangeException e) when (eType == "filter")
                {
                    BackgroundColor = ConsoleColor.Red;
                    WriteLine($"Main() FILTERED System.IndexOutOfRangeException catch block reached. Message:\n\"{e.Message}\"");
                    ResetColor();
                }
                catch (System.IndexOutOfRangeException e)
                {
                    WriteLine($"Main() System.IndexOutOfRangeException catch block reached. Message:\n\"{e.Message}\"");
                }
                catch
                {
                    WriteLine("Main() general catch block reached.");
                }
                finally
                {
                    WriteLine("Main() finally block reached.");
                }
                WriteLine();
            }

            void ThrowException(string exceptionType)
            {
                WriteLine($"ThrowException(\"{exceptionType}\") reached.");

                switch (exceptionType)
                {
                    case "none":
                        WriteLine("Not throwing an exception.");
                        break;
                    case "simple":
                        WriteLine("Throwing System.Exception.");
                        throw new System.Exception();
                    case "index":
                        WriteLine("Throwing System.IndexOutOfRangeException.");
                        var arr = new int[2];
                        arr[5] = 1;
                        break;
                    case "nested index":
                        try
                        {
                            WriteLine("ThrowException(\"nested index\") try block reached.");
                            WriteLine("ThrowException(\"index\") called.");
                            ThrowException("index");
                        }
                        catch
                        {
                            WriteLine("ThrowException(\"nested index\") general catch block reached.");
                            throw;
                        }
                        finally
                        {
                            WriteLine("ThrowException(\"nested index\") finally block reached.");
                        }
                        break;
                    case "filter":
                        try
                        {
                            WriteLine("ThrowException(\"filter\") try block reached.");
                            WriteLine("ThrowException(\"index\") called.");
                            ThrowException("index");
                        }
                        catch
                        {
                            WriteLine("ThrowException(\"filter\") general catch block reached.");
                            throw;
                        }
                        break;
                }
            }
        }

        static void DisposableObjects()
        {
            using (var x = new DisposableObject())
            {
                WriteLine($"Using {x}");
            }
        }

        static void Iterators()
        {
            var things = new Things();
            foreach (string item in things)
            {
                WriteLine($"Collection of {item}:");
                foreach (string part in things[item])
                    Write($"{part} ");
                WriteLine();
            }

        }

        static void CustomObjects()
        {
            var f1 = new Filter(1.2, 3.5);
            var f2 = new Filter(2, 5);
            if (f1)
                WriteLine(f1 + f2);
            if (f2)
                WriteLine("True filter");

            WriteLine(f1 && f2);
            WriteLine(f1 || f2);
            WriteLine(f1 == f2);

            var list = new ArrayList();
            list.Add(f2);
            list.Add(f1);
            list.Add(new Filter(0, 1));
            list.Sort();
            foreach (var f in list)
                WriteLine(f);
        }

        static void CustomExceptions()
        {
            try
            {
                var f = new Filter(0, -1);
            }
            catch (NegativeFrequencyException e)
            {
                WriteLine($"{e.Value} entered: {e.Message}");
            }
        }

        static void Events()
        {
            var e = new EventEmitters();
            e.SimpleEvent += delegate (object o, EventArgs args)
            {
                WriteLine("Handle simple event");
            };
            e.StartSimpleEvent();

            e.StringEvent += delegate (object o, string strng)
            {
                WriteLine($"Print string: {strng}");
            };
            e.StartStringEvent();
        }
    }

    class DisposableObject : IDisposable
    {
        public int MyProperty { get; set; }
        public void Dispose()
        {
            WriteLine("Finished using disposable object");
        }

    }

    public class Location
    {
        public Location(double latitude, double longitude) => (Latitude, Longitude) = (latitude, longitude);
        public double Latitude { get; }
        public double Longitude { get; }
        public void Deconstruct(out double latitude, out double longitude) => (latitude, longitude) = (Latitude, Longitude);
    }

    public class Things
    {
        Dictionary<string, List<string>> collections = new Dictionary<string, List<string>>();

        public Things()
        {
            collections.Add("Trinkets", new List<string>() { "micha", "kilpa", "wuikopa" });
            collections.Add("Snippets", new List<string>() { "gapet", "rapet", "mopet", "tropet" });
            collections.Add("Garps", new List<string>() { "garif", "garid", "kalp", "karg" });
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var item in collections)
            {
                yield return item.Key;
            }     
        }

        public IEnumerable this[string key]
        {
            get
            {
                foreach (var item in collections[key])
                    yield return item;
            }
        }
    }

    public class Filter : IComparable
    {
        public double low, high;

        public Filter(double l, double h)
        {
            if (l < 0 || h < 0)
                throw new NegativeFrequencyException(l<0 ? l : h);
            low = l;
            high = h;
        }

        public override string ToString() => $"[{low:N2}:{high:N2}]";

        public static Filter operator +(Filter f1, Filter f2) => new Filter(Math.Min(f1.low, f2.low), Math.Max(f1.high, f2.high));

        public static bool operator true(Filter f) => f.low <= f.high;

        public static bool operator false(Filter f) => f.low > f.high;

        public static Filter operator &(Filter f1, Filter f2) => f1 ? f2 : f1;

        public static Filter operator |(Filter f1, Filter f2) => f1 ? f1 : f2;

        public static bool operator ==(Filter f1, Filter f2) => (f1.low == f2.low) && (f1.high == f2.high);

        public static bool operator !=(Filter f1, Filter f2) => (f1.low != f2.low) || (f1.high != f2.high);

        public override bool Equals(object f) => f is Filter filter && this == filter;

        public override int GetHashCode() => Tuple.Create(low, high).GetHashCode();

        public int CompareTo(object f)
        {
            if (f is Filter ff)
            {
                var result = low + high - ff.low - ff.high;
                return result > 0 ? 1 : result < 0 ? -1 : 0;
            }
            else
            {
                throw new ArgumentException("Object must be a filter");
            }
        }
    }

    public class NegativeFrequencyException : Exception
    {
        public double Value { get; private set; } 

        public NegativeFrequencyException(double value) : base("Negative frequencies not allowed")
        {
            Value = value;
        }
    }

    public struct LaunchStatus
    {
        private int status;
        private LaunchStatus(int status)
        {
            this.status = status;
        }

        public static readonly LaunchStatus Green = new LaunchStatus(0);
        public static readonly LaunchStatus Yellow = new LaunchStatus(1);
        public static readonly LaunchStatus Red = new LaunchStatus(2);
        public static bool operator true(LaunchStatus x) => x == Green || x == Yellow;
        public static bool operator false(LaunchStatus x) => x == Red;
        public static LaunchStatus operator &(LaunchStatus x, LaunchStatus y)
        {
            if (x == Red || y == Red || (x == Yellow && y == Yellow))
            {
                return Red;
            }

            if (x == Yellow || y == Yellow)
            {
                return Yellow;
            }

            return Green;
        }
        public static bool operator ==(LaunchStatus x, LaunchStatus y) => x.status == y.status;
        public static bool operator !=(LaunchStatus x, LaunchStatus y) => !(x == y);
        public override bool Equals(object obj) => obj is LaunchStatus other && this == other;
        public override int GetHashCode() => status;
    }

    public class EventEmitters
    {
        public event EventHandler SimpleEvent;
        public event EventHandler<string> StringEvent;

        List<System.Timers.Timer> timers = new List<System.Timers.Timer>();

        public void StartSimpleEvent()
        {
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += OnSimpleEvent;
            timer.Elapsed += Elapsor(timer, 5);
            timer.Enabled = true;
            Console.WriteLine("SimpleEvent started");
            timers.Add(timer);
        }

        void OnSimpleEvent(object source, ElapsedEventArgs args)
        {
            SimpleEvent?.Invoke(this, EventArgs.Empty);
        }

        ElapsedEventHandler Elapsor(System.Timers.Timer timer, int count)
        {
            return delegate (object o, ElapsedEventArgs args)
            {
                if (--count <=0)
                    timer.Enabled = false;
            };
        }

        public void StartStringEvent()
        {
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += OnStringEvent;
            timer.Enabled = true;
            Console.WriteLine("StringEvent started");
            timers.Add(timer);
        }

        void OnStringEvent(object o, ElapsedEventArgs args)
        {
            StringEvent?.Invoke(this, args.SignalTime.ToString());
        }
    }

}
