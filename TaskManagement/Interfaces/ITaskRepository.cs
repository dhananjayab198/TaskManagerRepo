namespace TaskManagement.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<Task>> GetTasksByEmployeeIdAsync(Guid employeeId);
        Task<List<Task>> GetPendingTasksAsync();
        Task<List<Task>> GetAllTasksAsync();
        Task<Task> GetTaskIdAsync(Guid id);
        Task<Boolean> AddTaskAsync(Task task);

        Task<Boolean> UpdateTaskAsync(Task task);
        Task<Boolean> DeleteTaskAsync(Guid id);
    }
}
