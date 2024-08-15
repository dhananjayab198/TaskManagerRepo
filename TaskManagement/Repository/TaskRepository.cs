using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagement.Interfaces;

namespace TaskManagement.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private AppDbContext _dbContext;
        private readonly ILogger<TaskRepository> _logger;

        public TaskRepository(AppDbContext dbContext, ILogger<TaskRepository> logger)
        {
            this._dbContext = dbContext;
            this._logger = logger;   
        }
        public async Task<List<Task>> GetPendingTasksAsync()
        {
            try
            {
                List<Task> pendingTasks;
                pendingTasks = await _dbContext.Tasks.Where(m => m.Status == "Pending").ToListAsync();
                return pendingTasks;
            }
            catch (Exception ex) {
                _logger.LogInformation(ex.Message.ToString());
                return new List<Task>();
            }
        }

        public async Task<List<Task>> GetTasksByEmployeeIdAsync(Guid employeeId)
        {
            try
            {
                List<Task> tasks;
                tasks = await _dbContext.Tasks.Where(m => m.AssignedTo == employeeId).ToListAsync();
                return tasks;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message.ToString());
                return new List<Task>();
            }
        }

        public async Task<bool> AddTaskAsync(Task task)
        {
            try
            {
                if (task != null)
                {
                    task.Id = Guid.NewGuid();
                    _dbContext.Tasks.Add(task);
                    _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message.ToString());
                return false;
            }
        }

        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            try
            {
                Task task;
                task = await _dbContext.Tasks.Where(t => t.Id == id).FirstOrDefaultAsync();
                if (task != null)
                {
                    _dbContext.Tasks.Remove(task);
                    _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message.ToString());
                return false;
            }
        }

        public async Task<List<Task>> GetAllTasksAsync()
        {
            try
            {
                List<Task> tasks;
                tasks = await _dbContext.Tasks.ToListAsync();
                return tasks;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message.ToString());
                return new List<Task>();
            }
        }
        public async Task<Task> GetTaskIdAsync(Guid id)
        {
            try
            {
                Task tasks;
                tasks = await _dbContext.Tasks.Where(t => t.Id == id).FirstOrDefaultAsync();
                return tasks;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message.ToString());
                return new Task();
            }
            
        }

        public async Task<bool> UpdateTaskAsync(Task task)
        {
            try
            {

                Task tasks;
                tasks = await _dbContext.Tasks.Where(t => t.Id == task.Id).FirstOrDefaultAsync();
                if (tasks == null)
                    return false;
                tasks.Status = task.Status;
                tasks.AssignedTo = task.AssignedTo;
                tasks.Notes = task.Notes;
                tasks.DueDate = task.DueDate;
                tasks.Employee = task.Employee;
                tasks.Attachments = task.Attachments;
                tasks.Description = task.Description;
                tasks.Notes = task.Notes;
                tasks.Title = task.Title;
                _dbContext.Update(tasks);
                _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message.ToString());
                return false;
            }
        }
    }
}
