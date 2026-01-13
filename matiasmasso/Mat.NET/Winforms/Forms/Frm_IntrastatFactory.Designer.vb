<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_IntrastatFactory
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DtoYearMonth1 As DTOYearMonth = New DTOYearMonth()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_IntrastatFactory))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Xl_YearMonth1 = New Winforms.Xl_YearMonth()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonExport = New System.Windows.Forms.RadioButton()
        Me.RadioButtonImport = New System.Windows.Forms.RadioButton()
        Me.PanelWizardIntro = New System.Windows.Forms.Panel()
        Me.ButtonIntroNext = New System.Windows.Forms.Button()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_IntrastatPartidas1 = New Winforms.Xl_IntrastatPartidas()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.PanelWizardPartides = New System.Windows.Forms.Panel()
        Me.ButtonNext = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_IntrastatSkus1 = New Winforms.Xl_IntrastatSkus()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.PictureBoxDocfile = New System.Windows.Forms.PictureBox()
        Me.TextBoxPdfFilename = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Button_Exit = New System.Windows.Forms.Button()
        Me.ButtonUpload = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.ButtonBrowse = New System.Windows.Forms.Button()
        Me.TextBoxUrlIntrastat = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ButtonSaveFile = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.PanelWizardIntro.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Xl_IntrastatPartidas1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelWizardPartides.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_IntrastatSkus1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.PictureBoxDocfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.ImageList = Me.ImageList1
        Me.TabControl1.Location = New System.Drawing.Point(0, 24)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(798, 421)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Label5)
        Me.TabPage4.Controls.Add(Me.Xl_YearMonth1)
        Me.TabPage4.Controls.Add(Me.GroupBox1)
        Me.TabPage4.Controls.Add(Me.PanelWizardIntro)
        Me.TabPage4.Location = New System.Drawing.Point(4, 23)
        Me.TabPage4.Margin = New System.Windows.Forms.Padding(1)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(1)
        Me.TabPage4.Size = New System.Drawing.Size(790, 394)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Introducció"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(99, 169)
        Me.Label5.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 13)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "Periode:"
        '
        'Xl_YearMonth1
        '
        Me.Xl_YearMonth1.Location = New System.Drawing.Point(108, 196)
        Me.Xl_YearMonth1.Name = "Xl_YearMonth1"
        Me.Xl_YearMonth1.Size = New System.Drawing.Size(150, 19)
        Me.Xl_YearMonth1.TabIndex = 37
        DtoYearMonth1.Eur = New Decimal(New Integer() {0, 0, 0, 0})
        DtoYearMonth1.Month = DTOYearMonth.Months.May
        DtoYearMonth1.Year = 2019
        Me.Xl_YearMonth1.YearMonth = DtoYearMonth1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButtonExport)
        Me.GroupBox1.Controls.Add(Me.RadioButtonImport)
        Me.GroupBox1.Location = New System.Drawing.Point(92, 49)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(1)
        Me.GroupBox1.Size = New System.Drawing.Size(182, 95)
        Me.GroupBox1.TabIndex = 36
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Fluxe"
        '
        'RadioButtonExport
        '
        Me.RadioButtonExport.AutoSize = True
        Me.RadioButtonExport.Location = New System.Drawing.Point(16, 52)
        Me.RadioButtonExport.Margin = New System.Windows.Forms.Padding(1)
        Me.RadioButtonExport.Name = "RadioButtonExport"
        Me.RadioButtonExport.Size = New System.Drawing.Size(140, 17)
        Me.RadioButtonExport.TabIndex = 1
        Me.RadioButtonExport.TabStop = True
        Me.RadioButtonExport.Text = "Expedició (exportacions)"
        Me.RadioButtonExport.UseVisualStyleBackColor = True
        '
        'RadioButtonImport
        '
        Me.RadioButtonImport.AutoSize = True
        Me.RadioButtonImport.Location = New System.Drawing.Point(16, 27)
        Me.RadioButtonImport.Margin = New System.Windows.Forms.Padding(1)
        Me.RadioButtonImport.Name = "RadioButtonImport"
        Me.RadioButtonImport.Size = New System.Drawing.Size(146, 17)
        Me.RadioButtonImport.TabIndex = 0
        Me.RadioButtonImport.TabStop = True
        Me.RadioButtonImport.Text = "Introducció (importacions)"
        Me.RadioButtonImport.UseVisualStyleBackColor = True
        '
        'PanelWizardIntro
        '
        Me.PanelWizardIntro.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelWizardIntro.Controls.Add(Me.ButtonIntroNext)
        Me.PanelWizardIntro.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelWizardIntro.Location = New System.Drawing.Point(1, 362)
        Me.PanelWizardIntro.Name = "PanelWizardIntro"
        Me.PanelWizardIntro.Size = New System.Drawing.Size(788, 31)
        Me.PanelWizardIntro.TabIndex = 35
        '
        'ButtonIntroNext
        '
        Me.ButtonIntroNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonIntroNext.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonIntroNext.Location = New System.Drawing.Point(679, 4)
        Me.ButtonIntroNext.Name = "ButtonIntroNext"
        Me.ButtonIntroNext.Size = New System.Drawing.Size(104, 24)
        Me.ButtonIntroNext.TabIndex = 3
        Me.ButtonIntroNext.Text = "Següent >"
        Me.ButtonIntroNext.UseVisualStyleBackColor = False
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_IntrastatPartidas1)
        Me.TabPage1.Controls.Add(Me.ProgressBar1)
        Me.TabPage1.Controls.Add(Me.PanelWizardPartides)
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(790, 394)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Partides"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_IntrastatPartidas1
        '
        Me.Xl_IntrastatPartidas1.AllowUserToAddRows = False
        Me.Xl_IntrastatPartidas1.AllowUserToDeleteRows = False
        Me.Xl_IntrastatPartidas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_IntrastatPartidas1.DisplayObsolets = False
        Me.Xl_IntrastatPartidas1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_IntrastatPartidas1.Filter = Nothing
        Me.Xl_IntrastatPartidas1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_IntrastatPartidas1.MouseIsDown = False
        Me.Xl_IntrastatPartidas1.Name = "Xl_IntrastatPartidas1"
        Me.Xl_IntrastatPartidas1.ReadOnly = True
        Me.Xl_IntrastatPartidas1.Size = New System.Drawing.Size(784, 345)
        Me.Xl_IntrastatPartidas1.TabIndex = 0
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(3, 348)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(784, 12)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 35
        '
        'PanelWizardPartides
        '
        Me.PanelWizardPartides.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelWizardPartides.Controls.Add(Me.ButtonNext)
        Me.PanelWizardPartides.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelWizardPartides.Location = New System.Drawing.Point(3, 360)
        Me.PanelWizardPartides.Name = "PanelWizardPartides"
        Me.PanelWizardPartides.Size = New System.Drawing.Size(784, 31)
        Me.PanelWizardPartides.TabIndex = 34
        '
        'ButtonNext
        '
        Me.ButtonNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonNext.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonNext.Location = New System.Drawing.Point(675, 4)
        Me.ButtonNext.Name = "ButtonNext"
        Me.ButtonNext.Size = New System.Drawing.Size(104, 24)
        Me.ButtonNext.TabIndex = 3
        Me.ButtonNext.Text = "Següent >"
        Me.ButtonNext.UseVisualStyleBackColor = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_IntrastatSkus1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(790, 394)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Productes"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_IntrastatSkus1
        '
        Me.Xl_IntrastatSkus1.AllowUserToAddRows = False
        Me.Xl_IntrastatSkus1.AllowUserToDeleteRows = False
        Me.Xl_IntrastatSkus1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_IntrastatSkus1.DisplayObsolets = False
        Me.Xl_IntrastatSkus1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_IntrastatSkus1.Filter = Nothing
        Me.Xl_IntrastatSkus1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_IntrastatSkus1.MouseIsDown = False
        Me.Xl_IntrastatSkus1.Name = "Xl_IntrastatSkus1"
        Me.Xl_IntrastatSkus1.ReadOnly = True
        Me.Xl_IntrastatSkus1.Size = New System.Drawing.Size(784, 388)
        Me.Xl_IntrastatSkus1.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.PictureBoxDocfile)
        Me.TabPage3.Controls.Add(Me.TextBoxPdfFilename)
        Me.TabPage3.Controls.Add(Me.Panel2)
        Me.TabPage3.Controls.Add(Me.ButtonUpload)
        Me.TabPage3.Controls.Add(Me.TextBox2)
        Me.TabPage3.Controls.Add(Me.ButtonBrowse)
        Me.TabPage3.Controls.Add(Me.TextBoxUrlIntrastat)
        Me.TabPage3.Controls.Add(Me.Label4)
        Me.TabPage3.Controls.Add(Me.ButtonSaveFile)
        Me.TabPage3.Controls.Add(Me.Label3)
        Me.TabPage3.Controls.Add(Me.Label2)
        Me.TabPage3.Controls.Add(Me.Label1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 23)
        Me.TabPage3.Margin = New System.Windows.Forms.Padding(1)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(1)
        Me.TabPage3.Size = New System.Drawing.Size(790, 394)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Registre"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'PictureBoxDocfile
        '
        Me.PictureBoxDocfile.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxDocfile.Location = New System.Drawing.Point(459, 13)
        Me.PictureBoxDocfile.Name = "PictureBoxDocfile"
        Me.PictureBoxDocfile.Size = New System.Drawing.Size(325, 320)
        Me.PictureBoxDocfile.TabIndex = 37
        Me.PictureBoxDocfile.TabStop = False
        '
        'TextBoxPdfFilename
        '
        Me.TextBoxPdfFilename.Location = New System.Drawing.Point(70, 208)
        Me.TextBoxPdfFilename.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBoxPdfFilename.Name = "TextBoxPdfFilename"
        Me.TextBoxPdfFilename.ReadOnly = True
        Me.TextBoxPdfFilename.Size = New System.Drawing.Size(221, 20)
        Me.TextBoxPdfFilename.TabIndex = 36
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel2.Controls.Add(Me.Button_Exit)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(1, 362)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(788, 31)
        Me.Panel2.TabIndex = 35
        '
        'Button_Exit
        '
        Me.Button_Exit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Exit.BackColor = System.Drawing.SystemColors.Control
        Me.Button_Exit.Location = New System.Drawing.Point(679, 4)
        Me.Button_Exit.Name = "Button_Exit"
        Me.Button_Exit.Size = New System.Drawing.Size(104, 24)
        Me.Button_Exit.TabIndex = 3
        Me.Button_Exit.Text = "Sortir"
        Me.Button_Exit.UseVisualStyleBackColor = False
        '
        'ButtonUpload
        '
        Me.ButtonUpload.Location = New System.Drawing.Point(291, 207)
        Me.ButtonUpload.Margin = New System.Windows.Forms.Padding(1)
        Me.ButtonUpload.Name = "ButtonUpload"
        Me.ButtonUpload.Size = New System.Drawing.Size(78, 21)
        Me.ButtonUpload.TabIndex = 8
        Me.ButtonUpload.Text = "Cercar"
        Me.ButtonUpload.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(70, 152)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(221, 20)
        Me.TextBox2.TabIndex = 7
        '
        'ButtonBrowse
        '
        Me.ButtonBrowse.Location = New System.Drawing.Point(291, 102)
        Me.ButtonBrowse.Margin = New System.Windows.Forms.Padding(1)
        Me.ButtonBrowse.Name = "ButtonBrowse"
        Me.ButtonBrowse.Size = New System.Drawing.Size(78, 22)
        Me.ButtonBrowse.TabIndex = 6
        Me.ButtonBrowse.Text = "Navegar"
        Me.ButtonBrowse.UseVisualStyleBackColor = True
        '
        'TextBoxUrlIntrastat
        '
        Me.TextBoxUrlIntrastat.Location = New System.Drawing.Point(70, 104)
        Me.TextBoxUrlIntrastat.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBoxUrlIntrastat.Name = "TextBoxUrlIntrastat"
        Me.TextBoxUrlIntrastat.Size = New System.Drawing.Size(221, 20)
        Me.TextBoxUrlIntrastat.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(55, 193)
        Me.Label4.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(123, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "4. pujar el pdf al Mat.Net"
        '
        'ButtonSaveFile
        '
        Me.ButtonSaveFile.Location = New System.Drawing.Point(291, 42)
        Me.ButtonSaveFile.Margin = New System.Windows.Forms.Padding(1)
        Me.ButtonSaveFile.Name = "ButtonSaveFile"
        Me.ButtonSaveFile.Size = New System.Drawing.Size(78, 21)
        Me.ButtonSaveFile.TabIndex = 3
        Me.ButtonSaveFile.Text = "Descarregar"
        Me.ButtonSaveFile.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(55, 138)
        Me.Label3.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(212, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "3. apuntar el Codigo Seguro de Verificación"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(55, 88)
        Me.Label2.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(184, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "2. pujar fitxer CSV a la web d'Hisenda"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(55, 44)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(205, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "1. descarregar fitxer a la carpeta C:\AEAT"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "empty.gif")
        Me.ImageList1.Images.SetKeyName(1, "warn.gif")
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(798, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(100, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'Frm_IntrastatFactory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(798, 445)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_IntrastatFactory"
        Me.Text = "Nou Intrastat"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.PanelWizardIntro.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.Xl_IntrastatPartidas1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelWizardPartides.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_IntrastatSkus1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.PictureBoxDocfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_IntrastatPartidas1 As Xl_IntrastatPartidas
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents PanelWizardPartides As Panel
    Friend WithEvents ButtonNext As Button
    Friend WithEvents Xl_IntrastatSkus1 As Xl_IntrastatSkus
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Label1 As Label
    Friend WithEvents ButtonUpload As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents ButtonBrowse As Button
    Friend WithEvents TextBoxUrlIntrastat As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents ButtonSaveFile As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Button_Exit As Button
    Friend WithEvents TextBoxPdfFilename As TextBox
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Label5 As Label
    Friend WithEvents Xl_YearMonth1 As Xl_YearMonth
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadioButtonExport As RadioButton
    Friend WithEvents RadioButtonImport As RadioButton
    Friend WithEvents PanelWizardIntro As Panel
    Friend WithEvents ButtonIntroNext As Button
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents PictureBoxDocfile As PictureBox
End Class
