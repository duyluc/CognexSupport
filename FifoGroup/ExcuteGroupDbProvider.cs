using System;
using System.Collections.Generic;
using System.IO;
namespace FifoGroup
{
    public static class ExcuteGroupDbProvider
    {
        static public string DatabaseFilePath = @".\Database\ExcuteGroupDB.db";
        public class ExcuteGroupData
        {
            public int Id { get; set; }
            public string GroupId { get; set; }
            public string CameraSerialNumber { get; set; }
        }
        static public void SaveGroupID(ExcuteGroupData group)
        {
            if (!Directory.Exists(".\\Database")) Directory.CreateDirectory(".\\Database");
            if (string.IsNullOrEmpty(DatabaseFilePath)) throw new NullReferenceException(nameof(DatabaseFilePath));
            if (group == null) throw new NullReferenceException(nameof(group));
            DbProvider.SaveUniqueRecord(DatabaseFilePath, group, "GroupId");
        }
        static public List<ExcuteGroupData> GetGroupIDs(string modelname)
        {
            if (!Directory.Exists(".\\Database")) throw new DbProvider.DatabaseNotExist();
            if (string.IsNullOrEmpty(DatabaseFilePath)) throw new NullReferenceException(nameof(DatabaseFilePath));
            List<ExcuteGroupData> ExcuteGroupList = DbProvider.GetRecords<ExcuteGroupData>(DatabaseFilePath,modelname);
            return ExcuteGroupList;
        }
        static public void DeleteGoupID(ExcuteGroupData group,string modelname)
        {
            if (!Directory.Exists(".\\Database")) throw new DbProvider.DatabaseNotExist();
            if (string.IsNullOrEmpty(DatabaseFilePath)) throw new NullReferenceException(nameof(DatabaseFilePath));
            if (group == null) throw new NullReferenceException(nameof(group));
            DbProvider.DeleteRecord(DatabaseFilePath, group, "GroupId", modelname);
        }
        static public void EditGroupID(ExcuteGroupData group,string modelname)
        {
            if (!Directory.Exists(".\\Database")) throw new DbProvider.DatabaseNotExist();
            if (string.IsNullOrEmpty(DatabaseFilePath)) throw new NullReferenceException(nameof(DatabaseFilePath));
            if (group == null) throw new NullReferenceException(nameof(group));
            DbProvider.EditRecord(DatabaseFilePath, group, "GroupId",modelname);
        }
        static public void DeleteAllGroups(string modelname)
        {
            if (!Directory.Exists(".\\Database")) throw new DbProvider.DatabaseNotExist();
            if (string.IsNullOrEmpty(DatabaseFilePath)) throw new NullReferenceException(nameof(DatabaseFilePath));
            DbProvider.DeleteAll<ExcuteGroupData>(DatabaseFilePath,modelname);
        }
    }
}