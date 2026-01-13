<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Blog
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
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_TextboxSearchPosts = New Winforms.Xl_TextboxSearch()
        Me.Xl_BlogPosts1 = New Winforms.Xl_BlogPosts()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_LeadsSuscriptors = New Winforms.Xl_Leads()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Xl_SearchRequests1 = New Winforms.Xl_SearchRequests()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_TextboxSearchLeads = New Winforms.Xl_TextboxSearch()
        Me.Xl_Leads1 = New Winforms.Xl_Leads()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.TabControl1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_BlogPosts1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.Xl_LeadsSuscriptors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Xl_Leads1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(3, 42)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(797, 406)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Panel1)
        Me.TabPage2.Controls.Add(Me.Xl_TextboxSearchPosts)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(789, 380)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Posts"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_TextboxSearchPosts
        '
        Me.Xl_TextboxSearchPosts.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearchPosts.Location = New System.Drawing.Point(631, 6)
        Me.Xl_TextboxSearchPosts.Name = "Xl_TextboxSearchPosts"
        Me.Xl_TextboxSearchPosts.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearchPosts.TabIndex = 2
        '
        'Xl_BlogPosts1
        '
        Me.Xl_BlogPosts1.AllowUserToAddRows = False
        Me.Xl_BlogPosts1.AllowUserToDeleteRows = False
        Me.Xl_BlogPosts1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_BlogPosts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_BlogPosts1.Filter = Nothing
        Me.Xl_BlogPosts1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_BlogPosts1.Name = "Xl_BlogPosts1"
        Me.Xl_BlogPosts1.ReadOnly = True
        Me.Xl_BlogPosts1.Size = New System.Drawing.Size(789, 329)
        Me.Xl_BlogPosts1.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_LeadsSuscriptors)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(789, 380)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Subscriptors"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_LeadsSuscriptors
        '
        Me.Xl_LeadsSuscriptors.AllowUserToAddRows = False
        Me.Xl_LeadsSuscriptors.AllowUserToDeleteRows = False
        Me.Xl_LeadsSuscriptors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_LeadsSuscriptors.DisplayObsolets = False
        Me.Xl_LeadsSuscriptors.Filter = Nothing
        Me.Xl_LeadsSuscriptors.Location = New System.Drawing.Point(0, 37)
        Me.Xl_LeadsSuscriptors.MouseIsDown = False
        Me.Xl_LeadsSuscriptors.Name = "Xl_LeadsSuscriptors"
        Me.Xl_LeadsSuscriptors.ReadOnly = True
        Me.Xl_LeadsSuscriptors.Size = New System.Drawing.Size(789, 347)
        Me.Xl_LeadsSuscriptors.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Xl_SearchRequests1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(789, 380)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Keywords"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Xl_SearchRequests1
        '
        Me.Xl_SearchRequests1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_SearchRequests1.Location = New System.Drawing.Point(0, 32)
        Me.Xl_SearchRequests1.Name = "Xl_SearchRequests1"
        Me.Xl_SearchRequests1.Size = New System.Drawing.Size(789, 345)
        Me.Xl_SearchRequests1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_TextboxSearchLeads)
        Me.TabPage1.Controls.Add(Me.Xl_Leads1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(789, 380)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Leads"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_TextboxSearchLeads
        '
        Me.Xl_TextboxSearchLeads.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearchLeads.Location = New System.Drawing.Point(631, 6)
        Me.Xl_TextboxSearchLeads.Name = "Xl_TextboxSearchLeads"
        Me.Xl_TextboxSearchLeads.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearchLeads.TabIndex = 1
        '
        'Xl_Leads1
        '
        Me.Xl_Leads1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Leads1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Leads1.DisplayObsolets = False
        Me.Xl_Leads1.Filter = Nothing
        Me.Xl_Leads1.Location = New System.Drawing.Point(0, 32)
        Me.Xl_Leads1.MouseIsDown = False
        Me.Xl_Leads1.Name = "Xl_Leads1"
        Me.Xl_Leads1.Size = New System.Drawing.Size(789, 348)
        Me.Xl_Leads1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Xl_BlogPosts1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 32)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(789, 352)
        Me.Panel1.TabIndex = 3
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 329)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(789, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Frm_Blog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Blog"
        Me.Text = "Blog"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_BlogPosts1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Xl_LeadsSuscriptors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.Xl_Leads1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_Leads1 As Xl_Leads
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Xl_TextboxSearchLeads As Xl_TextboxSearch
    Friend WithEvents Xl_SearchRequests1 As Xl_SearchRequests
    Friend WithEvents Xl_TextboxSearchPosts As Xl_TextboxSearch
    Friend WithEvents Xl_BlogPosts1 As Xl_BlogPosts
    Friend WithEvents Xl_LeadsSuscriptors As Xl_Leads
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
