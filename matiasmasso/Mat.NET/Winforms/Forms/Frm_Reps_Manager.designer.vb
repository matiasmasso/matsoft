<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Reps_Manager
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_Rep_RepComPncCheck2 = New Winforms.Xl_Rep_RepComPncCheck()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_RepComLiquidableNewRepLiq1 = New Winforms.Xl_RepComLiquidableNewRepLiq()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonSaveNewRepLiq = New System.Windows.Forms.Button()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Xl_RepLiqs1 = New Winforms.Xl_Repliqs()
        Me.Buttonransferencies = New System.Windows.Forms.Button()
        Me.ProgressBarSave = New System.Windows.Forms.ProgressBar()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.Xl_RepLiqs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(1, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(800, 397)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_Rep_RepComPncCheck2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(792, 371)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "conflictes comisions"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_Rep_RepComPncCheck2
        '
        Me.Xl_Rep_RepComPncCheck2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Rep_RepComPncCheck2.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Rep_RepComPncCheck2.Name = "Xl_Rep_RepComPncCheck2"
        Me.Xl_Rep_RepComPncCheck2.Size = New System.Drawing.Size(786, 365)
        Me.Xl_Rep_RepComPncCheck2.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_RepComLiquidableNewRepLiq1)
        Me.TabPage3.Controls.Add(Me.ProgressBarSave)
        Me.TabPage3.Controls.Add(Me.Panel1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(792, 371)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Pendents de liquidar"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_RepComLiquidableNewRepLiq1
        '
        Me.Xl_RepComLiquidableNewRepLiq1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_RepComLiquidableNewRepLiq1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_RepComLiquidableNewRepLiq1.Name = "Xl_RepComLiquidableNewRepLiq1"
        Me.Xl_RepComLiquidableNewRepLiq1.Size = New System.Drawing.Size(786, 311)
        Me.Xl_RepComLiquidableNewRepLiq1.TabIndex = 42
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonSaveNewRepLiq)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(3, 337)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(786, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(567, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonSaveNewRepLiq
        '
        Me.ButtonSaveNewRepLiq.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSaveNewRepLiq.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonSaveNewRepLiq.Location = New System.Drawing.Point(678, 4)
        Me.ButtonSaveNewRepLiq.Name = "ButtonSaveNewRepLiq"
        Me.ButtonSaveNewRepLiq.Size = New System.Drawing.Size(104, 24)
        Me.ButtonSaveNewRepLiq.TabIndex = 11
        Me.ButtonSaveNewRepLiq.Text = "ACCEPTAR"
        Me.ButtonSaveNewRepLiq.UseVisualStyleBackColor = False
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Xl_RepLiqs1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(792, 371)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Liquidacions"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Xl_RepLiqs1
        '
        Me.Xl_RepLiqs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_RepLiqs1.Filter = Nothing
        Me.Xl_RepLiqs1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_RepLiqs1.Name = "Xl_RepLiqs1"
        Me.Xl_RepLiqs1.Size = New System.Drawing.Size(786, 365)
        Me.Xl_RepLiqs1.TabIndex = 0
        '
        'Buttonransferencies
        '
        Me.Buttonransferencies.Location = New System.Drawing.Point(711, 5)
        Me.Buttonransferencies.Name = "Buttonransferencies"
        Me.Buttonransferencies.Size = New System.Drawing.Size(85, 23)
        Me.Buttonransferencies.TabIndex = 1
        Me.Buttonransferencies.Text = "Transferencies"
        Me.Buttonransferencies.UseVisualStyleBackColor = True
        '
        'ProgressBarSave
        '
        Me.ProgressBarSave.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBarSave.Location = New System.Drawing.Point(3, 314)
        Me.ProgressBarSave.Name = "ProgressBarSave"
        Me.ProgressBarSave.Size = New System.Drawing.Size(786, 23)
        Me.ProgressBarSave.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBarSave.TabIndex = 43
        Me.ProgressBarSave.Visible = False
        '
        'Frm_Reps_Manager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 410)
        Me.Controls.Add(Me.Buttonransferencies)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Reps_Manager"
        Me.Text = "Reps manager"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        CType(Me.Xl_RepLiqs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_RepComLiquidableNewRepLiq1 As Winforms.Xl_RepComLiquidableNewRepLiq
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonSaveNewRepLiq As System.Windows.Forms.Button
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_RepLiqs1 As Winforms.Xl_Repliqs
    Friend WithEvents Xl_Rep_RepComPncCheck2 As Winforms.Xl_Rep_RepComPncCheck
    Friend WithEvents Buttonransferencies As System.Windows.Forms.Button
    Friend WithEvents ProgressBarSave As ProgressBar
End Class
