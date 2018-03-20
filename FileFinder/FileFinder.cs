﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileFinder
{
    public partial class FileFinder : Form
    {
        
        public FileFinder()
        {
            InitializeComponent();
        }

        //method for searching for the userString in files
        public bool SearchFiles(string fileName, string userString)
        {
            bool foundMatch = false;

            FileStream temp = new FileStream(fileName, FileMode.Open);
            StreamReader tempFile = new StreamReader(temp);

            while (!tempFile.EndOfStream)
            {
                string line = tempFile.ReadLine().ToLower();

                if (line.Contains(userString.ToLower()))
                {
                    foundMatch = true;
                    break;
                }
            }
            return foundMatch;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            ResultsBox.Items.Clear();
            string userExt = ExtInput.Text;
            string userPath = PathInput.Text;
            string userString = StringInput.Text;
            string[] files = Directory.GetFiles(userPath, "*" + userExt);

            try
            {
                //look at each file
                foreach (var file in files)
                {
                    bool results = SearchFiles(file.ToString(), userString);

                    //shows matching results + writes to log
                    if (results)
                    {
                        ResultsBox.Items.Add(file.ToString());
                        EnterLog(DateTime.Now , "Located file " + file);
                    }
                }
                //if no matching results are found, show message + add to log file
                if (ResultsBox.Items.Count == 0)
                {
                    ResultsBox.Items.Add("No files found with '" + userString + "' in " + userPath);
                    EnterLog(DateTime.Now , "Nothing found with " + userString);
                }
            }
            catch (Exception err)
            {
               EnterLog(DateTime.Now,"Error found" );
               
            }
        }

        //enters info into log file
        static void EnterLog ( DateTime logTime, string logLine)
        {
            string path = "Log.txt";
            FileStream newlf = new FileStream(path, FileMode.Append, FileAccess.Write);
            StreamWriter logIt = new StreamWriter(newlf);
            logIt.WriteLine(logTime + " " + logLine);
            logIt.Close();
        }

        //clears string and results boxes
        private void ClearButton_Click(object sender, EventArgs e)
        {
            ResultsBox.Items.Clear();
            StringInput.Clear();
        }
    }

}
    


//---------
