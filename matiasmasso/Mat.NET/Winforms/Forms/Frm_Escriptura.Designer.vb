<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Escriptura
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
        Me.TextBoxTomo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CheckBoxIndefinit = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DateTimePickerFchTo = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DateTimePickerFchFrom = New System.Windows.Forms.DateTimePicker()
        Me.ComboBoxCodis = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxFolio = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxHoja = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.TextBoxInscripcio = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Xl_DocFile1 = New Winforms.Xl_DocFile()
        Me.Xl_ContactNotari = New Winforms.Xl_Contact2()
        Me.Xl_ContactRegistreMercantil = New Winforms.Xl_Contact2()
        Me.Xl_TextBoxNumProtocol = New Winforms.Xl_TextBoxNum()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxTomo
        '
        Me.TextBoxTomo.Location = New System.Drawing.Point(106, 322)
        Me.TextBoxTomo.MaxLength = 30
        Me.TextBoxTomo.Name = "TextBoxTomo"
        Me.TextBoxTomo.Size = New System.Drawing.Size(92, 20)
        Me.TextBoxTomo.TabIndex = 9
        Me.TextBoxTomo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(23, 325)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Tomo"
        '
        'CheckBoxIndefinit
        '
        Me.CheckBoxIndefinit.AutoSize = True
        Me.CheckBoxIndefinit.Location = New System.Drawing.Point(315, 232)
        Me.CheckBoxIndefinit.Name = "CheckBoxIndefinit"
        Me.CheckBoxIndefinit.Size = New System.Drawing.Size(72, 17)
        Me.CheckBoxIndefinit.TabIndex = 6
        Me.CheckBoxIndefinit.Text = "Indefinida"
        Me.CheckBoxIndefinit.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(103, 259)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "data de finalització"
        '
        'DateTimePickerFchTo
        '
        Me.DateTimePickerFchTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchTo.Location = New System.Drawing.Point(209, 256)
        Me.DateTimePickerFchTo.Name = "DateTimePickerFchTo"
        Me.DateTimePickerFchTo.Size = New System.Drawing.Size(100, 20)
        Me.DateTimePickerFchTo.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(103, 233)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "data de inici"
        '
        'DateTimePickerFchFrom
        '
        Me.DateTimePickerFchFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchFrom.Location = New System.Drawing.Point(209, 230)
        Me.DateTimePickerFchFrom.Name = "DateTimePickerFchFrom"
        Me.DateTimePickerFchFrom.Size = New System.Drawing.Size(100, 20)
        Me.DateTimePickerFchFrom.TabIndex = 5
        '
        'ComboBoxCodis
        '
        Me.ComboBoxCodis.FormattingEnabled = True
        Me.ComboBoxCodis.Location = New System.Drawing.Point(106, 3)
        Me.ComboBoxCodis.Name = "ComboBoxCodis"
        Me.ComboBoxCodis.Size = New System.Drawing.Size(301, 21)
        Me.ComboBoxCodis.TabIndex = 0
        Me.ComboBoxCodis.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Codi"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 185)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Notari"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 207)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Num.Protocol"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 448)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(784, 31)
        Me.Panel1.TabIndex = 56
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(565, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 27
        Me.ButtonCancel.TabStop = False
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(676, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 26
        Me.ButtonOk.Text = "ACCEPTAR"
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
        Me.ButtonDel.TabIndex = 28
        Me.ButtonDel.TabStop = False
        Me.ButtonDel.Text = "RETROCEDIR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 303)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(91, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Registre mercantil"
        '
        'TextBoxFolio
        '
        Me.TextBoxFolio.Location = New System.Drawing.Point(106, 348)
        Me.TextBoxFolio.MaxLength = 30
        Me.TextBoxFolio.Name = "TextBoxFolio"
        Me.TextBoxFolio.Size = New System.Drawing.Size(92, 20)
        Me.TextBoxFolio.TabIndex = 10
        Me.TextBoxFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(23, 351)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(29, 13)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Folio"
        '
        'TextBoxHoja
        '
        Me.TextBoxHoja.Location = New System.Drawing.Point(106, 374)
        Me.TextBoxHoja.MaxLength = 12
        Me.TextBoxHoja.Name = "TextBoxHoja"
        Me.TextBoxHoja.Size = New System.Drawing.Size(92, 20)
        Me.TextBoxHoja.TabIndex = 11
        Me.TextBoxHoja.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(23, 377)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(29, 13)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "Hoja"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Location = New System.Drawing.Point(106, 30)
        Me.TextBoxNom.MaxLength = 50
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(301, 20)
        Me.TextBoxNom.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(9, 33)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 13)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "Concepte"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(9, 61)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(72, 13)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "Observacions"
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Location = New System.Drawing.Point(106, 58)
        Me.TextBoxObs.MaxLength = 0
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(301, 114)
        Me.TextBoxObs.TabIndex = 2
        Me.TextBoxObs.TabStop = False
        '
        'TextBoxInscripcio
        '
        Me.TextBoxInscripcio.Location = New System.Drawing.Point(106, 403)
        Me.TextBoxInscripcio.MaxLength = 30
        Me.TextBoxInscripcio.Name = "TextBoxInscripcio"
        Me.TextBoxInscripcio.Size = New System.Drawing.Size(92, 20)
        Me.TextBoxInscripcio.TabIndex = 12
        Me.TextBoxInscripcio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(23, 406)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(51, 13)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "inscripció"
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.IsInedit = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(430, 6)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 57
        '
        'Xl_ContactNotari
        '
        Me.Xl_ContactNotari.Contact = Nothing
        Me.Xl_ContactNotari.Emp = Nothing
        Me.Xl_ContactNotari.Location = New System.Drawing.Point(106, 178)
        Me.Xl_ContactNotari.Name = "Xl_ContactNotari"
        Me.Xl_ContactNotari.ReadOnly = False
        Me.Xl_ContactNotari.Size = New System.Drawing.Size(301, 20)
        Me.Xl_ContactNotari.TabIndex = 3
        '
        'Xl_ContactRegistreMercantil
        '
        Me.Xl_ContactRegistreMercantil.Contact = Nothing
        Me.Xl_ContactRegistreMercantil.Emp = Nothing
        Me.Xl_ContactRegistreMercantil.Location = New System.Drawing.Point(107, 296)
        Me.Xl_ContactRegistreMercantil.Name = "Xl_ContactRegistreMercantil"
        Me.Xl_ContactRegistreMercantil.ReadOnly = False
        Me.Xl_ContactRegistreMercantil.Size = New System.Drawing.Size(300, 20)
        Me.Xl_ContactRegistreMercantil.TabIndex = 8
        '
        'Xl_TextBoxNumProtocol
        '
        Me.Xl_TextBoxNumProtocol.Location = New System.Drawing.Point(107, 204)
        Me.Xl_TextBoxNumProtocol.Mat_FormatString = ""
        Me.Xl_TextBoxNumProtocol.Name = "Xl_TextBoxNumProtocol"
        Me.Xl_TextBoxNumProtocol.ReadOnly = False
        Me.Xl_TextBoxNumProtocol.Size = New System.Drawing.Size(91, 20)
        Me.Xl_TextBoxNumProtocol.TabIndex = 4
        Me.Xl_TextBoxNumProtocol.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Frm_Escriptura
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 479)
        Me.Controls.Add(Me.Xl_TextBoxNumProtocol)
        Me.Controls.Add(Me.Xl_ContactRegistreMercantil)
        Me.Controls.Add(Me.Xl_ContactNotari)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.TextBoxInscripcio)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.TextBoxHoja)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TextBoxFolio)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxTomo)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.CheckBoxIndefinit)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.DateTimePickerFchTo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.DateTimePickerFchFrom)
        Me.Controls.Add(Me.ComboBoxCodis)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Escriptura"
        Me.Text = "ESCRIPTURA"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxTomo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxIndefinit As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerFchTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerFchFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents ComboBoxCodis As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxFolio As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBoxHoja As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TextBoxObs As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxInscripcio As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Xl_DocFile1 As Winforms.Xl_DocFile
    Friend WithEvents Xl_ContactNotari As Winforms.Xl_Contact2
    Friend WithEvents Xl_ContactRegistreMercantil As Winforms.Xl_Contact2
    Friend WithEvents Xl_TextBoxNumProtocol As Xl_TextBoxNum
End Class
