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
        }
        static public void SaveGroupID(ExcuteGroupData group)
        {
            if (Directory.Exists(".\\Database")) Directory.CreateDirectory(".\\Database");
            if (string.IsNullOrEmpty(DatabaseFilePath)) throw new NullReferenceException(nameof(DatabaseFilePath));
            if (group != null) throw new NullReferenceException(nameof(group));
            DbProvider.GSaveUniqueRecord(DatabaseFilePath, group, "GroupId");
        }
        static public List<ExcuteGroupData> GetGroupIDs()
        {
            if (Directory.Exists(".\\Database")) throw new DbProvider.DatabaseNotExist();
            if (string.IsNullOrEmpty(DatabaseFilePath)) throw new NullReferenceException(nameof(DatabaseFilePath));
            List<ExcuteGroupData> ExcuteGroupList = DbProvider.GGetRecords<ExcuteGroupData>(DatabaseFilePath);
            return ExcuteGroupList;
        }
        static public void DeleteGoupID(ExcuteGroupData group)
        {
            if (Directory.Exists(".\\Database")) throw new DbProvider.DatabaseNotExist();
            if (string.IsNullOrEmpty(DatabaseFilePath)) throw new NullReferenceException(nameof(DatabaseFilePath));
            if (group == null) throw new NullReferenceException(nameof(group));
            DbProvider.DeleteRecord(DatabaseFilePath, group, "GroupId");
        }
    }
}