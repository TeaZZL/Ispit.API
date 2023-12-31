﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ispit.API.Data.Models;

namespace Ispit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListsController : ControllerBase
    {
        private readonly TodoListContext _context;

        public TodoListsController(TodoListContext context)
        {
            _context = context;
        }

        // GET: api/TodoLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoList>>> GetTodoLists()
        {
          if (_context.TodoLists == null)
          {
              return NotFound();
          }
            return await _context.TodoLists.ToListAsync();
        }

        // GET: api/TodoLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoList>> GetTodoList(int id)
        {
          if (_context.TodoLists == null)
          {
              return NotFound();
          }
            var todoList = await _context.TodoLists.FindAsync(id);

            if (todoList == null)
            {
                return NotFound();
            }

            return todoList;
        }

        // PUT: api/TodoLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoList(int id, TodoList todoList)
        {
            if (id != todoList.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoList>> PostTodoList(TodoList todoList)
        {
          if (_context.TodoLists == null)
          {
              return Problem("Entity set 'TodoListContext.TodoLists'  is null.");
          }
            _context.TodoLists.Add(new TodoList
            {
                Title = todoList.Title,
                Description = todoList.Description,
                IsCompleted = todoList.IsCompleted
            }
            );

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoList", new { id = todoList.Id }, todoList);
        }

        // DELETE: api/TodoLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoList(int id)
        {
            if (_context.TodoLists == null)
            {
                return NotFound();
            }
            var todoList = await _context.TodoLists.FindAsync(id);
            if (todoList == null)
            {
                return NotFound();
            }

            _context.TodoLists.Remove(todoList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoListExists(int id)
        {
            return (_context.TodoLists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
