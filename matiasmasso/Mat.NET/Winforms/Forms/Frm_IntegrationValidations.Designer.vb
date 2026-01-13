<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_IntegrationValidations
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TextBoxRequest = New System.Windows.Forms.TextBox()
        Me.TextBoxResponse = New System.Windows.Forms.TextBox()
        Me.ButtonPost = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxUrl = New System.Windows.Forms.TextBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.RadioButtonLocal = New System.Windows.Forms.RadioButton()
        Me.RadioButtonRemote = New System.Windows.Forms.RadioButton()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(1, 43)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(799, 407)
        Me.Panel1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(799, 384)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.RadioButtonRemote)
        Me.TabPage1.Controls.Add(Me.RadioButtonLocal)
        Me.TabPage1.Controls.Add(Me.SplitContainer1)
        Me.TabPage1.Controls.Add(Me.ButtonPost)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.TextBoxUrl)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(791, 358)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Comanda Json"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Location = New System.Drawing.Point(10, 92)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxRequest)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxResponse)
        Me.SplitContainer1.Size = New System.Drawing.Size(762, 231)
        Me.SplitContainer1.SplitterDistance = 361
        Me.SplitContainer1.TabIndex = 5
        '
        'TextBoxRequest
        '
        Me.TextBoxRequest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxRequest.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxRequest.Multiline = True
        Me.TextBoxRequest.Name = "TextBoxRequest"
        Me.TextBoxRequest.Size = New System.Drawing.Size(361, 231)
        Me.TextBoxRequest.TabIndex = 1
        '
        'TextBoxResponse
        '
        Me.TextBoxResponse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxResponse.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxResponse.Multiline = True
        Me.TextBoxResponse.Name = "TextBoxResponse"
        Me.TextBoxResponse.Size = New System.Drawing.Size(397, 231)
        Me.TextBoxResponse.TabIndex = 2
        '
        'ButtonPost
        '
        Me.ButtonPost.Location = New System.Drawing.Point(697, 329)
        Me.ButtonPost.Name = "ButtonPost"
        Me.ButtonPost.Size = New System.Drawing.Size(75, 23)
        Me.ButtonPost.TabIndex = 4
        Me.ButtonPost.Text = "Post"
        Me.ButtonPost.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Json data:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(21, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "url:"
        '
        'TextBoxUrl
        '
        Me.TextBoxUrl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxUrl.Location = New System.Drawing.Point(7, 31)
        Me.TextBoxUrl.Name = "TextBoxUrl"
        Me.TextBoxUrl.Size = New System.Drawing.Size(765, 20)
        Me.TextBoxUrl.TabIndex = 0
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 384)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(799, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        Me.ProgressBar1.Visible = False
        '
        'RadioButtonLocal
        '
        Me.RadioButtonLocal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadioButtonLocal.AutoSize = True
        Me.RadioButtonLocal.Checked = True
        Me.RadioButtonLocal.Location = New System.Drawing.Point(603, 7)
        Me.RadioButtonLocal.Name = "RadioButtonLocal"
        Me.RadioButtonLocal.Size = New System.Drawing.Size(63, 17)
        Me.RadioButtonLocal.TabIndex = 6
        Me.RadioButtonLocal.TabStop = True
        Me.RadioButtonLocal.Text = "Url local"
        Me.RadioButtonLocal.UseVisualStyleBackColor = True
        '
        'RadioButtonRemote
        '
        Me.RadioButtonRemote.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadioButtonRemote.AutoSize = True
        Me.RadioButtonRemote.Location = New System.Drawing.Point(699, 8)
        Me.RadioButtonRemote.Name = "RadioButtonRemote"
        Me.RadioButtonRemote.Size = New System.Drawing.Size(73, 17)
        Me.RadioButtonRemote.TabIndex = 7
        Me.RadioButtonRemote.Text = "Url remota"
        Me.RadioButtonRemote.UseVisualStyleBackColor = True
        '
        'Frm_IntegrationValidations
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_IntegrationValidations"
        Me.Text = "Frm_IntegrationValidations"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents ButtonPost As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxRequest As TextBox
    Friend WithEvents TextBoxUrl As TextBox
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents TextBoxResponse As TextBox
    Friend WithEvents RadioButtonRemote As RadioButton
    Friend WithEvents RadioButtonLocal As RadioButton
End Class
