using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Reflection;
using WMPLib;
using System.Drawing.Drawing2D;
using System.Linq;
 

namespace VS2019_ObjectAnnotationToolApp
{
    public enum ClassName
    {
        person = 0,
        bicycle = 1,
        car = 2,
        motorcycle = 3,
        airplane = 4,
        bus = 5,
        train = 6,
        truck = 7,
        boat = 8,
        trafficlight = 9,
        firehydrant = 10,
        stopsign = 11,
        parkingmeter = 12,
        bench = 13,
        bird = 14,
        cat = 15,
        dog = 16,
        horse = 17,
        sheep = 18,
        cow = 19,
        elephant = 20,
        bear = 21,
        zebra = 22,
        giraffe = 23,
        backpack = 24,
        umbrella = 25,
        handbag = 26,
        tie = 27,
        suitcase = 28,
        frisbee = 29,
        skis = 30,
        snowboard = 31,
        sportsball = 32,
        kite = 33,
        baseballbat = 34,
        baseballglove = 35,
        skateboard = 36,
        surfboard = 37,
        tennisracket = 38,
        bottle = 39,
        wineglass = 40,
        cup = 41,
        fork = 42,
        knife = 43,
        spoon = 44,
        bowl = 45,
        banana = 46,
        apple = 47,
        sandwich = 48,
        orange = 49,
        broccoli = 50,
        carrot = 51,
        hotdog = 52,
        pizza = 53,
        donut = 54,
        cake = 55,
        chair = 56,
        couch = 57,
        pottedplant = 58,
        bed = 59,
        diningtable = 60,
        toilet = 61,
        tv = 62,
        laptop = 63,
        mouse = 64,
        remote = 65,
        keyboard = 66,
        cellphone = 67,
        microwave = 68,
        oven = 69,
        toaster = 70,
        sink = 71,
        refrigerator = 72,
        book = 73,
        clock = 74,
        vase = 75,
        scissors = 76,
        teddybear = 77,
        hairdrier = 78,
        toothbrush = 79
    }

    public partial class Form1 : Form, IDisposable
    {

        private const float TIME_INCREMENT_SMALL = 0.1f;
        private const float TIME_INCREMENT_MEDIUM = 0.5f;
        private const float TIME_INCREMENT_LARGE = 1.0f;

        #region Fields and Properties
        private Image loadedImage;
        private float zoomFactor = 1.0f;
        private Point panOffset = new Point(0, 0);
        private Point mouseStart;
        private List<LabeledSelection> selections = new List<LabeledSelection>();
        private Type currentEnumType = typeof(ClassName);
        private Rectangle currentSelection;
        private bool isSelecting = false;
        private int selectedRectIndex = -1;
        private Point lastMousePos;
        private ToolStripStatusLabel statusLabel;
        private ToolTip selectionTooltip = new ToolTip();
        private Timer contextMenuTimer;

        // Add a new field to store the current enum type
        private Dictionary<object, int> itemUsageCount = new Dictionary<object, int>();

        private Stack<LabeledSelection> undoStack = new Stack<LabeledSelection>();
        private Stack<LabeledSelection> redoStack = new Stack<LabeledSelection>();


        public List<LabeledSelection> Selections
        {
            get => _selections;
            set
            {
                _selections = value;
                OutToLog($"Selections property set. New count: {_selections.Count}");
            }
        }

        private List<LabeledSelection> _selections = new List<LabeledSelection>();

        // Modify the PopulateComboBox method
        private void PopulateComboBox()
        {
            comboBox1.DrawMode = DrawMode.OwnerDrawVariable;
            comboBox1.DrawItem -= ComboBox1_DrawItem;
            comboBox1.DrawItem += ComboBox1_DrawItem;

            var currentSelection = comboBox1.SelectedItem;

            var items = Enum.GetValues(currentEnumType)
                .Cast<object>()
                .OrderByDescending(item => itemUsageCount.ContainsKey(item) ? itemUsageCount[item] : 0)
                .ThenBy(item => item.ToString())
                .ToList();

            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            comboBox1.DataSource = items;

            if (currentSelection != null && items.Contains(currentSelection))
            {
                comboBox1.SelectedItem = currentSelection;
            }
        }

        private void ComboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= comboBox1.Items.Count) return;

            e.DrawBackground();

            var item = comboBox1.Items[e.Index];
            if (item == null) return;

            var isCurrentlyUsed = selections.Any(s => s.Label != null && s.Label.Equals(item));

            using (var brush = new SolidBrush(e.ForeColor))
            {
                Font font = isCurrentlyUsed ? new Font(e.Font, FontStyle.Bold) : e.Font;

                if (font != null && !string.IsNullOrEmpty(item.ToString()) && e.Bounds.Width > 0 && e.Bounds.Height > 0)
                {
                    try
                    {
                        e.Graphics.DrawString(item.ToString(), font, brush, e.Bounds);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error drawing item: {ex.Message}");
                    }
                }

                if (isCurrentlyUsed)
                {
                    font.Dispose();
                }
            }

            e.DrawFocusRectangle();
        }

        // Call this method when a selection is made
        private void UpdateItemUsageCount(object selectedItem)
        {
            if (!itemUsageCount.ContainsKey(selectedItem))
            {
                itemUsageCount[selectedItem] = 0;
            }
            itemUsageCount[selectedItem]++;
            PopulateComboBox(); // Repopulate to reflect the new order
        }

        // Modify the LabeledSelection class
        public class LabeledSelection
        {
            public Rectangle Rect { get; set; }
            public object Label { get; set; } // Change to object to accommodate both enum types
            public Color Color { get; set; }
        }


        #endregion

        #region Initialization and Setup
        public Form1()
        {
            InitializeComponent();
            SetupControls();
            panel1.Paint += panel1_Paint;

            contextMenuTimer = new Timer();
            contextMenuTimer.Interval = 500; // 500 milliseconds
            contextMenuTimer.Tick += (s, e) =>
            {
                contextMenuTimer.Stop();
                pictureBox1.ContextMenuStrip = CreateContextMenu();
            };
            this.DoubleBuffered = false;
        }

        private void SetupControls()
        {
            SetupPictureBox();
            SetupStatusBar();
            SetupContextMenu();
            PopulateComboBox();
            SetupPanels();
        }

        private void SetupPictureBox()
        {
            OutToLog("SetupPictureBox called");
            if (pictureBox1 == null)
            {
                pictureBox1 = new PictureBox();
                this.Controls.Add(pictureBox1);
            }

            pictureBox1.Focus();
            pictureBox1.TabStop = true;
            pictureBox1.KeyDown += PictureBox1_KeyDown;

            // Create and set up the ContextMenuStrip
            if (pictureBox1.ContextMenuStrip == null)
            {
                pictureBox1.ContextMenuStrip = new ContextMenuStrip();
            }

            // Clear existing items and add new ones
            pictureBox1.ContextMenuStrip.Items.Clear();
            pictureBox1.ContextMenuStrip.Items.Add("Clear All Selections", null, (s, e) => ClearSelections());
            pictureBox1.ContextMenuStrip.Items.Add("Delete Selected", null, (s, e) => DeleteSelectedRectangle());
            pictureBox1.ContextMenuStrip.Items.Add("Reset View", null, (s, e) => ResetView());
            pictureBox1.ContextMenuStrip.Items.Add("Load Image", null, (s, e) => LoadImageFromDialog());

            // Add the Opening event handler
            pictureBox1.ContextMenuStrip.Opening += pictureBox1_ContextMenuStrip_Opening;
        }

        private void ShowKeyboardShortcuts()
        {
            string shortcuts = @"Keyboard Shortcuts:
                                Ctrl + +: Zoom in
                                Ctrl + -: Zoom out
                                Ctrl + 0: Reset view
                                Ctrl + S: Create Object Detection YAML dataset
                                Ctrl + Y: Generate dataset config
                                Ctrl + C: Clear all selections
                                Left Arrow: Move video backward
                                Right Arrow: Move video forward
                                Space: Capture video frame
                                Delete: Delete selected rectangle
                                P: Play/Pause video";

            MessageBox.Show(shortcuts, "Keyboard Shortcuts", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetupStatusBar()
        {
            statusLabel = new ToolStripStatusLabel();
            StatusStrip statusStrip = new StatusStrip();
            statusStrip.Items.Add(statusLabel);
            this.Controls.Add(statusStrip);
        }

        private void SetupContextMenu()
        {
            pictureBox1.ContextMenuStrip = CreateContextMenu();
        }

        private ContextMenuStrip CreateContextMenu()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Clear All Selections", null, (s, e) => ClearSelections());
            contextMenu.Items.Add("Delete Selected", null, (s, e) => DeleteSelectedRectangle());
            contextMenu.Items.Add("Reset View", null, (s, e) => ResetView());
            contextMenu.Items.Add("Load Image", null, (s, e) => LoadImageFromDialog());
            return contextMenu;
        }

        private void SetupPanels()
        {
            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, panel1, new object[] { true });
            panel1.AutoScroll = true;
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        private void SetInitialPath()
        {
            PathTextBox.Text = AppDomain.CurrentDomain.BaseDirectory;
            string[] drivesToCheck = { @"F:\", @"E:\", @"D:\", @"C:\" };
            foreach (string drive in drivesToCheck)
            {
                if (Directory.Exists(drive))
                {
                    PathTextBox.Text = drive;
                    break;
                }
            }
        }
        #endregion

        #region Event Handlers
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (loadedImage == null) return;

            e.Graphics.Clear(pictureBox1.BackColor);
            SetupGraphics(e.Graphics);
            DrawImage(e.Graphics);
            DrawSelections(e.Graphics);
            DrawCurrentSelection(e.Graphics);
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadImageFromDialog();
        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadImageFromDialog();
        }


        private bool justDeletedSelection = false;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ModifierKeys == Keys.Control)
            {
                StartNewSelection(e.Location);
            }
            else if (e.Button == MouseButtons.Left)
            {
                HandleLeftClick(e.Location);
            }
            else if (e.Button == MouseButtons.Right)
            {
                int index = FindSelectedRectangle(e.Location);
                if (index != -1)
                {
                    DeleteSelection(e.Location);
                    justDeletedSelection = true;
                }
                else
                {
                    justDeletedSelection = false;
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelecting)
            {
                UpdateCurrentSelection(e.Location);
            }
            else if (selectedRectIndex != -1)
            {
                MoveOrResizeSelection(e.Location);
            }
            else if (e.Button == MouseButtons.Left)
            {
                PanImage(e.Location);
            }
            else
            {
                HandleMouseHover(e.Location);
            }

            UpdateStatusBar(e.Location);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            FinishSelection();
        }

        private void pictureBox1_ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (justDeletedSelection)
            {
                e.Cancel = true;
                justDeletedSelection = false;
            }
        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (loadedImage == null) return;
            ZoomImage(e.Location, e.Delta);
        }

        private void PictureBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.Oemplus:
                    case Keys.Add:
                        ZoomImage(pictureBox1.PointToClient(Cursor.Position), 120);
                        break;
                    case Keys.OemMinus:
                    case Keys.Subtract:
                        ZoomImage(pictureBox1.PointToClient(Cursor.Position), -120);
                        break;
                    case Keys.D0:
                    case Keys.NumPad0:
                        ResetView();
                        break;
                    case Keys.S:
                        CreateYamlDataset();
                        break;
                    case Keys.Y:
                        GenerateDatasetConfig();
                        break;
                    case Keys.C:
                        ClearSelections();
                        break;
                    case Keys.P:
                        ToggleVideoPlayback();
                        break;
                    case Keys.Z:
                        UndoAction();
                        break;
                    case Keys.R:
                        RedoAction();
                        break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        AdjustVideoPosition(-1);
                        break;
                    case Keys.Right:
                        AdjustVideoPosition(1);
                        break;
                    case Keys.Space:
                        CaptureVideoFrame();
                        break;
                    case Keys.Delete:
                        DeleteSelectedRectangle();
                        break;
                    case Keys.P:
                        ToggleVideoPlayback();
                        break;
                }
            }
        }

        private void ToggleVideoPlayback()
        {
            if (axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPlaying)
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
            }
            else if (axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPaused)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        #endregion

        #region Drawing and Image Manipulation
        private void SetupGraphics(Graphics g)
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
        }

        private void DrawImage(Graphics g)
        {
            if (loadedImage != null)
            {
                int width = (int)(loadedImage.Width * zoomFactor);
                int height = (int)(loadedImage.Height * zoomFactor);
                g.DrawImage(loadedImage, panOffset.X, panOffset.Y, width, height);
            }
        }

        // Modify the DrawSelections method to draw a semi-transparent rectangle on top of the image
        private void DrawSelections(Graphics g)
        {
            foreach (var selection in selections)
            {
                Rectangle scaledSelection = ScaleRectangle(selection.Rect);
                using (Brush brush = new SolidBrush(Color.FromArgb(100, selection.Color)))
                {
                    g.FillRectangle(brush, scaledSelection);
                }
                using (Pen pen = new Pen(selection.Color, 2))
                {
                    g.DrawRectangle(pen, scaledSelection);
                }
                DrawLabel(g, selection.Label.ToString(), scaledSelection);
            }
        }

        private void DrawLabel(Graphics g, string label, Rectangle rect)
        {
            using (Font font = new Font("Arial", 15))
            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                g.DrawString(label, font, brush, rect.X + 2, rect.Y - 25);
            }
            using (Font font = new Font("Arial", 15))
            using (SolidBrush brush = new SolidBrush(Color.LightSalmon))
            {
                g.DrawString(label, font, brush, rect.X, rect.Y - 27);
            }
        }

        private void DrawCurrentSelection(Graphics g)
        {
            if (isSelecting)
            {
                Rectangle scaledSelection = ScaleRectangle(currentSelection);
                using (Pen currentSelectionPen = new Pen(Color.Blue, 2))
                {
                    g.DrawRectangle(currentSelectionPen, scaledSelection);
                }
            }
        }

        private void ZoomImage(Point mousePos, int delta)
        {
            float zoomInc = 0.1f;
            float oldZoom = zoomFactor;

            zoomFactor = delta > 0
                ? Math.Min(zoomFactor + zoomInc, 5.0f)
                : Math.Max(zoomFactor - zoomInc, 0.1f);

            if (oldZoom != zoomFactor)
            {
                AdjustPanOffset(mousePos, oldZoom);
                pictureBox1.Invalidate();
                UpdateStatusBar(mousePos);
            }
        }

        private void AdjustPanOffset(Point mousePos, float oldZoom)
        {
            panOffset.X = (int)((mousePos.X - panOffset.X) * (1 - zoomFactor / oldZoom)) + panOffset.X;
            panOffset.Y = (int)((mousePos.Y - panOffset.Y) * (1 - zoomFactor / oldZoom)) + panOffset.Y;
        }

        private void PanImage(Point location)
        {
            panOffset.X += location.X - mouseStart.X;
            panOffset.Y += location.Y - mouseStart.Y;
            mouseStart = location;
            pictureBox1.Invalidate();
        }
        #endregion

        #region Selection Handling
        private void StartNewSelection(Point location)
        {
            isSelecting = true;
            currentSelection = new Rectangle(
                (int)((location.X - panOffset.X) / zoomFactor),
                (int)((location.Y - panOffset.Y) / zoomFactor),
                0, 0
            );
            panel1.Cursor = Cursors.Cross;
        }

        private void HandleLeftClick(Point location)
        {
            selectedRectIndex = FindSelectedRectangle(location);
            if (selectedRectIndex != -1)
            {
                lastMousePos = location;
            }
            else
            {
                mouseStart = location;
            }
        }

        private void DeleteSelection(Point location)
        {
            int index = FindSelectedRectangle(location);
            if (index != -1)
            {
                var deletedItem = selections[index].Label;
                selections.RemoveAt(index);

                // Note: We're not decreasing the usage count here

                // Repopulate the ComboBox to reflect the updated order
                PopulateComboBox();

                pictureBox1.Invalidate();

                // Disable popup context menu after deleting a selection
                pictureBox1.ContextMenuStrip = null;
                contextMenuTimer.Start();
            }
        }

        private void UpdateCurrentSelection(Point location)
        {
            int x = (int)((location.X - panOffset.X) / zoomFactor);
            int y = (int)((location.Y - panOffset.Y) / zoomFactor);

            currentSelection.Width = x - currentSelection.X;
            currentSelection.Height = y - currentSelection.Y;

            pictureBox1.Invalidate();
        }

        private void MoveOrResizeSelection(Point location)
        {
            int dx = (int)((location.X - lastMousePos.X) / zoomFactor);
            int dy = (int)((location.Y - lastMousePos.Y) / zoomFactor);

            LabeledSelection selection = selections[selectedRectIndex];
            Rectangle newRect = selection.Rect;
            if (ModifierKeys == Keys.Shift)
            {
                newRect.Width += dx;
                newRect.Height += dy;
            }
            else
            {
                newRect.X += dx;
                newRect.Y += dy;
            }
            selection.Rect = newRect;
            lastMousePos = location;
            pictureBox1.Invalidate();
        }

        // Modify the FinishSelection method to update usage count
        private void FinishSelection()
        {
            if (isSelecting)
            {
                isSelecting = false;
                if (currentSelection.Width != 0 && currentSelection.Height != 0)
                {
                    currentSelection = NormalizeRectangle(currentSelection);
                    var selectedItem = comboBox1.SelectedItem;
                    if (selectedItem != null)
                    {
                        var newSelection = new LabeledSelection
                        {
                            Rect = currentSelection,
                            Label = selectedItem,
                            Color = GetRandomColor()
                        };
                        selections.Add(newSelection);
                        UpdateItemUsageCount(selectedItem);
                        pictureBox1.Invalidate();
                    }
                }
            }
            selectedRectIndex = -1;
            ResetSelectionCursor();
        }

        // Add methods for undoing and redoing actions
        private void UndoAction()
        {
            if (undoStack.Count > 0)
            {
                var selectionToUndo = undoStack.Pop();
                selections.Remove(selectionToUndo);
                redoStack.Push(selectionToUndo);
                pictureBox1.Invalidate();
            }
        }

        private void RedoAction()
        {
            if (redoStack.Count > 0)
            {
                var selectionToRedo = redoStack.Pop();
                selections.Add(selectionToRedo);
                undoStack.Push(selectionToRedo);
                pictureBox1.Invalidate();
            }
        }

        private void ClearSelections()
        {
            selections.Clear();
            // Note: We're not clearing or modifying itemUsageCount here
            pictureBox1.Invalidate();
        }

        private void DeleteSelectedRectangle()
        {
            if (selectedRectIndex != -1)
            {
                selections.RemoveAt(selectedRectIndex);

                // Update usage count
                if (itemUsageCount.ContainsKey(selections[selectedRectIndex].Label))
                {
                    itemUsageCount[selections[selectedRectIndex].Label]--;
                    if (itemUsageCount[selections[selectedRectIndex].Label] <= 0)
                    {
                        itemUsageCount.Remove(selections[selectedRectIndex].Label);
                    }
                }

                // Repopulate the ComboBox to reflect the updated order
                PopulateComboBox();

                pictureBox1.Invalidate();

                // Disable popup context menu after deleting a selection
                pictureBox1.ContextMenuStrip = null;
            }
        }
        #endregion

        #region Utility Methods
        private Color GetRandomColor()
        {
            Random random = new Random();
            return Color.FromArgb(random.Next(40, 200), random.Next(40, 200), random.Next(40, 200));
        }

        private Rectangle ScaleRectangle(Rectangle rect)
        {
            Rectangle scaled = new Rectangle(
                (int)(rect.X * zoomFactor) + panOffset.X,
                (int)(rect.Y * zoomFactor) + panOffset.Y,
                (int)(rect.Width * zoomFactor),
                (int)(rect.Height * zoomFactor)
            );
            return scaled;
        }

        private Rectangle NormalizeRectangle(Rectangle rect)
        {
            return new Rectangle(
                Math.Min(rect.X, rect.X + rect.Width),
                Math.Min(rect.Y, rect.Y + rect.Height),
                Math.Abs(rect.Width),
                Math.Abs(rect.Height)
            );
        }

        private int FindSelectedRectangle(Point mousePosition)
        {
            for (int i = 0; i < selections.Count; i++)
            {
                Rectangle scaledRect = ScaleRectangle(selections[i].Rect);
                if (scaledRect.Contains(mousePosition))
                {
                    return i;
                }
            }
            return -1;
        }

        private void UpdateStatusBar(Point location)
        {
            if (loadedImage != null)
            {
                int imageX = (int)((location.X - panOffset.X) / zoomFactor);
                int imageY = (int)((location.Y - panOffset.Y) / zoomFactor);
                statusLabel.Text = $"Zoom: {zoomFactor:P0} | Cursor: ({imageX}, {imageY})";
            }
            else
            {
                statusLabel.Text = "No image loaded";
            }
        }

        private void HandleMouseHover(Point location)
        {
            int index = FindSelectedRectangle(location);
            if (index != -1)
            {
                selectionTooltip.Show($"Selection {index + 1}\nRight-click to delete", pictureBox1, location, 2000);
            }
            else
            {
                selectionTooltip.Hide(pictureBox1);
            }
        }

        private void ResetSelectionCursor()
        {
            pictureBox1.Cursor = Cursors.Default;
            selectionTooltip.Hide(pictureBox1);
            pictureBox1.Invalidate();
        }

        private void ResetView()
        {
            OutToLog($"ResetView called. Selections before: {selections.Count}");
            pictureBox1.Image = null;
            zoomFactor = 1.0f;
            panOffset = new Point(0, 0);
            // Make sure ClearSelections() is not called here
            pictureBox1.Invalidate();
            UpdateStatusBar(Point.Empty);
            OutToLog($"ResetView finished. Selections after: {selections.Count}");
        }

        public static string GetFormattedDateTime()
        {
            return DateTime.Now.ToString("yyMMddHHmmss");
        }

        public void OutToLog(string output)
        {
            if (!string.IsNullOrWhiteSpace(OutPutRichTextBox1.Text))
            {
                OutPutRichTextBox1.AppendText("\r\n" + output);
            }
            else
            {
                OutPutRichTextBox1.AppendText(output);
            }
            OutPutRichTextBox1.ScrollToCaret();
        }

        public void ClearLog()
        {
            OutPutRichTextBox1.Clear();
            OutPutRichTextBox1.ScrollToCaret();
        }
        #endregion

        #region File Handling

        private string OpenDialogImageFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files(*.jpg;*.gif;*.png)|*.jpg;*.gif;*.png";
                openFileDialog.InitialDirectory = ".\\";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                return openFileDialog.ShowDialog() == DialogResult.OK ? openFileDialog.FileName : string.Empty;
            }
        }

        private string OpenDialogVideoFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "AVI/MP4/MOV Files(*.avi,*.mp4,*.mov)|*.avi;*.mp4;*.mov";
                openFileDialog.InitialDirectory = ".\\";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                return openFileDialog.ShowDialog() == DialogResult.OK ? openFileDialog.FileName : string.Empty;
            }
        }

        private void LoadImageFromDialog()
        {
            string filePath = OpenDialogImageFile();
            if (File.Exists(filePath))
            {
                LoadImage(filePath);
            }
        }

        private void LoadImage(string filePath)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                loadedImage?.Dispose();
                loadedImage = Image.FromFile(filePath);
                ResetView();
                panel1.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading the image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void SaveImage(string filePath)
        {
            try
            {
                loadedImage.Save(filePath, ImageFormat.Jpeg);
                OutToLog($"Image saved successfully: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                OutToLog($"Error saving image: {ex.Message}");
            }
        }
        #endregion

        #region YAML Dataset Generation
        private void CreateYamlDataset()
        {
            string datasetPath = Path.Combine(PathTextBox.Text, "datasets");
            string datasetImagePath = Path.Combine(datasetPath, "images", "train");
            string datasetLabelsPath = Path.Combine(datasetPath, "labels", "train");

            List<string> classNames = new List<string>(Enum.GetNames(typeof(ClassName)));

            if (loadedImage == null)
            {
                OutToLog("No image loaded!");
                return;
            }

            string fileName = $"Image{GetFormattedDateTime()}.jpg";
            string imagePath = Path.Combine(datasetImagePath, fileName);
            SaveImage(imagePath);

            string labelFileName = $"Image{GetFormattedDateTime()}.txt";
            string labelPath = Path.Combine(datasetLabelsPath, labelFileName);
            WriteLabelFile(labelPath, selections);

            selections.Clear();
            panel1.Invalidate();

            OutToLog("Object Detection YAML dataset created successfully!");
        }

        private void WriteLabelFile(string labelFile, List<LabeledSelection> labelInfos)
        {
            using (StreamWriter writer = new StreamWriter(labelFile))
            {
                foreach (var labelInfo in labelInfos)
                {
                    int labelValue = Convert.ToInt32(labelInfo.Label);
                    writer.WriteLine($"{labelValue} {labelInfo.Rect.X / (float)loadedImage.Width} {labelInfo.Rect.Y / (float)loadedImage.Height} {labelInfo.Rect.Width / (float)loadedImage.Width} {labelInfo.Rect.Height / (float)loadedImage.Height}");
                }
            }
        }

        private void GenerateDatasetConfig()
        {
            string rootPath = Path.Combine(PathTextBox.Text, "datasets");
            string trainPath = "images\\train";
            string valPath = "images\\train";
            string labelsPath = "labels\\train";

            var classNames = new Dictionary<string, int>();
            foreach (var className in Enum.GetValues(currentEnumType))
            {
                classNames.Add(className.ToString(), Convert.ToInt32(className));
            }

            var configGenerator = new DatasetYamlGenerator();
            configGenerator.GenerateConfig(PathTextBox.Text, rootPath, trainPath, valPath, labelsPath, classNames);

            OutToLog("Dataset configuration file generated successfully!");
        }

        #endregion

        #region Video Handling
        private void CaptureVideoFrame()
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();

            try
            {
                string uiModePrevious = axWindowsMediaPlayer1.uiMode;
                axWindowsMediaPlayer1.uiMode = "none";

                if (!string.IsNullOrEmpty(axWindowsMediaPlayer1.URL))
                {
                    using (Bitmap bitmap = new Bitmap(axWindowsMediaPlayer1.Width, axWindowsMediaPlayer1.Height))
                    {
                        using (Graphics g = Graphics.FromImage(bitmap))
                        {
                            using (Graphics gg = axWindowsMediaPlayer1.CreateGraphics())
                            {
                                this.BringToFront();
                                g.CopyFromScreen(axWindowsMediaPlayer1.PointToScreen(new Point()).X,
                                                 axWindowsMediaPlayer1.PointToScreen(new Point()).Y,
                                                 0, 0, new Size(axWindowsMediaPlayer1.Width, axWindowsMediaPlayer1.Height));

                                loadedImage = new Bitmap(bitmap);
                                ResetView();
                            }
                        }
                    }
                }

                axWindowsMediaPlayer1.uiMode = uiModePrevious;
            }
            catch (Exception ex)
            {
                OutToLog(ex.Message);
            }
        }

        private void AdjustVideoPosition(int direction)
        {
            float timeIncrement = GetTimeIncrement();

            if (axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPlaying ||
                axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPaused)
            {
                double newPosition = axWindowsMediaPlayer1.Ctlcontrols.currentPosition + (direction * timeIncrement);
                newPosition = Math.Max(0, Math.Min(newPosition, axWindowsMediaPlayer1.currentMedia.duration));
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = newPosition;

                if (axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPaused)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    axWindowsMediaPlayer1.Ctlcontrols.pause();
                }
            }
        }

        private float GetTimeIncrement()
        {
            if (radioButton1.Checked) return TIME_INCREMENT_SMALL;
            if (radioButton2.Checked) return TIME_INCREMENT_MEDIUM;
            if (radioButton3.Checked) return TIME_INCREMENT_LARGE;
            return TIME_INCREMENT_SMALL; // Default
        }
        #endregion

        #region Event Handlers
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            OutToLog($"panel1_Paint called. Selections count: {Selections.Count}");
            if (loadedImage == null) return;

            e.Graphics.Clear(panel1.BackColor);
            SetupGraphics(e.Graphics);
            DrawImage(e.Graphics);
            DrawSelections(e.Graphics);
            DrawCurrentSelection(e.Graphics);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ModifierKeys == Keys.Control)
            {
                StartNewSelection(e.Location);
            }
            else if (e.Button == MouseButtons.Left)
            {
                HandleLeftClick(e.Location);
            }
            else if (e.Button == MouseButtons.Right)
            {
                DeleteSelection(e.Location);
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelecting)
            {
                UpdateCurrentSelection(e.Location);
            }
            else if (selectedRectIndex != -1)
            {
                MoveOrResizeSelection(e.Location);
            }
            else if (e.Button == MouseButtons.Left)
            {
                PanImage(e.Location);
            }
            else
            {
                HandleMouseHover(e.Location);
            }

            UpdateStatusBar(e.Location);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            FinishSelection();
        }

        private void panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (loadedImage == null) return;
            ZoomImage(e.Location, e.Delta);
        }

        private void videoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePathLoad = OpenDialogVideoFile();
            if (File.Exists(filePathLoad))
            {
                axWindowsMediaPlayer1.URL = filePathLoad;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void snapShotButton_Click(object sender, EventArgs e)
        {
            CaptureVideoFrame();
        }

        private void processImageLabelDataButton_Click(object sender, EventArgs e)
        {
            CreateYamlDataset();
        }

        private void generateYMLButton_Click(object sender, EventArgs e)
        {
            GenerateDatasetConfig();
        }

        private void dataPathSelectionButton_Click(object sender, EventArgs e)
        {
            SelectDirectory();
        }

        private void clearSelectionButton_Click(object sender, EventArgs e)
        {
            ClearSelections();
        }

        private void minusTenSecBtn_Click(object sender, EventArgs e)
        {
            AdjustVideoPosition(-1);
        }

        private void plusTenSecBtn_Click(object sender, EventArgs e)
        {
            AdjustVideoPosition(1);
        }

        private void SelectDirectory()
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    PathTextBox.Text = folderDialog.SelectedPath;
                }
                else
                {
                    OutToLog("No directory selected.");
                }
            }
        }
        #endregion

        private void ImageSharpening()
        {
            if (loadedImage == null)
            {
                OutToLog("No image loaded to sharpen.");
                return;
            }

            try
            {
                // Log the number of selections before sharpening
                OutToLog($"Number of selections before sharpening: {selections.Count}");

                Bitmap original = new Bitmap(loadedImage);
                Bitmap sharpenedImage = new Bitmap(original.Width, original.Height);

                float sharpenFactor = 0.2f;

                float[,] kernel = new float[,]
                {
                    { -sharpenFactor, -sharpenFactor, -sharpenFactor },
                    { -sharpenFactor, 9 * sharpenFactor + 1, -sharpenFactor },
                    { -sharpenFactor, -sharpenFactor, -sharpenFactor }
                };

                // Apply convolution
                for (int x = 1; x < original.Width - 1; x++)
                {
                    for (int y = 1; y < original.Height - 1; y++)
                    {
                        float red = 0, green = 0, blue = 0;

                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                Color pixel = original.GetPixel(x + i, y + j);
                                red += pixel.R * kernel[i + 1, j + 1];
                                green += pixel.G * kernel[i + 1, j + 1];
                                blue += pixel.B * kernel[i + 1, j + 1];
                            }
                        }

                        red = Math.Min(Math.Max(red, 0), 255);
                        green = Math.Min(Math.Max(green, 0), 255);
                        blue = Math.Min(Math.Max(blue, 0), 255);

                        sharpenedImage.SetPixel(x, y, Color.FromArgb((int)red, (int)green, (int)blue));
                    }
                }

                // Store the current selections
                List<LabeledSelection> currentSelections = new List<LabeledSelection>(selections);

                // Log the number of stored selections
                OutToLog($"Number of stored selections: {currentSelections.Count}");

                // Replace the original image with the sharpened one
                loadedImage.Dispose();
                loadedImage = sharpenedImage;


                OutToLog($"Selections immediately before clearing: {selections.Count}");
                selections.Clear();
                OutToLog($"Selections immediately after clearing: {selections.Count}");

                // Add back the stored selections
                foreach (var selection in currentSelections)
                {
                    selections.Add(new LabeledSelection
                    {
                        Rect = selection.Rect,
                        Label = selection.Label,
                        Color = selection.Color
                    });
                    OutToLog($"Added selection: {selection.Label}");
                }

                OutToLog($"Selections immediately after adding: {selections.Count}");
                // Log the number of selections after adding them back
                OutToLog($"Number of selections after adding back: {selections.Count}");

                // Refresh the display
                ResetView();
                panel1.Invalidate();
                panel1.Update(); // Force immediate redraw

                OutToLog("Image sharpened successfully. Selections should be preserved.");
                OutToLog($"Number of selections after sharpening: {selections.Count}");
            }
            catch (Exception ex)
            {
                OutToLog($"Error sharpening image: {ex.Message}");
            }
        }

        private void ImageBlurring()
        {
            if (loadedImage == null)
            {
                OutToLog("No image loaded to blur.");
                return;
            }

            try
            {
                // Log the number of selections before blurring
                OutToLog($"Number of selections before blurring: {selections.Count}");

                Bitmap original = new Bitmap(loadedImage);
                Bitmap blurredImage = new Bitmap(original.Width, original.Height);

                float blurFactor = 0.11111111f;

                float[,] kernel = new float[,]
                {
                    { blurFactor, blurFactor, blurFactor },
                    { blurFactor, blurFactor, blurFactor },
                    { blurFactor, blurFactor, blurFactor }
                };

                // Apply convolution
                for (int x = 1; x < original.Width - 1; x++)
                {
                    for (int y = 1; y < original.Height - 1; y++)
                    {
                        float red = 0, green = 0, blue = 0;

                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                Color pixel = original.GetPixel(x + i, y + j);
                                red += pixel.R * kernel[i + 1, j + 1];
                                green += pixel.G * kernel[i + 1, j + 1];
                                blue += pixel.B * kernel[i + 1, j + 1];
                            }
                        }

                        red = Math.Min(Math.Max(red, 0), 255);
                        green = Math.Min(Math.Max(green, 0), 255);
                        blue = Math.Min(Math.Max(blue, 0), 255);

                        blurredImage.SetPixel(x, y, Color.FromArgb((int)red, (int)green, (int)blue));
                    }
                }

                // Store the current selections
                List<LabeledSelection> currentSelections = new List<LabeledSelection>(selections);

                // Log the number of stored selections
                OutToLog($"Number of stored selections: {currentSelections.Count}");

                // Replace the original image with the blurred one
                loadedImage.Dispose();
                loadedImage = blurredImage;


                OutToLog($"Selections immediately before clearing: {selections.Count}");
                selections.Clear();
                OutToLog($"Selections immediately after clearing: {selections.Count}");

                // Add back the stored selections
                foreach (var selection in currentSelections)
                {
                    selections.Add(new LabeledSelection
                    {
                        Rect = selection.Rect,
                        Label = selection.Label,
                        Color = selection.Color
                    });
                    OutToLog($"Added selection: {selection.Label}");
                }

                OutToLog($"Selections immediately after adding: {selections.Count}");
                // Log the number of selections after adding them back
                OutToLog($"Number of selections after adding back: {selections.Count}");

                // Refresh the display
                ResetView();
                panel1.Invalidate();
                panel1.Update(); // Force immediate redraw

                OutToLog("Image blurred successfully. Selections should be preserved.");
                OutToLog($"Number of selections after blurring: {selections.Count}");
            }
            catch (Exception ex)
            {
                OutToLog($"Error blurred image: {ex.Message}");
            }
        }

        public void ContrastStretch()
        {
            if (loadedImage == null)
            {
                OutToLog("No image loaded for contrast stretching.");
                return;
            }

            try
            {
                // Log the number of selections before contrast stretching
                OutToLog($"Number of selections before contrast stretching: {selections.Count}");

                Bitmap original = new Bitmap(loadedImage);
                Bitmap stretchedImage = new Bitmap(original.Width, original.Height);

                // Find the minimum and maximum pixel values
                int minR = 255, minG = 255, minB = 255;
                int maxR = 0, maxG = 0, maxB = 0;

                for (int x = 0; x < original.Width; x++)
                {
                    for (int y = 0; y < original.Height; y++)
                    {
                        Color pixel = original.GetPixel(x, y);
                        minR = Math.Min(minR, pixel.R);
                        minG = Math.Min(minG, pixel.G);
                        minB = Math.Min(minB, pixel.B);
                        maxR = Math.Max(maxR, pixel.R);
                        maxG = Math.Max(maxG, pixel.G);
                        maxB = Math.Max(maxB, pixel.B);
                    }
                }

                // Perform contrast stretching
                for (int x = 0; x < original.Width; x++)
                {
                    for (int y = 0; y < original.Height; y++)
                    {
                        Color pixel = original.GetPixel(x, y);
                        int newR = StretchChannel(pixel.R, minR, maxR);
                        int newG = StretchChannel(pixel.G, minG, maxG);
                        int newB = StretchChannel(pixel.B, minB, maxB);

                        stretchedImage.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                    }
                }

                // Store the current selections
                List<LabeledSelection> currentSelections = new List<LabeledSelection>(selections);

                // Log the number of stored selections
                OutToLog($"Number of stored selections: {currentSelections.Count}");

                // Replace the original image with the stretched one
                loadedImage.Dispose();
                loadedImage = stretchedImage;

                OutToLog($"Selections immediately before clearing: {selections.Count}");
                selections.Clear();
                OutToLog($"Selections immediately after clearing: {selections.Count}");

                // Add back the stored selections
                foreach (var selection in currentSelections)
                {
                    selections.Add(new LabeledSelection
                    {
                        Rect = selection.Rect,
                        Label = selection.Label,
                        Color = selection.Color
                    });
                    OutToLog($"Added selection: {selection.Label}");
                }

                OutToLog($"Selections immediately after adding: {selections.Count}");
                // Log the number of selections after adding them back
                OutToLog($"Number of selections after adding back: {selections.Count}");

                // Refresh the display
                ResetView();
                panel1.Invalidate();
                panel1.Update(); // Force immediate redraw

                OutToLog("Contrast stretching applied successfully. Selections should be preserved.");
                OutToLog($"Number of selections after contrast stretching: {selections.Count}");
            }
            catch (Exception ex)
            {
                OutToLog($"Error during contrast stretching: {ex.Message}");
            }
        }

        private int StretchChannel(int value, int min, int max)
        {
            if (max == min) return value; // Avoid division by zero
            return (int)((value - min) * 255.0 / (max - min));
        }

        public void AdjustBrightness(float factor)
        {
            if (loadedImage == null)
            {
                OutToLog("No image loaded for brightness adjustment.");
                return;
            }

            try
            {
                // Log the number of selections before brightness adjustment
                OutToLog($"Number of selections before brightness adjustment: {selections.Count}");

                Bitmap original = new Bitmap(loadedImage);
                Bitmap adjustedImage = new Bitmap(original.Width, original.Height);

                // Create color matrix for brightness adjustment
                ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                {
                    new float[] {factor, 0, 0, 0, 0},
                    new float[] {0, factor, 0, 0, 0},
                    new float[] {0, 0, factor, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });

                // Create image attributes and set the color matrix
                using (ImageAttributes attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(colorMatrix);

                    // Draw the adjusted image
                    using (Graphics g = Graphics.FromImage(adjustedImage))
                    {
                        g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                            0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
                    }
                }

                // Store the current selections
                List<LabeledSelection> currentSelections = new List<LabeledSelection>(selections);

                // Log the number of stored selections
                OutToLog($"Number of stored selections: {currentSelections.Count}");

                // Replace the original image with the adjusted one
                loadedImage.Dispose();
                loadedImage = adjustedImage;

                OutToLog($"Selections immediately before clearing: {selections.Count}");
                selections.Clear();
                OutToLog($"Selections immediately after clearing: {selections.Count}");

                // Add back the stored selections
                foreach (var selection in currentSelections)
                {
                    selections.Add(new LabeledSelection
                    {
                        Rect = selection.Rect,
                        Label = selection.Label,
                        Color = selection.Color
                    });
                    OutToLog($"Added selection: {selection.Label}");
                }

                OutToLog($"Selections immediately after adding: {selections.Count}");
                // Log the number of selections after adding them back
                OutToLog($"Number of selections after adding back: {selections.Count}");

                // Refresh the display
                ResetView();
                panel1.Invalidate();
                panel1.Update(); // Force immediate redraw

                OutToLog($"Brightness adjustment (factor: {factor}) applied successfully. Selections should be preserved.");
                OutToLog($"Number of selections after brightness adjustment: {selections.Count}");
            }
            catch (Exception ex)
            {
                OutToLog($"Error during brightness adjustment: {ex.Message}");
            }
        }

        private void ImageMedianFilter()
        {
            if (loadedImage == null)
            {
                OutToLog("No image loaded for median filtering.");
                return;
            }

            try
            {
                // Log the number of selections before median filtering
                OutToLog($"Number of selections before median filtering: {selections.Count}");

                Bitmap original = new Bitmap(loadedImage);
                Bitmap filteredImage = new Bitmap(original.Width, original.Height);

                // Define the filter kernel size (adjust as needed)
                int kernelSize = 3;

                // Apply median filtering
                for (int x = kernelSize / 2; x < original.Width - kernelSize / 2; x++)
                {
                    for (int y = kernelSize / 2; y < original.Height - kernelSize / 2; y++)
                    {
                        List<int> redValues = new List<int>();
                        List<int> greenValues = new List<int>();
                        List<int> blueValues = new List<int>();

                        for (int i = -kernelSize / 2; i <= kernelSize / 2; i++)
                        {
                            for (int j = -kernelSize / 2; j <= kernelSize / 2; j++)
                            {
                                Color pixel = original.GetPixel(x + i, y + j);
                                redValues.Add(pixel.R);
                                greenValues.Add(pixel.G);
                                blueValues.Add(pixel.B);
                            }
                        }

                        redValues.Sort();
                        greenValues.Sort();
                        blueValues.Sort();

                        int medianIndex = kernelSize * kernelSize / 2;
                        int medianRed = redValues[medianIndex];
                        int medianGreen = greenValues[medianIndex];
                        int medianBlue = blueValues[medianIndex];


                        filteredImage.SetPixel(x, y, Color.FromArgb(medianRed, medianGreen, medianBlue));
                    }
                }

                // Store the current selections
                List<LabeledSelection> currentSelections = new List<LabeledSelection>(selections);

                // Log the number of stored selections
                OutToLog($"Number of stored selections: {currentSelections.Count}");

                // Replace the original image with the filtered one
                loadedImage.Dispose();
                loadedImage = filteredImage;

                // Refresh the display
                ResetView();
                panel1.Invalidate();
                panel1.Update(); // Force immediate redraw
            }
            catch (Exception ex)
            {
                OutToLog($"Error during median filtering: {ex.Message}");
            }
        }

        private void ShowHelpPopup()
        {
            string helpText = @"Object Detection Annotation Tool Help

            The importance of providing high-quality data for computer vision models is emphasized. The focus is on annotating images for object detection models, specifically for a chess piece detection example. The seven key techniques covered are:
 
            1. Create Tight Bounding Boxes: Ensure bounding 
               boxes closely fit the objects of interest 
               without cutting off any part, aiming for 
               precision in model learning.
            2. Label All Objects of Interest: It is essential
               to label all instances of the objects you want 
               the model to recognize to avoid confusion and 
               ensure comprehensive learning.
            3. Label Occluded Objects: When objects block the
               view of others, label them as if they were 
               visible in their entirety, allowing the model
               to understand their presence.
            4. Label the Entirety of an Object: Avoid cutting
               off parts of objects when labeling; include 
               the complete object in the bounding box.
            5. Create Clear Labeling Instructions: Document 
               and share clear labeling instructions to 
               ensure reproducibility and consistency, 
               especially when working with a team or 
               outsourcing labeling services.";

            MessageBox.Show(helpText, "Annotation Tool Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OutToLog(helpText);
        }

        private void ShowHelpPopup2()
        {
            string helpText = @"Object Detection Annotation Tool Help
            Image Annotation:
            1. Load an image: File > Image or right - click > Load 
               Image
            2. Select a class from the dropdown menu
            3. Hold Ctrl and click-drag to draw a bounding box
            4. Adjust boxes: Click-drag to move, Shift-drag to 
               resize
            5. Right-click on a box to delete it
            6. Click 'Process Image Label Data' to save annotations

            Video Annotation:
            1. Load a video: File > Video
            2. Use player controls or +/- buttons to navigate
            3. Click 'Snap Shot' to capture current frame
            4. Annotate the captured frame as above
            5. Repeat for desired frames

            General Controls:
            - Mouse wheel or Ctrl +/- to zoom in/out
            - Click-drag to pan the image
            - 'Clear Selection' removes all annotations
            - 'Generate YML' creates dataset config file

            Keyboard Shortcuts:
            Ctrl + +/-: Zoom in/out
            Ctrl + 0: Reset view
            Ctrl + S: Create Object Detection (YAML) dataset
            Ctrl + Y: Generate dataset config
            Ctrl + C: Clear all selections
            Left/Right Arrow: Navigate video
            Space: Capture video frame
            Delete: Remove selected box
            P: Play/Pause video

            Tips:
            - Use 'Switch Enum' to change class sets
            - Status bar shows zoom level and cursor position
            - Frequently used classes appear at the top of the 
              dropdown";

            MessageBox.Show(helpText, "Annotation Tool Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OutToLog(helpText);
        }

        private void ShowHelpPopup3()
        {
            string helpText = @"The user can generate a 
            valid Object Detection (YAML) Training set using the given 
            code by following these steps:
            Load an image or video file using either 
            'File > Image' or right-clicking and 
            selecting 'Load Image'. Select a class 
            from the dropdown menu based on their preference.
            Hold down the Ctrl key while clicking and 
            dragging to draw a  bounding box around 
            the desired object in the image. 
            If needed, adjust the position and size 
            of the boxes by either clicking and 
            dragging without holding the Shift key or 
            holding the Shift key and clicking 
            and dragging to resize the boxes.
            To delete an existing bounding box, 
            right-click on it and select 'Delete Selected'.

            Once satisfied with the annotations, 
            click 'Process Image Label Data' to save the 
            annotations as a Object dataset.

            The user can perform various image 
            processing techniques using the provided buttons:

            Sharpening: Enhances the contrast of 
            the image by increasing its sharpness.

            Blurring: Decreases the details in 
            the image and creates a softer appearance.

            Contrast Stretching: Adjusts the contrast 
            levels of the image, making it 
            appear brighter or darker based on user preferences.

            Brightening/Darkening: Increases/decreases 
            the brightness of the image 
            to improve visibility.

            Keyboard shortcuts are available for ease of use:

            Ctrl +/-: Zoom in/out
            Ctrl 0: Reset view
            Ctrl S: Create Object dataset
            Ctrl Y: Generate dataset config
            Ctrl C: Clear all selections

            Left/Right Arrow: Navigate video
            Space: Capture video frame
            Delete: Remove selected box
            P: Play/Pause video

            The user can switch between different 
            sets of classes using the 'Switch Enum' button.
            Additionally, the status bar displays the current 
            zoom level and cursor position in the image.' +
            For further assistance or information on how 
            to use the tool effectively, the user can 
            refer to the help popup (Help > Show Help) 
            for detailed instructions and guidelines.

            The code also provides various event handlers 
            to handle different actions such as 
            loading images, capturing video frames, 
            adjusting brightness, and generating Object Detection
            YAML dataset. The code includes a Dispose method 
            to properly release resources when the form is closed.

            As part of this implementation:

            The user should provide input file path 
            (e.g., image or video file) in PathTextBox.

            The 'Generate YML Confg' button should call 
            the GenerateDatasetConfig function, passing 
            the appropriate parameters such as root 
            directory and dataset configuration file path.
            This will generate a YAML file that contains 
            metadata for training datasets.";

            MessageBox.Show(helpText, "Annotation Tool Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OutToLog(helpText);
        }

        public new void Dispose()
        {
            loadedImage?.Dispose();
            base.Dispose();
        }

        private void showAnnotationHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelpPopup();
        }

        private void showShortcutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelpPopup2();
        }
        private void annotationProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelpPopup3();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoAction();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RedoAction();
        }

        private void sharpenBtn_Click(object sender, EventArgs e)
        {
            OutToLog("Sharpening button clicked");
            ImageSharpening();
            OutToLog($"After ImageSharpening, selections count: {Selections.Count}");
        }

        private void blurrBtn_Click(object sender, EventArgs e)
        {
            OutToLog("Blur button clicked");
            ImageBlurring();
            OutToLog($"After ImageBlurring, selections count: {Selections.Count}");
        }

        private void contrastBtn_Click(object sender, EventArgs e)
        {
            OutToLog("Contrast stretch button clicked");
            ContrastStretch();
            OutToLog($"After ContrastStretch, selections count: {selections.Count}");
        }

        private void brightenBtn_Click(object sender, EventArgs e)
        {
            OutToLog("Brighten button clicked");
            AdjustBrightness(1.2f); // Increase brightness by 20%
            OutToLog($"After brightening, selections count: {selections.Count}");
        }

        private void darkenBtn_Click(object sender, EventArgs e)
        {
            OutToLog("Darken button clicked");
            AdjustBrightness(0.8f); // Decrease brightness by 20%
            OutToLog($"After darkening, selections count: {selections.Count}");
        }

        private void medianBtn_Click(object sender, EventArgs e)
        {
            OutToLog("Median Filter button clicked");
            ImageMedianFilter();
            OutToLog($"After Applying Median Filter, selections count: {selections.Count}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.dataPath.Length == 0)
            {
                SetInitialPath();
            }
            else
            {
                PathTextBox.Text = Properties.Settings.Default.dataPath;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.dataPath = PathTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void resetViewBtn_Click(object sender, EventArgs e)
        {
            // Refresh the display
            ResetView();
            panel1.Invalidate();
            panel1.Update(); // Force immediate redraw
        }
    }
}