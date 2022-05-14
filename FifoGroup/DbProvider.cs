using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FifoGroup
{
    public class DbProvider
    {

        public class DatabaseNotExist : Exception
        {
            public DatabaseNotExist()
            {
            }

            public DatabaseNotExist(string message)
                : base(message)
            {
            }

            public DatabaseNotExist(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        public class RecordExstException : Exception
        {
            public RecordExstException()
            {
            }

            public RecordExstException(string message)
                : base(message)
            {
            }

            public RecordExstException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        public class RecordNotExistException : Exception
        {
            public RecordNotExistException()
            {
            }
            public RecordNotExistException(string message)
                : base(message)
            {
            }
            public RecordNotExistException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        //Generic Method
        static public List<T> GetRecords<T>(string databaseFilePath,string tableName = "")
        {
            if (databaseFilePath == null) throw new NullReferenceException(nameof(databaseFilePath));
            List<T> ExcuteGroupList = new List<T>();
            using (LiteDatabase db = new LiteDatabase(databaseFilePath))
            {
                ILiteCollection<T> col;
                if (string.IsNullOrEmpty(tableName)) col = db.GetCollection<T>();
                else col = db.GetCollection<T>(tableName);
                ExcuteGroupList = col.FindAll().ToList();
            }
            return ExcuteGroupList;
        }
        static public void SaveRecord<T>(string databaseFilePath, T record, string tableName = "")
        {
            if (databaseFilePath == null) throw new NullReferenceException(nameof(databaseFilePath));
            using (LiteDatabase db = new LiteDatabase(databaseFilePath))
            {
                ILiteCollection<T> col;
                if (string.IsNullOrEmpty(tableName)) col = db.GetCollection<T>();
                else col = db.GetCollection<T>(tableName);
                col.Insert(record);
            }
        }
        static public void SaveUniqueRecord<T>(string databaseFilePath, T record,string property, string tableName = "")
        {
            if (databaseFilePath == null) throw new NullReferenceException(nameof(databaseFilePath));
            if(string.IsNullOrEmpty(property)) throw new NullReferenceException(nameof(property));
            PropertyInfo[] _propertyinfos = record.GetType().GetProperties();
            bool found = false;
            PropertyInfo _uniquePropertyInfo = null;
            
            foreach (PropertyInfo propertyInfo in _propertyinfos)
            {
                if(propertyInfo.Name == property)
                {
                    _uniquePropertyInfo = propertyInfo;
                    found = true;
                    break;
                }
            }
            if (!found) throw new ArgumentOutOfRangeException(nameof(property));
            Type _uniquePropertyType = _uniquePropertyInfo.PropertyType;
            object _uniquePropertyValue = _uniquePropertyInfo.GetValue(record);

            using (LiteDatabase db = new LiteDatabase(databaseFilePath))
            {
                ILiteCollection<T> col;
                if (string.IsNullOrEmpty(tableName)) col = db.GetCollection<T>();
                else col = db.GetCollection<T>(tableName);

                List<T> _records = col.FindAll().ToList();
                found = false;
                foreach(T _record in _records)
                {
                    if (_uniquePropertyInfo.GetValue(_record).Equals(_uniquePropertyValue))
                    {
                        found = true;
                        break;
                    }
                }
                if (found) throw new RecordExstException(nameof(record));
                col.Insert(record);
            }
        }
        static public void DeleteRecord<T>(string databaseFilePath, T record, string searchproperty, string tableName = "")
        {
            if (record == null) throw new NullReferenceException(nameof(record));
            if (string.IsNullOrEmpty(searchproperty)) throw new NullReferenceException(nameof(searchproperty));

            PropertyInfo[] _propertyinfos = record.GetType().GetProperties();
            bool foundproperty = false;
            bool found = false;
            PropertyInfo _propertyInfo = null;

            foreach (PropertyInfo pro in _propertyinfos)
            {
                if (pro.Name == searchproperty)
                {
                    _propertyInfo = pro;
                    foundproperty = true;
                    break;
                }
            }
            if (!foundproperty) throw new ArgumentOutOfRangeException(nameof(searchproperty));
            Type _propertyType = _propertyInfo.PropertyType;
            object _propertyValue = _propertyInfo.GetValue(record);

            using (LiteDatabase db = new LiteDatabase(databaseFilePath))
            {
                ILiteCollection<T> col;
                if (string.IsNullOrEmpty(tableName)) col = db.GetCollection<T>();
                else col = db.GetCollection<T>(tableName);

                List<T> _records = col.FindAll().ToList();
                found = false;
                int _id = 0;
                foreach (T _record in _records)
                {
                    if (_propertyInfo.GetValue(_record).Equals(_propertyValue))
                    {
                        found = true;
                        _id = (int)(_record.GetType().GetProperty("Id").GetValue(_record));
                        break;
                    }
                }
                if (!found) throw new RecordNotExistException(nameof(record));
                col.Delete(_id);
            }

        }
        static public void EditRecord<T>(string databaseFilePath, T record, string searchproperty, string tableName = "")
        {
            if (record == null) throw new NullReferenceException(nameof(record));
            if (string.IsNullOrEmpty(searchproperty)) throw new NullReferenceException(nameof(searchproperty));

            PropertyInfo[] _propertyinfos = record.GetType().GetProperties();
            bool foundproperty = false;
            bool found = false;
            PropertyInfo _propertyInfo = null;

            foreach (PropertyInfo pro in _propertyinfos)
            {
                if (pro.Name == searchproperty)
                {
                    _propertyInfo = pro;
                    foundproperty = true;
                    break;
                }
            }
            if (!foundproperty) throw new ArgumentOutOfRangeException(nameof(searchproperty));
            Type _propertyType = _propertyInfo.PropertyType;
            object _propertyValue = _propertyInfo.GetValue(record);

            using (LiteDatabase db = new LiteDatabase(databaseFilePath))
            {
                ILiteCollection<T> col;
                if (string.IsNullOrEmpty(tableName)) col = db.GetCollection<T>();
                else col = db.GetCollection<T>(tableName);

                List<T> _records = col.FindAll().ToList();
                found = false;
                int _id = 0;
                foreach (T _record in _records)
                {
                    if (_propertyInfo.GetValue(_record).Equals(_propertyValue))
                    {
                        found = true;
                        _id = (int)(_record.GetType().GetProperty("Id").GetValue(_record));
                        record.GetType().GetProperty("Id").SetValue(record, _id);
                        break;
                    }
                }
                if (!found) throw new RecordNotExistException(nameof(record));
                if(!col.Update(_id,record)) throw new RecordNotExistException(nameof(record));
            }

        }
    }
}