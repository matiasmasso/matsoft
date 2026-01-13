<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Zona
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
        Me.components = New System.ComponentModel.Container()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageGral = New System.Windows.Forms.TabPage()
        Me.ComboBoxExportCod = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CheckBoxSplitByComarcas = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TextBoxPaisNom = New System.Windows.Forms.TextBox()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabPageCits = New System.Windows.Forms.TabPage()
        Me.TabPageReps = New System.Windows.Forms.TabPage()
        Me.TabPageTrp = New System.Windows.Forms.TabPage()
        Me.PictureBoxExcelTrpTarifas = New System.Windows.Forms.PictureBox()
        Me.CheckBoxTrpHideUnactive = New System.Windows.Forms.CheckBox()
        Me.DataGridViewTrp = New System.Windows.Forms.DataGridView()
        Me.Xl_Langs1 = New Xl_Langs()
        Me.Xl_Pais1 = New Xl_Pais()
        Me.Xl_Image1 = New Xl_Image()
        Me.Xl_Locations1 = New Xl_Locations()
        Me.Xl_RepProducts1 = New Xl_RepProducts()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_LookupProvincia1 = New Xl_LookupProvincia()
        Me.TabControl1.SuspendLayout()
        Me.TabPageGral.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabPageCits.SuspendLayout()
        Me.TabPageReps.SuspendLayout()
        Me.TabPageTrp.SuspendLayout()
        CType(Me.PictureBoxExcelTrpTarifas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewTrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_Locations1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageGral)
        Me.TabControl1.Controls.Add(Me.TabPageCits)
        Me.TabControl1.Controls.Add(Me.TabPageReps)
        Me.TabControl1.Controls.Add(Me.TabPageTrp)
        Me.TabControl1.Location = New System.Drawing.Point(2, 11)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(452, 282)
        Me.TabControl1.TabIndex = 11
        '
        'TabPageGral
        '
        Me.TabPageGral.Controls.Add(Me.Xl_LookupProvincia1)
        Me.TabPageGral.Controls.Add(Me.Label1)
        Me.TabPageGral.Controls.Add(Me.ComboBoxExportCod)
        Me.TabPageGral.Controls.Add(Me.Label3)
        Me.TabPageGral.Controls.Add(Me.Label6)
        Me.TabPageGral.Controls.Add(Me.Xl_Langs1)
        Me.TabPageGral.Controls.Add(Me.CheckBoxSplitByComarcas)
        Me.TabPageGral.Controls.Add(Me.Label5)
        Me.TabPageGral.Controls.Add(Me.Panel1)
        Me.TabPageGral.Controls.Add(Me.TextBoxPaisNom)
        Me.TabPageGral.Controls.Add(Me.Xl_Pais1)
        Me.TabPageGral.Controls.Add(Me.Xl_Image1)
        Me.TabPageGral.Controls.Add(Me.TextBoxNom)
        Me.TabPageGral.Controls.Add(Me.Label2)
        Me.TabPageGral.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGral.Name = "TabPageGral"
        Me.TabPageGral.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGral.Size = New System.Drawing.Size(444, 256)
        Me.TabPageGral.TabIndex = 0
        Me.TabPageGral.Text = "GENERAL"
        '
        'ComboBoxExportCod
        '
        Me.ComboBoxExportCod.FormattingEnabled = True
        Me.ComboBoxExportCod.ItemHeight = 13
        Me.ComboBoxExportCod.Items.AddRange(New Object() {"(sel.leccionar codi import/export)", "Nacional", "CEE", "Fora de la CEE"})
        Me.ComboBoxExportCod.Location = New System.Drawing.Point(73, 96)
        Me.ComboBoxExportCod.Name = "ComboBoxExportCod"
        Me.ComboBoxExportCod.Size = New System.Drawing.Size(364, 21)
        Me.ComboBoxExportCod.TabIndex = 70
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 99)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 69
        Me.Label3.Text = "Export"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(9, 125)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 16)
        Me.Label6.TabIndex = 68
        Me.Label6.Text = "Idioma:"
        '
        'CheckBoxSplitByComarcas
        '
        Me.CheckBoxSplitByComarcas.AutoSize = True
        Me.CheckBoxSplitByComarcas.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxSplitByComarcas.Location = New System.Drawing.Point(6, 150)
        Me.CheckBoxSplitByComarcas.Name = "CheckBoxSplitByComarcas"
        Me.CheckBoxSplitByComarcas.Size = New System.Drawing.Size(128, 17)
        Me.CheckBoxSplitByComarcas.TabIndex = 4
        Me.CheckBoxSplitByComarcas.Text = "Dividir per comarques"
        Me.CheckBoxSplitByComarcas.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(8, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 16)
        Me.Label5.TabIndex = 65
        Me.Label5.Text = "Pais:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(3, 222)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(438, 31)
        Me.Panel1.TabIndex = 64
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(219, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 6
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(330, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 5
        Me.ButtonOk.Text = "Acceptar"
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
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'TextBoxPaisNom
        '
        Me.TextBoxPaisNom.Location = New System.Drawing.Point(73, 18)
        Me.TextBoxPaisNom.Name = "TextBoxPaisNom"
        Me.TextBoxPaisNom.ReadOnly = True
        Me.TextBoxPaisNom.Size = New System.Drawing.Size(364, 20)
        Me.TextBoxPaisNom.TabIndex = 60
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Location = New System.Drawing.Point(73, 44)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(364, 20)
        Me.TextBoxNom.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 16)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Nom:"
        '
        'TabPageCits
        '
        Me.TabPageCits.Controls.Add(Me.Xl_Locations1)
        Me.TabPageCits.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCits.Name = "TabPageCits"
        Me.TabPageCits.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageCits.Size = New System.Drawing.Size(444, 256)
        Me.TabPageCits.TabIndex = 1
        Me.TabPageCits.Text = "POBLACIONS"
        '
        'TabPageReps
        '
        Me.TabPageReps.Controls.Add(Me.Xl_RepProducts1)
        Me.TabPageReps.Location = New System.Drawing.Point(4, 22)
        Me.TabPageReps.Name = "TabPageReps"
        Me.TabPageReps.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageReps.Size = New System.Drawing.Size(444, 256)
        Me.TabPageReps.TabIndex = 2
        Me.TabPageReps.Text = "REPS"
        '
        'TabPageTrp
        '
        Me.TabPageTrp.Controls.Add(Me.PictureBoxExcelTrpTarifas)
        Me.TabPageTrp.Controls.Add(Me.CheckBoxTrpHideUnactive)
        Me.TabPageTrp.Controls.Add(Me.DataGridViewTrp)
        Me.TabPageTrp.Location = New System.Drawing.Point(4, 22)
        Me.TabPageTrp.Name = "TabPageTrp"
        Me.TabPageTrp.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageTrp.Size = New System.Drawing.Size(444, 256)
        Me.TabPageTrp.TabIndex = 3
        Me.TabPageTrp.Text = "TRANSPORTISTES"
        Me.TabPageTrp.UseVisualStyleBackColor = True
        '
        'PictureBoxExcelTrpTarifas
        '
        Me.PictureBoxExcelTrpTarifas.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxExcelTrpTarifas.Image = Global.Mat.Net.My.Resources.Resources.Excel
        Me.PictureBoxExcelTrpTarifas.Location = New System.Drawing.Point(404, 4)
        Me.PictureBoxExcelTrpTarifas.Name = "PictureBoxExcelTrpTarifas"
        Me.PictureBoxExcelTrpTarifas.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxExcelTrpTarifas.TabIndex = 4
        Me.PictureBoxExcelTrpTarifas.TabStop = False
        Me.PictureBoxExcelTrpTarifas.Tag = "Comparativa tarifes"
        '
        'CheckBoxTrpHideUnactive
        '
        Me.CheckBoxTrpHideUnactive.AutoSize = True
        Me.CheckBoxTrpHideUnactive.Checked = True
        Me.CheckBoxTrpHideUnactive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxTrpHideUnactive.Location = New System.Drawing.Point(4, 4)
        Me.CheckBoxTrpHideUnactive.Name = "CheckBoxTrpHideUnactive"
        Me.CheckBoxTrpHideUnactive.Size = New System.Drawing.Size(182, 17)
        Me.CheckBoxTrpHideUnactive.TabIndex = 3
        Me.CheckBoxTrpHideUnactive.Text = "Ocultar transportistes desactivats"
        Me.CheckBoxTrpHideUnactive.UseVisualStyleBackColor = True
        '
        'DataGridViewTrp
        '
        Me.DataGridViewTrp.AllowUserToAddRows = False
        Me.DataGridViewTrp.AllowUserToDeleteRows = False
        Me.DataGridViewTrp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewTrp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewTrp.Location = New System.Drawing.Point(3, 27)
        Me.DataGridViewTrp.Name = "DataGridViewTrp"
        Me.DataGridViewTrp.ReadOnly = True
        Me.DataGridViewTrp.Size = New System.Drawing.Size(437, 112)
        Me.DataGridViewTrp.TabIndex = 2
        '
        'Xl_Langs1
        '
        Me.Xl_Langs1.Location = New System.Drawing.Point(73, 123)
        Me.Xl_Langs1.Name = "Xl_Langs1"
        Me.Xl_Langs1.Size = New System.Drawing.Size(61, 21)
        Me.Xl_Langs1.TabIndex = 3
        Me.Xl_Langs1.Value = Nothing
        '
        'Xl_Pais1
        '
        Me.Xl_Pais1.Country = Nothing
        Me.Xl_Pais1.FlagVisible = False
        Me.Xl_Pais1.Location = New System.Drawing.Point(48, 18)
        Me.Xl_Pais1.Name = "Xl_Pais1"
        Me.Xl_Pais1.Size = New System.Drawing.Size(60, 15)
        Me.Xl_Pais1.TabIndex = 57
        '
        'Xl_Image1
        '
        Me.Xl_Image1.Bitmap = Nothing
        Me.Xl_Image1.EmptyImageLabelText = ""
        Me.Xl_Image1.IsDirty = False
        Me.Xl_Image1.Location = New System.Drawing.Point(129, 200)
        Me.Xl_Image1.Margin = New System.Windows.Forms.Padding(2, 1, 1, 3)
        Me.Xl_Image1.Name = "Xl_Image1"
        Me.Xl_Image1.Size = New System.Drawing.Size(300, 250)
        Me.Xl_Image1.TabIndex = 51
        Me.Xl_Image1.ZipStream = Nothing
        '
        'Xl_Locations1
        '
        Me.Xl_Locations1.DisplayObsolets = False
        Me.Xl_Locations1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Locations1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Locations1.MouseIsDown = False
        Me.Xl_Locations1.Name = "Xl_Locations1"
        Me.Xl_Locations1.Size = New System.Drawing.Size(438, 250)
        Me.Xl_Locations1.TabIndex = 0
        '
        'Xl_RepProducts1
        '
        Me.Xl_RepProducts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_RepProducts1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_RepProducts1.Name = "Xl_RepProducts1"
        Me.Xl_RepProducts1.Size = New System.Drawing.Size(438, 250)
        Me.Xl_RepProducts1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 71
        Me.Label1.Text = "Provincia:"
        '
        'Xl_LookupProvincia1
        '
        Me.Xl_LookupProvincia1.AreaProvincia = Nothing
        Me.Xl_LookupProvincia1.Country = Nothing
        Me.Xl_LookupProvincia1.IsDirty = False
        Me.Xl_LookupProvincia1.Location = New System.Drawing.Point(73, 70)
        Me.Xl_LookupProvincia1.Name = "Xl_LookupProvincia1"
        Me.Xl_LookupProvincia1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProvincia1.ReadOnlyLookup = False
        Me.Xl_LookupProvincia1.SelMode = DTOArea.SelectModes.SelectAny
        Me.Xl_LookupProvincia1.Size = New System.Drawing.Size(364, 20)
        Me.Xl_LookupProvincia1.TabIndex = 1
        Me.Xl_LookupProvincia1.Value = Nothing
        '
        'Frm_Zona
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 293)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Zona"
        Me.Text = "Zona"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageGral.ResumeLayout(False)
        Me.TabPageGral.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.TabPageCits.ResumeLayout(False)
        Me.TabPageReps.ResumeLayout(False)
        Me.TabPageTrp.ResumeLayout(False)
        Me.TabPageTrp.PerformLayout()
        CType(Me.PictureBoxExcelTrpTarifas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewTrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_Locations1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageGral As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Pais1 As Xl_Pais
    Friend WithEvents Xl_Image1 As Xl_Image
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TabPageCits As System.Windows.Forms.TabPage
    Friend WithEvents TabPageReps As System.Windows.Forms.TabPage
    Friend WithEvents TabPageTrp As System.Windows.Forms.TabPage
    Friend WithEvents PictureBoxExcelTrpTarifas As System.Windows.Forms.PictureBox
    Friend WithEvents CheckBoxTrpHideUnactive As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridViewTrp As System.Windows.Forms.DataGridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxPaisNom As System.Windows.Forms.TextBox
    Friend WithEvents Xl_Locations1 As Xl_Locations
    Friend WithEvents Xl_RepProducts1 As Xl_RepProducts
    Friend WithEvents CheckBoxSplitByComarcas As CheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Xl_Langs1 As Xl_Langs
    Friend WithEvents ComboBoxExportCod As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Xl_LookupProvincia1 As Xl_LookupProvincia
    Friend WithEvents Label1 As Label
End Class
