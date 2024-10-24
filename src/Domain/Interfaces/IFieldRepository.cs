using Domain.Entities;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Interfaces
{
    public interface IFieldRepository
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