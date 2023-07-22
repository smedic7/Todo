using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
    public class TodoTask
    {

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }
        public bool Status { get; set; }
        public int TodoListId { get; set; }
        public TodoList TodoList { get; set; }



    }
}
