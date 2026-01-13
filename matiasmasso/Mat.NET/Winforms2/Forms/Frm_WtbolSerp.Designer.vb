<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_WtbolSerp
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxFch = New System.Windows.Forms.TextBox()
        Me.TextBoxIp = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxProduct = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Xl_WtbolSerpItems1 = New Xl_WtbolSerpItems()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TextBoxUserAgent = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_WtbolSerpItems1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Data"
        '
        'TextBoxFch
        '
        Me.TextBoxFch.Location = New System.Drawing.Point(90, 16)
        Me.TextBoxFch.Name = "TextBoxFch"
        Me.TextBoxFch.ReadOnly = True
        Me.TextBoxFch.Size = New System.Drawing.Size(104, 20)
        Me.TextBoxFch.TabIndex = 1
        '
        'TextBoxIp
        '
        Me.TextBoxIp.Location = New System.Drawing.Point(90, 42)
        Me.TextBoxIp.Name = "TextBoxIp"
        Me.TextBoxIp.ReadOnly = True
        Me.TextBoxIp.Size = New System.Drawing.Size(104, 20)
        Me.TextBoxIp.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(16, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Ip"
        '
        'TextBoxProduct
        '
        Me.TextBoxProduct.Location = New System.Drawing.Point(90, 68)
        Me.TextBoxProduct.Name = "TextBoxProduct"
        Me.TextBoxProduct.ReadOnly = True
        Me.TextBoxProduct.Size = New System.Drawing.Size(429, 20)
        Me.TextBoxProduct.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 71)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Producte"
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(525, 139)
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'Xl_WtbolSerpItems1
        '
        Me.Xl_WtbolSerpItems1.AllowUserToAddRows = False
        Me.Xl_WtbolSerpItems1.AllowUserToDeleteRows = False
        Me.Xl_WtbolSerpItems1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_WtbolSerpItems1.DisplayObsolets = False
        Me.Xl_WtbolSerpItems1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_WtbolSerpItems1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_WtbolSerpItems1.MouseIsDown = False
        Me.Xl_WtbolSerpItems1.Name = "Xl_WtbolSerpItems1"
        Me.Xl_WtbolSerpItems1.ReadOnly = True
        Me.Xl_WtbolSerpItems1.Size = New System.Drawing.Size(525, 142)
        Me.Xl_WtbolSerpItems1.TabIndex = 7
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(2, 123)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_WtbolSerpItems1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.PictureBox1)
        Me.SplitContainer1.Size = New System.Drawing.Size(525, 285)
        Me.SplitContainer1.SplitterDistance = 142
        Me.SplitContainer1.TabIndex = 8
        '
        'TextBoxUserAgent
        '
        Me.TextBoxUserAgent.Location = New System.Drawing.Point(90, 94)
        Me.TextBoxUserAgent.Name = "TextBoxUserAgent"
        Me.TextBoxUserAgent.ReadOnly = True
        Me.TextBoxUserAgent.Size = New System.Drawing.Size(429, 20)
        Me.TextBoxUserAgent.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Navegador"
        '
        'Frm_WtbolSerp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(531, 409)
        Me.Controls.Add(Me.TextBoxUserAgent)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.TextBoxProduct)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxIp)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxFch)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_WtbolSerp"
        Me.Text = "Wtbol: Pàgina vista"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_WtbolSerpItems1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxFch As TextBox
    Friend WithEvents TextBoxIp As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxProduct As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Xl_WtbolSerpItems1 As Xl_WtbolSerpItems
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents TextBoxUserAgent As TextBox
    Friend WithEvents Label4 As Label
End Class
