<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Extracte
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TextBoxSearch = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.LabelSumPnds = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_Extracte_Years1 = New Mat.NET.Xl_Extracte_Years()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Xl_Extracte_Ctas1 = New Mat.NET.Xl_Extracte_Ctas()
        Me.Xl_Extracte_Ccbs1 = New Mat.NET.Xl_Extracte_Ccbs()
        Me.Xl_Pnds1 = New Mat.NET.Xl_Pnds()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_Extracte_Years1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.Xl_Extracte_Ctas1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_Extracte_Ccbs1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_Pnds1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxSearch
        '
        Me.TextBoxSearch.Location = New System.Drawing.Point(612, 7)
        Me.TextBoxSearch.Name = "TextBoxSearch"
        Me.TextBoxSearch.Size = New System.Drawing.Size(181, 20)
        Me.TextBoxSearch.TabIndex = 2
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Mat.NET.My.Resources.Resources.Lupa
        Me.PictureBox1.Location = New System.Drawing.Point(792, 7)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(25, 30)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 15)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(817, 247)
        Me.TabControl1.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.SplitContainer1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(809, 221)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Extractes"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.LabelSumPnds)
        Me.TabPage2.Controls.Add(Me.Xl_Pnds1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(809, 221)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Partides pendents"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'LabelSumPnds
        '
        Me.LabelSumPnds.AutoSize = True
        Me.LabelSumPnds.Location = New System.Drawing.Point(8, 16)
        Me.LabelSumPnds.Name = "LabelSumPnds"
        Me.LabelSumPnds.Size = New System.Drawing.Size(130, 13)
        Me.LabelSumPnds.TabIndex = 1
        Me.LabelSumPnds.Text = "Total pendent de liquidar: "
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_Extracte_Years1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(803, 215)
        Me.SplitContainer1.SplitterDistance = 60
        Me.SplitContainer1.TabIndex = 1
        '
        'Xl_Extracte_Years1
        '
        Me.Xl_Extracte_Years1.AllowUserToAddRows = False
        Me.Xl_Extracte_Years1.AllowUserToDeleteRows = False
        Me.Xl_Extracte_Years1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Extracte_Years1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Extracte_Years1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Extracte_Years1.Name = "Xl_Extracte_Years1"
        Me.Xl_Extracte_Years1.ReadOnly = True
        Me.Xl_Extracte_Years1.Size = New System.Drawing.Size(60, 215)
        Me.Xl_Extracte_Years1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Xl_Extracte_Ctas1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Xl_Extracte_Ccbs1)
        Me.SplitContainer2.Size = New System.Drawing.Size(739, 215)
        Me.SplitContainer2.SplitterDistance = 150
        Me.SplitContainer2.TabIndex = 0
        '
        'Xl_Extracte_Ctas1
        '
        Me.Xl_Extracte_Ctas1.AllowUserToAddRows = False
        Me.Xl_Extracte_Ctas1.AllowUserToDeleteRows = False
        Me.Xl_Extracte_Ctas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Extracte_Ctas1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Extracte_Ctas1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Extracte_Ctas1.Name = "Xl_Extracte_Ctas1"
        Me.Xl_Extracte_Ctas1.ReadOnly = True
        Me.Xl_Extracte_Ctas1.Size = New System.Drawing.Size(150, 215)
        Me.Xl_Extracte_Ctas1.TabIndex = 0
        '
        'Xl_Extracte_Ccbs1
        '
        Me.Xl_Extracte_Ccbs1.AllowUserToAddRows = False
        Me.Xl_Extracte_Ccbs1.AllowUserToDeleteRows = False
        Me.Xl_Extracte_Ccbs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Extracte_Ccbs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Extracte_Ccbs1.Filter = Nothing
        Me.Xl_Extracte_Ccbs1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Extracte_Ccbs1.Name = "Xl_Extracte_Ccbs1"
        Me.Xl_Extracte_Ccbs1.ReadOnly = True
        Me.Xl_Extracte_Ccbs1.Size = New System.Drawing.Size(585, 215)
        Me.Xl_Extracte_Ccbs1.TabIndex = 0
        '
        'Xl_Pnds1
        '
        Me.Xl_Pnds1.AllowUserToAddRows = False
        Me.Xl_Pnds1.AllowUserToDeleteRows = False
        Me.Xl_Pnds1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Pnds1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Pnds1.Location = New System.Drawing.Point(3, 32)
        Me.Xl_Pnds1.Name = "Xl_Pnds1"
        Me.Xl_Pnds1.ReadOnly = True
        Me.Xl_Pnds1.Size = New System.Drawing.Size(803, 186)
        Me.Xl_Pnds1.TabIndex = 0
        '
        'Frm_Extracte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(817, 261)
        Me.Controls.Add(Me.TextBoxSearch)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Extracte"
        Me.Text = "Comptes"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_Extracte_Years1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.Xl_Extracte_Ctas1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_Extracte_Ccbs1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_Pnds1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_Extracte_Years1 As Mat.NET.Xl_Extracte_Years
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Extracte_Ctas1 As Mat.NET.Xl_Extracte_Ctas
    Friend WithEvents Xl_Extracte_Ccbs1 As Mat.NET.Xl_Extracte_Ccbs
    Friend WithEvents TextBoxSearch As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Pnds1 As Mat.NET.Xl_Pnds
    Friend WithEvents LabelSumPnds As System.Windows.Forms.Label
End Class
