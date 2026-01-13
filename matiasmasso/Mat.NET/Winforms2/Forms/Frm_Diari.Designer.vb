<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Diari
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
        Me.Xl_Years1 = New Xl_Years()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_Diari_Months = New Xl_Diari()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Xl_Diari_Days = New Xl_Diari()
        Me.Xl_Diari_Pdcs = New Xl_Diari()
        Me.ComboBoxMode = New System.Windows.Forms.ComboBox()
        Me.Xl_LookupDistributionChannel1 = New Xl_LookupDistributionChannel()
        Me.CheckBoxChannelFilter = New System.Windows.Forms.CheckBox()
        Me.CheckBoxRepFilter = New System.Windows.Forms.CheckBox()
        Me.Xl_LookupRep1 = New Xl_LookupRep()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(12, 12)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 0
        Me.Xl_Years1.Value = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_Diari_Months)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1226, 397)
        Me.SplitContainer1.SplitterDistance = 148
        Me.SplitContainer1.TabIndex = 1
        '
        'Xl_Diari_Months
        '
        Me.Xl_Diari_Months.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Diari_Months.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Diari_Months.Name = "Xl_Diari_Months"
        Me.Xl_Diari_Months.Size = New System.Drawing.Size(1226, 148)
        Me.Xl_Diari_Months.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Xl_Diari_Days)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Xl_Diari_Pdcs)
        Me.SplitContainer2.Size = New System.Drawing.Size(1226, 245)
        Me.SplitContainer2.SplitterDistance = 123
        Me.SplitContainer2.TabIndex = 0
        '
        'Xl_Diari_Days
        '
        Me.Xl_Diari_Days.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Diari_Days.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Diari_Days.Name = "Xl_Diari_Days"
        Me.Xl_Diari_Days.Size = New System.Drawing.Size(1226, 123)
        Me.Xl_Diari_Days.TabIndex = 1
        '
        'Xl_Diari_Pdcs
        '
        Me.Xl_Diari_Pdcs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Diari_Pdcs.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Diari_Pdcs.Name = "Xl_Diari_Pdcs"
        Me.Xl_Diari_Pdcs.Size = New System.Drawing.Size(1226, 118)
        Me.Xl_Diari_Pdcs.TabIndex = 2
        '
        'ComboBoxMode
        '
        Me.ComboBoxMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxMode.FormattingEnabled = True
        Me.ComboBoxMode.Items.AddRange(New Object() {"Comandes", "Albarans", "Factures"})
        Me.ComboBoxMode.Location = New System.Drawing.Point(1071, 12)
        Me.ComboBoxMode.Name = "ComboBoxMode"
        Me.ComboBoxMode.Size = New System.Drawing.Size(155, 21)
        Me.ComboBoxMode.TabIndex = 2
        '
        'Xl_LookupDistributionChannel1
        '
        Me.Xl_LookupDistributionChannel1.DistributionChannel = Nothing
        Me.Xl_LookupDistributionChannel1.IsDirty = False
        Me.Xl_LookupDistributionChannel1.Location = New System.Drawing.Point(283, 12)
        Me.Xl_LookupDistributionChannel1.Name = "Xl_LookupDistributionChannel1"
        Me.Xl_LookupDistributionChannel1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupDistributionChannel1.ReadOnlyLookup = False
        Me.Xl_LookupDistributionChannel1.Size = New System.Drawing.Size(232, 20)
        Me.Xl_LookupDistributionChannel1.TabIndex = 3
        Me.Xl_LookupDistributionChannel1.Value = Nothing
        Me.Xl_LookupDistributionChannel1.Visible = False
        '
        'CheckBoxChannelFilter
        '
        Me.CheckBoxChannelFilter.AutoSize = True
        Me.CheckBoxChannelFilter.Location = New System.Drawing.Point(182, 14)
        Me.CheckBoxChannelFilter.Name = "CheckBoxChannelFilter"
        Me.CheckBoxChannelFilter.Size = New System.Drawing.Size(95, 17)
        Me.CheckBoxChannelFilter.TabIndex = 4
        Me.CheckBoxChannelFilter.Text = "filtrar per canal"
        Me.CheckBoxChannelFilter.UseVisualStyleBackColor = True
        '
        'CheckBoxRepFilter
        '
        Me.CheckBoxRepFilter.AutoSize = True
        Me.CheckBoxRepFilter.Location = New System.Drawing.Point(536, 14)
        Me.CheckBoxRepFilter.Name = "CheckBoxRepFilter"
        Me.CheckBoxRepFilter.Size = New System.Drawing.Size(84, 17)
        Me.CheckBoxRepFilter.TabIndex = 5
        Me.CheckBoxRepFilter.Text = "filtrar per rep"
        Me.CheckBoxRepFilter.UseVisualStyleBackColor = True
        '
        'Xl_LookupRep1
        '
        Me.Xl_LookupRep1.IsDirty = False
        Me.Xl_LookupRep1.Location = New System.Drawing.Point(618, 14)
        Me.Xl_LookupRep1.Name = "Xl_LookupRep1"
        Me.Xl_LookupRep1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupRep1.ReadOnlyLookup = False
        Me.Xl_LookupRep1.Rep = Nothing
        Me.Xl_LookupRep1.Size = New System.Drawing.Size(200, 20)
        Me.Xl_LookupRep1.TabIndex = 6
        Me.Xl_LookupRep1.Value = Nothing
        Me.Xl_LookupRep1.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 41)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1226, 420)
        Me.Panel1.TabIndex = 1
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 397)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(1226, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Frm_Diari
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1228, 462)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Xl_LookupRep1)
        Me.Controls.Add(Me.CheckBoxRepFilter)
        Me.Controls.Add(Me.CheckBoxChannelFilter)
        Me.Controls.Add(Me.Xl_LookupDistributionChannel1)
        Me.Controls.Add(Me.ComboBoxMode)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Name = "Frm_Diari"
        Me.Text = "Diari de vendes"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Diari_Months As Xl_Diari
    Friend WithEvents Xl_Diari_Days As Xl_Diari
    Friend WithEvents Xl_Diari_Pdcs As Xl_Diari
    Friend WithEvents ComboBoxMode As ComboBox
    Friend WithEvents Xl_LookupDistributionChannel1 As Xl_LookupDistributionChannel
    Friend WithEvents CheckBoxChannelFilter As CheckBox
    Friend WithEvents CheckBoxRepFilter As CheckBox
    Friend WithEvents Xl_LookupRep1 As Xl_LookupRep
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
