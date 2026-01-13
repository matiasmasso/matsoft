<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_ElCorteIngles
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_ContactsCompanies = New Mat.Net.Xl_Contacts()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Xl_ElCorteInglesDepts1 = New Mat.Net.Xl_ElCorteInglesDepts()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_Contacts1 = New Mat.Net.Xl_Contacts()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.Xl_Years1 = New Mat.Net.Xl_Years()
        Me.Xl_TextboxSearchOrders = New Mat.Net.Xl_TextboxSearch()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBoxDepts = New System.Windows.Forms.ComboBox()
        Me.Xl_ECIPurchaseOrders1 = New Mat.Net.Xl_ECIPurchaseOrders()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.Xl_HoldingInvrpt1 = New Mat.Net.Xl_HoldingInvrpt()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.CheckBoxHideObsolets = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Xl_ElCorteInglesCataleg1 = New Mat.Net.Xl_ElCorteInglesCataleg()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.ButtonUploadCatalogUpdate = New System.Windows.Forms.Button()
        Me.Xl_TextboxSearchCataleg = New Mat.Net.Xl_TextboxSearch()
        Me.TabPage8 = New System.Windows.Forms.TabPage()
        Me.Xl_ElCorteInglesAlineamientoDisponibilidadLogs1 = New Mat.Net.Xl_ElCorteInglesAlineamientoDisponibilidadLogs()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Xl_ProgressBarEnhanced1 = New Mat.Net.Xl_ProgressBarEnhanced()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PlantillesExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExhauritsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DescatalogatsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportarNovaPlantillaDeModificacioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IntegraciónStocksToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileStocksToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LabelCatalegCount = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Xl_ContactsCompanies, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.Xl_ElCorteInglesDepts1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_Contacts1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        CType(Me.Xl_ECIPurchaseOrders1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage6.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.Xl_ElCorteInglesCataleg1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage8.SuspendLayout()
        CType(Me.Xl_ElCorteInglesAlineamientoDisponibilidadLogs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_ProgressBarEnhanced1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Controls.Add(Me.TabPage7)
        Me.TabControl1.Controls.Add(Me.TabPage8)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(630, 309)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_ContactsCompanies)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(622, 283)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Empreses"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_ContactsCompanies
        '
        Me.Xl_ContactsCompanies.AllowUserToAddRows = False
        Me.Xl_ContactsCompanies.AllowUserToDeleteRows = False
        Me.Xl_ContactsCompanies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ContactsCompanies.DisplayObsolets = False
        Me.Xl_ContactsCompanies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ContactsCompanies.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ContactsCompanies.MouseIsDown = False
        Me.Xl_ContactsCompanies.Name = "Xl_ContactsCompanies"
        Me.Xl_ContactsCompanies.ReadOnly = True
        Me.Xl_ContactsCompanies.Size = New System.Drawing.Size(616, 277)
        Me.Xl_ContactsCompanies.TabIndex = 3
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Xl_ElCorteInglesDepts1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(622, 283)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Departaments"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Xl_ElCorteInglesDepts1
        '
        Me.Xl_ElCorteInglesDepts1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ElCorteInglesDepts1.DisplayObsolets = False
        Me.Xl_ElCorteInglesDepts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ElCorteInglesDepts1.Filter = Nothing
        Me.Xl_ElCorteInglesDepts1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ElCorteInglesDepts1.MouseIsDown = False
        Me.Xl_ElCorteInglesDepts1.Name = "Xl_ElCorteInglesDepts1"
        Me.Xl_ElCorteInglesDepts1.Size = New System.Drawing.Size(622, 283)
        Me.Xl_ElCorteInglesDepts1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_Contacts1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(622, 283)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Centres"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_Contacts1
        '
        Me.Xl_Contacts1.AllowUserToAddRows = False
        Me.Xl_Contacts1.AllowUserToDeleteRows = False
        Me.Xl_Contacts1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Contacts1.DisplayObsolets = False
        Me.Xl_Contacts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Contacts1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Contacts1.MouseIsDown = False
        Me.Xl_Contacts1.Name = "Xl_Contacts1"
        Me.Xl_Contacts1.ReadOnly = True
        Me.Xl_Contacts1.Size = New System.Drawing.Size(616, 277)
        Me.Xl_Contacts1.TabIndex = 2
        '
        'TabPage3
        '
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(622, 283)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Plataformes"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.Xl_Years1)
        Me.TabPage5.Controls.Add(Me.Xl_TextboxSearchOrders)
        Me.TabPage5.Controls.Add(Me.Label1)
        Me.TabPage5.Controls.Add(Me.ComboBoxDepts)
        Me.TabPage5.Controls.Add(Me.Xl_ECIPurchaseOrders1)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(622, 283)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Comandes"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(6, 5)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 9
        Me.Xl_Years1.Value = 0
        '
        'Xl_TextboxSearchOrders
        '
        Me.Xl_TextboxSearchOrders.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearchOrders.Location = New System.Drawing.Point(404, 5)
        Me.Xl_TextboxSearchOrders.Name = "Xl_TextboxSearchOrders"
        Me.Xl_TextboxSearchOrders.Size = New System.Drawing.Size(215, 20)
        Me.Xl_TextboxSearchOrders.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(181, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Departament:"
        '
        'ComboBoxDepts
        '
        Me.ComboBoxDepts.FormattingEnabled = True
        Me.ComboBoxDepts.Location = New System.Drawing.Point(258, 5)
        Me.ComboBoxDepts.Name = "ComboBoxDepts"
        Me.ComboBoxDepts.Size = New System.Drawing.Size(76, 21)
        Me.ComboBoxDepts.TabIndex = 6
        '
        'Xl_ECIPurchaseOrders1
        '
        Me.Xl_ECIPurchaseOrders1.AllowUserToAddRows = False
        Me.Xl_ECIPurchaseOrders1.AllowUserToDeleteRows = False
        Me.Xl_ECIPurchaseOrders1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ECIPurchaseOrders1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ECIPurchaseOrders1.Depto = Nothing
        Me.Xl_ECIPurchaseOrders1.DisplayObsolets = False
        Me.Xl_ECIPurchaseOrders1.Filter = Nothing
        Me.Xl_ECIPurchaseOrders1.Location = New System.Drawing.Point(0, 31)
        Me.Xl_ECIPurchaseOrders1.MouseIsDown = False
        Me.Xl_ECIPurchaseOrders1.Name = "Xl_ECIPurchaseOrders1"
        Me.Xl_ECIPurchaseOrders1.ReadOnly = True
        Me.Xl_ECIPurchaseOrders1.Size = New System.Drawing.Size(619, 248)
        Me.Xl_ECIPurchaseOrders1.TabIndex = 0
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.Xl_HoldingInvrpt1)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(622, 283)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "Stocks"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'Xl_HoldingInvrpt1
        '
        Me.Xl_HoldingInvrpt1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_HoldingInvrpt1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_HoldingInvrpt1.Name = "Xl_HoldingInvrpt1"
        Me.Xl_HoldingInvrpt1.Size = New System.Drawing.Size(622, 283)
        Me.Xl_HoldingInvrpt1.TabIndex = 0
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.LabelCatalegCount)
        Me.TabPage7.Controls.Add(Me.CheckBoxHideObsolets)
        Me.TabPage7.Controls.Add(Me.Panel2)
        Me.TabPage7.Controls.Add(Me.ButtonUploadCatalogUpdate)
        Me.TabPage7.Controls.Add(Me.Xl_TextboxSearchCataleg)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage7.Size = New System.Drawing.Size(622, 283)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "Cataleg"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'CheckBoxHideObsolets
        '
        Me.CheckBoxHideObsolets.AutoSize = True
        Me.CheckBoxHideObsolets.Checked = True
        Me.CheckBoxHideObsolets.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxHideObsolets.Location = New System.Drawing.Point(8, 9)
        Me.CheckBoxHideObsolets.Name = "CheckBoxHideObsolets"
        Me.CheckBoxHideObsolets.Size = New System.Drawing.Size(127, 17)
        Me.CheckBoxHideObsolets.TabIndex = 6
        Me.CheckBoxHideObsolets.Text = "amaga descatalogats"
        Me.CheckBoxHideObsolets.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.Xl_ElCorteInglesCataleg1)
        Me.Panel2.Controls.Add(Me.ProgressBar1)
        Me.Panel2.Location = New System.Drawing.Point(0, 32)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(622, 251)
        Me.Panel2.TabIndex = 5
        '
        'Xl_ElCorteInglesCataleg1
        '
        Me.Xl_ElCorteInglesCataleg1.AllowUserToAddRows = False
        Me.Xl_ElCorteInglesCataleg1.AllowUserToDeleteRows = False
        Me.Xl_ElCorteInglesCataleg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ElCorteInglesCataleg1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ElCorteInglesCataleg1.Filter = Nothing
        Me.Xl_ElCorteInglesCataleg1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ElCorteInglesCataleg1.Name = "Xl_ElCorteInglesCataleg1"
        Me.Xl_ElCorteInglesCataleg1.ReadOnly = True
        Me.Xl_ElCorteInglesCataleg1.Size = New System.Drawing.Size(622, 228)
        Me.Xl_ElCorteInglesCataleg1.TabIndex = 0
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 228)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(622, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 1
        '
        'ButtonUploadCatalogUpdate
        '
        Me.ButtonUploadCatalogUpdate.Image = Global.Mat.Net.My.Resources.Resources.upload_16
        Me.ButtonUploadCatalogUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonUploadCatalogUpdate.Location = New System.Drawing.Point(141, 5)
        Me.ButtonUploadCatalogUpdate.Name = "ButtonUploadCatalogUpdate"
        Me.ButtonUploadCatalogUpdate.Size = New System.Drawing.Size(119, 23)
        Me.ButtonUploadCatalogUpdate.TabIndex = 4
        Me.ButtonUploadCatalogUpdate.Text = "Pujar Excel"
        Me.ButtonUploadCatalogUpdate.UseVisualStyleBackColor = True
        '
        'Xl_TextboxSearchCataleg
        '
        Me.Xl_TextboxSearchCataleg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearchCataleg.Location = New System.Drawing.Point(423, 6)
        Me.Xl_TextboxSearchCataleg.Name = "Xl_TextboxSearchCataleg"
        Me.Xl_TextboxSearchCataleg.Size = New System.Drawing.Size(192, 20)
        Me.Xl_TextboxSearchCataleg.TabIndex = 1
        '
        'TabPage8
        '
        Me.TabPage8.Controls.Add(Me.Xl_ElCorteInglesAlineamientoDisponibilidadLogs1)
        Me.TabPage8.Location = New System.Drawing.Point(4, 22)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Size = New System.Drawing.Size(622, 283)
        Me.TabPage8.TabIndex = 7
        Me.TabPage8.Text = "Alineam.Stocks"
        Me.TabPage8.UseVisualStyleBackColor = True
        '
        'Xl_ElCorteInglesAlineamientoDisponibilidadLogs1
        '
        Me.Xl_ElCorteInglesAlineamientoDisponibilidadLogs1.AllowUserToAddRows = False
        Me.Xl_ElCorteInglesAlineamientoDisponibilidadLogs1.AllowUserToDeleteRows = False
        Me.Xl_ElCorteInglesAlineamientoDisponibilidadLogs1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Xl_ElCorteInglesAlineamientoDisponibilidadLogs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ElCorteInglesAlineamientoDisponibilidadLogs1.DisplayObsolets = False
        Me.Xl_ElCorteInglesAlineamientoDisponibilidadLogs1.Location = New System.Drawing.Point(49, 3)
        Me.Xl_ElCorteInglesAlineamientoDisponibilidadLogs1.MouseIsDown = False
        Me.Xl_ElCorteInglesAlineamientoDisponibilidadLogs1.Name = "Xl_ElCorteInglesAlineamientoDisponibilidadLogs1"
        Me.Xl_ElCorteInglesAlineamientoDisponibilidadLogs1.ReadOnly = True
        Me.Xl_ElCorteInglesAlineamientoDisponibilidadLogs1.Size = New System.Drawing.Size(240, 277)
        Me.Xl_ElCorteInglesAlineamientoDisponibilidadLogs1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Controls.Add(Me.Xl_ProgressBarEnhanced1)
        Me.Panel1.Location = New System.Drawing.Point(0, 42)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(630, 332)
        Me.Panel1.TabIndex = 1
        '
        'Xl_ProgressBarEnhanced1
        '
        Me.Xl_ProgressBarEnhanced1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_ProgressBarEnhanced1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Xl_ProgressBarEnhanced1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!)
        Me.Xl_ProgressBarEnhanced1.Location = New System.Drawing.Point(0, 309)
        Me.Xl_ProgressBarEnhanced1.Name = "Xl_ProgressBarEnhanced1"
        Me.Xl_ProgressBarEnhanced1.Size = New System.Drawing.Size(630, 23)
        Me.Xl_ProgressBarEnhanced1.TabIndex = 1
        Me.Xl_ProgressBarEnhanced1.TabStop = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(631, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(12, 20)
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PlantillesExcelToolStripMenuItem, Me.IntegraciónStocksToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'PlantillesExcelToolStripMenuItem
        '
        Me.PlantillesExcelToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExhauritsToolStripMenuItem, Me.DescatalogatsToolStripMenuItem, Me.ImportarNovaPlantillaDeModificacioToolStripMenuItem})
        Me.PlantillesExcelToolStripMenuItem.Image = Global.Mat.Net.My.Resources.Resources.Excel
        Me.PlantillesExcelToolStripMenuItem.Name = "PlantillesExcelToolStripMenuItem"
        Me.PlantillesExcelToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.PlantillesExcelToolStripMenuItem.Text = "Plantilles Excel"
        '
        'ExhauritsToolStripMenuItem
        '
        Me.ExhauritsToolStripMenuItem.Name = "ExhauritsToolStripMenuItem"
        Me.ExhauritsToolStripMenuItem.Size = New System.Drawing.Size(276, 22)
        Me.ExhauritsToolStripMenuItem.Text = "Exhaurits"
        '
        'DescatalogatsToolStripMenuItem
        '
        Me.DescatalogatsToolStripMenuItem.Name = "DescatalogatsToolStripMenuItem"
        Me.DescatalogatsToolStripMenuItem.Size = New System.Drawing.Size(276, 22)
        Me.DescatalogatsToolStripMenuItem.Text = "Descatalogats"
        '
        'ImportarNovaPlantillaDeModificacioToolStripMenuItem
        '
        Me.ImportarNovaPlantillaDeModificacioToolStripMenuItem.Name = "ImportarNovaPlantillaDeModificacioToolStripMenuItem"
        Me.ImportarNovaPlantillaDeModificacioToolStripMenuItem.Size = New System.Drawing.Size(276, 22)
        Me.ImportarNovaPlantillaDeModificacioToolStripMenuItem.Text = "Importar nova plantilla de Modificacio"
        '
        'IntegraciónStocksToolStripMenuItem
        '
        Me.IntegraciónStocksToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveFileStocksToolStripMenuItem})
        Me.IntegraciónStocksToolStripMenuItem.Name = "IntegraciónStocksToolStripMenuItem"
        Me.IntegraciónStocksToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.IntegraciónStocksToolStripMenuItem.Text = "Integració Stocks"
        '
        'SaveFileStocksToolStripMenuItem
        '
        Me.SaveFileStocksToolStripMenuItem.Name = "SaveFileStocksToolStripMenuItem"
        Me.SaveFileStocksToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
        Me.SaveFileStocksToolStripMenuItem.Text = "Desar fitxer"
        '
        'LabelCatalegCount
        '
        Me.LabelCatalegCount.AutoSize = True
        Me.LabelCatalegCount.Location = New System.Drawing.Point(286, 9)
        Me.LabelCatalegCount.Name = "LabelCatalegCount"
        Me.LabelCatalegCount.Size = New System.Drawing.Size(44, 13)
        Me.LabelCatalegCount.TabIndex = 7
        Me.LabelCatalegCount.Text = "Refs: ..."
        '
        'Frm_ElCorteIngles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(631, 373)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_ElCorteIngles"
        Me.Text = "El Corte Ingles"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.Xl_ContactsCompanies, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        CType(Me.Xl_ElCorteInglesDepts1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_Contacts1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        CType(Me.Xl_ECIPurchaseOrders1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.TabPage7.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.Xl_ElCorteInglesCataleg1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage8.ResumeLayout(False)
        CType(Me.Xl_ElCorteInglesAlineamientoDisponibilidadLogs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_ProgressBarEnhanced1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PlantillesExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DescatalogatsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Xl_Contacts1 As Xl_Contacts
    Friend WithEvents Xl_ContactsCompanies As Xl_Contacts
    Friend WithEvents IntegraciónStocksToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveFileStocksToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExhauritsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Xl_ElCorteInglesDepts1 As Xl_ElCorteInglesDepts
    Friend WithEvents ImportarNovaPlantillaDeModificacioToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents Xl_ECIPurchaseOrders1 As Xl_ECIPurchaseOrders
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents Xl_TextboxSearchOrders As Xl_TextboxSearch
    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBoxDepts As ComboBox
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents Xl_HoldingInvrpt1 As Xl_HoldingInvrpt
    Friend WithEvents TabPage7 As TabPage
    Friend WithEvents Xl_TextboxSearchCataleg As Xl_TextboxSearch
    Friend WithEvents Xl_ElCorteInglesCataleg1 As Xl_ElCorteInglesCataleg
    Friend WithEvents TabPage8 As TabPage
    Friend WithEvents Xl_ElCorteInglesAlineamientoDisponibilidadLogs1 As Xl_ElCorteInglesAlineamientoDisponibilidadLogs
    Friend WithEvents Xl_ProgressBarEnhanced1 As Xl_ProgressBarEnhanced
    Friend WithEvents ButtonUploadCatalogUpdate As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents CheckBoxHideObsolets As CheckBox
    Friend WithEvents LabelCatalegCount As Label
End Class
