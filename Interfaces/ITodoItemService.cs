using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Interfaces {

    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItemDTO>> GetTodoItems();
        Task<TodoItemDTO?> GetTodoItem(long id);
        Task PutTodoItem(long id, TodoItemDTO todoDTO);
        Task<TodoItemDTO> PostTodoItem(TodoItemDTO todoDTO);
        Task DeleteTodoItem(long id);
    }
}