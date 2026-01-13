<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Contact_Correspondencia
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.DataGridViewMails = New System.Windows.Forms.DataGridView()
        Me.ToolStripMails = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButtonRefrescaMails = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonNewMail = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripMemos = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButtonRefrescaMemos = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonNewMemo = New System.Windows.Forms.ToolStripButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_Contact_Correspondencies1 = New Mat.NET.Xl_Contact_Correspondencies()
        Me.Xl_Mems1 = New Mat.NET.Xl_Mems()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.DataGridViewMails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripMails.SuspendLayout()
        Me.ToolStripMemos.SuspendLayout()
        CType(Me.Xl_Mems1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 21)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ToolStripMails)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_Mems1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ToolStripMemos)
        Me.SplitContainer1.Size = New System.Drawing.Size(699, 349)
        Me.SplitContainer1.SplitterDistance = 368
        Me.SplitContainer1.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.DataGridViewMails)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Xl_Contact_Correspondencies1)
        Me.SplitContainer2.Size = New System.Drawing.Size(368, 324)
        Me.SplitContainer2.SplitterDistance = 100
        Me.SplitContainer2.TabIndex = 2
        '
        'DataGridViewMails
        '
        Me.DataGridViewMails.AllowUserToAddRows = False
        Me.DataGridViewMails.AllowUserToDeleteRows = False
        Me.DataGridViewMails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewMails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewMails.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewMails.Name = "DataGridViewMails"
        Me.DataGridViewMails.ReadOnly = True
        Me.DataGridViewMails.Size = New System.Drawing.Size(368, 100)
        Me.DataGridViewMails.TabIndex = 1
        '
        'ToolStripMails
        '
        Me.ToolStripMails.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonRefrescaMails, Me.ToolStripButtonNewMail})
        Me.ToolStripMails.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripMails.Name = "ToolStripMails"
        Me.ToolStripMails.Size = New System.Drawing.Size(368, 25)
        Me.ToolStripMails.TabIndex = 0
        Me.ToolStripMails.Text = "ToolStrip1"
        '
        'ToolStripButtonRefrescaMails
        '
        Me.ToolStripButtonRefrescaMails.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonRefrescaMails.Image = Global.Mat.NET.My.Resources.Resources.refresca
        Me.ToolStripButtonRefrescaMails.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonRefrescaMails.Name = "ToolStripButtonRefrescaMails"
        Me.ToolStripButtonRefrescaMails.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonRefrescaMails.Text = "refresca"
        '
        'ToolStripButtonNewMail
        '
        Me.ToolStripButtonNewMail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonNewMail.Image = Global.Mat.NET.My.Resources.Resources.clip
        Me.ToolStripButtonNewMail.Name = "ToolStripButtonNewMail"
        Me.ToolStripButtonNewMail.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonNewMail.Text = "Nou mail"
        '
        'ToolStripMemos
        '
        Me.ToolStripMemos.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonRefrescaMemos, Me.ToolStripButtonNewMemo})
        Me.ToolStripMemos.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripMemos.Name = "ToolStripMemos"
        Me.ToolStripMemos.Size = New System.Drawing.Size(327, 25)
        Me.ToolStripMemos.TabIndex = 0
        Me.ToolStripMemos.Text = "ToolStrip2"
        '
        'ToolStripButtonRefrescaMemos
        '
        Me.ToolStripButtonRefrescaMemos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonRefrescaMemos.Image = Global.Mat.NET.My.Resources.Resources.refresca
        Me.ToolStripButtonRefrescaMemos.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonRefrescaMemos.Name = "ToolStripButtonRefrescaMemos"
        Me.ToolStripButtonRefrescaMemos.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonRefrescaMemos.Text = "refresca"
        '
        'ToolStripButtonNewMemo
        '
        Me.ToolStripButtonNewMemo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonNewMemo.Image = Global.Mat.NET.My.Resources.Resources.clip
        Me.ToolStripButtonNewMemo.Name = "ToolStripButtonNewMemo"
        Me.ToolStripButtonNewMemo.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonNewMemo.Text = "Nou memo"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "correspondencia"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(659, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "memos"
        '
        'Xl_Contact_Correspondencies1
        '
        Me.Xl_Contact_Correspondencies1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Contact_Correspondencies1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Contact_Correspondencies1.Name = "Xl_Contact_Correspondencies1"
        Me.Xl_Contact_Correspondencies1.Size = New System.Drawing.Size(368, 220)
        Me.Xl_Contact_Correspondencies1.TabIndex = 0
        '
        'Xl_Mems1
        '
        Me.Xl_Mems1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Mems1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Mems1.Filter = Nothing
        Me.Xl_Mems1.Location = New System.Drawing.Point(0, 25)
        Me.Xl_Mems1.Name = "Xl_Mems1"
        Me.Xl_Mems1.Size = New System.Drawing.Size(327, 324)
        Me.Xl_Mems1.TabIndex = 1
        '
        'Frm_Contact_Correspondencia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(699, 370)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Frm_Contact_Correspondencia"
        Me.Text = "CORRESPONDENCIA I MEMOS DE ..."
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.DataGridViewMails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripMails.ResumeLayout(False)
        Me.ToolStripMails.PerformLayout()
        Me.ToolStripMemos.ResumeLayout(False)
        Me.ToolStripMemos.PerformLayout()
        CType(Me.Xl_Mems1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ToolStripMails As System.Windows.Forms.ToolStrip
    Friend WithEvents DataGridViewMails As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStripMemos As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonRefrescaMails As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonNewMail As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonRefrescaMemos As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonNewMemo As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Contact_Correspondencies1 As Mat.NET.Xl_Contact_Correspondencies
    Friend WithEvents Xl_Mems1 As Mat.NET.Xl_Mems
End Class
