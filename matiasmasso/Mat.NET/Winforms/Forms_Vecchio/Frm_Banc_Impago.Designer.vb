<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Banc_Impago
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
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
        Dim DtoAmt3 As DTOAmt = New DTOAmt()
        Dim DtoAmt4 As DTOAmt = New DTOAmt()
        Dim DtoAmt5 As DTOAmt = New DTOAmt()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.GroupBoxCondicions = New System.Windows.Forms.GroupBox()
        Me.PictureBoxBancLogo = New System.Windows.Forms.PictureBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CheckBoxMailxRebut = New System.Windows.Forms.CheckBox()
        Me.CheckBoxIvaMail = New System.Windows.Forms.CheckBox()
        Me.TextBoxCondsMail = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxMinim = New System.Windows.Forms.TextBox()
        Me.TextBoxTipus = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Xl_AmtTotal = New Winforms.Xl_Eur()
        Me.Xl_AmtCorreu = New Winforms.Xl_Eur()
        Me.Xl_AmtIVA = New Winforms.Xl_Eur()
        Me.Xl_AmtComisions = New Winforms.Xl_Eur()
        Me.Xl_AmtNominal = New Winforms.Xl_Eur()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.CheckBoxWarnReps = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBoxCondicions.SuspendLayout()
        CType(Me.PictureBoxBancLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 433)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(712, 31)
        Me.Panel1.TabIndex = 43
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(493, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Location = New System.Drawing.Point(604, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(617, 25)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(89, 20)
        Me.DateTimePicker1.TabIndex = 45
        '
        'GroupBoxCondicions
        '
        Me.GroupBoxCondicions.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxCondicions.Controls.Add(Me.PictureBoxBancLogo)
        Me.GroupBoxCondicions.Controls.Add(Me.Label6)
        Me.GroupBoxCondicions.Controls.Add(Me.CheckBoxMailxRebut)
        Me.GroupBoxCondicions.Controls.Add(Me.CheckBoxIvaMail)
        Me.GroupBoxCondicions.Controls.Add(Me.TextBoxCondsMail)
        Me.GroupBoxCondicions.Controls.Add(Me.Label7)
        Me.GroupBoxCondicions.Controls.Add(Me.Label8)
        Me.GroupBoxCondicions.Controls.Add(Me.TextBoxMinim)
        Me.GroupBoxCondicions.Controls.Add(Me.TextBoxTipus)
        Me.GroupBoxCondicions.Location = New System.Drawing.Point(6, 13)
        Me.GroupBoxCondicions.Name = "GroupBoxCondicions"
        Me.GroupBoxCondicions.Size = New System.Drawing.Size(458, 66)
        Me.GroupBoxCondicions.TabIndex = 44
        Me.GroupBoxCondicions.TabStop = False
        Me.GroupBoxCondicions.Text = "Condicions de data..."
        '
        'PictureBoxBancLogo
        '
        Me.PictureBoxBancLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxBancLogo.Location = New System.Drawing.Point(298, 12)
        Me.PictureBoxBancLogo.Name = "PictureBoxBancLogo"
        Me.PictureBoxBancLogo.Size = New System.Drawing.Size(150, 48)
        Me.PictureBoxBancLogo.TabIndex = 41
        Me.PictureBoxBancLogo.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(81, 22)
        Me.Label6.Margin = New System.Windows.Forms.Padding(1, 3, 3, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 40
        Me.Label6.Text = "Correu"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'CheckBoxMailxRebut
        '
        Me.CheckBoxMailxRebut.AutoSize = True
        Me.CheckBoxMailxRebut.Location = New System.Drawing.Point(129, 44)
        Me.CheckBoxMailxRebut.Margin = New System.Windows.Forms.Padding(1, 1, 3, 3)
        Me.CheckBoxMailxRebut.Name = "CheckBoxMailxRebut"
        Me.CheckBoxMailxRebut.Size = New System.Drawing.Size(107, 17)
        Me.CheckBoxMailxRebut.TabIndex = 39
        Me.CheckBoxMailxRebut.TabStop = False
        Me.CheckBoxMailxRebut.Text = "Correu per Rebut"
        '
        'CheckBoxIvaMail
        '
        Me.CheckBoxIvaMail.AutoSize = True
        Me.CheckBoxIvaMail.Location = New System.Drawing.Point(129, 29)
        Me.CheckBoxIvaMail.Margin = New System.Windows.Forms.Padding(1, 3, 3, 2)
        Me.CheckBoxIvaMail.Name = "CheckBoxIvaMail"
        Me.CheckBoxIvaMail.Size = New System.Drawing.Size(91, 17)
        Me.CheckBoxIvaMail.TabIndex = 38
        Me.CheckBoxIvaMail.TabStop = False
        Me.CheckBoxIvaMail.Text = "IVA en correu"
        '
        'TextBoxCondsMail
        '
        Me.TextBoxCondsMail.Location = New System.Drawing.Point(87, 39)
        Me.TextBoxCondsMail.Margin = New System.Windows.Forms.Padding(3, 1, 2, 3)
        Me.TextBoxCondsMail.Name = "TextBoxCondsMail"
        Me.TextBoxCondsMail.ReadOnly = True
        Me.TextBoxCondsMail.Size = New System.Drawing.Size(32, 20)
        Me.TextBoxCondsMail.TabIndex = 37
        Me.TextBoxCondsMail.TabStop = False
        Me.TextBoxCondsMail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(43, 22)
        Me.Label7.Margin = New System.Windows.Forms.Padding(1, 3, 2, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(34, 13)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "Minim"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(7, 22)
        Me.Label8.Margin = New System.Windows.Forms.Padding(3, 3, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(33, 13)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "Tipus"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextBoxMinim
        '
        Me.TextBoxMinim.Location = New System.Drawing.Point(46, 39)
        Me.TextBoxMinim.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.TextBoxMinim.Name = "TextBoxMinim"
        Me.TextBoxMinim.ReadOnly = True
        Me.TextBoxMinim.Size = New System.Drawing.Size(32, 20)
        Me.TextBoxMinim.TabIndex = 25
        Me.TextBoxMinim.TabStop = False
        Me.TextBoxMinim.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxTipus
        '
        Me.TextBoxTipus.Location = New System.Drawing.Point(7, 39)
        Me.TextBoxTipus.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.TextBoxTipus.Name = "TextBoxTipus"
        Me.TextBoxTipus.ReadOnly = True
        Me.TextBoxTipus.Size = New System.Drawing.Size(32, 20)
        Me.TextBoxTipus.TabIndex = 24
        Me.TextBoxTipus.TabStop = False
        Me.TextBoxTipus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Xl_AmtTotal)
        Me.GroupBox1.Controls.Add(Me.Xl_AmtCorreu)
        Me.GroupBox1.Controls.Add(Me.Xl_AmtIVA)
        Me.GroupBox1.Controls.Add(Me.Xl_AmtComisions)
        Me.GroupBox1.Controls.Add(Me.Xl_AmtNominal)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(332, 359)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(375, 66)
        Me.GroupBox1.TabIndex = 46
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Despeses"
        '
        'Xl_AmtTotal
        '
        DtoAmt1.Cur = Nothing
        DtoAmt1.Eur = New Decimal(New Integer() {0, 0, 0, 0})
        DtoAmt1.Val = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Xl_AmtTotal.Amt = DtoAmt1
        Me.Xl_AmtTotal.Enabled = False
        Me.Xl_AmtTotal.Location = New System.Drawing.Point(294, 40)
        Me.Xl_AmtTotal.Name = "Xl_AmtTotal"
        Me.Xl_AmtTotal.Size = New System.Drawing.Size(78, 20)
        Me.Xl_AmtTotal.TabIndex = 30
        Me.Xl_AmtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Xl_AmtCorreu
        '
        DtoAmt2.Cur = Nothing
        DtoAmt2.Eur = New Decimal(New Integer() {0, 0, 0, 0})
        DtoAmt2.Val = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Xl_AmtCorreu.Amt = DtoAmt2
        Me.Xl_AmtCorreu.Location = New System.Drawing.Point(228, 39)
        Me.Xl_AmtCorreu.Name = "Xl_AmtCorreu"
        Me.Xl_AmtCorreu.Size = New System.Drawing.Size(60, 20)
        Me.Xl_AmtCorreu.TabIndex = 29
        Me.Xl_AmtCorreu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Xl_AmtIVA
        '
        DtoAmt3.Cur = Nothing
        DtoAmt3.Eur = New Decimal(New Integer() {0, 0, 0, 0})
        DtoAmt3.Val = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Xl_AmtIVA.Amt = DtoAmt3
        Me.Xl_AmtIVA.Location = New System.Drawing.Point(162, 40)
        Me.Xl_AmtIVA.Name = "Xl_AmtIVA"
        Me.Xl_AmtIVA.Size = New System.Drawing.Size(60, 20)
        Me.Xl_AmtIVA.TabIndex = 28
        Me.Xl_AmtIVA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Xl_AmtComisions
        '
        DtoAmt4.Cur = Nothing
        DtoAmt4.Eur = New Decimal(New Integer() {0, 0, 0, 0})
        DtoAmt4.Val = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Xl_AmtComisions.Amt = DtoAmt4
        Me.Xl_AmtComisions.Location = New System.Drawing.Point(96, 40)
        Me.Xl_AmtComisions.Name = "Xl_AmtComisions"
        Me.Xl_AmtComisions.Size = New System.Drawing.Size(60, 20)
        Me.Xl_AmtComisions.TabIndex = 27
        Me.Xl_AmtComisions.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Xl_AmtNominal
        '
        DtoAmt5.Cur = Nothing
        DtoAmt5.Eur = New Decimal(New Integer() {0, 0, 0, 0})
        DtoAmt5.Val = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Xl_AmtNominal.Amt = DtoAmt5
        Me.Xl_AmtNominal.Enabled = False
        Me.Xl_AmtNominal.Location = New System.Drawing.Point(7, 40)
        Me.Xl_AmtNominal.Name = "Xl_AmtNominal"
        Me.Xl_AmtNominal.Size = New System.Drawing.Size(78, 20)
        Me.Xl_AmtNominal.TabIndex = 26
        Me.Xl_AmtNominal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(341, 21)
        Me.Label5.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(31, 13)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "Total"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(250, 22)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 13)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "Correu"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(198, 23)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(24, 13)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "IVA"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(102, 22)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Comisions"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(40, 21)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Nominal"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(6, 85)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(700, 268)
        Me.DataGridView1.TabIndex = 47
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(6, 359)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(320, 64)
        Me.CheckedListBox1.TabIndex = 48
        '
        'CheckBoxWarnReps
        '
        Me.CheckBoxWarnReps.AutoSize = True
        Me.CheckBoxWarnReps.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxWarnReps.Checked = True
        Me.CheckBoxWarnReps.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxWarnReps.Location = New System.Drawing.Point(611, 62)
        Me.CheckBoxWarnReps.Name = "CheckBoxWarnReps"
        Me.CheckBoxWarnReps.Size = New System.Drawing.Size(97, 17)
        Me.CheckBoxWarnReps.TabIndex = 49
        Me.CheckBoxWarnReps.Text = "Notificar a reps"
        Me.CheckBoxWarnReps.UseVisualStyleBackColor = True
        '
        'Frm_Banc_Impago
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(712, 464)
        Me.Controls.Add(Me.CheckBoxWarnReps)
        Me.Controls.Add(Me.CheckedListBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.GroupBoxCondicions)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Banc_Impago"
        Me.Text = "IMPAGATS"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBoxCondicions.ResumeLayout(False)
        Me.GroupBoxCondicions.PerformLayout()
        CType(Me.PictureBoxBancLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBoxCondicions As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBoxBancLogo As System.Windows.Forms.PictureBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxMailxRebut As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxIvaMail As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxCondsMail As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBoxMinim As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxTipus As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents CheckedListBox1 As System.Windows.Forms.CheckedListBox
    Friend WithEvents Xl_AmtTotal As Xl_Eur
    Friend WithEvents Xl_AmtCorreu As Xl_Eur
    Friend WithEvents Xl_AmtIVA As Xl_Eur
    Friend WithEvents Xl_AmtComisions As Xl_Eur
    Friend WithEvents Xl_AmtNominal As Xl_Eur
    Friend WithEvents CheckBoxWarnReps As CheckBox
End Class
