<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_BancTransferPool
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
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TextBoxFch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxBancEmissor = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxCca = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxTotal = New System.Windows.Forms.TextBox()
        Me.Xl_BancTransferPools1 = New Winforms.Xl_BancTransferPools()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxRef = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.Xl_BancTransferPools1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 966)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1280, 74)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(696, 10)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(277, 57)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(992, 10)
        Me.ButtonOk.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(277, 57)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(16, 10)
        Me.ButtonDel.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(277, 57)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'TextBoxFch
        '
        Me.TextBoxFch.Location = New System.Drawing.Point(325, 95)
        Me.TextBoxFch.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxFch.Name = "TextBoxFch"
        Me.TextBoxFch.ReadOnly = True
        Me.TextBoxFch.Size = New System.Drawing.Size(260, 38)
        Me.TextBoxFch.TabIndex = 43
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(69, 110)
        Me.Label1.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 32)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Data"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(69, 172)
        Me.Label2.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(186, 32)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "Banc emissor"
        '
        'TextBoxBancEmissor
        '
        Me.TextBoxBancEmissor.Location = New System.Drawing.Point(325, 157)
        Me.TextBoxBancEmissor.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxBancEmissor.Name = "TextBoxBancEmissor"
        Me.TextBoxBancEmissor.ReadOnly = True
        Me.TextBoxBancEmissor.Size = New System.Drawing.Size(260, 38)
        Me.TextBoxBancEmissor.TabIndex = 45
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(16, 5, 0, 5)
        Me.MenuStrip1.Size = New System.Drawing.Size(1280, 55)
        Me.MenuStrip1.TabIndex = 47
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(12, 45)
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(97, 45)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(69, 296)
        Me.Label3.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(181, 32)
        Me.Label3.TabIndex = 49
        Me.Label3.Text = "Assentament"
        '
        'TextBoxCca
        '
        Me.TextBoxCca.Location = New System.Drawing.Point(325, 281)
        Me.TextBoxCca.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxCca.Name = "TextBoxCca"
        Me.TextBoxCca.ReadOnly = True
        Me.TextBoxCca.Size = New System.Drawing.Size(863, 38)
        Me.TextBoxCca.TabIndex = 48
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(69, 234)
        Me.Label4.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 32)
        Me.Label4.TabIndex = 51
        Me.Label4.Text = "Total"
        '
        'TextBoxTotal
        '
        Me.TextBoxTotal.Location = New System.Drawing.Point(325, 219)
        Me.TextBoxTotal.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxTotal.Name = "TextBoxTotal"
        Me.TextBoxTotal.ReadOnly = True
        Me.TextBoxTotal.Size = New System.Drawing.Size(260, 38)
        Me.TextBoxTotal.TabIndex = 50
        '
        'Xl_BancTransferPools1
        '
        Me.Xl_BancTransferPools1.AllowUserToAddRows = False
        Me.Xl_BancTransferPools1.AllowUserToDeleteRows = False
        Me.Xl_BancTransferPools1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_BancTransferPools1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_BancTransferPools1.DisplayObsolets = False
        Me.Xl_BancTransferPools1.Filter = Nothing
        Me.Xl_BancTransferPools1.Location = New System.Drawing.Point(0, 429)
        Me.Xl_BancTransferPools1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_BancTransferPools1.Name = "Xl_BancTransferPools1"
        Me.Xl_BancTransferPools1.ReadOnly = True
        Me.Xl_BancTransferPools1.Size = New System.Drawing.Size(1280, 532)
        Me.Xl_BancTransferPools1.TabIndex = 52
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(69, 351)
        Me.Label5.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(142, 32)
        Me.Label5.TabIndex = 53
        Me.Label5.Text = "referencia"
        '
        'TextBoxRef
        '
        Me.TextBoxRef.Location = New System.Drawing.Point(325, 343)
        Me.TextBoxRef.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxRef.Name = "TextBoxRef"
        Me.TextBoxRef.Size = New System.Drawing.Size(260, 38)
        Me.TextBoxRef.TabIndex = 54
        '
        'Frm_BancTransferPool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1280, 1040)
        Me.Controls.Add(Me.TextBoxRef)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Xl_BancTransferPools1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxTotal)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxCca)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxBancEmissor)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxFch)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Name = "Frm_BancTransferPool"
        Me.Text = "Transferencia bancaria"
        Me.Panel1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.Xl_BancTransferPools1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents TextBoxFch As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxBancEmissor As TextBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxCca As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxTotal As TextBox
    Friend WithEvents Xl_BancTransferPools1 As Xl_BancTransferPools
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxRef As TextBox
End Class
