<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_SepaMe
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
        Me.PanelButtons = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Xl_DocFile1 = New Mat.Net.Xl_DocFile()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_Contact2Banc = New Mat.Net.Xl_Contact2()
        Me.Xl_Contact2Lliurador = New Mat.Net.Xl_Contact2()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxRef = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DateTimePickerFchFrom = New Mat.Net.Xl_DateTimePicker()
        Me.CheckBoxFchTo = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerFchTo = New Mat.Net.Xl_DateTimePicker()
        Me.Xl_UsrLog1 = New Mat.Net.Xl_UsrLog()
        Me.PanelButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelButtons.Controls.Add(Me.ButtonCancel)
        Me.PanelButtons.Controls.Add(Me.ButtonOk)
        Me.PanelButtons.Controls.Add(Me.ButtonDel)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(0, 428)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(800, 31)
        Me.PanelButtons.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(581, 4)
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
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(692, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.IsInedit = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(446, 3)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 46
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 54
        Me.Label1.Text = "Banc"
        '
        'Xl_Contact2Banc
        '
        Me.Xl_Contact2Banc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact2Banc.Contact = Nothing
        Me.Xl_Contact2Banc.Emp = Nothing
        Me.Xl_Contact2Banc.Location = New System.Drawing.Point(71, 27)
        Me.Xl_Contact2Banc.Name = "Xl_Contact2Banc"
        Me.Xl_Contact2Banc.ReadOnly = False
        Me.Xl_Contact2Banc.Size = New System.Drawing.Size(369, 20)
        Me.Xl_Contact2Banc.TabIndex = 55
        '
        'Xl_Contact2Lliurador
        '
        Me.Xl_Contact2Lliurador.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact2Lliurador.Contact = Nothing
        Me.Xl_Contact2Lliurador.Emp = Nothing
        Me.Xl_Contact2Lliurador.Location = New System.Drawing.Point(71, 53)
        Me.Xl_Contact2Lliurador.Name = "Xl_Contact2Lliurador"
        Me.Xl_Contact2Lliurador.ReadOnly = False
        Me.Xl_Contact2Lliurador.Size = New System.Drawing.Size(369, 20)
        Me.Xl_Contact2Lliurador.TabIndex = 57
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 56
        Me.Label2.Text = "Lliurador"
        '
        'TextBoxRef
        '
        Me.TextBoxRef.Location = New System.Drawing.Point(71, 79)
        Me.TextBoxRef.Name = "TextBoxRef"
        Me.TextBoxRef.Size = New System.Drawing.Size(369, 20)
        Me.TextBoxRef.TabIndex = 61
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 82)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 60
        Me.Label4.Text = "Referència"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 109)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(30, 13)
        Me.Label5.TabIndex = 62
        Me.Label5.Text = "Data"
        '
        'DateTimePickerFchFrom
        '
        Me.DateTimePickerFchFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchFrom.Location = New System.Drawing.Point(71, 106)
        Me.DateTimePickerFchFrom.Name = "DateTimePickerFchFrom"
        Me.DateTimePickerFchFrom.Size = New System.Drawing.Size(97, 20)
        Me.DateTimePickerFchFrom.TabIndex = 63
        '
        'CheckBoxFchTo
        '
        Me.CheckBoxFchTo.AutoSize = True
        Me.CheckBoxFchTo.Location = New System.Drawing.Point(15, 134)
        Me.CheckBoxFchTo.Name = "CheckBoxFchTo"
        Me.CheckBoxFchTo.Size = New System.Drawing.Size(66, 17)
        Me.CheckBoxFchTo.TabIndex = 64
        Me.CheckBoxFchTo.Text = "Caducat"
        Me.CheckBoxFchTo.UseVisualStyleBackColor = True
        '
        'DateTimePickerFchTo
        '
        Me.DateTimePickerFchTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchTo.Location = New System.Drawing.Point(99, 132)
        Me.DateTimePickerFchTo.Name = "DateTimePickerFchTo"
        Me.DateTimePickerFchTo.Size = New System.Drawing.Size(97, 20)
        Me.DateTimePickerFchTo.TabIndex = 65
        Me.DateTimePickerFchTo.Visible = False
        '
        'Xl_UsrLog1
        '
        Me.Xl_UsrLog1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_UsrLog1.Location = New System.Drawing.Point(6, 403)
        Me.Xl_UsrLog1.Name = "Xl_UsrLog1"
        Me.Xl_UsrLog1.ReadOnly = True
        Me.Xl_UsrLog1.Size = New System.Drawing.Size(434, 20)
        Me.Xl_UsrLog1.TabIndex = 66
        '
        'Frm_SepaMe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 459)
        Me.Controls.Add(Me.Xl_UsrLog1)
        Me.Controls.Add(Me.DateTimePickerFchTo)
        Me.Controls.Add(Me.CheckBoxFchTo)
        Me.Controls.Add(Me.DateTimePickerFchFrom)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxRef)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_Contact2Lliurador)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_Contact2Banc)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.PanelButtons)
        Me.Name = "Frm_SepaMe"
        Me.Text = "Mandato Sepa signat"
        Me.PanelButtons.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PanelButtons As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Xl_DocFile1 As Xl_DocFile
    Friend WithEvents Label1 As Label
    Friend WithEvents Xl_Contact2Banc As Xl_Contact2
    Friend WithEvents Xl_Contact2Lliurador As Xl_Contact2
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxRef As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents DateTimePickerFchFrom As Xl_DateTimePicker
    Friend WithEvents CheckBoxFchTo As CheckBox
    Friend WithEvents DateTimePickerFchTo As Xl_DateTimePicker
    Friend WithEvents Xl_UsrLog1 As Xl_UsrLog
End Class
