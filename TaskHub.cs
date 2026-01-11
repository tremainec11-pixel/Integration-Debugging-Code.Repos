HubConnection hubConnection = new HubConnectionBuilder()
    .WithUrl(NavigationManager.ToAbsoluteUri("/taskHub"))
    .Build();
public class TaskHub : Hub {
    public async Task NotifyTaskAdded(TaskDto task) {
        await Clients.All.SendAsync("TaskAdded", task);
    }
}
