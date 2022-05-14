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

namespace EditGroupTable.Ver1
{
    public partial class EditGroupTable: UserControl
    {
        private int ValidedRow = 0;
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

        public EditGroupTable()
        {
            InitializeComponent();
        }
        public void InitialTable()
        {

            try
            {
                List<ExcuteGroupDbProvider.ExcuteGroupData> _groups = ExcuteGroupDbProvider.GetGroupIDs();
                ValidedRow = _groups.Count;
                ShowGroup(_groups);
            }
            catch
            {
                this.Datagridview.Rows.Clear();
                this.ValidedRow = 0;
            }
            this.Datagridview.Rows.Add(new DataGridViewRow());
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Datagridview.Rows[this.ValidedRow].Cells[0].Value == null) return;
                if (this.Datagridview.Rows[this.ValidedRow].Cells[1].Value == null) return;
                if (string.IsNullOrEmpty(this.Datagridview.Rows[this.ValidedRow].Cells[0].Value.ToString())) return;
                if (string.IsNullOrEmpty(this.Datagridview.Rows[this.ValidedRow].Cells[1].Value.ToString())) return;
                if (this.Datagridview.Rows.Count > this.ValidedRow +1) return;
                ExcuteGroupDbProvider.ExcuteGroupData newGroup = new ExcuteGroupDbProvider.ExcuteGroupData
                {
                    GroupId = this.Datagridview.Rows[this.ValidedRow].Cells[0].Value.ToString(),
                    CameraSerialName = this.Datagridview.Rows[this.ValidedRow].Cells[1].Value.ToString()
                };
                ExcuteGroupDbProvider.SaveGroupID(newGroup);
                this.ValidedRow++;
                MessageBox.Show("Added!");
                this.Datagridview.Rows.Add(new DataGridViewRow());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                
            }
        }

        public void ShowGroup(List<ExcuteGroupDbProvider.ExcuteGroupData> groups)
        {
            if (groups == null) return;
            foreach(ExcuteGroupDbProvider.ExcuteGroupData group in groups)
            {
                DataGridViewRow newrow = new DataGridViewRow();
                DataGridViewTextBoxCell groupid = new DataGridViewTextBoxCell();
                groupid.Value = group.GroupId;
                DataGridViewTextBoxCell cameraserialnumber = new DataGridViewTextBoxCell();
                cameraserialnumber.Value = group.CameraSerialName;
                newrow.Cells.AddRange(new DataGridViewCell[2]{ groupid,cameraserialnumber });
                this.Datagridview.Rows.Add(newrow);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Datagridview.CurrentRow == null) return;
                if (this.Datagridview.CurrentRow.Cells[0] == null) return;
                if (this.Datagridview.CurrentRow.Cells[1] == null) return;
                if (this.Datagridview.CurrentRow.Cells[0].Value == null) return;
                if (this.Datagridview.CurrentRow.Cells[1].Value == null) return;
                string _groupid = this.Datagridview.CurrentRow.Cells[0].Value.ToString();
                string _camserial = this.Datagridview.CurrentRow.Cells[1].Value.ToString();
                if (string.IsNullOrEmpty(_groupid) || string.IsNullOrEmpty(_camserial)) return;
                if (MessageBox.Show($"Delete Group {_groupid}?", "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK) return; 
                ExcuteGroupDbProvider.ExcuteGroupData deletegroup = new ExcuteGroupDbProvider.ExcuteGroupData
                {
                    GroupId = _groupid,
                    CameraSerialName = _camserial,
                };
                ExcuteGroupDbProvider.DeleteGoupID(deletegroup);
                this.Datagridview.Rows.Remove(this.Datagridview.CurrentRow);
                this.ValidedRow -= 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Datagridview.CurrentRow == null) return;
                if (this.Datagridview.CurrentRow.Cells[0] == null) return;
                if (this.Datagridview.CurrentRow.Cells[1] == null) return;
                if (this.Datagridview.CurrentRow.Cells[0].Value == null) return;
                if (this.Datagridview.CurrentRow.Cells[1].Value == null) return;
                string _groupid = this.Datagridview.CurrentRow.Cells[0].Value.ToString();
                string _camserial = this.Datagridview.CurrentRow.Cells[1].Value.ToString();
                if (string.IsNullOrEmpty(_groupid) || string.IsNullOrEmpty(_camserial)) return;
                if (MessageBox.Show($"Edit Group {_groupid}?", "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK) return;
                ExcuteGroupDbProvider.ExcuteGroupData deletegroup = new ExcuteGroupDbProvider.ExcuteGroupData
                {
                    GroupId = _groupid,
                    CameraSerialName = _camserial,
                };
                ExcuteGroupDbProvider.EditGroupID(deletegroup);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
