using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FifoGroup;

namespace EditGroupTable.Ver4
{
    public partial class EditGroupTable : UserControl
    {
        public class GroupData
        {
            public string GroupId { get; set; }
            public string SerialNumber { get; set; }
            public string VideoFormat { get; set; }
            public string PixelFormat { get; set; }
        }
        public event EventHandler GroupAdded;
        public event EventHandler GroupDeleted;
        public event EventHandler GroupEdited;
        public void OnGroupAdded()
        {
            GroupAdded?.Invoke(this, EventArgs.Empty);
        }
        public void OnGroupDeleted()
        {
            GroupDeleted?.Invoke(this, EventArgs.Empty);
        }
        public void OnGroupEdited()
        {
            GroupEdited?.Invoke(this, EventArgs.Empty);
        }

        public List<GroupData> BackupGroupDatas { get; set; }


        public EditGroupTable()
        {
            InitializeComponent();
            this.Datagridview.Columns[0].Width = 80;
            this.Datagridview.Columns[1].Width = 100;
        }
        public void InitialTable()
        {
            this.btnAccept.Enabled = false;
            this.btnCancel.Enabled = false;
            this.Datagridview.ReadOnly = true;
            this.btnEdit.Enabled = true;
            try
            {
                List<ExcuteGroupDbProvider.ExcuteGroupData> _groups = ExcuteGroupDbProvider.GetGroupIDs();
                List<AcqFifoInfoDbProvider.AcqFifoDbData> _infos = AcqFifoInfoDbProvider.GetAcqFifoInfos();
                List<GroupData> groupdatas = CreateGroupData(_groups,_infos);
                ShowGroup(groupdatas);
            }
            catch (Exception ex)
            {
                this.Datagridview.Rows.Clear();
                MessageBox.Show(ex.Message);
            }
        }

        public void ShowGroup(List<GroupData> groups)
        {
            this.Datagridview.AllowUserToAddRows = false;
            if (groups == null) return;
            List<string> _cameraSerialNumbers = this.GetExistCamSN();
            List<string> _videoformat = GetAvailableVF();
            List<string> _pixelformat = GetPixelFormats();
            this.Datagridview.Rows.Clear();
            this.CameraSerialNumber.DataSource = _cameraSerialNumbers;
            this.PixelFormat.DataSource = _pixelformat;
            this.VideoFormat.DataSource = _videoformat;
            foreach (GroupData group in groups)
            {
                this.Datagridview.Rows.Add(new DataGridViewRow());
                this.Datagridview.Rows[this.Datagridview.Rows.Count - 1].Cells[0].Value = group.GroupId;
                if (this.CameraSerialNumber.Items.Contains(group.SerialNumber))
                {
                    this.Datagridview.Rows[this.Datagridview.Rows.Count - 1].Cells[1].Value = group.SerialNumber;
                }
                if (this.VideoFormat.Items.Contains(group.VideoFormat))
                {
                    this.Datagridview.Rows[this.Datagridview.Rows.Count - 1].Cells[2].Value = group.VideoFormat;
                }
                if (this.PixelFormat.Items.Contains(group.PixelFormat))
                {
                    this.Datagridview.Rows[this.Datagridview.Rows.Count - 1].Cells[3].Value = group.PixelFormat;
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnEdit.BackColor = System.Drawing.SystemColors.GradientActiveCaption; ;
                this.BackupGroupDatas = BackupGroupData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            this.btnAccept.Enabled = true;
            this.btnCancel.Enabled = true;
            this.Datagridview.ReadOnly = false;
            this.btnEdit.Enabled = false;
            this.Datagridview.AllowUserToAddRows = true;
        }

        public List<GroupData> BackupGroupData()
        {
            List<GroupData> list = new List<GroupData>();
            if (Datagridview.Rows.Count == 0)
            {
                return new List<GroupData>();
            }
            foreach (DataGridViewRow row in Datagridview.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value == null) continue;
                    if (string.IsNullOrEmpty(cell.Value.ToString())) continue;
                }
                if (string.IsNullOrEmpty(row.Cells[1].Value.ToString())) continue;
                GroupData group = new GroupData()
                {
                    GroupId = row.Cells[0].Value.ToString(),
                    SerialNumber = row.Cells[1].Value.ToString(),
                    VideoFormat = row.Cells[2].Value.ToString(),
                    PixelFormat = row.Cells[3].Value.ToString(),
                };
                list.Add(group);
            }
            return list;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.btnAccept.Enabled = false;
            this.btnCancel.Enabled = false;
            this.Datagridview.ReadOnly = true;
            this.btnEdit.Enabled = true;
            this.btnEdit.BackColor = Color.Transparent;
            ShowGroup(this.BackupGroupDatas);
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnAccept.Enabled = false;
                this.btnCancel.Enabled = false;
                this.Datagridview.ReadOnly = true;
                this.btnEdit.Enabled = true;
                this.btnEdit.BackColor = Color.Transparent;
                this.Datagridview.AllowUserToAddRows = false;
                ExcuteGroupDbProvider.DeleteAllGroups();
                AcqFifoInfoDbProvider.DeleteAllInfo();
                if (this.Datagridview.Rows.Count != 0)
                    foreach (DataGridViewRow row in this.Datagridview.Rows)
                    {
                        bool checkcondition = true;
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Value == null)
                            {
                                checkcondition = false;
                                break;
                            }
                            if (string.IsNullOrEmpty(cell.Value.ToString()))
                            {
                                checkcondition = false;
                                break;
                            }
                        }
                        if (!checkcondition)
                        {
                            this.Datagridview.Rows.Remove(row);
                            continue;
                        }
                        ExcuteGroupDbProvider.ExcuteGroupData newGroup = new ExcuteGroupDbProvider.ExcuteGroupData
                        {
                            GroupId = row.Cells[0].Value.ToString(),
                            CameraSerialNumber = row.Cells[1].Value.ToString(),
                        };
                        AcqFifoInfoDbProvider.AcqFifoDbData newinfo = new AcqFifoInfoDbProvider.AcqFifoDbData
                        {
                            GroupId = row.Cells[0].Value.ToString(),
                            SerialNumber = row.Cells[1].Value.ToString(),
                            VideoFormat = row.Cells[2].Value.ToString(),
                            PixelFormat = row.Cells[3].Value.ToString(),
                        };
                        ExcuteGroupDbProvider.SaveGroupID(newGroup);
                        AcqFifoInfoDbProvider.SaveInfo(newinfo);
                    }
                MessageBox.Show("Table was Saved!");
            }
            catch (Exception ex)
            {
                this.btnAccept.Enabled = false;
                this.btnCancel.Enabled = false;
                this.Datagridview.ReadOnly = true;
                this.btnEdit.Enabled = true;
                this.btnEdit.BackColor = Color.Transparent;
                this.Datagridview.Rows.Clear();
                ShowGroup(this.BackupGroupDatas);
                //}
                MessageBox.Show("Save Fault!");
            }
        }
        public List<string> GetExistCamSN()
        {
            //Test
            return new List<string>(new string[6] { "None", "CAMERA1", "CAMERA2", "CAMERA3", "CAMERA4", "CAMERA5", });
        }
        /// <summary>
        /// Get Available Video Format
        /// </summary>
        /// <returns></returns>
        public List<string> GetAvailableVF()
        {
            return new List<string>(new string[2] { "Generic GigEVision (Mono)", "Generic GigEVision (Color)" });
        }
        public List<string> GetPixelFormats()
        {
            return new List<string>(new string[2] { "Format8Grey", "Format16Grey" });
        }
        private void Datagridview_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            string i = e.Exception.Message;
        }
        private void ValidateSelect(int index)
        {
            if (this.Datagridview.Rows[index].Cells[1].Value == null) return;
            string _selectserialnumber = this.Datagridview.Rows[index].Cells[1].Value.ToString();
            if (_selectserialnumber == "None") return;
            foreach (DataGridViewRow row in this.Datagridview.Rows)
            {
                if (row.Index == index) continue;
                if (row.Cells[1].Value == null) continue;
                string _serialnumber = row.Cells[1].Value.ToString();
                if (_selectserialnumber.Equals(_serialnumber))
                {
                    MessageBox.Show($"{_selectserialnumber} was selected by other Group!");
                    this.Datagridview.Rows[index].Cells[1].Value = "None";
                }
            }
        }
        private void Datagridview_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 1) return;
            ValidateSelect(e.RowIndex);
        }
        public static List<GroupData> CreateGroupData(List<ExcuteGroupDbProvider.ExcuteGroupData> groups, List<AcqFifoInfoDbProvider.AcqFifoDbData> infos)
        {
            List<GroupData> result = new List<GroupData>();
            foreach(ExcuteGroupDbProvider.ExcuteGroupData group in groups)
            {
                foreach(AcqFifoInfoDbProvider.AcqFifoDbData info in infos)
                {
                    if(group.GroupId == info.GroupId)
                    {
                        GroupData groupData = new GroupData
                        {
                            GroupId = group.GroupId,
                            SerialNumber = group.CameraSerialNumber,
                            PixelFormat = info.PixelFormat,
                            VideoFormat = info.VideoFormat
                        };
                        result.Add(groupData);
                        break;
                    }
                }
            }
            return result;
        }
        public static void ExploreGroupData(List<GroupData> groupdatas, List<ExcuteGroupDbProvider.ExcuteGroupData> groups, List<AcqFifoInfoDbProvider.AcqFifoDbData> infos)
        {
            if (groups == null || infos == null|| groupdatas == null) throw new ArgumentNullException();
            groups.Clear();
            infos.Clear();
            foreach(GroupData groupData in groupdatas)
            {
                ExcuteGroupDbProvider.ExcuteGroupData group = new ExcuteGroupDbProvider.ExcuteGroupData
                {
                    GroupId = groupData.GroupId,
                    CameraSerialNumber = groupData.SerialNumber,
                };
                groups.Add(group);
                AcqFifoInfoDbProvider.AcqFifoDbData info = new AcqFifoInfoDbProvider.AcqFifoDbData
                {
                    GroupId = groupData.GroupId,
                    SerialNumber = groupData.SerialNumber,
                    PixelFormat = groupData.PixelFormat,
                    VideoFormat = groupData.VideoFormat,
                    Port = 0
                };
                infos.Add(info);
            }
        }
    }
}
