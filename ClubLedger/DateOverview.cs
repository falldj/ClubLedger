﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace ClubLedger
{
    public partial class DateOverview : Form
    {

        public DateOverview(string startDate, string endDate)
        {
            SQLiteConnection c = new SQLiteConnection("Data Source=ledger");
            c.Open();
            InitializeComponent();

            //DateTime d = DateTime.ParseExact(startDate, "MM/dd/yyyy", null);
            //data.Text = d.ToLongDateString();
            try
            {
                if (startDate.Equals(endDate))
                {
                    SQLiteCommand com = new SQLiteCommand("SELECT * FROM transactions WHERE startDate ='" + startDate + "'", c);
                    SQLiteDataReader r = com.ExecuteReader();

                    while (r.Read())
                    {
                        dataGridView.Rows.Add(r["type"].ToString(), r["startDate"].ToString(), r["endDate"].ToString(), r["earned"].ToString(), r["spent"].ToString(), r["notes"].ToString());
                    }
                }
                else
                {
                    SQLiteCommand com = new SQLiteCommand("SELECT * FROM transactions WHERE startDate >='" + startDate + "' AND endDate <= '" + endDate + "'", c);
                    SQLiteDataReader r = com.ExecuteReader();

                    while (r.Read())
                    {
                        dataGridView.Rows.Add(r["type"].ToString(), r["startDate"].ToString(), r["endDate"].ToString(), r["earned"].ToString(), r["spent"].ToString(), r["notes"].ToString());
                    }
                }
            }
            catch (SQLiteException e)
            {
                //headers.Text = "ERROR!!!!!!!!" + e.ToString(); TODO: Fix dis sick error handling filth
            }
        }
    }
}