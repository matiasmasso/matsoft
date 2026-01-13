<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Ranking
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
        Me.Xl_RankingItems1 = New Mat.Net.Xl_RankingItems()
        Me.Xl_ContactsComboProveidors = New Mat.Net.Xl_ContactsCombo()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_LookupProduct1 = New Mat.Net.Xl_LookupProduct()
        Me.Xl_RepsCombo1 = New Mat.Net.Xl_RepsCombo()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PictureBoxExcel = New System.Windows.Forms.PictureBox()
        Me.Xl_LookupDistributionChannel1 = New Mat.Net.Xl_LookupDistributionChannel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_LookupArea1 = New Mat.Net.Xl_LookupArea()
        Me.CheckBoxZona = New System.Windows.Forms.CheckBox()
        Me.CheckBoxProduct = New System.Windows.Forms.CheckBox()
        Me.CheckBoxChannel = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NumericUpDownYear = New System.Windows.Forms.NumericUpDown()
        Me.ComboBoxMonthFrom = New System.Windows.Forms.ComboBox()
        Me.ComboBoxMonthTo = New System.Windows.Forms.ComboBox()
        Me.CheckBoxGroupCcx = New System.Windows.Forms.CheckBox()
        CType(Me.Xl_RankingItems1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.NumericUpDownYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_RankingItems1
        '
        Me.Xl_RankingItems1.DisplayObsolets = False
        Me.Xl_RankingItems1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_RankingItems1.Filter = Nothing
        Me.Xl_RankingItems1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_RankingItems1.MouseIsDown = False
        Me.Xl_RankingItems1.Name = "Xl_RankingItems1"
        Me.Xl_RankingItems1.Size = New System.Drawing.Size(962, 385)
        Me.Xl_RankingItems1.TabIndex = 0
        '
        'Xl_ContactsComboProveidors
        '
        Me.Xl_ContactsComboProveidors.Location = New System.Drawing.Point(100, 39)
        Me.Xl_ContactsComboProveidors.Name = "Xl_ContactsComboProveidors"
        Me.Xl_ContactsComboProveidors.Size = New System.Drawing.Size(207, 21)
        Me.Xl_ContactsComboProveidors.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Proveidors"
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(449, 39)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.ReadOnlyLookup = False
        Me.Xl_LookupProduct1.SelectionMode = DTO.DTOProduct.SelectionModes.SelectAny
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(294, 20)
        Me.Xl_LookupProduct1.TabIndex = 14
        Me.Xl_LookupProduct1.Value = Nothing
        Me.Xl_LookupProduct1.Visible = False
        '
        'Xl_RepsCombo1
        '
        Me.Xl_RepsCombo1.Location = New System.Drawing.Point(100, 12)
        Me.Xl_RepsCombo1.Name = "Xl_RepsCombo1"
        Me.Xl_RepsCombo1.Size = New System.Drawing.Size(207, 21)
        Me.Xl_RepsCombo1.TabIndex = 15
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 13)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Reps"
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(816, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "des de"
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(816, 69)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(23, 13)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "fins"
        '
        'PictureBoxExcel
        '
        Me.PictureBoxExcel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxExcel.Image = Global.Mat.Net.My.Resources.Resources.Excel_16
        Me.PictureBoxExcel.Location = New System.Drawing.Point(775, 17)
        Me.PictureBoxExcel.Name = "PictureBoxExcel"
        Me.PictureBoxExcel.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxExcel.TabIndex = 21
        Me.PictureBoxExcel.TabStop = False
        '
        'Xl_LookupDistributionChannel1
        '
        Me.Xl_LookupDistributionChannel1.DistributionChannel = Nothing
        Me.Xl_LookupDistributionChannel1.IsDirty = False
        Me.Xl_LookupDistributionChannel1.Location = New System.Drawing.Point(449, 65)
        Me.Xl_LookupDistributionChannel1.Name = "Xl_LookupDistributionChannel1"
        Me.Xl_LookupDistributionChannel1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupDistributionChannel1.ReadOnlyLookup = False
        Me.Xl_LookupDistributionChannel1.Size = New System.Drawing.Size(294, 20)
        Me.Xl_LookupDistributionChannel1.TabIndex = 22
        Me.Xl_LookupDistributionChannel1.Value = Nothing
        Me.Xl_LookupDistributionChannel1.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Xl_RankingItems1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 96)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(962, 408)
        Me.Panel1.TabIndex = 24
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 385)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(962, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Xl_LookupArea1
        '
        Me.Xl_LookupArea1.IsDirty = False
        Me.Xl_LookupArea1.Location = New System.Drawing.Point(449, 13)
        Me.Xl_LookupArea1.Name = "Xl_LookupArea1"
        Me.Xl_LookupArea1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupArea1.ReadOnlyLookup = False
        Me.Xl_LookupArea1.Size = New System.Drawing.Size(294, 20)
        Me.Xl_LookupArea1.TabIndex = 25
        Me.Xl_LookupArea1.Value = Nothing
        Me.Xl_LookupArea1.Visible = False
        '
        'CheckBoxZona
        '
        Me.CheckBoxZona.AutoSize = True
        Me.CheckBoxZona.Location = New System.Drawing.Point(374, 14)
        Me.CheckBoxZona.Name = "CheckBoxZona"
        Me.CheckBoxZona.Size = New System.Drawing.Size(51, 17)
        Me.CheckBoxZona.TabIndex = 26
        Me.CheckBoxZona.Text = "Zona"
        Me.CheckBoxZona.UseVisualStyleBackColor = True
        '
        'CheckBoxProduct
        '
        Me.CheckBoxProduct.AutoSize = True
        Me.CheckBoxProduct.Location = New System.Drawing.Point(374, 40)
        Me.CheckBoxProduct.Name = "CheckBoxProduct"
        Me.CheckBoxProduct.Size = New System.Drawing.Size(69, 17)
        Me.CheckBoxProduct.TabIndex = 27
        Me.CheckBoxProduct.Text = "Producte"
        Me.CheckBoxProduct.UseVisualStyleBackColor = True
        '
        'CheckBoxChannel
        '
        Me.CheckBoxChannel.AutoSize = True
        Me.CheckBoxChannel.Location = New System.Drawing.Point(374, 65)
        Me.CheckBoxChannel.Name = "CheckBoxChannel"
        Me.CheckBoxChannel.Size = New System.Drawing.Size(53, 17)
        Me.CheckBoxChannel.TabIndex = 28
        Me.CheckBoxChannel.Text = "Canal"
        Me.CheckBoxChannel.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(816, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(24, 13)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "any"
        '
        'NumericUpDownYear
        '
        Me.NumericUpDownYear.Location = New System.Drawing.Point(883, 14)
        Me.NumericUpDownYear.Maximum = New Decimal(New Integer() {3000, 0, 0, 0})
        Me.NumericUpDownYear.Minimum = New Decimal(New Integer() {1985, 0, 0, 0})
        Me.NumericUpDownYear.Name = "NumericUpDownYear"
        Me.NumericUpDownYear.Size = New System.Drawing.Size(69, 20)
        Me.NumericUpDownYear.TabIndex = 30
        Me.NumericUpDownYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumericUpDownYear.Value = New Decimal(New Integer() {1985, 0, 0, 0})
        '
        'ComboBoxMonthFrom
        '
        Me.ComboBoxMonthFrom.FormattingEnabled = True
        Me.ComboBoxMonthFrom.Location = New System.Drawing.Point(883, 39)
        Me.ComboBoxMonthFrom.Name = "ComboBoxMonthFrom"
        Me.ComboBoxMonthFrom.Size = New System.Drawing.Size(69, 21)
        Me.ComboBoxMonthFrom.TabIndex = 31
        '
        'ComboBoxMonthTo
        '
        Me.ComboBoxMonthTo.FormattingEnabled = True
        Me.ComboBoxMonthTo.Location = New System.Drawing.Point(883, 64)
        Me.ComboBoxMonthTo.Name = "ComboBoxMonthTo"
        Me.ComboBoxMonthTo.Size = New System.Drawing.Size(69, 21)
        Me.ComboBoxMonthTo.TabIndex = 32
        '
        'CheckBoxGroupCcx
        '
        Me.CheckBoxGroupCcx.AutoSize = True
        Me.CheckBoxGroupCcx.Checked = True
        Me.CheckBoxGroupCcx.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxGroupCcx.Location = New System.Drawing.Point(17, 69)
        Me.CheckBoxGroupCcx.Name = "CheckBoxGroupCcx"
        Me.CheckBoxGroupCcx.Size = New System.Drawing.Size(199, 17)
        Me.CheckBoxGroupCcx.TabIndex = 33
        Me.CheckBoxGroupCcx.Text = "Agrupa botigues del mateix propietari"
        Me.CheckBoxGroupCcx.UseVisualStyleBackColor = True
        '
        'Frm_Ranking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(963, 505)
        Me.Controls.Add(Me.CheckBoxGroupCcx)
        Me.Controls.Add(Me.ComboBoxMonthTo)
        Me.Controls.Add(Me.ComboBoxMonthFrom)
        Me.Controls.Add(Me.NumericUpDownYear)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CheckBoxChannel)
        Me.Controls.Add(Me.CheckBoxProduct)
        Me.Controls.Add(Me.CheckBoxZona)
        Me.Controls.Add(Me.Xl_LookupArea1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Xl_LookupDistributionChannel1)
        Me.Controls.Add(Me.PictureBoxExcel)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_RepsCombo1)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_ContactsComboProveidors)
        Me.Name = "Frm_Ranking"
        Me.Text = "Ranking"
        CType(Me.Xl_RankingItems1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxExcel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.NumericUpDownYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_RankingItems1 As Xl_RankingItems
    Friend WithEvents Xl_ContactsComboProveidors As Xl_ContactsCombo
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_LookupProduct1 As Xl_LookupProduct
    Friend WithEvents Xl_RepsCombo1 As Xl_RepsCombo
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents PictureBoxExcel As System.Windows.Forms.PictureBox
    Friend WithEvents Xl_LookupDistributionChannel1 As Xl_LookupDistributionChannel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Xl_LookupArea1 As Xl_LookupArea
    Friend WithEvents CheckBoxZona As CheckBox
    Friend WithEvents CheckBoxProduct As CheckBox
    Friend WithEvents CheckBoxChannel As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents NumericUpDownYear As NumericUpDown
    Friend WithEvents ComboBoxMonthFrom As ComboBox
    Friend WithEvents ComboBoxMonthTo As ComboBox
    Friend WithEvents CheckBoxGroupCcx As CheckBox
End Class
