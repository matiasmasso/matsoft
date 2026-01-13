<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_Contact2_Rep
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Xl_Image1 = New Xl_Image
        Me.Xl_ContactFiscal = New Xl_Contact
        Me.CheckBoxFiscal = New System.Windows.Forms.CheckBox
        Me.ButtonTraspas = New System.Windows.Forms.Button
        Me.ButtonRepLiqs = New System.Windows.Forms.Button
        Me.Xl_IBAN1 = New Xl_Iban
        Me.Label6 = New System.Windows.Forms.Label
        Me.TextBoxCustomIRPF = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.ComboBoxIRPF = New System.Windows.Forms.ComboBox
        Me.CheckBoxIVA = New System.Windows.Forms.CheckBox
        Me.GroupBoxComisio = New System.Windows.Forms.GroupBox
        Me.TextBoxComReducida = New System.Windows.Forms.TextBox
        Me.TextBoxComStandard = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.CheckBoxBaja = New System.Windows.Forms.CheckBox
        Me.DateTimePickerBaja = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.DateTimePickerAlta = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBoxAlias = New System.Windows.Forms.TextBox
        Me.GroupBoxComisio.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_Image1
        '
        Me.Xl_Image1.Bitmap = Nothing
        Me.Xl_Image1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_Image1.EmptyImageLabelText = "imatge 350x400 px"
        Me.Xl_Image1.Location = New System.Drawing.Point(308, 0)
        Me.Xl_Image1.MaxHeight = 0
        Me.Xl_Image1.MaxWidth = 0
        Me.Xl_Image1.Name = "Xl_Image1"
        Me.Xl_Image1.Size = New System.Drawing.Size(350, 400)
        Me.Xl_Image1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Xl_Image1.TabIndex = 57
        Me.Xl_Image1.ZipStream = Nothing
        '
        'Xl_ContactFiscal
        '
        Me.Xl_ContactFiscal.Location = New System.Drawing.Point(307, 432)
        Me.Xl_ContactFiscal.Name = "Xl_ContactFiscal"
        Me.Xl_ContactFiscal.Size = New System.Drawing.Size(250, 20)
        Me.Xl_ContactFiscal.TabIndex = 56
        Me.Xl_ContactFiscal.Visible = False
        '
        'CheckBoxFiscal
        '
        Me.CheckBoxFiscal.AutoSize = True
        Me.CheckBoxFiscal.Location = New System.Drawing.Point(307, 409)
        Me.CheckBoxFiscal.Name = "CheckBoxFiscal"
        Me.CheckBoxFiscal.Size = New System.Drawing.Size(124, 17)
        Me.CheckBoxFiscal.TabIndex = 55
        Me.CheckBoxFiscal.Text = "rao social facturació:"
        Me.CheckBoxFiscal.UseVisualStyleBackColor = True
        '
        'ButtonTraspas
        '
        Me.ButtonTraspas.Location = New System.Drawing.Point(3, 233)
        Me.ButtonTraspas.Name = "ButtonTraspas"
        Me.ButtonTraspas.Size = New System.Drawing.Size(96, 24)
        Me.ButtonTraspas.TabIndex = 54
        Me.ButtonTraspas.Text = "TRASPAS"
        Me.ButtonTraspas.UseVisualStyleBackColor = True
        '
        'ButtonRepLiqs
        '
        Me.ButtonRepLiqs.Location = New System.Drawing.Point(158, 233)
        Me.ButtonRepLiqs.Name = "ButtonRepLiqs"
        Me.ButtonRepLiqs.Size = New System.Drawing.Size(96, 24)
        Me.ButtonRepLiqs.TabIndex = 53
        Me.ButtonRepLiqs.Text = "LIQUIDACIONS"
        Me.ButtonRepLiqs.UseVisualStyleBackColor = True
        '
        'Xl_IBAN1
        '
        Me.Xl_IBAN1.Location = New System.Drawing.Point(307, 474)
        Me.Xl_IBAN1.Name = "Xl_IBAN1"
        Me.Xl_IBAN1.Size = New System.Drawing.Size(250, 50)
        Me.Xl_IBAN1.TabIndex = 52
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(305, 455)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(112, 16)
        Me.Label6.TabIndex = 50
        Me.Label6.Text = "banc transferencias:"
        '
        'TextBoxCustomIRPF
        '
        Me.TextBoxCustomIRPF.Location = New System.Drawing.Point(213, 477)
        Me.TextBoxCustomIRPF.Name = "TextBoxCustomIRPF"
        Me.TextBoxCustomIRPF.Size = New System.Drawing.Size(32, 20)
        Me.TextBoxCustomIRPF.TabIndex = 68
        Me.TextBoxCustomIRPF.Text = "0"
        Me.TextBoxCustomIRPF.Visible = False
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(85, 477)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 16)
        Me.Label5.TabIndex = 67
        Me.Label5.Text = "IRPF:"
        '
        'ComboBoxIRPF
        '
        Me.ComboBoxIRPF.FormattingEnabled = True
        Me.ComboBoxIRPF.Items.AddRange(New Object() {"exento", "standard", "custom"})
        Me.ComboBoxIRPF.Location = New System.Drawing.Point(141, 477)
        Me.ComboBoxIRPF.Name = "ComboBoxIRPF"
        Me.ComboBoxIRPF.Size = New System.Drawing.Size(72, 21)
        Me.ComboBoxIRPF.TabIndex = 66
        Me.ComboBoxIRPF.Text = "standard"
        '
        'CheckBoxIVA
        '
        Me.CheckBoxIVA.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxIVA.Location = New System.Drawing.Point(85, 453)
        Me.CheckBoxIVA.Name = "CheckBoxIVA"
        Me.CheckBoxIVA.Size = New System.Drawing.Size(72, 16)
        Me.CheckBoxIVA.TabIndex = 65
        Me.CheckBoxIVA.Text = "IVA"
        '
        'GroupBoxComisio
        '
        Me.GroupBoxComisio.Controls.Add(Me.TextBoxComReducida)
        Me.GroupBoxComisio.Controls.Add(Me.TextBoxComStandard)
        Me.GroupBoxComisio.Controls.Add(Me.Label4)
        Me.GroupBoxComisio.Controls.Add(Me.Label3)
        Me.GroupBoxComisio.Location = New System.Drawing.Point(85, 355)
        Me.GroupBoxComisio.Name = "GroupBoxComisio"
        Me.GroupBoxComisio.Size = New System.Drawing.Size(168, 80)
        Me.GroupBoxComisio.TabIndex = 64
        Me.GroupBoxComisio.TabStop = False
        Me.GroupBoxComisio.Text = "Comisions:"
        '
        'TextBoxComReducida
        '
        Me.TextBoxComReducida.Location = New System.Drawing.Point(84, 41)
        Me.TextBoxComReducida.Name = "TextBoxComReducida"
        Me.TextBoxComReducida.Size = New System.Drawing.Size(32, 20)
        Me.TextBoxComReducida.TabIndex = 7
        '
        'TextBoxComStandard
        '
        Me.TextBoxComStandard.Location = New System.Drawing.Point(84, 19)
        Me.TextBoxComStandard.Name = "TextBoxComStandard"
        Me.TextBoxComStandard.Size = New System.Drawing.Size(32, 20)
        Me.TextBoxComStandard.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(24, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 16)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Reducida:"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(24, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 16)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Standard:"
        '
        'CheckBoxBaja
        '
        Me.CheckBoxBaja.Location = New System.Drawing.Point(85, 323)
        Me.CheckBoxBaja.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.CheckBoxBaja.Name = "CheckBoxBaja"
        Me.CheckBoxBaja.Size = New System.Drawing.Size(72, 16)
        Me.CheckBoxBaja.TabIndex = 63
        Me.CheckBoxBaja.Text = "Baja"
        '
        'DateTimePickerBaja
        '
        Me.DateTimePickerBaja.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerBaja.Location = New System.Drawing.Point(165, 323)
        Me.DateTimePickerBaja.Name = "DateTimePickerBaja"
        Me.DateTimePickerBaja.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePickerBaja.TabIndex = 62
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(101, 305)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 16)
        Me.Label2.TabIndex = 61
        Me.Label2.Text = "Alta:"
        '
        'DateTimePickerAlta
        '
        Me.DateTimePickerAlta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerAlta.Location = New System.Drawing.Point(165, 299)
        Me.DateTimePickerAlta.Name = "DateTimePickerAlta"
        Me.DateTimePickerAlta.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePickerAlta.TabIndex = 60
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(101, 283)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 16)
        Me.Label1.TabIndex = 59
        Me.Label1.Text = "Alias:"
        '
        'TextBoxAlias
        '
        Me.TextBoxAlias.Location = New System.Drawing.Point(165, 275)
        Me.TextBoxAlias.Name = "TextBoxAlias"
        Me.TextBoxAlias.Size = New System.Drawing.Size(88, 20)
        Me.TextBoxAlias.TabIndex = 58
        '
        'Xl_Contact2_Rep
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TextBoxCustomIRPF)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ComboBoxIRPF)
        Me.Controls.Add(Me.CheckBoxIVA)
        Me.Controls.Add(Me.GroupBoxComisio)
        Me.Controls.Add(Me.CheckBoxBaja)
        Me.Controls.Add(Me.DateTimePickerBaja)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePickerAlta)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxAlias)
        Me.Controls.Add(Me.Xl_Image1)
        Me.Controls.Add(Me.Xl_ContactFiscal)
        Me.Controls.Add(Me.CheckBoxFiscal)
        Me.Controls.Add(Me.ButtonTraspas)
        Me.Controls.Add(Me.ButtonRepLiqs)
        Me.Controls.Add(Me.Xl_IBAN1)
        Me.Controls.Add(Me.Label6)
        Me.Name = "Xl_Contact2_Rep"
        Me.Size = New System.Drawing.Size(661, 538)
        Me.GroupBoxComisio.ResumeLayout(False)
        Me.GroupBoxComisio.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_Image1 As Xl_Image
    Friend WithEvents Xl_ContactFiscal As Xl_Contact
    Friend WithEvents CheckBoxFiscal As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonTraspas As System.Windows.Forms.Button
    Friend WithEvents ButtonRepLiqs As System.Windows.Forms.Button
    Friend WithEvents Xl_IBAN1 As Xl_Iban
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCustomIRPF As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxIRPF As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBoxIVA As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBoxComisio As System.Windows.Forms.GroupBox
    Friend WithEvents TextBoxComReducida As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxComStandard As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxBaja As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePickerBaja As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerAlta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxAlias As System.Windows.Forms.TextBox

End Class
