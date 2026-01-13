<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Stats
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Stats))
        Me.PictureBoxWait = New System.Windows.Forms.PictureBox()
        Me.ComboBoxConcept = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_RepsCombo1 = New Mat.NET.Xl_RepsCombo()
        Me.Xl_Years1 = New Mat.NET.Xl_Years()
        Me.Xl_Stats1 = New Mat.NET.Xl_Stats()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonQty = New System.Windows.Forms.RadioButton()
        Me.RadioButtonAmt = New System.Windows.Forms.RadioButton()
        Me.CheckBoxIncludeHidden = New System.Windows.Forms.CheckBox()
        Me.PictureBoxExcel = New System.Windows.Forms.PictureBox()
        Me.CheckBoxGroupByHolding = New System.Windows.Forms.CheckBox()
        Me.Xl_GuidNoms_Proveidors = New Mat.NET.Xl_GuidNoms_Combo()
        Me.Xl_LookupArea1 = New Mat.NET.Xl_LookupArea()
        Me.Xl_LookupProduct1 = New Mat.NET.Xl_LookupProduct()
        CType(Me.PictureBoxWait, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBoxExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBoxWait
        '
        Me.PictureBoxWait.Image = CType(resources.GetObject("PictureBoxWait.Image"), System.Drawing.Image)
        Me.PictureBoxWait.Location = New System.Drawing.Point(26, 106)
        Me.PictureBoxWait.Name = "PictureBoxWait"
        Me.PictureBoxWait.Size = New System.Drawing.Size(29, 28)
        Me.PictureBoxWait.TabIndex = 2
        Me.PictureBoxWait.TabStop = False
        Me.PictureBoxWait.Visible = False
        '
        'ComboBoxConcept
        '
        Me.ComboBoxConcept.FormattingEnabled = True
        Me.ComboBoxConcept.Location = New System.Drawing.Point(12, 41)
        Me.ComboBoxConcept.Name = "ComboBoxConcept"
        Me.ComboBoxConcept.Size = New System.Drawing.Size(163, 21)
        Me.ComboBoxConcept.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(343, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Zona/client"
        '
        'Xl_RepsCombo1
        '
        Me.Xl_RepsCombo1.Location = New System.Drawing.Point(198, 14)
        Me.Xl_RepsCombo1.MinimumSize = New System.Drawing.Size(80, 0)
        Me.Xl_RepsCombo1.Name = "Xl_RepsCombo1"
        Me.Xl_RepsCombo1.Size = New System.Drawing.Size(140, 21)
        Me.Xl_RepsCombo1.TabIndex = 3
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(12, 12)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 1
        Me.Xl_Years1.Value = 0
        '
        'Xl_Stats1
        '
        Me.Xl_Stats1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Stats1.Location = New System.Drawing.Point(1, 80)
        Me.Xl_Stats1.Name = "Xl_Stats1"
        Me.Xl_Stats1.Size = New System.Drawing.Size(1065, 262)
        Me.Xl_Stats1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(343, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Producte"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.RadioButtonQty)
        Me.GroupBox1.Controls.Add(Me.RadioButtonAmt)
        Me.GroupBox1.Location = New System.Drawing.Point(743, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(127, 56)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Valors"
        '
        'RadioButtonQty
        '
        Me.RadioButtonQty.AutoSize = True
        Me.RadioButtonQty.Location = New System.Drawing.Point(56, 35)
        Me.RadioButtonQty.Name = "RadioButtonQty"
        Me.RadioButtonQty.Size = New System.Drawing.Size(56, 17)
        Me.RadioButtonQty.TabIndex = 1
        Me.RadioButtonQty.Text = "unitats"
        Me.RadioButtonQty.UseVisualStyleBackColor = True
        '
        'RadioButtonAmt
        '
        Me.RadioButtonAmt.AutoSize = True
        Me.RadioButtonAmt.Checked = True
        Me.RadioButtonAmt.Location = New System.Drawing.Point(56, 13)
        Me.RadioButtonAmt.Name = "RadioButtonAmt"
        Me.RadioButtonAmt.Size = New System.Drawing.Size(53, 17)
        Me.RadioButtonAmt.TabIndex = 0
        Me.RadioButtonAmt.TabStop = True
        Me.RadioButtonAmt.Text = "import"
        Me.RadioButtonAmt.UseVisualStyleBackColor = True
        '
        'CheckBoxIncludeHidden
        '
        Me.CheckBoxIncludeHidden.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxIncludeHidden.AutoSize = True
        Me.CheckBoxIncludeHidden.Location = New System.Drawing.Point(877, 13)
        Me.CheckBoxIncludeHidden.Name = "CheckBoxIncludeHidden"
        Me.CheckBoxIncludeHidden.Size = New System.Drawing.Size(86, 17)
        Me.CheckBoxIncludeHidden.TabIndex = 12
        Me.CheckBoxIncludeHidden.Text = "Inclou ocults"
        Me.CheckBoxIncludeHidden.UseVisualStyleBackColor = True
        '
        'PictureBoxExcel
        '
        Me.PictureBoxExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxExcel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxExcel.Image = Global.Mat.NET.My.Resources.Resources.Excel
        Me.PictureBoxExcel.Location = New System.Drawing.Point(1040, 12)
        Me.PictureBoxExcel.Name = "PictureBoxExcel"
        Me.PictureBoxExcel.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxExcel.TabIndex = 13
        Me.PictureBoxExcel.TabStop = False
        '
        'CheckBoxGroupByHolding
        '
        Me.CheckBoxGroupByHolding.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxGroupByHolding.AutoSize = True
        Me.CheckBoxGroupByHolding.Location = New System.Drawing.Point(877, 42)
        Me.CheckBoxGroupByHolding.Name = "CheckBoxGroupByHolding"
        Me.CheckBoxGroupByHolding.Size = New System.Drawing.Size(180, 17)
        Me.CheckBoxGroupByHolding.TabIndex = 14
        Me.CheckBoxGroupByHolding.Text = "agrupa botigues del mateix client"
        Me.CheckBoxGroupByHolding.UseVisualStyleBackColor = True
        '
        'Xl_GuidNoms_Proveidors
        '
        Me.Xl_GuidNoms_Proveidors.FormattingEnabled = True
        Me.Xl_GuidNoms_Proveidors.Location = New System.Drawing.Point(198, 42)
        Me.Xl_GuidNoms_Proveidors.MinimumSize = New System.Drawing.Size(80, 0)
        Me.Xl_GuidNoms_Proveidors.Name = "Xl_GuidNoms_Proveidors"
        Me.Xl_GuidNoms_Proveidors.Size = New System.Drawing.Size(140, 21)
        Me.Xl_GuidNoms_Proveidors.TabIndex = 15
        '
        'Xl_LookupArea1
        '
        Me.Xl_LookupArea1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupArea1.Area = Nothing
        Me.Xl_LookupArea1.IsDirty = False
        Me.Xl_LookupArea1.Location = New System.Drawing.Point(413, 14)
        Me.Xl_LookupArea1.Name = "Xl_LookupArea1"
        Me.Xl_LookupArea1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupArea1.Size = New System.Drawing.Size(324, 20)
        Me.Xl_LookupArea1.TabIndex = 16
        Me.Xl_LookupArea1.Value = Nothing
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(413, 41)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(324, 20)
        Me.Xl_LookupProduct1.TabIndex = 17
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Frm_Stats
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1068, 344)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.Xl_LookupArea1)
        Me.Controls.Add(Me.Xl_GuidNoms_Proveidors)
        Me.Controls.Add(Me.CheckBoxGroupByHolding)
        Me.Controls.Add(Me.PictureBoxExcel)
        Me.Controls.Add(Me.CheckBoxIncludeHidden)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ComboBoxConcept)
        Me.Controls.Add(Me.Xl_RepsCombo1)
        Me.Controls.Add(Me.PictureBoxWait)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.Xl_Stats1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Name = "Frm_Stats"
        Me.Text = "Estadistiques"
        CType(Me.PictureBoxWait, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBoxExcel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_Stats1 As Mat.NET.Xl_Stats
    Friend WithEvents Xl_Years1 As Mat.NET.Xl_Years
    Friend WithEvents PictureBoxWait As System.Windows.Forms.PictureBox
    Friend WithEvents Xl_RepsCombo1 As Mat.NET.Xl_RepsCombo
    Friend WithEvents ComboBoxConcept As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonQty As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonAmt As System.Windows.Forms.RadioButton
    Friend WithEvents CheckBoxIncludeHidden As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBoxExcel As System.Windows.Forms.PictureBox
    Friend WithEvents CheckBoxGroupByHolding As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_GuidNoms_Proveidors As Mat.NET.Xl_GuidNoms_Combo
    Friend WithEvents Xl_LookupArea1 As Mat.NET.Xl_LookupArea
    Friend WithEvents Xl_LookupProduct1 As Mat.NET.Xl_LookupProduct
End Class
