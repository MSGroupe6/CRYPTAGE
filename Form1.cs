using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using System.IO;

namespace Cryptage
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        Document _doc;
      
        public Form1(Document doc)
        {
            InitializeComponent();
            _doc = doc;
        }

        private void Form1_Load(object sender, EventArgs e)
        {                     
                try
                {
                 
                }
                catch
                {
                }                 
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public bool RoomName()
        {
            if (checkBox_room_name.Checked == true)
                return true;
            else
                return false;
        }

        public bool RoomFinishes()
        {
            if (checkBox_room_finish.Checked==true)
                return true;
            else
                return false;
        }

        public bool RoomWalls()
        {
            if (checkBox_walls.Checked==true)
                return true;
            else
                return false;
        }


        public bool RoomDoors()
        {
            if (checkBox_doors.Checked)
                return true;
            else
                return false;
        }

        public bool RoomFurniture()
        {
            if (checkBox_furniture.Checked)
                return true;
            else
                return false;
        }

        public bool RoomAddFurnitures()
        {
            if (checkBox_add_furniture.Checked)
                return true;
            else
                return false;
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SelectAll_Click(object sender, EventArgs e)
        {
            checkBox_add_furniture.Checked = true;
            checkBox_doors.Checked = true;
            checkBox_furniture.Checked = true;
            checkBox_room_finish.Checked = true;
            checkBox_walls.Checked = true;
            checkBox_room_name.Checked = true;
        }

        private void Unselect_All_Click(object sender, EventArgs e)
        {
            checkBox_add_furniture.Checked = false;
            checkBox_doors.Checked = false;
            checkBox_furniture.Checked = false;
            checkBox_room_finish.Checked = false;
            checkBox_walls.Checked = false;
            checkBox_room_name.Checked = false;
        }
    }
}
