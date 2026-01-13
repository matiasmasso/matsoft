<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Xec
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
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Xl_Iban1 = New Winforms.Xl_Iban()
        Me.TextBoxXecNum = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxLliurador = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_Amount1 = New Winforms.Xl_Amount()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxRebut = New System.Windows.Forms.TextBox()
        Me.TextBoxPresentat = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxVençut = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ComboBoxPresentacio = New System.Windows.Forms.ComboBox()
        Me.Xl_Pnds1 = New Winforms.Xl_Pnds()
        Me.DateTimePickerVto = New System.Windows.Forms.DateTimePicker()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_Pnds1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Enabled = False
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(92, 81)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePicker1.TabIndex = 52
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 441)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(538, 31)
        Me.Panel1.TabIndex = 49
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(319, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(430, 4)
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
        'Xl_Iban1
        '
        Me.Xl_Iban1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Iban1.Location = New System.Drawing.Point(276, 55)
        Me.Xl_Iban1.Name = "Xl_Iban1"
        Me.Xl_Iban1.Size = New System.Drawing.Size(250, 50)
        Me.Xl_Iban1.TabIndex = 53
        '
        'TextBoxXecNum
        '
        Me.TextBoxXecNum.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxXecNum.Location = New System.Drawing.Point(92, 55)
        Me.TextBoxXecNum.Name = "TextBoxXecNum"
        Me.TextBoxXecNum.Size = New System.Drawing.Size(174, 20)
        Me.TextBoxXecNum.TabIndex = 51
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Xec numero"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 85)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "Data recepció"
        '
        'TextBoxLliurador
        '
        Me.TextBoxLliurador.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxLliurador.Location = New System.Drawing.Point(92, 29)
        Me.TextBoxLliurador.Name = "TextBoxLliurador"
        Me.TextBoxLliurador.ReadOnly = True
        Me.TextBoxLliurador.Size = New System.Drawing.Size(434, 20)
        Me.TextBoxLliurador.TabIndex = 56
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "Lliurador"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 13)
        Me.Label4.TabIndex = 57
        Me.Label4.Text = "Import"
        '
        'Xl_Amount1
        '
        Me.Xl_Amount1.Amt = Nothing
        Me.Xl_Amount1.Location = New System.Drawing.Point(92, 108)
        Me.Xl_Amount1.Name = "Xl_Amount1"
        Me.Xl_Amount1.Size = New System.Drawing.Size(80, 20)
        Me.Xl_Amount1.TabIndex = 58
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 137)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(31, 13)
        Me.Label5.TabIndex = 59
        Me.Label5.Text = "rebut"
        '
        'TextBoxRebut
        '
        Me.TextBoxRebut.Location = New System.Drawing.Point(92, 134)
        Me.TextBoxRebut.Name = "TextBoxRebut"
        Me.TextBoxRebut.ReadOnly = True
        Me.TextBoxRebut.Size = New System.Drawing.Size(434, 20)
        Me.TextBoxRebut.TabIndex = 60
        '
        'TextBoxPresentat
        '
        Me.TextBoxPresentat.Location = New System.Drawing.Point(92, 160)
        Me.TextBoxPresentat.Name = "TextBoxPresentat"
        Me.TextBoxPresentat.ReadOnly = True
        Me.TextBoxPresentat.Size = New System.Drawing.Size(318, 20)
        Me.TextBoxPresentat.TabIndex = 62
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 163)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 13)
        Me.Label6.TabIndex = 61
        Me.Label6.Text = "presentat"
        '
        'TextBoxVençut
        '
        Me.TextBoxVençut.Location = New System.Drawing.Point(92, 186)
        Me.TextBoxVençut.Name = "TextBoxVençut"
        Me.TextBoxVençut.ReadOnly = True
        Me.TextBoxVençut.Size = New System.Drawing.Size(318, 20)
        Me.TextBoxVençut.TabIndex = 64
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 189)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 13)
        Me.Label7.TabIndex = 63
        Me.Label7.Text = "vençut"
        '
        'ComboBoxPresentacio
        '
        Me.ComboBoxPresentacio.FormattingEnabled = True
        Me.ComboBoxPresentacio.Location = New System.Drawing.Point(416, 159)
        Me.ComboBoxPresentacio.Name = "ComboBoxPresentacio"
        Me.ComboBoxPresentacio.Size = New System.Drawing.Size(110, 21)
        Me.ComboBoxPresentacio.TabIndex = 65
        '
        'Xl_Pnds1
        '
        Me.Xl_Pnds1.AllowUserToAddRows = False
        Me.Xl_Pnds1.AllowUserToDeleteRows = False
        Me.Xl_Pnds1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Pnds1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Pnds1.Filter = Nothing
        Me.Xl_Pnds1.Location = New System.Drawing.Point(15, 225)
        Me.Xl_Pnds1.Name = "Xl_Pnds1"
        Me.Xl_Pnds1.ReadOnly = True
        Me.Xl_Pnds1.Size = New System.Drawing.Size(511, 195)
        Me.Xl_Pnds1.TabIndex = 66
        '
        'DateTimePickerVto
        '
        Me.DateTimePickerVto.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerVto.Enabled = False
        Me.DateTimePickerVto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerVto.Location = New System.Drawing.Point(416, 186)
        Me.DateTimePickerVto.Name = "DateTimePickerVto"
        Me.DateTimePickerVto.Size = New System.Drawing.Size(110, 20)
        Me.DateTimePickerVto.TabIndex = 67
        '
        'Frm_Xec
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(538, 472)
        Me.Controls.Add(Me.DateTimePickerVto)
        Me.Controls.Add(Me.Xl_Pnds1)
        Me.Controls.Add(Me.ComboBoxPresentacio)
        Me.Controls.Add(Me.TextBoxVençut)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxPresentat)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxRebut)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Xl_Amount1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxLliurador)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_Iban1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.TextBoxXecNum)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Xec"
        Me.Text = "Xec rebut"
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_Pnds1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Xl_Iban1 As Xl_Iban
    Friend WithEvents TextBoxXecNum As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxLliurador As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Xl_Amount1 As Xl_Amount
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxRebut As TextBox
    Friend WithEvents TextBoxPresentat As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxVençut As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents ComboBoxPresentacio As ComboBox
    Friend WithEvents Xl_Pnds1 As Xl_Pnds
    Friend WithEvents DateTimePickerVto As DateTimePicker
End Class
