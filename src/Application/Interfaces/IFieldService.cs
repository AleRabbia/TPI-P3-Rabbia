using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Interfaces
{
    public interface IFieldService
    {
        void AddField(Field field);
        IEnumerable<Field> GetAllFields();
        Field GetFieldById(int id);
        void UpdateField(Field field);
        void DeleteFieldLogic(Field field);
        void DeleteField(int id);
        Field GetFieldByName(string name);
        IEnumerable<Field> GetEnabledFields();
    }
}