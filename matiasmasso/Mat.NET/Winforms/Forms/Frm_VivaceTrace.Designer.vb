<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_VivaceTrace
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
        Me.LabelDelivery = New System.Windows.Forms.Label()
        Me.TextBoxAlb = New System.Windows.Forms.TextBox()
        Me.TextBoxTrp = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox3Tracking = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ButtonBrowse = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_VivaceTrace1 = New Winforms.Xl_VivaceTrace()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_VivaceTrace1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelDelivery
        '
        Me.LabelDelivery.AutoSize = True
        Me.LabelDelivery.Location = New System.Drawing.Point(12, 12)
        Me.LabelDelivery.Name = "LabelDelivery"
        Me.LabelDelivery.Size = New System.Drawing.Size(37, 13)
        Me.LabelDelivery.TabIndex = 1
        Me.LabelDelivery.Text = "Albarà"
        '
        'TextBoxAlb
        '
        Me.TextBoxAlb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxAlb.Location = New System.Drawing.Point(89, 9)
        Me.TextBoxAlb.Name = "TextBoxAlb"
        Me.TextBoxAlb.ReadOnly = True
        Me.TextBoxAlb.Size = New System.Drawing.Size(408, 20)
        Me.TextBoxAlb.TabIndex = 2
        '
        'TextBoxTrp
        '
        Me.TextBoxTrp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTrp.Location = New System.Drawing.Point(89, 35)
        Me.TextBoxTrp.Name = "TextBoxTrp"
        Me.TextBoxTrp.ReadOnly = True
        Me.TextBoxTrp.Size = New System.Drawing.Size(408, 20)
        Me.TextBoxTrp.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Transportista"
        '
        'TextBox3Tracking
        '
        Me.TextBox3Tracking.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox3Tracking.Location = New System.Drawing.Point(89, 61)
        Me.TextBox3Tracking.Name = "TextBox3Tracking"
        Me.TextBox3Tracking.ReadOnly = True
        Me.TextBox3Tracking.Size = New System.Drawing.Size(305, 20)
        Me.TextBox3Tracking.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Tracking #"
        '
        'ButtonBrowse
        '
        Me.ButtonBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonBrowse.Location = New System.Drawing.Point(405, 61)
        Me.ButtonBrowse.Name = "ButtonBrowse"
        Me.ButtonBrowse.Size = New System.Drawing.Size(92, 20)
        Me.ButtonBrowse.TabIndex = 7
        Me.ButtonBrowse.Text = "Seguiment"
        Me.ButtonBrowse.UseVisualStyleBackColor = True
        Me.ButtonBrowse.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Xl_VivaceTrace1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 88)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(497, 200)
        Me.Panel1.TabIndex = 8
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 177)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(497, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 1
        '
        'Xl_VivaceTrace1
        '
        Me.Xl_VivaceTrace1.AllowUserToAddRows = False
        Me.Xl_VivaceTrace1.AllowUserToDeleteRows = False
        Me.Xl_VivaceTrace1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_VivaceTrace1.DisplayObsolets = False
        Me.Xl_VivaceTrace1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_VivaceTrace1.Filter = Nothing
        Me.Xl_VivaceTrace1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_VivaceTrace1.MouseIsDown = False
        Me.Xl_VivaceTrace1.Name = "Xl_VivaceTrace1"
        Me.Xl_VivaceTrace1.ReadOnly = True
        Me.Xl_VivaceTrace1.Size = New System.Drawing.Size(497, 177)
        Me.Xl_VivaceTrace1.TabIndex = 0
        '
        'Frm_VivaceTrace
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(501, 290)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ButtonBrowse)
        Me.Controls.Add(Me.TextBox3Tracking)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxTrp)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxAlb)
        Me.Controls.Add(Me.LabelDelivery)
        Me.Name = "Frm_VivaceTrace"
        Me.Text = "Seguiment enviaments Vivace"
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_VivaceTrace1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_VivaceTrace1 As Xl_VivaceTrace
    Friend WithEvents LabelDelivery As Label
    Friend WithEvents TextBoxAlb As TextBox
    Friend WithEvents TextBoxTrp As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox3Tracking As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents ButtonBrowse As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
