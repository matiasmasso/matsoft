<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_BancTraspas
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
        Dim DtoAmt1 As DTOAmt = New DTOAmt()
        Dim DtoAmt2 As DTOAmt = New DTOAmt()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ComboBoxBancEmissor = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ComboBoxBancReceptor = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_EurImport = New Winforms.Xl_Eur()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Xl_EurExpenses = New Winforms.Xl_Eur()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(112, 165)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePicker1.TabIndex = 52
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 171)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Data emissió:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 235)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(284, 31)
        Me.Panel1.TabIndex = 49
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(65, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(176, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ComboBoxBancEmissor
        '
        Me.ComboBoxBancEmissor.FormattingEnabled = True
        Me.ComboBoxBancEmissor.Location = New System.Drawing.Point(112, 58)
        Me.ComboBoxBancEmissor.Name = "ComboBoxBancEmissor"
        Me.ComboBoxBancEmissor.Size = New System.Drawing.Size(151, 21)
        Me.ComboBoxBancEmissor.TabIndex = 53
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "Banc emissor"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 13)
        Me.Label4.TabIndex = 58
        Me.Label4.Text = "Banc receptor"
        '
        'ComboBoxBancReceptor
        '
        Me.ComboBoxBancReceptor.FormattingEnabled = True
        Me.ComboBoxBancReceptor.Location = New System.Drawing.Point(112, 85)
        Me.ComboBoxBancReceptor.Name = "ComboBoxBancReceptor"
        Me.ComboBoxBancReceptor.Size = New System.Drawing.Size(151, 21)
        Me.ComboBoxBancReceptor.TabIndex = 57
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 60
        Me.Label3.Text = "Import"
        '
        'Xl_EurImport
        '
        DtoAmt1.Cur = Nothing
        DtoAmt1.Eur = New Decimal(New Integer() {0, 0, 0, 0})
        DtoAmt1.Val = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Xl_EurImport.Amt = DtoAmt1
        Me.Xl_EurImport.Location = New System.Drawing.Point(112, 113)
        Me.Xl_EurImport.Name = "Xl_EurImport"
        Me.Xl_EurImport.Size = New System.Drawing.Size(80, 20)
        Me.Xl_EurImport.TabIndex = 59
        Me.Xl_EurImport.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 144)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 13)
        Me.Label5.TabIndex = 62
        Me.Label5.Text = "Despeses"
        '
        'Xl_EurExpenses
        '
        DtoAmt2.Cur = Nothing
        DtoAmt2.Eur = New Decimal(New Integer() {0, 0, 0, 0})
        DtoAmt2.Val = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Xl_EurExpenses.Amt = DtoAmt2
        Me.Xl_EurExpenses.Location = New System.Drawing.Point(112, 139)
        Me.Xl_EurExpenses.Name = "Xl_EurExpenses"
        Me.Xl_EurExpenses.Size = New System.Drawing.Size(80, 20)
        Me.Xl_EurExpenses.TabIndex = 61
        Me.Xl_EurExpenses.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Frm_BancTraspas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 266)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Xl_EurExpenses)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_EurImport)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ComboBoxBancReceptor)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ComboBoxBancEmissor)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_BancTraspas"
        Me.Text = "Traspas entre comptes"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ComboBoxBancEmissor As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents ComboBoxBancReceptor As ComboBox
    Friend WithEvents Xl_EurImport As Xl_Eur
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Xl_EurExpenses As Xl_Eur
End Class
