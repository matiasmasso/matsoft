<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Cobrament_TransferenciaPrevia
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
        Me.Xl_Contact1 = New Mat.NET.Xl_Contact()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_Bancs_Select1 = New Mat.NET.Xl_Bancs_Select()
        Me.TextBoxConcepte = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.Xl_Amt1 = New Mat.Net.Xl_Amount()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_DocFile1 = New Mat.NET.Xl_DocFile()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_Contact1
        '
        Me.Xl_Contact1.Contact = Nothing
        Me.Xl_Contact1.Location = New System.Drawing.Point(440, 10)
        Me.Xl_Contact1.Name = "Xl_Contact1"
        Me.Xl_Contact1.Size = New System.Drawing.Size(291, 20)
        Me.Xl_Contact1.TabIndex = 2
        Me.Xl_Contact1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(378, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Client:"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(440, 36)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(100, 20)
        Me.DateTimePicker1.TabIndex = 4
        Me.DateTimePicker1.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(376, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "data:"
        '
        'Xl_Bancs_Select1
        '
        Me.Xl_Bancs_Select1.Banc = Nothing
        Me.Xl_Bancs_Select1.Location = New System.Drawing.Point(381, 63)
        Me.Xl_Bancs_Select1.Name = "Xl_Bancs_Select1"
        Me.Xl_Bancs_Select1.Size = New System.Drawing.Size(350, 341)
        Me.Xl_Bancs_Select1.TabIndex = 7
        '
        'TextBoxConcepte
        '
        Me.TextBoxConcepte.Location = New System.Drawing.Point(440, 410)
        Me.TextBoxConcepte.Name = "TextBoxConcepte"
        Me.TextBoxConcepte.Size = New System.Drawing.Size(291, 20)
        Me.TextBoxConcepte.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(378, 417)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Concepte:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 452)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(737, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(518, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 11
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(629, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 10
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Xl_Amt1
        '
        Me.Xl_Amt1.Amt = Nothing
        Me.Xl_Amt1.Location = New System.Drawing.Point(629, 36)
        Me.Xl_Amt1.Name = "Xl_Amt1"
        Me.Xl_Amt1.Size = New System.Drawing.Size(102, 20)
        Me.Xl_Amt1.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(566, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "import:"
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(12, 12)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 43
        '
        'Frm_Cobrament_TransferenciaPrevia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(737, 483)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_Amt1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxConcepte)
        Me.Controls.Add(Me.Xl_Bancs_Select1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_Contact1)
        Me.Name = "Frm_Cobrament_TransferenciaPrevia"
        Me.Text = "COBRAMENT TRANSFERÈNCIA PREVIA"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_Contact1 As Xl_Contact
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Xl_Bancs_Select1 As Xl_Bancs_Select
    Friend WithEvents TextBoxConcepte As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents Xl_Amt1 As Xl_Amount
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Xl_DocFile1 As Mat.NET.Xl_DocFile
End Class
