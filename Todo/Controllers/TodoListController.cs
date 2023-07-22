using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Ispit.Todo.Controllers
{


    [Authorize]
    public class TodoListController : Controller
    {

        ApplicationDbContext _dbContext;


        public TodoListController(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            List<TodoList> list = _dbContext.Todolists.ToList();

            // TODO load list iz baze

            return View(list);
        }

        public IActionResult Create()
        {
            return View(new TodoList());
        }

        [HttpPost]
        public IActionResult Create(TodoList todoList)
        {
            // Spremi novi TodoList u bazu
            _dbContext.Todolists.Add(todoList);
            _dbContext.SaveChanges();

            // Preusmjeri na Index akciju u TodoListController-u
            return RedirectToAction("Index");
        }

    }

}