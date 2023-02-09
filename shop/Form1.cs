using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "coursework_shopDataSet.view_admin". При необходимости она может быть перемещена или удалена.
            this.view_adminTableAdapter.Fill(this.coursework_shopDataSet.view_admin);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "coursework_shopDataSet.view_sales". При необходимости она может быть перемещена или удалена.

            // TODO: данная строка кода позволяет загрузить данные в таблицу "coursework_shopDataSet.view_client". При необходимости она может быть перемещена или удалена.
            this.view_clientTableAdapter.Fill(this.coursework_shopDataSet.view_client);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "coursework_shopDataSet.product". При необходимости она может быть перемещена или удалена.
            this.productTableAdapter.Fill(this.coursework_shopDataSet.product);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "coursework_shopDataSet.group_product". При необходимости она может быть перемещена или удалена.
            this.group_productTableAdapter.Fill(this.coursework_shopDataSet.group_product);

            
            this.view_salesTableAdapter.Fill(this.coursework_shopDataSet.view_sales);


            foreach (DataRow row in this.coursework_shopDataSet.group_product.Rows)
            {
                checkedListBox1.Items.Add(row["obj_name"].ToString());
            }

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\coursework_shop.mdf;Integrated Security=True;Connect Timeout=30";
            string sql = "select Производитель from view_client group by Производитель";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Создаем объект DataAdapter
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                // Создаем объект Dataset
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                // Отображаем данные
                foreach (DataRow row in ds.Tables[0].Rows)
                    checkedListBox2.Items.Add(row["Производитель"].ToString());
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.group_productTableAdapter.FillBy(this.coursework_shopDataSet.group_product);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string sql = "1 = 1";

            List<string> checkedItems1 = new List<string>();
            foreach (var item in checkedListBox1.CheckedItems)
                checkedItems1.Add(item.ToString());

            if (e.NewValue == CheckState.Checked)
                checkedItems1.Add(checkedListBox1.Items[e.Index].ToString());
            else
                checkedItems1.Remove(checkedListBox1.Items[e.Index].ToString());

            List<string> checkedItems2 = new List<string>();
            foreach (var item in checkedListBox2.CheckedItems)
                checkedItems2.Add(item.ToString());

            if (checkedItems1.Count == 0 && checkedItems2.Count == 0)
            {
                richTextBox1.Text = "Фильтрация: нет";
            }
            else
            {
                richTextBox1.Text = "Фильтрация:";
            }


            if (checkedItems1.Count > 0)
            {

                sql += " and (1 = 0";
                foreach (string item in checkedItems1)
                {
                    sql += $" or Тип = '{item}'";
                    richTextBox1.Text += $" {item};";
                }
                sql += ")";
            }
            if (checkedItems2.Count > 0)
            {
                sql += " and (1 = 0";

                foreach (string item in checkedItems2)
                {
                    sql += $" or Производитель = '{item}'";
                    richTextBox1.Text += $" {item};";
                }
                sql += ")";
            }

            if (textBox1.Text.Length > 0)
            {
                sql += " and";
                sql += $" Цена >= {textBox1.Text}";
                richTextBox1.Text += $" Цена от {textBox1.Text};";

            }

            if (textBox2.Text.Length > 0)
            {
                sql += " and";
                sql += $" Цена <= {textBox2.Text}";
                richTextBox1.Text += $" Цена до {textBox2.Text};";
            }

            viewclientBindingSource.Filter = sql;
        }

        private void checkedListBox2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string sql = "1 = 1";

            List<string> checkedItems1 = new List<string>();
            foreach (var item in checkedListBox1.CheckedItems)
                checkedItems1.Add(item.ToString());

            List<string> checkedItems2 = new List<string>();
            foreach (var item in checkedListBox2.CheckedItems)
                checkedItems2.Add(item.ToString());

            if (e.NewValue == CheckState.Checked)
                checkedItems2.Add(checkedListBox2.Items[e.Index].ToString());
            else
                checkedItems2.Remove(checkedListBox2.Items[e.Index].ToString());

            if (checkedItems1.Count == 0 && checkedItems2.Count == 0)
            {
                richTextBox1.Text = "Фильтрация: нет";
            }
            else
            {
                richTextBox1.Text = "Фильтрация:";
            }


            if (checkedItems1.Count > 0)
            {

                sql += " and (1 = 0";
                foreach (string item in checkedItems1)
                {
                    sql += $" or Тип = '{item}'";
                    richTextBox1.Text += $" {item};";
                }
                sql += ")";
            }
            if (checkedItems2.Count > 0)
            {
                sql += " and (1 = 0";

                foreach (string item in checkedItems2)
                {
                    sql += $" or Производитель = '{item}'";
                    richTextBox1.Text += $" {item};";
                }
                sql += ")";
            }

            if (textBox1.Text.Length > 0)
            {
                sql += " and";
                sql += $" Цена >= {textBox1.Text}";
                richTextBox1.Text += $" Цена от {textBox1.Text};";

            }

            if (textBox2.Text.Length > 0)
            {
                sql += " and";
                sql += $" Цена <= {textBox2.Text}";
                richTextBox1.Text += $" Цена до {textBox2.Text};";
            }

            viewclientBindingSource.Filter = sql;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                checkedListBox2.SetItemChecked(i, false);
            }
            textBox1.Text = "";
            textBox2.Text = "";
            richTextBox1.Text = "Фильтрация: нет";

            viewclientBindingSource.Filter = null;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "1 = 1";

            List<string> checkedItems1 = new List<string>();
            foreach (var item in checkedListBox1.CheckedItems)
                checkedItems1.Add(item.ToString());

            List<string> checkedItems2 = new List<string>();
            foreach (var item in checkedListBox2.CheckedItems)
                checkedItems2.Add(item.ToString());

            if (checkedItems1.Count == 0 && checkedItems2.Count == 0)
            {
                richTextBox1.Text = "Фильтрация: нет";
            }
            else
            {
                richTextBox1.Text = "Фильтрация:";
            }

            
            if (checkedItems1.Count > 0)
            {
                
                sql += " and (1 = 0";
                foreach (string item in checkedItems1)
                {
                    sql += $" or Тип = '{item}'";
                    richTextBox1.Text += $" {item};";
                }
                sql += ")";
            }
            if (checkedItems2.Count > 0)
            {
                sql += " and (1 = 0";
                
                foreach (string item in checkedItems2)
                {
                    sql += $" or Производитель = '{item}'";
                    richTextBox1.Text += $" {item};";
                }
                sql += ")";
            }

            if (textBox1.Text.Length > 0)
            {
                sql += " and";
                sql += $" Цена >= {textBox1.Text}";
                richTextBox1.Text += $" Цена от {textBox1.Text};";
                
            }

            if (textBox2.Text.Length > 0)
            {
                sql += " and";
                sql += $" Цена <= {textBox2.Text}";
                richTextBox1.Text += $" Цена до {textBox2.Text};";
            }

            viewclientBindingSource.Filter = sql;

           
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string cons = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\coursework_shop.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(cons);
            con.Open();
            string q;
            
            string type = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            string pr = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            string mo = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            if (type == "Холодильник")
            {
                q = @"select product.price as 'Цена', info.brand as 'Производитель', info.model as 'Модель', info.country as 'Страна', fridge.volume as 'Характеристика 1', fridge.freezer as 'Характеристика 2', fridge.compressor as 'Характеристика 3'
                from (product join info on info.id_product = product.id join fridge on fridge.id_product = info.id)
                where info.brand = N'" + pr + "' and info.model = N'" + mo + "'";
            }
            else if (type == "Стиральная машина")
            {
                q = @"select product.price as 'Цена', info.brand as 'Производитель', info.model as 'Модель', info.country as 'Страна', washing_machine.max_weight as 'Характеристика 1', washing_machine.engine as 'Характеристика 2', washing_machine.loading as 'Характеристика 3'
                from (product join info on info.id_product = product.id join washing_machine on washing_machine.id_product = info.id)
                where info.brand = N'" + pr + "' and info.model = N'" + mo + "'";
            }
            else if (type == "Бойлер")
            {
                q = @"select product.price as 'Цена', info.brand as 'Производитель', info.model as 'Модель', info.country as 'Страна', boiler.volume as 'Характеристика 1', boiler.shape as 'Характеристика 2', boiler.heater as 'Характеристика 3'
                from (product join info on info.id_product = product.id join boiler on boiler.id_product = info.id)
                where info.brand = N'" + pr + "' and info.model = N'" + mo + "'";
            }
            else if (type == "Кондиционер")
            {
                q = @"select product.price as 'Цена', info.brand as 'Производитель', info.model as 'Модель', info.country as 'Страна', conditioner.mark as 'Характеристика 1', conditioner.room_square as 'Характеристика 2', conditioner.kind as 'Характеристика 3'
                from (product join info on info.id_product = product.id join conditioner on conditioner.id_product = info.id)
                where info.brand = N'" + pr + "' and info.model = N'" + mo + "'";
            }
            else if (type == "Электрочайник")
            {
                q = @"select product.price as 'Цена', info.brand as 'Производитель', info.model as 'Модель', info.country as 'Страна', electric_kettle.voltage_power as 'Характеристика 1', electric_kettle.volume as 'Характеристика 2', electric_kettle.material as 'Характеристика 3'
                from (product join info on info.id_product = product.id join electric_kettle on electric_kettle.id_product = info.id)
                where info.brand = N'" + pr + "' and info.model = N'" + mo + "'";
            }
            else if (type == "Мультиварка")
            {
                q = @"select product.price as 'Цена', info.brand as 'Производитель', info.model as 'Модель', info.country as 'Страна', multicooker.voltage_power as 'Характеристика 1', multicooker.volume as 'Характеристика 2', multicooker.kind as 'Характеристика 3'
                from (product join info on info.id_product = product.id join multicooker on multicooker.id_product = info.id)
                where info.brand = N'" + pr + "' and info.model = N'" + mo + "'";
            }
            else return;

            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader dr = cmd.ExecuteReader();
            
            while (dr.Read())
            {
                label3.Text = $"Цена: {dr.GetValue(0)} грн";
                label4.Text = $"Производитель: {dr.GetValue(1)}";
                label5.Text = $"Модель: {dr.GetValue(2)}";
                label6.Text = $"Страна: {dr.GetValue(3)}";
                if (type == "Холодильник")
                {
                    label7.Text = $"Объем холодильной камеры: {dr.GetValue(4)} л";
                    label8.Text = $"Расположение морозильного отдела: {dr.GetValue(5)}";
                    label9.Text = $"Тип компрессора: {dr.GetValue(6)}";
                }
                else if (type == "Стиральная машина")
                {
                    label7.Text = $"Максимальный вес загрузки: {dr.GetValue(4)} кг";
                    label8.Text = $"Тип двигателя: {dr.GetValue(5)}";
                    label9.Text = $"Тип загрузки: {dr.GetValue(6)}";
                }
                else if (type == "Бойлер")
                {
                    label7.Text = $"Объем: {dr.GetValue(4)} л";
                    label8.Text = $"Форма корпуса: {dr.GetValue(5)}";
                    label9.Text = $"Тип ТЭНа: {dr.GetValue(6)}";
                }
                else if (type == "Кондиционер")
                {
                    label7.Text = $"Маркировка: {dr.GetValue(4)}";
                    label8.Text = $"Обслуживаемая площадь: {dr.GetValue(5)} м^2";
                    label9.Text = $"Тип: {dr.GetValue(6)}";
                }
                else if (type == "Электрочайник")
                {
                    label7.Text = $"Мощность: {dr.GetValue(4)} Вт";
                    label8.Text = $"Объем: {dr.GetValue(5)} л";
                    label9.Text = $"Материал корпуса: {dr.GetValue(6)}";
                }
                else if (type == "Мультиварка")
                {
                    label7.Text = $"Мощность: {dr.GetValue(4)} Вт";
                    label8.Text = $"Объем: {dr.GetValue(5)} л";
                    label9.Text = $"Тип: {dr.GetValue(6)}";
                }
            }
            dr.Close();
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql = "1 = 1";
            if (textBox3.Text.Length > 0)
                sql += $" and [Номер чека] = '{textBox3.Text}'";
            if (textBox4.Text.Length > 0)
                sql += $" and Производитель = '{textBox4.Text}'";
            if (textBox5.Text.Length > 0)
                sql += $" and Модель = '{textBox5.Text}'";
            if (textBox6.Text.Length > 0)
                sql += $" and Дата = '{textBox6.Text}'";

            viewsalesBindingSource.Filter = sql;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            viewsalesBindingSource.Filter = null;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox7.Text.Length > 0 && textBox8.Text.Length > 0 && textBox9.Text.Length > 0)
            {
                string cons = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\coursework_shop.mdf;Integrated Security=True;Connect Timeout=30";
                SqlConnection con = new SqlConnection(cons);
                con.Open();
                System.DateTime date = System.DateTime.Now;
                string s = $"insert into sale(date_sale) values ('{date.Year}-{date.Month}-{date.Day}')";
                SqlCommand cmd = new SqlCommand(s, con);
                int k = cmd.ExecuteNonQuery();

                int id_sale = 0;
                s = @"select top 1 id from sale order by id desc";
                cmd = new SqlCommand(s, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    id_sale = Int32.Parse(dr.GetValue(0).ToString());
                }
                dr.Close();


                int id_prod = 0;
                s = $"select top 1 product.id from product join info on info.id_product = product.id where brand = N'{textBox7.Text}' and model = N'{textBox8.Text}'";
                cmd = new SqlCommand(s, con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    id_prod = Int32.Parse(dr.GetValue(0).ToString());
                }
                dr.Close();

                s = $"insert into sale_row(id_sale, id_product, k) values ({id_sale}, {id_prod}, {Int32.Parse(textBox9.Text)})";
                cmd = new SqlCommand(s, con);
                k = cmd.ExecuteNonQuery();


                this.view_salesTableAdapter.Fill(this.coursework_shopDataSet.view_sales);
                con.Close();
            }
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            panel3.Visible = false;
        }


        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            textBox10.Text = dataGridView3.Rows[row].Cells[0].Value.ToString();
            textBox11.Text = dataGridView3.Rows[row].Cells[1].Value.ToString();
            textBox12.Text = dataGridView3.Rows[row].Cells[2].Value.ToString();
            textBox13.Text = dataGridView3.Rows[row].Cells[3].Value.ToString();
            textBox14.Text = dataGridView3.Rows[row].Cells[4].Value.ToString();
            textBox15.Text = dataGridView3.Rows[row].Cells[5].Value.ToString();
            textBox16.Text = dataGridView3.Rows[row].Cells[6].Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string cons = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\coursework_shop.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(cons);
            con.Open();

            

            string s = $"select top 1 Тип from view_client where Производитель = N'{textBox10.Text}' and Модель = N'{textBox11.Text}'";
            SqlCommand cmd = new SqlCommand(s, con);
            SqlDataReader dr = cmd.ExecuteReader();
            string type = "";
            while (dr.Read())
            {
                type = dr.GetValue(0).ToString();
            }
            dr.Close();

            s = $"select top 1 product.id from product join info on info.id_product = product.id where info.brand = N'{textBox10.Text}' and info.model = N'{textBox11.Text}'";
            cmd = new SqlCommand(s, con);
            dr = cmd.ExecuteReader();
            int id = 0;
            while (dr.Read())
            {
                id = Int32.Parse(dr.GetValue(0).ToString());
            }
            dr.Close();

            s = $"update product set price = {Int32.Parse(textBox12.Text)} where id = {id}";
            cmd = new SqlCommand(s, con);
            int k = cmd.ExecuteNonQuery();

            s = $"update storage set k = {Int32.Parse(textBox16.Text)} where id_product = {id}";
            cmd = new SqlCommand(s, con);
            k = cmd.ExecuteNonQuery();

            if (type == "Холодильник")
            {
                s = $"update fridge set volume = {Int32.Parse(textBox13.Text)}, freezer = N'{textBox14.Text}', compressor = N'{textBox15.Text}' where id_product = {id}";
                cmd = new SqlCommand(s, con);
                k = cmd.ExecuteNonQuery();
            }
            else if (type == "Стиральная машина")
            {
                s = $"update washing_machine set max_weight = {Int32.Parse(textBox13.Text)}, engine = N'{textBox14.Text}', loading = N'{textBox15.Text}' where id_product = {id}";
                cmd = new SqlCommand(s, con);
                k = cmd.ExecuteNonQuery();
            }
            else if (type == "Бойлер")
            {
                s = $"update boiler set volume = {Int32.Parse(textBox13.Text)}, shape = N'{textBox14.Text}', heater = N'{textBox15.Text}' where id_product = {id}";
                cmd = new SqlCommand(s, con);
                k = cmd.ExecuteNonQuery();
            }
            else if (type == "Кондиционер")
            {
                s = $"update conditioner set mark = {Int32.Parse(textBox13.Text)}, room_square = {Int32.Parse(textBox14.Text)}, kind = N'{textBox15.Text}' where id_product = {id}";
                cmd = new SqlCommand(s, con);
                k = cmd.ExecuteNonQuery();
            }
            else if (type == "Электрочайник")
            {
                s = $"update electric_kettle set voltage_power = {Int32.Parse(textBox13.Text)}, volume = {Int32.Parse(textBox14.Text)}, material = N'{textBox15.Text}' where id_product = {id}";
                cmd = new SqlCommand(s, con);
                k = cmd.ExecuteNonQuery();
            }
            else if (type == "Мультиварка")
            {
                s = $"update multicooker set voltage_power = {Int32.Parse(textBox13.Text)}, volume = {Int32.Parse(textBox14.Text)}, kind = N'{textBox15.Text}' where id_product = {id}";
                cmd = new SqlCommand(s, con);
                k = cmd.ExecuteNonQuery();
            }

            this.view_clientTableAdapter.Fill(this.coursework_shopDataSet.view_client);
            this.view_salesTableAdapter.Fill(this.coursework_shopDataSet.view_sales);
            this.view_adminTableAdapter.Fill(this.coursework_shopDataSet.view_admin);
            con.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string cons = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\coursework_shop.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(cons);
            con.Open();

            int id = Int32.Parse(textBox17.Text);

            string s = $"delete from sale where id = {id}";
            SqlCommand cmd = new SqlCommand(s, con);
            int k = cmd.ExecuteNonQuery();

            this.view_salesTableAdapter.Fill(this.coursework_shopDataSet.view_sales);
            con.Close();
            textBox17.Text = "";
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox17.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
        }
    }
}
