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

namespace EditGroupTable.Ver3
{
    public partial class EditGroupTable: UserControl
    {
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

        public List<ExcuteGroupDbProvider.ExcuteGroupData> BackupExcuteGoupDatas { get; set; }

        public EditGroupTable()
        {
            InitializeComponent();
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
                ShowGroup(_groups);
            }
            catch (Exception ex)
            {
                this.Datagridview.Rows.Clear();
                MessageBox.Show(ex.Message);
            }
        }

        public void ShowGroup(List<ExcuteGroupDbProvider.ExcuteGroupData> groups)
        {
            this.Datagridview.AllowUserToAddRows = false;
            if (groups == null) return;
            List<string> _cameraSerialNumbers = this.GetExistCamSN();
            this.Datagridview.Rows.Clear();
            this.CameraSerialNumber.DataSource = _cameraSerialNumbers;
            foreach (ExcuteGroupDbProvider.ExcuteGroupData group in groups)
            {
                this.Datagridview.Rows.Add(new DataGridViewRow());
                this.Datagridview.Rows[this.Datagridview.Rows.Count - 1].Cells[0].Value = group.GroupId;
                if (this.CameraSerialNumber.Items.Contains(group.CameraSerialNumber))
                {
                    this.Datagridview.Rows[this.Datagridview.Rows.Count - 1].Cells[1].Value = group.CameraSerialNumber;
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnEdit.BackColor = System.Drawing.SystemColors.GradientActiveCaption; ;
                this.BackupExcuteGoupDatas = BackupExcuteGroupData();
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

        public List<ExcuteGroupDbProvider.ExcuteGroupData> BackupExcuteGroupData()
        {
            List<ExcuteGroupDbProvider.ExcuteGroupData> list = new List<ExcuteGroupDbProvider.ExcuteGroupData>();
            if (Datagridview.Rows.Count == 0)
            {
                return new List<ExcuteGroupDbProvider.ExcuteGroupData>();
            }
            foreach (DataGridViewRow row in Datagridview.Rows)
            {
                if (row.Cells[0].Value == null || row.Cells[1].Value == null)
                {
                    continue;
                }
                if (string.IsNullOrEmpty(row.Cells[1].Value.ToString())) continue;
                ExcuteGroupDbProvider.ExcuteGroupData group = new ExcuteGroupDbProvider.ExcuteGroupData
                {
                    GroupId = row.Cells[0].Value.ToString(),
                    CameraSerialNumber = row.Cells[1].Value.ToString()
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
            ShowGroup(this.BackupExcuteGoupDatas);
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
                if (this.Datagridview.Rows.Count != 0)
                    foreach (DataGridViewRow row in this.Datagridview.Rows)
                    {
                        bool checkcondition = true;
                        if (row.Cells[0].Value == null || row.Cells[1].Value == null) checkcondition = false;
                        else
                        {
                            if (string.IsNullOrEmpty(row.Cells[0].Value.ToString()) || string.IsNullOrEmpty(row.Cells[1].Value.ToString())) checkcondition = false;
                        }
                        if (!checkcondition)
                        {
                            this.Datagridview.Rows.Remove(row);
                            continue;
                        }
                        ExcuteGroupDbProvider.ExcuteGroupData newGroup = new ExcuteGroupDbProvider.ExcuteGroupData
                        {
                            GroupId = row.Cells[0].Value.ToString(),
                            CameraSerialNumber = row.Cells[1].Value.ToString()
                        };
                        ExcuteGroupDbProvider.SaveGroupID(newGroup);
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
                ShowGroup(this.BackupExcuteGoupDatas);
                //}
                MessageBox.Show("Save Fault!");
            }
        }
        public List<string> GetExistCamSN()
        {
            //Test
            return new List<string>(new string[5] { "CAMERA1", "CAMERA2", "CAMERA3", "CAMERA4", "CAMERA5", });
        }

        private void Datagridview_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            string i = e.Exception.Message;
        }
    }
}
