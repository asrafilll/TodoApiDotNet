
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Interfaces;
using TodoApi.Models;

namespace TodoApi.Services{


    public class TodoItemService : ITodoItemService
    {

        private readonly TodoContext _context;

        public TodoItemService(TodoContext context){
            _context = context;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetTodoItems()
        {
            return await _context.TodoItems
            .Select(x => ItemToDTO(x))
            .ToListAsync();
        }

        public async Task<TodoItemDTO?> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if(todoItem == null){
                return null;
            }
            return ItemToDTO(todoItem);
        }

        public async Task<TodoItemDTO> PostTodoItem(TodoItemDTO todoDTO)
        {
            var todoItem = new TodoItem{
                Name = todoDTO.Name,
                IsComplete = todoDTO.IsComplete,
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return ItemToDTO(todoItem);
        }

        
        public async Task PutTodoItem(long id, TodoItemDTO todoDTO)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            
            if(todoItem == null){
                throw new KeyNotFoundException($"Todo Item with ID {id} not found");
            }

            todoItem.Name = todoDTO.Name;
            todoItem.IsComplete = todoDTO.IsComplete;
            
            _context.TodoItems.Update(todoItem);
            
            await _context.SaveChangesAsync();
            
        }

        public async Task DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                throw new KeyNotFoundException($"Todo Item with ID {id} not found");
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }      

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) => new TodoItemDTO
        {
           Id = todoItem.Id,
           Name = todoItem.Name,
           IsComplete = todoItem.IsComplete
        };
    }

}

