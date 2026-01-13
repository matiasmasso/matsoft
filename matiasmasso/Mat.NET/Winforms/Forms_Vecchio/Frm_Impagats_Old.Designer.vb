<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Impagats_Old
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Impagats_Old))
        Me.DataGridViewImpagats = New System.Windows.Forms.DataGridView
        Me.CheckBoxHideSaldats = New System.Windows.Forms.CheckBox
        Me.CheckBoxHideInsolvencias = New System.Windows.Forms.CheckBox
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPageImpagats = New System.Windows.Forms.TabPage
        Me.TextBoxImpagats = New System.Windows.Forms.TextBox
        Me.TabPageInsolvencias = New System.Windows.Forms.TabPage
        Me.CheckBoxHideLiquidats = New System.Windows.Forms.CheckBox
        Me.DataGridViewInsolvencies = New System.Windows.Forms.DataGridView
        Me.TabPageDeutors = New System.Windows.Forms.TabPage
        Me.TextBoxDeutors = New System.Windows.Forms.TextBox
        Me.DataGridViewDeutors = New System.Windows.Forms.DataGridView
        Me.TabPageStat = New System.Windows.Forms.TabPage
        Me.ComboBoxStatYea = New System.Windows.Forms.ComboBox
        Me.DataGridViewStat = New System.Windows.Forms.DataGridView
        Me.PictureBoxGraph = New System.Windows.Forms.PictureBox
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.TabPageMxf = New System.Windows.Forms.TabPage
        Me.DataGridViewMxf = New System.Windows.Forms.DataGridView
        CType(Me.DataGridViewImpagats, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPageImpagats.SuspendLayout()
        Me.TabPageInsolvencias.SuspendLayout()
        CType(Me.DataGridViewInsolvencies, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageDeutors.SuspendLayout()
        CType(Me.DataGridViewDeutors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageStat.SuspendLayout()
        CType(Me.DataGridViewStat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxGraph, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageMxf.SuspendLayout()
        CType(Me.DataGridViewMxf, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridViewImpagats
        '
        Me.DataGridViewImpagats.AllowUserToAddRows = False
        Me.DataGridViewImpagats.AllowUserToDeleteRows = False
        Me.DataGridViewImpagats.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewImpagats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewImpagats.Location = New System.Drawing.Point(3, 31)
        Me.DataGridViewImpagats.Name = "DataGridViewImpagats"
        Me.DataGridViewImpagats.ReadOnly = True
        Me.DataGridViewImpagats.Size = New System.Drawing.Size(901, 535)
        Me.DataGridViewImpagats.TabIndex = 1
        '
        'CheckBoxHideSaldats
        '
        Me.CheckBoxHideSaldats.AutoSize = True
        Me.CheckBoxHideSaldats.Checked = True
        Me.CheckBoxHideSaldats.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxHideSaldats.Location = New System.Drawing.Point(549, 6)
        Me.CheckBoxHideSaldats.Name = "CheckBoxHideSaldats"
        Me.CheckBoxHideSaldats.Size = New System.Drawing.Size(93, 17)
        Me.CheckBoxHideSaldats.TabIndex = 2
        Me.CheckBoxHideSaldats.Text = "Oculta saldats"
        Me.CheckBoxHideSaldats.UseVisualStyleBackColor = True
        '
        'CheckBoxHideInsolvencias
        '
        Me.CheckBoxHideInsolvencias.AutoSize = True
        Me.CheckBoxHideInsolvencias.Checked = True
        Me.CheckBoxHideInsolvencias.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxHideInsolvencias.Location = New System.Drawing.Point(660, 6)
        Me.CheckBoxHideInsolvencias.Name = "CheckBoxHideInsolvencias"
        Me.CheckBoxHideInsolvencias.Size = New System.Drawing.Size(118, 17)
        Me.CheckBoxHideInsolvencias.TabIndex = 3
        Me.CheckBoxHideInsolvencias.Text = "Oculta insolvencies"
        Me.CheckBoxHideInsolvencias.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageImpagats)
        Me.TabControl1.Controls.Add(Me.TabPageInsolvencias)
        Me.TabControl1.Controls.Add(Me.TabPageDeutors)
        Me.TabControl1.Controls.Add(Me.TabPageStat)
        Me.TabControl1.Controls.Add(Me.TabPageMxf)
        Me.TabControl1.ImageList = Me.ImageList1
        Me.TabControl1.Location = New System.Drawing.Point(0, 50)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(923, 593)
        Me.TabControl1.TabIndex = 5
        '
        'TabPageImpagats
        '
        Me.TabPageImpagats.Controls.Add(Me.TextBoxImpagats)
        Me.TabPageImpagats.Controls.Add(Me.DataGridViewImpagats)
        Me.TabPageImpagats.Controls.Add(Me.CheckBoxHideInsolvencias)
        Me.TabPageImpagats.Controls.Add(Me.CheckBoxHideSaldats)
        Me.TabPageImpagats.ImageIndex = 0
        Me.TabPageImpagats.Location = New System.Drawing.Point(4, 23)
        Me.TabPageImpagats.Name = "TabPageImpagats"
        Me.TabPageImpagats.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageImpagats.Size = New System.Drawing.Size(915, 566)
        Me.TabPageImpagats.TabIndex = 0
        Me.TabPageImpagats.Text = "IMPAGATS"
        Me.TabPageImpagats.UseVisualStyleBackColor = True
        '
        'TextBoxImpagats
        '
        Me.TextBoxImpagats.Location = New System.Drawing.Point(7, 6)
        Me.TextBoxImpagats.Name = "TextBoxImpagats"
        Me.TextBoxImpagats.ReadOnly = True
        Me.TextBoxImpagats.Size = New System.Drawing.Size(531, 20)
        Me.TextBoxImpagats.TabIndex = 4
        '
        'TabPageInsolvencias
        '
        Me.TabPageInsolvencias.Controls.Add(Me.CheckBoxHideLiquidats)
        Me.TabPageInsolvencias.Controls.Add(Me.DataGridViewInsolvencies)
        Me.TabPageInsolvencias.ImageIndex = 1
        Me.TabPageInsolvencias.Location = New System.Drawing.Point(4, 23)
        Me.TabPageInsolvencias.Name = "TabPageInsolvencias"
        Me.TabPageInsolvencias.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageInsolvencias.Size = New System.Drawing.Size(915, 566)
        Me.TabPageInsolvencias.TabIndex = 1
        Me.TabPageInsolvencias.Text = "INSOLVENCIES"
        Me.TabPageInsolvencias.UseVisualStyleBackColor = True
        '
        'CheckBoxHideLiquidats
        '
        Me.CheckBoxHideLiquidats.AutoSize = True
        Me.CheckBoxHideLiquidats.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxHideLiquidats.Checked = True
        Me.CheckBoxHideLiquidats.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxHideLiquidats.Location = New System.Drawing.Point(809, 6)
        Me.CheckBoxHideLiquidats.Name = "CheckBoxHideLiquidats"
        Me.CheckBoxHideLiquidats.Size = New System.Drawing.Size(98, 17)
        Me.CheckBoxHideLiquidats.TabIndex = 3
        Me.CheckBoxHideLiquidats.Text = "Oculta liquidats"
        Me.CheckBoxHideLiquidats.UseVisualStyleBackColor = True
        '
        'DataGridViewInsolvencies
        '
        Me.DataGridViewInsolvencies.AllowUserToAddRows = False
        Me.DataGridViewInsolvencies.AllowUserToDeleteRows = False
        Me.DataGridViewInsolvencies.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewInsolvencies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewInsolvencies.Location = New System.Drawing.Point(3, 28)
        Me.DataGridViewInsolvencies.Name = "DataGridViewInsolvencies"
        Me.DataGridViewInsolvencies.ReadOnly = True
        Me.DataGridViewInsolvencies.Size = New System.Drawing.Size(904, 535)
        Me.DataGridViewInsolvencies.TabIndex = 2
        '
        'TabPageDeutors
        '
        Me.TabPageDeutors.Controls.Add(Me.TextBoxDeutors)
        Me.TabPageDeutors.Controls.Add(Me.DataGridViewDeutors)
        Me.TabPageDeutors.ImageIndex = 2
        Me.TabPageDeutors.Location = New System.Drawing.Point(4, 23)
        Me.TabPageDeutors.Name = "TabPageDeutors"
        Me.TabPageDeutors.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageDeutors.Size = New System.Drawing.Size(915, 566)
        Me.TabPageDeutors.TabIndex = 2
        Me.TabPageDeutors.Text = "DEUTORS"
        Me.TabPageDeutors.UseVisualStyleBackColor = True
        '
        'TextBoxDeutors
        '
        Me.TextBoxDeutors.Location = New System.Drawing.Point(11, 3)
        Me.TextBoxDeutors.Name = "TextBoxDeutors"
        Me.TextBoxDeutors.ReadOnly = True
        Me.TextBoxDeutors.Size = New System.Drawing.Size(531, 20)
        Me.TextBoxDeutors.TabIndex = 6
        '
        'DataGridViewDeutors
        '
        Me.DataGridViewDeutors.AllowUserToAddRows = False
        Me.DataGridViewDeutors.AllowUserToDeleteRows = False
        Me.DataGridViewDeutors.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewDeutors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewDeutors.Location = New System.Drawing.Point(7, 28)
        Me.DataGridViewDeutors.Name = "DataGridViewDeutors"
        Me.DataGridViewDeutors.ReadOnly = True
        Me.DataGridViewDeutors.Size = New System.Drawing.Size(778, 535)
        Me.DataGridViewDeutors.TabIndex = 5
        '
        'TabPageStat
        '
        Me.TabPageStat.Controls.Add(Me.ComboBoxStatYea)
        Me.TabPageStat.Controls.Add(Me.DataGridViewStat)
        Me.TabPageStat.Controls.Add(Me.PictureBoxGraph)
        Me.TabPageStat.Location = New System.Drawing.Point(4, 23)
        Me.TabPageStat.Name = "TabPageStat"
        Me.TabPageStat.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageStat.Size = New System.Drawing.Size(915, 566)
        Me.TabPageStat.TabIndex = 3
        Me.TabPageStat.Text = "ESTADISTICA"
        Me.TabPageStat.UseVisualStyleBackColor = True
        '
        'ComboBoxStatYea
        '
        Me.ComboBoxStatYea.FormattingEnabled = True
        Me.ComboBoxStatYea.Location = New System.Drawing.Point(426, 18)
        Me.ComboBoxStatYea.Name = "ComboBoxStatYea"
        Me.ComboBoxStatYea.Size = New System.Drawing.Size(52, 21)
        Me.ComboBoxStatYea.TabIndex = 3
        Me.ComboBoxStatYea.Text = "2008"
        '
        'DataGridViewStat
        '
        Me.DataGridViewStat.AllowUserToAddRows = False
        Me.DataGridViewStat.AllowUserToDeleteRows = False
        Me.DataGridViewStat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewStat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewStat.Location = New System.Drawing.Point(3, 3)
        Me.DataGridViewStat.Name = "DataGridViewStat"
        Me.DataGridViewStat.ReadOnly = True
        Me.DataGridViewStat.Size = New System.Drawing.Size(321, 560)
        Me.DataGridViewStat.TabIndex = 2
        '
        'PictureBoxGraph
        '
        Me.PictureBoxGraph.Location = New System.Drawing.Point(330, 94)
        Me.PictureBoxGraph.Name = "PictureBoxGraph"
        Me.PictureBoxGraph.Size = New System.Drawing.Size(579, 335)
        Me.PictureBoxGraph.TabIndex = 0
        Me.PictureBoxGraph.TabStop = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "pirata18X20.gif")
        Me.ImageList1.Images.SetKeyName(1, "cyc.gif")
        Me.ImageList1.Images.SetKeyName(2, "People_Blue.gif")
        '
        'TabPageMxf
        '
        Me.TabPageMxf.Controls.Add(Me.DataGridViewMxf)
        Me.TabPageMxf.Location = New System.Drawing.Point(4, 23)
        Me.TabPageMxf.Name = "TabPageMxf"
        Me.TabPageMxf.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageMxf.Size = New System.Drawing.Size(915, 566)
        Me.TabPageMxf.TabIndex = 4
        Me.TabPageMxf.Text = "MXF"
        Me.TabPageMxf.UseVisualStyleBackColor = True
        '
        'DataGridViewMxf
        '
        Me.DataGridViewMxf.AllowUserToAddRows = False
        Me.DataGridViewMxf.AllowUserToDeleteRows = False
        Me.DataGridViewMxf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewMxf.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewMxf.Location = New System.Drawing.Point(3, 3)
        Me.DataGridViewMxf.Name = "DataGridViewMxf"
        Me.DataGridViewMxf.ReadOnly = True
        Me.DataGridViewMxf.Size = New System.Drawing.Size(909, 560)
        Me.DataGridViewMxf.TabIndex = 6
        '
        'Frm_Impagats_New
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(927, 644)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Impagats_New"
        Me.Text = "MOROSITAT"
        CType(Me.DataGridViewImpagats, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageImpagats.ResumeLayout(False)
        Me.TabPageImpagats.PerformLayout()
        Me.TabPageInsolvencias.ResumeLayout(False)
        Me.TabPageInsolvencias.PerformLayout()
        CType(Me.DataGridViewInsolvencies, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageDeutors.ResumeLayout(False)
        Me.TabPageDeutors.PerformLayout()
        CType(Me.DataGridViewDeutors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageStat.ResumeLayout(False)
        CType(Me.DataGridViewStat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxGraph, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageMxf.ResumeLayout(False)
        CType(Me.DataGridViewMxf, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridViewImpagats As System.Windows.Forms.DataGridView
    Friend WithEvents CheckBoxHideSaldats As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxHideInsolvencias As System.Windows.Forms.CheckBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageImpagats As System.Windows.Forms.TabPage
    Friend WithEvents TabPageInsolvencias As System.Windows.Forms.TabPage
    Friend WithEvents DataGridViewInsolvencies As System.Windows.Forms.DataGridView
    Friend WithEvents TextBoxImpagats As System.Windows.Forms.TextBox
    Friend WithEvents TabPageDeutors As System.Windows.Forms.TabPage
    Friend WithEvents TextBoxDeutors As System.Windows.Forms.TextBox
    Friend WithEvents DataGridViewDeutors As System.Windows.Forms.DataGridView
    Friend WithEvents CheckBoxHideLiquidats As System.Windows.Forms.CheckBox
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents TabPageStat As System.Windows.Forms.TabPage
    Friend WithEvents PictureBoxGraph As System.Windows.Forms.PictureBox
    Friend WithEvents DataGridViewStat As System.Windows.Forms.DataGridView
    Friend WithEvents ComboBoxStatYea As System.Windows.Forms.ComboBox
    Friend WithEvents TabPageMxf As System.Windows.Forms.TabPage
    Friend WithEvents DataGridViewMxf As System.Windows.Forms.DataGridView
End Class
