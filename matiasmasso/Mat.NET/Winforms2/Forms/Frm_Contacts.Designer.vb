<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Contacts
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_ContactClasses1 = New Xl_ContactClasses()
        Me.Xl_Contacts1 = New Xl_Contacts()
        Me.Xl_TextboxSearch1 = New Xl_TextboxSearch()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Xl_DistributionChannels1 = New Xl_DistributionChannels()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_ContactClasses1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_Contacts1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.Xl_DistributionChannels1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_ContactClasses1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_Contacts1)
        Me.SplitContainer1.Size = New System.Drawing.Size(498, 435)
        Me.SplitContainer1.SplitterDistance = 197
        Me.SplitContainer1.TabIndex = 0
        '
        'Xl_ContactClasses1
        '
        Me.Xl_ContactClasses1.AllowUserToAddRows = False
        Me.Xl_ContactClasses1.AllowUserToDeleteRows = False
        Me.Xl_ContactClasses1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ContactClasses1.DisplayObsolets = False
        Me.Xl_ContactClasses1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ContactClasses1.Filter = Nothing
        Me.Xl_ContactClasses1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ContactClasses1.MouseIsDown = False
        Me.Xl_ContactClasses1.Name = "Xl_ContactClasses1"
        Me.Xl_ContactClasses1.ReadOnly = True
        Me.Xl_ContactClasses1.Size = New System.Drawing.Size(197, 435)
        Me.Xl_ContactClasses1.TabIndex = 0
        '
        'Xl_Contacts1
        '
        Me.Xl_Contacts1.AllowUserToAddRows = False
        Me.Xl_Contacts1.AllowUserToDeleteRows = False
        Me.Xl_Contacts1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Contacts1.DisplayObsolets = False
        Me.Xl_Contacts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Contacts1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Contacts1.MouseIsDown = False
        Me.Xl_Contacts1.Name = "Xl_Contacts1"
        Me.Xl_Contacts1.ReadOnly = True
        Me.Xl_Contacts1.Size = New System.Drawing.Size(297, 435)
        Me.Xl_Contacts1.TabIndex = 0
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(527, 12)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Xl_DistributionChannels1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer1)
        Me.SplitContainer2.Size = New System.Drawing.Size(689, 435)
        Me.SplitContainer2.SplitterDistance = 187
        Me.SplitContainer2.TabIndex = 2
        '
        'Xl_DistributionChannels1
        '
        Me.Xl_DistributionChannels1.AllowUserToAddRows = False
        Me.Xl_DistributionChannels1.AllowUserToDeleteRows = False
        Me.Xl_DistributionChannels1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_DistributionChannels1.DisplayObsolets = False
        Me.Xl_DistributionChannels1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_DistributionChannels1.Filter = Nothing
        Me.Xl_DistributionChannels1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_DistributionChannels1.MouseIsDown = False
        Me.Xl_DistributionChannels1.Name = "Xl_DistributionChannels1"
        Me.Xl_DistributionChannels1.ReadOnly = True
        Me.Xl_DistributionChannels1.Size = New System.Drawing.Size(187, 435)
        Me.Xl_DistributionChannels1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.SplitContainer2)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 38)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(689, 458)
        Me.Panel1.TabIndex = 3
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 435)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(689, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 3
        '
        'Frm_Contacts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(689, 497)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Name = "Frm_Contacts"
        Me.Text = "Contactes"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_ContactClasses1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_Contacts1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.Xl_DistributionChannels1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_ContactClasses1 As Xl_ContactClasses
    Friend WithEvents Xl_Contacts1 As Xl_Contacts
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents Xl_DistributionChannels1 As Xl_DistributionChannels
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
