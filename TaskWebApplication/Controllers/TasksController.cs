using Microsoft.AspNetCore.Mvc;
using TaskWebApplication.Data;
using TaskWebApplication.Models;

namespace TaskWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public TasksController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllTask(string ? sortBy , DateTime? filterByDoj)
        {
            IQueryable<Tasks> query = _dbContext.TasksProperty;

            if (!string.IsNullOrEmpty(sortBy))
            {
                if(sortBy.ToLower() == "fullname")
                {
                    query = query.OrderBy(t => t.FirstName + t.LastName);

                }
            }

             if (filterByDoj != null)
            {
                query = query.Where(t => t.DateOfJoining.Value.Year == filterByDoj.Value.Year);
            }

            var TasksQuery = query.ToList();

            return Ok(TasksQuery);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTask(int id) 
        {
            var getTask = _dbContext.TasksProperty.FirstOrDefault(x => x.id == id);
            if(getTask == null)
            {
                return NotFound("Task is not found");
            }
            return Ok(getTask);
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] Tasks task)
        {
            var createTask = new Tasks()
            {
                id=task.id,
                FirstName = task.FirstName,
                LastName = task.LastName,
                age= task.age,
                DateOfJoining = task.DateOfJoining,                
                Department = task.Department,
                City = task.City
            };
            
            _dbContext.Add(createTask);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetTask), new { createTask.id }, createTask);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] Tasks task)
        {
            var existingTask = _dbContext.TasksProperty.FirstOrDefault(x => x.id == id);
            if (existingTask == null)
            {
                return NotFound(); 
            }

            existingTask.FirstName = task.FirstName;
            existingTask.LastName = task.LastName;
            existingTask.age = task.age;
            existingTask.DateOfJoining = task.DateOfJoining;
            existingTask.Department = task.Department;
            existingTask.City = task.City;

            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var existingTask = _dbContext.TasksProperty.FirstOrDefault(x => x.id == id);
            if (existingTask == null)
            {
                return NotFound();
            }
            
            _dbContext.Remove(existingTask);
            _dbContext.SaveChanges();

            return NoContent(); 
        }


    }
}
