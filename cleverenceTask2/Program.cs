using cleverenceTask2;

var h = new EventHandler<CustomEventArgs>(MyEventHandler!);
var ac = new AsyncCaller(h);

bool compeltedOK1 = ac.Invoke(1000, null, new CustomEventArgs());
Console.WriteLine($"Не успел выполниться: {compeltedOK1}");

bool compeltedOK2 = ac.Invoke(3000, null, new CustomEventArgs());
Console.WriteLine($"Успел выполниться: {compeltedOK2}");

void MyEventHandler (object sender, CustomEventArgs e)
{
    var token = e.token;

    //Симуляция работы
    Thread.Sleep(2000);

    if (token.IsCancellationRequested) return;

    Console.WriteLine("EVENT HANDLER FINISHED");
}

public class CustomEventArgs : EventArgs
{
    public CancellationToken token { get; set; } = default;
}
