<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Bloggers
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
        Me.Xl_Bloggers1 = New Winforms.Xl_Bloggers()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_BloggerPostsOfTheWeekEsp = New Winforms.Xl_BloggerPostsOfTheWeek()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_BloggerPostsOfTheWeekPor = New Winforms.Xl_BloggerPostsOfTheWeek()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_BloggerPostsOfTheWeekEsp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.Xl_BloggerPostsOfTheWeekPor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_Bloggers1
        '
        Me.Xl_Bloggers1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Bloggers1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Bloggers1.Name = "Xl_Bloggers1"
        Me.Xl_Bloggers1.Size = New System.Drawing.Size(458, 370)
        Me.Xl_Bloggers1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(2, 33)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(472, 402)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_Bloggers1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(464, 376)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Bloggers"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_BloggerPostsOfTheWeekEsp)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(465, 376)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Posts destacats (espanyol)"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_BloggerPostsOfTheWeekEsp
        '
        Me.Xl_BloggerPostsOfTheWeekEsp.AllowUserToAddRows = False
        Me.Xl_BloggerPostsOfTheWeekEsp.AllowUserToDeleteRows = False
        Me.Xl_BloggerPostsOfTheWeekEsp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_BloggerPostsOfTheWeekEsp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_BloggerPostsOfTheWeekEsp.Filter = Nothing
        Me.Xl_BloggerPostsOfTheWeekEsp.Location = New System.Drawing.Point(0, 37)
        Me.Xl_BloggerPostsOfTheWeekEsp.Name = "Xl_BloggerPostsOfTheWeekEsp"
        Me.Xl_BloggerPostsOfTheWeekEsp.ReadOnly = True
        Me.Xl_BloggerPostsOfTheWeekEsp.Size = New System.Drawing.Size(465, 339)
        Me.Xl_BloggerPostsOfTheWeekEsp.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_BloggerPostsOfTheWeekPor)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(464, 376)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Posts destacats (portugal)"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_BloggerPostsOfTheWeekPor
        '
        Me.Xl_BloggerPostsOfTheWeekPor.AllowUserToAddRows = False
        Me.Xl_BloggerPostsOfTheWeekPor.AllowUserToDeleteRows = False
        Me.Xl_BloggerPostsOfTheWeekPor.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_BloggerPostsOfTheWeekPor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_BloggerPostsOfTheWeekPor.Filter = Nothing
        Me.Xl_BloggerPostsOfTheWeekPor.Location = New System.Drawing.Point(-3, 41)
        Me.Xl_BloggerPostsOfTheWeekPor.Name = "Xl_BloggerPostsOfTheWeekPor"
        Me.Xl_BloggerPostsOfTheWeekPor.ReadOnly = True
        Me.Xl_BloggerPostsOfTheWeekPor.Size = New System.Drawing.Size(464, 339)
        Me.Xl_BloggerPostsOfTheWeekPor.TabIndex = 1
        '
        'Frm_Bloggers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(475, 435)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Bloggers"
        Me.Text = "Bloggers"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_BloggerPostsOfTheWeekEsp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Xl_BloggerPostsOfTheWeekPor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_Bloggers1 As Winforms.Xl_Bloggers
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_BloggerPostsOfTheWeekEsp As Winforms.Xl_BloggerPostsOfTheWeek
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Xl_BloggerPostsOfTheWeekPor As Xl_BloggerPostsOfTheWeek
End Class
