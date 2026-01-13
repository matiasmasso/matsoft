<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_DistributionChannel
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
        Me.TextBoxNomEsp = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxNomCat = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxNomEng = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_ContactClasses1 = New Xl_ContactClasses()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.NumericUpDownConsumerPriority = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDownOrd = New System.Windows.Forms.NumericUpDown()
        Me.TextBoxNomPor = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Xl_ProductChannels1 = New Xl_ProductChannels()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Xl_CustomerDtos1 = New Xl_CustomerTarifaDtos()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_ContactClasses1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.NumericUpDownConsumerPriority, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownOrd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.Xl_ProductChannels1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxNomEsp
        '
        Me.TextBoxNomEsp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomEsp.Location = New System.Drawing.Point(237, 72)
        Me.TextBoxNomEsp.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxNomEsp.MaxLength = 50
        Me.TextBoxNomEsp.Name = "TextBoxNomEsp"
        Me.TextBoxNomEsp.Size = New System.Drawing.Size(780, 38)
        Me.TextBoxNomEsp.TabIndex = 60
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 768)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1083, 74)
        Me.Panel1.TabIndex = 58
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(499, 10)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(277, 57)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(795, 10)
        Me.ButtonOk.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(277, 57)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(16, 10)
        Me.ButtonDel.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(277, 57)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(59, 79)
        Me.Label1.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(133, 32)
        Me.Label1.TabIndex = 59
        Me.Label1.Text = "Espanyol"
        '
        'TextBoxNomCat
        '
        Me.TextBoxNomCat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomCat.Location = New System.Drawing.Point(237, 134)
        Me.TextBoxNomCat.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxNomCat.MaxLength = 50
        Me.TextBoxNomCat.Name = "TextBoxNomCat"
        Me.TextBoxNomCat.Size = New System.Drawing.Size(780, 38)
        Me.TextBoxNomCat.TabIndex = 62
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(59, 141)
        Me.Label2.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 32)
        Me.Label2.TabIndex = 61
        Me.Label2.Text = "Català"
        '
        'TextBoxNomEng
        '
        Me.TextBoxNomEng.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomEng.Location = New System.Drawing.Point(237, 196)
        Me.TextBoxNomEng.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxNomEng.MaxLength = 50
        Me.TextBoxNomEng.Name = "TextBoxNomEng"
        Me.TextBoxNomEng.Size = New System.Drawing.Size(780, 38)
        Me.TextBoxNomEng.TabIndex = 64
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(59, 203)
        Me.Label3.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 32)
        Me.Label3.TabIndex = 63
        Me.Label3.Text = "Anglès"
        '
        'Xl_ContactClasses1
        '
        Me.Xl_ContactClasses1.AllowUserToAddRows = False
        Me.Xl_ContactClasses1.AllowUserToDeleteRows = False
        Me.Xl_ContactClasses1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ContactClasses1.DisplayObsolets = False
        Me.Xl_ContactClasses1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ContactClasses1.Filter = Nothing
        Me.Xl_ContactClasses1.Location = New System.Drawing.Point(8, 7)
        Me.Xl_ContactClasses1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_ContactClasses1.MouseIsDown = False
        Me.Xl_ContactClasses1.Name = "Xl_ContactClasses1"
        Me.Xl_ContactClasses1.ReadOnly = True
        Me.Xl_ContactClasses1.Size = New System.Drawing.Size(1031, 658)
        Me.Xl_ContactClasses1.TabIndex = 65
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(16, 33)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1067, 730)
        Me.TabControl1.TabIndex = 66
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.NumericUpDownConsumerPriority)
        Me.TabPage1.Controls.Add(Me.NumericUpDownOrd)
        Me.TabPage1.Controls.Add(Me.TextBoxNomPor)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.TextBoxNomEng)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.TextBoxNomEsp)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.TextBoxNomCat)
        Me.TabPage1.Location = New System.Drawing.Point(10, 48)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TabPage1.Size = New System.Drawing.Size(1047, 672)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(379, 415)
        Me.Label6.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(446, 32)
        Me.Label6.TabIndex = 70
        Me.Label6.Text = "Ordre de publicació al consumidor"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(379, 362)
        Me.Label5.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(430, 32)
        Me.Label5.TabIndex = 69
        Me.Label5.Text = "Ordre d'aparició en estadístiques"
        '
        'NumericUpDownConsumerPriority
        '
        Me.NumericUpDownConsumerPriority.Location = New System.Drawing.Point(237, 410)
        Me.NumericUpDownConsumerPriority.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.NumericUpDownConsumerPriority.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.NumericUpDownConsumerPriority.Name = "NumericUpDownConsumerPriority"
        Me.NumericUpDownConsumerPriority.Size = New System.Drawing.Size(123, 38)
        Me.NumericUpDownConsumerPriority.TabIndex = 68
        '
        'NumericUpDownOrd
        '
        Me.NumericUpDownOrd.Location = New System.Drawing.Point(237, 348)
        Me.NumericUpDownOrd.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.NumericUpDownOrd.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.NumericUpDownOrd.Name = "NumericUpDownOrd"
        Me.NumericUpDownOrd.Size = New System.Drawing.Size(123, 38)
        Me.NumericUpDownOrd.TabIndex = 67
        '
        'TextBoxNomPor
        '
        Me.TextBoxNomPor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomPor.Location = New System.Drawing.Point(237, 258)
        Me.TextBoxNomPor.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxNomPor.MaxLength = 50
        Me.TextBoxNomPor.Name = "TextBoxNomPor"
        Me.TextBoxNomPor.Size = New System.Drawing.Size(780, 38)
        Me.TextBoxNomPor.TabIndex = 66
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(59, 265)
        Me.Label4.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(145, 32)
        Me.Label4.TabIndex = 65
        Me.Label4.Text = "Portuguès"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_ContactClasses1)
        Me.TabPage2.Location = New System.Drawing.Point(10, 48)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TabPage2.Size = New System.Drawing.Size(1047, 672)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Classes de contacte"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Xl_ProductChannels1)
        Me.TabPage4.Location = New System.Drawing.Point(10, 48)
        Me.TabPage4.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(1047, 672)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Gama de productes"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Xl_ProductChannels1
        '
        Me.Xl_ProductChannels1.AllowUserToAddRows = False
        Me.Xl_ProductChannels1.AllowUserToDeleteRows = False
        Me.Xl_ProductChannels1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ProductChannels1.DisplayObsolets = False
        Me.Xl_ProductChannels1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductChannels1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductChannels1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_ProductChannels1.MouseIsDown = False
        Me.Xl_ProductChannels1.Name = "Xl_ProductChannels1"
        Me.Xl_ProductChannels1.ReadOnly = True
        Me.Xl_ProductChannels1.Size = New System.Drawing.Size(1047, 672)
        Me.Xl_ProductChannels1.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.GroupBox2)
        Me.TabPage3.Location = New System.Drawing.Point(10, 48)
        Me.TabPage3.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TabPage3.Size = New System.Drawing.Size(1047, 672)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Marges comercials"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Xl_CustomerDtos1)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 41)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.GroupBox2.Size = New System.Drawing.Size(1013, 613)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Descompte sobre el PVP per calcular el preu de tarifa"
        '
        'Xl_CustomerDtos1
        '
        Me.Xl_CustomerDtos1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_CustomerDtos1.Location = New System.Drawing.Point(91, 45)
        Me.Xl_CustomerDtos1.Margin = New System.Windows.Forms.Padding(21, 17, 21, 17)
        Me.Xl_CustomerDtos1.Name = "Xl_CustomerDtos1"
        Me.Xl_CustomerDtos1.Size = New System.Drawing.Size(880, 537)
        Me.Xl_CustomerDtos1.TabIndex = 0
        '
        'Frm_DistributionChannel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1083, 842)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Name = "Frm_DistributionChannel"
        Me.Text = "Canal de distribució"
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_ContactClasses1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.NumericUpDownConsumerPriority, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownOrd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        CType(Me.Xl_ProductChannels1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TextBoxNomEsp As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxNomCat As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxNomEng As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Xl_ContactClasses1 As Xl_ContactClasses
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TextBoxNomPor As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Xl_CustomerDtos1 As Xl_CustomerTarifaDtos
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Xl_ProductChannels1 As Xl_ProductChannels
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents NumericUpDownConsumerPriority As NumericUpDown
    Friend WithEvents NumericUpDownOrd As NumericUpDown
End Class
