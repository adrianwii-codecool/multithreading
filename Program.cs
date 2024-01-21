namespace Multithreading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();

            int sum = 0;
            sum = calculator.Add(35, 15);
            Console.WriteLine($"Add: {sum}");

            sum = calculator.AddInLoop(35, 15);

            Console.WriteLine($"AddInLoop: {sum}");

            sum = calculator.AddInLoopWithThreads(35, 15);
            Console.WriteLine($"AddInLoopWithThreads: {sum}");

            sum = calculator.AddInLoopWithThreadsLock(35, 15);
            Console.WriteLine($"AddInLoopWithThreads: {sum}");

            sum = calculator.AddInLoopWithThreadsLockPrimitive(35, 15);
            Console.WriteLine($"AddInLoopWithThreadsLockPrimitive: {sum}");

            sum = calculator.AddInLoopWithThreadsMutex(35, 15);
            Console.WriteLine($"AddInLoopWithThreadsMutex: {sum}");
        }
    }
}
