using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lucene.Net.Documents.Field;

namespace SignalTracing
{
    class Lucene_Indexer
    {

        private static object key = new object();
        private static IndexWriter writer;
        private int _offset, _cnt_thread;
        private Boolean m_stop = false;
        //private static readonly StandardAnalyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
        private static readonly CaseInsensitiveWhitespaceAnalyzer analyzer = new CaseInsensitiveWhitespaceAnalyzer();

        public delegate void IndexInforEventHandler(FoundInfoEventArgs e);
        public event IndexInforEventHandler IndexInfo;

        public delegate void ThreadEndedEventHandler(ThreadEndedEventArgs e);
        public event ThreadEndedEventHandler ThreadEnded;

        public Boolean IsDone { get; set; } = false;
        public string IndexDir { get; }
        public FileInfo[] Files { get; }
        private Lucene_Indexer()
        { }
        public Lucene_Indexer(int offset, int cnt_thread, string indexdir, FileInfo[] files)
        {
            _offset = offset;
            _cnt_thread = cnt_thread;
            IndexDir = indexdir;
            Files = files;
        }

        private static Lucene_Indexer instance;
        public static Lucene_Indexer Instance
        {
            get
            {
                lock (key)
                {
                    if (instance == null)
                    {
                        instance = new Lucene_Indexer();
                    }
                }
                return instance;
            }
        }

        #region Method
        public FileInfo[] ListFile(String dataDir, String tagFile)
        {
            if (!IsAllowAccessPermission(dataDir))
            {
                MessageBox.Show("Accessing permission is deny", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }


            //Get all file in Directory
            List<string> list = DirectoryAlternative.EnumerateFiles(dataDir, "*.*", SearchOption.AllDirectories).ToList();
            var fileInfos = list.ToArray().Select(f => new FileInfo(f));
            FileInfo[] files = fileInfos.ToArray();
            if (tagFile != "*.*")
            {
                tagFile = tagFile.Replace("*", "");
                string[] groupTagfile = tagFile.Split(';');
                files = files.Where(f => groupTagfile.Contains(f.Extension)).ToArray();
            }
            //Save dir and tagFile
            return files;

        }

        public void CreateIndex(String indexDir, FileInfo[] files)
        {
            //Set up directory for writer
            FSDirectory indexDirectory = FSDirectory.Open(indexDir);
            if (writer == null)
                writer = new IndexWriter(indexDirectory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);
            foreach (FileInfo file in Files)
            {
                if (file.Exists)
                {
                    IndexFile(file);
                    IndexInfo(new FoundInfoEventArgs());
                }
            }
            writer.Optimize();
            writer.Dispose();
            writer = null;
        }
        public void CreateIndex_Thread()
        {
            //Set up directory for writer
            FSDirectory indexDirectory = FSDirectory.Open(IndexDir);
            IndexWriter writer_thread = new IndexWriter(indexDirectory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);
            for (int i = _offset; i < Files.Length; i += _cnt_thread)
            {
                if (Files[i].Exists)
                {
                    IndexFileThread(Files[i], writer_thread);
                    lock (this)
                    {
                        IndexInfo(new FoundInfoEventArgs());
                    }
                }
                if (m_stop)
                {
                    break;
                }
            }
            writer_thread.Optimize();
            writer_thread.Dispose();
            IsDone = true;
            // Raise an event:
            if (ThreadEnded != null && IsDone)
            {
                ThreadEnded(new ThreadEndedEventArgs(true, ""));
            }
        }
        public void StopIndexing()
        {
            m_stop = true;
        }
        private void IndexFileThread(FileInfo file, IndexWriter idwr)
        {
            Document doc = new Document();
            Field contentField = new Field(Lucene_Constants.CONTENTS, new StreamReader(file.FullName));
            Field fileNameField = new Field(Lucene_Constants.FILE_NAME, file.FullName.ToLower(), Field.Store.YES, Field.Index.NOT_ANALYZED);

            doc.Add(contentField);
            doc.Add(fileNameField);
            //  doc.Add(filePathField);

            idwr.AddDocument(doc);
        }
        private void IndexFile(FileInfo file)
        {
            Document doc = new Document();
            Field contentField = new Field(Lucene_Constants.CONTENTS, new StreamReader(file.FullName));
            Field fileNameField = new Field(Lucene_Constants.FILE_NAME, file.FullName.ToLower(), Field.Store.YES, Field.Index.NOT_ANALYZED);

            doc.Add(contentField);
            doc.Add(fileNameField);
            //  doc.Add(filePathField);

            writer.AddDocument(doc);
        }

        private bool IsAllowAccessPermission(String path)
        {
            var accessControlList = System.IO.Directory.GetAccessControl(path);
            var accessRules = accessControlList.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
            bool writeAllow = false;
            bool writeDeny = false;

            foreach (FileSystemAccessRule rule in accessRules)
            {
                if ((FileSystemRights.Write & rule.FileSystemRights) != FileSystemRights.Write)
                    continue;

                if (rule.AccessControlType == AccessControlType.Allow)
                    writeAllow = true;
                else if (rule.AccessControlType == AccessControlType.Deny)
                    writeDeny = false;
            }
            bool isAllow = writeAllow && !writeDeny;
            return isAllow;
        }

        public void MergeIndexes(string parentIndexDir)
        {
            //Setup ParentIndexDir
            FSDirectory indexDirectory = FSDirectory.Open(parentIndexDir);
            // Check writer still exist
            if (writer != null)
            {
                writer.Dispose();
                writer = null;
            }
            writer = new IndexWriter(indexDirectory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);
            // Get SubIndexDir
            List<string> lstSubIndexDir = System.IO.Directory.GetDirectories(parentIndexDir).ToList();
            int count = 0;
            IndexReader[] indexes = new IndexReader[lstSubIndexDir.Count()];
            foreach (var index in indexes)
            {
                indexes[count] = IndexReader.Open(FSDirectory.Open(lstSubIndexDir[count]), true);
                count++;
            }
            //Merge Index
            writer.AddIndexes(indexes);
            //Close SubIndex and delete SubIndexDir
            count = 0;
            foreach (var index in indexes)
            {
                index.Dispose();
                System.IO.Directory.Delete(lstSubIndexDir[count], true);
                count++;
            }
            writer.Dispose();
            writer = null;
        }

        public void UpdateDocuments(string Indexdir, List<string> filesToDelete, List<string> filesToUpdate)
        {
            //Setup Indexdir
            //Lucene.Net.Store.Directory indexDirectory = new MMapDirectory(new DirectoryInfo(Indexdir));
            FSDirectory indexDirectory = FSDirectory.Open(Indexdir);
            // Check writer still exist
            if (writer != null)
            {
                writer.Dispose();
                writer = null;
            }
            writer = new IndexWriter(indexDirectory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED);
            foreach (string filetoDelete in filesToDelete)
            {
                writer.DeleteDocuments(new TermQuery(new Term(Lucene_Constants.FILE_NAME, filetoDelete.ToLower())));
            }
            foreach (string filetoUpdate in filesToUpdate)
            {
                writer.DeleteDocuments(new TermQuery(new Term(Lucene_Constants.FILE_NAME, filetoUpdate.ToLower())));
                IndexFile(new FileInfo(filetoUpdate));
            }
            writer.Dispose();
            writer = null;
        }

        #endregion

    }
}
