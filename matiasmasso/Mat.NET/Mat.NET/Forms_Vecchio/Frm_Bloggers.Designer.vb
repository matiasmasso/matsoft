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
        Me.Xl_Bloggers1 = New Mat.NET.Xl_Bloggers()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_BloggerPostsOfTheWeek1 = New Mat.NET.Xl_BloggerPostsOfTheWeek()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_BloggerPostsOfTheWeek1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_Bloggers1
        '
        Me.Xl_Bloggers1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Bloggers1.Location = New System.Drawing.Point(-2, 6)
        Me.Xl_Bloggers1.Name = "Xl_Bloggers1"
        Me.Xl_Bloggers1.Size = New System.Drawing.Size(465, 370)
        Me.Xl_Bloggers1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 33)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(473, 402)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_Bloggers1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(465, 376)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Bloggers"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_BloggerPostsOfTheWeek1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(465, 376)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Posts destacats"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_BloggerPostsOfTheWeek1
        '
        Me.Xl_BloggerPostsOfTheWeek1.AllowUserToAddRows = False
        Me.Xl_BloggerPostsOfTheWeek1.AllowUserToDeleteRows = False
        Me.Xl_BloggerPostsOfTheWeek1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_BloggerPostsOfTheWeek1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_BloggerPostsOfTheWeek1.Filter = Nothing
        Me.Xl_BloggerPostsOfTheWeek1.Location = New System.Drawing.Point(0, 37)
        Me.Xl_BloggerPostsOfTheWeek1.Name = "Xl_BloggerPostsOfTheWeek1"
        Me.Xl_BloggerPostsOfTheWeek1.ReadOnly = True
        Me.Xl_BloggerPostsOfTheWeek1.Size = New System.Drawing.Size(465, 339)
        Me.Xl_BloggerPostsOfTheWeek1.TabIndex = 0
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
        CType(Me.Xl_BloggerPostsOfTheWeek1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_Bloggers1 As Mat.NET.Xl_Bloggers
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_BloggerPostsOfTheWeek1 As Mat.NET.Xl_BloggerPostsOfTheWeek
End Class
