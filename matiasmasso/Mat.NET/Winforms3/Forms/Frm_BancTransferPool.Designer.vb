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
        Me.Xl_BancTransferPools1 = New Mat.Net.Xl_BancTransferPools()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxRef = New System.Windows.Forms.TextBox()
        Me.HelpProviderHG = New System.Windows.Forms.HelpProvider()
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
        Me.Panel1.Location = New System.Drawing.Point(0, 263)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(480, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonCancel, "Frm_BancTransferPool.htm#ButtonCancel")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonCancel, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonCancel.Location = New System.Drawing.Point(261, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonCancel, True)
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonOk, "Frm_BancTransferPool.htm#ButtonOk")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonOk, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonOk.Location = New System.Drawing.Point(372, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonOk, True)
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonDel, "Frm_BancTransferPool.htm#ButtonDel")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonDel, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonDel, True)
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'TextBoxFch
        '
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBoxFch, "Frm_BancTransferPool.htm#Label1")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBoxFch, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBoxFch.Location = New System.Drawing.Point(122, 40)
        Me.TextBoxFch.Name = "TextBoxFch"
        Me.TextBoxFch.ReadOnly = True
        Me.HelpProviderHG.SetShowHelp(Me.TextBoxFch, True)
        Me.TextBoxFch.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxFch.TabIndex = 43
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Data"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(26, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "Banc emissor"
        '
        'TextBoxBancEmissor
        '
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBoxBancEmissor, "Frm_BancTransferPool.htm#Label2")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBoxBancEmissor, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBoxBancEmissor.Location = New System.Drawing.Point(122, 66)
        Me.TextBoxBancEmissor.Name = "TextBoxBancEmissor"
        Me.TextBoxBancEmissor.ReadOnly = True
        Me.HelpProviderHG.SetShowHelp(Me.TextBoxBancEmissor, True)
        Me.TextBoxBancEmissor.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxBancEmissor.TabIndex = 45
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(480, 24)
        Me.MenuStrip1.TabIndex = 47
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(12, 20)
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(26, 124)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 49
        Me.Label3.Text = "Assentament"
        '
        'TextBoxCca
        '
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBoxCca, "Frm_BancTransferPool.htm#Label3")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBoxCca, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBoxCca.Location = New System.Drawing.Point(122, 118)
        Me.TextBoxCca.Name = "TextBoxCca"
        Me.TextBoxCca.ReadOnly = True
        Me.HelpProviderHG.SetShowHelp(Me.TextBoxCca, True)
        Me.TextBoxCca.Size = New System.Drawing.Size(326, 20)
        Me.TextBoxCca.TabIndex = 48
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(26, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 51
        Me.Label4.Text = "Total"
        '
        'TextBoxTotal
        '
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBoxTotal, "Frm_BancTransferPool.htm#Label4")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBoxTotal, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBoxTotal.Location = New System.Drawing.Point(122, 92)
        Me.TextBoxTotal.Name = "TextBoxTotal"
        Me.TextBoxTotal.ReadOnly = True
        Me.HelpProviderHG.SetShowHelp(Me.TextBoxTotal, True)
        Me.TextBoxTotal.Size = New System.Drawing.Size(100, 20)
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
        Me.HelpProviderHG.SetHelpKeyword(Me.Xl_BancTransferPools1, "Frm_BancTransferPool.htm#Xl_BancTransferPools1")
        Me.HelpProviderHG.SetHelpNavigator(Me.Xl_BancTransferPools1, System.Windows.Forms.HelpNavigator.Topic)
        Me.Xl_BancTransferPools1.Location = New System.Drawing.Point(0, 180)
        Me.Xl_BancTransferPools1.MouseIsDown = False
        Me.Xl_BancTransferPools1.Name = "Xl_BancTransferPools1"
        Me.Xl_BancTransferPools1.ReadOnly = True
        Me.HelpProviderHG.SetShowHelp(Me.Xl_BancTransferPools1, True)
        Me.Xl_BancTransferPools1.Size = New System.Drawing.Size(480, 223)
        Me.Xl_BancTransferPools1.TabIndex = 52
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(26, 147)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 13)
        Me.Label5.TabIndex = 53
        Me.Label5.Text = "referencia"
        '
        'TextBoxRef
        '
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBoxRef, "Frm_BancTransferPool.htm#Label5")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBoxRef, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBoxRef.Location = New System.Drawing.Point(122, 144)
        Me.TextBoxRef.Name = "TextBoxRef"
        Me.HelpProviderHG.SetShowHelp(Me.TextBoxRef, True)
        Me.TextBoxRef.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxRef.TabIndex = 54
        '
        'HelpProviderHG
        '
        Me.HelpProviderHG.HelpNamespace = "MatNET.chm"
        '
        'Frm_BancTransferPool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(480, 294)
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
        Me.HelpProviderHG.SetHelpKeyword(Me, "Frm_BancTransferPool.htm")
        Me.HelpProviderHG.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_BancTransferPool"
        Me.HelpProviderHG.SetShowHelp(Me, True)
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
    Friend WithEvents HelpProviderHG As HelpProvider
End Class
