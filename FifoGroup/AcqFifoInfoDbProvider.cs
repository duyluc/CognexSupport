using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FifoGroup
{
    public class AcqFifoInfoDbProvider
    {
        public class AcqFifoDbData
        {
            public int Id { get;set; }
            public string GroupId { get;set; }
            public string SerialNumber { get; set; }
            public string VideoFormat { get; set;}
            public string PixelFormat { get; set; }
            public int Port { get; set; }
        }
        static public string DatabaseFilePath = @".\Database\AcqFifoInfoDB.db";

        static public void SaveInfo(AcqFifoDbData info)
        {
            if (!Directory.Exists(".\\Database")) Directory.CreateDirectory(".\\Database");
            if (string.IsNullOrEmpty(DatabaseFilePath)) throw new NullReferenceException(nameof(DatabaseFilePath));
            if (info == null) throw new NullReferenceException(nameof(info));
            DbProvider.SaveUniqueRecord(DatabaseFilePath, info, "GroupId");
        }
        static public List<AcqFifoDbData> GetAcqFifoInfos()
        {
            if (!Directory.Exists(".\\Database")) throw new DbProvider.DatabaseNotExist();
            if (string.IsNullOrEmpty(DatabaseFilePath)) throw new NullReferenceException(nameof(DatabaseFilePath));
            List<AcqFifoDbData> AcqFifoInfos = DbProvider.GetRecords<AcqFifoDbData>(DatabaseFilePath);
            return AcqFifoInfos;
        }
        static public void DeleteInfo(AcqFifoDbData acqfifoinfo)
        {
            if (!Directory.Exists(".\\Database")) throw new DbProvider.DatabaseNotExist();
            if (string.IsNullOrEmpty(DatabaseFilePath)) throw new NullReferenceException(nameof(DatabaseFilePath));
            if (acqfifoinfo == null) throw new NullReferenceException(nameof(acqfifoinfo));
            DbProvider.DeleteRecord(DatabaseFilePath, acqfifoinfo, "GroupId");
        }
        static public void EditInfo(AcqFifoDbData acqfifoinfo)
        {
            if (!Directory.Exists(".\\Database")) throw new DbProvider.DatabaseNotExist();
            if (string.IsNullOrEmpty(DatabaseFilePath)) throw new NullReferenceException(nameof(DatabaseFilePath));
            if (acqfifoinfo == null) throw new NullReferenceException(nameof(acqfifoinfo));
            DbProvider.EditRecord(DatabaseFilePath, acqfifoinfo, "GroupId");
        }
        static public void DeleteAllInfo()
        {
            if (!Directory.Exists(".\\Database")) throw new DbProvider.DatabaseNotExist();
            if (string.IsNullOrEmpty(DatabaseFilePath)) throw new NullReferenceException(nameof(DatabaseFilePath));
            DbProvider.DeleteAll<AcqFifoDbData>(DatabaseFilePath);
        }
    }
}