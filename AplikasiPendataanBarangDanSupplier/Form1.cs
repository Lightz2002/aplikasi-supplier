using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace AplikasiPendataanBarangDanSupplier
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        MySqlConnection con = new MySqlConnection(@"server=localhost;user id=root;database=sdbtest; Convert Zero Datetime=True");
        public int Id_barang;
        public int Id_supplier;
        public int Id_barang_rusak;



        private void Form1_Load(object sender, EventArgs e)
        {
            GetSuppliesRecord();
            GetSupplierRecord();
            GetBrokenSuppliesRecord();
            fillIdBarangCombo();
        }

        private void fillIdSupplierCombo()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM supplierTb", con);
            MySqlDataReader sdr;

            try
            {
                con.Open();

                sdr = cmd.ExecuteReader();
                if (comboBoxIdSupplier.Items.Count > 0) {
                    comboBoxIdSupplier.Items.Clear();
                }

                while(sdr.Read())
                {
                    string Id = sdr.GetString("Id_supplier");
                    comboBoxIdSupplier.Items.Add(Id);
                }
                con.Close();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             

        }
        private void fillIdBarangCombo()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM suppliestb", con);
            MySqlDataReader sdr;

            try
            {
                con.Open();

                sdr = cmd.ExecuteReader();
                if (comboBoxIdBarang.Items.Count > 0)
                {
                    comboBoxIdBarang.Items.Clear();
                }
                while (sdr.Read())
                {
                    string Id = sdr.GetString("Id_barang");
                    comboBoxIdBarang.Items.Add(Id);
                }
                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void GetSuppliesRecord()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM SuppliesTb", con);
            DataTable dt = new DataTable();

            con.Open();

            MySqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dt;
            fillIdBarangCombo();
        }
        private void GetSupplierRecord()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM suppliertb", con);
            DataTable dt = new DataTable();

            con.Open();

            MySqlDataReader sdr = cmd.ExecuteReader();

            dt.Load(sdr);
            con.Close();

            dataGridView3.DataSource = dt;
            fillIdSupplierCombo();


        }
        private void GetBrokenSuppliesRecord()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM  brokensuppliestb", con);
            DataTable dt = new DataTable();

            con.Open();

            MySqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView4.DataSource = dt;
        }
        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (InputSuppliesIsValid())
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO SuppliesTb VALUES (NULL, @Nama_barang, @Jumlah_barang, @Uom, @Harga_barang, @Tanggal_masuk, @Id_supplier)", con);
               
                cmd.CommandType = CommandType.Text;
               
                cmd.Parameters.AddWithValue("@Nama_barang", txtNamaBarang.Text);
                cmd.Parameters.AddWithValue("@Jumlah_barang", txtJumlahBarang.Text);
                cmd.Parameters.AddWithValue("@Uom", txtUom.Text);
                cmd.Parameters.AddWithValue("@Harga_barang", txtHargaBarang.Text);
                cmd.Parameters.AddWithValue("@Tanggal_masuk", txtTanggalMasuk.Value);
                cmd.Parameters.AddWithValue("@Id_supplier", comboBoxIdSupplier.SelectedItem.ToString());


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Data baru telah berhasil ditambahkan ke database", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetSuppliesRecord();
                ResetSuppliesForm();
            }
        }
       

        private bool InputSuppliesIsValid()
        {
            if (txtNamaBarang.Text == string.Empty)
            {
                MessageBox.Show("Nama Barang Tidak Boleh Kosong", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } else if (comboBoxIdSupplier.Items.ToString() == null)
            {
                MessageBox.Show("Id Supplier Tidak Boleh Kosong", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private bool InputSupplierIsValid()
        {
             if (txtNamaSupplier.Text == string.Empty)
            {
                MessageBox.Show("Nama Supplier Tidak Boleh Kosong", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private bool InputBrokenSuppliesIsValid()
        {
            if (txtJumlahBarangRusak.Text == string.Empty && txtPenyebabBarangRusak.Text == string.Empty)
            {
                MessageBox.Show("Jumlah dan Penyebab Barang Rusak  Tidak Boleh Kosong", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void ResetSuppliesForm()
        {
            Id_barang = 0;
            txtNamaBarang.Clear();
            txtHargaBarang.Clear();
            txtUom.Clear();
            txtJumlahBarang.Clear();
            txtTanggalMasuk.CustomFormat = " ";
            comboBoxIdSupplier.Text = " ";

            txtNamaBarang.Focus();
        }
        private void ResetSupplierForm()
        {
            Id_supplier = 0;
            txtNamaSupplier.Clear();
           
            txtNamaSupplier.Focus();
        }
        private void ResetBrokenSuppliesForm()
        {
            Id_barang_rusak = 0;
            txtJumlahBarangRusak.Clear();
            txtPenyebabBarangRusak.Clear();
            comboBoxIdBarang.Text = "";

            txtJumlahBarangRusak.Focus();
        }
        private void input_nama_barang_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetSuppliesForm();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            Id_barang = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            txtNamaBarang.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtJumlahBarang.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtUom.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtHargaBarang.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();

            if (dataGridView1.SelectedRows[0].Cells[5].Value == System.DBNull.Value)
            {
                txtTanggalMasuk.CustomFormat = " ";
            } else
            {
                txtTanggalMasuk.CustomFormat = "dd-MM-yyyy";
                txtTanggalMasuk.Value = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[5].Value);

            }
            comboBoxIdSupplier.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(Id_barang > 0)
            {
                MySqlCommand cmd = new MySqlCommand("UPDATE SuppliesTb SET Nama_barang = @Nama_barang, Jumlah_barang = @Jumlah_barang,  Uom = @Uom , Harga_barang = @Harga_barang, Tanggal_masuk = @Tanggal_masuk, Id_supplier = @Id_supplier WHERE Id_barang = @ID", con);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Nama_barang", txtNamaBarang.Text);
                cmd.Parameters.AddWithValue("@Jumlah_barang", txtJumlahBarang.Text);
                cmd.Parameters.AddWithValue("@Uom", txtUom.Text);
                cmd.Parameters.AddWithValue("@Harga_barang", txtHargaBarang.Text);
                cmd.Parameters.AddWithValue("@Tanggal_masuk", txtTanggalMasuk.Value);
                cmd.Parameters.AddWithValue("@Id_supplier", comboBoxIdSupplier.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@ID", this.Id_barang);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Data telah berhasil diupdate", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetSuppliesRecord();
                ResetSuppliesForm();
            }
            else
            {
                MessageBox.Show("Pilih Seseorang Untuk Diupdate", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Id_barang > 0)
            {
                MySqlCommand cmd = new MySqlCommand("DELETE FROM SuppliesTb WHERE Id_barang = @ID", con);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ID", this.Id_barang);
              
                con.Open();
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Data telah berhasil dihapus", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetSuppliesRecord();
                ResetSuppliesForm();
            }
            else
            {
                MessageBox.Show("Pilih barang Untuk Dihapus", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

               con.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM SuppliesTb WHERE Nama_barang like '%"+ txtSearchNamaBarang.Text.ToString()+"%'", con);
                DataTable dt = new DataTable();


                MySqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                con.Close();

                dataGridView1.DataSource = dt;
       
            
        }

        private void label10_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtIdSupplier_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtSearchBar_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Id_supplier > 0)
            {
                MySqlCommand cmd = new MySqlCommand("UPDATE SupplierTb SET Nama_supplier = @Nama_supplier WHERE Id_supplier = @ID", con);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Nama_supplier", txtNamaSupplier.Text);
                cmd.Parameters.AddWithValue("@ID", this.Id_supplier);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Data telah berhasil diupdate", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetSupplierRecord();
                ResetSupplierForm();
            }
            else
            {
                MessageBox.Show("Pilih Seseorang Untuk Diupdate", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Id_supplier > 0)
            {
                MySqlCommand cmd = new MySqlCommand("DELETE FROM SupplierTb WHERE Id_supplier = @ID", con);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ID", this.Id_supplier);

                con.Open();
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Data telah berhasil dihapus", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetSupplierRecord();
                ResetSupplierForm();
            }
            else
            {
                MessageBox.Show("Pilih Seseorang Untuk Dihapus", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (InputSupplierIsValid())
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO suppliertb VALUES (NULL, @Nama_supplier)", con);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Nama_supplier", txtNamaSupplier.Text);
               


                con.Open();
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Data baru telah berhasil ditambahkan ke database", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetSupplierRecord();
                ResetSupplierForm();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ResetSupplierForm();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Id_supplier = Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[0].Value);
            txtNamaSupplier.Text = dataGridView3.SelectedRows[0].Cells[1].Value.ToString();
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(InputBrokenSuppliesIsValid())
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO brokensuppliesTb VALUES (NULL, @Jumlah_barang_rusak, @Penyebab_barang_rusak, @Id_barang)", con);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Jumlah_barang_rusak", txtJumlahBarangRusak.Text);
                cmd.Parameters.AddWithValue("@Penyebab_barang_rusak", txtPenyebabBarangRusak.Text);
                cmd.Parameters.AddWithValue("@Id_barang", comboBoxIdBarang.SelectedItem.ToString());


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Data baru telah berhasil ditambahkan ke database", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetBrokenSuppliesRecord();
                ResetBrokenSuppliesForm();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Id_barang_rusak > 0)
            {
                MySqlCommand cmd = new MySqlCommand("UPDATE brokensuppliestb SET Jumlah_barang_rusak = @Jumlah_barang_rusak, Penyebab_barang_rusak = @Penyebab_barang_rusak, Id_barang = @Id_barang WHERE Id_barang_rusak = @ID", con);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Jumlah_barang_rusak", txtJumlahBarangRusak.Text);
                cmd.Parameters.AddWithValue("@Penyebab_barang_rusak", txtPenyebabBarangRusak.Text);
                cmd.Parameters.AddWithValue("@Id_barang", comboBoxIdBarang.SelectedItem.ToString());

                cmd.Parameters.AddWithValue("@ID", this.Id_barang_rusak);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Data telah berhasil diupdate", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetBrokenSuppliesRecord();
                ResetBrokenSuppliesForm();
            }
            else
            {
                MessageBox.Show("Pilih Seseorang Untuk Diupdate", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Id_barang_rusak = Convert.ToInt32(dataGridView4.SelectedRows[0].Cells[0].Value);
            txtJumlahBarangRusak.Text = dataGridView4.SelectedRows[0].Cells[1].Value.ToString();
            txtPenyebabBarangRusak.Text = dataGridView4.SelectedRows[0].Cells[2].Value.ToString();
            comboBoxIdBarang.Text = dataGridView4.SelectedRows[0].Cells[3].Value.ToString();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Id_barang_rusak > 0)
            {
                MySqlCommand cmd = new MySqlCommand("DELETE FROM brokensuppliestb WHERE Id_barang = @ID", con);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ID", this.Id_barang_rusak);

                con.Open();
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Data telah berhasil dihapus", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetBrokenSuppliesRecord();
                ResetBrokenSuppliesForm();
            }
            else
            {
                MessageBox.Show("Pilih barang Untuk Dihapus", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ResetBrokenSuppliesForm();
        }

        private void txtTanggalMasuk_ValueChanged(object sender, EventArgs e)
        {
            txtTanggalMasuk.CustomFormat = "dd/MM/yyyy";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            con.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM suppliertb WHERE Nama_supplier like '%" + txtSearchNamaSupplier.Text.ToString() + "%'", con);
            DataTable dt = new DataTable();


            MySqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView3.DataSource = dt;

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

            con.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM brokensuppliestb WHERE Penyebab_barang_rusak like '%" + txtSearchPenyebabBarangRusak.Text.ToString() + "%'", con);
            DataTable dt = new DataTable();


            MySqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView4.DataSource = dt;


        }
    }
}
