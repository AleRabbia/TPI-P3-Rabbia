using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Application.Services
{
    public class FieldService : IFieldService
    {
        private readonly IFieldRepository _fieldRepository;

        public FieldService(IFieldRepository fieldRepository)
        {
            _fieldRepository = fieldRepository;
        }

        public void AddField(Field field)
        {
            _fieldRepository.AddField(field);
        }
        public IEnumerable<Field> GetAllFields()
        {
            return _fieldRepository.GetAllFields();
        }
        public Field GetFieldById(int id)
        {
            return _fieldRepository.GetFieldById(id);
        }
        public void UpdateField(Field field)
        {
            _fieldRepository.UpdateField(field);
        }
        public void DeleteFieldLogic(Field field)
        {
            _fieldRepository.DeleteFieldLogic(field);
        }
        public void DeleteField(int id)
        {
            _fieldRepository.DeleteField(id);
        }
        public Field GetFieldByName(string name)
        {
            return _fieldRepository.GetFieldByName(name);
        }
        public IEnumerable<Field> GetEnabledFields()
        {
            return _fieldRepository.GetEnabledFields();
        }
    }
}