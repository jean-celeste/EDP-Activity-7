﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDP_GUI
{
    public partial class Sales : UserControl
    {
        private MySqlConnection conn;
        public DataGridView DataGridViewControl => dataGridView1;
        

        public Sales()
        {
            InitializeComponent();
            conn = DatabaseConnection.GetConnection();
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                DataTable dataTable = new DataTable();

                string query = @"
                SELECT b.book_id, b.title, b.price AS unit_price, COUNT(c.borrowed_books) AS quantity,
                       (b.price * COUNT(c.borrowed_books)) AS total
                FROM books_db.customers AS c
                JOIN books_db.books AS b ON c.borrowed_books = b.book_id
                GROUP BY b.book_id, b.title, b.price
                ORDER BY b.book_id";

                MySqlCommand command = new MySqlCommand(query, conn);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        dataTable.Load(reader);

                        dataGridView1.Rows.Clear();

                        foreach (DataRow row in dataTable.Rows)
                        {
                            dataGridView1.Rows.Add(
                                row["book_id"].ToString(),
                                row["title"].ToString(),
                                row["unit_price"].ToString(),
                                row["quantity"].ToString(),
                                row["total"].ToString()
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sales data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }

}
