using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Todo.Data;
using Todo.Models;

namespace Ispit.Todo.Controllers
{



    [Authorize]
    public class TodoTaskController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public TodoTaskController(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index(int todo_list_id)
        {
            var tasks = _dbContext.TodoTask.Where(t => t.TodoListId == todo_list_id).ToList();

            var todoList = _dbContext.Todolists.FirstOrDefault(t => t.Id == todo_list_id);
            if (todoList == null)
            {
                return NotFound();
            }

            ViewBag.TodoList = todoList;

            return View(tasks);
        }

        [HttpGet]
        public IActionResult Create(int todoListId)
        {
            // ide u bazu po listu po id-u, radi validacije
            var todoList = _dbContext.TodoTask.FirstOrDefault(t => t.Id == todoListId);

            if (todoList == null)
            {
                return NotFound();
            }

            ViewBag.TodoList = todoList;

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var todoTask = _dbContext.TodoTask.FirstOrDefault(t => t.Id == id);

            if (todoTask == null)
            {
                return NotFound();
            }

            return View(todoTask);
        }


        [HttpPost]
        public IActionResult Create(TodoTask model)
        {
            var todoList = _dbContext.Todolists.FirstOrDefault(t => t.Id == model.TodoListId);
            if (todoList == null)
            {
                return NotFound();
            }

            _dbContext.TodoTask.Add(model);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", new { todo_list_id = model.TodoListId });
        }

        [HttpPost]
        public IActionResult Edit(TodoTask model)
        {
            _dbContext.TodoTask.Add(model);
            _dbContext.Update(model);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", new { todo_list_id = model.TodoListId });
        }







    }
}


