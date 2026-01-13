Public Partial Class Frm_Fiscal_LlibresOficials
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
        Me.ButtonRenum = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.NumericUpDownYea = New System.Windows.Forms.NumericUpDown()
        Me.TextBoxStatusBar = New System.Windows.Forms.TextBox()
        Me.ComboBoxMes = New System.Windows.Forms.ComboBox()
        Me.ButtonBalanç = New System.Windows.Forms.Button()
        Me.ButtonPGC = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.ButtonLlibreAlbs = New System.Windows.Forms.Button()
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonRenum
        '
        Me.ButtonRenum.Location = New System.Drawing.Point(39, 27)
        Me.ButtonRenum.Name = "ButtonRenum"
        Me.ButtonRenum.Size = New System.Drawing.Size(138, 32)
        Me.ButtonRenum.TabIndex = 1
        Me.ButtonRenum.Text = "RENUMERAR"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(279, 16)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "exercisi:"
        '
        'NumericUpDownYea
        '
        Me.NumericUpDownYea.Location = New System.Drawing.Point(327, 12)
        Me.NumericUpDownYea.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.NumericUpDownYea.Maximum = New Decimal(New Integer() {2100, 0, 0, 0})
        Me.NumericUpDownYea.Minimum = New Decimal(New Integer() {1985, 0, 0, 0})
        Me.NumericUpDownYea.Name = "NumericUpDownYea"
        Me.NumericUpDownYea.Size = New System.Drawing.Size(54, 20)
        Me.NumericUpDownYea.TabIndex = 4
        Me.NumericUpDownYea.Value = New Decimal(New Integer() {1985, 0, 0, 0})
        '
        'TextBoxStatusBar
        '
        Me.TextBoxStatusBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxStatusBar.BackColor = System.Drawing.SystemColors.MenuBar
        Me.TextBoxStatusBar.Location = New System.Drawing.Point(-1, 359)
        Me.TextBoxStatusBar.Name = "TextBoxStatusBar"
        Me.TextBoxStatusBar.Size = New System.Drawing.Size(407, 20)
        Me.TextBoxStatusBar.TabIndex = 9
        '
        'ComboBoxMes
        '
        Me.ComboBoxMes.FormattingEnabled = True
        Me.ComboBoxMes.Location = New System.Drawing.Point(327, 39)
        Me.ComboBoxMes.Name = "ComboBoxMes"
        Me.ComboBoxMes.Size = New System.Drawing.Size(54, 21)
        Me.ComboBoxMes.TabIndex = 20
        '
        'ButtonBalanç
        '
        Me.ButtonBalanç.Location = New System.Drawing.Point(39, 262)
        Me.ButtonBalanç.Name = "ButtonBalanç"
        Me.ButtonBalanç.Size = New System.Drawing.Size(138, 31)
        Me.ButtonBalanç.TabIndex = 21
        Me.ButtonBalanç.Text = "BALANÇ"
        '
        'ButtonPGC
        '
        Me.ButtonPGC.Location = New System.Drawing.Point(39, 300)
        Me.ButtonPGC.Name = "ButtonPGC"
        Me.ButtonPGC.Size = New System.Drawing.Size(138, 31)
        Me.ButtonPGC.TabIndex = 26
        Me.ButtonPGC.Text = "PLA GRAL.COMPTES"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(200, 262)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(63, 31)
        Me.Button1.TabIndex = 31
        Me.Button1.Text = "Excel"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(-1, 349)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(407, 11)
        Me.ProgressBar1.TabIndex = 34
        Me.ProgressBar1.Visible = False
        '
        'ButtonLlibreAlbs
        '
        Me.ButtonLlibreAlbs.Location = New System.Drawing.Point(39, 225)
        Me.ButtonLlibreAlbs.Name = "ButtonLlibreAlbs"
        Me.ButtonLlibreAlbs.Size = New System.Drawing.Size(138, 31)
        Me.ButtonLlibreAlbs.TabIndex = 37
        Me.ButtonLlibreAlbs.Text = "LLIBRE ALBARANS"
        '
        'Frm_Fiscal_LlibresOficials
        '
        Me.ClientSize = New System.Drawing.Size(404, 378)
        Me.Controls.Add(Me.ButtonLlibreAlbs)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ButtonPGC)
        Me.Controls.Add(Me.ButtonBalanç)
        Me.Controls.Add(Me.ComboBoxMes)
        Me.Controls.Add(Me.TextBoxStatusBar)
        Me.Controls.Add(Me.NumericUpDownYea)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ButtonRenum)
        Me.Name = "Frm_Fiscal_LlibresOficials"
        Me.Text = "LLIBRES OFICIALS"
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonRenum As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownYea As System.Windows.Forms.NumericUpDown
    Friend WithEvents TextBoxStatusBar As System.Windows.Forms.TextBox
    Friend WithEvents ComboBoxMes As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonBalanç As System.Windows.Forms.Button
    Friend WithEvents ButtonPGC As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents ButtonLlibreAlbs As System.Windows.Forms.Button
End Class
