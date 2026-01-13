<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Contracts
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IncludePrivatsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImprimirActiusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckBoxFch = New System.Windows.Forms.CheckBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_ContractCodis1 = New Mat.Net.Xl_ContractCodis()
        Me.Xl_Contracts1 = New Mat.Net.Xl_Contracts()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_ContractCodis1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_Contracts1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_ContractCodis1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_Contracts1)
        Me.SplitContainer1.Size = New System.Drawing.Size(732, 238)
        Me.SplitContainer1.SplitterDistance = 249
        Me.SplitContainer1.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(732, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IncludePrivatsToolStripMenuItem, Me.ExcelToolStripMenuItem, Me.ImprimirActiusToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 22)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'IncludePrivatsToolStripMenuItem
        '
        Me.IncludePrivatsToolStripMenuItem.CheckOnClick = True
        Me.IncludePrivatsToolStripMenuItem.Name = "IncludePrivatsToolStripMenuItem"
        Me.IncludePrivatsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.IncludePrivatsToolStripMenuItem.Text = "Inclou privats"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Image = Global.Mat.Net.My.Resources.Resources.Excel_16
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(188, 30)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'ImprimirActiusToolStripMenuItem
        '
        Me.ImprimirActiusToolStripMenuItem.Image = Global.Mat.Net.My.Resources.Resources.pdf
        Me.ImprimirActiusToolStripMenuItem.Name = "ImprimirActiusToolStripMenuItem"
        Me.ImprimirActiusToolStripMenuItem.Size = New System.Drawing.Size(188, 30)
        Me.ImprimirActiusToolStripMenuItem.Text = "Imprimir actius"
        '
        'CheckBoxFch
        '
        Me.CheckBoxFch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxFch.AutoSize = True
        Me.CheckBoxFch.Location = New System.Drawing.Point(466, 9)
        Me.CheckBoxFch.Name = "CheckBoxFch"
        Me.CheckBoxFch.Size = New System.Drawing.Size(162, 17)
        Me.CheckBoxFch.TabIndex = 2
        Me.CheckBoxFch.Text = "Contractes actius a una data"
        Me.CheckBoxFch.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Enabled = False
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(635, 7)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(97, 20)
        Me.DateTimePicker1.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 40)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(732, 261)
        Me.Panel1.TabIndex = 4
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 238)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(732, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 1
        Me.ProgressBar1.Visible = False
        '
        'Xl_ContractCodis1
        '
        Me.Xl_ContractCodis1.AllowUserToAddRows = False
        Me.Xl_ContractCodis1.AllowUserToDeleteRows = False
        Me.Xl_ContractCodis1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ContractCodis1.DisplayObsolets = False
        Me.Xl_ContractCodis1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ContractCodis1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ContractCodis1.MouseIsDown = False
        Me.Xl_ContractCodis1.Name = "Xl_ContractCodis1"
        Me.Xl_ContractCodis1.ReadOnly = True
        Me.Xl_ContractCodis1.RowHeadersWidth = 62
        Me.Xl_ContractCodis1.Size = New System.Drawing.Size(249, 238)
        Me.Xl_ContractCodis1.TabIndex = 0
        '
        'Xl_Contracts1
        '
        Me.Xl_Contracts1.AllowUserToAddRows = False
        Me.Xl_Contracts1.AllowUserToDeleteRows = False
        Me.Xl_Contracts1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Contracts1.DisplayObsolets = False
        Me.Xl_Contracts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Contracts1.Filter = Nothing
        Me.Xl_Contracts1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Contracts1.MouseIsDown = False
        Me.Xl_Contracts1.Name = "Xl_Contracts1"
        Me.Xl_Contracts1.ReadOnly = True
        Me.Xl_Contracts1.RowHeadersWidth = 62
        Me.Xl_Contracts1.Size = New System.Drawing.Size(479, 238)
        Me.Xl_Contracts1.TabIndex = 0
        '
        'Frm_Contracts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(732, 302)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.CheckBoxFch)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Contracts"
        Me.Text = "Contractes"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_ContractCodis1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_Contracts1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_ContractCodis1 As Xl_ContractCodis
    Friend WithEvents Xl_Contracts1 As Xl_Contracts
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents IncludePrivatsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CheckBoxFch As CheckBox
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents ImprimirActiusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
