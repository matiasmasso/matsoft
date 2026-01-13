<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PqHdr
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
        Me.LabelTrp = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.LabelMgz = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.LabelFch = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelTrp
        '
        Me.LabelTrp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelTrp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelTrp.Location = New System.Drawing.Point(112, 57)
        Me.LabelTrp.Name = "LabelTrp"
        Me.LabelTrp.Size = New System.Drawing.Size(424, 20)
        Me.LabelTrp.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(8, 57)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 16)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Transportista:"
        '
        'LabelMgz
        '
        Me.LabelMgz.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelMgz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelMgz.Location = New System.Drawing.Point(112, 33)
        Me.LabelMgz.Name = "LabelMgz"
        Me.LabelMgz.Size = New System.Drawing.Size(424, 20)
        Me.LabelMgz.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 16)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Magatzem:"
        '
        'LabelFch
        '
        Me.LabelFch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelFch.Location = New System.Drawing.Point(112, 9)
        Me.LabelFch.Name = "LabelFch"
        Me.LabelFch.Size = New System.Drawing.Size(120, 20)
        Me.LabelFch.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 16)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Data:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(1, 90)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(542, 175)
        Me.DataGridView1.TabIndex = 12
        '
        'Frm_PqHdr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(544, 266)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.LabelTrp)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.LabelMgz)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LabelFch)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_PqHdr"
        Me.Text = "REMESA TRANSPORT"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LabelTrp As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents LabelMgz As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LabelFch As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
End Class
