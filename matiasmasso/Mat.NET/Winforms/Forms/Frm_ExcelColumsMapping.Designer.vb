<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_ExcelColumsMapping
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Xl_ExcelColumnsMapping1 = New Winforms.Xl_ExcelColumnsMapping()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.Xl_ExcelSheet1 = New Winforms.Xl_ExcelSheet()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.CheckBoxFirstRowHeaders = New System.Windows.Forms.CheckBox()
        CType(Me.Xl_ExcelColumnsMapping1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_ExcelSheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_ExcelColumnsMapping1
        '
        Me.Xl_ExcelColumnsMapping1.AllowUserToAddRows = False
        Me.Xl_ExcelColumnsMapping1.AllowUserToDeleteRows = False
        Me.Xl_ExcelColumnsMapping1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ExcelColumnsMapping1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ExcelColumnsMapping1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ExcelColumnsMapping1.Name = "Xl_ExcelColumnsMapping1"
        Me.Xl_ExcelColumnsMapping1.ReadOnly = True
        Me.Xl_ExcelColumnsMapping1.Size = New System.Drawing.Size(201, 376)
        Me.Xl_ExcelColumnsMapping1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 414)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(603, 31)
        Me.Panel1.TabIndex = 50
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(384, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Location = New System.Drawing.Point(495, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Xl_ExcelSheet1
        '
        Me.Xl_ExcelSheet1.AllowUserToAddRows = False
        Me.Xl_ExcelSheet1.AllowUserToDeleteRows = False
        Me.Xl_ExcelSheet1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ExcelSheet1.DisplayObsolets = False
        Me.Xl_ExcelSheet1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ExcelSheet1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ExcelSheet1.MouseIsDown = False
        Me.Xl_ExcelSheet1.Name = "Xl_ExcelSheet1"
        Me.Xl_ExcelSheet1.ReadOnly = True
        Me.Xl_ExcelSheet1.Size = New System.Drawing.Size(398, 376)
        Me.Xl_ExcelSheet1.TabIndex = 51
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 36)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_ExcelColumnsMapping1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_ExcelSheet1)
        Me.SplitContainer1.Size = New System.Drawing.Size(603, 376)
        Me.SplitContainer1.SplitterDistance = 201
        Me.SplitContainer1.TabIndex = 52
        '
        'CheckBoxFirstRowHeaders
        '
        Me.CheckBoxFirstRowHeaders.AutoSize = True
        Me.CheckBoxFirstRowHeaders.Checked = True
        Me.CheckBoxFirstRowHeaders.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxFirstRowHeaders.Location = New System.Drawing.Point(0, 5)
        Me.CheckBoxFirstRowHeaders.Name = "CheckBoxFirstRowHeaders"
        Me.CheckBoxFirstRowHeaders.Size = New System.Drawing.Size(218, 17)
        Me.CheckBoxFirstRowHeaders.TabIndex = 53
        Me.CheckBoxFirstRowHeaders.Text = "Capçaleres de columna a la primera linia "
        Me.CheckBoxFirstRowHeaders.UseVisualStyleBackColor = True
        '
        'Frm_ExcelColumsMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(603, 445)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.CheckBoxFirstRowHeaders)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_ExcelColumsMapping"
        Me.Text = "Mapeig columnes Excel"
        CType(Me.Xl_ExcelColumnsMapping1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_ExcelSheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_ExcelColumnsMapping1 As Xl_ExcelColumnsMapping
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents Xl_ExcelSheet1 As Xl_ExcelSheet
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents CheckBoxFirstRowHeaders As CheckBox
    Friend WithEvents Panel1 As Panel
End Class
