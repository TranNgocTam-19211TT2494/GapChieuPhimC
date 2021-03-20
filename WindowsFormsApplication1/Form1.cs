using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Customer> dsKhachHang = new List<Customer>();
        private void Form1_Load(object sender, EventArgs e)
        {
            xuLyVe100GheLenGiaoDien();
        }

        private void xuLyVe100GheLenGiaoDien()
        {
            pnGhe.Controls.Clear();
            int ghe = 1;
            for(int i = 0; i < pnGhe.RowCount; i++)
            {
                for(int j = 0; j < pnGhe.ColumnCount; j++)
                {
                    Button btnGhe = new Button();
                    btnGhe.Text = ghe+"";
                    btnGhe.Dock = DockStyle.Fill;
                    btnGhe.TextAlign = ContentAlignment.MiddleCenter;
                    btnGhe.AutoSize = false;
                    btnGhe.Width = btnGhe.Height = 50;
                    btnGhe.BackColor = Color.White;
                    pnGhe.Controls.Add(btnGhe, j, i);
                    ghe++;
                    btnGhe.Click += BtnGhe_Click;
                }
            }
        }

        private void BtnGhe_Click(object sender, EventArgs e)
        {
            Button btnGhe = sender as Button;
            if (btnGhe.BackColor == Color.White)
                btnGhe.BackColor = Color.Green;
            else if (btnGhe.BackColor == Color.Green)
                btnGhe.BackColor = Color.White;
            else if (btnGhe.BackColor == Color.Yellow)
                MessageBox.Show("Ghế "+btnGhe.Text+" Đã Có Người Đặt Ròi !");
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            frmThongTinKhachHang frm = new frmThongTinKhachHang();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //lấy ghế màu xanh ra để đổi thành màu vàng 
                Customer cus = new Customer();
                cus.name = frm.txtTen.Text;
                cus.phone = frm.txtPhone.Text;
                for(int i = 0; i < pnGhe.Controls.Count; i++)
                {
                    Button btnGhe = pnGhe.Controls[i] as Button;
                    if (btnGhe.BackColor == Color.Green)
                    {
                        btnGhe.BackColor = Color.Yellow;
                        int ghe = int.Parse(btnGhe.Text);
                        cus.Ghe.Add(ghe);
                    }
                    
                }
                lblThanhTien.Text = cus.TinhTien + " VNĐ";
                dsKhachHang.Add(cus);
                HienThiTongTien();
                HienThiThongTinLenListBox();
            }
        }

        private void HienThiThongTinLenListBox()
        {
            lstKhachHang.Items.Clear();
            foreach(Customer cus in dsKhachHang)
            {
                lstKhachHang.Items.Add(cus);
            }
        }

        private void HienThiTongTien()
        {
            int sum = 0;
            foreach (Customer cus in dsKhachHang)
            {

                sum += cus.TinhTien;
                lblTongTien.Text = sum + " VNĐ";
            }
        }
        //khi nhan vao danh sach trong listKhachHang thi hien thi so tien của khach hang do
        private void lstKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstKhachHang.SelectedIndex != -1)
            {
                Customer cus = lstKhachHang.SelectedItem as Customer;
                
                lblThanhTien.Text = cus.TinhTien + " VNĐ";

                for (int i = 0; i < pnGhe.Controls.Count; i++)
                {
                    Button btnGhe = pnGhe.Controls[i] as Button;
                    if (btnGhe.BackColor == Color.Pink)
                    {
                        btnGhe.BackColor = Color.Yellow;
                    }
                    int maGhe = int.Parse(btnGhe.Text);
                    for(int j = 0; j < cus.Ghe.Count; j++)
                    {
                        int gheDat = cus.Ghe[j];

                        if (maGhe == gheDat)
                        {
                            btnGhe.BackColor = Color.Pink;
                        }
                    }
                    
                }     
                      
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (lstKhachHang.SelectedIndex != -1)
            {
                Customer cus = lstKhachHang.SelectedItem as Customer;
                // kiểm tra DateTime.now với cus.GioDatGhe
                //quá 30 phút thì không cho hủy
                for(int i = 0; i < pnGhe.Controls.Count; i++)
                {
                    Button btnGhe = pnGhe.Controls[i] as Button;
                    int maGhe = int.Parse(btnGhe.Text);
                    int temp = 0;
                    while(cus.Ghe.Count > 0 && temp < cus.Ghe.Count)
                    {
                        int gheDat = cus.Ghe[0];
                        if (maGhe == gheDat)
                        {
                            btnGhe.BackColor = Color.White;
                            cus.Ghe.Remove(gheDat);
                        }
                        temp++;
                    }
                }
                dsKhachHang.Remove(cus);
                HienThiThongTinLenListBox();
                HienThiTongTien();
            }
            else
            {
                MessageBox.Show("Bạn Hãy Chọn Khách Hàng Để Hủy");
            }
        }
    }
}
