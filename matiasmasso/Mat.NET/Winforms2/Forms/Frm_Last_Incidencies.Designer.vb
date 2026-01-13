<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Last_Incidencies
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
        Me.ComboBoxApertura = New System.Windows.Forms.ComboBox()
        Me.CheckBoxSrcProducte = New System.Windows.Forms.CheckBox()
        Me.CheckBoxSrcTransport = New System.Windows.Forms.CheckBox()
        Me.CheckBoxIncludeClosed = New System.Windows.Forms.CheckBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReposicionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Xl_Incidencies1 = New Mat.Net.Xl_IncidenciesOld()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.ComboBoxTancament = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBoxBrand = New System.Windows.Forms.ComboBox()
        Me.ComboBoxCategory = New System.Windows.Forms.ComboBox()
        Me.ComboBoxSku = New System.Windows.Forms.ComboBox()
        Me.ComboBoxCustomer = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBoxYear = New System.Windows.Forms.CheckBox()
        Me.Xl_Years1 = New Mat.Net.Xl_Years()
        Me.Xl_TextboxSearch1 = New Mat.Net.Xl_TextboxSearch()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ComboBoxApertura
        '
        Me.ComboBoxApertura.FormattingEnabled = True
        Me.ComboBoxApertura.Location = New System.Drawing.Point(521, 47)
        Me.ComboBoxApertura.Name = "ComboBoxApertura"
        Me.ComboBoxApertura.Size = New System.Drawing.Size(265, 21)
        Me.ComboBoxApertura.TabIndex = 3
        '
        'CheckBoxSrcProducte
        '
        Me.CheckBoxSrcProducte.AutoSize = True
        Me.CheckBoxSrcProducte.Checked = True
        Me.CheckBoxSrcProducte.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxSrcProducte.Location = New System.Drawing.Point(11, 31)
        Me.CheckBoxSrcProducte.Name = "CheckBoxSrcProducte"
        Me.CheckBoxSrcProducte.Size = New System.Drawing.Size(83, 17)
        Me.CheckBoxSrcProducte.TabIndex = 6
        Me.CheckBoxSrcProducte.Text = "de producte"
        Me.CheckBoxSrcProducte.UseVisualStyleBackColor = True
        '
        'CheckBoxSrcTransport
        '
        Me.CheckBoxSrcTransport.AutoSize = True
        Me.CheckBoxSrcTransport.Checked = True
        Me.CheckBoxSrcTransport.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxSrcTransport.Location = New System.Drawing.Point(11, 50)
        Me.CheckBoxSrcTransport.Name = "CheckBoxSrcTransport"
        Me.CheckBoxSrcTransport.Size = New System.Drawing.Size(82, 17)
        Me.CheckBoxSrcTransport.TabIndex = 7
        Me.CheckBoxSrcTransport.Text = "de transport"
        Me.CheckBoxSrcTransport.UseVisualStyleBackColor = True
        '
        'CheckBoxIncludeClosed
        '
        Me.CheckBoxIncludeClosed.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxIncludeClosed.AutoSize = True
        Me.CheckBoxIncludeClosed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxIncludeClosed.Checked = True
        Me.CheckBoxIncludeClosed.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxIncludeClosed.Location = New System.Drawing.Point(874, 51)
        Me.CheckBoxIncludeClosed.Name = "CheckBoxIncludeClosed"
        Me.CheckBoxIncludeClosed.Size = New System.Drawing.Size(184, 17)
        Me.CheckBoxIncludeClosed.TabIndex = 14
        Me.CheckBoxIncludeClosed.Text = "inclou les incidencies ja tancades"
        Me.CheckBoxIncludeClosed.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1070, 24)
        Me.MenuStrip1.TabIndex = 16
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripMenuItem, Me.ReposicionsToolStripMenuItem, Me.ToolStripMenuItem1})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'ReposicionsToolStripMenuItem
        '
        Me.ReposicionsToolStripMenuItem.Name = "ReposicionsToolStripMenuItem"
        Me.ReposicionsToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.ReposicionsToolStripMenuItem.Text = "Reposicions"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(137, 22)
        Me.ToolStripMenuItem1.Text = "Refresca"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Xl_Incidencies1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 98)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1070, 386)
        Me.Panel1.TabIndex = 18
        '
        'Xl_Incidencies1
        '
        Me.Xl_Incidencies1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Incidencies1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Incidencies1.Name = "Xl_Incidencies1"
        Me.Xl_Incidencies1.Size = New System.Drawing.Size(1070, 363)
        Me.Xl_Incidencies1.TabIndex = 5
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 363)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(1070, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'ComboBoxTancament
        '
        Me.ComboBoxTancament.FormattingEnabled = True
        Me.ComboBoxTancament.Location = New System.Drawing.Point(521, 68)
        Me.ComboBoxTancament.Name = "ComboBoxTancament"
        Me.ComboBoxTancament.Size = New System.Drawing.Size(265, 21)
        Me.ComboBoxTancament.TabIndex = 21
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(446, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Apertura"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(446, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Tancament"
        '
        'ComboBoxBrand
        '
        Me.ComboBoxBrand.FormattingEnabled = True
        Me.ComboBoxBrand.Location = New System.Drawing.Point(129, 24)
        Me.ComboBoxBrand.Name = "ComboBoxBrand"
        Me.ComboBoxBrand.Size = New System.Drawing.Size(265, 21)
        Me.ComboBoxBrand.TabIndex = 24
        '
        'ComboBoxCategory
        '
        Me.ComboBoxCategory.FormattingEnabled = True
        Me.ComboBoxCategory.Location = New System.Drawing.Point(129, 45)
        Me.ComboBoxCategory.Name = "ComboBoxCategory"
        Me.ComboBoxCategory.Size = New System.Drawing.Size(265, 21)
        Me.ComboBoxCategory.TabIndex = 25
        Me.ComboBoxCategory.Visible = False
        '
        'ComboBoxSku
        '
        Me.ComboBoxSku.FormattingEnabled = True
        Me.ComboBoxSku.Location = New System.Drawing.Point(129, 66)
        Me.ComboBoxSku.Name = "ComboBoxSku"
        Me.ComboBoxSku.Size = New System.Drawing.Size(265, 21)
        Me.ComboBoxSku.TabIndex = 26
        Me.ComboBoxSku.Visible = False
        '
        'ComboBoxCustomer
        '
        Me.ComboBoxCustomer.FormattingEnabled = True
        Me.ComboBoxCustomer.Location = New System.Drawing.Point(521, 26)
        Me.ComboBoxCustomer.Name = "ComboBoxCustomer"
        Me.ComboBoxCustomer.Size = New System.Drawing.Size(265, 21)
        Me.ComboBoxCustomer.TabIndex = 27
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(446, 31)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 13)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Client"
        '
        'CheckBoxYear
        '
        Me.CheckBoxYear.AutoSize = True
        Me.CheckBoxYear.Location = New System.Drawing.Point(845, 30)
        Me.CheckBoxYear.Name = "CheckBoxYear"
        Me.CheckBoxYear.Size = New System.Drawing.Size(44, 17)
        Me.CheckBoxYear.TabIndex = 30
        Me.CheckBoxYear.Text = "Any"
        Me.CheckBoxYear.UseVisualStyleBackColor = True
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Years1.Enabled = False
        Me.Xl_Years1.Location = New System.Drawing.Point(895, 26)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 29
        Me.Xl_Years1.Value = 0
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(845, 72)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(213, 20)
        Me.Xl_TextboxSearch1.TabIndex = 31
        '
        'Frm_Last_Incidencies
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1070, 487)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.CheckBoxYear)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ComboBoxCustomer)
        Me.Controls.Add(Me.ComboBoxSku)
        Me.Controls.Add(Me.ComboBoxCategory)
        Me.Controls.Add(Me.ComboBoxBrand)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBoxTancament)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.CheckBoxIncludeClosed)
        Me.Controls.Add(Me.CheckBoxSrcTransport)
        Me.Controls.Add(Me.CheckBoxSrcProducte)
        Me.Controls.Add(Me.ComboBoxApertura)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Last_Incidencies"
        Me.Text = "Ultimes Incidències"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBoxApertura As System.Windows.Forms.ComboBox
    Friend WithEvents Xl_Incidencies1 As Xl_IncidenciesOld
    Friend WithEvents CheckBoxSrcProducte As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxSrcTransport As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxIncludeClosed As System.Windows.Forms.CheckBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReposicionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ComboBoxTancament As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ComboBoxBrand As ComboBox
    Friend WithEvents ComboBoxCategory As ComboBox
    Friend WithEvents ComboBoxSku As ComboBox
    Friend WithEvents ComboBoxCustomer As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents CheckBoxYear As CheckBox
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
End Class
