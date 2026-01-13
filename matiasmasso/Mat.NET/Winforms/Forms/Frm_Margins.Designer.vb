<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Margins
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
        Me.SplitContainer12 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer123 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer34 = New System.Windows.Forms.SplitContainer()
        Me.DateTimePickerFchFrom = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerFchTo = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_Marges1 = New Winforms.Xl_Marges()
        Me.Xl_Marges2 = New Winforms.Xl_Marges()
        Me.Xl_Marges3 = New Winforms.Xl_Marges()
        Me.Xl_CustomerPmcs1 = New Winforms.Xl_CustomerPmcs()
        CType(Me.SplitContainer12, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer12.Panel1.SuspendLayout()
        Me.SplitContainer12.Panel2.SuspendLayout()
        Me.SplitContainer12.SuspendLayout()
        CType(Me.SplitContainer123, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer123.Panel1.SuspendLayout()
        Me.SplitContainer123.Panel2.SuspendLayout()
        Me.SplitContainer123.SuspendLayout()
        CType(Me.SplitContainer34, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer34.Panel1.SuspendLayout()
        Me.SplitContainer34.Panel2.SuspendLayout()
        Me.SplitContainer34.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_Marges1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_Marges2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_Marges3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_CustomerPmcs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer12
        '
        Me.SplitContainer12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer12.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer12.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer12.Name = "SplitContainer12"
        Me.SplitContainer12.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer12.Panel1
        '
        Me.SplitContainer12.Panel1.Controls.Add(Me.Xl_Marges1)
        '
        'SplitContainer12.Panel2
        '
        Me.SplitContainer12.Panel2.Controls.Add(Me.Xl_Marges2)
        Me.SplitContainer12.Size = New System.Drawing.Size(742, 316)
        Me.SplitContainer12.SplitterDistance = 158
        Me.SplitContainer12.TabIndex = 1
        '
        'SplitContainer123
        '
        Me.SplitContainer123.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer123.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer123.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer123.Name = "SplitContainer123"
        Me.SplitContainer123.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer123.Panel1
        '
        Me.SplitContainer123.Panel1.Controls.Add(Me.SplitContainer12)
        '
        'SplitContainer123.Panel2
        '
        Me.SplitContainer123.Panel2.Controls.Add(Me.SplitContainer34)
        Me.SplitContainer123.Size = New System.Drawing.Size(742, 612)
        Me.SplitContainer123.SplitterDistance = 316
        Me.SplitContainer123.TabIndex = 2
        '
        'SplitContainer34
        '
        Me.SplitContainer34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer34.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer34.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer34.Name = "SplitContainer34"
        Me.SplitContainer34.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer34.Panel1
        '
        Me.SplitContainer34.Panel1.Controls.Add(Me.Xl_Marges3)
        '
        'SplitContainer34.Panel2
        '
        Me.SplitContainer34.Panel2.Controls.Add(Me.Xl_CustomerPmcs1)
        Me.SplitContainer34.Size = New System.Drawing.Size(742, 292)
        Me.SplitContainer34.SplitterDistance = 153
        Me.SplitContainer34.TabIndex = 1
        '
        'DateTimePickerFchFrom
        '
        Me.DateTimePickerFchFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerFchFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchFrom.Location = New System.Drawing.Point(635, 4)
        Me.DateTimePickerFchFrom.Name = "DateTimePickerFchFrom"
        Me.DateTimePickerFchFrom.Size = New System.Drawing.Size(99, 20)
        Me.DateTimePickerFchFrom.TabIndex = 3
        '
        'DateTimePickerFchTo
        '
        Me.DateTimePickerFchTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerFchTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchTo.Location = New System.Drawing.Point(635, 28)
        Me.DateTimePickerFchTo.Name = "DateTimePickerFchTo"
        Me.DateTimePickerFchTo.Size = New System.Drawing.Size(99, 20)
        Me.DateTimePickerFchTo.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(587, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "des de:"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(589, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "fins:"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.SplitContainer123)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(742, 635)
        Me.Panel1.TabIndex = 7
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 612)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(742, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar1.TabIndex = 3
        '
        'Xl_Marges1
        '
        Me.Xl_Marges1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Marges1.DisplayObsolets = False
        Me.Xl_Marges1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Marges1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Marges1.MouseIsDown = False
        Me.Xl_Marges1.Name = "Xl_Marges1"
        Me.Xl_Marges1.Size = New System.Drawing.Size(742, 158)
        Me.Xl_Marges1.TabIndex = 0
        '
        'Xl_Marges2
        '
        Me.Xl_Marges2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Marges2.DisplayObsolets = False
        Me.Xl_Marges2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Marges2.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Marges2.MouseIsDown = False
        Me.Xl_Marges2.Name = "Xl_Marges2"
        Me.Xl_Marges2.Size = New System.Drawing.Size(742, 154)
        Me.Xl_Marges2.TabIndex = 1
        '
        'Xl_Marges3
        '
        Me.Xl_Marges3.AllowUserToAddRows = False
        Me.Xl_Marges3.AllowUserToDeleteRows = False
        Me.Xl_Marges3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Marges3.DisplayObsolets = False
        Me.Xl_Marges3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Marges3.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Marges3.MouseIsDown = False
        Me.Xl_Marges3.Name = "Xl_Marges3"
        Me.Xl_Marges3.ReadOnly = True
        Me.Xl_Marges3.Size = New System.Drawing.Size(742, 153)
        Me.Xl_Marges3.TabIndex = 0
        '
        'Xl_CustomerPmcs1
        '
        Me.Xl_CustomerPmcs1.AllowUserToAddRows = False
        Me.Xl_CustomerPmcs1.AllowUserToDeleteRows = False
        Me.Xl_CustomerPmcs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_CustomerPmcs1.DisplayObsolets = False
        Me.Xl_CustomerPmcs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_CustomerPmcs1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_CustomerPmcs1.MouseIsDown = False
        Me.Xl_CustomerPmcs1.Name = "Xl_CustomerPmcs1"
        Me.Xl_CustomerPmcs1.ReadOnly = True
        Me.Xl_CustomerPmcs1.Size = New System.Drawing.Size(742, 135)
        Me.Xl_CustomerPmcs1.TabIndex = 0
        '
        'Frm_Margins
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(743, 688)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DateTimePickerFchTo)
        Me.Controls.Add(Me.DateTimePickerFchFrom)
        Me.Name = "Frm_Margins"
        Me.Text = "Marges comercials"
        Me.SplitContainer12.Panel1.ResumeLayout(False)
        Me.SplitContainer12.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer12, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer12.ResumeLayout(False)
        Me.SplitContainer123.Panel1.ResumeLayout(False)
        Me.SplitContainer123.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer123, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer123.ResumeLayout(False)
        Me.SplitContainer34.Panel1.ResumeLayout(False)
        Me.SplitContainer34.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer34, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer34.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_Marges1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_Marges2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_Marges3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_CustomerPmcs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_Marges1 As Xl_Marges
    Friend WithEvents SplitContainer12 As SplitContainer
    Friend WithEvents Xl_Marges2 As Xl_Marges
    Friend WithEvents SplitContainer123 As SplitContainer
    Friend WithEvents Xl_CustomerPmcs1 As Xl_CustomerPmcs
    Friend WithEvents DateTimePickerFchFrom As DateTimePicker
    Friend WithEvents DateTimePickerFchTo As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents SplitContainer34 As SplitContainer
    Friend WithEvents Xl_Marges3 As Xl_Marges
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
