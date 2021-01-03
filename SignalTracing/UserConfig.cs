using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
namespace SignalTracing
{
    public class UserConfigData
    {
        // ----- Public Properties -----

        public Int32 LocationX { get; set; } = 100;
        public Int32 LocationY { get; set; } = 100;

        public Int32 Width { get; set; } = 528;

        public Int32 Height { get; set; } = 551;

        public Int32 WindowState { get; set; } = 0;


        public String[] SearchDir { get; set; } = new String[14];


        public String FileName { get; set; } = "*.c;*.h";

        public String NotePad { get; set; } = @"C:\Program Files\Notepad++\notepad++.exe";


        public String[] ContainingText { get; set; } = new String[14];


        public String ResultsFilePath { get; set; } = "";
        public TreeView Search { get; set; } = new TreeView();
        public TreeView Interfaces { get; set; } = new TreeView();
        public Boolean IsServer { get; set; } = false;

        // public int LocationX1 { get => LocationX; set => LocationX = value; }
        // public int LocationX2 { get => LocationX; set => LocationX = value; }
    }
    class UserConfig
    {
        // ----- Variables -----

        private static String m_path = Path.Combine(Application.UserAppDataPath, "UserConfig.txt");
        private static UserConfigData m_configData = new UserConfigData();

        public static List<bool> IsExpland { get; private set; } = new List<bool>();
        // ----- Public Properties -----

        public static UserConfigData Data
        {
            get { return m_configData; }
        }


        // ----- Public Methods -----
        private static TreeView _getTreeView(TreeView treeview, String path)
        {
            string[] Lines = File.ReadAllLines(path);
            TreeNode current_treenode = new TreeNode();
            if (Lines.Length == 0) return new TreeView();
            for (int i = 0; i < Lines.Length; i++)
            {
                int level = 0;
                string content = Lines[i];
                while (content.Substring(0, 1) == "\t")
                {
                    level++;
                    content = content.Remove(0, 1);
                }
                switch (level)
                {
                    case 0:
                        if (content.LastIndexOf('1') == content.Length - 1)
                        {
                            if (content.LastIndexOf(':') != content.Length - 2)
                            {
                                treeview.Nodes.Add(content.Remove(content.LastIndexOf(')') + 1));
                                treeview.Nodes[treeview.GetNodeCount(false) - 1].Tag = content.Substring(content.LastIndexOf(')') + 1, content.Length - content.LastIndexOf(')') - 2);
                            }
                            else
                            {
                                treeview.Nodes.Add(content.Remove(content.LastIndexOf(':') + 1));
                            }
                            IsExpland.Add(true);
                        }
                        else if (content.LastIndexOf(':') == content.Length - 1)
                        {
                            treeview.Nodes.Add(content);
                            IsExpland.Add(false);
                        }
                        else if (content.LastIndexOf(')') != -1)
                        {
                            treeview.Nodes.Add(content.Substring(0, content.LastIndexOf(')') + 1));
                            IsExpland.Add(false);
                            treeview.Nodes[treeview.GetNodeCount(false) - 1].Tag = content.Substring(content.LastIndexOf(')') + 1, content.Length - content.LastIndexOf(')') - 1);
                        }
                        else
                        {
                            treeview.Nodes.Add(content);
                            IsExpland.Add(false);
                        }
                        treeview.Nodes[treeview.GetNodeCount(false) - 1].ForeColor = Color.Red;
                        treeview.Nodes[treeview.GetNodeCount(false) - 1].NodeFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
                        break;
                    case 1:
                        if (content.LastIndexOf('1') == content.Length - 1)
                        {
                            treeview.Nodes[treeview.GetNodeCount(false) - 1].Nodes.Add(content.Remove(content.Length - 1));
                            IsExpland.Add(true);
                        }
                        else
                        {
                            treeview.Nodes[treeview.GetNodeCount(false) - 1].Nodes.Add(content);
                            IsExpland.Add(false);
                        }
                        treeview.Nodes[treeview.GetNodeCount(false) - 1].
                                 Nodes[treeview.Nodes[treeview.GetNodeCount(false) - 1].
                                 GetNodeCount(false) - 1].
                                 ForeColor = Color.DarkGreen;
                        treeview.Nodes[treeview.GetNodeCount(false) - 1].
                                 Nodes[treeview.Nodes[treeview.GetNodeCount(false) - 1].
                                 GetNodeCount(false) - 1].NodeFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
                        break;
                    case 2:
                        if (content.LastIndexOf('1') == content.Length - 1)
                        {
                            treeview.Nodes[treeview.GetNodeCount(false) - 1].
                                 Nodes[treeview.Nodes[treeview.GetNodeCount(false) - 1].GetNodeCount(false) - 1].
                                 Nodes.Add(content.Remove(content.Length - 1));
                            IsExpland.Add(true);
                        }
                        else
                        {
                            treeview.Nodes[treeview.GetNodeCount(false) - 1].
                                   Nodes[treeview.Nodes[treeview.GetNodeCount(false) - 1].GetNodeCount(false) - 1].
                                   Nodes.Add(content);
                            IsExpland.Add(false);
                        }
                        break;
                    case 3:
                        treeview.Nodes[treeview.GetNodeCount(false) - 1].
                                 Nodes[treeview.Nodes[treeview.GetNodeCount(false) - 1].GetNodeCount(false) - 1].
                                 Nodes[treeview.Nodes[treeview.GetNodeCount(false) - 1].
                                 Nodes[treeview.Nodes[treeview.GetNodeCount(false) - 1].GetNodeCount(false) - 1].GetNodeCount(false) - 1].
                                 Nodes.Add(content);
                        treeview.Nodes[treeview.GetNodeCount(false) - 1].
                                  Nodes[treeview.Nodes[treeview.GetNodeCount(false) - 1].GetNodeCount(false) - 1].
                                  Nodes[treeview.Nodes[treeview.GetNodeCount(false) - 1].
                                  Nodes[treeview.Nodes[treeview.GetNodeCount(false) - 1].GetNodeCount(false) - 1].GetNodeCount(false) - 1].ForeColor = Color.DarkGreen;
                        break;
                }
            }
            return treeview;
        }
        public static Boolean Load()
        {
            Boolean success = false;

            try
            {
                // Zeilen aus der Datei "Config.txt" lesen:
                List<String> lines = new List<String>();
                FileStream fileStream = new FileStream(m_path, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fileStream);
                while (streamReader.Peek() >= 0)
                {
                    String line = streamReader.ReadLine();
                    lines.Add(line);
                }
                streamReader.Close();
                fileStream.Close();

                // Properties mit Werten belegen:
                PropertyInfo[] propertyInfos = m_configData.GetType().GetProperties();
                if (propertyInfos.Length == lines.Count)
                {
                    for (Int32 i = 0; i < propertyInfos.Length; i++)
                    {
                        PropertyInfo propertyInfo = propertyInfos[i];
                        String line = lines[i];
                        Object value = null;
                        switch (propertyInfo.PropertyType.Name)
                        {
                            case "String":
                                value = line;
                                break;
                            case "Int32":
                                value = Convert.ToInt32(line, System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            case "Boolean":
                                value = Convert.ToBoolean(line, System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            case "DateTime":
                                value = Convert.ToDateTime(line, System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            case "String[]":
                                value = line.Split('♥');
                                break;
                            case "TreeView":
                                string path = @line;
                                value = _getTreeView(new TreeView(), @path);
                                break;
                            default:
                                break;
                        }
                        propertyInfo.SetValue(m_configData, value, null);
                    }

                    success = true;
                }
            }
            catch (Exception)
            {
            }

            return success;
        }
        private static void writeTreeViewResults(string path, TreeView treeview)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                String delimeter = "\t";

                //bool bFirstHdr = true;
                /*
                foreach (ColumnHeader hdr in resultsList.Columns)
                {
                    if (bFirstHdr)
                    {
                        bFirstHdr = false;
                        sw.Write(hdr.Text + ":");
                    }
                    else
                    {
                        sw.Write(delimeter + hdr.Text + ":");
                    }
                }
                sw.WriteLine();
                */

                foreach (TreeNode item in treeview.Nodes)
                {
                    bool bFirstLvsi = true;
                    string lvsi = item.Text;
                    {
                        if (bFirstLvsi)
                        {
                            bFirstLvsi = false;
                            sw.Write(lvsi);
                        }
                        else
                        {
                            sw.Write(delimeter + lvsi);
                        }
                    }
                    sw.Write(item.Tag);
                    if (item.IsExpanded) sw.Write("1");
                    sw.WriteLine();
                    foreach (TreeNode child_item in item.Nodes)
                    {
                        lvsi = child_item.Text;
                        sw.Write(delimeter + lvsi);
                        if (child_item.IsExpanded) sw.Write("1");
                        sw.WriteLine();
                        foreach (TreeNode child_item_2 in child_item.Nodes)
                        {
                            lvsi = child_item_2.Text;
                            sw.Write(delimeter + delimeter + lvsi);
                            if (child_item_2.IsExpanded) sw.Write("1");
                            sw.WriteLine();
                            if (child_item_2.GetNodeCount(false) > 0)
                            {
                                foreach (TreeNode child_item_3 in child_item_2.Nodes)
                                {
                                    lvsi = child_item_3.Text;
                                    sw.Write(delimeter + delimeter + delimeter + lvsi);
                                    sw.WriteLine();
                                }
                            }
                        }
                    }
                }

                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public static Boolean Save()
        {
            Boolean success = false;

            try
            {
                if (File.Exists(m_path))
                {
                    // Schreibschutz aufheben:
                    FileAttributes attributes = File.GetAttributes(m_path);
                    if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        FileAttributes newAttributes = attributes ^ FileAttributes.ReadOnly;
                        File.SetAttributes(m_path, newAttributes);
                    }
                    // Datei löschen:
                    File.Delete(m_path);
                }
                FileStream fileStream = new FileStream(m_path, FileMode.Create, FileAccess.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);

                try
                {
                    int cnt_treenode = 0;
                    foreach (PropertyInfo propertyInfo in m_configData.GetType().GetProperties())
                    {
                        String line = "";
                        Object obj = propertyInfo.GetValue(m_configData, null);
                        switch (propertyInfo.PropertyType.Name)
                        {
                            case "String":
                                line = (String)obj;
                                break;
                            case "Int32":
                                Int32 i = (Int32)obj;
                                line = i.ToString(System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            case "Boolean":
                                Boolean b = (Boolean)obj;
                                line = b.ToString(System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            case "DateTime":
                                DateTime dt = (DateTime)obj;
                                line = dt.ToString(System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            case "String[]":
                                String[] strs = (String[])obj;
                                if (strs.Length == 0) break;
                                line = strs[0];
                                for (int j = 1; j < strs.Length; j++)
                                {
                                    line += "♥" + strs[j];
                                    if (j == 14) break;
                                }
                                break;
                            case "TreeView":
                                string name = "";
                                TreeView treeview = new TreeView();
                                if (cnt_treenode == 1) { name = "InterfaceResults.txt"; treeview = UserConfig.Data.Interfaces; }
                                else { name = "SearchResults.txt"; treeview = UserConfig.Data.Search; }
                                // if (!File.Exists(Path.Combine(Application.UserAppDataPath, name)))
                                // {
                                // File.Create(Path.Combine(Application.UserAppDataPath, name));
                                // }
                                line = Path.Combine(Application.UserAppDataPath, name);
                                writeTreeViewResults(@line, treeview);
                                cnt_treenode++;

                                break;
                            default:
                                break;
                        }
                        streamWriter.WriteLine(line);
                    }

                    success = true;
                }
                catch (Exception)
                {
                }

                streamWriter.Close();
                fileStream.Close();
            }
            catch (Exception)
            {
            }

            return success;
        }
    }
}
