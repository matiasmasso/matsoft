Public Partial Class Frm_Balance
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ExcelToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.DescuadresToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.BuscarToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButtonSumesySaldos = New System.Windows.Forms.ToolStripButton
        Me.PdfToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.RefrescaToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButtonExcelSumesiSdosDetall = New System.Windows.Forms.ToolStripButton
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.DataGridView2 = New System.Windows.Forms.DataGridView
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.DataGridView3 = New System.Windows.Forms.DataGridView
        Me.DataGridView4 = New System.Windows.Forms.DataGridView
        Me.TabPageResults = New System.Windows.Forms.TabPage
        Me.DataGridViewResults = New System.Windows.Forms.DataGridView
        Me.PictureBoxResults = New System.Windows.Forms.PictureBox
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.DataGridViewResultsDetail = New System.Windows.Forms.DataGridView
        Me.ToolStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageResults.SuspendLayout()
        CType(Me.DataGridViewResults, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxResults, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewResultsDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripButton, Me.ToolStripSeparator1, Me.DescuadresToolStripButton, Me.BuscarToolStripButton, Me.ToolStripButtonSumesySaldos, Me.PdfToolStripButton, Me.RefrescaToolStripButton, Me.ToolStripButtonExcelSumesiSdosDetall})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(865, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ExcelToolStripButton
        '
        Me.ExcelToolStripButton.Image = My.Resources.Resources.Excel
        Me.ExcelToolStripButton.Name = "ExcelToolStripButton"
        Me.ExcelToolStripButton.Size = New System.Drawing.Size(53, 22)
        Me.ExcelToolStripButton.Text = "Excel"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'DescuadresToolStripButton
        '
        Me.DescuadresToolStripButton.Image = My.Resources.Resources.warn
        Me.DescuadresToolStripButton.Name = "DescuadresToolStripButton"
        Me.DescuadresToolStripButton.Size = New System.Drawing.Size(87, 22)
        Me.DescuadresToolStripButton.Text = "Descuadres"
        '
        'BuscarToolStripButton
        '
        Me.BuscarToolStripButton.Image = My.Resources.Resources.search_16
        Me.BuscarToolStripButton.Name = "BuscarToolStripButton"
        Me.BuscarToolStripButton.Size = New System.Drawing.Size(62, 22)
        Me.BuscarToolStripButton.Text = "Buscar"
        '
        'ToolStripButtonSumesySaldos
        '
        Me.ToolStripButtonSumesySaldos.Image = My.Resources.Resources.tabla_16
        Me.ToolStripButtonSumesySaldos.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonSumesySaldos.Name = "ToolStripButtonSumesySaldos"
        Me.ToolStripButtonSumesySaldos.Size = New System.Drawing.Size(104, 22)
        Me.ToolStripButtonSumesySaldos.Text = "Sumes i saldos"
        '
        'PdfToolStripButton
        '
        Me.PdfToolStripButton.Image = My.Resources.Resources.pdf
        Me.PdfToolStripButton.Name = "PdfToolStripButton"
        Me.PdfToolStripButton.Size = New System.Drawing.Size(45, 22)
        Me.PdfToolStripButton.Text = "pdf"
        '
        'RefrescaToolStripButton
        '
        Me.RefrescaToolStripButton.Image = My.Resources.Resources.refresca
        Me.RefrescaToolStripButton.Name = "RefrescaToolStripButton"
        Me.RefrescaToolStripButton.Size = New System.Drawing.Size(68, 22)
        Me.RefrescaToolStripButton.Text = "refresca"
        '
        'ToolStripButtonExcelSumesiSdosDetall
        '
        Me.ToolStripButtonExcelSumesiSdosDetall.Image = My.Resources.Resources.Excel
        Me.ToolStripButtonExcelSumesiSdosDetall.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonExcelSumesiSdosDetall.Name = "ToolStripButtonExcelSumesiSdosDetall"
        Me.ToolStripButtonExcelSumesiSdosDetall.Size = New System.Drawing.Size(136, 22)
        Me.ToolStripButtonExcelSumesiSdosDetall.Text = "Sumes i saldos detall"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPageResults)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 25)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(865, 439)
        Me.TabControl1.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.SplitContainer1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(857, 413)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "BALANÇ"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridView2)
        Me.SplitContainer1.Size = New System.Drawing.Size(851, 407)
        Me.SplitContainer1.SplitterDistance = 421
        Me.SplitContainer1.TabIndex = 5
        Me.SplitContainer1.Text = "SplitContainer1"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(421, 407)
        Me.DataGridView1.TabIndex = 0
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView2.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.Size = New System.Drawing.Size(426, 407)
        Me.DataGridView2.TabIndex = 1
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.SplitContainer2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(857, 413)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "COMPTE D'EXPLOTACIO"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.DataGridView3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.DataGridView4)
        Me.SplitContainer2.Size = New System.Drawing.Size(851, 407)
        Me.SplitContainer2.SplitterDistance = 425
        Me.SplitContainer2.TabIndex = 5
        Me.SplitContainer2.Text = "SplitContainer2"
        '
        'DataGridView3
        '
        Me.DataGridView3.AllowUserToAddRows = False
        Me.DataGridView3.AllowUserToDeleteRows = False
        Me.DataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView3.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView3.Name = "DataGridView3"
        Me.DataGridView3.ReadOnly = True
        Me.DataGridView3.Size = New System.Drawing.Size(425, 407)
        Me.DataGridView3.TabIndex = 1
        '
        'DataGridView4
        '
        Me.DataGridView4.AllowUserToAddRows = False
        Me.DataGridView4.AllowUserToDeleteRows = False
        Me.DataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView4.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView4.Name = "DataGridView4"
        Me.DataGridView4.ReadOnly = True
        Me.DataGridView4.Size = New System.Drawing.Size(422, 407)
        Me.DataGridView4.TabIndex = 1
        '
        'TabPageResults
        '
        Me.TabPageResults.Controls.Add(Me.DataGridViewResultsDetail)
        Me.TabPageResults.Controls.Add(Me.DataGridViewResults)
        Me.TabPageResults.Controls.Add(Me.PictureBoxResults)
        Me.TabPageResults.Location = New System.Drawing.Point(4, 22)
        Me.TabPageResults.Name = "TabPageResults"
        Me.TabPageResults.Size = New System.Drawing.Size(857, 413)
        Me.TabPageResults.TabIndex = 2
        Me.TabPageResults.Text = "RESULTATS"
        Me.TabPageResults.UseVisualStyleBackColor = True
        '
        'DataGridViewResults
        '
        Me.DataGridViewResults.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewResults.Location = New System.Drawing.Point(444, 23)
        Me.DataGridViewResults.Name = "DataGridViewResults"
        Me.DataGridViewResults.Size = New System.Drawing.Size(385, 145)
        Me.DataGridViewResults.TabIndex = 1
        '
        'PictureBoxResults
        '
        Me.PictureBoxResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxResults.Location = New System.Drawing.Point(19, 23)
        Me.PictureBoxResults.Name = "PictureBoxResults"
        Me.PictureBoxResults.Size = New System.Drawing.Size(419, 361)
        Me.PictureBoxResults.TabIndex = 0
        Me.PictureBoxResults.TabStop = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(766, 1)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePicker1.TabIndex = 10
        '
        'DataGridViewResultsDetail
        '
        Me.DataGridViewResultsDetail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewResultsDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewResultsDetail.Location = New System.Drawing.Point(444, 174)
        Me.DataGridViewResultsDetail.Name = "DataGridViewResultsDetail"
        Me.DataGridViewResultsDetail.Size = New System.Drawing.Size(385, 210)
        Me.DataGridViewResultsDetail.TabIndex = 2
        '
        'Frm_Balance
        '
        Me.ClientSize = New System.Drawing.Size(865, 464)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_Balance"
        Me.Text = "BALANÇ"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageResults.ResumeLayout(False)
        CType(Me.DataGridViewResults, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxResults, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewResultsDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ExcelToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DescuadresToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents BuscarToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PdfToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents RefrescaToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents TabPageResults As System.Windows.Forms.TabPage
    Friend WithEvents PictureBoxResults As System.Windows.Forms.PictureBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewResults As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView3 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView4 As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStripButtonSumesySaldos As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonExcelSumesiSdosDetall As System.Windows.Forms.ToolStripButton
    Friend WithEvents DataGridViewResultsDetail As System.Windows.Forms.DataGridView
End Class
