using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Subtitles_Extractor_GUI
{
    public partial class CfrmMain : Form
    {
        string[] _args;

        List<Process> _processes = new List<Process>();
        List<MediaFile> _mediaFiles = new List<MediaFile>();
        bool _running = false;

        public CfrmMain(string[] args)
        {
#if DEBUG
            //args = new string[]
            //{
            //    @"D:\Torrents\MediaPilot"
            //};
#endif

            _args = args;

            InitializeComponent();

            Prepare();
        }

        void Prepare()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, dgvMediaFiles, new object[] { true });

            if (!Tool.ShortcutExists())
            {
                createRemoveToolStripMenuItem.Text = "Create";
            }
            else
            {
                createRemoveToolStripMenuItem.Text = "Remove";
            }
        }

        void ToggleUI(bool value)
        {
            Invoke((Action)delegate
            {
                foreach (Control control in Controls)
                {
                    if (control != lblStatus)
                    {
                        control.Enabled = value;
                    }
                }
            });
        }

        void SoftToggleDGV(bool value)
        {
            Invoke((Action)delegate
            {
                dgvMediaFiles.Columns[1].ReadOnly = !value;
            });
        }

        void AddFile(string path)
        {
            try
            {
                FileInfo fi = new FileInfo(path);

                if (!Info.ValidFileExtensions.Contains(fi.Extension.ToLower()))
                {
                    return;
                }

                if (fi.Exists && fi.Length != 0 && !_mediaFiles.Any(x => x.PathLower == path.ToLower()))
                {
                    List<SubtitleStream> streams = FFMPEG.GetSubtitleStreams(path);
                    string[] comboBoxStreams = streams.Select(x => x.Stream + " (" + x.Language + ") " + x.SubtitleStreamType.ToString()).ToArray();
                    if (comboBoxStreams.Length == 0)
                    {
                        return;
                    }

                    _mediaFiles.Add(new MediaFile()
                    {
                        Path = path,
                        SubtitleStreams = streams
                    });

                    Invoke((Action)delegate
                    {
                        dgvMediaFiles.Rows.Add(new string[] { fi.Name, "", "Ready" });

                        ((DataGridViewLinkCell)dgvMediaFiles.Rows[dgvMediaFiles.RowCount - 1].Cells[0]).LinkBehavior = LinkBehavior.NeverUnderline;
                        ((DataGridViewComboBoxCell)dgvMediaFiles.Rows[dgvMediaFiles.RowCount - 1].Cells[1]).Items.AddRange(comboBoxStreams);
                        ((DataGridViewComboBoxCell)dgvMediaFiles.Rows[dgvMediaFiles.RowCount - 1].Cells[1]).Value = comboBoxStreams[0];
                        ((DataGridViewTextBoxCell)dgvMediaFiles.Rows[dgvMediaFiles.RowCount - 1].Cells[2]).Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        lblStatus.Text = dgvMediaFiles.RowCount + " File(s)";
                    });
                }
            }
            catch { }
        }

        void ResetStatus()
        {
            for (int i = 0; i < dgvMediaFiles.RowCount; i++)
            {
                dgvMediaFiles[2, i].Value = "Ready";
            }
        }

        async void Extract()
        {
            lblStatus.Text = "Extracted 0/" + _mediaFiles.Count + " Subtitle Files (0.00%)";

            double extractedCount = 0;

            List<Task> tasks = new List<Task>();
            SemaphoreSlim semaphore = new SemaphoreSlim(2); // Limit to 2 concurrent tasks

            for (int i = 0; i < dgvMediaFiles.RowCount; i++)
            {
                DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)dgvMediaFiles.Rows[i].Cells[1];
                int streamIndex = comboBoxCell.Items.IndexOf(comboBoxCell.Value);
                var stream = _mediaFiles[i].SubtitleStreams[streamIndex];

                DataGridViewTextBoxCell textBoxCell = (DataGridViewTextBoxCell)dgvMediaFiles.Rows[i].Cells[2];

                bool taskStarted = false;

                int id = i;

                Task extract = Task.Run(async () =>
                {
                    taskStarted = true;

                    await semaphore.WaitAsync(); // Acquire semaphore

                    if (!_running)
                    {
                        semaphore.Release();
                        return;
                    }

                    try
                    {
                        textBoxCell.Value = "Extracting";
                        stream.Extract();

                        if (!_running)
                        {
                            semaphore.Release();
                            return;
                        }

                        if (stream.SubtitleStreamType == SubtitleStreamType.hdmv_pgs_subtitle)
                        {
                            semaphore.Release(); // Release semaphore as this does not affect disk speed
                            textBoxCell.Value = "Performing OCR";
                            stream.PerformOCR();
                        }

                        extractedCount++;

                        Invoke((Action)delegate
                        {
                            lblStatus.Text = "Extracted " + extractedCount + "/" + _mediaFiles.Count + " Subtitle Files (" + (extractedCount / _mediaFiles.Count * 100).ToString("F2") + "%)";
                        });

                        textBoxCell.Value = "Success";
                    }
                    catch (Exception ex)
                    {
                        textBoxCell.Value = "Error: " + ex.Message;
                    }
                    finally
                    {
                        semaphore.Release(); // Release semaphore
                    }
                });

                tasks.Add(extract);

                while (_running && !taskStarted)
                {
                    await Task.Delay(10);
                }
            }

            await Task.WhenAll(tasks); // Wait for all tasks to complete

            _running = false;

            lblStatus.Text = _mediaFiles.Count + " Files";

            ResetStatus();

            btnExtract.Text = "Extract";
            btnExtract.Enabled = true;

            SoftToggleDGV(true);
        }

        void Stop(bool force = false)
        {
            if (!force)
            {
                if (!_running || MessageBox.Show(this, "Are you sure you want to cancel the operation?", "Subtitles Extractor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
            }

            btnExtract.Enabled = false;

            _running = false;

            for (int i = 0; i < Info.RunningProcesses.Count; i++)
            {
                try
                {
                    Info.RunningProcesses[i].Kill();
                    Info.RunningProcesses.RemoveAt(i--);
                }
                catch { }
            }
        }

        private void CfrmMain_Shown(object sender, EventArgs e)
        {
            ToggleUI(false);

            Task.Factory.StartNew(() =>
            {
                foreach (string path in _args)
                {
                    if (Directory.Exists(path))
                    {
                        var files = Tool.SearchDirectory(path, true, new string[] { "*.*" });
                        for (int i = 0; i < files.Count; i++)
                        {
                            AddFile(files[i]);
                        }
                    }
                    else
                    {
                        AddFile(path);
                    }
                }

                ToggleUI(true);
            }, TaskCreationOptions.LongRunning);
        }

        private void CfrmMain_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void CfrmMain_DragDrop(object sender, DragEventArgs e)
        {
            List<string> dropped = (e.Data.GetData(DataFormats.FileDrop) as string[]).ToList();

            bool subdirectoriesQuestionAsked = false;
            bool subdirectories = false;

            for (int i = 0; i < dropped.Count; i++)
            {
                if (Directory.Exists(dropped[i]) && Directory.GetDirectories(dropped[i]).Length > 0)
                {
                    if (!subdirectoriesQuestionAsked)
                    {
                        subdirectoriesQuestionAsked = true;

                        if (MessageBox.Show(this, "Include subdirectories?", "Subtitles Extractor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            subdirectories = true;
                        }
                    }
                }

                if (Directory.Exists(dropped[i]))
                {
                    dropped = dropped.Concat(Tool.SearchDirectory(dropped[i], subdirectories)).ToList();
                }
            }

            Task.Factory.StartNew(() =>
            {
                ToggleUI(false);

                for (int i = 0; i < dropped.Count; i++)
                {
                    AddFile(dropped[i]);
                }

                ToggleUI(true);
            }, TaskCreationOptions.LongRunning);
        }

        private void CfrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_running)
            {
                if (MessageBox.Show(this, "Cancel operation and terminate the application?", "Subtitles Extractor", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            Stop(true);
        }

        private void BtnExtract_Click(object sender, EventArgs e)
        {
            if (!_running)
            {
                _running = true;
                SoftToggleDGV(false);
                btnExtract.Text = "Stop";
                Extract();
            }
            else
            {
                Stop();
            }
        }

        private void DgvMediaFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (_running)
            {
                return;
            }

            if (e.KeyCode == Keys.Delete)
            {
                for (int i = dgvMediaFiles.SelectedRows.Count - 1; i >= 0; i--)
                {
                    int index = dgvMediaFiles.SelectedRows[i].Index;
                    dgvMediaFiles.Rows.RemoveAt(index);
                    _mediaFiles.RemoveAt(index);
                }
            }

            lblStatus.Text = dgvMediaFiles.RowCount + " File(s)";
        }

        private void DgvMediaFiles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_running)
            {
                return;
            }

            if (e.ColumnIndex == 1)
            {
                var editingControl = dgvMediaFiles.EditingControl as DataGridViewComboBoxEditingControl;
                if (editingControl != null)
                {
                    editingControl.DroppedDown = true;
                }
            }
        }

        private void DgvMediaFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                Process.Start(_mediaFiles[e.RowIndex].Path);
            }
        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                AddFile(dlgOpenFile.FileName);
            }

            dlgOpenFile.FileName = string.Empty;
        }

        private void MediaWithExistingSubtitlesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _mediaFiles.Count; i++)
            {
                FileInfo subtitles = new FileInfo(Path.ChangeExtension(_mediaFiles[i].Path, ".srt"));
                if (subtitles.Exists)
                {
                    dgvMediaFiles.Rows.RemoveAt(i);
                    _mediaFiles.RemoveAt(i--);
                }
            }
        }

        private void MediaWithExistingSubtitles10kbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _mediaFiles.Count; i++)
            {
                FileInfo subtitles = new FileInfo(Path.ChangeExtension(_mediaFiles[i].Path, ".srt"));
                if (subtitles.Exists && subtitles.Length > (10 * 1024))
                {
                    dgvMediaFiles.Rows.RemoveAt(i);
                    _mediaFiles.RemoveAt(i--);
                }
            }
        }

        private void Subtitles10kbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _mediaFiles.Count; i++)
            {
                try
                {
                    FileInfo subtitles = new FileInfo(Path.ChangeExtension(_mediaFiles[i].Path, ".srt"));
                    if (subtitles.Exists && subtitles.Length < (10 * 1024))
                    {
                        subtitles.Delete();
                    }
                }
                catch { }
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CreateRemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Tool.ShortcutExists())
            {
                Tool.CreateShortcut();
                createRemoveToolStripMenuItem.Text = "Remove";
            }
            else
            {
                Tool.RemoveShortcut();
                createRemoveToolStripMenuItem.Text = "Create";
            }
        }
    }
}
