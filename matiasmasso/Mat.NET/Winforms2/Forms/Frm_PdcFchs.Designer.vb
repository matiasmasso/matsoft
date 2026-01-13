<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PdcFchs
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
        Me.DateTimePickerFrom = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerTo = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_PdcFchs1 = New Xl_PdcFchs()
        Me.Xl_PdcSrcs_Checklist1 = New Xl_PdcSrcs_Checklist()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_PdcFchs1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_PdcSrcs_Checklist1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 112)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_PdcSrcs_Checklist1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_PdcFchs1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1549, 598)
        Me.SplitContainer1.SplitterDistance = 516
        Me.SplitContainer1.TabIndex = 0
        '
        'DateTimePickerFrom
        '
        Me.DateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFrom.Location = New System.Drawing.Point(953, 32)
        Me.DateTimePickerFrom.Name = "DateTimePickerFrom"
        Me.DateTimePickerFrom.Size = New System.Drawing.Size(200, 38)
        Me.DateTimePickerFrom.TabIndex = 1
        '
        'DateTimePickerTo
        '
        Me.DateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerTo.Location = New System.Drawing.Point(1337, 32)
        Me.DateTimePickerTo.Name = "DateTimePickerTo"
        Me.DateTimePickerTo.Size = New System.Drawing.Size(200, 38)
        Me.DateTimePickerTo.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(847, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 32)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "des de"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(1271, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 32)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "fins"
        '
        'Xl_PdcFchs1
        '
        Me.Xl_PdcFchs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PdcFchs1.DisplayObsolets = False
        Me.Xl_PdcFchs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PdcFchs1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PdcFchs1.MouseIsDown = False
        Me.Xl_PdcFchs1.Name = "Xl_PdcFchs1"
        Me.Xl_PdcFchs1.RowTemplate.Height = 40
        Me.Xl_PdcFchs1.Size = New System.Drawing.Size(1029, 598)
        Me.Xl_PdcFchs1.TabIndex = 0
        '
        'Xl_PdcSrcs_Checklist1
        '
        Me.Xl_PdcSrcs_Checklist1.AllowUserToAddRows = False
        Me.Xl_PdcSrcs_Checklist1.AllowUserToDeleteRows = False
        Me.Xl_PdcSrcs_Checklist1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PdcSrcs_Checklist1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PdcSrcs_Checklist1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PdcSrcs_Checklist1.Name = "Xl_PdcSrcs_Checklist1"
        Me.Xl_PdcSrcs_Checklist1.ReadOnly = True
        Me.Xl_PdcSrcs_Checklist1.RowTemplate.Height = 40
        Me.Xl_PdcSrcs_Checklist1.Size = New System.Drawing.Size(516, 598)
        Me.Xl_PdcSrcs_Checklist1.TabIndex = 0
        '
        'Frm_PdcFchs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1549, 710)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DateTimePickerTo)
        Me.Controls.Add(Me.DateTimePickerFrom)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Frm_PdcFchs"
        Me.Text = "Distribució horaria comandes"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_PdcFchs1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_PdcSrcs_Checklist1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents DateTimePickerFrom As DateTimePicker
    Friend WithEvents DateTimePickerTo As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Xl_PdcSrcs_Checklist1 As Xl_PdcSrcs_Checklist
    Friend WithEvents Xl_PdcFchs1 As Xl_PdcFchs
End Class
