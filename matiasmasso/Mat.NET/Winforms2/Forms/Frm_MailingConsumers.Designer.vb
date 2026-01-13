<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_MailingConsumers
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
        Me.ButtonAddArea = New System.Windows.Forms.Button()
        Me.Xl_Areas1 = New Xl_Areas()
        Me.Xl_LookupArea1 = New Xl_LookupArea()
        Me.Xl_Rols1 = New Xl_Rols()
        Me.Xl_Users1 = New Xl_Users()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Xl_LookupRol1 = New Xl_LookupRol()
        Me.ButtonAddRol = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        CType(Me.Xl_Areas1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_Users1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonAddArea
        '
        Me.ButtonAddArea.Enabled = False
        Me.ButtonAddArea.Location = New System.Drawing.Point(258, 19)
        Me.ButtonAddArea.Name = "ButtonAddArea"
        Me.ButtonAddArea.Size = New System.Drawing.Size(74, 21)
        Me.ButtonAddArea.TabIndex = 5
        Me.ButtonAddArea.Text = "afegir"
        Me.ButtonAddArea.UseVisualStyleBackColor = True
        '
        'Xl_Areas1
        '
        Me.Xl_Areas1.AllowUserToAddRows = False
        Me.Xl_Areas1.AllowUserToDeleteRows = False
        Me.Xl_Areas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Areas1.Filter = Nothing
        Me.Xl_Areas1.Location = New System.Drawing.Point(6, 45)
        Me.Xl_Areas1.Name = "Xl_Areas1"
        Me.Xl_Areas1.ReadOnly = True
        Me.Xl_Areas1.Size = New System.Drawing.Size(326, 123)
        Me.Xl_Areas1.TabIndex = 4
        '
        'Xl_LookupArea1
        '
        Me.Xl_LookupArea1.IsDirty = False
        Me.Xl_LookupArea1.Location = New System.Drawing.Point(6, 19)
        Me.Xl_LookupArea1.Name = "Xl_LookupArea1"
        Me.Xl_LookupArea1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupArea1.Size = New System.Drawing.Size(246, 20)
        Me.Xl_LookupArea1.TabIndex = 3
        Me.Xl_LookupArea1.Value = Nothing
        '
        'Xl_Rols1
        '
        Me.Xl_Rols1.Location = New System.Drawing.Point(8, 47)
        Me.Xl_Rols1.Name = "Xl_Rols1"
        Me.Xl_Rols1.Size = New System.Drawing.Size(326, 121)
        Me.Xl_Rols1.TabIndex = 1
        '
        'Xl_Users1
        '
        Me.Xl_Users1.AllowUserToAddRows = False
        Me.Xl_Users1.AllowUserToDeleteRows = False
        Me.Xl_Users1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Users1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Users1.Filter = Nothing
        Me.Xl_Users1.Location = New System.Drawing.Point(360, -2)
        Me.Xl_Users1.Name = "Xl_Users1"
        Me.Xl_Users1.ReadOnly = True
        Me.Xl_Users1.Size = New System.Drawing.Size(510, 409)
        Me.Xl_Users1.TabIndex = 0
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 410)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(870, 22)
        Me.StatusStrip1.TabIndex = 6
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(82, 17)
        Me.ToolStripStatusLabel1.Text = "nº destinataris"
        '
        'Xl_LookupRol1
        '
        Me.Xl_LookupRol1.IsDirty = False
        Me.Xl_LookupRol1.Location = New System.Drawing.Point(8, 21)
        Me.Xl_LookupRol1.Name = "Xl_LookupRol1"
        Me.Xl_LookupRol1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupRol1.Rol = Nothing
        Me.Xl_LookupRol1.Size = New System.Drawing.Size(246, 20)
        Me.Xl_LookupRol1.TabIndex = 7
        Me.Xl_LookupRol1.Value = Nothing
        '
        'ButtonAddRol
        '
        Me.ButtonAddRol.Enabled = False
        Me.ButtonAddRol.Location = New System.Drawing.Point(260, 21)
        Me.ButtonAddRol.Name = "ButtonAddRol"
        Me.ButtonAddRol.Size = New System.Drawing.Size(74, 21)
        Me.ButtonAddRol.TabIndex = 8
        Me.ButtonAddRol.Text = "afegir"
        Me.ButtonAddRol.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ButtonAddArea)
        Me.GroupBox1.Controls.Add(Me.Xl_Areas1)
        Me.GroupBox1.Controls.Add(Me.Xl_LookupArea1)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 195)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(340, 174)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "areas"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Xl_Rols1)
        Me.GroupBox2.Controls.Add(Me.Xl_LookupRol1)
        Me.GroupBox2.Controls.Add(Me.ButtonAddRol)
        Me.GroupBox2.Location = New System.Drawing.Point(14, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(340, 174)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "rols"
        '
        'Frm_MailingConsumers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(870, 432)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Xl_Users1)
        Me.Name = "Frm_MailingConsumers"
        Me.Text = "Destinataris Mailing"
        CType(Me.Xl_Areas1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_Users1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_Users1 As Xl_Users
    Friend WithEvents Xl_Rols1 As Xl_Rols
    Friend WithEvents Xl_LookupArea1 As Xl_LookupArea
    Friend WithEvents Xl_Areas1 As Xl_Areas
    Friend WithEvents ButtonAddArea As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Xl_LookupRol1 As Xl_LookupRol
    Friend WithEvents ButtonAddRol As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
End Class
