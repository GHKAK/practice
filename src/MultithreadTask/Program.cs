using MultithreadTask;
using System.Diagnostics;
using System.Threading.Tasks;

Stopwatch sw = new();
int totalInvocations=1000;
int completedInvocations = 0;
int numToComplete = 0;
object lockObject = new object();
for (int i = 1; i <= 20; i++) {
    List<Thread> threads = new();
    numToComplete = totalInvocations / i;
    for (int k = 0; k < i; k++) {
        threads.Add(new Thread(() => {
            for (int i = 0; i < numToComplete; i++) {
            PayloadMethod.CalcFactorial();
            }
        }));
    }
    sw.Restart();
    for (int j = 0; j < i; j++) {
        threads[j].Start();
    }
    foreach (Thread thread in threads) { thread.Join(); }
    sw.Stop();
    Console.WriteLine($"{sw.Elapsed} {i} Thread   completed {numToComplete} in one Thread");
    completedInvocations = 0;
}
var runLimit = 1000;
var taskLimit = 20;
for (int i = 0; i < taskLimit; i++) {
    var timer = System.Diagnostics.Stopwatch.StartNew();
    var tasks = new Task[i + 1];
    for (var k = 0; k < tasks.Length; k++) {
        tasks[k] = Task.Run(PayloadMethod.CalcFactorial);
    }

    for (var k = 0; k < runLimit - tasks.Length; k++) {
        var index = Task.WaitAny(tasks);
        tasks[index] = Task.Run(PayloadMethod.CalcFactorial);
    }
    Task.WaitAll(tasks);

    timer.Stop();
    Console.WriteLine($"Thread number - {i + 1}, elapsed time - {timer.Elapsed}");
}
Console.ReadLine();
