<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_RepLiq
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Xl_RepliqFras1 = New Xl_RepliqFras()
        Me.Xl_AmtTotalBaseFras = New Xl_Amt()
        Me.Xl_AmtTotalComisions = New Xl_Amt()
        Me.PictureBoxIcon = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBoxIVA = New System.Windows.Forms.CheckBox()
        Me.Xl_PercentIVA = New Xl_Percent()
        Me.Xl_AmtIVA = New Xl_Amt()
        Me.Xl_AmtIRPF = New Xl_Amt()
        Me.Xl_PercentIRPF = New Xl_Percent()
        Me.CheckBoxIRPF = New System.Windows.Forms.CheckBox()
        Me.Xl_AmtLiquid = New Xl_Amt()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_DocFile1 = New Xl_DocFile_Old()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBoxIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(694, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 448)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(694, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(475, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(586, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
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
        Me.ButtonDel.Text = "RETROCEDIR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(605, 2)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(85, 20)
        Me.DateTimePicker1.TabIndex = 42
        '
        'Xl_RepliqFras1
        '
        Me.Xl_RepliqFras1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_RepliqFras1.Location = New System.Drawing.Point(6, 132)
        Me.Xl_RepliqFras1.Name = "Xl_RepliqFras1"
        Me.Xl_RepliqFras1.Size = New System.Drawing.Size(333, 314)
        Me.Xl_RepliqFras1.TabIndex = 43
        '
        'Xl_AmtTotalBaseFras
        '
        Me.Xl_AmtTotalBaseFras.Enabled = False
        Me.Xl_AmtTotalBaseFras.Location = New System.Drawing.Point(6, 28)
        Me.Xl_AmtTotalBaseFras.Name = "Xl_AmtTotalBaseFras"
        Me.Xl_AmtTotalBaseFras.Size = New System.Drawing.Size(110, 20)
        Me.Xl_AmtTotalBaseFras.TabIndex = 44
        Me.Xl_AmtTotalBaseFras.Value = Nothing
        '
        'Xl_AmtTotalComisions
        '
        Me.Xl_AmtTotalComisions.Enabled = False
        Me.Xl_AmtTotalComisions.Location = New System.Drawing.Point(253, 28)
        Me.Xl_AmtTotalComisions.Name = "Xl_AmtTotalComisions"
        Me.Xl_AmtTotalComisions.Size = New System.Drawing.Size(85, 20)
        Me.Xl_AmtTotalComisions.TabIndex = 45
        Me.Xl_AmtTotalComisions.Value = Nothing
        '
        'PictureBoxIcon
        '
        Me.PictureBoxIcon.Location = New System.Drawing.Point(230, 32)
        Me.PictureBoxIcon.Name = "PictureBoxIcon"
        Me.PictureBoxIcon.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxIcon.TabIndex = 46
        Me.PictureBoxIcon.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 13)
        Me.Label1.TabIndex = 47
        Me.Label1.Text = "suma bases facturas"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(257, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "suma comisions"
        '
        'CheckBoxIVA
        '
        Me.CheckBoxIVA.AutoSize = True
        Me.CheckBoxIVA.Location = New System.Drawing.Point(147, 56)
        Me.CheckBoxIVA.Name = "CheckBoxIVA"
        Me.CheckBoxIVA.Size = New System.Drawing.Size(43, 17)
        Me.CheckBoxIVA.TabIndex = 49
        Me.CheckBoxIVA.Text = "IVA"
        Me.CheckBoxIVA.UseVisualStyleBackColor = True
        '
        'Xl_PercentIVA
        '
        Me.Xl_PercentIVA.Location = New System.Drawing.Point(203, 54)
        Me.Xl_PercentIVA.Name = "Xl_PercentIVA"
        Me.Xl_PercentIVA.Size = New System.Drawing.Size(43, 20)
        Me.Xl_PercentIVA.TabIndex = 50
        Me.Xl_PercentIVA.Text = "0,00 %"
        Me.Xl_PercentIVA.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'Xl_AmtIVA
        '
        Me.Xl_AmtIVA.Enabled = False
        Me.Xl_AmtIVA.Location = New System.Drawing.Point(254, 54)
        Me.Xl_AmtIVA.Name = "Xl_AmtIVA"
        Me.Xl_AmtIVA.Size = New System.Drawing.Size(85, 20)
        Me.Xl_AmtIVA.TabIndex = 51
        Me.Xl_AmtIVA.Value = Nothing
        '
        'Xl_AmtIRPF
        '
        Me.Xl_AmtIRPF.Enabled = False
        Me.Xl_AmtIRPF.Location = New System.Drawing.Point(254, 80)
        Me.Xl_AmtIRPF.Name = "Xl_AmtIRPF"
        Me.Xl_AmtIRPF.Size = New System.Drawing.Size(85, 20)
        Me.Xl_AmtIRPF.TabIndex = 54
        Me.Xl_AmtIRPF.Value = Nothing
        '
        'Xl_PercentIRPF
        '
        Me.Xl_PercentIRPF.Location = New System.Drawing.Point(203, 80)
        Me.Xl_PercentIRPF.Name = "Xl_PercentIRPF"
        Me.Xl_PercentIRPF.Size = New System.Drawing.Size(43, 20)
        Me.Xl_PercentIRPF.TabIndex = 53
        Me.Xl_PercentIRPF.Text = "0,00 %"
        Me.Xl_PercentIRPF.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'CheckBoxIRPF
        '
        Me.CheckBoxIRPF.AutoSize = True
        Me.CheckBoxIRPF.Location = New System.Drawing.Point(147, 82)
        Me.CheckBoxIRPF.Name = "CheckBoxIRPF"
        Me.CheckBoxIRPF.Size = New System.Drawing.Size(50, 17)
        Me.CheckBoxIRPF.TabIndex = 52
        Me.CheckBoxIRPF.Text = "IRPF"
        Me.CheckBoxIRPF.UseVisualStyleBackColor = True
        '
        'Xl_AmtLiquid
        '
        Me.Xl_AmtLiquid.Enabled = False
        Me.Xl_AmtLiquid.Location = New System.Drawing.Point(254, 106)
        Me.Xl_AmtLiquid.Name = "Xl_AmtLiquid"
        Me.Xl_AmtLiquid.Size = New System.Drawing.Size(85, 20)
        Me.Xl_AmtLiquid.TabIndex = 55
        Me.Xl_AmtLiquid.Value = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(144, 110)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 13)
        Me.Label3.TabIndex = 56
        Me.Label3.Text = "liquid"
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(341, 28)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 57
        '
        'Frm_RepLiq
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(694, 479)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_AmtLiquid)
        Me.Controls.Add(Me.Xl_AmtIRPF)
        Me.Controls.Add(Me.Xl_PercentIRPF)
        Me.Controls.Add(Me.CheckBoxIRPF)
        Me.Controls.Add(Me.Xl_AmtIVA)
        Me.Controls.Add(Me.Xl_PercentIVA)
        Me.Controls.Add(Me.CheckBoxIVA)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBoxIcon)
        Me.Controls.Add(Me.Xl_AmtTotalComisions)
        Me.Controls.Add(Me.Xl_AmtTotalBaseFras)
        Me.Controls.Add(Me.Xl_RepliqFras1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_RepLiq"
        Me.Text = "LIQUIDACIO REPRESENTANT"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBoxIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Xl_RepliqFras1 As Xl_RepliqFras
    Friend WithEvents Xl_AmtTotalBaseFras As Xl_Amt
    Friend WithEvents Xl_AmtTotalComisions As Xl_Amt
    Friend WithEvents PictureBoxIcon As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxIVA As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_PercentIVA As Xl_Percent
    Friend WithEvents Xl_AmtIVA As Xl_Amt
    Friend WithEvents Xl_AmtIRPF As Xl_Amt
    Friend WithEvents Xl_PercentIRPF As Xl_Percent
    Friend WithEvents CheckBoxIRPF As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_AmtLiquid As Xl_Amt
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_DocFile1 As Xl_DocFile_Old
End Class
