<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_EDiversaOrdrSp
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
        Me.Xl_EdiversaOrdrSps1 = New Xl_EdiversaOrdrSps()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxFch = New System.Windows.Forms.TextBox()
        Me.TextBoxNum = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxPdc = New System.Windows.Forms.TextBox()
        CType(Me.Xl_EdiversaOrdrSps1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_EdiversaOrdrSps1
        '
        Me.Xl_EdiversaOrdrSps1.AllowUserToAddRows = False
        Me.Xl_EdiversaOrdrSps1.AllowUserToDeleteRows = False
        Me.Xl_EdiversaOrdrSps1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_EdiversaOrdrSps1.DisplayObsolets = False
        Me.Xl_EdiversaOrdrSps1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_EdiversaOrdrSps1.Filter = Nothing
        Me.Xl_EdiversaOrdrSps1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_EdiversaOrdrSps1.MouseIsDown = False
        Me.Xl_EdiversaOrdrSps1.Name = "Xl_EdiversaOrdrSps1"
        Me.Xl_EdiversaOrdrSps1.ReadOnly = True
        Me.Xl_EdiversaOrdrSps1.Size = New System.Drawing.Size(685, 404)
        Me.Xl_EdiversaOrdrSps1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(1, 70)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(699, 436)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_EdiversaOrdrSps1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(691, 410)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Detall"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(691, 410)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Missatge Edi"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Data"
        '
        'TextBoxFch
        '
        Me.TextBoxFch.Location = New System.Drawing.Point(48, 22)
        Me.TextBoxFch.Name = "TextBoxFch"
        Me.TextBoxFch.ReadOnly = True
        Me.TextBoxFch.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxFch.TabIndex = 3
        '
        'TextBoxNum
        '
        Me.TextBoxNum.Location = New System.Drawing.Point(222, 22)
        Me.TextBoxNum.Name = "TextBoxNum"
        Me.TextBoxNum.ReadOnly = True
        Me.TextBoxNum.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxNum.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(172, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Numero"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(348, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Comanda"
        '
        'TextBoxPdc
        '
        Me.TextBoxPdc.Location = New System.Drawing.Point(406, 22)
        Me.TextBoxPdc.Name = "TextBoxPdc"
        Me.TextBoxPdc.ReadOnly = True
        Me.TextBoxPdc.Size = New System.Drawing.Size(290, 20)
        Me.TextBoxPdc.TabIndex = 6
        '
        'Frm_EDiversaOrdrSp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(700, 509)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxPdc)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxNum)
        Me.Controls.Add(Me.TextBoxFch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_EDiversaOrdrSp"
        Me.Text = "EDI ORDRSP Confirmació de comanda"
        CType(Me.Xl_EdiversaOrdrSps1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_EdiversaOrdrSps1 As Xl_EdiversaOrdrSps
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxFch As TextBox
    Friend WithEvents TextBoxNum As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxPdc As TextBox
End Class
