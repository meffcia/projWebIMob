using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using proj2.Models;
using proj2.Extensions;

namespace proj2.Controllers
{
    public class TodoController : Controller
    {
        private const string SessionKey = "TodoList";

        // Metoda pomocnicza do pobierania listy TODO z sesji
        private List<TodoItem> GetTodoList()
        {
            var todoList = HttpContext.Session.GetObjectFromJson<List<TodoItem>>(SessionKey);
            return todoList ?? new List<TodoItem>(); // Zwróć pustą listę, jeśli nic nie ma w sesji
        }

        // Metoda pomocnicza do zapisywania listy TODO w sesji
        private void SaveTodoList(List<TodoItem> todoList)
        {
            HttpContext.Session.SetObjectAsJson(SessionKey, todoList);
        }

        // GET: /Todo
        [HttpGet]
        public IActionResult Index()
        {
            var todoList = GetTodoList();
            return View(todoList);
        }

        // POST: /Todo/Add
        [HttpPost]
        public IActionResult Add(string description)
        {
            var todoList = GetTodoList();
            todoList.Add(new TodoItem { Description = description, IsCompleted = false });
            SaveTodoList(todoList);
            return RedirectToAction("Index");
        }

        // POST: /Todo/Complete
        [HttpPost]
        public IActionResult Complete(int index)
        {
            var todoList = GetTodoList();
            if (index >= 0 && index < todoList.Count)
            {
                todoList[index].IsCompleted = true;
                SaveTodoList(todoList);
            }
            return RedirectToAction("Index");
        }

        // POST: /Todo/Delete
        [HttpPost]
        public IActionResult Delete(int index)
        {
            var todoList = GetTodoList();
            if (index >= 0 && index < todoList.Count)
            {
                todoList.RemoveAt(index);
                SaveTodoList(todoList);
            }
            return RedirectToAction("Index");
        }
    }
}
