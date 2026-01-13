<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_WebLog
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
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ButtonRefresh = New System.Windows.Forms.Button()
        Me.Xl_Weblogs2Summary1 = New Winforms.Xl_Weblogs2Summary()
        Me.Xl_WebLogs21 = New Winforms.Xl_WebLogs2()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_Weblogs2Summary1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_WebLogs21, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ComboBox1
        '
        Me.ComboBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"(entre dates...)", "avui", "ahir", "darrera setmana", "darrer mes", "lo que va d'any"})
        Me.ComboBox1.Location = New System.Drawing.Point(13, 13)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(328, 21)
        Me.ComboBox1.TabIndex = 0
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(343, 14)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(102, 20)
        Me.DateTimePicker1.TabIndex = 1
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker2.Location = New System.Drawing.Point(451, 14)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(102, 20)
        Me.DateTimePicker2.TabIndex = 2
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(12, 40)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_Weblogs2Summary1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_WebLogs21)
        Me.SplitContainer1.Size = New System.Drawing.Size(582, 336)
        Me.SplitContainer1.SplitterDistance = 168
        Me.SplitContainer1.TabIndex = 3
        '
        'ButtonRefresh
        '
        Me.ButtonRefresh.Location = New System.Drawing.Point(560, 13)
        Me.ButtonRefresh.Name = "ButtonRefresh"
        Me.ButtonRefresh.Size = New System.Drawing.Size(34, 23)
        Me.ButtonRefresh.TabIndex = 4
        Me.ButtonRefresh.Text = "..."
        Me.ButtonRefresh.UseVisualStyleBackColor = True
        '
        'Xl_Weblogs2Summary1
        '
        Me.Xl_Weblogs2Summary1.AllowUserToAddRows = False
        Me.Xl_Weblogs2Summary1.AllowUserToDeleteRows = False
        Me.Xl_Weblogs2Summary1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Weblogs2Summary1.DisplayObsolets = False
        Me.Xl_Weblogs2Summary1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Weblogs2Summary1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Weblogs2Summary1.MouseIsDown = False
        Me.Xl_Weblogs2Summary1.Name = "Xl_Weblogs2Summary1"
        Me.Xl_Weblogs2Summary1.ReadOnly = True
        Me.Xl_Weblogs2Summary1.Size = New System.Drawing.Size(582, 168)
        Me.Xl_Weblogs2Summary1.TabIndex = 0
        '
        'Xl_WebLogs21
        '
        Me.Xl_WebLogs21.AllowUserToAddRows = False
        Me.Xl_WebLogs21.AllowUserToDeleteRows = False
        Me.Xl_WebLogs21.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_WebLogs21.DisplayObsolets = False
        Me.Xl_WebLogs21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_WebLogs21.Location = New System.Drawing.Point(0, 0)
        Me.Xl_WebLogs21.MouseIsDown = False
        Me.Xl_WebLogs21.Name = "Xl_WebLogs21"
        Me.Xl_WebLogs21.ReadOnly = True
        Me.Xl_WebLogs21.Size = New System.Drawing.Size(582, 164)
        Me.Xl_WebLogs21.TabIndex = 0
        '
        'Frm_WebLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(600, 388)
        Me.Controls.Add(Me.ButtonRefresh)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Name = "Frm_WebLog"
        Me.Text = "Registre de visites web"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_Weblogs2Summary1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_WebLogs21, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ButtonRefresh As System.Windows.Forms.Button
    Friend WithEvents Xl_Weblogs2Summary1 As Xl_Weblogs2Summary
    Friend WithEvents Xl_WebLogs21 As Xl_WebLogs2
End Class
