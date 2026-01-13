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
        Me.Xl_RankingItems1 = New Mat.NET.Xl_RankingItems()
        Me.Xl_ContactsComboProveidors = New Mat.NET.Xl_ContactsCombo()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_Lookup_ProductBase1 = New Mat.NET.Xl_Lookup_ProductBase()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_Lookup_DTOContact1 = New Mat.NET.Xl_Lookup_DTOArea()
        Me.Xl_RepsCombo1 = New Mat.NET.Xl_RepsCombo()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DateTimePickerTo = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DateTimePickerFrom = New System.Windows.Forms.DateTimePicker()
        Me.PictureBoxExcel = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBoxExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_RankingItems1
        '
        Me.Xl_RankingItems1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_RankingItems1.Location = New System.Drawing.Point(0, 80)
        Me.Xl_RankingItems1.Name = "Xl_RankingItems1"
        Me.Xl_RankingItems1.Size = New System.Drawing.Size(963, 181)
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
        'Xl_Lookup_ProductBase1
        '
        Me.Xl_Lookup_ProductBase1.IsDirty = False
        Me.Xl_Lookup_ProductBase1.Location = New System.Drawing.Point(378, 39)
        Me.Xl_Lookup_ProductBase1.Name = "Xl_Lookup_ProductBase1"
        Me.Xl_Lookup_ProductBase1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_Lookup_ProductBase1.Product = Nothing
        Me.Xl_Lookup_ProductBase1.Size = New System.Drawing.Size(365, 20)
        Me.Xl_Lookup_ProductBase1.TabIndex = 14
        Me.Xl_Lookup_ProductBase1.Value = Nothing
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(313, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Producte"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(313, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Zona"
        '
        'Xl_Lookup_DTOContact1
        '
        Me.Xl_Lookup_DTOContact1.Area = Nothing
        Me.Xl_Lookup_DTOContact1.IsDirty = False
        Me.Xl_Lookup_DTOContact1.Location = New System.Drawing.Point(378, 13)
        Me.Xl_Lookup_DTOContact1.Name = "Xl_Lookup_DTOContact1"
        Me.Xl_Lookup_DTOContact1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_Lookup_DTOContact1.Size = New System.Drawing.Size(365, 20)
        Me.Xl_Lookup_DTOContact1.TabIndex = 11
        Me.Xl_Lookup_DTOContact1.Value = Nothing
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
        'DateTimePickerTo
        '
        Me.DateTimePickerTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerTo.Location = New System.Drawing.Point(856, 13)
        Me.DateTimePickerTo.Name = "DateTimePickerTo"
        Me.DateTimePickerTo.Size = New System.Drawing.Size(95, 20)
        Me.DateTimePickerTo.TabIndex = 17
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(816, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(23, 13)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "fins"
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(816, 43)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "des de"
        '
        'DateTimePickerFrom
        '
        Me.DateTimePickerFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFrom.Location = New System.Drawing.Point(856, 39)
        Me.DateTimePickerFrom.Name = "DateTimePickerFrom"
        Me.DateTimePickerFrom.Size = New System.Drawing.Size(95, 20)
        Me.DateTimePickerFrom.TabIndex = 19
        '
        'PictureBoxExcel
        '
        Me.PictureBoxExcel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxExcel.Image = Global.Mat.NET.My.Resources.Resources.Excel_16
        Me.PictureBoxExcel.Location = New System.Drawing.Point(775, 17)
        Me.PictureBoxExcel.Name = "PictureBoxExcel"
        Me.PictureBoxExcel.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxExcel.TabIndex = 21
        Me.PictureBoxExcel.TabStop = False
        '
        'Frm_Ranking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(963, 261)
        Me.Controls.Add(Me.PictureBoxExcel)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.DateTimePickerFrom)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.DateTimePickerTo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_RepsCombo1)
        Me.Controls.Add(Me.Xl_Lookup_ProductBase1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_Lookup_DTOContact1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_ContactsComboProveidors)
        Me.Controls.Add(Me.Xl_RankingItems1)
        Me.Name = "Frm_Ranking"
        Me.Text = "Ranking"
        CType(Me.PictureBoxExcel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_RankingItems1 As Mat.NET.Xl_RankingItems
    Friend WithEvents Xl_ContactsComboProveidors As Mat.NET.Xl_ContactsCombo
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_Lookup_ProductBase1 As Mat.NET.Xl_Lookup_ProductBase
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_Lookup_DTOContact1 As Mat.NET.Xl_Lookup_DTOArea
    Friend WithEvents Xl_RepsCombo1 As Mat.NET.Xl_RepsCombo
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents PictureBoxExcel As System.Windows.Forms.PictureBox
End Class
