<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_RepGoal
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Xl_LookupRepKpi1 = New Winforms.Xl_LookupRepKpi()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_LookupRep1 = New Winforms.Xl_LookupRep()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Xl_TextBoxNumGoal = New Winforms.Xl_TextBoxNum()
        Me.Xl_AmountGoal = New Winforms.Xl_Amount()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_AmountReward = New Winforms.Xl_Amount()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 205)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(532, 31)
        Me.Panel1.TabIndex = 55
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(313, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(424, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
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
        'Xl_LookupRepKpi1
        '
        Me.Xl_LookupRepKpi1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupRepKpi1.IsDirty = False
        Me.Xl_LookupRepKpi1.Location = New System.Drawing.Point(81, 67)
        Me.Xl_LookupRepKpi1.Name = "Xl_LookupRepKpi1"
        Me.Xl_LookupRepKpi1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupRepKpi1.RepKpi = Nothing
        Me.Xl_LookupRepKpi1.Size = New System.Drawing.Size(411, 20)
        Me.Xl_LookupRepKpi1.TabIndex = 56
        Me.Xl_LookupRepKpi1.Value = Nothing
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(22, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Kpi"
        '
        'Xl_LookupRep1
        '
        Me.Xl_LookupRep1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupRep1.IsDirty = False
        Me.Xl_LookupRep1.Location = New System.Drawing.Point(81, 41)
        Me.Xl_LookupRep1.Name = "Xl_LookupRep1"
        Me.Xl_LookupRep1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupRep1.Rep = Nothing
        Me.Xl_LookupRep1.Size = New System.Drawing.Size(411, 20)
        Me.Xl_LookupRep1.TabIndex = 58
        Me.Xl_LookupRep1.Value = Nothing
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(31, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 59
        Me.Label2.Text = "Rep"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(30, 98)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 60
        Me.Label3.Text = "Data"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(81, 94)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(103, 20)
        Me.DateTimePicker1.TabIndex = 61
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.Xl_TextBoxNumGoal)
        Me.FlowLayoutPanel1.Controls.Add(Me.Xl_AmountGoal)
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(81, 118)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(411, 25)
        Me.FlowLayoutPanel1.TabIndex = 62
        '
        'Xl_TextBoxNumGoal
        '
        Me.Xl_TextBoxNumGoal.Location = New System.Drawing.Point(3, 3)
        Me.Xl_TextBoxNumGoal.Mat_FormatString = ""
        Me.Xl_TextBoxNumGoal.Name = "Xl_TextBoxNumGoal"
        Me.Xl_TextBoxNumGoal.ReadOnly = False
        Me.Xl_TextBoxNumGoal.Size = New System.Drawing.Size(100, 20)
        Me.Xl_TextBoxNumGoal.TabIndex = 0
        Me.Xl_TextBoxNumGoal.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Xl_AmountGoal
        '
        Me.Xl_AmountGoal.Amt = Nothing
        Me.Xl_AmountGoal.Location = New System.Drawing.Point(109, 3)
        Me.Xl_AmountGoal.Name = "Xl_AmountGoal"
        Me.Xl_AmountGoal.Size = New System.Drawing.Size(100, 20)
        Me.Xl_AmountGoal.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(31, 124)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 13)
        Me.Label4.TabIndex = 63
        Me.Label4.Text = "Goal"
        '
        'Xl_AmountReward
        '
        Me.Xl_AmountReward.Amt = Nothing
        Me.Xl_AmountReward.Location = New System.Drawing.Point(81, 147)
        Me.Xl_AmountReward.Name = "Xl_AmountReward"
        Me.Xl_AmountReward.Size = New System.Drawing.Size(100, 20)
        Me.Xl_AmountReward.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(31, 150)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 13)
        Me.Label5.TabIndex = 64
        Me.Label5.Text = "Reward"
        '
        'Frm_RepGoal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(532, 236)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Controls.Add(Me.Xl_AmountReward)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_LookupRep1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_LookupRepKpi1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_RepGoal"
        Me.Text = "Objectiu comercials"
        Me.Panel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Xl_LookupRepKpi1 As Xl_LookupRepKpi
    Friend WithEvents Label1 As Label
    Friend WithEvents Xl_LookupRep1 As Xl_LookupRep
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents Xl_TextBoxNumGoal As Xl_TextBoxNum
    Friend WithEvents Xl_AmountGoal As Xl_Amount
    Friend WithEvents Label4 As Label
    Friend WithEvents Xl_AmountReward As Xl_Amount
    Friend WithEvents Label5 As Label
End Class
