Public Partial Class Frm_Cobrament
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Dim DtoIban1 As DTOIban = New DTOIban()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.PictureBoxLogo = New System.Windows.Forms.PictureBox()
        Me.ButtonPrevious = New System.Windows.Forms.Button()
        Me.ButtonEnd = New System.Windows.Forms.Button()
        Me.ButtonNext = New System.Windows.Forms.Button()
        Me.TextBoxEnd = New System.Windows.Forms.TextBox()
        Me.TabPagePndSel = New System.Windows.Forms.TabPage()
        Me.Xl_Pnds_Select1 = New Xl_Pnds_Select()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageFpg = New System.Windows.Forms.TabPage()
        Me.CheckBoxDespeses = New System.Windows.Forms.CheckBox()
        Me.GroupBoxDespeses = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_AmtNominal = New Xl_Amount()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_AmtLiquid = New Xl_Amount()
        Me.Xl_AmtDespeses = New Xl_Amount()
        Me.Xl_Cobrament1 = New Xl_Cobrament()
        Me.TabPageEnd = New System.Windows.Forms.TabPage()
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPagePndSel.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageFpg.SuspendLayout()
        Me.GroupBoxDespeses.SuspendLayout()
        Me.TabPageEnd.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(15, 17)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(91, 20)
        Me.DateTimePicker1.TabIndex = 38
        '
        'TextBoxNom
        '
        Me.TextBoxNom.BackColor = System.Drawing.SystemColors.MenuBar
        Me.TextBoxNom.Location = New System.Drawing.Point(120, 17)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(357, 20)
        Me.TextBoxNom.TabIndex = 37
        Me.TextBoxNom.TabStop = False
        '
        'PictureBoxLogo
        '
        Me.PictureBoxLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxLogo.Location = New System.Drawing.Point(541, 17)
        Me.PictureBoxLogo.Name = "PictureBoxLogo"
        Me.PictureBoxLogo.Size = New System.Drawing.Size(150, 48)
        Me.PictureBoxLogo.TabIndex = 36
        Me.PictureBoxLogo.TabStop = False
        '
        'ButtonPrevious
        '
        Me.ButtonPrevious.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonPrevious.Enabled = False
        Me.ButtonPrevious.Location = New System.Drawing.Point(15, 431)
        Me.ButtonPrevious.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.ButtonPrevious.Name = "ButtonPrevious"
        Me.ButtonPrevious.Size = New System.Drawing.Size(96, 24)
        Me.ButtonPrevious.TabIndex = 33
        Me.ButtonPrevious.Text = "< ENRERA"
        '
        'ButtonEnd
        '
        Me.ButtonEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonEnd.Enabled = False
        Me.ButtonEnd.Location = New System.Drawing.Point(599, 431)
        Me.ButtonEnd.Name = "ButtonEnd"
        Me.ButtonEnd.Size = New System.Drawing.Size(96, 24)
        Me.ButtonEnd.TabIndex = 34
        Me.ButtonEnd.Text = "FI >>"
        '
        'ButtonNext
        '
        Me.ButtonNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonNext.Enabled = False
        Me.ButtonNext.Location = New System.Drawing.Point(495, 431)
        Me.ButtonNext.Name = "ButtonNext"
        Me.ButtonNext.Size = New System.Drawing.Size(96, 24)
        Me.ButtonNext.TabIndex = 35
        Me.ButtonNext.Text = "SEGÜENT >"
        '
        'TextBoxEnd
        '
        Me.TextBoxEnd.BackColor = System.Drawing.SystemColors.Info
        Me.TextBoxEnd.Location = New System.Drawing.Point(207, 152)
        Me.TextBoxEnd.Name = "TextBoxEnd"
        Me.TextBoxEnd.ReadOnly = True
        Me.TextBoxEnd.Size = New System.Drawing.Size(251, 20)
        Me.TextBoxEnd.TabIndex = 0
        Me.TextBoxEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TabPagePndSel
        '
        Me.TabPagePndSel.Controls.Add(Me.Xl_Pnds_Select1)
        Me.TabPagePndSel.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePndSel.Name = "TabPagePndSel"
        Me.TabPagePndSel.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePndSel.Size = New System.Drawing.Size(668, 351)
        Me.TabPagePndSel.TabIndex = 0
        Me.TabPagePndSel.Text = "SELECCIO"
        '
        'Xl_Pnds_Select1
        '
        Me.Xl_Pnds_Select1.Codi = DTOPnd.Codis.NotSet
        Me.Xl_Pnds_Select1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Pnds_Select1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Pnds_Select1.Name = "Xl_Pnds_Select1"
        Me.Xl_Pnds_Select1.Size = New System.Drawing.Size(662, 345)
        Me.Xl_Pnds_Select1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPagePndSel)
        Me.TabControl1.Controls.Add(Me.TabPageFpg)
        Me.TabControl1.Controls.Add(Me.TabPageEnd)
        Me.TabControl1.Location = New System.Drawing.Point(15, 50)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(676, 377)
        Me.TabControl1.TabIndex = 32
        '
        'TabPageFpg
        '
        Me.TabPageFpg.Controls.Add(Me.CheckBoxDespeses)
        Me.TabPageFpg.Controls.Add(Me.GroupBoxDespeses)
        Me.TabPageFpg.Controls.Add(Me.Xl_Cobrament1)
        Me.TabPageFpg.Location = New System.Drawing.Point(4, 22)
        Me.TabPageFpg.Name = "TabPageFpg"
        Me.TabPageFpg.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageFpg.Size = New System.Drawing.Size(668, 351)
        Me.TabPageFpg.TabIndex = 1
        Me.TabPageFpg.Text = "MODALITAT"
        '
        'CheckBoxDespeses
        '
        Me.CheckBoxDespeses.AutoSize = True
        Me.CheckBoxDespeses.Location = New System.Drawing.Point(142, 20)
        Me.CheckBoxDespeses.Name = "CheckBoxDespeses"
        Me.CheckBoxDespeses.Size = New System.Drawing.Size(98, 17)
        Me.CheckBoxDespeses.TabIndex = 1
        Me.CheckBoxDespeses.Text = "paga despeses"
        '
        'GroupBoxDespeses
        '
        Me.GroupBoxDespeses.Controls.Add(Me.Label3)
        Me.GroupBoxDespeses.Controls.Add(Me.Xl_AmtNominal)
        Me.GroupBoxDespeses.Controls.Add(Me.Label2)
        Me.GroupBoxDespeses.Controls.Add(Me.Label1)
        Me.GroupBoxDespeses.Controls.Add(Me.Xl_AmtLiquid)
        Me.GroupBoxDespeses.Controls.Add(Me.Xl_AmtDespeses)
        Me.GroupBoxDespeses.Location = New System.Drawing.Point(131, 21)
        Me.GroupBoxDespeses.Name = "GroupBoxDespeses"
        Me.GroupBoxDespeses.Size = New System.Drawing.Size(505, 65)
        Me.GroupBoxDespeses.TabIndex = 2
        Me.GroupBoxDespeses.TabStop = False
        Me.GroupBoxDespeses.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(227, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "nominal:"
        '
        'Xl_AmtNominal
        '
        Me.Xl_AmtNominal.Amt = Nothing
        Me.Xl_AmtNominal.Location = New System.Drawing.Point(173, 35)
        Me.Xl_AmtNominal.Name = "Xl_AmtNominal"
        Me.Xl_AmtNominal.Size = New System.Drawing.Size(96, 20)
        Me.Xl_AmtNominal.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(327, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "despeses:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(461, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "total:"
        '
        'Xl_AmtLiquid
        '
        Me.Xl_AmtLiquid.Amt = Nothing
        Me.Xl_AmtLiquid.Location = New System.Drawing.Point(391, 35)
        Me.Xl_AmtLiquid.Name = "Xl_AmtLiquid"
        Me.Xl_AmtLiquid.Size = New System.Drawing.Size(96, 20)
        Me.Xl_AmtLiquid.TabIndex = 1
        '
        'Xl_AmtDespeses
        '
        Me.Xl_AmtDespeses.Amt = Nothing
        Me.Xl_AmtDespeses.Location = New System.Drawing.Point(282, 35)
        Me.Xl_AmtDespeses.Name = "Xl_AmtDespeses"
        Me.Xl_AmtDespeses.Size = New System.Drawing.Size(96, 20)
        Me.Xl_AmtDespeses.TabIndex = 0
        '
        'Xl_Cobrament1
        '
        Me.Xl_Cobrament1.CodiFpg = Xl_Cobrament.Fpgs.Cash
        Me.Xl_Cobrament1.Location = New System.Drawing.Point(131, 95)
        Me.Xl_Cobrament1.Name = "Xl_Cobrament1"
        Me.Xl_Cobrament1.Size = New System.Drawing.Size(400, 200)
        Me.Xl_Cobrament1.TabIndex = 0
        DtoIban1.BankBranch = Nothing
        DtoIban1.Cod = DTOIban.Cods._NotSet
        DtoIban1.Digits = ""
        DtoIban1.DocFile = Nothing
        DtoIban1.fchApproved = New Date(CType(0, Long))
        DtoIban1.fchDownloaded = New Date(CType(0, Long))
        DtoIban1.fchFrom = New Date(CType(0, Long))
        DtoIban1.fchTo = New Date(CType(0, Long))
        DtoIban1.fchUploaded = New Date(CType(0, Long))
        DtoIban1.Format = DTOIban.Formats.NotSet
        DtoIban1.IbanStructure = Nothing
        DtoIban1.IsLoaded = False
        DtoIban1.IsNew = True
        DtoIban1.Status = DTOIban.StatusEnum.All
        DtoIban1.Titular = Nothing
        DtoIban1.UsrApproved = Nothing
        DtoIban1.UsrDownloaded = Nothing
        DtoIban1.UsrUploaded = Nothing
        Me.Xl_Cobrament1.XecIBAN = DtoIban1
        '
        'TabPageEnd
        '
        Me.TabPageEnd.Controls.Add(Me.TextBoxEnd)
        Me.TabPageEnd.Location = New System.Drawing.Point(4, 22)
        Me.TabPageEnd.Name = "TabPageEnd"
        Me.TabPageEnd.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageEnd.Size = New System.Drawing.Size(668, 351)
        Me.TabPageEnd.TabIndex = 2
        Me.TabPageEnd.Text = "FINAL"
        '
        'Frm_Cobrament
        '
        Me.ClientSize = New System.Drawing.Size(710, 471)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.PictureBoxLogo)
        Me.Controls.Add(Me.ButtonPrevious)
        Me.Controls.Add(Me.ButtonEnd)
        Me.Controls.Add(Me.ButtonNext)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Cobrament"
        Me.Text = "COBROS"
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPagePndSel.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageFpg.ResumeLayout(False)
        Me.TabPageFpg.PerformLayout()
        Me.GroupBoxDespeses.ResumeLayout(False)
        Me.GroupBoxDespeses.PerformLayout()
        Me.TabPageEnd.ResumeLayout(False)
        Me.TabPageEnd.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxLogo As System.Windows.Forms.PictureBox
    Friend WithEvents ButtonPrevious As System.Windows.Forms.Button
    Friend WithEvents ButtonEnd As System.Windows.Forms.Button
    Friend WithEvents ButtonNext As System.Windows.Forms.Button
    Friend WithEvents TextBoxEnd As System.Windows.Forms.TextBox
    Friend WithEvents TabPagePndSel As System.Windows.Forms.TabPage
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageFpg As System.Windows.Forms.TabPage
    Friend WithEvents TabPageEnd As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Cobrament1 As Xl_Cobrament
    Friend WithEvents CheckBoxDespeses As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBoxDespeses As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtNominal As Xl_Amount
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtLiquid As Xl_Amount
    Friend WithEvents Xl_AmtDespeses As Xl_Amount
    Friend WithEvents Xl_Pnds_Select1 As Xl_Pnds_Select
End Class
