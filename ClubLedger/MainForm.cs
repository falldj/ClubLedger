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
    public partial class MainForm : Form
    {
        private string selectedStartDate;
        private string selectedEndDate;

        public MainForm()
        {
            InitializeComponent();
            selectedStartDate = monthCalendar1.SelectionStart.ToShortDateString();
            selectedEndDate = monthCalendar1.SelectionStart.ToShortDateString();
            RefreshBoldDates();
        }

        public void RefreshBoldDates()
        {
            SQLiteConnection c = new SQLiteConnection("Data Source=ledger");
            c.Open();
            SQLiteCommand com = new SQLiteCommand("SELECT startDate, endDate FROM transactions WHERE startDate != \"\"", c);
            SQLiteDataReader r = com.ExecuteReader();
            while (r.Read())
            {
                string[] split = r["startDate"].ToString().Split('/');
                monthCalendar1.AddBoldedDate(new DateTime(Int32.Parse(split[2].Split(' ')[0]), Int32.Parse(split[0]), Int32.Parse(split[1])));
            }
            this.Refresh();
        }

        private void LedgerButton_Click(object sender, EventArgs e)
        {
            DateOverview LF = new DateOverview(selectedStartDate, selectedEndDate);
            LF.Show();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            SelectionRange s = monthCalendar1.SelectionRange;
            string startDate = s.ToString();
            string[] splitDate = startDate.Split(' ');

            startDate = splitDate[2];
            string endDate = splitDate[6];
            selectedStartDate = startDate;
            selectedEndDate = endDate;

            string[] splittedStartDate = selectedStartDate.Split('/');
            string[] splittedEndDate = selectedEndDate.Split('/');
            //HACK: Multiple dirty if-statements. Check if there's an easier way to format dates here.
            if (splittedStartDate[0].Length == 1 && splittedStartDate[1].Length == 1)
            {
                selectedStartDate = splittedStartDate[2] + "-" + "0" + splittedStartDate[0] + "-" + "0" + splittedStartDate[1];
            }
            else if (splittedStartDate[0].Length == 1)
            {
                selectedStartDate = splittedStartDate[2] + "-" + "0" + splittedStartDate[0] + "-" + splittedStartDate[1];
            }
            else if (splittedStartDate[1].Length == 1)
            {
                selectedStartDate = "0" + splittedStartDate[2] + "-" + splittedStartDate[0] + "-" + splittedStartDate[1];
            }
            else
            {
                selectedStartDate = splittedStartDate[2] + "-" + splittedStartDate[0] + "-" + splittedStartDate[1];
            }

            if (splittedEndDate[0].Length == 1 && splittedEndDate[1].Length == 1)
            {
                selectedEndDate = splittedEndDate[2] + "-" + "0" + splittedEndDate[0] + "-" + "0" + splittedEndDate[1];
            }
            else if (splittedEndDate[0].Length == 1)
            {
                selectedEndDate = splittedEndDate[2] + "-" + "0" + splittedEndDate[0] + "-" + splittedEndDate[1];
            }
            else if (splittedEndDate[1].Length == 1)
            {
                selectedEndDate = splittedEndDate[2] + "-" + splittedEndDate[0] + "-" + "0" + splittedEndDate[1];
            }
            else
            {
                selectedEndDate = splittedEndDate[2] + "-" + splittedEndDate[0] + "-" + splittedEndDate[1];
            }
            // end dirty hack
        }

        private void LedgerButton_Click_1(object sender, EventArgs e)
        {
            LedgerForm LF = new LedgerForm();
            LF.Show();
        }

        private void newEventButton_Click(object sender, EventArgs e)
        {
            NewEntryForm EF = new NewEntryForm();
            EF.f = this;
            EF.Show();
        }
    }
}