using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace Infrastructure.Repositories
{
    public class FieldRepository : IFieldRepository
    {
        private readonly ApplicationDbContext _context;

        public FieldRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddField(Field field)
        {
            _context.Fields.Add(field);
            _context.SaveChanges();
        }
        public IEnumerable<Field> GetAllFields()
        {
            return _context.Fields.ToList();
        }
        public Field GetFieldById(int id)
        {
            /*if (field == null)
            {
                throw new Exception($"No se encontró un campo con ID {id}.");
            }
            return field;*/
            var field = _context.Fields.Find(id) ?? throw new Exception($"No se encontró un campo con ID {id}.");
            return field;
        }
        public void UpdateField(Field field)
        {
            _context.Fields.Update(field);
            _context.SaveChanges();
        }
        public void DeleteFieldLogic(Field field)
        {
            _context.Fields.Update(field);
            _context.SaveChanges();
        }
        public void DeleteField(int id)
        {
            var field = _context.Fields.Find(id);
            if (field != null)
            {
                _context.Remove(field);
                _context.SaveChanges();
            }
        }
        public Field GetFieldByName(string name)
        {
           var field = _context.Fields.FirstOrDefault(u => u.Name == name) ?? throw new Exception($"No se encontró un campo con nombre {name}.");
           return field;
        }
        public IEnumerable<Field> GetEnabledFields()
        {
            return _context.Fields.Where(u => u.Enabled).ToList();
        }

    }
}