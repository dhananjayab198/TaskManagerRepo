using Microsoft.AspNetCore.Mvc;
using TaskManagement.Interfaces;

namespace TaskManagement.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITaskRepository _taskRepository;
        public TasksController(IEmployeeRepository employeeRepository,ITaskRepository taskRepository)
        {
            this._employeeRepository = employeeRepository;
            this._taskRepository = taskRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllTasks()
        {
            List<Task> tasks = await _taskRepository.GetAllTasksAsync();
            if (tasks == null)
            {
                return NotFound();
            }
            return Ok(tasks);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Task>> GetTask([FromRoute] Guid Id)
        {
            Task task = await _taskRepository.GetTaskIdAsync(Id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }
         
        [HttpPost]
        public async Task<ActionResult> AddTask([FromBody] Task task)
        {
            if (task == null)
            {
                return BadRequest("Bad data of task");
            }
            var istaskAdded = await _taskRepository.AddTaskAsync(task);
            if (!istaskAdded)
            {
                return BadRequest("Failed to create task");
            }
            return Ok("Task Added Successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask([FromRoute] Guid id, [FromBody] Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            var isUpdatedEmployee = await _taskRepository.UpdateTaskAsync(task);

            if (!isUpdatedEmployee)
            {
                return BadRequest("task Update Failure");
            }

            return Ok("task Updated successfully");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid Id)
        {
            var task = await _taskRepository.DeleteTaskAsync(Id);
            if (!task)
            {
                return NotFound("task not found");
            }
            return Ok("task successfully");
        }
    }
}
