<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Mailing
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
        Me.ButtonExcel = New System.Windows.Forms.Button()
        Me.ButtonEtqs = New System.Windows.Forms.Button()
        Me.CheckBoxPrv = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNoSpam = New System.Windows.Forms.CheckBox()
        Me.ButtonTestCircular = New System.Windows.Forms.Button()
        Me.TextBoxUrl = New System.Windows.Forms.TextBox()
        Me.TextBoxSubject = New System.Windows.Forms.TextBox()
        Me.ButtonCircular = New System.Windows.Forms.Button()
        Me.CheckedListBoxZons = New System.Windows.Forms.CheckedListBox()
        Me.CheckBoxAllZons = New System.Windows.Forms.CheckBox()
        Me.CheckedListBoxTpas = New System.Windows.Forms.CheckedListBox()
        Me.CheckBoxAllTpas = New System.Windows.Forms.CheckBox()
        Me.GroupBoxPrint = New System.Windows.Forms.GroupBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ComboBoxTarifa = New System.Windows.Forms.ComboBox()
        Me.CheckBoxOnLineShops = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCliSalePoints = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCliMasters = New System.Windows.Forms.CheckBox()
        Me.CheckBoxClis = New System.Windows.Forms.CheckBox()
        Me.CheckBoxSalesmen = New System.Windows.Forms.CheckBox()
        Me.ButtonEmails = New System.Windows.Forms.Button()
        Me.CheckBoxReps = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ButtonAdrs = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ComboBoxLang = New System.Windows.Forms.ComboBox()
        Me.GroupBoxPrint.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonExcel
        '
        Me.ButtonExcel.Location = New System.Drawing.Point(64, 40)
        Me.ButtonExcel.Name = "ButtonExcel"
        Me.ButtonExcel.Size = New System.Drawing.Size(80, 24)
        Me.ButtonExcel.TabIndex = 10
        Me.ButtonExcel.Text = "EXCEL"
        '
        'ButtonEtqs
        '
        Me.ButtonEtqs.Location = New System.Drawing.Point(64, 16)
        Me.ButtonEtqs.Name = "ButtonEtqs"
        Me.ButtonEtqs.Size = New System.Drawing.Size(80, 24)
        Me.ButtonEtqs.TabIndex = 1
        Me.ButtonEtqs.Text = "ETIQUETES"
        '
        'CheckBoxPrv
        '
        Me.CheckBoxPrv.Location = New System.Drawing.Point(18, 197)
        Me.CheckBoxPrv.Name = "CheckBoxPrv"
        Me.CheckBoxPrv.Size = New System.Drawing.Size(136, 16)
        Me.CheckBoxPrv.TabIndex = 41
        Me.CheckBoxPrv.Text = "Proveidors"
        '
        'CheckBoxNoSpam
        '
        Me.CheckBoxNoSpam.Checked = True
        Me.CheckBoxNoSpam.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxNoSpam.Location = New System.Drawing.Point(18, 212)
        Me.CheckBoxNoSpam.Name = "CheckBoxNoSpam"
        Me.CheckBoxNoSpam.Size = New System.Drawing.Size(136, 33)
        Me.CheckBoxNoSpam.TabIndex = 40
        Me.CheckBoxNoSpam.Text = "descartar els que no volen rebre noticies"
        '
        'ButtonTestCircular
        '
        Me.ButtonTestCircular.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonTestCircular.Location = New System.Drawing.Point(754, 483)
        Me.ButtonTestCircular.Name = "ButtonTestCircular"
        Me.ButtonTestCircular.Size = New System.Drawing.Size(43, 20)
        Me.ButtonTestCircular.TabIndex = 39
        Me.ButtonTestCircular.Text = "TEST"
        '
        'TextBoxUrl
        '
        Me.TextBoxUrl.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxUrl.Location = New System.Drawing.Point(664, 483)
        Me.TextBoxUrl.Name = "TextBoxUrl"
        Me.TextBoxUrl.Size = New System.Drawing.Size(81, 20)
        Me.TextBoxUrl.TabIndex = 38
        '
        'TextBoxSubject
        '
        Me.TextBoxSubject.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSubject.Location = New System.Drawing.Point(171, 483)
        Me.TextBoxSubject.Name = "TextBoxSubject"
        Me.TextBoxSubject.Size = New System.Drawing.Size(486, 20)
        Me.TextBoxSubject.TabIndex = 37
        '
        'ButtonCircular
        '
        Me.ButtonCircular.Location = New System.Drawing.Point(80, 481)
        Me.ButtonCircular.Name = "ButtonCircular"
        Me.ButtonCircular.Size = New System.Drawing.Size(75, 22)
        Me.ButtonCircular.TabIndex = 36
        Me.ButtonCircular.Text = "E-CIRCULAR"
        '
        'CheckedListBoxZons
        '
        Me.CheckedListBoxZons.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckedListBoxZons.FormattingEnabled = True
        Me.CheckedListBoxZons.Location = New System.Drawing.Point(664, 201)
        Me.CheckedListBoxZons.Name = "CheckedListBoxZons"
        Me.CheckedListBoxZons.Size = New System.Drawing.Size(134, 274)
        Me.CheckedListBoxZons.TabIndex = 35
        Me.CheckedListBoxZons.Visible = False
        '
        'CheckBoxAllZons
        '
        Me.CheckBoxAllZons.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxAllZons.AutoSize = True
        Me.CheckBoxAllZons.Checked = True
        Me.CheckBoxAllZons.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxAllZons.Location = New System.Drawing.Point(664, 181)
        Me.CheckBoxAllZons.Name = "CheckBoxAllZons"
        Me.CheckBoxAllZons.Size = New System.Drawing.Size(100, 17)
        Me.CheckBoxAllZons.TabIndex = 34
        Me.CheckBoxAllZons.Text = "Totes les zones"
        '
        'CheckedListBoxTpas
        '
        Me.CheckedListBoxTpas.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckedListBoxTpas.FormattingEnabled = True
        Me.CheckedListBoxTpas.Location = New System.Drawing.Point(664, 37)
        Me.CheckedListBoxTpas.Name = "CheckedListBoxTpas"
        Me.CheckedListBoxTpas.Size = New System.Drawing.Size(132, 139)
        Me.CheckedListBoxTpas.TabIndex = 33
        Me.CheckedListBoxTpas.Visible = False
        '
        'CheckBoxAllTpas
        '
        Me.CheckBoxAllTpas.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxAllTpas.AutoSize = True
        Me.CheckBoxAllTpas.Checked = True
        Me.CheckBoxAllTpas.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxAllTpas.Location = New System.Drawing.Point(664, 14)
        Me.CheckBoxAllTpas.Name = "CheckBoxAllTpas"
        Me.CheckBoxAllTpas.Size = New System.Drawing.Size(112, 17)
        Me.CheckBoxAllTpas.TabIndex = 32
        Me.CheckBoxAllTpas.Text = "Totes les marques"
        '
        'GroupBoxPrint
        '
        Me.GroupBoxPrint.Controls.Add(Me.ButtonExcel)
        Me.GroupBoxPrint.Controls.Add(Me.ButtonEtqs)
        Me.GroupBoxPrint.Location = New System.Drawing.Point(12, 400)
        Me.GroupBoxPrint.Name = "GroupBoxPrint"
        Me.GroupBoxPrint.Size = New System.Drawing.Size(152, 72)
        Me.GroupBoxPrint.TabIndex = 31
        Me.GroupBoxPrint.TabStop = False
        Me.GroupBoxPrint.Text = "Sortida:"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(56, 16)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePicker1.TabIndex = 7
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ComboBoxTarifa)
        Me.GroupBox1.Controls.Add(Me.CheckBoxOnLineShops)
        Me.GroupBox1.Controls.Add(Me.CheckBoxCliSalePoints)
        Me.GroupBox1.Controls.Add(Me.CheckBoxCliMasters)
        Me.GroupBox1.Controls.Add(Me.CheckBoxClis)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 49)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(152, 142)
        Me.GroupBox1.TabIndex = 29
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Clients amb comandes desde:"
        '
        'ComboBoxTarifa
        '
        Me.ComboBoxTarifa.FormattingEnabled = True
        Me.ComboBoxTarifa.Items.AddRange(New Object() {"totes tarifes", "exclou TB", "nomes TB"})
        Me.ComboBoxTarifa.Location = New System.Drawing.Point(8, 115)
        Me.ComboBoxTarifa.Name = "ComboBoxTarifa"
        Me.ComboBoxTarifa.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxTarifa.TabIndex = 12
        '
        'CheckBoxOnLineShops
        '
        Me.CheckBoxOnLineShops.Location = New System.Drawing.Point(8, 96)
        Me.CheckBoxOnLineShops.Name = "CheckBoxOnLineShops"
        Me.CheckBoxOnLineShops.Size = New System.Drawing.Size(136, 16)
        Me.CheckBoxOnLineShops.TabIndex = 11
        Me.CheckBoxOnLineShops.Text = "Botigues On-Line"
        '
        'CheckBoxCliSalePoints
        '
        Me.CheckBoxCliSalePoints.Location = New System.Drawing.Point(8, 80)
        Me.CheckBoxCliSalePoints.Name = "CheckBoxCliSalePoints"
        Me.CheckBoxCliSalePoints.Size = New System.Drawing.Size(136, 16)
        Me.CheckBoxCliSalePoints.TabIndex = 10
        Me.CheckBoxCliSalePoints.Text = "Punts de venda"
        '
        'CheckBoxCliMasters
        '
        Me.CheckBoxCliMasters.Location = New System.Drawing.Point(8, 64)
        Me.CheckBoxCliMasters.Name = "CheckBoxCliMasters"
        Me.CheckBoxCliMasters.Size = New System.Drawing.Size(136, 16)
        Me.CheckBoxCliMasters.TabIndex = 9
        Me.CheckBoxCliMasters.Text = "Centrals de compres"
        '
        'CheckBoxClis
        '
        Me.CheckBoxClis.Location = New System.Drawing.Point(8, 48)
        Me.CheckBoxClis.Name = "CheckBoxClis"
        Me.CheckBoxClis.Size = New System.Drawing.Size(136, 16)
        Me.CheckBoxClis.TabIndex = 8
        Me.CheckBoxClis.Text = "Clients"
        '
        'CheckBoxSalesmen
        '
        Me.CheckBoxSalesmen.Location = New System.Drawing.Point(10, 30)
        Me.CheckBoxSalesmen.Name = "CheckBoxSalesmen"
        Me.CheckBoxSalesmen.Size = New System.Drawing.Size(136, 16)
        Me.CheckBoxSalesmen.TabIndex = 28
        Me.CheckBoxSalesmen.Text = "Comercials"
        '
        'ButtonEmails
        '
        Me.ButtonEmails.Location = New System.Drawing.Point(64, 40)
        Me.ButtonEmails.Name = "ButtonEmails"
        Me.ButtonEmails.Size = New System.Drawing.Size(80, 24)
        Me.ButtonEmails.TabIndex = 10
        Me.ButtonEmails.Text = "EMAILS"
        '
        'CheckBoxReps
        '
        Me.CheckBoxReps.Location = New System.Drawing.Point(10, 14)
        Me.CheckBoxReps.Name = "CheckBoxReps"
        Me.CheckBoxReps.Size = New System.Drawing.Size(136, 16)
        Me.CheckBoxReps.TabIndex = 27
        Me.CheckBoxReps.Text = "Representants"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ButtonEmails)
        Me.GroupBox2.Controls.Add(Me.ButtonAdrs)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 320)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(152, 72)
        Me.GroupBox2.TabIndex = 30
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Format:"
        '
        'ButtonAdrs
        '
        Me.ButtonAdrs.Location = New System.Drawing.Point(64, 16)
        Me.ButtonAdrs.Name = "ButtonAdrs"
        Me.ButtonAdrs.Size = New System.Drawing.Size(80, 24)
        Me.ButtonAdrs.TabIndex = 1
        Me.ButtonAdrs.Text = "ADREÇES"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(171, 14)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(484, 463)
        Me.DataGridView1.TabIndex = 42
        '
        'ComboBoxLang
        '
        Me.ComboBoxLang.FormattingEnabled = True
        Me.ComboBoxLang.Items.AddRange(New Object() {"(tots els idiomes)", "Solo español", "Només catalá", "English only"})
        Me.ComboBoxLang.Location = New System.Drawing.Point(18, 251)
        Me.ComboBoxLang.Name = "ComboBoxLang"
        Me.ComboBoxLang.Size = New System.Drawing.Size(143, 21)
        Me.ComboBoxLang.TabIndex = 43
        '
        'Frm_Mailing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(806, 517)
        Me.Controls.Add(Me.ComboBoxLang)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.CheckBoxPrv)
        Me.Controls.Add(Me.CheckBoxNoSpam)
        Me.Controls.Add(Me.ButtonTestCircular)
        Me.Controls.Add(Me.TextBoxUrl)
        Me.Controls.Add(Me.TextBoxSubject)
        Me.Controls.Add(Me.ButtonCircular)
        Me.Controls.Add(Me.CheckedListBoxZons)
        Me.Controls.Add(Me.CheckBoxAllZons)
        Me.Controls.Add(Me.CheckedListBoxTpas)
        Me.Controls.Add(Me.CheckBoxAllTpas)
        Me.Controls.Add(Me.GroupBoxPrint)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CheckBoxSalesmen)
        Me.Controls.Add(Me.CheckBoxReps)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "Frm_Mailing"
        Me.Text = "MAILINGS"
        Me.GroupBoxPrint.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonExcel As System.Windows.Forms.Button
    Friend WithEvents ButtonEtqs As System.Windows.Forms.Button
    Friend WithEvents CheckBoxPrv As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxNoSpam As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonTestCircular As System.Windows.Forms.Button
    Friend WithEvents TextBoxUrl As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxSubject As System.Windows.Forms.TextBox
    Friend WithEvents ButtonCircular As System.Windows.Forms.Button
    Friend WithEvents CheckedListBoxZons As System.Windows.Forms.CheckedListBox
    Friend WithEvents CheckBoxAllZons As System.Windows.Forms.CheckBox
    Friend WithEvents CheckedListBoxTpas As System.Windows.Forms.CheckedListBox
    Friend WithEvents CheckBoxAllTpas As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBoxPrint As System.Windows.Forms.GroupBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxOnLineShops As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxCliSalePoints As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxCliMasters As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxClis As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxSalesmen As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonEmails As System.Windows.Forms.Button
    Friend WithEvents CheckBoxReps As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonAdrs As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ComboBoxTarifa As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxLang As System.Windows.Forms.ComboBox
End Class
