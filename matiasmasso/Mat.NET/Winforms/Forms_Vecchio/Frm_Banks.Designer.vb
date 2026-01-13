<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Banks
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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Xl_Banks1 = New Winforms.Xl_Banks()
        Me.Xl_BankBranches1 = New Winforms.Xl_BankBranches()
        Me.Xl_Countries1 = New Winforms.Xl_Countries()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_Countries1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(583, 261)
        Me.SplitContainer1.SplitterDistance = 130
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Xl_Banks1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Xl_BankBranches1)
        Me.SplitContainer2.Size = New System.Drawing.Size(449, 261)
        Me.SplitContainer2.SplitterDistance = 179
        Me.SplitContainer2.TabIndex = 0
        '
        'Xl_Banks1
        '
        Me.Xl_Banks1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Banks1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Banks1.Name = "Xl_Banks1"
        Me.Xl_Banks1.Size = New System.Drawing.Size(179, 261)
        Me.Xl_Banks1.TabIndex = 0
        '
        'Xl_BankBranches1
        '
        Me.Xl_BankBranches1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_BankBranches1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_BankBranches1.Name = "Xl_BankBranches1"
        Me.Xl_BankBranches1.Size = New System.Drawing.Size(266, 261)
        Me.Xl_BankBranches1.TabIndex = 0
        '
        'Xl_Countries1
        '
        Me.Xl_Countries1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Countries1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Countries1.Name = "Xl_Countries1"
        Me.Xl_Countries1.Size = New System.Drawing.Size(130, 261)
        Me.Xl_Countries1.TabIndex = 0
        '
        'Frm_Banks
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(583, 261)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Frm_Banks"
        Me.Text = "Entitats Bancàries"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Banks1 As Winforms.Xl_Banks
    Friend WithEvents Xl_BankBranches1 As Winforms.Xl_BankBranches
    Friend WithEvents Xl_Countries1 As Winforms.Xl_Countries
End Class
